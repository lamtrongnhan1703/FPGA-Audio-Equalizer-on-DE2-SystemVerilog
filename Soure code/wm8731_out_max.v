module wm8731_out_max (
    input  wire        clk50,      // 50 MHz
    input  wire        rst_n,      // reset active-low

    // 24-bit sample input (2's complement signed). Latch mỗi frame (mỗi sample)
    input  wire [23:0] sample_in,

    // ===== WM8731 pins =====
    output wire        aud_xck,      // MCLK
    output wire        aud_bclk,     // BCLK
    output reg         aud_daclrck,  // LRCLK (DACLRCK)
    output reg         aud_dacdat,   // DACDAT
    inout  wire        i2c_sda,      // WM8731 SDIN (I2C SDA)
    output wire        i2c_scl       // WM8731 SCLK (I2C SCL)
);

    // ============================================================
    // 1) Clock generation (no PLL): Fs ≈ 50MHz/1024 = 48.828125kHz
    //    MCLK = 256*Fs = 12.5MHz  (50MHz/4)
    //    BCLK = 64*Fs  = 3.125MHz (50MHz/16)
    // ============================================================

    // MCLK = 12.5MHz: toggle mỗi 2 clk50 (period 4)
    reg [1:0] mclk_div = 2'd0;
    reg       mclk = 1'b0;
    always @(posedge clk50 or negedge rst_n) begin
        if (!rst_n) begin
            mclk_div <= 2'd0;
            mclk     <= 1'b0;
        end else begin
            mclk_div <= mclk_div + 1'b1;
            if (mclk_div == 2'd1) begin
                mclk_div <= 2'd0;
                mclk     <= ~mclk;
            end
        end
    end
    assign aud_xck = mclk;

    // BCLK = 3.125MHz: toggle mỗi 8 clk50 (period 16)
    reg [3:0] bclk_div = 4'd0;
    reg       bclk = 1'b0;
    always @(posedge clk50 or negedge rst_n) begin
        if (!rst_n) begin
            bclk_div <= 4'd0;
            bclk     <= 1'b0;
        end else begin
            bclk_div <= bclk_div + 1'b1;
            if (bclk_div == 4'd7) begin
                bclk_div <= 4'd0;
                bclk     <= ~bclk;
            end
        end
    end
    assign aud_bclk = bclk;

    // ============================================================
    // 2) I2C init sequencer using I2C_Protocol.v
    //    I2C_Protocol thực hiện 1 transaction khi ignition=1,
    //    và finish_flag báo xong.
    // ============================================================

    // Pack format for WM8731: 16-bit = {reg[6:0], data[8], data[7:0]}
    function [15:0] wm_pack;
        input [6:0] reg7;
        input [8:0] data9;
        begin
            wm_pack = {reg7, data9[8], data9[7:0]};
        end
    endfunction

    // Danh sách init tối thiểu để DAC ra được tín hiệu I2S 24-bit
    // Lưu ý: giá trị có thể tùy theo bạn muốn dùng line-out/headphone, volume...
    localparam integer INIT_N = 11;
    reg [15:0] init_word [0:INIT_N-1];

        initial begin
        init_word[0]  = wm_pack(7'h0F, 9'h000); // R15 Reset

        // Line In volume (ADC input gain) - set high so ADC sees strong signal
        // R0/R1: LINVOL/RINVOL (0..31). Here set max (31).
        init_word[1]  = wm_pack(7'h00, 9'h01F); // Left Line In Vol = max
        init_word[2]  = wm_pack(7'h01, 9'h01F); // Right Line In Vol = max

        // R6 Power Down: 0 = power up all blocks (ADC + DAC + LineIn)
        // Trước đây bạn để 0x007 => tắt ADC/LineIn nên sẽ luôn đọc "im lặng".
        init_word[3]  = wm_pack(7'h06, 9'h000);

        // R4 Analogue Audio Path: DACSEL=1 (bật DAC), BYPASS=0, INSEL=0 (LINEIN), MUTEMIC=1
        init_word[4]  = wm_pack(7'h04, 9'h012);

        // R5 Digital Audio Path: DACMU=0 (unmute)
        init_word[5]  = wm_pack(7'h05, 9'h000);

        // R7 Digital Audio Interface: I2S + 24-bit + slave => 0x0A
        init_word[6]  = wm_pack(7'h07, 9'h00A);

        // R8 Sampling Control: normal, 256fs, SR=0000 (default) => 0x00
        init_word[7]  = wm_pack(7'h08, 9'h000);

        // Headphone volume max
        init_word[8]  = wm_pack(7'h02, 9'h07F); // LHPVOL MAX
        init_word[9]  = wm_pack(7'h03, 9'h07F); // RHPVOL MAX

        // R9 Active: ACTIVE=1
        init_word[10] = wm_pack(7'h09, 9'h001);
    end

    reg        ignition;
    reg [15:0] mux_input;
    wire       finish_flag;
    wire [2:0] ACK_unused;
    reg [3:0]  init_idx;
    reg        init_done;

    // I2C_Protocol: reset trong module này là active-high enable (nhưng trong code nó reset khi !reset)
    // Vì vậy ta đưa reset = rst_n (1 là chạy, 0 là reset)
    I2C_Protocol u_i2c (
        .clk        (clk50),
        .reset      (rst_n),
        .ignition   (ignition),
        .MUX_input  (mux_input),
        .SDIN       (i2c_sda),
        .finish_flag(finish_flag),
        .ACK        (ACK_unused),
        .SCLK       (i2c_scl)
    );

    // Init FSM: bắn từng word, đợi finish_flag, rồi bắn word tiếp theo
    reg finish_d;
    always @(posedge clk50 or negedge rst_n) begin
        if (!rst_n) begin
            ignition   <= 1'b0;
            mux_input  <= 16'h0000;
            init_idx   <= 4'd0;
            init_done  <= 1'b0;
            finish_d   <= 1'b0;
        end else begin
            finish_d <= finish_flag;

            if (!init_done) begin
                // nếu đang idle -> bật ignition và nạp word hiện tại
                if (!ignition) begin
                    mux_input <= init_word[init_idx];
                    ignition  <= 1'b1;
                end

                // bắt cạnh lên finish_flag (kết thúc 1 transaction)
                if (!finish_d && finish_flag) begin
                    ignition <= 1'b0; // tắt để tránh module I2C tự lặp tiếp

                    if (init_idx == INIT_N-1) begin
                        init_done <= 1'b1;
                        init_idx  <= 4'd0;
                    end else begin
                        init_idx <= init_idx + 1'b1;
                    end
                end
            end else begin
                ignition <= 1'b0; // giữ I2C im khi đã init xong
            end
        end
    end

    // ============================================================
    // 3) I2S transmitter (mono -> L/R), 24-bit, padded to 32-bit
    //    Standard I2S: LRCLK đổi trước MSB 1 bit-clock (bit_cnt==0)
    //    Data valid trước rising edge => shift trên negedge BCLK
    // ============================================================

    reg [5:0]  bit_cnt;
    reg [23:0] sample_lat;

    // latch sample tại đầu frame (trước kênh LEFT)
    // bit_cnt==0 tương ứng "1 bit delay" theo I2S, MSB bắt đầu ở bit_cnt==1
    always @(negedge bclk or negedge rst_n) begin
        if (!rst_n) begin
            bit_cnt    <= 6'd0;
            aud_daclrck<= 1'b0;
            aud_dacdat <= 1'b0;
            sample_lat <= 24'sd0;
        end else begin
            // advance bit counter 0..63
            if (bit_cnt == 6'd63)
                bit_cnt <= 6'd0;
            else
                bit_cnt <= bit_cnt + 1'b1;

            // LRCLK control (0=Left, 1=Right)
            if (bit_cnt == 6'd0) begin
                aud_daclrck <= 1'b0;   // LEFT
                sample_lat  <= sample_in; // latch mới mỗi sample/frame
            end else if (bit_cnt == 6'd32) begin
                aud_daclrck <= 1'b1;   // RIGHT
            end

            // Data output
            if (!init_done) begin
                aud_dacdat <= 1'b0; // im lặng trước khi init xong
            end else begin
                // LEFT channel data bits: bit_cnt 1..24 => sample[23]..sample[0]
                if (bit_cnt >= 6'd1 && bit_cnt <= 6'd24) begin
                    aud_dacdat <= sample_lat[24 - bit_cnt]; // 1->23, 24->0
                end
                // LEFT padding: 25..32 => 0
                else if (bit_cnt >= 6'd25 && bit_cnt <= 6'd32) begin
                    aud_dacdat <= 1'b0;
                end
                // RIGHT channel data bits: 33..56 => sample[23]..sample[0]
                else if (bit_cnt >= 6'd33 && bit_cnt <= 6'd56) begin
                    aud_dacdat <= sample_lat[56 - bit_cnt]; // 33->23, 56->0
                end
                // RIGHT padding: 57..63 + 0 => 0
                else begin
                    aud_dacdat <= 1'b0;
                end
            end
        end
    end

endmodule
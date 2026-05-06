module wm8731_out_runtime_vol (
    input  wire        clk50,
    input  wire        rst_n,
    input  wire [23:0] sample_in,

    // runtime volume control from UART/parser
    input  wire [6:0]  hp_vol_code,     // WM8731 code: 7'h30..7'h7F, <7'h30 => mute
    input  wire        hp_vol_update,   // 1 pulse clk50 to request volume update
    output reg         hp_vol_busy,     // high while I2C is updating volume

    // ===== WM8731 pins =====
    output wire        aud_xck,
    output wire        aud_bclk,
    output reg         aud_daclrck,
    output reg         aud_dacdat,
    inout  wire        i2c_sda,
    output wire        i2c_scl
);

    // ============================================================
    // 1) Clock generation (no PLL): Fs ≈ 50MHz/1024 = 48.828125kHz
    //    MCLK = 256*Fs = 12.5MHz  (50MHz/4)
    //    BCLK = 64*Fs  = 3.125MHz (50MHz/16)
    // ============================================================
    reg [1:0] mclk_div;
    reg       mclk;
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

    reg [3:0] bclk_div;
    reg       bclk;
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
    // 2) I2C init + runtime headphone-volume update
    // ============================================================
    function [15:0] wm_pack;
        input [6:0] reg7;
        input [8:0] data9;
        begin
            wm_pack = {reg7, data9[8], data9[7:0]};
        end
    endfunction

    // register 0x02: Left Headphone Out
    // bit8  = LRHPBOTH (1 => write left, copy to right too)
    // bit7  = LZCEN
    // bit6:0= LHPVOL
    function [15:0] wm_hp_both_word;
        input [6:0] vol;
        begin
            wm_hp_both_word = wm_pack(7'h02, {1'b1, 1'b0, vol});
        end
    endfunction

    localparam integer INIT_N = 10;
    reg [15:0] init_word [0:INIT_N-1];
    initial begin
        init_word[0] = wm_pack(7'h0F, 9'h000); // reset
        init_word[1] = wm_pack(7'h00, 9'h017); // line in left = 0dB
        init_word[2] = wm_pack(7'h01, 9'h017); // line in right = 0dB
        init_word[3] = wm_pack(7'h06, 9'h000); // power up all
        init_word[4] = wm_pack(7'h04, 9'h012); // DAC select, line input path settings
        init_word[5] = wm_pack(7'h05, 9'h000); // DAC unmute
        init_word[6] = wm_pack(7'h07, 9'h00A); // I2S, 24-bit, slave
        init_word[7] = wm_pack(7'h08, 9'h000); // normal sample control
        init_word[8] = wm_hp_both_word(7'h79); // headphone 0dB default, update both L/R
        init_word[9] = wm_pack(7'h09, 9'h001); // ACTIVE=1
    end

    reg        ignition;
    reg [15:0] mux_input;
    wire       finish_flag;
    wire [2:0] ack_unused;
    reg  [3:0] init_idx;
    reg        init_done;
    reg        finish_d;

    reg        vol_req_pending;
    reg  [6:0] vol_shadow;

    I2C_Protocol u_i2c (
        .clk        (clk50),
        .reset      (rst_n),
        .ignition   (ignition),
        .MUX_input  (mux_input),
        .SDIN       (i2c_sda),
        .finish_flag(finish_flag),
        .ACK        (ack_unused),
        .SCLK       (i2c_scl)
    );

    always @(posedge clk50 or negedge rst_n) begin
        if (!rst_n) begin
            ignition        <= 1'b0;
            mux_input       <= 16'h0000;
            init_idx        <= 4'd0;
            init_done       <= 1'b0;
            finish_d        <= 1'b0;
            vol_req_pending <= 1'b0;
            vol_shadow      <= 7'h79;
            hp_vol_busy     <= 1'b0;
        end else begin
            finish_d <= finish_flag;

            // latch latest request
            if (hp_vol_update) begin
                vol_shadow      <= hp_vol_code;
                vol_req_pending <= 1'b1;
            end

            if (!init_done) begin
                hp_vol_busy <= 1'b1;

                if (!ignition) begin
                    mux_input <= init_word[init_idx];
                    ignition  <= 1'b1;
                end

                if (!finish_d && finish_flag) begin
                    ignition <= 1'b0;
                    if (init_idx == INIT_N-1) begin
                        init_done <= 1'b1;
                        init_idx  <= 4'd0;
                    end else begin
                        init_idx <= init_idx + 1'b1;
                    end
                end
            end else begin
                // runtime volume update: 1 I2C write to reg 0x02 with LRHPBOTH=1
                if (vol_req_pending && !ignition) begin
                    mux_input       <= wm_hp_both_word(vol_shadow);
                    ignition        <= 1'b1;
                    vol_req_pending <= 1'b0;
                    hp_vol_busy     <= 1'b1;
                end else if (!finish_d && finish_flag) begin
                    ignition    <= 1'b0;
                    hp_vol_busy <= 1'b0;
                end else if (!ignition) begin
                    hp_vol_busy <= 1'b0;
                end
            end
        end
    end

    // ============================================================
    // 3) I2S transmitter (mono -> L/R), 24-bit, padded to 32-bit
    // ============================================================
    reg [5:0]  bit_cnt;
    reg [23:0] sample_lat;

    always @(negedge bclk or negedge rst_n) begin
        if (!rst_n) begin
            bit_cnt     <= 6'd0;
            aud_daclrck <= 1'b0;
            aud_dacdat  <= 1'b0;
            sample_lat  <= 24'd0;
        end else begin
            if (bit_cnt == 6'd63)
                bit_cnt <= 6'd0;
            else
                bit_cnt <= bit_cnt + 1'b1;

            if (bit_cnt == 6'd0) begin
                aud_daclrck <= 1'b0;
                sample_lat  <= sample_in;
            end else if (bit_cnt == 6'd32) begin
                aud_daclrck <= 1'b1;
            end

            if (!init_done) begin
                aud_dacdat <= 1'b0;
            end else begin
                if (bit_cnt >= 6'd1 && bit_cnt <= 6'd24)
                    aud_dacdat <= sample_lat[24 - bit_cnt];
                else if (bit_cnt >= 6'd33 && bit_cnt <= 6'd56)
                    aud_dacdat <= sample_lat[56 - bit_cnt];
                else
                    aud_dacdat <= 1'b0;
            end
        end
    end
endmodule

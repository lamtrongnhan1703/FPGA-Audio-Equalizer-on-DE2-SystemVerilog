module wm8731_in (
    input  wire        rst_n,         // reset active-low
    input  wire        bclk,          // BCLK
    input  wire        lrck,          // ADCLRC / LRCK
    input  wire        adcdat,        // ADCDAT từ WM8731

    output reg  [23:0] left_sample,   // 24-bit signed
    output reg  [23:0] right_sample,  // 24-bit signed
    output reg         left_valid,    // 1 xung bclk khi vừa chốt xong LEFT
    output reg         right_valid,   // 1 xung bclk khi vừa chốt xong RIGHT

    output reg  [23:0] sample_data,   // mẫu đưa sang FIR, hiện lấy từ LEFT
    output reg         sample_valid   // 1 xung bclk khi sample_data hợp lệ
);

    // =========================================================
    // Đồng bộ LRCK để bắt cạnh
    // =========================================================
    reg lrck_d;

    always @(posedge bclk or negedge rst_n) begin
        if (!rst_n)
            lrck_d <= 1'b0;
        else
            lrck_d <= lrck;
    end

    wire lrck_edge = (lrck_d ^ lrck);

    // =========================================================
    // bit_cnt:
    //   I2S có 1-bit delay sau khi LRCK đổi trạng thái
    //   dữ liệu 24-bit nằm ở các nhịp 1..24
    // =========================================================
    reg [5:0]  bit_cnt;
    reg [23:0] shift24;
    reg        chan;      // 0 = LEFT, 1 = RIGHT
    reg [23:0] sample_now;

    always @(posedge bclk or negedge rst_n) begin
        if (!rst_n) begin
            bit_cnt      <= 6'd0;
            shift24      <= 24'd0;
            chan         <= 1'b0;

            left_sample  <= 24'd0;
            right_sample <= 24'd0;
            left_valid   <= 1'b0;
            right_valid  <= 1'b0;

            sample_data  <= 24'd0;
            sample_valid <= 1'b0;

            sample_now   <= 24'd0;
        end else begin
            // mặc định pulse valid về 0
            left_valid   <= 1'b0;
            right_valid  <= 1'b0;
            sample_valid <= 1'b0;

            // Khi LRCK đổi trạng thái => bắt đầu nửa frame mới
            if (lrck_edge) begin
                bit_cnt <= 6'd0;
                chan    <= lrck;   // quy ước: lrck=0 LEFT, lrck=1 RIGHT
            end else begin
                bit_cnt <= bit_cnt + 6'd1;
            end

            // Nhận dữ liệu 24-bit ở các nhịp 1..24
            if ((bit_cnt >= 6'd1) && (bit_cnt <= 6'd24)) begin
                // bit_cnt=1 -> MSB [23]
                shift24[24 - bit_cnt] <= adcdat;
            end

            // Khi vừa nhận xong bit cuối cùng (LSB)
            if (bit_cnt == 6'd24) begin
                sample_now = {shift24[23:1], adcdat};

                if (chan == 1'b0) begin
                    // LEFT
                    left_sample  <= sample_now;
                    left_valid   <= 1'b1;

                    // sample dùng cho FIR: lấy từ LEFT
                    sample_data  <= sample_now;
                    sample_valid <= 1'b1;
                end else begin
                    // RIGHT
                    right_sample <= sample_now;
                    right_valid  <= 1'b1;
                end
            end
        end
    end

endmodule
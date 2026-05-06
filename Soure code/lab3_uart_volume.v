module lab3_uart_volume(
    input  wire clk50,
    input  wire rst_n,
    input  wire aud_adcDat,
    input  wire uart_rx_pin,
    output wire led_stt,
    output wire aud_xck,
    output wire aud_bclk,
    output wire aud_dacLrck,
    output wire aud_i2c_sda,
    output wire aud_i2c_scl,
    output wire aud_dacDat,
    output wire aud_adc_lrck,
    output wire uart_tx_pin
);

    wire        bclk_w;
    wire        lrck_w;
    wire [23:0] adc_left_sample;

    assign aud_bclk     = bclk_w;
    assign aud_dacLrck  = lrck_w;
    assign aud_adc_lrck = lrck_w;

    // ------------------------------------------------------------
    // Audio in
    // ------------------------------------------------------------
    wm8731_in u_audio_in(
        .rst_n      (rst_n),
        .bclk       (bclk_w),
        .lrck       (lrck_w),
        .adcdat     (aud_adcDat),
        .left_sample(adc_left_sample)
    );

    // ------------------------------------------------------------
    // UART RX: demo step 1 = receive exactly 1 byte volume code
    // PC should send one raw byte 0x30..0x7F.
    //   0x79 = 0dB
    //   0x7F = +6dB
    //   0x30 = -73dB
    //   0x00..0x2F = mute
    // ------------------------------------------------------------
    wire [7:0] rx_byte;
    wire       rx_valid;

    uart_rx #(
        .BAUD_DIV(5208) // 50MHz / 9600 ~= 5208
    ) u_uart_rx (
        .clk     (clk50),
        .rst_n   (rst_n),
        .rx      (uart_rx_pin),
        .data_out(rx_byte),
        .valid   (rx_valid)
    );

    reg [6:0] hp_vol_code;
    reg       hp_vol_update;
    wire      hp_vol_busy;

    always @(posedge clk50 or negedge rst_n) begin
        if (!rst_n) begin
            hp_vol_code   <= 7'h79; // 0dB default
            hp_vol_update <= 1'b0;
        end else begin
            hp_vol_update <= 1'b0;
            if (rx_valid) begin
                hp_vol_code   <= rx_byte[6:0];
                hp_vol_update <= 1'b1;
            end
        end
    end

    // ------------------------------------------------------------
    // Optional UART TX: echo ACK byte after volume write finished
    // sends 0x4B ('K')
    // ------------------------------------------------------------
    reg        tx_trigger;
    reg        tx_pending_ack;
    wire       tx_busy;

    always @(posedge clk50 or negedge rst_n) begin
        if (!rst_n) begin
            tx_trigger    <= 1'b0;
            tx_pending_ack<= 1'b0;
        end else begin
            tx_trigger <= 1'b0;

            if (hp_vol_update)
                tx_pending_ack <= 1'b1;

            if (tx_pending_ack && !hp_vol_busy && !tx_busy) begin
                tx_trigger     <= 1'b1;
                tx_pending_ack <= 1'b0;
            end
        end
    end

    uart_tx #(
        .BAUD_DIV(5208) // 50MHz / 9600 ~= 5208
    ) u_uart_tx (
        .clk    (clk50),
        .rst_n  (rst_n),
        .trigger(tx_trigger),
        .data_in(8'h4B),
        .tx     (uart_tx_pin),
        .busy   (tx_busy)
    );

    // ------------------------------------------------------------
    // Audio out + runtime HP volume control
    // ------------------------------------------------------------
    wm8731_out_runtime_vol u_audio_out(
        .clk50        (clk50),
        .rst_n        (rst_n),
        .sample_in    (adc_left_sample),
        .hp_vol_code  (hp_vol_code),
        .hp_vol_update(hp_vol_update),
        .hp_vol_busy  (hp_vol_busy),
        .aud_xck      (aud_xck),
        .aud_bclk     (bclk_w),
        .aud_daclrck  (lrck_w),
        .aud_dacdat   (aud_dacDat),
        .i2c_sda      (aud_i2c_sda),
        .i2c_scl      (aud_i2c_scl)
    );

    blink_led u_blink_led(
        .clk  (clk50),
        .rst_n(rst_n),
        .led  (led_stt)
    );

endmodule

module lab3_uart_vol_gain(
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

    wm8731_in u_audio_in(
        .rst_n      (rst_n),
        .bclk       (bclk_w),
        .lrck       (lrck_w),
        .adcdat     (aud_adcDat),
        .left_sample(adc_left_sample)
    );

    wire [7:0] rx_byte;
    wire       rx_valid;

    uart_rx #(
        .BAUD_DIV(5208)
    ) u_uart_rx (
        .clk     (clk50),
        .rst_n   (rst_n),
        .rx      (uart_rx_pin),
        .data_out(rx_byte),
        .valid   (rx_valid)
    );

    // Command format from PC:
    //   'V' + 1 byte (7-bit WM8731 headphone volume code)
    //   'G' + 1 byte (5-bit WM8731 line input gain code)
    localparam [7:0] CMD_VOL  = 8'h56; // 'V'
    localparam [7:0] CMD_GAIN = 8'h47; // 'G'

    reg [7:0] cmd_wait;
    reg [6:0] hp_vol_code;
    reg       hp_vol_update;
    reg [4:0] line_gain_code;
    reg       line_gain_update;
    wire      ctrl_busy;

    always @(posedge clk50 or negedge rst_n) begin
        if (!rst_n) begin
            cmd_wait         <= 8'h00;
            hp_vol_code      <= 7'h79;
            hp_vol_update    <= 1'b0;
            line_gain_code   <= 5'h17; // 0 dB default
            line_gain_update <= 1'b0;
        end else begin
            hp_vol_update    <= 1'b0;
            line_gain_update <= 1'b0;

            if (rx_valid) begin
                if (cmd_wait == 8'h00) begin
                    if (rx_byte == CMD_VOL || rx_byte == CMD_GAIN)
                        cmd_wait <= rx_byte;
                end else begin
                    if (cmd_wait == CMD_VOL) begin
                        hp_vol_code   <= rx_byte[6:0];
                        hp_vol_update <= 1'b1;
                    end else if (cmd_wait == CMD_GAIN) begin
                        line_gain_code   <= rx_byte[4:0];
                        line_gain_update <= 1'b1;
                    end
                    cmd_wait <= 8'h00;
                end
            end
        end
    end

    // ACK byte after control write finished
    reg        tx_trigger;
    reg        tx_pending_ack;
    wire       tx_busy;

    always @(posedge clk50 or negedge rst_n) begin
        if (!rst_n) begin
            tx_trigger     <= 1'b0;
            tx_pending_ack <= 1'b0;
        end else begin
            tx_trigger <= 1'b0;

            if (hp_vol_update || line_gain_update)
                tx_pending_ack <= 1'b1;

            if (tx_pending_ack && !ctrl_busy && !tx_busy) begin
                tx_trigger     <= 1'b1;
                tx_pending_ack <= 1'b0;
            end
        end
    end

    uart_tx #(
        .BAUD_DIV(5208)
    ) u_uart_tx (
        .clk    (clk50),
        .rst_n  (rst_n),
        .trigger(tx_trigger),
        .data_in(8'h4B), // 'K'
        .tx     (uart_tx_pin),
        .busy   (tx_busy)
    );

    wm8731_out_runtime_vol_gain u_audio_out(
        .clk50           (clk50),
        .rst_n           (rst_n),
        .sample_in       (adc_left_sample),
        .hp_vol_code     (hp_vol_code),
        .hp_vol_update   (hp_vol_update),
        .line_gain_code  (line_gain_code),
        .line_gain_update(line_gain_update),
        .ctrl_busy       (ctrl_busy),
        .aud_xck         (aud_xck),
        .aud_bclk        (bclk_w),
        .aud_daclrck     (lrck_w),
        .aud_dacdat      (aud_dacDat),
        .i2c_sda         (aud_i2c_sda),
        .i2c_scl         (aud_i2c_scl)
    );

    blink_led u_blink_led(
        .clk  (clk50),
        .rst_n(rst_n),
        .led  (led_stt)
    );

endmodule

module lab3_eq10_uart(
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
    wire [23:0] adc_right_sample;
    wire        adc_left_valid;
    wire        adc_right_valid;
    wire [23:0] adc_sample_bclk;
    wire        adc_sample_valid_bclk;

    assign aud_bclk     = bclk_w;
    assign aud_dacLrck  = lrck_w;
    assign aud_adc_lrck = lrck_w;

    // Audio in from WM8731 ADC (I2S slave)
    wm8731_in u_audio_in(
        .rst_n      (rst_n),
        .bclk       (bclk_w),
        .lrck       (lrck_w),
        .adcdat     (aud_adcDat),
        .left_sample(adc_left_sample),
        .right_sample(adc_right_sample),
        .left_valid (adc_left_valid),
        .right_valid(adc_right_valid),
        .sample_data(adc_sample_bclk),
        .sample_valid(adc_sample_valid_bclk)
    );

    // Pulse CDC: sample_valid from bclk domain -> clk50 domain.
    wire sample_en_50m;
    cdc_toggle_sync u_cdc_sample_valid(
        .src_clk   (bclk_w),
        .src_rst_n (rst_n),
        .src_pulse (adc_sample_valid_bclk),
        .dst_clk   (clk50),
        .dst_rst_n (rst_n),
        .dst_pulse (sample_en_50m)
    );

    // Sample bus is stable for one full audio sample period (~20us),
    // so latching it on clk50 when sample_en_50m arrives is sufficient here.
    reg signed [23:0] sample_in_50m;
    always @(posedge clk50 or negedge rst_n) begin
        if (!rst_n)
            sample_in_50m <= 24'sd0;
        else if (sample_en_50m)
            sample_in_50m <= adc_sample_bclk;
    end

    // ------------------------------------------------------------
    // UART RX: commands from PC
    //   'V' + 1 byte  : WM8731 headphone volume raw[6:0]
    //   'G' + 1 byte  : WM8731 line input gain raw[4:0]
    //   'B' + idx + g : EQ band index 0..9, gain code 0..24  (=> -12..+12 dB)
    // ------------------------------------------------------------
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

    localparam [7:0] CMD_VOL  = 8'h56; // 'V'
    localparam [7:0] CMD_GAIN = 8'h47; // 'G'
    localparam [7:0] CMD_BAND = 8'h42; // 'B'

    localparam [1:0] ST_IDLE = 2'd0,
                     ST_WAIT_VALUE = 2'd1,
                     ST_WAIT_BAND_GAIN = 2'd2;

    reg [1:0] parse_state;
    reg [7:0] pending_cmd;
    reg [3:0] pending_band_idx;

    reg [6:0] hp_vol_code;
    reg       hp_vol_update;
    reg [4:0] line_gain_code;
    reg       line_gain_update;

    reg signed [5:0] band_db0;
    reg signed [5:0] band_db1;
    reg signed [5:0] band_db2;
    reg signed [5:0] band_db3;
    reg signed [5:0] band_db4;
    reg signed [5:0] band_db5;
    reg signed [5:0] band_db6;
    reg signed [5:0] band_db7;
    reg signed [5:0] band_db8;
    reg signed [5:0] band_db9;
    reg       band_update_pulse;

    wire signed [5:0] band_db_from_code;
    assign band_db_from_code = $signed({1'b0, rx_byte[4:0]}) - 6'sd12;

    always @(posedge clk50 or negedge rst_n) begin
        if (!rst_n) begin
            parse_state       <= ST_IDLE;
            pending_cmd       <= 8'h00;
            pending_band_idx  <= 4'd0;
            hp_vol_code       <= 7'h79;
            hp_vol_update     <= 1'b0;
            line_gain_code    <= 5'h17;
            line_gain_update  <= 1'b0;
            band_update_pulse <= 1'b0;
            band_db0 <= 6'sd0; band_db1 <= 6'sd0; band_db2 <= 6'sd0; band_db3 <= 6'sd0; band_db4 <= 6'sd0;
            band_db5 <= 6'sd0; band_db6 <= 6'sd0; band_db7 <= 6'sd0; band_db8 <= 6'sd0; band_db9 <= 6'sd0;
        end else begin
            hp_vol_update     <= 1'b0;
            line_gain_update  <= 1'b0;
            band_update_pulse <= 1'b0;

            if (rx_valid) begin
                case (parse_state)
                    ST_IDLE: begin
                        if (rx_byte == CMD_VOL || rx_byte == CMD_GAIN) begin
                            pending_cmd <= rx_byte;
                            parse_state <= ST_WAIT_VALUE;
                        end else if (rx_byte == CMD_BAND) begin
                            pending_cmd <= rx_byte;
                            parse_state <= ST_WAIT_VALUE;
                        end
                    end

                    ST_WAIT_VALUE: begin
                        if (pending_cmd == CMD_VOL) begin
                            hp_vol_code   <= rx_byte[6:0];
                            hp_vol_update <= 1'b1;
                            parse_state   <= ST_IDLE;
                        end else if (pending_cmd == CMD_GAIN) begin
                            line_gain_code   <= rx_byte[4:0];
                            line_gain_update <= 1'b1;
                            parse_state      <= ST_IDLE;
                        end else if (pending_cmd == CMD_BAND) begin
                            pending_band_idx <= rx_byte[3:0];
                            parse_state      <= ST_WAIT_BAND_GAIN;
                        end else begin
                            parse_state <= ST_IDLE;
                        end
                    end

                    ST_WAIT_BAND_GAIN: begin
                        case (pending_band_idx)
                            4'd0: band_db0 <= band_db_from_code;
                            4'd1: band_db1 <= band_db_from_code;
                            4'd2: band_db2 <= band_db_from_code;
                            4'd3: band_db3 <= band_db_from_code;
                            4'd4: band_db4 <= band_db_from_code;
                            4'd5: band_db5 <= band_db_from_code;
                            4'd6: band_db6 <= band_db_from_code;
                            4'd7: band_db7 <= band_db_from_code;
                            4'd8: band_db8 <= band_db_from_code;
                            4'd9: band_db9 <= band_db_from_code;
                            default: ;
                        endcase
                        band_update_pulse <= 1'b1;
                        parse_state <= ST_IDLE;
                    end

                    default: parse_state <= ST_IDLE;
                endcase
            end
        end
    end

    // ------------------------------------------------------------
    // 10-band EQ core
    // ------------------------------------------------------------
    wire signed [23:0] eq_sample_out;
    wire               eq_sample_out_valid;

    eq10_core #(
        .DATA_W(24),
        .COEF_W(18),
        .NTAPS(127),
        .COEFF0_FILE("eq10_mem/fir_31hz.mem"),
        .COEFF1_FILE("eq10_mem/fir_62hz.mem"),
        .COEFF2_FILE("eq10_mem/fir_125hz.mem"),
        .COEFF3_FILE("eq10_mem/fir_250hz.mem"),
        .COEFF4_FILE("eq10_mem/fir_500hz.mem"),
        .COEFF5_FILE("eq10_mem/fir_1khz.mem"),
        .COEFF6_FILE("eq10_mem/fir_2khz.mem"),
        .COEFF7_FILE("eq10_mem/fir_4khz.mem"),
        .COEFF8_FILE("eq10_mem/fir_8khz.mem"),
        .COEFF9_FILE("eq10_mem/fir_16khz.mem")
    ) u_eq10_core (
        .clk            (clk50),
        .rst_n          (rst_n),
        .sample_en      (sample_en_50m),
        .sample_in      (sample_in_50m),
        .db0            (band_db0),
        .db1            (band_db1),
        .db2            (band_db2),
        .db3            (band_db3),
        .db4            (band_db4),
        .db5            (band_db5),
        .db6            (band_db6),
        .db7            (band_db7),
        .db8            (band_db8),
        .db9            (band_db9),
        .sample_out     (eq_sample_out),
        .sample_out_valid(eq_sample_out_valid)
    );

    // Hold latest EQ sample until WM8731 transmitter consumes it.
    reg signed [23:0] dac_sample_hold;
    always @(posedge clk50 or negedge rst_n) begin
        if (!rst_n)
            dac_sample_hold <= 24'sd0;
        else if (eq_sample_out_valid)
            dac_sample_hold <= eq_sample_out;
    end

    // ------------------------------------------------------------
    // WM8731 output + runtime volume/input gain control
    // ------------------------------------------------------------
    wire ctrl_busy;
    wm8731_out_runtime_vol_gain u_audio_out(
        .clk50           (clk50),
        .rst_n           (rst_n),
        .sample_in       (dac_sample_hold),
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

    // ------------------------------------------------------------
    // UART TX ACK
    // Send 'K' after any command is applied. For V/G, wait until
    // WM8731 control write is idle. For band update, ACK immediately.
    // ------------------------------------------------------------
    reg        tx_trigger;
    reg        tx_pending_ack;
    reg        tx_wait_ctrl_clear;
    wire       tx_busy;

    always @(posedge clk50 or negedge rst_n) begin
        if (!rst_n) begin
            tx_trigger         <= 1'b0;
            tx_pending_ack     <= 1'b0;
            tx_wait_ctrl_clear <= 1'b0;
        end else begin
            tx_trigger <= 1'b0;

            if (hp_vol_update || line_gain_update) begin
                tx_pending_ack     <= 1'b1;
                tx_wait_ctrl_clear <= 1'b1;
            end else if (band_update_pulse) begin
                tx_pending_ack     <= 1'b1;
                tx_wait_ctrl_clear <= 1'b0;
            end

            if (tx_pending_ack && !tx_busy) begin
                if (!tx_wait_ctrl_clear || !ctrl_busy) begin
                    tx_trigger         <= 1'b1;
                    tx_pending_ack     <= 1'b0;
                    tx_wait_ctrl_clear <= 1'b0;
                end
            end
        end
    end

    uart_tx #(
        .BAUD_DIV(5208)
    ) u_uart_tx (
        .clk    (clk50),
        .rst_n  (rst_n),
        .trigger(tx_trigger),
        .data_in(8'h4B),
        .tx     (uart_tx_pin),
        .busy   (tx_busy)
    );

    blink_led u_blink_led(
        .clk  (clk50),
        .rst_n(rst_n),
        .led  (led_stt)
    );
endmodule

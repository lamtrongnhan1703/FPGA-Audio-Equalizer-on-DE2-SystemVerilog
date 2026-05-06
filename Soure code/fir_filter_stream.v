// Quartus 12 friendly FIR stream filter
module fir_filter_stream #(
    parameter DATA_W = 24,
    parameter COEF_W = 18,
    parameter NTAPS  = 127,
    parameter COEFF_FILE = "fir_coeffs.mem"
)(
    input  wire                     clk,
    input  wire                     rst_n,
    input  wire                     sample_en,
    input  wire signed [DATA_W-1:0] dataIn,
    output reg  signed [DATA_W-1:0] dataOut,
    output reg                      dataOut_valid
);
    reg signed [COEF_W-1:0] h [0:NTAPS-1];
    reg signed [DATA_W-1:0] x [0:NTAPS-1];

    integer ii;
    integer k;
    initial begin
        for (ii = 0; ii < NTAPS; ii = ii + 1)
            h[ii] = {COEF_W{1'b0}};
        $readmemh(COEFF_FILE, h);
    end

    localparam ACC_W = DATA_W + COEF_W + 8;
    localparam SHIFT = COEF_W - 1;

    reg signed [ACC_W-1:0] acc;
    reg [15:0] tap_idx;
    reg [1:0] state;
    reg signed [ACC_W-1:0] acc_r;
    reg signed [ACC_W-1:0] y_s;

    localparam S_IDLE = 2'd0,
               S_MAC  = 2'd1,
               S_DONE = 2'd2;

    function [DATA_W-1:0] sat_dataw;
        input signed [ACC_W-1:0] v;
        reg signed [ACC_W-1:0] vmax, vmin;
        begin
            vmax = (({{(ACC_W-1){1'b0}},1'b1} <<< (DATA_W-1)) - 1);
            vmin = -({{(ACC_W-1){1'b0}},1'b1} <<< (DATA_W-1));
            if (v > vmax)
                sat_dataw = {1'b0, {(DATA_W-1){1'b1}}};
            else if (v < vmin)
                sat_dataw = {1'b1, {(DATA_W-1){1'b0}}};
            else
                sat_dataw = v[DATA_W-1:0];
        end
    endfunction

    function signed [ACC_W-1:0] round_shift;
        input signed [ACC_W-1:0] v;
        input integer sh;
        reg signed [ACC_W-1:0] half;
        begin
            if (sh <= 0)
                round_shift = v;
            else begin
                half = ({{(ACC_W-1){1'b0}},1'b1} <<< (sh-1));
                if (v >= 0)
                    round_shift = v + half;
                else
                    round_shift = v - half;
            end
        end
    endfunction

    always @(posedge clk or negedge rst_n) begin
        if (!rst_n) begin
            for (k = 0; k < NTAPS; k = k + 1)
                x[k] <= {DATA_W{1'b0}};
        end else if (sample_en) begin
            x[0] <= dataIn;
            for (k = 1; k < NTAPS; k = k + 1)
                x[k] <= x[k-1];
        end
    end

    always @(posedge clk or negedge rst_n) begin
        if (!rst_n) begin
            state         <= S_IDLE;
            tap_idx       <= 16'd0;
            acc           <= {ACC_W{1'b0}};
            acc_r         <= {ACC_W{1'b0}};
            y_s           <= {ACC_W{1'b0}};
            dataOut       <= {DATA_W{1'b0}};
            dataOut_valid <= 1'b0;
        end else begin
            dataOut_valid <= 1'b0;
            case (state)
                S_IDLE: begin
                    if (sample_en) begin
                        tap_idx <= 16'd0;
                        acc     <= {ACC_W{1'b0}};
                        state   <= S_MAC;
                    end
                end
                S_MAC: begin
                    acc <= acc + ($signed(x[tap_idx]) * $signed(h[tap_idx]));
                    if (tap_idx == NTAPS-1)
                        state <= S_DONE;
                    else
                        tap_idx <= tap_idx + 16'd1;
                end
                S_DONE: begin
                    acc_r <= round_shift(acc, SHIFT);
                    y_s   <= round_shift(acc, SHIFT) >>> SHIFT;
                    dataOut <= $signed(sat_dataw(round_shift(acc, SHIFT) >>> SHIFT));
                    dataOut_valid <= 1'b1;
                    state <= S_IDLE;
                end
                default: state <= S_IDLE;
            endcase
        end
    end
endmodule

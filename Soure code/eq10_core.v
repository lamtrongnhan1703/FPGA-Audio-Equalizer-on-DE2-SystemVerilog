module eq10_core #(
    parameter integer DATA_W = 24,
    parameter integer COEF_W = 18,
    parameter integer NTAPS  = 127,
    parameter integer GAIN_SHIFT = 14,
    parameter        COEFF0_FILE = "fir_31hz.mem",
    parameter        COEFF1_FILE = "fir_62hz.mem",
    parameter        COEFF2_FILE = "fir_125hz.mem",
    parameter        COEFF3_FILE = "fir_250hz.mem",
    parameter        COEFF4_FILE = "fir_500hz.mem",
    parameter        COEFF5_FILE = "fir_1khz.mem",
    parameter        COEFF6_FILE = "fir_2khz.mem",
    parameter        COEFF7_FILE = "fir_4khz.mem",
    parameter        COEFF8_FILE = "fir_8khz.mem",
    parameter        COEFF9_FILE = "fir_16khz.mem"
)(
    input  wire                     clk,
    input  wire                     rst_n,
    input  wire                     sample_en,
    input  wire signed [DATA_W-1:0] sample_in,

    input  wire signed [5:0] db0,
    input  wire signed [5:0] db1,
    input  wire signed [5:0] db2,
    input  wire signed [5:0] db3,
    input  wire signed [5:0] db4,
    input  wire signed [5:0] db5,
    input  wire signed [5:0] db6,
    input  wire signed [5:0] db7,
    input  wire signed [5:0] db8,
    input  wire signed [5:0] db9,

    output reg  signed [DATA_W-1:0] sample_out,
    output reg                      sample_out_valid
);
    wire signed [DATA_W-1:0] y0, y1, y2, y3, y4, y5, y6, y7, y8, y9;
    wire v0, v1, v2, v3, v4, v5, v6, v7, v8, v9;

    fir_filter_stream #(.DATA_W(DATA_W), .COEF_W(COEF_W), .NTAPS(NTAPS), .COEFF_FILE(COEFF0_FILE)) u_fir0 (
        .clk(clk), .rst_n(rst_n), .sample_en(sample_en), .dataIn(sample_in), .dataOut(y0), .dataOut_valid(v0));
    fir_filter_stream #(.DATA_W(DATA_W), .COEF_W(COEF_W), .NTAPS(NTAPS), .COEFF_FILE(COEFF1_FILE)) u_fir1 (
        .clk(clk), .rst_n(rst_n), .sample_en(sample_en), .dataIn(sample_in), .dataOut(y1), .dataOut_valid(v1));
    fir_filter_stream #(.DATA_W(DATA_W), .COEF_W(COEF_W), .NTAPS(NTAPS), .COEFF_FILE(COEFF2_FILE)) u_fir2 (
        .clk(clk), .rst_n(rst_n), .sample_en(sample_en), .dataIn(sample_in), .dataOut(y2), .dataOut_valid(v2));
    fir_filter_stream #(.DATA_W(DATA_W), .COEF_W(COEF_W), .NTAPS(NTAPS), .COEFF_FILE(COEFF3_FILE)) u_fir3 (
        .clk(clk), .rst_n(rst_n), .sample_en(sample_en), .dataIn(sample_in), .dataOut(y3), .dataOut_valid(v3));
    fir_filter_stream #(.DATA_W(DATA_W), .COEF_W(COEF_W), .NTAPS(NTAPS), .COEFF_FILE(COEFF4_FILE)) u_fir4 (
        .clk(clk), .rst_n(rst_n), .sample_en(sample_en), .dataIn(sample_in), .dataOut(y4), .dataOut_valid(v4));
    fir_filter_stream #(.DATA_W(DATA_W), .COEF_W(COEF_W), .NTAPS(NTAPS), .COEFF_FILE(COEFF5_FILE)) u_fir5 (
        .clk(clk), .rst_n(rst_n), .sample_en(sample_en), .dataIn(sample_in), .dataOut(y5), .dataOut_valid(v5));
    fir_filter_stream #(.DATA_W(DATA_W), .COEF_W(COEF_W), .NTAPS(NTAPS), .COEFF_FILE(COEFF6_FILE)) u_fir6 (
        .clk(clk), .rst_n(rst_n), .sample_en(sample_en), .dataIn(sample_in), .dataOut(y6), .dataOut_valid(v6));
    fir_filter_stream #(.DATA_W(DATA_W), .COEF_W(COEF_W), .NTAPS(NTAPS), .COEFF_FILE(COEFF7_FILE)) u_fir7 (
        .clk(clk), .rst_n(rst_n), .sample_en(sample_en), .dataIn(sample_in), .dataOut(y7), .dataOut_valid(v7));
    fir_filter_stream #(.DATA_W(DATA_W), .COEF_W(COEF_W), .NTAPS(NTAPS), .COEFF_FILE(COEFF8_FILE)) u_fir8 (
        .clk(clk), .rst_n(rst_n), .sample_en(sample_en), .dataIn(sample_in), .dataOut(y8), .dataOut_valid(v8));
    fir_filter_stream #(.DATA_W(DATA_W), .COEF_W(COEF_W), .NTAPS(NTAPS), .COEFF_FILE(COEFF9_FILE)) u_fir9 (
        .clk(clk), .rst_n(rst_n), .sample_en(sample_en), .dataIn(sample_in), .dataOut(y9), .dataOut_valid(v9));

    wire [15:0] g0, g1, g2, g3, g4, g5, g6, g7, g8, g9;
    eq10_gain_lut u_g0(.db_in(db0), .gain_q2_14(g0));
    eq10_gain_lut u_g1(.db_in(db1), .gain_q2_14(g1));
    eq10_gain_lut u_g2(.db_in(db2), .gain_q2_14(g2));
    eq10_gain_lut u_g3(.db_in(db3), .gain_q2_14(g3));
    eq10_gain_lut u_g4(.db_in(db4), .gain_q2_14(g4));
    eq10_gain_lut u_g5(.db_in(db5), .gain_q2_14(g5));
    eq10_gain_lut u_g6(.db_in(db6), .gain_q2_14(g6));
    eq10_gain_lut u_g7(.db_in(db7), .gain_q2_14(g7));
    eq10_gain_lut u_g8(.db_in(db8), .gain_q2_14(g8));
    eq10_gain_lut u_g9(.db_in(db9), .gain_q2_14(g9));

    wire signed [39:0] p0 = $signed(y0) * $signed({1'b0, g0});
    wire signed [39:0] p1 = $signed(y1) * $signed({1'b0, g1});
    wire signed [39:0] p2 = $signed(y2) * $signed({1'b0, g2});
    wire signed [39:0] p3 = $signed(y3) * $signed({1'b0, g3});
    wire signed [39:0] p4 = $signed(y4) * $signed({1'b0, g4});
    wire signed [39:0] p5 = $signed(y5) * $signed({1'b0, g5});
    wire signed [39:0] p6 = $signed(y6) * $signed({1'b0, g6});
    wire signed [39:0] p7 = $signed(y7) * $signed({1'b0, g7});
    wire signed [39:0] p8 = $signed(y8) * $signed({1'b0, g8});
    wire signed [39:0] p9 = $signed(y9) * $signed({1'b0, g9});

    wire signed [39:0] s0 = p0 >>> GAIN_SHIFT;
    wire signed [39:0] s1 = p1 >>> GAIN_SHIFT;
    wire signed [39:0] s2 = p2 >>> GAIN_SHIFT;
    wire signed [39:0] s3 = p3 >>> GAIN_SHIFT;
    wire signed [39:0] s4 = p4 >>> GAIN_SHIFT;
    wire signed [39:0] s5 = p5 >>> GAIN_SHIFT;
    wire signed [39:0] s6 = p6 >>> GAIN_SHIFT;
    wire signed [39:0] s7 = p7 >>> GAIN_SHIFT;
    wire signed [39:0] s8 = p8 >>> GAIN_SHIFT;
    wire signed [39:0] s9 = p9 >>> GAIN_SHIFT;

    wire signed [47:0] mix_sum =
        $signed({{8{s0[39]}}, s0}) + $signed({{8{s1[39]}}, s1}) +
        $signed({{8{s2[39]}}, s2}) + $signed({{8{s3[39]}}, s3}) +
        $signed({{8{s4[39]}}, s4}) + $signed({{8{s5[39]}}, s5}) +
        $signed({{8{s6[39]}}, s6}) + $signed({{8{s7[39]}}, s7}) +
        $signed({{8{s8[39]}}, s8}) + $signed({{8{s9[39]}}, s9});

    function [DATA_W-1:0] sat24;
        input signed [47:0] v;
        reg signed [47:0] vmax;
        reg signed [47:0] vmin;
        begin
            vmax = 48'sd8388607;
            vmin = -48'sd8388608;
            if (v > vmax)
                sat24 = 24'sh7FFFFF;
            else if (v < vmin)
                sat24 = 24'sh800000;
            else
                sat24 = v[23:0];
        end
    endfunction

    wire all_valid = v0 & v1 & v2 & v3 & v4 & v5 & v6 & v7 & v8 & v9;

    always @(posedge clk or negedge rst_n) begin
        if (!rst_n) begin
            sample_out       <= {DATA_W{1'b0}};
            sample_out_valid <= 1'b0;
        end else begin
            sample_out_valid <= 1'b0;
            if (all_valid) begin
                sample_out       <= $signed(sat24(mix_sum));
                sample_out_valid <= 1'b1;
            end
        end
    end
endmodule

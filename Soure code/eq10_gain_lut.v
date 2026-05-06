module eq10_gain_lut (
    input  wire signed [5:0] db_in,      // -12 .. +12
    output reg  [15:0]       gain_q2_14  // linear gain in Q2.14
);
    always @(*) begin
        case (db_in)
            -12: gain_q2_14 = 16'd4115;   // 0.2512
            -11: gain_q2_14 = 16'd4618;
            -10: gain_q2_14 = 16'd5182;
             -9: gain_q2_14 = 16'd5815;
             -8: gain_q2_14 = 16'd6525;
             -7: gain_q2_14 = 16'd7321;
             -6: gain_q2_14 = 16'd8211;   // 0.5012
             -5: gain_q2_14 = 16'd9209;
             -4: gain_q2_14 = 16'd10328;
             -3: gain_q2_14 = 16'd11583;
             -2: gain_q2_14 = 16'd12991;
             -1: gain_q2_14 = 16'd14570;
              0: gain_q2_14 = 16'd16384;  // 1.0000
              1: gain_q2_14 = 16'd18327;
              2: gain_q2_14 = 16'd20554;
              3: gain_q2_14 = 16'd23034;
              4: gain_q2_14 = 16'd25815;
              5: gain_q2_14 = 16'd28933;
              6: gain_q2_14 = 16'd32683;  // 1.9953
              7: gain_q2_14 = 16'd36699;
              8: gain_q2_14 = 16'd41199;
              9: gain_q2_14 = 16'd46250;
             10: gain_q2_14 = 16'd51922;
             11: gain_q2_14 = 16'd58289;
             12: gain_q2_14 = 16'd65228;  // 3.9811
            default: gain_q2_14 = 16'd16384;
        endcase
    end
endmodule

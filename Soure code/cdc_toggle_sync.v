module cdc_toggle_sync (
    input  wire src_clk,
    input  wire src_rst_n,
    input  wire src_pulse,
    input  wire dst_clk,
    input  wire dst_rst_n,
    output wire dst_pulse
);
    reg src_toggle;
    always @(posedge src_clk or negedge src_rst_n) begin
        if (!src_rst_n) src_toggle <= 1'b0;
        else if (src_pulse) src_toggle <= ~src_toggle;
    end

    reg [2:0] sync_ff;
    always @(posedge dst_clk or negedge dst_rst_n) begin
        if (!dst_rst_n) sync_ff <= 3'b000;
        else sync_ff <= {sync_ff[1:0], src_toggle};
    end

    assign dst_pulse = sync_ff[2] ^ sync_ff[1];
endmodule

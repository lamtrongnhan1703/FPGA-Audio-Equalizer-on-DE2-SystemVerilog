module blink_led (
    input  wire clk,   // Clock 50 MHz
    input  wire rst_n,   // Reset active-high
    output reg  led    // LED output
);

    // 50 MHz -> 1 chu kỳ = 20 ns
    // 100 ms / 20 ns = 5_000_000 xung
    localparam integer COUNT_MAX = 5_000_000 - 1;

    reg [22:0] counter;

    always @(posedge clk or posedge !rst_n) begin
        if (!rst_n) begin
            counter <= 23'd0;
            led     <= 1'b0;     // trạng thái LED sau reset
        end else begin
            if (counter >= COUNT_MAX) begin
                counter <= 23'd0;
                led     <= ~led; // đảo trạng thái LED mỗi 100 ms
            end else begin
                counter <= counter + 1'b1;
            end
        end
    end

endmodule

// Copyright (C) 1991-2012 Altera Corporation
// Your use of Altera Corporation's design tools, logic functions 
// and other software and tools, and its AMPP partner logic 
// functions, and any output files from any of the foregoing 
// (including device programming or simulation files), and any 
// associated documentation or information are expressly subject 
// to the terms and conditions of the Altera Program License 
// Subscription Agreement, Altera MegaCore Function License 
// Agreement, or other applicable license agreement, including, 
// without limitation, that your use is for the sole purpose of 
// programming logic devices manufactured by Altera and sold by 
// Altera or its authorized distributors.  Please refer to the 
// applicable agreement for further details.

// PROGRAM		"Quartus II 32-bit"
// VERSION		"Version 12.1 Build 177 11/07/2012 SJ Web Edition"
// CREATED		"Fri Mar 27 01:30:27 2026"

module lab3(
	clk50,
	rst_n,
	aud_adcDat,
	led_stt,
	aud_xck,
	aud_bclk,
	aud_dacLrck,
	aud_i2c_sda,
	aud_i2c_scl,
	aud_dacDat,
	aud_adc_lrck
);


input wire	clk50;
input wire	rst_n;
input wire	aud_adcDat;
output wire	led_stt;
output wire	aud_xck;
output wire	aud_bclk;
output wire	aud_dacLrck;
output wire	aud_i2c_sda;
output wire	aud_i2c_scl;
output wire	aud_dacDat;
output wire	aud_adc_lrck;

wire	SYNTHESIZED_WIRE_0;
wire	SYNTHESIZED_WIRE_4;
wire	[23:0] SYNTHESIZED_WIRE_2;

assign	aud_bclk = SYNTHESIZED_WIRE_0;
assign	aud_dacLrck = SYNTHESIZED_WIRE_4;
assign	aud_adc_lrck = SYNTHESIZED_WIRE_4;




wm8731_in	b2v_audio_in(
	.rst_n(rst_n),
	.bclk(SYNTHESIZED_WIRE_0),
	.lrck(SYNTHESIZED_WIRE_4),
	.adcdat(aud_adcDat),
	
	
	
	.left_sample(SYNTHESIZED_WIRE_2)
	
	);


wm8731_out_max	b2v_audio_out(
	.clk50(clk50),
	.rst_n(rst_n),
	.i2c_sda(aud_i2c_sda),
	.sample_in(SYNTHESIZED_WIRE_2),
	.aud_xck(aud_xck),
	.aud_bclk(SYNTHESIZED_WIRE_0),
	.aud_daclrck(SYNTHESIZED_WIRE_4),
	.aud_dacdat(aud_dacDat),
	.i2c_scl(aud_i2c_scl)
	);


blink_led	b2v_inst(
	.clk(clk50),
	.rst_n(rst_n),
	.led(led_stt));


endmodule

# Digital Audio Equalizer on FPGA DE2 via UART

## General Description
This project implements a hardware-based Digital Audio Equalizer on the Altera DE2 FPGA board. The system allows users to independently adjust the overall master volume as well as the gain for specific, fixed audio frequency bands. 

To provide a user-friendly control interface, the system utilizes a CH340 USB-to-TTL module to establish a UART serial connection between a computer and the DE2 board. A custom desktop application sends tuning parameters over this serial link, which the FPGA then processes in real-time to manipulate the audio output.

## Technologies & Hardware
* Hardware Platform: Altera DE2 Board.
* Communication Interface: CH340 (USB to UART/TTL module).
* Hardware Description Language: SystemVerilog.
* FPGA Development Environment: Quartus II.
* Software Application: C# (.NET) for the PC control interface.
* Core Logic: Real-time Digital Signal Processing (DSP), UART communication protocol.

## Project Structure
The repository is organized into four main directories:

### 1. /Report
Contains the detailed project documentation. This includes the theoretical background of the digital filters, the system architecture, and crucially, the hardware wiring diagram showing exactly how to connect the CH340 module pins (TX, RX, GND) to the GPIO headers of the DE2 board.

### 2. /Source_code
Contains the SystemVerilog source files and the Quartus project configuration. This logic defines the UART receiver, the audio processing pipeline, and the hardware equalizer on the FPGA.

### 3. /Audio_Equalizer
Contains the C# project source code for the PC application. This software acts as the control panel, allowing the user to adjust sliders for different frequency bands and sending the corresponding command packets to the FPGA via the CH340 serial port.

### 4. /eq10_mem
Contains the memory initialization files and generated parameters (filter coefficients) required by the FPGA to accurately tune and isolate the specific frequency bands.

## How to Run the Project
To set up and run this system, you need to configure both the FPGA hardware and the PC software application:

1. Hardware Connection:
   * Connect the CH340 module to your PC via USB, and wire the TX/RX/GND pins to the DE2 board exactly as illustrated in the documentation inside the `/Report` folder.

2. Flashing the FPGA:
   * Open the Quartus software.
   * Navigate to the `/Source_code` folder and open the Quartus Project File named `lab 3.qpf`.
   * Open the Quartus Programmer, select your USB-Blaster, and flash the compiled configuration file (.sof) to the DE2 kit.

3. Running the Control Interface:
   * Navigate to the `/Audio_Equalizer` folder.
   * Open the C# Project Source file (`Audio Equalizer.csproj` or the main solution file) using Visual Studio.
   * Build and run the project on your computer. 
   * Select the correct COM port corresponding to your CH340 module in the application interface to start controlling the audio frequencies on the DE2 board.

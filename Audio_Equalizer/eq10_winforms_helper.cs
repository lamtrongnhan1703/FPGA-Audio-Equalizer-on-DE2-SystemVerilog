using System;
using System.Windows.Forms;

namespace Audio_Equalizer
{
    public partial class Form1 : Form
    {
        private void InitEqBandHandlers()
        {
            trackBar_31hz.Scroll += EqBand_Scroll;
            trackBar_62hz.Scroll += EqBand_Scroll;
            trackBar_125hz.Scroll += EqBand_Scroll;
            trackBar_250hz.Scroll += EqBand_Scroll;
            trackBar_500hz.Scroll += EqBand_Scroll;
            trackBar_1khz.Scroll += EqBand_Scroll;
            trackBar_2khz.Scroll += EqBand_Scroll;
            trackBar_4khz.Scroll += EqBand_Scroll;
            trackBar_8khz.Scroll += EqBand_Scroll;
            trackBar_16khz.Scroll += EqBand_Scroll;
        }

        private void EqBand_Scroll(object sender, EventArgs e)
        {
            System.Windows.Forms.TrackBar tb = sender as System.Windows.Forms.TrackBar;
            if (tb == null) return;

            int idx = GetEqBandIndex(tb);
            if (idx < 0) return;

            // TrackBar range expected: -12 .. +12 dB
            int db = tb.Value;
            if (db < -12) db = -12;
            if (db > 12) db = 12;

            SendEqBand(idx, db);
        }

        private int GetEqBandIndex(System.Windows.Forms.TrackBar tb)
        {
            if (tb == trackBar_31hz) return 0;
            if (tb == trackBar_62hz) return 1;
            if (tb == trackBar_125hz) return 2;
            if (tb == trackBar_250hz) return 3;
            if (tb == trackBar_500hz) return 4;
            if (tb == trackBar_1khz) return 5;
            if (tb == trackBar_2khz) return 6;
            if (tb == trackBar_4khz) return 7;
            if (tb == trackBar_8khz) return 8;
            if (tb == trackBar_16khz) return 9;
            return -1;
        }

        private string GetEqBandName(int idx)
        {
            switch (idx)
            {
                case 0: return "31Hz";
                case 1: return "62Hz";
                case 2: return "125Hz";
                case 3: return "250Hz";
                case 4: return "500Hz";
                case 5: return "1kHz";
                case 6: return "2kHz";
                case 7: return "4kHz";
                case 8: return "8kHz";
                case 9: return "16kHz";
                default: return "?";
            }
        }

        private void SendEqBand(int bandIndex, int db)
        {
            try
            {
                if (serialPort1 == null || !serialPort1.IsOpen)
                    return;

                if (bandIndex < 0 || bandIndex > 9)
                    return;

                if (db < -12) db = -12;
                if (db > 12) db = 12;

                // FPGA expects code 0..24 corresponding to -12..+12 dB
                byte gainCode = (byte)(db + 12);
                byte[] frame = new byte[] { (byte)'B', (byte)bandIndex, gainCode };

                serialPort1.Write(frame, 0, frame.Length);
                LogTx($"BAND {bandIndex} ({GetEqBandName(bandIndex)}) = {db} dB, raw=0x{gainCode:X2}");
            }
            catch (Exception ex)
            {
                LogInfo("Send BAND error: " + ex.Message);
            }
        }

        private void SendAllEqBandsFlat()
        {
            SendEqBand(0, 0);
            SendEqBand(1, 0);
            SendEqBand(2, 0);
            SendEqBand(3, 0);
            SendEqBand(4, 0);
            SendEqBand(5, 0);
            SendEqBand(6, 0);
            SendEqBand(7, 0);
            SendEqBand(8, 0);
            SendEqBand(9, 0);
        }

        private void SyncEqTrackBarsToFlat()
        {
            trackBar_31hz.Value = 0;
            trackBar_62hz.Value = 0;
            trackBar_125hz.Value = 0;
            trackBar_250hz.Value = 0;
            trackBar_500hz.Value = 0;
            trackBar_1khz.Value = 0;
            trackBar_2khz.Value = 0;
            trackBar_4khz.Value = 0;
            trackBar_8khz.Value = 0;
            trackBar_16khz.Value = 0;
        }
    }
}

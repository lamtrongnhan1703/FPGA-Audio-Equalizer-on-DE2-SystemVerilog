using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using WinTrackBar = System.Windows.Forms.TrackBar;

namespace Audio_Equalizer
{
    public partial class Form1 : Form
    {
        private FormLog logForm;

        private const int EQ_DB_MIN = -12;
        private const int EQ_DB_MAX = 12;

        private const int WM8731_VOL_MIN = 0;
        private const int WM8731_VOL_MAX = 127;

        private const int WM8731_GAIN_MIN = 0;
        private const int WM8731_GAIN_MAX = 31;

        private readonly int[] EQ_FLAT = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        private readonly int[] EQ_BASS = { 8, 7, 6, 4, 2, 0, -2, -3, -4, -4 };
        private readonly int[] EQ_ROCK = { 5, 4, 2, 0, -2, -1, 2, 4, 5, 4 };
        private readonly int[] EQ_VOCAL = { -4, -3, -2, 1, 3, 5, 5, 3, 0, -2 };
        private readonly int[] EQ_TREBLE = { -4, -4, -3, -2, 0, 1, 3, 5, 7, 8 };
        private readonly int[] EQ_POP = { 3, 2, 1, 0, -1, 1, 3, 4, 3, 2 };


        public Form1()
        {
            InitializeComponent();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                if (serialPort1.IsOpen)
                {
                    btnConnect.Text = "Connect";
                    btnConnect.BackColor = Color.FromArgb(225, 225, 225);
                    // Xóa bộ đệm dữ liệu đầu vào
                    serialPort1.DiscardInBuffer();
                    // Xóa bộ đệm dữ liệu đầu ra
                    serialPort1.DiscardOutBuffer();
                    serialPort1.Close();
                    // Cập nhật label khi ngắt kết nối
                    label5.Text = "Chưa kết nối UART.";
                }
                else
                {
                    serialPort1.PortName = cb_port.Text;
                    serialPort1.Open();
                    btnConnect.Text = "Connected!";
                    btnConnect.BackColor = Color.FromArgb(128, 255, 128);

                    // Hiển thị thông tin UART lên label5
                    label5.Text = $"Cổng: {serialPort1.PortName} | " +
                                  $"Tốc độ: {serialPort1.BaudRate} bps | " +
                                  $"Data bits: {serialPort1.DataBits} | " +
                                  $"Stop bits: {serialPort1.StopBits} | " +
                                  $"Parity: {serialPort1.Parity}";
                }
            }
            catch
            {
                MessageBox.Show("Connection False !");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            InitTrackBars();
            InitEqBandHandlers();
            SyncEqTrackBarsToFlat();

            // Lấy danh sách cổng COM và sắp xếp theo số sau "COM"
            string[] ports = SerialPort.GetPortNames()
                .OrderBy(p =>
                {
                    // Tách số từ chuỗi COM
                    Match match = Regex.Match(p, @"\d+");
                    return match.Success ? int.Parse(match.Value) : int.MaxValue;
                })
                .ToArray();

            // Gán vào ComboBox
            cb_port.DataSource = ports;

        }

        private void ApplyEqPreset(int[] gainsDb)
        {
            if (gainsDb == null || gainsDb.Length != 10)
                return;

            // Cap nhat trackbar
            trackBar_31hz.Value = ClampEq(gainsDb[0]);
            trackBar_62hz.Value = ClampEq(gainsDb[1]);
            trackBar_125hz.Value = ClampEq(gainsDb[2]);
            trackBar_250hz.Value = ClampEq(gainsDb[3]);
            trackBar_500hz.Value = ClampEq(gainsDb[4]);
            trackBar_1khz.Value = ClampEq(gainsDb[5]);
            trackBar_2khz.Value = ClampEq(gainsDb[6]);
            trackBar_4khz.Value = ClampEq(gainsDb[7]);
            trackBar_8khz.Value = ClampEq(gainsDb[8]);
            trackBar_16khz.Value = ClampEq(gainsDb[9]);

            // Gui xuong FPGA
            SendEqBand(0, trackBar_31hz.Value);
            SendEqBand(1, trackBar_62hz.Value);
            SendEqBand(2, trackBar_125hz.Value);
            SendEqBand(3, trackBar_250hz.Value);
            SendEqBand(4, trackBar_500hz.Value);
            SendEqBand(5, trackBar_1khz.Value);
            SendEqBand(6, trackBar_2khz.Value);
            SendEqBand(7, trackBar_4khz.Value);
            SendEqBand(8, trackBar_8khz.Value);
            SendEqBand(9, trackBar_16khz.Value);

            LogInfo("EQ preset applied.");
        }

        private int ClampEq(int v)
        {
            if (v < -12) return -12;
            if (v > 12) return 12;
            return v;
        }

        private void SendEqBand(byte bandIndex, int dbValue)
        {
            try
            {
                if (serialPort1 == null || !serialPort1.IsOpen)
                    return;

                // Map -12..+12 -> 0..24
                byte gainCode = (byte)(dbValue + 12);

                byte[] frame = new byte[]
                {
            (byte)'B',
            bandIndex,
            gainCode
                };

                serialPort1.Write(frame, 0, frame.Length);
                LogTx($"BAND {bandIndex} = {dbValue} dB, raw=0x{gainCode:X2}");
            }
            catch (Exception ex)
            {
                LogInfo("Send EQ band error: " + ex.Message);
            }
        }

        private void InitEqTrackBar(System.Windows.Forms.TrackBar tb, int min, int max, int value)
        {
            tb.Minimum = min;
            tb.Maximum = max;
            tb.Value = value;
            tb.TickFrequency = 3;
            tb.SmallChange = 1;
            tb.LargeChange = 2;
        }

        private void InitCodecTrackBar(System.Windows.Forms.TrackBar tb, int min, int max, int value)
        {
            tb.Minimum = min;
            tb.Maximum = max;
            tb.Value = value;
            tb.TickFrequency = (max - min) / 8;
            if (tb.TickFrequency <= 0) tb.TickFrequency = 1;
            tb.SmallChange = 1;
            tb.LargeChange = 2;
        }
        private void InitTrackBars()
        {
            InitEqTrackBar(trackBar_31hz, -12, 12, 0);
            InitEqTrackBar(trackBar_62hz, -12, 12, 0);
            InitEqTrackBar(trackBar_125hz, -12, 12, 0);
            InitEqTrackBar(trackBar_250hz, -12, 12, 0);
            InitEqTrackBar(trackBar_500hz, -12, 12, 0);
            InitEqTrackBar(trackBar_1khz, -12, 12, 0);
            InitEqTrackBar(trackBar_2khz, -12, 12, 0);
            InitEqTrackBar(trackBar_4khz, -12, 12, 0);
            InitEqTrackBar(trackBar_8khz, -12, 12, 0);
            InitEqTrackBar(trackBar_16khz, -12, 12, 0);

            InitCodecTrackBar(track_vol, 0, 127, 100);
            InitCodecTrackBar(trackBar_gain, 0, 31, 23);
        }

        private void btnLog_Click(object sender, EventArgs e)
        {
            if (logForm == null || logForm.IsDisposed)
            {
                logForm = new FormLog();
                logForm.Show();
                logForm.AddInfoLog("Log window opened.");
            }
            else
            {
                if (!logForm.Visible)
                    logForm.Show();

                logForm.BringToFront();
                logForm.Focus();
            }
        }

        private void LogTx(string text)
        {
            if (logForm != null && !logForm.IsDisposed)
            {
                logForm.AddTxLog(text);
            }
        }

        private void LogRx(string text)
        {
            if (logForm != null && !logForm.IsDisposed)
            {
                logForm.AddRxLog(text);
            }
        }

        private void LogInfo(string text)
        {
            if (logForm != null && !logForm.IsDisposed)
            {
                logForm.AddInfoLog(text);
            }
        }

        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                string data = serialPort1.ReadExisting();

                this.BeginInvoke(new Action(() =>
                {
                    LogRx(data);

                    // Neu ban co label hien thi info
                    // label5.Text = data;
                }));
            }
            catch (Exception ex)
            {
                this.BeginInvoke(new Action(() =>
                {
                    LogInfo("Receive error: " + ex.Message);
                }));
            }
        }

        private void track_vol_Scroll(object sender, EventArgs e)
        {
            byte rawVol = (byte)(track_vol.Value & 0x7F);
            SendVolumeRaw(rawVol);
        }

        private void SendVolumeRaw(byte rawVol)
        {
            try
            {
                if (serialPort1 == null || !serialPort1.IsOpen)
                    return;

                byte[] frame = new byte[] { (byte)'V', rawVol };
                serialPort1.Write(frame, 0, frame.Length);
                LogTx($"VOL raw=0x{rawVol:X2}");
            }
            catch (Exception ex)
            {
                LogInfo("Send VOL error: " + ex.Message);
            }
        }

        private void trackBar_gain_Scroll(object sender, EventArgs e)
        {
            byte rawGain = (byte)(trackBar_gain.Value & 0x1F);
            SendGainRaw(rawGain);
        }

        // Goi khi thay doi trackBar_gain
        private void SendGainRaw(byte rawGain)
        {
            try
            {
                if (serialPort1 == null || !serialPort1.IsOpen)
                    return;

                byte[] frame = new byte[] { (byte)'G', rawGain };
                serialPort1.Write(frame, 0, frame.Length);
                LogTx($"GAIN raw=0x{rawGain:X2}");
            }
            catch (Exception ex)
            {
                LogInfo("Send GAIN error: " + ex.Message);
            }
        }

        private void btnFlat_Click(object sender, EventArgs e)
        {
            ApplyEqPreset(EQ_FLAT);
        }

        private void btnBass_Click(object sender, EventArgs e)
        {
            ApplyEqPreset(EQ_BASS);
        }

        private void btnRock_Click(object sender, EventArgs e)
        {
            ApplyEqPreset(EQ_ROCK);
        }

        private void btnVocal_Click(object sender, EventArgs e)
        {
            ApplyEqPreset(EQ_VOCAL);
        }

        private void btnTreble_Click(object sender, EventArgs e)
        {
            ApplyEqPreset(EQ_TREBLE);
        }

        private void btnPop_Click(object sender, EventArgs e)
        {
            ApplyEqPreset(EQ_POP);
        }
    }
}

using System;
using System.Drawing;
using System.Windows.Forms;

namespace Audio_Equalizer
{
    public partial class FormLog : Form
    {
        public FormLog()
        {
            //InitializeComponent();
            InitUi();
        }

        private RichTextBox rtbLog;
        private Button btnClear;

        private void InitUi()
        {
            this.Text = "UART Log";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Size = new Size(900, 500);

            btnClear = new Button();
            btnClear.Text = "Clear";
            btnClear.Width = 80;
            btnClear.Height = 30;
            btnClear.Top = 10;
            btnClear.Left = 10;
            btnClear.Click += BtnClear_Click;
            this.Controls.Add(btnClear);

            rtbLog = new RichTextBox();
            rtbLog.ReadOnly = true;
            rtbLog.Font = new Font("Consolas", 10F);
            rtbLog.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            rtbLog.Location = new Point(10, 50);
            rtbLog.Size = new Size(this.ClientSize.Width - 20, this.ClientSize.Height - 60);
            rtbLog.BackColor = Color.White;
            rtbLog.ForeColor = Color.Black;
            this.Controls.Add(rtbLog);

            this.Resize += FormLog_Resize;
        }

        private void FormLog_Resize(object sender, EventArgs e)
        {
            if (rtbLog != null)
            {
                rtbLog.Size = new Size(this.ClientSize.Width - 20, this.ClientSize.Height - 60);
            }
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            rtbLog.Clear();
        }

        public void AddLog(string text, Color color)
        {
            if (rtbLog.InvokeRequired)
            {
                rtbLog.Invoke(new Action(() => AddLog(text, color)));
                return;
            }

            rtbLog.SelectionStart = rtbLog.TextLength;
            rtbLog.SelectionLength = 0;
            rtbLog.SelectionColor = color;
            rtbLog.AppendText(text + Environment.NewLine);
            rtbLog.SelectionColor = rtbLog.ForeColor;
            rtbLog.ScrollToCaret();
        }

        public void AddTxLog(string text)
        {
            AddLog("[PC -> FPGA] " + text, Color.Red);
        }

        public void AddRxLog(string text)
        {
            AddLog("[FPGA -> PC] " + text, Color.Blue);
        }

        public void AddInfoLog(string text)
        {
            AddLog("[INFO] " + text, Color.DarkGreen);
        }
    }
}
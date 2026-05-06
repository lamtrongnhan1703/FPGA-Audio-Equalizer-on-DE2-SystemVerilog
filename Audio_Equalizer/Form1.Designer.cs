namespace Audio_Equalizer
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btn_refresh = new System.Windows.Forms.Button();
            this.cb_port = new System.Windows.Forms.ComboBox();
            this.btnConnect = new System.Windows.Forms.Button();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.track_vol = new System.Windows.Forms.TrackBar();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.trackBar_31hz = new System.Windows.Forms.TrackBar();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.trackBar_62hz = new System.Windows.Forms.TrackBar();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.trackBar_125hz = new System.Windows.Forms.TrackBar();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.trackBar_250hz = new System.Windows.Forms.TrackBar();
            this.panel6 = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.trackBar_500hz = new System.Windows.Forms.TrackBar();
            this.panel7 = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.trackBar_1khz = new System.Windows.Forms.TrackBar();
            this.panel8 = new System.Windows.Forms.Panel();
            this.label9 = new System.Windows.Forms.Label();
            this.trackBar_2khz = new System.Windows.Forms.TrackBar();
            this.panel9 = new System.Windows.Forms.Panel();
            this.label10 = new System.Windows.Forms.Label();
            this.trackBar_4khz = new System.Windows.Forms.TrackBar();
            this.panel10 = new System.Windows.Forms.Panel();
            this.label11 = new System.Windows.Forms.Label();
            this.trackBar_8khz = new System.Windows.Forms.TrackBar();
            this.panel11 = new System.Windows.Forms.Panel();
            this.label12 = new System.Windows.Forms.Label();
            this.trackBar_16khz = new System.Windows.Forms.TrackBar();
            this.panel12 = new System.Windows.Forms.Panel();
            this.label13 = new System.Windows.Forms.Label();
            this.trackBar_gain = new System.Windows.Forms.TrackBar();
            this.btnLog = new System.Windows.Forms.Button();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.btnFlat = new System.Windows.Forms.Button();
            this.btnBass = new System.Windows.Forms.Button();
            this.btnRock = new System.Windows.Forms.Button();
            this.btnVocal = new System.Windows.Forms.Button();
            this.btnTreble = new System.Windows.Forms.Button();
            this.btnPop = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.track_vol)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_31hz)).BeginInit();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_62hz)).BeginInit();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_125hz)).BeginInit();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_250hz)).BeginInit();
            this.panel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_500hz)).BeginInit();
            this.panel7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_1khz)).BeginInit();
            this.panel8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_2khz)).BeginInit();
            this.panel9.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_4khz)).BeginInit();
            this.panel10.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_8khz)).BeginInit();
            this.panel11.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_16khz)).BeginInit();
            this.panel12.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_gain)).BeginInit();
            this.tableLayoutPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 55F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 47F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1001, 459);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnLog);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.btn_refresh);
            this.groupBox1.Controls.Add(this.cb_port);
            this.groupBox1.Controls.Add(this.btnConnect);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(995, 49);
            this.groupBox1.TabIndex = 25;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Connect";
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(369, 16);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(531, 31);
            this.label5.TabIndex = 22;
            this.label5.Text = "info";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btn_refresh
            // 
            this.btn_refresh.BackColor = System.Drawing.Color.Gainsboro;
            this.btn_refresh.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btn_refresh.Location = new System.Drawing.Point(279, 16);
            this.btn_refresh.Margin = new System.Windows.Forms.Padding(4);
            this.btn_refresh.Name = "btn_refresh";
            this.btn_refresh.Size = new System.Drawing.Size(83, 31);
            this.btn_refresh.TabIndex = 18;
            this.btn_refresh.Text = "Refresh";
            this.btn_refresh.UseVisualStyleBackColor = false;
            // 
            // cb_port
            // 
            this.cb_port.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_port.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.cb_port.FormattingEnabled = true;
            this.cb_port.Location = new System.Drawing.Point(6, 19);
            this.cb_port.Margin = new System.Windows.Forms.Padding(4);
            this.cb_port.Name = "cb_port";
            this.cb_port.Size = new System.Drawing.Size(135, 24);
            this.cb_port.TabIndex = 16;
            // 
            // btnConnect
            // 
            this.btnConnect.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnConnect.Location = new System.Drawing.Point(149, 16);
            this.btnConnect.Margin = new System.Windows.Forms.Padding(4);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(122, 31);
            this.btnConnect.TabIndex = 17;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // serialPort1
            // 
            this.serialPort1.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPort1_DataReceived);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 12;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8.333332F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8.333332F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8.333332F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8.333332F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8.333332F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8.333332F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8.333332F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8.333332F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8.333332F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8.333332F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8.333332F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8.333332F));
            this.tableLayoutPanel2.Controls.Add(this.panel12, 11, 0);
            this.tableLayoutPanel2.Controls.Add(this.panel11, 10, 0);
            this.tableLayoutPanel2.Controls.Add(this.panel10, 9, 0);
            this.tableLayoutPanel2.Controls.Add(this.panel9, 8, 0);
            this.tableLayoutPanel2.Controls.Add(this.panel8, 7, 0);
            this.tableLayoutPanel2.Controls.Add(this.panel7, 6, 0);
            this.tableLayoutPanel2.Controls.Add(this.panel6, 5, 0);
            this.tableLayoutPanel2.Controls.Add(this.panel5, 4, 0);
            this.tableLayoutPanel2.Controls.Add(this.panel4, 3, 0);
            this.tableLayoutPanel2.Controls.Add(this.panel3, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.panel2, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 58);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(995, 351);
            this.tableLayoutPanel2.TabIndex = 26;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Cornsilk;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.track_vol);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(76, 345);
            this.panel1.TabIndex = 0;
            // 
            // track_vol
            // 
            this.track_vol.Location = new System.Drawing.Point(16, 44);
            this.track_vol.Name = "track_vol";
            this.track_vol.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.track_vol.Size = new System.Drawing.Size(45, 294);
            this.track_vol.TabIndex = 0;
            this.track_vol.Scroll += new System.EventHandler(this.track_vol_Scroll);
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 24);
            this.label1.TabIndex = 1;
            this.label1.Text = "VOL";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.SeaShell;
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.trackBar_31hz);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(85, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(76, 345);
            this.panel2.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 24);
            this.label2.TabIndex = 1;
            this.label2.Text = "31 Hz";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // trackBar_31hz
            // 
            this.trackBar_31hz.Location = new System.Drawing.Point(16, 44);
            this.trackBar_31hz.Name = "trackBar_31hz";
            this.trackBar_31hz.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.trackBar_31hz.Size = new System.Drawing.Size(45, 294);
            this.trackBar_31hz.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.SeaShell;
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.trackBar_62hz);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(167, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(76, 345);
            this.panel3.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.Dock = System.Windows.Forms.DockStyle.Top;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(0, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 24);
            this.label3.TabIndex = 1;
            this.label3.Text = "62 Hz";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // trackBar_62hz
            // 
            this.trackBar_62hz.Location = new System.Drawing.Point(16, 44);
            this.trackBar_62hz.Name = "trackBar_62hz";
            this.trackBar_62hz.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.trackBar_62hz.Size = new System.Drawing.Size(45, 294);
            this.trackBar_62hz.TabIndex = 0;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.SeaShell;
            this.panel4.Controls.Add(this.label4);
            this.panel4.Controls.Add(this.trackBar_125hz);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(249, 3);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(76, 345);
            this.panel4.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.Dock = System.Windows.Forms.DockStyle.Top;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(0, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(76, 24);
            this.label4.TabIndex = 1;
            this.label4.Text = "125 Hz";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // trackBar_125hz
            // 
            this.trackBar_125hz.Location = new System.Drawing.Point(16, 44);
            this.trackBar_125hz.Name = "trackBar_125hz";
            this.trackBar_125hz.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.trackBar_125hz.Size = new System.Drawing.Size(45, 294);
            this.trackBar_125hz.TabIndex = 0;
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.SeaShell;
            this.panel5.Controls.Add(this.label6);
            this.panel5.Controls.Add(this.trackBar_250hz);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(331, 3);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(76, 345);
            this.panel5.TabIndex = 4;
            // 
            // label6
            // 
            this.label6.Dock = System.Windows.Forms.DockStyle.Top;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(0, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(76, 24);
            this.label6.TabIndex = 1;
            this.label6.Text = "250 Hz";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // trackBar_250hz
            // 
            this.trackBar_250hz.Location = new System.Drawing.Point(16, 44);
            this.trackBar_250hz.Name = "trackBar_250hz";
            this.trackBar_250hz.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.trackBar_250hz.Size = new System.Drawing.Size(45, 294);
            this.trackBar_250hz.TabIndex = 0;
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.SeaShell;
            this.panel6.Controls.Add(this.label7);
            this.panel6.Controls.Add(this.trackBar_500hz);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel6.Location = new System.Drawing.Point(413, 3);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(76, 345);
            this.panel6.TabIndex = 5;
            // 
            // label7
            // 
            this.label7.Dock = System.Windows.Forms.DockStyle.Top;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(0, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(76, 24);
            this.label7.TabIndex = 1;
            this.label7.Text = "500 Hz";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // trackBar_500hz
            // 
            this.trackBar_500hz.Location = new System.Drawing.Point(16, 44);
            this.trackBar_500hz.Name = "trackBar_500hz";
            this.trackBar_500hz.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.trackBar_500hz.Size = new System.Drawing.Size(45, 294);
            this.trackBar_500hz.TabIndex = 0;
            // 
            // panel7
            // 
            this.panel7.BackColor = System.Drawing.Color.SeaShell;
            this.panel7.Controls.Add(this.label8);
            this.panel7.Controls.Add(this.trackBar_1khz);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel7.Location = new System.Drawing.Point(495, 3);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(76, 345);
            this.panel7.TabIndex = 6;
            // 
            // label8
            // 
            this.label8.Dock = System.Windows.Forms.DockStyle.Top;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(0, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(76, 24);
            this.label8.TabIndex = 1;
            this.label8.Text = "1 kHz";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // trackBar_1khz
            // 
            this.trackBar_1khz.Location = new System.Drawing.Point(16, 44);
            this.trackBar_1khz.Name = "trackBar_1khz";
            this.trackBar_1khz.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.trackBar_1khz.Size = new System.Drawing.Size(45, 294);
            this.trackBar_1khz.TabIndex = 0;
            // 
            // panel8
            // 
            this.panel8.BackColor = System.Drawing.Color.SeaShell;
            this.panel8.Controls.Add(this.label9);
            this.panel8.Controls.Add(this.trackBar_2khz);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel8.Location = new System.Drawing.Point(577, 3);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(76, 345);
            this.panel8.TabIndex = 7;
            // 
            // label9
            // 
            this.label9.Dock = System.Windows.Forms.DockStyle.Top;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(0, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(76, 24);
            this.label9.TabIndex = 1;
            this.label9.Text = "2 kHz";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // trackBar_2khz
            // 
            this.trackBar_2khz.Location = new System.Drawing.Point(16, 44);
            this.trackBar_2khz.Name = "trackBar_2khz";
            this.trackBar_2khz.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.trackBar_2khz.Size = new System.Drawing.Size(45, 294);
            this.trackBar_2khz.TabIndex = 0;
            // 
            // panel9
            // 
            this.panel9.BackColor = System.Drawing.Color.SeaShell;
            this.panel9.Controls.Add(this.label10);
            this.panel9.Controls.Add(this.trackBar_4khz);
            this.panel9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel9.Location = new System.Drawing.Point(659, 3);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(76, 345);
            this.panel9.TabIndex = 8;
            // 
            // label10
            // 
            this.label10.Dock = System.Windows.Forms.DockStyle.Top;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(0, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(76, 24);
            this.label10.TabIndex = 1;
            this.label10.Text = "4 kHz";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // trackBar_4khz
            // 
            this.trackBar_4khz.Location = new System.Drawing.Point(16, 44);
            this.trackBar_4khz.Name = "trackBar_4khz";
            this.trackBar_4khz.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.trackBar_4khz.Size = new System.Drawing.Size(45, 294);
            this.trackBar_4khz.TabIndex = 0;
            // 
            // panel10
            // 
            this.panel10.BackColor = System.Drawing.Color.SeaShell;
            this.panel10.Controls.Add(this.label11);
            this.panel10.Controls.Add(this.trackBar_8khz);
            this.panel10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel10.Location = new System.Drawing.Point(741, 3);
            this.panel10.Name = "panel10";
            this.panel10.Size = new System.Drawing.Size(76, 345);
            this.panel10.TabIndex = 9;
            // 
            // label11
            // 
            this.label11.Dock = System.Windows.Forms.DockStyle.Top;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(0, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(76, 24);
            this.label11.TabIndex = 1;
            this.label11.Text = "8 kHz";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // trackBar_8khz
            // 
            this.trackBar_8khz.Location = new System.Drawing.Point(16, 44);
            this.trackBar_8khz.Name = "trackBar_8khz";
            this.trackBar_8khz.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.trackBar_8khz.Size = new System.Drawing.Size(45, 294);
            this.trackBar_8khz.TabIndex = 0;
            // 
            // panel11
            // 
            this.panel11.BackColor = System.Drawing.Color.SeaShell;
            this.panel11.Controls.Add(this.label12);
            this.panel11.Controls.Add(this.trackBar_16khz);
            this.panel11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel11.Location = new System.Drawing.Point(823, 3);
            this.panel11.Name = "panel11";
            this.panel11.Size = new System.Drawing.Size(76, 345);
            this.panel11.TabIndex = 10;
            // 
            // label12
            // 
            this.label12.Dock = System.Windows.Forms.DockStyle.Top;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(0, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(76, 24);
            this.label12.TabIndex = 1;
            this.label12.Text = "16 kHz";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // trackBar_16khz
            // 
            this.trackBar_16khz.Location = new System.Drawing.Point(16, 44);
            this.trackBar_16khz.Name = "trackBar_16khz";
            this.trackBar_16khz.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.trackBar_16khz.Size = new System.Drawing.Size(45, 294);
            this.trackBar_16khz.TabIndex = 0;
            // 
            // panel12
            // 
            this.panel12.BackColor = System.Drawing.SystemColors.Window;
            this.panel12.Controls.Add(this.label13);
            this.panel12.Controls.Add(this.trackBar_gain);
            this.panel12.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel12.Location = new System.Drawing.Point(905, 3);
            this.panel12.Name = "panel12";
            this.panel12.Size = new System.Drawing.Size(87, 345);
            this.panel12.TabIndex = 11;
            // 
            // label13
            // 
            this.label13.Dock = System.Windows.Forms.DockStyle.Top;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(0, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(87, 24);
            this.label13.TabIndex = 1;
            this.label13.Text = "GAIN";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // trackBar_gain
            // 
            this.trackBar_gain.Location = new System.Drawing.Point(16, 44);
            this.trackBar_gain.Name = "trackBar_gain";
            this.trackBar_gain.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.trackBar_gain.Size = new System.Drawing.Size(45, 294);
            this.trackBar_gain.TabIndex = 0;
            this.trackBar_gain.Scroll += new System.EventHandler(this.trackBar_gain_Scroll);
            // 
            // btnLog
            // 
            this.btnLog.Location = new System.Drawing.Point(914, 16);
            this.btnLog.Name = "btnLog";
            this.btnLog.Size = new System.Drawing.Size(75, 27);
            this.btnLog.TabIndex = 23;
            this.btnLog.Text = "Log";
            this.btnLog.UseVisualStyleBackColor = true;
            this.btnLog.Click += new System.EventHandler(this.btnLog_Click);
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 6;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel3.Controls.Add(this.btnPop, 5, 0);
            this.tableLayoutPanel3.Controls.Add(this.btnTreble, 4, 0);
            this.tableLayoutPanel3.Controls.Add(this.btnVocal, 3, 0);
            this.tableLayoutPanel3.Controls.Add(this.btnRock, 2, 0);
            this.tableLayoutPanel3.Controls.Add(this.btnBass, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.btnFlat, 0, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 415);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(995, 41);
            this.tableLayoutPanel3.TabIndex = 27;
            // 
            // btnFlat
            // 
            this.btnFlat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnFlat.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFlat.Location = new System.Drawing.Point(3, 3);
            this.btnFlat.Name = "btnFlat";
            this.btnFlat.Size = new System.Drawing.Size(159, 35);
            this.btnFlat.TabIndex = 0;
            this.btnFlat.Text = "Flat";
            this.btnFlat.UseVisualStyleBackColor = true;
            this.btnFlat.Click += new System.EventHandler(this.btnFlat_Click);
            // 
            // btnBass
            // 
            this.btnBass.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnBass.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBass.Location = new System.Drawing.Point(168, 3);
            this.btnBass.Name = "btnBass";
            this.btnBass.Size = new System.Drawing.Size(159, 35);
            this.btnBass.TabIndex = 1;
            this.btnBass.Text = "Bass";
            this.btnBass.UseVisualStyleBackColor = true;
            this.btnBass.Click += new System.EventHandler(this.btnBass_Click);
            // 
            // btnRock
            // 
            this.btnRock.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnRock.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRock.Location = new System.Drawing.Point(333, 3);
            this.btnRock.Name = "btnRock";
            this.btnRock.Size = new System.Drawing.Size(159, 35);
            this.btnRock.TabIndex = 2;
            this.btnRock.Text = "Rock";
            this.btnRock.UseVisualStyleBackColor = true;
            this.btnRock.Click += new System.EventHandler(this.btnRock_Click);
            // 
            // btnVocal
            // 
            this.btnVocal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnVocal.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVocal.Location = new System.Drawing.Point(498, 3);
            this.btnVocal.Name = "btnVocal";
            this.btnVocal.Size = new System.Drawing.Size(159, 35);
            this.btnVocal.TabIndex = 3;
            this.btnVocal.Text = "Vocal";
            this.btnVocal.UseVisualStyleBackColor = true;
            this.btnVocal.Click += new System.EventHandler(this.btnVocal_Click);
            // 
            // btnTreble
            // 
            this.btnTreble.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnTreble.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTreble.Location = new System.Drawing.Point(663, 3);
            this.btnTreble.Name = "btnTreble";
            this.btnTreble.Size = new System.Drawing.Size(159, 35);
            this.btnTreble.TabIndex = 4;
            this.btnTreble.Text = "Treble";
            this.btnTreble.UseVisualStyleBackColor = true;
            this.btnTreble.Click += new System.EventHandler(this.btnTreble_Click);
            // 
            // btnPop
            // 
            this.btnPop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnPop.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPop.Location = new System.Drawing.Point(828, 3);
            this.btnPop.Name = "btnPop";
            this.btnPop.Size = new System.Drawing.Size(164, 35);
            this.btnPop.TabIndex = 5;
            this.btnPop.Text = "Pop";
            this.btnPop.UseVisualStyleBackColor = true;
            this.btnPop.Click += new System.EventHandler(this.btnPop_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1001, 459);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Audio_Equalizer";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.track_vol)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_31hz)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_62hz)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_125hz)).EndInit();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_250hz)).EndInit();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_500hz)).EndInit();
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_1khz)).EndInit();
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_2khz)).EndInit();
            this.panel9.ResumeLayout(false);
            this.panel9.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_4khz)).EndInit();
            this.panel10.ResumeLayout(false);
            this.panel10.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_8khz)).EndInit();
            this.panel11.ResumeLayout(false);
            this.panel11.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_16khz)).EndInit();
            this.panel12.ResumeLayout(false);
            this.panel12.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_gain)).EndInit();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btn_refresh;
        private System.Windows.Forms.ComboBox cb_port;
        private System.Windows.Forms.Button btnConnect;
        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TrackBar track_vol;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TrackBar trackBar_gain;
        private System.Windows.Forms.Panel panel11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TrackBar trackBar_16khz;
        private System.Windows.Forms.Panel panel10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TrackBar trackBar_8khz;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TrackBar trackBar_4khz;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TrackBar trackBar_2khz;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TrackBar trackBar_1khz;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TrackBar trackBar_500hz;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TrackBar trackBar_250hz;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TrackBar trackBar_125hz;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TrackBar trackBar_62hz;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TrackBar trackBar_31hz;
        private System.Windows.Forms.Button btnLog;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Button btnPop;
        private System.Windows.Forms.Button btnTreble;
        private System.Windows.Forms.Button btnVocal;
        private System.Windows.Forms.Button btnRock;
        private System.Windows.Forms.Button btnBass;
        private System.Windows.Forms.Button btnFlat;
    }
}


namespace AnnarComMICROSESV60.Forms
{
    partial class Resultados
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Resultados));
            this.timerIntervalos = new System.Windows.Forms.Timer(this.components);
            this.lblIntervalos = new System.Windows.Forms.Label();
            this.flpContenedorResul = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlSubMenu = new System.Windows.Forms.Panel();
            this.cmbPortName = new System.Windows.Forms.ComboBox();
            this.lblComPort = new System.Windows.Forms.Label();
            this.lblBaudRate = new System.Windows.Forms.Label();
            this.lblParity = new System.Windows.Forms.Label();
            this.lblStopBits = new System.Windows.Forms.Label();
            this.lblDataBits = new System.Windows.Forms.Label();
            this.cmbStopBits = new System.Windows.Forms.ComboBox();
            this.cmbDataBits = new System.Windows.Forms.ComboBox();
            this.cmbParity = new System.Windows.Forms.ComboBox();
            this.cmbBaudRate = new System.Windows.Forms.ComboBox();
            this.pbStop = new System.Windows.Forms.PictureBox();
            this.pbData = new System.Windows.Forms.PictureBox();
            this.pbParity = new System.Windows.Forms.PictureBox();
            this.pbBaud = new System.Windows.Forms.PictureBox();
            this.pbPuerto = new System.Windows.Forms.PictureBox();
            this.pictureBox6 = new System.Windows.Forms.PictureBox();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.chkDSR = new System.Windows.Forms.CheckBox();
            this.chkCD = new System.Windows.Forms.CheckBox();
            this.chkCTS = new System.Windows.Forms.CheckBox();
            this.rbText = new System.Windows.Forms.RadioButton();
            this.rbHex = new System.Windows.Forms.RadioButton();
            this.chkClearWithDTR = new System.Windows.Forms.CheckBox();
            this.chkClearOnOpen = new System.Windows.Forms.CheckBox();
            this.chkRTS = new System.Windows.Forms.CheckBox();
            this.chkDTR = new System.Windows.Forms.CheckBox();
            this.tmrCheckComPorts = new System.Windows.Forms.Timer(this.components);
            this.btnOpenPort = new System.Windows.Forms.Button();
            this.rjButton1 = new CustomControls.RJControls.RJButton();
            this.pnlSubMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbStop)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbParity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbBaud)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPuerto)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // timerIntervalos
            // 
            this.timerIntervalos.Tick += new System.EventHandler(this.timerIntervalos_Tick);
            // 
            // lblIntervalos
            // 
            this.lblIntervalos.BackColor = System.Drawing.Color.Transparent;
            this.lblIntervalos.Location = new System.Drawing.Point(699, 583);
            this.lblIntervalos.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblIntervalos.Name = "lblIntervalos";
            this.lblIntervalos.Size = new System.Drawing.Size(35, 15);
            this.lblIntervalos.TabIndex = 0;
            this.lblIntervalos.Text = "Timer";
            this.lblIntervalos.Visible = false;
            // 
            // flpContenedorResul
            // 
            this.flpContenedorResul.AutoScroll = true;
            this.flpContenedorResul.BackColor = System.Drawing.Color.White;
            this.flpContenedorResul.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpContenedorResul.Font = new System.Drawing.Font("Century Gothic", 9.25F, System.Drawing.FontStyle.Bold);
            this.flpContenedorResul.Location = new System.Drawing.Point(24, 315);
            this.flpContenedorResul.Name = "flpContenedorResul";
            this.flpContenedorResul.Size = new System.Drawing.Size(748, 265);
            this.flpContenedorResul.TabIndex = 16;
            this.flpContenedorResul.WrapContents = false;
            this.flpContenedorResul.Paint += new System.Windows.Forms.PaintEventHandler(this.flpContenedorResul_Paint);
            // 
            // pnlSubMenu
            // 
            this.pnlSubMenu.AutoScroll = true;
            this.pnlSubMenu.BackColor = System.Drawing.Color.White;
            this.pnlSubMenu.Controls.Add(this.cmbPortName);
            this.pnlSubMenu.Controls.Add(this.lblComPort);
            this.pnlSubMenu.Controls.Add(this.lblBaudRate);
            this.pnlSubMenu.Controls.Add(this.lblParity);
            this.pnlSubMenu.Controls.Add(this.lblStopBits);
            this.pnlSubMenu.Controls.Add(this.lblDataBits);
            this.pnlSubMenu.Controls.Add(this.cmbStopBits);
            this.pnlSubMenu.Controls.Add(this.cmbDataBits);
            this.pnlSubMenu.Controls.Add(this.cmbParity);
            this.pnlSubMenu.Controls.Add(this.cmbBaudRate);
            this.pnlSubMenu.Controls.Add(this.pbStop);
            this.pnlSubMenu.Controls.Add(this.pbData);
            this.pnlSubMenu.Controls.Add(this.pbParity);
            this.pnlSubMenu.Controls.Add(this.pbBaud);
            this.pnlSubMenu.Controls.Add(this.pbPuerto);
            this.pnlSubMenu.Controls.Add(this.pictureBox6);
            this.pnlSubMenu.Controls.Add(this.pictureBox5);
            this.pnlSubMenu.Controls.Add(this.pictureBox4);
            this.pnlSubMenu.Controls.Add(this.pictureBox3);
            this.pnlSubMenu.Controls.Add(this.pictureBox2);
            this.pnlSubMenu.Controls.Add(this.pictureBox1);
            this.pnlSubMenu.Font = new System.Drawing.Font("Microsoft YaHei", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlSubMenu.Location = new System.Drawing.Point(24, 100);
            this.pnlSubMenu.Name = "pnlSubMenu";
            this.pnlSubMenu.Size = new System.Drawing.Size(748, 152);
            this.pnlSubMenu.TabIndex = 27;
            this.pnlSubMenu.Visible = false;
            this.pnlSubMenu.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlSubMenu_Paint);
            // 
            // cmbPortName
            // 
            this.cmbPortName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPortName.FormattingEnabled = true;
            this.cmbPortName.Items.AddRange(new object[] {
            "7",
            "8",
            "9"});
            this.cmbPortName.Location = new System.Drawing.Point(83, 30);
            this.cmbPortName.Name = "cmbPortName";
            this.cmbPortName.Size = new System.Drawing.Size(143, 24);
            this.cmbPortName.TabIndex = 29;
            // 
            // lblComPort
            // 
            this.lblComPort.AutoSize = true;
            this.lblComPort.BackColor = System.Drawing.Color.Transparent;
            this.lblComPort.Font = new System.Drawing.Font("Microsoft YaHei", 8F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblComPort.ForeColor = System.Drawing.Color.DodgerBlue;
            this.lblComPort.Location = new System.Drawing.Point(93, 9);
            this.lblComPort.Name = "lblComPort";
            this.lblComPort.Size = new System.Drawing.Size(62, 16);
            this.lblComPort.TabIndex = 11;
            this.lblComPort.Text = "COM Port:";
            // 
            // lblBaudRate
            // 
            this.lblBaudRate.AutoSize = true;
            this.lblBaudRate.BackColor = System.Drawing.Color.White;
            this.lblBaudRate.Font = new System.Drawing.Font("Microsoft YaHei", 8F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBaudRate.ForeColor = System.Drawing.Color.DodgerBlue;
            this.lblBaudRate.Location = new System.Drawing.Point(319, 9);
            this.lblBaudRate.Name = "lblBaudRate";
            this.lblBaudRate.Size = new System.Drawing.Size(63, 16);
            this.lblBaudRate.TabIndex = 17;
            this.lblBaudRate.Text = "Baud Rate:";
            // 
            // lblParity
            // 
            this.lblParity.AutoSize = true;
            this.lblParity.BackColor = System.Drawing.Color.White;
            this.lblParity.Font = new System.Drawing.Font("Microsoft YaHei", 8F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblParity.ForeColor = System.Drawing.Color.DodgerBlue;
            this.lblParity.Location = new System.Drawing.Point(561, 9);
            this.lblParity.Name = "lblParity";
            this.lblParity.Size = new System.Drawing.Size(40, 16);
            this.lblParity.TabIndex = 18;
            this.lblParity.Text = "Parity:";
            // 
            // lblStopBits
            // 
            this.lblStopBits.AutoSize = true;
            this.lblStopBits.BackColor = System.Drawing.Color.White;
            this.lblStopBits.Font = new System.Drawing.Font("Microsoft YaHei", 8F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStopBits.ForeColor = System.Drawing.Color.DodgerBlue;
            this.lblStopBits.Location = new System.Drawing.Point(319, 74);
            this.lblStopBits.Name = "lblStopBits";
            this.lblStopBits.Size = new System.Drawing.Size(56, 16);
            this.lblStopBits.TabIndex = 20;
            this.lblStopBits.Text = "Stop Bits:";
            // 
            // lblDataBits
            // 
            this.lblDataBits.AutoSize = true;
            this.lblDataBits.BackColor = System.Drawing.Color.White;
            this.lblDataBits.Font = new System.Drawing.Font("Microsoft YaHei", 8F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDataBits.ForeColor = System.Drawing.Color.DodgerBlue;
            this.lblDataBits.Location = new System.Drawing.Point(93, 74);
            this.lblDataBits.Name = "lblDataBits";
            this.lblDataBits.Size = new System.Drawing.Size(56, 16);
            this.lblDataBits.TabIndex = 19;
            this.lblDataBits.Text = "Data Bits:";
            // 
            // cmbStopBits
            // 
            this.cmbStopBits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStopBits.FormattingEnabled = true;
            this.cmbStopBits.Items.AddRange(new object[] {
            "1",
            "2",
            "3"});
            this.cmbStopBits.Location = new System.Drawing.Point(316, 95);
            this.cmbStopBits.Name = "cmbStopBits";
            this.cmbStopBits.Size = new System.Drawing.Size(141, 24);
            this.cmbStopBits.TabIndex = 12;
            // 
            // cmbDataBits
            // 
            this.cmbDataBits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDataBits.FormattingEnabled = true;
            this.cmbDataBits.Items.AddRange(new object[] {
            "7",
            "8",
            "9"});
            this.cmbDataBits.Location = new System.Drawing.Point(83, 95);
            this.cmbDataBits.Name = "cmbDataBits";
            this.cmbDataBits.Size = new System.Drawing.Size(143, 24);
            this.cmbDataBits.TabIndex = 13;
            // 
            // cmbParity
            // 
            this.cmbParity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbParity.FormattingEnabled = true;
            this.cmbParity.Items.AddRange(new object[] {
            "None",
            "Even",
            "Odd"});
            this.cmbParity.Location = new System.Drawing.Point(550, 30);
            this.cmbParity.Name = "cmbParity";
            this.cmbParity.Size = new System.Drawing.Size(143, 24);
            this.cmbParity.TabIndex = 14;
            // 
            // cmbBaudRate
            // 
            this.cmbBaudRate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBaudRate.FormattingEnabled = true;
            this.cmbBaudRate.Items.AddRange(new object[] {
            "1200",
            "2400",
            "4800",
            "9600",
            "19200",
            "38400",
            "57600",
            "115200"});
            this.cmbBaudRate.Location = new System.Drawing.Point(316, 30);
            this.cmbBaudRate.Name = "cmbBaudRate";
            this.cmbBaudRate.Size = new System.Drawing.Size(141, 24);
            this.cmbBaudRate.TabIndex = 15;
            this.cmbBaudRate.SelectedIndexChanged += new System.EventHandler(this.cmbBaudRate_SelectedIndexChanged);
            // 
            // pbStop
            // 
            this.pbStop.BackColor = System.Drawing.Color.DodgerBlue;
            this.pbStop.Location = new System.Drawing.Point(314, 93);
            this.pbStop.Name = "pbStop";
            this.pbStop.Size = new System.Drawing.Size(145, 28);
            this.pbStop.TabIndex = 21;
            this.pbStop.TabStop = false;
            // 
            // pbData
            // 
            this.pbData.BackColor = System.Drawing.Color.DodgerBlue;
            this.pbData.Location = new System.Drawing.Point(81, 93);
            this.pbData.Name = "pbData";
            this.pbData.Size = new System.Drawing.Size(147, 28);
            this.pbData.TabIndex = 22;
            this.pbData.TabStop = false;
            // 
            // pbParity
            // 
            this.pbParity.BackColor = System.Drawing.Color.DodgerBlue;
            this.pbParity.Location = new System.Drawing.Point(548, 28);
            this.pbParity.Name = "pbParity";
            this.pbParity.Size = new System.Drawing.Size(147, 28);
            this.pbParity.TabIndex = 23;
            this.pbParity.TabStop = false;
            // 
            // pbBaud
            // 
            this.pbBaud.BackColor = System.Drawing.Color.DodgerBlue;
            this.pbBaud.Location = new System.Drawing.Point(314, 28);
            this.pbBaud.Name = "pbBaud";
            this.pbBaud.Size = new System.Drawing.Size(145, 28);
            this.pbBaud.TabIndex = 24;
            this.pbBaud.TabStop = false;
            // 
            // pbPuerto
            // 
            this.pbPuerto.BackColor = System.Drawing.Color.DodgerBlue;
            this.pbPuerto.Location = new System.Drawing.Point(81, 28);
            this.pbPuerto.Name = "pbPuerto";
            this.pbPuerto.Size = new System.Drawing.Size(147, 28);
            this.pbPuerto.TabIndex = 25;
            this.pbPuerto.TabStop = false;
            // 
            // pictureBox6
            // 
            this.pictureBox6.BackColor = System.Drawing.Color.White;
            this.pictureBox6.Location = new System.Drawing.Point(488, 85);
            this.pictureBox6.Name = "pictureBox6";
            this.pictureBox6.Size = new System.Drawing.Size(170, 50);
            this.pictureBox6.TabIndex = 28;
            this.pictureBox6.TabStop = false;
            // 
            // pictureBox5
            // 
            this.pictureBox5.BackColor = System.Drawing.Color.White;
            this.pictureBox5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pictureBox5.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox5.Image")));
            this.pictureBox5.Location = new System.Drawing.Point(256, 85);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new System.Drawing.Size(221, 50);
            this.pictureBox5.TabIndex = 28;
            this.pictureBox5.TabStop = false;
            // 
            // pictureBox4
            // 
            this.pictureBox4.BackColor = System.Drawing.Color.White;
            this.pictureBox4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pictureBox4.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox4.Image")));
            this.pictureBox4.Location = new System.Drawing.Point(23, 85);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(223, 50);
            this.pictureBox4.TabIndex = 28;
            this.pictureBox4.TabStop = false;
            // 
            // pictureBox3
            // 
            this.pictureBox3.BackColor = System.Drawing.Color.White;
            this.pictureBox3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox3.Image")));
            this.pictureBox3.Location = new System.Drawing.Point(489, 19);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(223, 50);
            this.pictureBox3.TabIndex = 28;
            this.pictureBox3.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.White;
            this.pictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(256, 19);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(221, 50);
            this.pictureBox2.TabIndex = 28;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.White;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(23, 19);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(223, 50);
            this.pictureBox1.TabIndex = 28;
            this.pictureBox1.TabStop = false;
            // 
            // chkDSR
            // 
            this.chkDSR.AutoSize = true;
            this.chkDSR.Enabled = false;
            this.chkDSR.Location = new System.Drawing.Point(464, 295);
            this.chkDSR.Name = "chkDSR";
            this.chkDSR.Size = new System.Drawing.Size(47, 19);
            this.chkDSR.TabIndex = 29;
            this.chkDSR.Text = "DSR";
            this.chkDSR.UseVisualStyleBackColor = true;
            this.chkDSR.Visible = false;
            // 
            // chkCD
            // 
            this.chkCD.AutoSize = true;
            this.chkCD.Enabled = false;
            this.chkCD.Location = new System.Drawing.Point(410, 295);
            this.chkCD.Name = "chkCD";
            this.chkCD.Size = new System.Drawing.Size(42, 19);
            this.chkCD.TabIndex = 30;
            this.chkCD.Text = "CD";
            this.chkCD.UseVisualStyleBackColor = true;
            this.chkCD.Visible = false;
            // 
            // chkCTS
            // 
            this.chkCTS.AutoSize = true;
            this.chkCTS.Enabled = false;
            this.chkCTS.Location = new System.Drawing.Point(514, 245);
            this.chkCTS.Name = "chkCTS";
            this.chkCTS.Size = new System.Drawing.Size(45, 19);
            this.chkCTS.TabIndex = 28;
            this.chkCTS.Text = "CTS";
            this.chkCTS.UseVisualStyleBackColor = true;
            this.chkCTS.Visible = false;
            // 
            // rbText
            // 
            this.rbText.AutoSize = true;
            this.rbText.Location = new System.Drawing.Point(464, 270);
            this.rbText.Name = "rbText";
            this.rbText.Size = new System.Drawing.Size(46, 19);
            this.rbText.TabIndex = 31;
            this.rbText.Text = "Text";
            this.rbText.Visible = false;
            // 
            // rbHex
            // 
            this.rbHex.AutoSize = true;
            this.rbHex.Location = new System.Drawing.Point(410, 270);
            this.rbHex.Name = "rbHex";
            this.rbHex.Size = new System.Drawing.Size(46, 19);
            this.rbHex.TabIndex = 32;
            this.rbHex.Text = "Hex";
            this.rbHex.Visible = false;
            // 
            // chkClearWithDTR
            // 
            this.chkClearWithDTR.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkClearWithDTR.AutoSize = true;
            this.chkClearWithDTR.Location = new System.Drawing.Point(170, 267);
            this.chkClearWithDTR.Name = "chkClearWithDTR";
            this.chkClearWithDTR.Size = new System.Drawing.Size(103, 19);
            this.chkClearWithDTR.TabIndex = 34;
            this.chkClearWithDTR.Text = "Clear with DTR";
            this.chkClearWithDTR.UseVisualStyleBackColor = true;
            this.chkClearWithDTR.Visible = false;
            // 
            // chkClearOnOpen
            // 
            this.chkClearOnOpen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkClearOnOpen.AutoSize = true;
            this.chkClearOnOpen.Location = new System.Drawing.Point(55, 267);
            this.chkClearOnOpen.Name = "chkClearOnOpen";
            this.chkClearOnOpen.Size = new System.Drawing.Size(105, 19);
            this.chkClearOnOpen.TabIndex = 33;
            this.chkClearOnOpen.Text = "Clear on Open";
            this.chkClearOnOpen.UseVisualStyleBackColor = true;
            this.chkClearOnOpen.Visible = false;
            // 
            // chkRTS
            // 
            this.chkRTS.AutoSize = true;
            this.chkRTS.Location = new System.Drawing.Point(464, 245);
            this.chkRTS.Name = "chkRTS";
            this.chkRTS.Size = new System.Drawing.Size(44, 19);
            this.chkRTS.TabIndex = 36;
            this.chkRTS.Text = "RTS";
            this.chkRTS.UseVisualStyleBackColor = true;
            this.chkRTS.Visible = false;
            // 
            // chkDTR
            // 
            this.chkDTR.AutoSize = true;
            this.chkDTR.Location = new System.Drawing.Point(410, 245);
            this.chkDTR.Name = "chkDTR";
            this.chkDTR.Size = new System.Drawing.Size(46, 19);
            this.chkDTR.TabIndex = 35;
            this.chkDTR.Text = "DTR";
            this.chkDTR.UseVisualStyleBackColor = true;
            this.chkDTR.Visible = false;
            // 
            // tmrCheckComPorts
            // 
            this.tmrCheckComPorts.Interval = 580;
            this.tmrCheckComPorts.Tick += new System.EventHandler(this.tmrCheckComPorts_Tick);
            // 
            // btnOpenPort
            // 
            this.btnOpenPort.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOpenPort.BackColor = System.Drawing.Color.Transparent;
            this.btnOpenPort.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnOpenPort.BackgroundImage")));
            this.btnOpenPort.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnOpenPort.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOpenPort.FlatAppearance.BorderSize = 0;
            this.btnOpenPort.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(242)))), ((int)(((byte)(253)))));
            this.btnOpenPort.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(242)))), ((int)(((byte)(253)))));
            this.btnOpenPort.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOpenPort.ForeColor = System.Drawing.Color.Transparent;
            this.btnOpenPort.Location = new System.Drawing.Point(614, 14);
            this.btnOpenPort.Name = "btnOpenPort";
            this.btnOpenPort.Size = new System.Drawing.Size(167, 50);
            this.btnOpenPort.TabIndex = 18;
            this.btnOpenPort.UseVisualStyleBackColor = false;
            this.btnOpenPort.Click += new System.EventHandler(this.btnOpenPort_Click);
            // 
            // rjButton1
            // 
            this.rjButton1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(189)))), ((int)(((byte)(255)))));
            this.rjButton1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(189)))), ((int)(((byte)(255)))));
            this.rjButton1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("rjButton1.BackgroundImage")));
            this.rjButton1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.rjButton1.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.rjButton1.BorderRadius = 10;
            this.rjButton1.BorderSize = 0;
            this.rjButton1.FlatAppearance.BorderSize = 0;
            this.rjButton1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(189)))), ((int)(((byte)(255)))));
            this.rjButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rjButton1.Font = new System.Drawing.Font("Century Gothic", 18F, System.Drawing.FontStyle.Bold);
            this.rjButton1.FontSize = 18F;
            this.rjButton1.ForeColor = System.Drawing.Color.White;
            this.rjButton1.Location = new System.Drawing.Point(12, 14);
            this.rjButton1.Name = "rjButton1";
            this.rjButton1.Size = new System.Drawing.Size(596, 57);
            this.rjButton1.TabIndex = 37;
            this.rjButton1.TextColor = System.Drawing.Color.White;
            this.rjButton1.UseVisualStyleBackColor = false;
            this.rjButton1.Click += new System.EventHandler(this.rjButton1_Click);
            // 
            // Resultados
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(242)))), ((int)(((byte)(253)))));
            this.ClientSize = new System.Drawing.Size(797, 606);
            this.Controls.Add(this.rjButton1);
            this.Controls.Add(this.chkRTS);
            this.Controls.Add(this.chkDTR);
            this.Controls.Add(this.chkClearWithDTR);
            this.Controls.Add(this.chkClearOnOpen);
            this.Controls.Add(this.rbText);
            this.Controls.Add(this.rbHex);
            this.Controls.Add(this.chkDSR);
            this.Controls.Add(this.chkCD);
            this.Controls.Add(this.chkCTS);
            this.Controls.Add(this.pnlSubMenu);
            this.Controls.Add(this.btnOpenPort);
            this.Controls.Add(this.lblIntervalos);
            this.Controls.Add(this.flpContenedorResul);
            this.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "Resultados";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Terminal";
            this.Load += new System.EventHandler(this.Terminal_Load);
            this.Shown += new System.EventHandler(this.Resultados_Shown);
            this.pnlSubMenu.ResumeLayout(false);
            this.pnlSubMenu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbStop)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbParity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbBaud)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPuerto)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer timerIntervalos;
        private System.Windows.Forms.Label lblIntervalos;
        private System.Windows.Forms.FlowLayoutPanel flpContenedorResul;
        private System.Windows.Forms.Button btnOpenPort;
        private System.Windows.Forms.Panel pnlSubMenu;
        private System.Windows.Forms.Label lblComPort;
        private System.Windows.Forms.Label lblBaudRate;
        private System.Windows.Forms.Label lblParity;
        private System.Windows.Forms.Label lblStopBits;
        private System.Windows.Forms.Label lblDataBits;
        private System.Windows.Forms.ComboBox cmbStopBits;
        private System.Windows.Forms.ComboBox cmbDataBits;
        private System.Windows.Forms.ComboBox cmbParity;
        private System.Windows.Forms.ComboBox cmbBaudRate;
        private System.Windows.Forms.PictureBox pbStop;
        private System.Windows.Forms.PictureBox pbData;
        private System.Windows.Forms.PictureBox pbBaud;
        private System.Windows.Forms.PictureBox pbPuerto;
        private System.Windows.Forms.PictureBox pictureBox5;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox6;
        private System.Windows.Forms.PictureBox pbParity;
        private System.Windows.Forms.CheckBox chkDSR;
        private System.Windows.Forms.CheckBox chkCD;
        private System.Windows.Forms.CheckBox chkCTS;
        private System.Windows.Forms.RadioButton rbText;
        private System.Windows.Forms.RadioButton rbHex;
        private System.Windows.Forms.CheckBox chkClearWithDTR;
        private System.Windows.Forms.CheckBox chkClearOnOpen;
        private System.Windows.Forms.CheckBox chkRTS;
        private System.Windows.Forms.CheckBox chkDTR;
        private System.Windows.Forms.Timer tmrCheckComPorts;
        private System.Windows.Forms.ComboBox cmbPortName;
        private CustomControls.RJControls.RJButton rjButton1;
    }
}
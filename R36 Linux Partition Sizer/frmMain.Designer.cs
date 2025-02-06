namespace R36_Linux_Partition_Sizer
{
    partial class frmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tbSizeAdjuster = new System.Windows.Forms.TrackBar();
            this.lblSliderTitle = new System.Windows.Forms.Label();
            this.imgRomsPartition = new System.Windows.Forms.PictureBox();
            this.lblBootPartSize = new System.Windows.Forms.Label();
            this.imgBootPartition = new System.Windows.Forms.PictureBox();
            this.imgLinuxPartition = new System.Windows.Forms.PictureBox();
            this.lblRomsPartSize = new System.Windows.Forms.Label();
            this.lblLinuxPartSize = new System.Windows.Forms.Label();
            this.lblNewSize = new System.Windows.Forms.Label();
            this.txtNewSize = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblDriveLttr = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblCardSize = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.rbBase2 = new System.Windows.Forms.RadioButton();
            this.rbBase10 = new System.Windows.Forms.RadioButton();
            this.lblMinimalLinuxPartitionSize = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.imgSDIllustration = new System.Windows.Forms.PictureBox();
            this.LlblYouTube1 = new System.Windows.Forms.LinkLabel();
            this.LlblWebsite = new System.Windows.Forms.LinkLabel();
            this.LlblYouTube2 = new System.Windows.Forms.LinkLabel();
            this.LlblAbout = new System.Windows.Forms.LinkLabel();
            this.LlblHelp = new System.Windows.Forms.LinkLabel();
            this.imgXNLFtLogo = new System.Windows.Forms.PictureBox();
            this.LlblR36Central = new System.Windows.Forms.LinkLabel();
            this.tmrProMode = new System.Windows.Forms.Timer(this.components);
            this.btnExit = new System.Windows.Forms.Button();
            this.btnApply = new System.Windows.Forms.Button();
            this.gbProMode = new System.Windows.Forms.GroupBox();
            this.imgWarning = new System.Windows.Forms.PictureBox();
            this.tmrBlinkWarning = new System.Windows.Forms.Timer(this.components);
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbSizeAdjuster)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgRomsPartition)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgBootPartition)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgLinuxPartition)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgSDIllustration)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgXNLFtLogo)).BeginInit();
            this.gbProMode.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgWarning)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tbSizeAdjuster);
            this.groupBox1.Controls.Add(this.lblSliderTitle);
            this.groupBox1.Controls.Add(this.imgRomsPartition);
            this.groupBox1.Controls.Add(this.lblBootPartSize);
            this.groupBox1.Controls.Add(this.imgBootPartition);
            this.groupBox1.Controls.Add(this.imgLinuxPartition);
            this.groupBox1.Controls.Add(this.lblRomsPartSize);
            this.groupBox1.Controls.Add(this.lblLinuxPartSize);
            this.groupBox1.Location = new System.Drawing.Point(9, 122);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(614, 126);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // tbSizeAdjuster
            // 
            this.tbSizeAdjuster.LargeChange = 1;
            this.tbSizeAdjuster.Location = new System.Drawing.Point(194, 45);
            this.tbSizeAdjuster.Maximum = 50000;
            this.tbSizeAdjuster.Name = "tbSizeAdjuster";
            this.tbSizeAdjuster.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.tbSizeAdjuster.Size = new System.Drawing.Size(320, 45);
            this.tbSizeAdjuster.TabIndex = 4;
            this.tbSizeAdjuster.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.tbSizeAdjuster.ValueChanged += new System.EventHandler(this.tbSizeAdjuster_ValueChanged);
            // 
            // lblSliderTitle
            // 
            this.lblSliderTitle.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblSliderTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSliderTitle.Location = new System.Drawing.Point(194, 18);
            this.lblSliderTitle.Name = "lblSliderTitle";
            this.lblSliderTitle.Size = new System.Drawing.Size(320, 32);
            this.lblSliderTitle.TabIndex = 10;
            this.lblSliderTitle.Text = "Adjust Partition Sizes";
            this.lblSliderTitle.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lblSliderTitle.Click += new System.EventHandler(this.lblSliderTitle_Click);
            // 
            // imgRomsPartition
            // 
            this.imgRomsPartition.BackgroundImage = global::R36_Linux_Partition_Sizer.Properties.Resources.SDRoms;
            this.imgRomsPartition.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.imgRomsPartition.Cursor = System.Windows.Forms.Cursors.Hand;
            this.imgRomsPartition.Location = new System.Drawing.Point(520, 18);
            this.imgRomsPartition.Name = "imgRomsPartition";
            this.imgRomsPartition.Size = new System.Drawing.Size(88, 72);
            this.imgRomsPartition.TabIndex = 9;
            this.imgRomsPartition.TabStop = false;
            this.imgRomsPartition.Click += new System.EventHandler(this.imgRomsPartition_Click);
            // 
            // lblBootPartSize
            // 
            this.lblBootPartSize.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblBootPartSize.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBootPartSize.Location = new System.Drawing.Point(2, 93);
            this.lblBootPartSize.Name = "lblBootPartSize";
            this.lblBootPartSize.Size = new System.Drawing.Size(92, 29);
            this.lblBootPartSize.TabIndex = 8;
            this.lblBootPartSize.Text = "N/A";
            this.lblBootPartSize.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lblBootPartSize.Click += new System.EventHandler(this.imgBootPartition_Click);
            // 
            // imgBootPartition
            // 
            this.imgBootPartition.BackgroundImage = global::R36_Linux_Partition_Sizer.Properties.Resources.SDBoot;
            this.imgBootPartition.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.imgBootPartition.Cursor = System.Windows.Forms.Cursors.Hand;
            this.imgBootPartition.Location = new System.Drawing.Point(6, 18);
            this.imgBootPartition.Name = "imgBootPartition";
            this.imgBootPartition.Size = new System.Drawing.Size(88, 72);
            this.imgBootPartition.TabIndex = 7;
            this.imgBootPartition.TabStop = false;
            this.imgBootPartition.Click += new System.EventHandler(this.imgBootPartition_Click);
            // 
            // imgLinuxPartition
            // 
            this.imgLinuxPartition.BackgroundImage = global::R36_Linux_Partition_Sizer.Properties.Resources.SDLinux;
            this.imgLinuxPartition.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.imgLinuxPartition.Cursor = System.Windows.Forms.Cursors.Hand;
            this.imgLinuxPartition.Location = new System.Drawing.Point(100, 18);
            this.imgLinuxPartition.Name = "imgLinuxPartition";
            this.imgLinuxPartition.Size = new System.Drawing.Size(88, 72);
            this.imgLinuxPartition.TabIndex = 2;
            this.imgLinuxPartition.TabStop = false;
            this.imgLinuxPartition.Click += new System.EventHandler(this.imgLinuxPartition_Click);
            // 
            // lblRomsPartSize
            // 
            this.lblRomsPartSize.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblRomsPartSize.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRomsPartSize.Location = new System.Drawing.Point(520, 93);
            this.lblRomsPartSize.Name = "lblRomsPartSize";
            this.lblRomsPartSize.Size = new System.Drawing.Size(88, 29);
            this.lblRomsPartSize.TabIndex = 6;
            this.lblRomsPartSize.Text = "N/A";
            this.lblRomsPartSize.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lblRomsPartSize.Click += new System.EventHandler(this.imgRomsPartition_Click);
            // 
            // lblLinuxPartSize
            // 
            this.lblLinuxPartSize.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblLinuxPartSize.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLinuxPartSize.Location = new System.Drawing.Point(96, 93);
            this.lblLinuxPartSize.Name = "lblLinuxPartSize";
            this.lblLinuxPartSize.Size = new System.Drawing.Size(92, 29);
            this.lblLinuxPartSize.TabIndex = 5;
            this.lblLinuxPartSize.Text = "N/A";
            this.lblLinuxPartSize.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lblLinuxPartSize.Click += new System.EventHandler(this.imgLinuxPartition_Click);
            // 
            // lblNewSize
            // 
            this.lblNewSize.AutoSize = true;
            this.lblNewSize.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblNewSize.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNewSize.Location = new System.Drawing.Point(6, 12);
            this.lblNewSize.Name = "lblNewSize";
            this.lblNewSize.Size = new System.Drawing.Size(75, 18);
            this.lblNewSize.TabIndex = 12;
            this.lblNewSize.Text = "New Size:";
            this.lblNewSize.Click += new System.EventHandler(this.lblNewSize_Click);
            // 
            // txtNewSize
            // 
            this.txtNewSize.BackColor = System.Drawing.SystemColors.Window;
            this.txtNewSize.Location = new System.Drawing.Point(87, 12);
            this.txtNewSize.Name = "txtNewSize";
            this.txtNewSize.ReadOnly = true;
            this.txtNewSize.Size = new System.Drawing.Size(217, 20);
            this.txtNewSize.TabIndex = 11;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(139, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(145, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "Detected Boot Drive:";
            // 
            // lblDriveLttr
            // 
            this.lblDriveLttr.AutoSize = true;
            this.lblDriveLttr.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblDriveLttr.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDriveLttr.Location = new System.Drawing.Point(335, 37);
            this.lblDriveLttr.Name = "lblDriveLttr";
            this.lblDriveLttr.Size = new System.Drawing.Size(35, 18);
            this.lblDriveLttr.TabIndex = 1;
            this.lblDriveLttr.Text = "N/A";
            this.lblDriveLttr.Click += new System.EventHandler(this.lblDriveLttr_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(139, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(200, 18);
            this.label2.TabIndex = 2;
            this.label2.Text = "Detected (Usable) Card Size:";
            // 
            // lblCardSize
            // 
            this.lblCardSize.AutoSize = true;
            this.lblCardSize.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblCardSize.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCardSize.Location = new System.Drawing.Point(335, 16);
            this.lblCardSize.Name = "lblCardSize";
            this.lblCardSize.Size = new System.Drawing.Size(35, 18);
            this.lblCardSize.TabIndex = 3;
            this.lblCardSize.Text = "N/A";
            this.lblCardSize.Click += new System.EventHandler(this.lblCardSize_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.rbBase2);
            this.groupBox2.Controls.Add(this.rbBase10);
            this.groupBox2.Controls.Add(this.lblMinimalLinuxPartitionSize);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.imgSDIllustration);
            this.groupBox2.Controls.Add(this.lblDriveLttr);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.lblCardSize);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Location = new System.Drawing.Point(9, 5);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(443, 111);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(139, 82);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(127, 18);
            this.label4.TabIndex = 8;
            this.label4.Text = "Calculation Mode:";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // rbBase2
            // 
            this.rbBase2.AutoSize = true;
            this.rbBase2.Checked = true;
            this.rbBase2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbBase2.Location = new System.Drawing.Point(277, 80);
            this.rbBase2.Name = "rbBase2";
            this.rbBase2.Size = new System.Drawing.Size(73, 22);
            this.rbBase2.TabIndex = 7;
            this.rbBase2.TabStop = true;
            this.rbBase2.Text = "Base-2";
            this.rbBase2.UseVisualStyleBackColor = true;
            // 
            // rbBase10
            // 
            this.rbBase10.AutoSize = true;
            this.rbBase10.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbBase10.Location = new System.Drawing.Point(356, 80);
            this.rbBase10.Name = "rbBase10";
            this.rbBase10.Size = new System.Drawing.Size(81, 22);
            this.rbBase10.TabIndex = 6;
            this.rbBase10.Text = "Base-10";
            this.rbBase10.UseVisualStyleBackColor = true;
            this.rbBase10.CheckedChanged += new System.EventHandler(this.rbBase10_CheckedChanged);
            // 
            // lblMinimalLinuxPartitionSize
            // 
            this.lblMinimalLinuxPartitionSize.AutoSize = true;
            this.lblMinimalLinuxPartitionSize.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblMinimalLinuxPartitionSize.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMinimalLinuxPartitionSize.Location = new System.Drawing.Point(335, 59);
            this.lblMinimalLinuxPartitionSize.Name = "lblMinimalLinuxPartitionSize";
            this.lblMinimalLinuxPartitionSize.Size = new System.Drawing.Size(35, 18);
            this.lblMinimalLinuxPartitionSize.TabIndex = 5;
            this.lblMinimalLinuxPartitionSize.Text = "N/A";
            this.lblMinimalLinuxPartitionSize.Click += new System.EventHandler(this.lblMinimalLinuxPartitionSize_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(139, 59);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(192, 18);
            this.label3.TabIndex = 4;
            this.label3.Text = "Minimal Linux Partition Size:";
            // 
            // imgSDIllustration
            // 
            this.imgSDIllustration.BackgroundImage = global::R36_Linux_Partition_Sizer.Properties.Resources.SDCardIllu;
            this.imgSDIllustration.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.imgSDIllustration.Cursor = System.Windows.Forms.Cursors.Hand;
            this.imgSDIllustration.Location = new System.Drawing.Point(6, 10);
            this.imgSDIllustration.Name = "imgSDIllustration";
            this.imgSDIllustration.Size = new System.Drawing.Size(127, 96);
            this.imgSDIllustration.TabIndex = 1;
            this.imgSDIllustration.TabStop = false;
            this.imgSDIllustration.Click += new System.EventHandler(this.imgSDIllustration_Click);
            // 
            // LlblYouTube1
            // 
            this.LlblYouTube1.AutoSize = true;
            this.LlblYouTube1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.LlblYouTube1.Location = new System.Drawing.Point(7, 301);
            this.LlblYouTube1.Name = "LlblYouTube1";
            this.LlblYouTube1.Size = new System.Drawing.Size(175, 13);
            this.LlblYouTube1.TabIndex = 5;
            this.LlblYouTube1.TabStop = true;
            this.LlblYouTube1.Text = "XNL Future Technologies YouTube";
            this.LlblYouTube1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LlblYouTube1_LinkClicked);
            // 
            // LlblWebsite
            // 
            this.LlblWebsite.AutoSize = true;
            this.LlblWebsite.Cursor = System.Windows.Forms.Cursors.Hand;
            this.LlblWebsite.Location = new System.Drawing.Point(436, 301);
            this.LlblWebsite.Name = "LlblWebsite";
            this.LlblWebsite.Size = new System.Drawing.Size(188, 13);
            this.LlblWebsite.TabIndex = 6;
            this.LlblWebsite.TabStop = true;
            this.LlblWebsite.Text = "XNL R36 Linux Partition Sizer Website";
            this.LlblWebsite.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LlblWebsite_LinkClicked);
            // 
            // LlblYouTube2
            // 
            this.LlblYouTube2.AutoSize = true;
            this.LlblYouTube2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.LlblYouTube2.Location = new System.Drawing.Point(192, 301);
            this.LlblYouTube2.Name = "LlblYouTube2";
            this.LlblYouTube2.Size = new System.Drawing.Size(235, 13);
            this.LlblYouTube2.TabIndex = 7;
            this.LlblYouTube2.TabStop = true;
            this.LlblYouTube2.Text = "XNL Future Technologies YouTube (Secondary)";
            this.LlblYouTube2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LlblYouTube2_LinkClicked);
            // 
            // LlblAbout
            // 
            this.LlblAbout.AutoSize = true;
            this.LlblAbout.Cursor = System.Windows.Forms.Cursors.Hand;
            this.LlblAbout.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LlblAbout.Location = new System.Drawing.Point(575, 4);
            this.LlblAbout.Name = "LlblAbout";
            this.LlblAbout.Size = new System.Drawing.Size(51, 18);
            this.LlblAbout.TabIndex = 8;
            this.LlblAbout.TabStop = true;
            this.LlblAbout.Text = "About";
            this.LlblAbout.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LlblAbout_LinkClicked);
            // 
            // LlblHelp
            // 
            this.LlblHelp.AutoSize = true;
            this.LlblHelp.Cursor = System.Windows.Forms.Cursors.Hand;
            this.LlblHelp.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LlblHelp.Location = new System.Drawing.Point(524, 4);
            this.LlblHelp.Name = "LlblHelp";
            this.LlblHelp.Size = new System.Drawing.Size(42, 18);
            this.LlblHelp.TabIndex = 9;
            this.LlblHelp.TabStop = true;
            this.LlblHelp.Text = "Help";
            this.LlblHelp.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LlblHelp_LinkClicked);
            // 
            // imgXNLFtLogo
            // 
            this.imgXNLFtLogo.BackgroundImage = global::R36_Linux_Partition_Sizer.Properties.Resources.XNLFt;
            this.imgXNLFtLogo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.imgXNLFtLogo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.imgXNLFtLogo.Location = new System.Drawing.Point(458, 38);
            this.imgXNLFtLogo.Name = "imgXNLFtLogo";
            this.imgXNLFtLogo.Size = new System.Drawing.Size(165, 52);
            this.imgXNLFtLogo.TabIndex = 10;
            this.imgXNLFtLogo.TabStop = false;
            this.imgXNLFtLogo.Click += new System.EventHandler(this.imgXNLFtLogo_Click);
            // 
            // LlblR36Central
            // 
            this.LlblR36Central.AutoSize = true;
            this.LlblR36Central.Cursor = System.Windows.Forms.Cursors.Hand;
            this.LlblR36Central.Location = new System.Drawing.Point(479, 103);
            this.LlblR36Central.Name = "LlblR36Central";
            this.LlblR36Central.Size = new System.Drawing.Size(134, 13);
            this.LlblR36Central.TabIndex = 11;
            this.LlblR36Central.TabStop = true;
            this.LlblR36Central.Text = "XNL R36S && R36H Central";
            this.LlblR36Central.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LlblR36Central_LinkClicked);
            // 
            // tmrProMode
            // 
            this.tmrProMode.Interval = 1500;
            this.tmrProMode.Tick += new System.EventHandler(this.tmrProMode_Tick);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(504, 254);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(119, 36);
            this.btnExit.TabIndex = 12;
            this.btnExit.Text = "E&xit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnApply
            // 
            this.btnApply.Location = new System.Drawing.Point(379, 254);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(119, 36);
            this.btnApply.TabIndex = 13;
            this.btnApply.Text = "&Save && Exit";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // gbProMode
            // 
            this.gbProMode.Controls.Add(this.lblNewSize);
            this.gbProMode.Controls.Add(this.txtNewSize);
            this.gbProMode.Location = new System.Drawing.Point(62, 249);
            this.gbProMode.Name = "gbProMode";
            this.gbProMode.Size = new System.Drawing.Size(311, 41);
            this.gbProMode.TabIndex = 14;
            this.gbProMode.TabStop = false;
            this.gbProMode.Visible = false;
            // 
            // imgWarning
            // 
            this.imgWarning.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.imgWarning.Cursor = System.Windows.Forms.Cursors.Hand;
            this.imgWarning.Location = new System.Drawing.Point(9, 254);
            this.imgWarning.Name = "imgWarning";
            this.imgWarning.Size = new System.Drawing.Size(36, 36);
            this.imgWarning.TabIndex = 15;
            this.imgWarning.TabStop = false;
            this.imgWarning.Visible = false;
            this.imgWarning.Click += new System.EventHandler(this.imgWarning_Click);
            // 
            // tmrBlinkWarning
            // 
            this.tmrBlinkWarning.Interval = 500;
            this.tmrBlinkWarning.Tick += new System.EventHandler(this.tmrBlinkWarning_Tick);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(631, 323);
            this.Controls.Add(this.imgWarning);
            this.Controls.Add(this.gbProMode);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.LlblR36Central);
            this.Controls.Add(this.imgXNLFtLogo);
            this.Controls.Add(this.LlblHelp);
            this.Controls.Add(this.LlblAbout);
            this.Controls.Add(this.LlblYouTube2);
            this.Controls.Add(this.LlblWebsite);
            this.Controls.Add(this.LlblYouTube1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "XNL R36 Linux Partition Sizer";
            this.Activated += new System.EventHandler(this.frmMain_Activated);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmMain_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.frmMain_KeyUp);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbSizeAdjuster)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgRomsPartition)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgBootPartition)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgLinuxPartition)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgSDIllustration)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgXNLFtLogo)).EndInit();
            this.gbProMode.ResumeLayout(false);
            this.gbProMode.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgWarning)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblDriveLttr;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblCardSize;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblRomsPartSize;
        private System.Windows.Forms.Label lblLinuxPartSize;
        private System.Windows.Forms.TrackBar tbSizeAdjuster;
        private System.Windows.Forms.PictureBox imgSDIllustration;
        private System.Windows.Forms.PictureBox imgLinuxPartition;
        private System.Windows.Forms.Label lblBootPartSize;
        private System.Windows.Forms.PictureBox imgBootPartition;
        private System.Windows.Forms.PictureBox imgRomsPartition;
        private System.Windows.Forms.Label lblSliderTitle;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.LinkLabel LlblYouTube1;
        private System.Windows.Forms.LinkLabel LlblWebsite;
        private System.Windows.Forms.LinkLabel LlblYouTube2;
        private System.Windows.Forms.LinkLabel LlblAbout;
        private System.Windows.Forms.LinkLabel LlblHelp;
        private System.Windows.Forms.Label lblMinimalLinuxPartitionSize;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RadioButton rbBase2;
        private System.Windows.Forms.RadioButton rbBase10;
        private System.Windows.Forms.PictureBox imgXNLFtLogo;
        private System.Windows.Forms.LinkLabel LlblR36Central;
        private System.Windows.Forms.Timer tmrProMode;
        private System.Windows.Forms.Label lblNewSize;
        private System.Windows.Forms.TextBox txtNewSize;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.GroupBox gbProMode;
        private System.Windows.Forms.PictureBox imgWarning;
        private System.Windows.Forms.Timer tmrBlinkWarning;
    }
}


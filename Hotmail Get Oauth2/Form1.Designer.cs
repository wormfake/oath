namespace Hotmail_Get_Oauth2
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.panelMail = new System.Windows.Forms.Panel();
            this.tbListMail = new System.Windows.Forms.TextBox();
            this.panelOut = new System.Windows.Forms.Panel();
            this.tbOut = new System.Windows.Forms.TextBox();
            this.panelError = new System.Windows.Forms.Panel();
            this.tbError = new System.Windows.Forms.TextBox();
            this.panel6 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.cbProxyInFile = new System.Windows.Forms.CheckBox();
            this.cbAccessToken = new System.Windows.Forms.CheckBox();
            this.cbImap = new System.Windows.Forms.CheckBox();
            this.cbLoaiProxy = new System.Windows.Forms.CheckBox();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.nrudSoLuong = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panelProxy = new System.Windows.Forms.Panel();
            this.tbListProxy = new System.Windows.Forms.TextBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.nrudTime = new System.Windows.Forms.NumericUpDown();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pageSetupDialog1 = new System.Windows.Forms.PageSetupDialog();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.panelMail.SuspendLayout();
            this.panelOut.SuspendLayout();
            this.panelError.SuspendLayout();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nrudSoLuong)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panelProxy.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nrudTime)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(4);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1245, 949);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.panelMail);
            this.tabPage1.Controls.Add(this.panelOut);
            this.tabPage1.Controls.Add(this.panelError);
            this.tabPage1.Controls.Add(this.panel6);
            this.tabPage1.Controls.Add(this.panel5);
            this.tabPage1.Location = new System.Drawing.Point(4, 33);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(4);
            this.tabPage1.Size = new System.Drawing.Size(1237, 912);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Main";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // panelMail
            // 
            this.panelMail.Controls.Add(this.tbListMail);
            this.panelMail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMail.Location = new System.Drawing.Point(291, 4);
            this.panelMail.Margin = new System.Windows.Forms.Padding(4);
            this.panelMail.Name = "panelMail";
            this.panelMail.Size = new System.Drawing.Size(942, 230);
            this.panelMail.TabIndex = 4;
            // 
            // tbListMail
            // 
            this.tbListMail.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.tbListMail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbListMail.Location = new System.Drawing.Point(0, 0);
            this.tbListMail.Margin = new System.Windows.Forms.Padding(4);
            this.tbListMail.MaxLength = 327670000;
            this.tbListMail.Multiline = true;
            this.tbListMail.Name = "tbListMail";
            this.tbListMail.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbListMail.Size = new System.Drawing.Size(942, 230);
            this.tbListMail.TabIndex = 1;
            this.tbListMail.WordWrap = false;
            // 
            // panelOut
            // 
            this.panelOut.Controls.Add(this.tbOut);
            this.panelOut.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelOut.Location = new System.Drawing.Point(291, 234);
            this.panelOut.Margin = new System.Windows.Forms.Padding(4);
            this.panelOut.Name = "panelOut";
            this.panelOut.Size = new System.Drawing.Size(942, 337);
            this.panelOut.TabIndex = 3;
            // 
            // tbOut
            // 
            this.tbOut.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.tbOut.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbOut.Location = new System.Drawing.Point(0, 0);
            this.tbOut.Margin = new System.Windows.Forms.Padding(4);
            this.tbOut.MaxLength = 327670000;
            this.tbOut.Multiline = true;
            this.tbOut.Name = "tbOut";
            this.tbOut.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbOut.Size = new System.Drawing.Size(942, 337);
            this.tbOut.TabIndex = 2;
            this.tbOut.WordWrap = false;
            // 
            // panelError
            // 
            this.panelError.Controls.Add(this.tbError);
            this.panelError.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelError.Location = new System.Drawing.Point(291, 571);
            this.panelError.Margin = new System.Windows.Forms.Padding(4);
            this.panelError.Name = "panelError";
            this.panelError.Size = new System.Drawing.Size(942, 337);
            this.panelError.TabIndex = 2;
            // 
            // tbError
            // 
            this.tbError.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.tbError.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbError.Location = new System.Drawing.Point(0, 0);
            this.tbError.Margin = new System.Windows.Forms.Padding(4);
            this.tbError.MaxLength = 327670000;
            this.tbError.Multiline = true;
            this.tbError.Name = "tbError";
            this.tbError.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbError.Size = new System.Drawing.Size(942, 337);
            this.tbError.TabIndex = 2;
            this.tbError.WordWrap = false;
            // 
            // panel6
            // 
            this.panel6.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel6.Location = new System.Drawing.Point(223, 4);
            this.panel6.Margin = new System.Windows.Forms.Padding(4);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(68, 904);
            this.panel6.TabIndex = 1;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.cbProxyInFile);
            this.panel5.Controls.Add(this.cbAccessToken);
            this.panel5.Controls.Add(this.cbImap);
            this.panel5.Controls.Add(this.cbLoaiProxy);
            this.panel5.Controls.Add(this.btnStop);
            this.panel5.Controls.Add(this.btnStart);
            this.panel5.Controls.Add(this.nrudSoLuong);
            this.panel5.Controls.Add(this.label2);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel5.Location = new System.Drawing.Point(4, 4);
            this.panel5.Margin = new System.Windows.Forms.Padding(4);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(219, 904);
            this.panel5.TabIndex = 0;
            // 
            // cbProxyInFile
            // 
            this.cbProxyInFile.AutoSize = true;
            this.cbProxyInFile.Location = new System.Drawing.Point(12, 438);
            this.cbProxyInFile.Margin = new System.Windows.Forms.Padding(4);
            this.cbProxyInFile.Name = "cbProxyInFile";
            this.cbProxyInFile.Size = new System.Drawing.Size(181, 29);
            this.cbProxyInFile.TabIndex = 8;
            this.cbProxyInFile.Text = "User proxy in file";
            this.cbProxyInFile.UseVisualStyleBackColor = true;
            this.cbProxyInFile.CheckedChanged += new System.EventHandler(this.cbProxyInFile_CheckedChanged);
            // 
            // cbAccessToken
            // 
            this.cbAccessToken.AutoSize = true;
            this.cbAccessToken.Location = new System.Drawing.Point(12, 385);
            this.cbAccessToken.Margin = new System.Windows.Forms.Padding(4);
            this.cbAccessToken.Name = "cbAccessToken";
            this.cbAccessToken.Size = new System.Drawing.Size(199, 29);
            this.cbAccessToken.TabIndex = 7;
            this.cbAccessToken.Text = "Xuất access token";
            this.cbAccessToken.UseVisualStyleBackColor = true;
            // 
            // cbImap
            // 
            this.cbImap.AutoSize = true;
            this.cbImap.Location = new System.Drawing.Point(12, 335);
            this.cbImap.Margin = new System.Windows.Forms.Padding(4);
            this.cbImap.Name = "cbImap";
            this.cbImap.Size = new System.Drawing.Size(81, 29);
            this.cbImap.TabIndex = 6;
            this.cbImap.Text = "Imap";
            this.cbImap.UseVisualStyleBackColor = true;
            this.cbImap.CheckedChanged += new System.EventHandler(this.cbImap_CheckedChanged);
            // 
            // cbLoaiProxy
            // 
            this.cbLoaiProxy.AutoSize = true;
            this.cbLoaiProxy.Location = new System.Drawing.Point(12, 284);
            this.cbLoaiProxy.Margin = new System.Windows.Forms.Padding(4);
            this.cbLoaiProxy.Name = "cbLoaiProxy";
            this.cbLoaiProxy.Size = new System.Drawing.Size(125, 29);
            this.cbLoaiProxy.TabIndex = 5;
            this.cbLoaiProxy.Text = "Proxy http";
            this.cbLoaiProxy.UseVisualStyleBackColor = true;
            this.cbLoaiProxy.CheckedChanged += new System.EventHandler(this.cbLoaiProxy_CheckedChanged);
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(12, 95);
            this.btnStop.Margin = new System.Windows.Forms.Padding(4);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(155, 52);
            this.btnStop.TabIndex = 3;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(12, 14);
            this.btnStart.Margin = new System.Windows.Forms.Padding(4);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(155, 52);
            this.btnStart.TabIndex = 2;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // nrudSoLuong
            // 
            this.nrudSoLuong.Location = new System.Drawing.Point(12, 214);
            this.nrudSoLuong.Margin = new System.Windows.Forms.Padding(4);
            this.nrudSoLuong.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.nrudSoLuong.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nrudSoLuong.Name = "nrudSoLuong";
            this.nrudSoLuong.Size = new System.Drawing.Size(88, 29);
            this.nrudSoLuong.TabIndex = 1;
            this.nrudSoLuong.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 174);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 25);
            this.label2.TabIndex = 0;
            this.label2.Text = "Số luồng:";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.panel3);
            this.tabPage2.Controls.Add(this.panel2);
            this.tabPage2.Controls.Add(this.panel1);
            this.tabPage2.Location = new System.Drawing.Point(4, 33);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(4);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(4);
            this.tabPage2.Size = new System.Drawing.Size(1237, 912);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Setting";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.panelProxy);
            this.panel3.Controls.Add(this.panel4);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(248, 4);
            this.panel3.Margin = new System.Windows.Forms.Padding(4);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(985, 581);
            this.panel3.TabIndex = 2;
            // 
            // panelProxy
            // 
            this.panelProxy.Controls.Add(this.tbListProxy);
            this.panelProxy.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelProxy.Location = new System.Drawing.Point(128, 0);
            this.panelProxy.Margin = new System.Windows.Forms.Padding(4);
            this.panelProxy.Name = "panelProxy";
            this.panelProxy.Size = new System.Drawing.Size(857, 581);
            this.panelProxy.TabIndex = 2;
            // 
            // tbListProxy
            // 
            this.tbListProxy.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.tbListProxy.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbListProxy.Location = new System.Drawing.Point(0, 0);
            this.tbListProxy.Margin = new System.Windows.Forms.Padding(4);
            this.tbListProxy.Multiline = true;
            this.tbListProxy.Name = "tbListProxy";
            this.tbListProxy.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbListProxy.Size = new System.Drawing.Size(857, 581);
            this.tbListProxy.TabIndex = 0;
            this.tbListProxy.WordWrap = false;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.label1);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Margin = new System.Windows.Forms.Padding(4);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(128, 581);
            this.panel4.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 19);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "List proxy:";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.nrudTime);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(248, 585);
            this.panel2.Margin = new System.Windows.Forms.Padding(4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(985, 323);
            this.panel2.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 27);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(200, 25);
            this.label3.TabIndex = 3;
            this.label3.Text = "Time get again proxy:";
            // 
            // nrudTime
            // 
            this.nrudTime.Location = new System.Drawing.Point(215, 23);
            this.nrudTime.Margin = new System.Windows.Forms.Padding(4);
            this.nrudTime.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.nrudTime.Name = "nrudTime";
            this.nrudTime.Size = new System.Drawing.Size(88, 29);
            this.nrudTime.TabIndex = 2;
            this.nrudTime.ValueChanged += new System.EventHandler(this.nrudTime_ValueChanged);
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(4, 4);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(244, 904);
            this.panel1.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1245, 949);
            this.Controls.Add(this.tabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.MinimumSize = new System.Drawing.Size(719, 834);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Hotmail Get Oauth2 V2.0";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.panelMail.ResumeLayout(false);
            this.panelMail.PerformLayout();
            this.panelOut.ResumeLayout(false);
            this.panelOut.PerformLayout();
            this.panelError.ResumeLayout(false);
            this.panelError.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nrudSoLuong)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panelProxy.ResumeLayout(false);
            this.panelProxy.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nrudTime)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panelProxy;
        private System.Windows.Forms.PageSetupDialog pageSetupDialog1;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Panel panelError;
        private System.Windows.Forms.Panel panelMail;
        private System.Windows.Forms.Panel panelOut;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown nrudSoLuong;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.CheckBox cbLoaiProxy;
        private System.Windows.Forms.TextBox tbListProxy;
        private System.Windows.Forms.TextBox tbListMail;
        private System.Windows.Forms.TextBox tbOut;
        private System.Windows.Forms.TextBox tbError;
        private System.Windows.Forms.CheckBox cbImap;
        private System.Windows.Forms.CheckBox cbAccessToken;
        private System.Windows.Forms.CheckBox cbProxyInFile;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown nrudTime;
    }
}


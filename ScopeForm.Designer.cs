namespace NebScope
{
    partial class ScopeForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ScopeForm));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.btnSettings = new System.Windows.Forms.Button();
            this.chkCapture = new System.Windows.Forms.CheckBox();
            this.btnHelp = new System.Windows.Forms.Button();
            this.txtMsgs = new System.Windows.Forms.RichTextBox();
            this.potCh2Position = new NBagOfUis.Pot();
            this.potCh1Position = new NBagOfUis.Pot();
            this.potXPosition = new NBagOfUis.Pot();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.selTimebase = new System.Windows.Forms.ComboBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.selCh1VoltsPerDiv = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.selCh2VoltsPerDiv = new System.Windows.Forms.ComboBox();
            this.timerHousekeeping = new System.Windows.Forms.Timer(this.components);
            this.display = new NebScope.Display();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.btnSettings);
            this.splitContainer1.Panel1.Controls.Add(this.chkCapture);
            this.splitContainer1.Panel1.Controls.Add(this.btnHelp);
            this.splitContainer1.Panel1.Controls.Add(this.txtMsgs);
            this.splitContainer1.Panel1.Controls.Add(this.potCh2Position);
            this.splitContainer1.Panel1.Controls.Add(this.potCh1Position);
            this.splitContainer1.Panel1.Controls.Add(this.potXPosition);
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            this.splitContainer1.Panel1.Controls.Add(this.groupBox3);
            this.splitContainer1.Panel1.Controls.Add(this.groupBox2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.display);
            this.splitContainer1.Size = new System.Drawing.Size(1109, 742);
            this.splitContainer1.SplitterDistance = 257;
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 1;
            // 
            // btnSettings
            // 
            this.btnSettings.Image = global::NebScope.Properties.Resources.glyphicons_137_cogwheel;
            this.btnSettings.Location = new System.Drawing.Point(148, 19);
            this.btnSettings.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Size = new System.Drawing.Size(43, 49);
            this.btnSettings.TabIndex = 18;
            this.btnSettings.UseVisualStyleBackColor = true;
            this.btnSettings.Click += new System.EventHandler(this.UserSettings_Click);
            // 
            // chkCapture
            // 
            this.chkCapture.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkCapture.AutoSize = true;
            this.chkCapture.Image = global::NebScope.Properties.Resources.glyphicons_366_restart;
            this.chkCapture.Location = new System.Drawing.Point(17, 19);
            this.chkCapture.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chkCapture.Name = "chkCapture";
            this.chkCapture.Size = new System.Drawing.Size(31, 31);
            this.chkCapture.TabIndex = 17;
            this.chkCapture.UseVisualStyleBackColor = true;
            this.chkCapture.CheckedChanged += new System.EventHandler(this.ChkCapture_CheckedChanged);
            // 
            // btnHelp
            // 
            this.btnHelp.Image = global::NebScope.Properties.Resources.glyphicons_195_question_sign;
            this.btnHelp.Location = new System.Drawing.Point(199, 19);
            this.btnHelp.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Size = new System.Drawing.Size(43, 49);
            this.btnHelp.TabIndex = 15;
            this.btnHelp.UseVisualStyleBackColor = true;
            this.btnHelp.Click += new System.EventHandler(this.BtnHelp_Click);
            // 
            // txtMsgs
            // 
            this.txtMsgs.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.txtMsgs.BackColor = System.Drawing.SystemColors.Control;
            this.txtMsgs.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMsgs.Location = new System.Drawing.Point(17, 495);
            this.txtMsgs.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtMsgs.Name = "txtMsgs";
            this.txtMsgs.Size = new System.Drawing.Size(223, 232);
            this.txtMsgs.TabIndex = 13;
            this.txtMsgs.Text = "";
            this.txtMsgs.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.Msgs_MouseDoubleClick);
            // 
            // potCh2Position
            // 
            this.potCh2Position.DecPlaces = 2;
            this.potCh2Position.DrawColor = System.Drawing.Color.Black;
            this.potCh2Position.Label = "Pos";
            this.potCh2Position.Location = new System.Drawing.Point(155, 389);
            this.potCh2Position.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.potCh2Position.Maximum = 1D;
            this.potCh2Position.Minimum = -1D;
            this.potCh2Position.Name = "potCh2Position";
            this.potCh2Position.Size = new System.Drawing.Size(67, 78);
            this.potCh2Position.TabIndex = 6;
            this.potCh2Position.Taper = NBagOfUis.Taper.Linear;
            this.potCh2Position.Value = 0D;
            this.potCh2Position.ValueChanged += new System.EventHandler(this.Pot_ValueChanged);
            this.potCh2Position.DoubleClick += new System.EventHandler(this.Pot_DoubleClick);
            // 
            // potCh1Position
            // 
            this.potCh1Position.DecPlaces = 2;
            this.potCh1Position.DrawColor = System.Drawing.Color.Black;
            this.potCh1Position.Label = "Pos";
            this.potCh1Position.Location = new System.Drawing.Point(155, 260);
            this.potCh1Position.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.potCh1Position.Maximum = 1D;
            this.potCh1Position.Minimum = -1D;
            this.potCh1Position.Name = "potCh1Position";
            this.potCh1Position.Size = new System.Drawing.Size(67, 78);
            this.potCh1Position.TabIndex = 5;
            this.potCh1Position.Taper = NBagOfUis.Taper.Linear;
            this.potCh1Position.Value = 0D;
            this.potCh1Position.ValueChanged += new System.EventHandler(this.Pot_ValueChanged);
            this.potCh1Position.DoubleClick += new System.EventHandler(this.Pot_DoubleClick);
            // 
            // potXPosition
            // 
            this.potXPosition.DecPlaces = 2;
            this.potXPosition.DrawColor = System.Drawing.Color.Black;
            this.potXPosition.Label = "Pos";
            this.potXPosition.Location = new System.Drawing.Point(155, 131);
            this.potXPosition.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.potXPosition.Maximum = 1D;
            this.potXPosition.Minimum = -1D;
            this.potXPosition.Name = "potXPosition";
            this.potXPosition.Size = new System.Drawing.Size(67, 78);
            this.potXPosition.TabIndex = 1;
            this.potXPosition.Taper = NBagOfUis.Taper.Linear;
            this.potXPosition.Value = 0D;
            this.potXPosition.ValueChanged += new System.EventHandler(this.Pot_ValueChanged);
            this.potXPosition.DoubleClick += new System.EventHandler(this.Pot_DoubleClick);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.selTimebase);
            this.groupBox1.Location = new System.Drawing.Point(16, 112);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox1.Size = new System.Drawing.Size(225, 118);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "X";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(32, 72);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 20);
            this.label2.TabIndex = 15;
            this.label2.Text = "Sec/div";
            // 
            // selTimebase
            // 
            this.selTimebase.FormattingEnabled = true;
            this.selTimebase.Location = new System.Drawing.Point(29, 35);
            this.selTimebase.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.selTimebase.Name = "selTimebase";
            this.selTimebase.Size = new System.Drawing.Size(79, 28);
            this.selTimebase.TabIndex = 13;
            this.selTimebase.SelectedValueChanged += new System.EventHandler(this.Sel_SelectedValueChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.selCh1VoltsPerDiv);
            this.groupBox3.Location = new System.Drawing.Point(16, 239);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox3.Size = new System.Drawing.Size(225, 118);
            this.groupBox3.TabIndex = 12;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Ch 1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(32, 74);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 20);
            this.label1.TabIndex = 16;
            this.label1.Text = "Volts/div";
            // 
            // selCh1VoltsPerDiv
            // 
            this.selCh1VoltsPerDiv.FormattingEnabled = true;
            this.selCh1VoltsPerDiv.Location = new System.Drawing.Point(29, 32);
            this.selCh1VoltsPerDiv.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.selCh1VoltsPerDiv.Name = "selCh1VoltsPerDiv";
            this.selCh1VoltsPerDiv.Size = new System.Drawing.Size(79, 28);
            this.selCh1VoltsPerDiv.TabIndex = 14;
            this.selCh1VoltsPerDiv.SelectedValueChanged += new System.EventHandler(this.Sel_SelectedValueChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.selCh2VoltsPerDiv);
            this.groupBox2.Location = new System.Drawing.Point(16, 368);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox2.Size = new System.Drawing.Size(225, 118);
            this.groupBox2.TabIndex = 12;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Ch 2";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(32, 59);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 20);
            this.label3.TabIndex = 17;
            this.label3.Text = "Volts/div";
            // 
            // selCh2VoltsPerDiv
            // 
            this.selCh2VoltsPerDiv.FormattingEnabled = true;
            this.selCh2VoltsPerDiv.Location = new System.Drawing.Point(29, 21);
            this.selCh2VoltsPerDiv.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.selCh2VoltsPerDiv.Name = "selCh2VoltsPerDiv";
            this.selCh2VoltsPerDiv.Size = new System.Drawing.Size(79, 28);
            this.selCh2VoltsPerDiv.TabIndex = 15;
            this.selCh2VoltsPerDiv.SelectedValueChanged += new System.EventHandler(this.Sel_SelectedValueChanged);
            // 
            // timerHousekeeping
            // 
            this.timerHousekeeping.Tick += new System.EventHandler(this.TimerHousekeeping_Tick);
            // 
            // display
            // 
            this.display.Dock = System.Windows.Forms.DockStyle.Fill;
            this.display.Location = new System.Drawing.Point(0, 0);
            this.display.Name = "display";
            this.display.Size = new System.Drawing.Size(847, 742);
            this.display.TabIndex = 0;
            // 
            // ScopeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1109, 742);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "ScopeForm";
            this.Text = "NebScope";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ScopeForm_FormClosing);
            this.Load += new System.EventHandler(this.ScopeForm_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.SplitContainer splitContainer1;
        private NBagOfUis.Pot potXPosition;
        private NBagOfUis.Pot potCh2Position;
        private NBagOfUis.Pot potCh1Position;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox selTimebase;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox selCh1VoltsPerDiv;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox selCh2VoltsPerDiv;
        private System.Windows.Forms.RichTextBox txtMsgs;
        private System.Windows.Forms.Timer timerHousekeeping;
        private System.Windows.Forms.Button btnHelp;
        private System.Windows.Forms.CheckBox chkCapture;
        private System.Windows.Forms.Button btnSettings;
        private Display display;
    }
}


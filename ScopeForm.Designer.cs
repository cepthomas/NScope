namespace Ephemera.NScope
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
            this.display = new NScope.Display();
            this.timerHousekeeping = new System.Windows.Forms.Timer(this.components);
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.btnSettings = new System.Windows.Forms.Button();
            this.chkCapture = new System.Windows.Forms.CheckBox();
            this.btnHelp = new System.Windows.Forms.Button();
            this.txtMsgs = new System.Windows.Forms.RichTextBox();
            this.sldCh2Position = new NBagOfUis.Slider();
            this.sldCh1Position = new NBagOfUis.Slider();
            this.sldXPosition = new NBagOfUis.Slider();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.selTimebase = new System.Windows.Forms.ComboBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.selCh1VoltsPerDiv = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.selCh2VoltsPerDiv = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // display
            // 
            this.display.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.display.BackColor = System.Drawing.Color.Black;
            this.display.Location = new System.Drawing.Point(239, 12);
            this.display.Name = "display";
            this.display.Size = new System.Drawing.Size(877, 656);
            this.display.TabIndex = 0;
            // 
            // timerHousekeeping
            // 
            this.timerHousekeeping.Tick += new System.EventHandler(this.TimerHousekeeping_Tick);
            // 
            // btnSettings
            // 
            this.btnSettings.BackColor = System.Drawing.SystemColors.Control;
            this.btnSettings.FlatAppearance.BorderSize = 0;
            this.btnSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSettings.Image = global::Ephemera.NScope.Properties.Resources.glyphicons_137_cogwheel;
            this.btnSettings.Location = new System.Drawing.Point(128, 4);
            this.btnSettings.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Size = new System.Drawing.Size(48, 48);
            this.btnSettings.TabIndex = 28;
            this.toolTip1.SetToolTip(this.btnSettings, "User settings");
            this.btnSettings.UseVisualStyleBackColor = false;
            this.btnSettings.Click += new System.EventHandler(this.UserSettings_Click);
            // 
            // chkCapture
            // 
            this.chkCapture.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkCapture.BackColor = System.Drawing.SystemColors.Control;
            this.chkCapture.FlatAppearance.BorderSize = 0;
            this.chkCapture.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkCapture.Image = global::Ephemera.NScope.Properties.Resources.glyphicons_366_restart;
            this.chkCapture.Location = new System.Drawing.Point(8, 4);
            this.chkCapture.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chkCapture.Name = "chkCapture";
            this.chkCapture.Size = new System.Drawing.Size(48, 48);
            this.chkCapture.TabIndex = 27;
            this.toolTip1.SetToolTip(this.chkCapture, "Capture enable");
            this.chkCapture.UseVisualStyleBackColor = false;
            this.chkCapture.CheckedChanged += new System.EventHandler(this.ChkCapture_CheckedChanged);
            // 
            // btnHelp
            // 
            this.btnHelp.BackColor = System.Drawing.SystemColors.Control;
            this.btnHelp.FlatAppearance.BorderSize = 0;
            this.btnHelp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHelp.Image = global::Ephemera.NScope.Properties.Resources.glyphicons_195_question_sign;
            this.btnHelp.Location = new System.Drawing.Point(184, 4);
            this.btnHelp.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Size = new System.Drawing.Size(48, 48);
            this.btnHelp.TabIndex = 26;
            this.toolTip1.SetToolTip(this.btnHelp, "Help");
            this.btnHelp.UseVisualStyleBackColor = false;
            this.btnHelp.Click += new System.EventHandler(this.BtnHelp_Click);
            // 
            // txtMsgs
            // 
            this.txtMsgs.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.txtMsgs.BackColor = System.Drawing.Color.Lavender;
            this.txtMsgs.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtMsgs.Location = new System.Drawing.Point(8, 441);
            this.txtMsgs.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtMsgs.Name = "txtMsgs";
            this.txtMsgs.Size = new System.Drawing.Size(224, 227);
            this.txtMsgs.TabIndex = 25;
            this.txtMsgs.Text = "";
            // 
            // sldCh2Position
            // 
            this.sldCh2Position.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.sldCh2Position.DrawColor = System.Drawing.Color.Black;
            this.sldCh2Position.Label = "Pos";
            this.sldCh2Position.Location = new System.Drawing.Point(146, 335);
            this.sldCh2Position.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.sldCh2Position.Maximum = 1D;
            this.sldCh2Position.Minimum = -1D;
            this.sldCh2Position.Name = "sldCh2Position";
            this.sldCh2Position.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.sldCh2Position.Resolution = 0.01D;
            this.sldCh2Position.Size = new System.Drawing.Size(50, 78);
            this.sldCh2Position.TabIndex = 21;
            this.sldCh2Position.Value = 0D;
            this.sldCh2Position.ValueChanged += new System.EventHandler(this.Pot_ValueChanged);
            this.sldCh2Position.DoubleClick += new System.EventHandler(this.Pot_DoubleClick);
            // 
            // sldCh1Position
            // 
            this.sldCh1Position.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.sldCh1Position.DrawColor = System.Drawing.Color.Black;
            this.sldCh1Position.Label = "Pos";
            this.sldCh1Position.Location = new System.Drawing.Point(146, 206);
            this.sldCh1Position.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.sldCh1Position.Maximum = 1D;
            this.sldCh1Position.Minimum = -1D;
            this.sldCh1Position.Name = "sldCh1Position";
            this.sldCh1Position.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.sldCh1Position.Resolution = 0.01D;
            this.sldCh1Position.Size = new System.Drawing.Size(50, 78);
            this.sldCh1Position.TabIndex = 20;
            this.sldCh1Position.Value = 0D;
            this.sldCh1Position.ValueChanged += new System.EventHandler(this.Pot_ValueChanged);
            this.sldCh1Position.DoubleClick += new System.EventHandler(this.Pot_DoubleClick);
            // 
            // sldXPosition
            // 
            this.sldXPosition.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.sldXPosition.DrawColor = System.Drawing.Color.Black;
            this.sldXPosition.Label = "Pos";
            this.sldXPosition.Location = new System.Drawing.Point(146, 93);
            this.sldXPosition.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.sldXPosition.Maximum = 1D;
            this.sldXPosition.Minimum = -1D;
            this.sldXPosition.Name = "sldXPosition";
            this.sldXPosition.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.sldXPosition.Resolution = 0.01D;
            this.sldXPosition.Size = new System.Drawing.Size(67, 45);
            this.sldXPosition.TabIndex = 19;
            this.sldXPosition.Value = 0D;
            this.sldXPosition.ValueChanged += new System.EventHandler(this.Pot_ValueChanged);
            this.sldXPosition.DoubleClick += new System.EventHandler(this.Pot_DoubleClick);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.selTimebase);
            this.groupBox1.Location = new System.Drawing.Point(7, 58);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox1.Size = new System.Drawing.Size(225, 118);
            this.groupBox1.TabIndex = 22;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "X";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(33, 74);
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
            this.selTimebase.SelectionChangeCommitted += new System.EventHandler(this.Sel_SelectedValueChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.selCh1VoltsPerDiv);
            this.groupBox3.Location = new System.Drawing.Point(7, 185);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox3.Size = new System.Drawing.Size(225, 118);
            this.groupBox3.TabIndex = 23;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Ch 1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(33, 76);
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
            this.selCh1VoltsPerDiv.SelectionChangeCommitted += new System.EventHandler(this.Sel_SelectedValueChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.selCh2VoltsPerDiv);
            this.groupBox2.Location = new System.Drawing.Point(7, 314);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox2.Size = new System.Drawing.Size(225, 118);
            this.groupBox2.TabIndex = 24;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Ch 2";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(33, 61);
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
            this.selCh2VoltsPerDiv.SelectionChangeCommitted += new System.EventHandler(this.Sel_SelectedValueChanged);
            // 
            // ScopeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1128, 680);
            this.Controls.Add(this.display);
            this.Controls.Add(this.btnSettings);
            this.Controls.Add(this.chkCapture);
            this.Controls.Add(this.btnHelp);
            this.Controls.Add(this.txtMsgs);
            this.Controls.Add(this.sldCh2Position);
            this.Controls.Add(this.sldCh1Position);
            this.Controls.Add(this.sldXPosition);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "ScopeForm";
            this.Text = "NScope";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Timer timerHousekeeping;
        private Display display;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button btnSettings;
        private System.Windows.Forms.CheckBox chkCapture;
        private System.Windows.Forms.Button btnHelp;
        private System.Windows.Forms.RichTextBox txtMsgs;
        private NBagOfUis.Slider sldCh2Position;
        private NBagOfUis.Slider sldCh1Position;
        private NBagOfUis.Slider sldXPosition;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox selTimebase;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox selCh1VoltsPerDiv;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox selCh2VoltsPerDiv;
    }
}


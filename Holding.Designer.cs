namespace NebScope
{
    partial class Holding
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
            this.label3 = new System.Windows.Forms.Label();
            this.selTrigSlope = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.selTrigMode = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.selTrigChannel = new System.Windows.Forms.ComboBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnTrig50Pct = new System.Windows.Forms.Button();
            this.btnTrig0 = new System.Windows.Forms.Button();
            this.potTrigLevel = new NebScope.Pot();
            this.potCh2VoltsPerDiv = new NebScope.Pot();
            this.potCh1VoltsPerDiv = new NebScope.Pot();
            this.potTimebase = new NebScope.Pot();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(327, 222);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 13);
            this.label3.TabIndex = 24;
            this.label3.Text = "Slope";
            // 
            // selTrigSlope
            // 
            this.selTrigSlope.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.selTrigSlope.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.selTrigSlope.FormattingEnabled = true;
            this.selTrigSlope.Location = new System.Drawing.Point(386, 219);
            this.selTrigSlope.Name = "selTrigSlope";
            this.selTrigSlope.Size = new System.Drawing.Size(85, 21);
            this.selTrigSlope.TabIndex = 23;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(327, 192);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 13);
            this.label2.TabIndex = 22;
            this.label2.Text = "Mode";
            // 
            // selTrigMode
            // 
            this.selTrigMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.selTrigMode.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.selTrigMode.FormattingEnabled = true;
            this.selTrigMode.Items.AddRange(new object[] {
            "yyy",
            "uuuu",
            "nnnn",
            "rrrr"});
            this.selTrigMode.Location = new System.Drawing.Point(386, 189);
            this.selTrigMode.Name = "selTrigMode";
            this.selTrigMode.Size = new System.Drawing.Size(85, 21);
            this.selTrigMode.TabIndex = 21;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(326, 162);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 19;
            this.label1.Text = "Channel";
            // 
            // selTrigChannel
            // 
            this.selTrigChannel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.selTrigChannel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.selTrigChannel.FormattingEnabled = true;
            this.selTrigChannel.Items.AddRange(new object[] {
            "yyy",
            "uuuu",
            "nnnn",
            "rrrr"});
            this.selTrigChannel.Location = new System.Drawing.Point(385, 159);
            this.selTrigChannel.Name = "selTrigChannel";
            this.selTrigChannel.Size = new System.Drawing.Size(85, 21);
            this.selTrigChannel.TabIndex = 18;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btnTrig50Pct);
            this.groupBox4.Controls.Add(this.btnTrig0);
            this.groupBox4.Controls.Add(this.potTrigLevel);
            this.groupBox4.Location = new System.Drawing.Point(316, 144);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(169, 162);
            this.groupBox4.TabIndex = 20;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Trigger";
            // 
            // btnTrig50Pct
            // 
            this.btnTrig50Pct.Location = new System.Drawing.Point(87, 131);
            this.btnTrig50Pct.Name = "btnTrig50Pct";
            this.btnTrig50Pct.Size = new System.Drawing.Size(35, 23);
            this.btnTrig50Pct.TabIndex = 20;
            this.btnTrig50Pct.Text = "50%";
            this.btnTrig50Pct.UseVisualStyleBackColor = true;
            // 
            // btnTrig0
            // 
            this.btnTrig0.Location = new System.Drawing.Point(87, 106);
            this.btnTrig0.Name = "btnTrig0";
            this.btnTrig0.Size = new System.Drawing.Size(35, 23);
            this.btnTrig0.TabIndex = 19;
            this.btnTrig0.Text = "0";
            this.btnTrig0.UseVisualStyleBackColor = true;
            // 
            // potTrigLevel
            // 
            this.potTrigLevel.ControlColor = System.Drawing.Color.Black;
            this.potTrigLevel.DecPlaces = 1;
            this.potTrigLevel.Label = "Volts";
            this.potTrigLevel.Location = new System.Drawing.Point(14, 106);
            this.potTrigLevel.Maximum = 1D;
            this.potTrigLevel.Minimum = 0D;
            this.potTrigLevel.Name = "potTrigLevel";
            this.potTrigLevel.Size = new System.Drawing.Size(50, 50);
            this.potTrigLevel.TabIndex = 18;
            this.potTrigLevel.Taper = NebScope.Taper.Linear;
            this.potTrigLevel.Value = 0.5D;
            // 
            // potCh2VoltsPerDiv
            // 
            this.potCh2VoltsPerDiv.ControlColor = System.Drawing.Color.Black;
            this.potCh2VoltsPerDiv.DecPlaces = 3;
            this.potCh2VoltsPerDiv.Label = "Volts/div";
            this.potCh2VoltsPerDiv.Location = new System.Drawing.Point(587, 248);
            this.potCh2VoltsPerDiv.Maximum = 5D;
            this.potCh2VoltsPerDiv.Minimum = 0.01D;
            this.potCh2VoltsPerDiv.Name = "potCh2VoltsPerDiv";
            this.potCh2VoltsPerDiv.Size = new System.Drawing.Size(50, 50);
            this.potCh2VoltsPerDiv.TabIndex = 27;
            this.potCh2VoltsPerDiv.Taper = NebScope.Taper.Log;
            this.potCh2VoltsPerDiv.Value = 0.01D;
            // 
            // potCh1VoltsPerDiv
            // 
            this.potCh1VoltsPerDiv.ControlColor = System.Drawing.Color.Black;
            this.potCh1VoltsPerDiv.DecPlaces = 3;
            this.potCh1VoltsPerDiv.Label = "Volts/div";
            this.potCh1VoltsPerDiv.Location = new System.Drawing.Point(587, 192);
            this.potCh1VoltsPerDiv.Maximum = 5D;
            this.potCh1VoltsPerDiv.Minimum = 0.01D;
            this.potCh1VoltsPerDiv.Name = "potCh1VoltsPerDiv";
            this.potCh1VoltsPerDiv.Size = new System.Drawing.Size(50, 50);
            this.potCh1VoltsPerDiv.TabIndex = 26;
            this.potCh1VoltsPerDiv.Taper = NebScope.Taper.Log;
            this.potCh1VoltsPerDiv.Value = 1D;
            // 
            // potTimebase
            // 
            this.potTimebase.ControlColor = System.Drawing.Color.Black;
            this.potTimebase.DecPlaces = 3;
            this.potTimebase.Label = "Sec/div";
            this.potTimebase.Location = new System.Drawing.Point(587, 125);
            this.potTimebase.Maximum = 5D;
            this.potTimebase.Minimum = 0.01D;
            this.potTimebase.Name = "potTimebase";
            this.potTimebase.Size = new System.Drawing.Size(50, 50);
            this.potTimebase.TabIndex = 25;
            this.potTimebase.Taper = NebScope.Taper.Log;
            this.potTimebase.Value = 1D;
            // 
            // Holding
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.potCh2VoltsPerDiv);
            this.Controls.Add(this.potCh1VoltsPerDiv);
            this.Controls.Add(this.potTimebase);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.selTrigSlope);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.selTrigMode);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.selTrigChannel);
            this.Controls.Add(this.groupBox4);
            this.Name = "Holding";
            this.Text = "Holding";
            this.Load += new System.EventHandler(this.Holding_Load);
            this.groupBox4.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox selTrigSlope;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox selTrigMode;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox selTrigChannel;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btnTrig50Pct;
        private System.Windows.Forms.Button btnTrig0;
        private Pot potTrigLevel;
        private Pot potCh2VoltsPerDiv;
        private Pot potCh1VoltsPerDiv;
        private Pot potTimebase;
    }
}
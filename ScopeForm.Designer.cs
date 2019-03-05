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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ScopeForm));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.label3 = new System.Windows.Forms.Label();
            this.selTrigSlope = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.selTrigMode = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.selTrigChannel = new System.Windows.Forms.ComboBox();
            this.potCh2Position = new NebScope.Pot();
            this.potCh1Position = new NebScope.Pot();
            this.potCh2VoltsPerDiv = new NebScope.Pot();
            this.potCh1VoltsPerDiv = new NebScope.Pot();
            this.selTimebase = new System.Windows.Forms.ComboBox();
            this.potXPosition = new NebScope.Pot();
            this.btnTest = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnTrig50Pct = new System.Windows.Forms.Button();
            this.btnTrig0 = new System.Windows.Forms.Button();
            this.potTrigLevel = new NebScope.Pot();
            this.skControl = new SkiaSharp.Views.Desktop.SKControl();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.label3);
            this.splitContainer1.Panel1.Controls.Add(this.selTrigSlope);
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            this.splitContainer1.Panel1.Controls.Add(this.selTrigMode);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.selTrigChannel);
            this.splitContainer1.Panel1.Controls.Add(this.potCh2Position);
            this.splitContainer1.Panel1.Controls.Add(this.potCh1Position);
            this.splitContainer1.Panel1.Controls.Add(this.potCh2VoltsPerDiv);
            this.splitContainer1.Panel1.Controls.Add(this.potCh1VoltsPerDiv);
            this.splitContainer1.Panel1.Controls.Add(this.selTimebase);
            this.splitContainer1.Panel1.Controls.Add(this.potXPosition);
            this.splitContainer1.Panel1.Controls.Add(this.btnTest);
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            this.splitContainer1.Panel1.Controls.Add(this.groupBox3);
            this.splitContainer1.Panel1.Controls.Add(this.groupBox2);
            this.splitContainer1.Panel1.Controls.Add(this.groupBox4);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.skControl);
            this.splitContainer1.Size = new System.Drawing.Size(832, 483);
            this.splitContainer1.SplitterDistance = 195;
            this.splitContainer1.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(23, 336);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 13);
            this.label3.TabIndex = 17;
            this.label3.Text = "Slope";
            // 
            // selTrigSlope
            // 
            this.selTrigSlope.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.selTrigSlope.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.selTrigSlope.FormattingEnabled = true;
            this.selTrigSlope.Location = new System.Drawing.Point(82, 333);
            this.selTrigSlope.Name = "selTrigSlope";
            this.selTrigSlope.Size = new System.Drawing.Size(85, 21);
            this.selTrigSlope.TabIndex = 16;
            this.selTrigSlope.SelectedValueChanged += new System.EventHandler(this.Sel_SelectedValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(23, 306);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 13);
            this.label2.TabIndex = 15;
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
            this.selTrigMode.Location = new System.Drawing.Point(82, 303);
            this.selTrigMode.Name = "selTrigMode";
            this.selTrigMode.Size = new System.Drawing.Size(85, 21);
            this.selTrigMode.TabIndex = 14;
            this.selTrigMode.SelectedValueChanged += new System.EventHandler(this.Sel_SelectedValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 276);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 10;
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
            this.selTrigChannel.Location = new System.Drawing.Point(81, 273);
            this.selTrigChannel.Name = "selTrigChannel";
            this.selTrigChannel.Size = new System.Drawing.Size(85, 21);
            this.selTrigChannel.TabIndex = 9;
            this.selTrigChannel.SelectedValueChanged += new System.EventHandler(this.Sel_SelectedValueChanged);
            // 
            // potCh2Position
            // 
            this.potCh2Position.ControlColor = System.Drawing.Color.Black;
            this.potCh2Position.DecPlaces = 1;
            this.potCh2Position.Label = "Pos";
            this.potCh2Position.Location = new System.Drawing.Point(116, 190);
            this.potCh2Position.Maximum = 1D;
            this.potCh2Position.Minimum = 0D;
            this.potCh2Position.Name = "potCh2Position";
            this.potCh2Position.Size = new System.Drawing.Size(50, 50);
            this.potCh2Position.TabIndex = 6;
            this.potCh2Position.Value = 0.5D;
            this.potCh2Position.ValueChanged += new System.EventHandler(this.Pot_ValueChanged);
            // 
            // potCh1Position
            // 
            this.potCh1Position.ControlColor = System.Drawing.Color.Black;
            this.potCh1Position.DecPlaces = 1;
            this.potCh1Position.Label = "Pos";
            this.potCh1Position.Location = new System.Drawing.Point(116, 106);
            this.potCh1Position.Maximum = 1D;
            this.potCh1Position.Minimum = 0D;
            this.potCh1Position.Name = "potCh1Position";
            this.potCh1Position.Size = new System.Drawing.Size(50, 50);
            this.potCh1Position.TabIndex = 5;
            this.potCh1Position.Value = 0.5D;
            this.potCh1Position.ValueChanged += new System.EventHandler(this.Pot_ValueChanged);
            // 
            // potCh2VoltsPerDiv
            // 
            this.potCh2VoltsPerDiv.ControlColor = System.Drawing.Color.Black;
            this.potCh2VoltsPerDiv.DecPlaces = 1;
            this.potCh2VoltsPerDiv.Label = "Volts/div";
            this.potCh2VoltsPerDiv.Location = new System.Drawing.Point(22, 190);
            this.potCh2VoltsPerDiv.Maximum = 1D;
            this.potCh2VoltsPerDiv.Minimum = 0D;
            this.potCh2VoltsPerDiv.Name = "potCh2VoltsPerDiv";
            this.potCh2VoltsPerDiv.Size = new System.Drawing.Size(50, 50);
            this.potCh2VoltsPerDiv.TabIndex = 4;
            this.potCh2VoltsPerDiv.Value = 0.5D;
            this.potCh2VoltsPerDiv.ValueChanged += new System.EventHandler(this.Pot_ValueChanged);
            // 
            // potCh1VoltsPerDiv
            // 
            this.potCh1VoltsPerDiv.ControlColor = System.Drawing.Color.Black;
            this.potCh1VoltsPerDiv.DecPlaces = 1;
            this.potCh1VoltsPerDiv.Label = "Volts/div";
            this.potCh1VoltsPerDiv.Location = new System.Drawing.Point(22, 106);
            this.potCh1VoltsPerDiv.Maximum = 1D;
            this.potCh1VoltsPerDiv.Minimum = 0D;
            this.potCh1VoltsPerDiv.Name = "potCh1VoltsPerDiv";
            this.potCh1VoltsPerDiv.Size = new System.Drawing.Size(50, 50);
            this.potCh1VoltsPerDiv.TabIndex = 3;
            this.potCh1VoltsPerDiv.Value = 0.5D;
            this.potCh1VoltsPerDiv.ValueChanged += new System.EventHandler(this.Pot_ValueChanged);
            // 
            // selTimebase
            // 
            this.selTimebase.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.selTimebase.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.selTimebase.FormattingEnabled = true;
            this.selTimebase.Location = new System.Drawing.Point(22, 22);
            this.selTimebase.Name = "selTimebase";
            this.selTimebase.Size = new System.Drawing.Size(85, 21);
            this.selTimebase.TabIndex = 2;
            this.selTimebase.SelectedValueChanged += new System.EventHandler(this.Sel_SelectedValueChanged);
            // 
            // potXPosition
            // 
            this.potXPosition.ControlColor = System.Drawing.Color.Black;
            this.potXPosition.DecPlaces = 1;
            this.potXPosition.Label = "Pos";
            this.potXPosition.Location = new System.Drawing.Point(116, 22);
            this.potXPosition.Maximum = 1D;
            this.potXPosition.Minimum = 0D;
            this.potXPosition.Name = "potXPosition";
            this.potXPosition.Size = new System.Drawing.Size(50, 50);
            this.potXPosition.TabIndex = 1;
            this.potXPosition.Value = 0.5D;
            this.potXPosition.ValueChanged += new System.EventHandler(this.Pot_ValueChanged);
            // 
            // btnTest
            // 
            this.btnTest.Location = new System.Drawing.Point(25, 448);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(75, 23);
            this.btnTest.TabIndex = 0;
            this.btnTest.Text = "Test";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Location = new System.Drawing.Point(12, 10);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(169, 76);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "X";
            // 
            // groupBox3
            // 
            this.groupBox3.Location = new System.Drawing.Point(12, 92);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(169, 76);
            this.groupBox3.TabIndex = 12;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Ch 1";
            // 
            // groupBox2
            // 
            this.groupBox2.Location = new System.Drawing.Point(12, 176);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(169, 76);
            this.groupBox2.TabIndex = 12;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Ch 2";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btnTrig50Pct);
            this.groupBox4.Controls.Add(this.btnTrig0);
            this.groupBox4.Controls.Add(this.potTrigLevel);
            this.groupBox4.Location = new System.Drawing.Point(12, 258);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(169, 162);
            this.groupBox4.TabIndex = 13;
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
            this.btnTrig50Pct.Click += new System.EventHandler(this.BtnTrig_Click);
            // 
            // btnTrig0
            // 
            this.btnTrig0.Location = new System.Drawing.Point(87, 106);
            this.btnTrig0.Name = "btnTrig0";
            this.btnTrig0.Size = new System.Drawing.Size(35, 23);
            this.btnTrig0.TabIndex = 19;
            this.btnTrig0.Text = "0";
            this.btnTrig0.UseVisualStyleBackColor = true;
            this.btnTrig0.Click += new System.EventHandler(this.BtnTrig_Click);
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
            this.potTrigLevel.Value = 0.5D;
            this.potTrigLevel.ValueChanged += new System.EventHandler(this.Pot_ValueChanged);
            // 
            // skControl
            // 
            this.skControl.BackColor = System.Drawing.Color.Moccasin;
            this.skControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.skControl.Location = new System.Drawing.Point(0, 0);
            this.skControl.Name = "skControl";
            this.skControl.Size = new System.Drawing.Size(633, 483);
            this.skControl.TabIndex = 0;
            this.skControl.Text = "skControl";
            this.skControl.PaintSurface += new System.EventHandler<SkiaSharp.Views.Desktop.SKPaintSurfaceEventArgs>(this.SkControl_PaintSurface);
            this.skControl.Resize += new System.EventHandler(this.SkControl_Resize);
            // 
            // ScopeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(832, 483);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ScopeForm";
            this.Text = "NebScope";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ScopeForm_FormClosing);
            this.Load += new System.EventHandler(this.ScopeForm_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.ScopeForm_Paint);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.SplitContainer splitContainer1;
        private SkiaSharp.Views.Desktop.SKControl skControl;
        private System.Windows.Forms.Button btnTest;
        private Pot potXPosition;
        private Pot potCh2Position;
        private Pot potCh1Position;
        private Pot potCh2VoltsPerDiv;
        private Pot potCh1VoltsPerDiv;
        private Pot potTrigLevel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox selTrigChannel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox selTrigSlope;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox selTrigMode;
        private System.Windows.Forms.Button btnTrig50Pct;
        private System.Windows.Forms.Button btnTrig0;
        private System.Windows.Forms.ComboBox selTimebase;
    }
}


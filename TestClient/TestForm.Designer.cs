namespace TestClient
{
    partial class TestForm
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
            this.chkRun = new System.Windows.Forms.CheckBox();
            this.txtLog = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // chkRun
            // 
            this.chkRun.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkRun.AutoSize = true;
            this.chkRun.Location = new System.Drawing.Point(12, 12);
            this.chkRun.Name = "chkRun";
            this.chkRun.Size = new System.Drawing.Size(37, 23);
            this.chkRun.TabIndex = 1;
            this.chkRun.Text = "Run";
            this.chkRun.UseVisualStyleBackColor = true;
            this.chkRun.CheckedChanged += new System.EventHandler(this.chkRun_CheckedChanged);
            // 
            // txtLog
            // 
            this.txtLog.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtLog.Location = new System.Drawing.Point(13, 101);
            this.txtLog.Name = "txtLog";
            this.txtLog.Size = new System.Drawing.Size(673, 267);
            this.txtLog.TabIndex = 2;
            this.txtLog.Text = "";
            // 
            // TestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(698, 380);
            this.Controls.Add(this.txtLog);
            this.Controls.Add(this.chkRun);
            this.Name = "TestForm";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.TestForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.CheckBox chkRun;
        private System.Windows.Forms.RichTextBox txtLog;
    }
}


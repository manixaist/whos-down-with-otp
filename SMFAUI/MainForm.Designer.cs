namespace SMFAUI
{
    partial class MainForm
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
            this.textBoxOtp = new System.Windows.Forms.TextBox();
            this.progressOtp = new System.Windows.Forms.ProgressBar();
            this.buttonCopyOtp = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBoxOtp
            // 
            this.textBoxOtp.Font = new System.Drawing.Font("Consolas", 72F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxOtp.Location = new System.Drawing.Point(12, 12);
            this.textBoxOtp.Name = "textBoxOtp";
            this.textBoxOtp.ReadOnly = true;
            this.textBoxOtp.Size = new System.Drawing.Size(373, 120);
            this.textBoxOtp.TabIndex = 0;
            // 
            // progressOtp
            // 
            this.progressOtp.Location = new System.Drawing.Point(12, 138);
            this.progressOtp.Name = "progressOtp";
            this.progressOtp.Size = new System.Drawing.Size(373, 29);
            this.progressOtp.Step = 1;
            this.progressOtp.TabIndex = 1;
            // 
            // buttonCopyOtp
            // 
            this.buttonCopyOtp.Location = new System.Drawing.Point(12, 173);
            this.buttonCopyOtp.Name = "buttonCopyOtp";
            this.buttonCopyOtp.Size = new System.Drawing.Size(373, 43);
            this.buttonCopyOtp.TabIndex = 2;
            this.buttonCopyOtp.Text = "&Copy To Clipboard";
            this.buttonCopyOtp.UseVisualStyleBackColor = true;
            this.buttonCopyOtp.Click += new System.EventHandler(this.buttonCopyOtp_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(398, 227);
            this.Controls.Add(this.buttonCopyOtp);
            this.Controls.Add(this.progressOtp);
            this.Controls.Add(this.textBoxOtp);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxOtp;
        private System.Windows.Forms.ProgressBar progressOtp;
        private System.Windows.Forms.Button buttonCopyOtp;
    }
}


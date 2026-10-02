namespace Tm.English.CodesApp
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tbPrivateKey = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnShowQRCode = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.udQuantity = new System.Windows.Forms.NumericUpDown();
            this.btnGenerateReport = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.btnRefreshPrivate = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udQuantity)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnRefreshPrivate);
            this.groupBox1.Controls.Add(this.tbPrivateKey);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btnShowQRCode);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(321, 89);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Private Area";
            // 
            // tbPrivateKey
            // 
            this.tbPrivateKey.Location = new System.Drawing.Point(75, 17);
            this.tbPrivateKey.Name = "tbPrivateKey";
            this.tbPrivateKey.Size = new System.Drawing.Size(240, 20);
            this.tbPrivateKey.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Secret Key:";
            // 
            // btnShowQRCode
            // 
            this.btnShowQRCode.Location = new System.Drawing.Point(219, 43);
            this.btnShowQRCode.Name = "btnShowQRCode";
            this.btnShowQRCode.Size = new System.Drawing.Size(96, 23);
            this.btnShowQRCode.TabIndex = 0;
            this.btnShowQRCode.Text = "Show QR Code";
            this.btnShowQRCode.UseVisualStyleBackColor = true;
            this.btnShowQRCode.Click += new System.EventHandler(this.btnShowQRCode_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox2.Controls.Add(this.udQuantity);
            this.groupBox2.Controls.Add(this.btnGenerateReport);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Location = new System.Drawing.Point(13, 108);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(320, 165);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "QR Codes";
            // 
            // udQuantity
            // 
            this.udQuantity.Location = new System.Drawing.Point(108, 20);
            this.udQuantity.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.udQuantity.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.udQuantity.Name = "udQuantity";
            this.udQuantity.Size = new System.Drawing.Size(120, 20);
            this.udQuantity.TabIndex = 2;
            this.udQuantity.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // btnGenerateReport
            // 
            this.btnGenerateReport.Location = new System.Drawing.Point(234, 19);
            this.btnGenerateReport.Name = "btnGenerateReport";
            this.btnGenerateReport.Size = new System.Drawing.Size(80, 23);
            this.btnGenerateReport.TabIndex = 3;
            this.btnGenerateReport.Text = "Generate Report";
            this.btnGenerateReport.UseVisualStyleBackColor = true;
            this.btnGenerateReport.Click += new System.EventHandler(this.btnGenerateReport_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "QR Codes quantity";
            // 
            // btnRefreshPrivate
            // 
            this.btnRefreshPrivate.Location = new System.Drawing.Point(141, 43);
            this.btnRefreshPrivate.Name = "btnRefreshPrivate";
            this.btnRefreshPrivate.Size = new System.Drawing.Size(72, 23);
            this.btnRefreshPrivate.TabIndex = 3;
            this.btnRefreshPrivate.Text = "Refresh ";
            this.btnRefreshPrivate.UseVisualStyleBackColor = true;
            this.btnRefreshPrivate.Click += new System.EventHandler(this.btnRefreshPrivate_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(365, 278);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "QR Codes Gen";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udQuantity)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox tbPrivateKey;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnShowQRCode;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.NumericUpDown udQuantity;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnGenerateReport;
        private System.Windows.Forms.Button btnRefreshPrivate;
    }
}


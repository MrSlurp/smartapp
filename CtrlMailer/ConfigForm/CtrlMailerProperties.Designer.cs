namespace CtrlMailer
{
    partial class CtrlMailerProperties
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
            this.edtFrom = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.edtTo = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnCnfSMTP = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.edtSubject = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lblHost = new System.Windows.Forms.Label();
            this.lblSSL = new System.Windows.Forms.Label();
            this.lblPort = new System.Windows.Forms.Label();
            this.edtBody = new System.Windows.Forms.TextBox();
            this.btnInsertData = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // edtFrom
            // 
            this.edtFrom.Location = new System.Drawing.Point(12, 24);
            this.edtFrom.Name = "edtFrom";
            this.edtFrom.ReadOnly = true;
            this.edtFrom.Size = new System.Drawing.Size(173, 20);
            this.edtFrom.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "From";
            // 
            // edtTo
            // 
            this.edtTo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.edtTo.Location = new System.Drawing.Point(12, 63);
            this.edtTo.Name = "edtTo";
            this.edtTo.Size = new System.Drawing.Size(522, 20);
            this.edtTo.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(20, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "To";
            // 
            // btnCnfSMTP
            // 
            this.btnCnfSMTP.Location = new System.Drawing.Point(191, 21);
            this.btnCnfSMTP.Name = "btnCnfSMTP";
            this.btnCnfSMTP.Size = new System.Drawing.Size(140, 23);
            this.btnCnfSMTP.TabIndex = 4;
            this.btnCnfSMTP.Text = "Configure SMTP";
            this.btnCnfSMTP.UseVisualStyleBackColor = true;
            this.btnCnfSMTP.Click += new System.EventHandler(this.btnCnfSMTP_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 87);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Subject";
            // 
            // edtSubject
            // 
            this.edtSubject.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.edtSubject.Location = new System.Drawing.Point(12, 103);
            this.edtSubject.Name = "edtSubject";
            this.edtSubject.Size = new System.Drawing.Size(522, 20);
            this.edtSubject.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 126);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(50, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Message";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(338, 5);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(60, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "SMTP host";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(338, 34);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(49, 13);
            this.label6.TabIndex = 13;
            this.label6.Text = "Use SSL";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(338, 20);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(59, 13);
            this.label7.TabIndex = 12;
            this.label7.Text = "SMTP Port";
            // 
            // lblHost
            // 
            this.lblHost.AutoSize = true;
            this.lblHost.Location = new System.Drawing.Point(446, 5);
            this.lblHost.Name = "lblHost";
            this.lblHost.Size = new System.Drawing.Size(27, 13);
            this.lblHost.TabIndex = 14;
            this.lblHost.Text = "host";
            // 
            // lblSSL
            // 
            this.lblSSL.AutoSize = true;
            this.lblSSL.Location = new System.Drawing.Point(446, 34);
            this.lblSSL.Name = "lblSSL";
            this.lblSSL.Size = new System.Drawing.Size(27, 13);
            this.lblSSL.TabIndex = 16;
            this.lblSSL.Text = "SSL";
            // 
            // lblPort
            // 
            this.lblPort.AutoSize = true;
            this.lblPort.Location = new System.Drawing.Point(446, 20);
            this.lblPort.Name = "lblPort";
            this.lblPort.Size = new System.Drawing.Size(25, 13);
            this.lblPort.TabIndex = 15;
            this.lblPort.Text = "port";
            // 
            // edtBody
            // 
            this.edtBody.AcceptsReturn = true;
            this.edtBody.AcceptsTab = true;
            this.edtBody.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.edtBody.Location = new System.Drawing.Point(12, 143);
            this.edtBody.Multiline = true;
            this.edtBody.Name = "edtBody";
            this.edtBody.Size = new System.Drawing.Size(522, 166);
            this.edtBody.TabIndex = 3;
            // 
            // btnInsertData
            // 
            this.btnInsertData.Location = new System.Drawing.Point(12, 315);
            this.btnInsertData.Name = "btnInsertData";
            this.btnInsertData.Size = new System.Drawing.Size(114, 23);
            this.btnInsertData.TabIndex = 17;
            this.btnInsertData.Text = "Insert data";
            this.btnInsertData.UseVisualStyleBackColor = true;
            this.btnInsertData.Click += new System.EventHandler(this.btnInsertData_Click);
            // 
            // MailEditionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(546, 344);
            this.Controls.Add(this.btnInsertData);
            this.Controls.Add(this.edtBody);
            this.Controls.Add(this.lblPort);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.lblSSL);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.lblHost);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btnCnfSMTP);
            this.Controls.Add(this.edtSubject);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.edtTo);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.edtFrom);
            this.Name = "MailEditionForm";
            this.Text = "e-Mail configuration";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox edtFrom;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox edtTo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnCnfSMTP;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox edtSubject;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblHost;
        private System.Windows.Forms.Label lblSSL;
        private System.Windows.Forms.Label lblPort;
        private System.Windows.Forms.TextBox edtBody;
        private System.Windows.Forms.Button btnInsertData;
    }
}
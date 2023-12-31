namespace SmartApp
{
    partial class OptionForm
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
            this.m_btnBrowse = new System.Windows.Forms.Button();
            this.m_textLogDir = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.m_checkAutoStartProjOnOpen = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.m_checkHideMonitorOnStart = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // m_btnBrowse
            // 
            this.m_btnBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnBrowse.Location = new System.Drawing.Point(280, 31);
            this.m_btnBrowse.Name = "m_btnBrowse";
            this.m_btnBrowse.Size = new System.Drawing.Size(77, 20);
            this.m_btnBrowse.TabIndex = 13;
            this.m_btnBrowse.Text = "Browse";
            this.m_btnBrowse.UseVisualStyleBackColor = true;
            this.m_btnBrowse.Click += new System.EventHandler(this.m_btnBrowse_Click);
            // 
            // m_textLogDir
            // 
            this.m_textLogDir.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_textLogDir.Location = new System.Drawing.Point(10, 31);
            this.m_textLogDir.Name = "m_textLogDir";
            this.m_textLogDir.ReadOnly = true;
            this.m_textLogDir.Size = new System.Drawing.Size(263, 20);
            this.m_textLogDir.TabIndex = 12;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 13);
            this.label1.TabIndex = 14;
            this.label1.Text = "Log file directory";
            // 
            // m_checkAutoStartProjOnOpen
            // 
            this.m_checkAutoStartProjOnOpen.AutoSize = true;
            this.m_checkAutoStartProjOnOpen.Location = new System.Drawing.Point(10, 64);
            this.m_checkAutoStartProjOnOpen.Name = "m_checkAutoStartProjOnOpen";
            this.m_checkAutoStartProjOnOpen.Size = new System.Drawing.Size(267, 17);
            this.m_checkAutoStartProjOnOpen.TabIndex = 15;
            this.m_checkAutoStartProjOnOpen.Text = "Automatically start supervision after project opening";
            this.m_checkAutoStartProjOnOpen.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button1.Location = new System.Drawing.Point(12, 117);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 16;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button2.Location = new System.Drawing.Point(93, 117);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 16;
            this.button2.Text = "Cancel";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // m_checkHideMonitorOnStart
            // 
            this.m_checkHideMonitorOnStart.AutoSize = true;
            this.m_checkHideMonitorOnStart.Location = new System.Drawing.Point(10, 87);
            this.m_checkHideMonitorOnStart.Name = "m_checkHideMonitorOnStart";
            this.m_checkHideMonitorOnStart.Size = new System.Drawing.Size(167, 17);
            this.m_checkHideMonitorOnStart.TabIndex = 15;
            this.m_checkHideMonitorOnStart.Text = "Hide monitor after project start";
            this.m_checkHideMonitorOnStart.UseVisualStyleBackColor = true;
            // 
            // OptionForm
            // 
            this.AcceptButton = this.button1;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.button2;
            this.ClientSize = new System.Drawing.Size(390, 152);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.m_checkHideMonitorOnStart);
            this.Controls.Add(this.m_checkAutoStartProjOnOpen);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.m_btnBrowse);
            this.Controls.Add(this.m_textLogDir);
            this.Name = "OptionForm";
            this.Text = "OptionForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button m_btnBrowse;
        private System.Windows.Forms.TextBox m_textLogDir;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox m_checkAutoStartProjOnOpen;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.CheckBox m_checkHideMonitorOnStart;
    }
}
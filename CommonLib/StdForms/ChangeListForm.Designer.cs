namespace CommonLib
{
    partial class ChangeListForm
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
            this.m_BtnOK = new System.Windows.Forms.Button();
            this.m_BtnCancel = new System.Windows.Forms.Button();
            this.m_ListUsersMess = new System.Windows.Forms.ListBox();
            this.m_labelMess = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // m_BtnOK
            // 
            this.m_BtnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.m_BtnOK.Location = new System.Drawing.Point(13, 231);
            this.m_BtnOK.Name = "m_BtnOK";
            this.m_BtnOK.Size = new System.Drawing.Size(116, 23);
            this.m_BtnOK.TabIndex = 0;
            this.m_BtnOK.Text = "OK";
            this.m_BtnOK.UseVisualStyleBackColor = true;
            this.m_BtnOK.Click += new System.EventHandler(this.m_BtnOK_Click);
            // 
            // m_BtnCancel
            // 
            this.m_BtnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.m_BtnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_BtnCancel.Location = new System.Drawing.Point(164, 231);
            this.m_BtnCancel.Name = "m_BtnCancel";
            this.m_BtnCancel.Size = new System.Drawing.Size(116, 23);
            this.m_BtnCancel.TabIndex = 1;
            this.m_BtnCancel.Text = "Cancel";
            this.m_BtnCancel.UseVisualStyleBackColor = true;
            this.m_BtnCancel.Click += new System.EventHandler(this.m_BtnCancel_Click);
            // 
            // m_ListUsersMess
            // 
            this.m_ListUsersMess.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_ListUsersMess.FormattingEnabled = true;
            this.m_ListUsersMess.Location = new System.Drawing.Point(13, 26);
            this.m_ListUsersMess.Name = "m_ListUsersMess";
            this.m_ListUsersMess.Size = new System.Drawing.Size(283, 186);
            this.m_ListUsersMess.TabIndex = 2;
            // 
            // m_labelMess
            // 
            this.m_labelMess.AutoSize = true;
            this.m_labelMess.Location = new System.Drawing.Point(13, 7);
            this.m_labelMess.Name = "m_labelMess";
            this.m_labelMess.Size = new System.Drawing.Size(235, 13);
            this.m_labelMess.TabIndex = 3;
            this.m_labelMess.Text = "The item you want to delete is used. See details:";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 215);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(152, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Do really you want to delete it?";
            // 
            // ChangeListForm
            // 
            this.AcceptButton = this.m_BtnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.m_BtnCancel;
            this.ClientSize = new System.Drawing.Size(308, 266);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.m_labelMess);
            this.Controls.Add(this.m_ListUsersMess);
            this.Controls.Add(this.m_BtnCancel);
            this.Controls.Add(this.m_BtnOK);
            this.DoubleBuffered = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ChangeListForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Change list";
            this.Load += new System.EventHandler(this.ChangeListForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button m_BtnOK;
        private System.Windows.Forms.Button m_BtnCancel;
        private System.Windows.Forms.ListBox m_ListUsersMess;
        private System.Windows.Forms.Label m_labelMess;
        private System.Windows.Forms.Label label1;
    }
}
namespace CommonLib
{
    partial class ScreenPropertiesPanel
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label3 = new System.Windows.Forms.Label();
            this.m_textBoxTitle = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.m_textBkgndFile = new System.Windows.Forms.TextBox();
            this.m_btnBrowseBkFile = new CommonLib.BrowseFileBtn();
            this.m_btnRemoveFile = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(27, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Title";
            // 
            // m_textBoxTitle
            // 
            this.m_textBoxTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_textBoxTitle.Location = new System.Drawing.Point(0, 16);
            this.m_textBoxTitle.Name = "m_textBoxTitle";
            this.m_textBoxTitle.Size = new System.Drawing.Size(232, 20);
            this.m_textBoxTitle.TabIndex = 10;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 43);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(97, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Background Image";
            // 
            // m_textBkgndFile
            // 
            this.m_textBkgndFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_textBkgndFile.Location = new System.Drawing.Point(0, 59);
            this.m_textBkgndFile.Name = "m_textBkgndFile";
            this.m_textBkgndFile.ReadOnly = true;
            this.m_textBkgndFile.Size = new System.Drawing.Size(196, 20);
            this.m_textBkgndFile.TabIndex = 10;
            // 
            // m_btnBrowseBkFile
            // 
            this.m_btnBrowseBkFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnBrowseBkFile.Location = new System.Drawing.Point(202, 58);
            this.m_btnBrowseBkFile.Name = "m_btnBrowseBkFile";
            this.m_btnBrowseBkFile.Size = new System.Drawing.Size(30, 20);
            this.m_btnBrowseBkFile.TabIndex = 11;
            this.m_btnBrowseBkFile.Text = "...";
            this.m_btnBrowseBkFile.UseVisualStyleBackColor = true;
            this.m_btnBrowseBkFile.OnBrowseFrom += new CommonLib.BrowseFileBtn.BrowseFromEvent(this.m_btnBrowseBkFile_Click);
            // 
            // m_btnRemoveFile
            // 
            this.m_btnRemoveFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnRemoveFile.Location = new System.Drawing.Point(0, 85);
            this.m_btnRemoveFile.Name = "m_btnRemoveFile";
            this.m_btnRemoveFile.Size = new System.Drawing.Size(170, 23);
            this.m_btnRemoveFile.TabIndex = 11;
            this.m_btnRemoveFile.Text = "Remove background image";
            this.m_btnRemoveFile.UseVisualStyleBackColor = true;
            this.m_btnRemoveFile.Click += new System.EventHandler(this.m_btnRemoveFile_Click);
            // 
            // ScreenPropertiesPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.m_btnRemoveFile);
            this.Controls.Add(this.m_btnBrowseBkFile);
            this.Controls.Add(this.m_textBkgndFile);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.m_textBoxTitle);
            this.Controls.Add(this.label3);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "ScreenPropertiesPanel";
            this.Size = new System.Drawing.Size(232, 114);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox m_textBoxTitle;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox m_textBkgndFile;
        private CommonLib.BrowseFileBtn m_btnBrowseBkFile;
        private System.Windows.Forms.Button m_btnRemoveFile;
    }
}

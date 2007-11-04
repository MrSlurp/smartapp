namespace SmartApp.Ihm
{
    partial class ScreenPropertiesControl
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
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.m_richTextDesc = new System.Windows.Forms.RichTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.m_textBoxTitle = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.m_textBkgndFile = new System.Windows.Forms.TextBox();
            this.m_btnBrowseBkFile = new System.Windows.Forms.Button();
            this.m_textBoxSymbol = new SmartApp.Ihm.SymbolTextBox();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 101);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Symbol";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Description";
            // 
            // m_richTextDesc
            // 
            this.m_richTextDesc.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_richTextDesc.Location = new System.Drawing.Point(0, 16);
            this.m_richTextDesc.Name = "m_richTextDesc";
            this.m_richTextDesc.Size = new System.Drawing.Size(227, 79);
            this.m_richTextDesc.TabIndex = 7;
            this.m_richTextDesc.Text = "";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 147);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(27, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Title";
            // 
            // m_textBoxTitle
            // 
            this.m_textBoxTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_textBoxTitle.Location = new System.Drawing.Point(0, 163);
            this.m_textBoxTitle.Name = "m_textBoxTitle";
            this.m_textBoxTitle.Size = new System.Drawing.Size(227, 20);
            this.m_textBoxTitle.TabIndex = 10;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 190);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(97, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Background Image";
            // 
            // m_textBkgndFile
            // 
            this.m_textBkgndFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_textBkgndFile.Location = new System.Drawing.Point(0, 206);
            this.m_textBkgndFile.Name = "m_textBkgndFile";
            this.m_textBkgndFile.ReadOnly = true;
            this.m_textBkgndFile.Size = new System.Drawing.Size(165, 20);
            this.m_textBkgndFile.TabIndex = 10;
            // 
            // m_btnBrowseBkFile
            // 
            this.m_btnBrowseBkFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnBrowseBkFile.Location = new System.Drawing.Point(172, 206);
            this.m_btnBrowseBkFile.Name = "m_btnBrowseBkFile";
            this.m_btnBrowseBkFile.Size = new System.Drawing.Size(30, 20);
            this.m_btnBrowseBkFile.TabIndex = 11;
            this.m_btnBrowseBkFile.Text = "...";
            this.m_btnBrowseBkFile.UseVisualStyleBackColor = true;
            this.m_btnBrowseBkFile.Click += new System.EventHandler(this.m_btnBrowseBkFile_Click);
            // 
            // m_textBoxSymbol
            // 
            this.m_textBoxSymbol.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_textBoxSymbol.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.m_textBoxSymbol.Location = new System.Drawing.Point(0, 117);
            this.m_textBoxSymbol.Name = "m_textBoxSymbol";
            this.m_textBoxSymbol.Size = new System.Drawing.Size(227, 20);
            this.m_textBoxSymbol.TabIndex = 10;
            // 
            // ScreenPropertiesControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.m_btnBrowseBkFile);
            this.Controls.Add(this.m_textBkgndFile);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.m_textBoxTitle);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.m_textBoxSymbol);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.m_richTextDesc);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "ScreenPropertiesControl";
            this.Size = new System.Drawing.Size(227, 236);
            this.Validating += new System.ComponentModel.CancelEventHandler(this.PropertiesControlValidating);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private SmartApp.Ihm.SymbolTextBox m_textBoxSymbol;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox m_richTextDesc;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox m_textBoxTitle;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox m_textBkgndFile;
        private System.Windows.Forms.Button m_btnBrowseBkFile;
    }
}

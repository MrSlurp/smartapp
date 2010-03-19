namespace SmartApp.Ihm
{
    partial class FunctionPropertiesControl
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
            this.m_labelScript = new System.Windows.Forms.Label();
            this.m_textSymbol = new SmartApp.Ihm.SymbolTextBox();
            this.m_labelSymbol = new System.Windows.Forms.Label();
            this.m_labelDesc = new System.Windows.Forms.Label();
            this.m_richTextDesc = new System.Windows.Forms.RichTextBox();
            this.m_EditScript = new System.Windows.Forms.RichTextBox();
            this.m_BtnEditScript = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // m_labelScript
            // 
            this.m_labelScript.AutoSize = true;
            this.m_labelScript.Location = new System.Drawing.Point(9, 121);
            this.m_labelScript.Name = "m_labelScript";
            this.m_labelScript.Size = new System.Drawing.Size(34, 13);
            this.m_labelScript.TabIndex = 14;
            this.m_labelScript.Text = "Script";
            // 
            // m_textSymbol
            // 
            this.m_textSymbol.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.m_textSymbol.Location = new System.Drawing.Point(6, 94);
            this.m_textSymbol.Name = "m_textSymbol";
            this.m_textSymbol.Size = new System.Drawing.Size(180, 20);
            this.m_textSymbol.TabIndex = 13;
            // 
            // m_labelSymbol
            // 
            this.m_labelSymbol.AutoSize = true;
            this.m_labelSymbol.Location = new System.Drawing.Point(6, 78);
            this.m_labelSymbol.Name = "m_labelSymbol";
            this.m_labelSymbol.Size = new System.Drawing.Size(41, 13);
            this.m_labelSymbol.TabIndex = 12;
            this.m_labelSymbol.Text = "Symbol";
            // 
            // m_labelDesc
            // 
            this.m_labelDesc.AutoSize = true;
            this.m_labelDesc.Location = new System.Drawing.Point(6, 3);
            this.m_labelDesc.Name = "m_labelDesc";
            this.m_labelDesc.Size = new System.Drawing.Size(60, 13);
            this.m_labelDesc.TabIndex = 11;
            this.m_labelDesc.Text = "Description";
            // 
            // m_richTextDesc
            // 
            this.m_richTextDesc.Location = new System.Drawing.Point(6, 19);
            this.m_richTextDesc.Name = "m_richTextDesc";
            this.m_richTextDesc.Size = new System.Drawing.Size(387, 53);
            this.m_richTextDesc.TabIndex = 10;
            this.m_richTextDesc.Text = "";
            // 
            // m_EditScript
            // 
            this.m_EditScript.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_EditScript.Location = new System.Drawing.Point(6, 137);
            this.m_EditScript.Name = "m_EditScript";
            this.m_EditScript.ReadOnly = true;
            this.m_EditScript.Size = new System.Drawing.Size(429, 179);
            this.m_EditScript.TabIndex = 9;
            this.m_EditScript.Text = "";
            // 
            // m_BtnEditScript
            // 
            this.m_BtnEditScript.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.m_BtnEditScript.Location = new System.Drawing.Point(12, 323);
            this.m_BtnEditScript.Name = "m_BtnEditScript";
            this.m_BtnEditScript.Size = new System.Drawing.Size(126, 23);
            this.m_BtnEditScript.TabIndex = 16;
            this.m_BtnEditScript.Text = "Edit Script";
            this.m_BtnEditScript.UseVisualStyleBackColor = true;
            this.m_BtnEditScript.Click += new System.EventHandler(this.OnBtnEditScriptClick);
            // 
            // FunctionPropertiesControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.m_BtnEditScript);
            this.Controls.Add(this.m_labelScript);
            this.Controls.Add(this.m_textSymbol);
            this.Controls.Add(this.m_labelSymbol);
            this.Controls.Add(this.m_labelDesc);
            this.Controls.Add(this.m_richTextDesc);
            this.Controls.Add(this.m_EditScript);
            this.Name = "FunctionPropertiesControl";
            this.Size = new System.Drawing.Size(441, 351);
            this.Validating += new System.ComponentModel.CancelEventHandler(this.PropertiesControlValidating);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label m_labelScript;
        private SmartApp.Ihm.SymbolTextBox m_textSymbol;
        private System.Windows.Forms.Label m_labelSymbol;
        private System.Windows.Forms.Label m_labelDesc;
        private System.Windows.Forms.RichTextBox m_richTextDesc;
        private System.Windows.Forms.RichTextBox m_EditScript;
        private System.Windows.Forms.Button m_BtnEditScript;
    }
}

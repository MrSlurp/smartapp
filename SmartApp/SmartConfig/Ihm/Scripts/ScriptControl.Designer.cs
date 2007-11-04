namespace SmartApp.Ihm
{
    partial class ScriptControl
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
            this.m_TextScript = new System.Windows.Forms.RichTextBox();
            this.m_labelTitle = new System.Windows.Forms.Label();
            this.m_BtnEditScript = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // m_TextScript
            // 
            this.m_TextScript.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_TextScript.BackColor = System.Drawing.Color.White;
            this.m_TextScript.Location = new System.Drawing.Point(3, 20);
            this.m_TextScript.Name = "m_TextScript";
            this.m_TextScript.ReadOnly = true;
            this.m_TextScript.Size = new System.Drawing.Size(196, 100);
            this.m_TextScript.TabIndex = 0;
            this.m_TextScript.Text = "";
            this.m_TextScript.WordWrap = false;
            // 
            // m_labelTitle
            // 
            this.m_labelTitle.AutoSize = true;
            this.m_labelTitle.Location = new System.Drawing.Point(4, 4);
            this.m_labelTitle.Name = "m_labelTitle";
            this.m_labelTitle.Size = new System.Drawing.Size(27, 13);
            this.m_labelTitle.TabIndex = 1;
            this.m_labelTitle.Text = "Title";
            // 
            // m_BtnEditScript
            // 
            this.m_BtnEditScript.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.m_BtnEditScript.Location = new System.Drawing.Point(3, 126);
            this.m_BtnEditScript.Name = "m_BtnEditScript";
            this.m_BtnEditScript.Size = new System.Drawing.Size(75, 23);
            this.m_BtnEditScript.TabIndex = 17;
            this.m_BtnEditScript.Text = "Edit Script";
            this.m_BtnEditScript.UseVisualStyleBackColor = true;
            this.m_BtnEditScript.Click += new System.EventHandler(this.m_BtnEditScript_Click);
            // 
            // ScriptControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.m_BtnEditScript);
            this.Controls.Add(this.m_labelTitle);
            this.Controls.Add(this.m_TextScript);
            this.Name = "ScriptControl";
            this.Size = new System.Drawing.Size(202, 152);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox m_TextScript;
        private System.Windows.Forms.Label m_labelTitle;
        private System.Windows.Forms.Button m_BtnEditScript;
    }
}

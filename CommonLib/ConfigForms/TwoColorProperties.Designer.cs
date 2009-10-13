namespace CommonLib
{
    partial class TwoColorProperties
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.m_BtnSelectActiveColor = new System.Windows.Forms.Button();
            this.m_TextActiveColor = new System.Windows.Forms.TextBox();
            this.m_BtnSelectInactiveColor = new System.Windows.Forms.Button();
            this.m_TextInactiveColor = new System.Windows.Forms.TextBox();
            this.m_clrDlg = new System.Windows.Forms.ColorDialog();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(-2, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Inactive color";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(-2, 4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Active color";
            // 
            // m_BtnSelectActiveColor
            // 
            this.m_BtnSelectActiveColor.Location = new System.Drawing.Point(101, 0);
            this.m_BtnSelectActiveColor.Name = "m_BtnSelectActiveColor";
            this.m_BtnSelectActiveColor.Size = new System.Drawing.Size(51, 20);
            this.m_BtnSelectActiveColor.TabIndex = 9;
            this.m_BtnSelectActiveColor.Text = "Select";
            this.m_BtnSelectActiveColor.UseVisualStyleBackColor = true;
            this.m_BtnSelectActiveColor.Click += new System.EventHandler(this.m_BtnSelectActiveColor_Click);
            // 
            // m_TextActiveColor
            // 
            this.m_TextActiveColor.BackColor = System.Drawing.Color.White;
            this.m_TextActiveColor.Location = new System.Drawing.Point(72, 0);
            this.m_TextActiveColor.Name = "m_TextActiveColor";
            this.m_TextActiveColor.ReadOnly = true;
            this.m_TextActiveColor.Size = new System.Drawing.Size(28, 20);
            this.m_TextActiveColor.TabIndex = 8;
            // 
            // m_BtnSelectInactiveColor
            // 
            this.m_BtnSelectInactiveColor.Location = new System.Drawing.Point(101, 24);
            this.m_BtnSelectInactiveColor.Name = "m_BtnSelectInactiveColor";
            this.m_BtnSelectInactiveColor.Size = new System.Drawing.Size(51, 20);
            this.m_BtnSelectInactiveColor.TabIndex = 11;
            this.m_BtnSelectInactiveColor.Text = "Select";
            this.m_BtnSelectInactiveColor.UseVisualStyleBackColor = true;
            this.m_BtnSelectInactiveColor.Click += new System.EventHandler(this.m_BtnSelectInactiveColor_Click);
            // 
            // m_TextInactiveColor
            // 
            this.m_TextInactiveColor.BackColor = System.Drawing.Color.White;
            this.m_TextInactiveColor.Location = new System.Drawing.Point(72, 24);
            this.m_TextInactiveColor.Name = "m_TextInactiveColor";
            this.m_TextInactiveColor.ReadOnly = true;
            this.m_TextInactiveColor.Size = new System.Drawing.Size(28, 20);
            this.m_TextInactiveColor.TabIndex = 10;
            // 
            // TwoColorProperties
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.m_BtnSelectInactiveColor);
            this.Controls.Add(this.m_TextInactiveColor);
            this.Controls.Add(this.m_BtnSelectActiveColor);
            this.Controls.Add(this.m_TextActiveColor);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "TwoColorProperties";
            this.Size = new System.Drawing.Size(155, 47);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button m_BtnSelectActiveColor;
        private System.Windows.Forms.TextBox m_TextActiveColor;
        private System.Windows.Forms.Button m_BtnSelectInactiveColor;
        private System.Windows.Forms.TextBox m_TextInactiveColor;
        private System.Windows.Forms.ColorDialog m_clrDlg;
    }
}

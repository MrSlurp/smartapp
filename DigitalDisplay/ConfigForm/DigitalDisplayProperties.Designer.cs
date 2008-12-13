namespace DigitalDisplay
{
    partial class DigitalDisplayProperties
    {
        /// <summary> 
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur de composants

        /// <summary> 
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas 
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.m_rdBtn3 = new System.Windows.Forms.RadioButton();
            this.m_rdBtn2 = new System.Windows.Forms.RadioButton();
            this.m_rdBtn1 = new System.Windows.Forms.RadioButton();
            this.m_rdBtn0 = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.m_BtnSelectMinColor = new System.Windows.Forms.Button();
            this.m_TextColor = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.m_clrDlg = new System.Windows.Forms.ColorDialog();
            this.SuspendLayout();
            // 
            // m_rdBtn3
            // 
            this.m_rdBtn3.AutoSize = true;
            this.m_rdBtn3.Location = new System.Drawing.Point(18, 121);
            this.m_rdBtn3.Name = "m_rdBtn3";
            this.m_rdBtn3.Size = new System.Drawing.Size(58, 17);
            this.m_rdBtn3.TabIndex = 4;
            this.m_rdBtn3.TabStop = true;
            this.m_rdBtn3.Text = "12.345";
            this.m_rdBtn3.UseVisualStyleBackColor = true;
            this.m_rdBtn3.Click += new System.EventHandler(this.m_rdBtn_CheckedChanged);
            // 
            // m_rdBtn2
            // 
            this.m_rdBtn2.AutoSize = true;
            this.m_rdBtn2.Location = new System.Drawing.Point(18, 98);
            this.m_rdBtn2.Name = "m_rdBtn2";
            this.m_rdBtn2.Size = new System.Drawing.Size(58, 17);
            this.m_rdBtn2.TabIndex = 5;
            this.m_rdBtn2.TabStop = true;
            this.m_rdBtn2.Text = "123.45";
            this.m_rdBtn2.UseVisualStyleBackColor = true;
            this.m_rdBtn2.Click += new System.EventHandler(this.m_rdBtn_CheckedChanged);
            // 
            // m_rdBtn1
            // 
            this.m_rdBtn1.AutoSize = true;
            this.m_rdBtn1.Location = new System.Drawing.Point(18, 75);
            this.m_rdBtn1.Name = "m_rdBtn1";
            this.m_rdBtn1.Size = new System.Drawing.Size(58, 17);
            this.m_rdBtn1.TabIndex = 6;
            this.m_rdBtn1.TabStop = true;
            this.m_rdBtn1.Text = "1234.5";
            this.m_rdBtn1.UseVisualStyleBackColor = true;
            this.m_rdBtn1.Click += new System.EventHandler(this.m_rdBtn_CheckedChanged);
            // 
            // m_rdBtn0
            // 
            this.m_rdBtn0.AutoSize = true;
            this.m_rdBtn0.Location = new System.Drawing.Point(18, 52);
            this.m_rdBtn0.Name = "m_rdBtn0";
            this.m_rdBtn0.Size = new System.Drawing.Size(55, 17);
            this.m_rdBtn0.TabIndex = 3;
            this.m_rdBtn0.TabStop = true;
            this.m_rdBtn0.Text = "12345";
            this.m_rdBtn0.UseVisualStyleBackColor = true;
            this.m_rdBtn0.Click += new System.EventHandler(this.m_rdBtn_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Format";
            // 
            // m_BtnSelectMinColor
            // 
            this.m_BtnSelectMinColor.Location = new System.Drawing.Point(72, 8);
            this.m_BtnSelectMinColor.Name = "m_BtnSelectMinColor";
            this.m_BtnSelectMinColor.Size = new System.Drawing.Size(51, 20);
            this.m_BtnSelectMinColor.TabIndex = 18;
            this.m_BtnSelectMinColor.Text = "Select";
            this.m_BtnSelectMinColor.UseVisualStyleBackColor = true;
            this.m_BtnSelectMinColor.Click += new System.EventHandler(this.m_BtnSelectColor_Click);
            // 
            // m_TextColor
            // 
            this.m_TextColor.BackColor = System.Drawing.Color.White;
            this.m_TextColor.Location = new System.Drawing.Point(43, 8);
            this.m_TextColor.Name = "m_TextColor";
            this.m_TextColor.ReadOnly = true;
            this.m_TextColor.Size = new System.Drawing.Size(28, 20);
            this.m_TextColor.TabIndex = 17;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 13);
            this.label2.TabIndex = 16;
            this.label2.Text = "Color";
            // 
            // DigitalDisplayProperties
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.m_BtnSelectMinColor);
            this.Controls.Add(this.m_TextColor);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.m_rdBtn3);
            this.Controls.Add(this.m_rdBtn2);
            this.Controls.Add(this.m_rdBtn1);
            this.Controls.Add(this.m_rdBtn0);
            this.Controls.Add(this.label1);
            this.Name = "DigitalDisplayProperties";
            this.Size = new System.Drawing.Size(134, 146);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton m_rdBtn3;
        private System.Windows.Forms.RadioButton m_rdBtn2;
        private System.Windows.Forms.RadioButton m_rdBtn1;
        private System.Windows.Forms.RadioButton m_rdBtn0;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button m_BtnSelectMinColor;
        private System.Windows.Forms.TextBox m_TextColor;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ColorDialog m_clrDlg;

    }
}

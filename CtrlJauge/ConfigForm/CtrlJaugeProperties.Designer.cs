namespace CtrlJauge
{
    partial class CtrlJaugeProperties
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
            this.m_BtnSelecMaxColor = new System.Windows.Forms.Button();
            this.m_TextMaxColor = new System.Windows.Forms.TextBox();
            this.m_BtnSelectMinColor = new System.Windows.Forms.Button();
            this.m_TextMinColor = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.m_rdBtn3 = new System.Windows.Forms.RadioButton();
            this.m_rdBtn2 = new System.Windows.Forms.RadioButton();
            this.m_rdBtn1 = new System.Windows.Forms.RadioButton();
            this.m_rdBtn0 = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.m_clrDlg = new System.Windows.Forms.ColorDialog();
            this.SuspendLayout();
            // 
            // m_BtnSelecMaxColor
            // 
            this.m_BtnSelecMaxColor.Location = new System.Drawing.Point(115, 131);
            this.m_BtnSelecMaxColor.Name = "m_BtnSelecMaxColor";
            this.m_BtnSelecMaxColor.Size = new System.Drawing.Size(51, 20);
            this.m_BtnSelecMaxColor.TabIndex = 17;
            this.m_BtnSelecMaxColor.Text = "Select";
            this.m_BtnSelecMaxColor.UseVisualStyleBackColor = true;
            this.m_BtnSelecMaxColor.Click += new System.EventHandler(this.m_BtnSelecMaxColor_Click);
            // 
            // m_TextMaxColor
            // 
            this.m_TextMaxColor.BackColor = System.Drawing.Color.White;
            this.m_TextMaxColor.Location = new System.Drawing.Point(86, 131);
            this.m_TextMaxColor.Name = "m_TextMaxColor";
            this.m_TextMaxColor.ReadOnly = true;
            this.m_TextMaxColor.Size = new System.Drawing.Size(28, 20);
            this.m_TextMaxColor.TabIndex = 16;
            // 
            // m_BtnSelectMinColor
            // 
            this.m_BtnSelectMinColor.Location = new System.Drawing.Point(115, 107);
            this.m_BtnSelectMinColor.Name = "m_BtnSelectMinColor";
            this.m_BtnSelectMinColor.Size = new System.Drawing.Size(51, 20);
            this.m_BtnSelectMinColor.TabIndex = 15;
            this.m_BtnSelectMinColor.Text = "Select";
            this.m_BtnSelectMinColor.UseVisualStyleBackColor = true;
            this.m_BtnSelectMinColor.Click += new System.EventHandler(this.m_BtnSelectMinColor_Click);
            // 
            // m_TextMinColor
            // 
            this.m_TextMinColor.BackColor = System.Drawing.Color.White;
            this.m_TextMinColor.Location = new System.Drawing.Point(86, 107);
            this.m_TextMinColor.Name = "m_TextMinColor";
            this.m_TextMinColor.ReadOnly = true;
            this.m_TextMinColor.Size = new System.Drawing.Size(28, 20);
            this.m_TextMinColor.TabIndex = 14;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 111);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Min color";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 135);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "Max color";
            // 
            // m_rdBtn3
            // 
            this.m_rdBtn3.AutoSize = true;
            this.m_rdBtn3.Location = new System.Drawing.Point(15, 87);
            this.m_rdBtn3.Name = "m_rdBtn3";
            this.m_rdBtn3.Size = new System.Drawing.Size(125, 17);
            this.m_rdBtn3.TabIndex = 20;
            this.m_rdBtn3.TabStop = true;
            this.m_rdBtn3.Text = "Vertical top to bottom";
            this.m_rdBtn3.UseVisualStyleBackColor = true;
            this.m_rdBtn3.CheckedChanged += new System.EventHandler(this.m_rdBtn_CheckedChanged);
            // 
            // m_rdBtn2
            // 
            this.m_rdBtn2.AutoSize = true;
            this.m_rdBtn2.Location = new System.Drawing.Point(15, 64);
            this.m_rdBtn2.Name = "m_rdBtn2";
            this.m_rdBtn2.Size = new System.Drawing.Size(125, 17);
            this.m_rdBtn2.TabIndex = 21;
            this.m_rdBtn2.TabStop = true;
            this.m_rdBtn2.Text = "Vertical bottom to top";
            this.m_rdBtn2.UseVisualStyleBackColor = true;
            this.m_rdBtn2.CheckedChanged += new System.EventHandler(this.m_rdBtn_CheckedChanged);
            // 
            // m_rdBtn1
            // 
            this.m_rdBtn1.AutoSize = true;
            this.m_rdBtn1.Location = new System.Drawing.Point(15, 41);
            this.m_rdBtn1.Name = "m_rdBtn1";
            this.m_rdBtn1.Size = new System.Drawing.Size(124, 17);
            this.m_rdBtn1.TabIndex = 18;
            this.m_rdBtn1.TabStop = true;
            this.m_rdBtn1.Text = "Horizontal left to right";
            this.m_rdBtn1.UseVisualStyleBackColor = true;
            this.m_rdBtn1.CheckedChanged += new System.EventHandler(this.m_rdBtn_CheckedChanged);
            // 
            // m_rdBtn0
            // 
            this.m_rdBtn0.AutoSize = true;
            this.m_rdBtn0.Location = new System.Drawing.Point(15, 18);
            this.m_rdBtn0.Name = "m_rdBtn0";
            this.m_rdBtn0.Size = new System.Drawing.Size(124, 17);
            this.m_rdBtn0.TabIndex = 19;
            this.m_rdBtn0.TabStop = true;
            this.m_rdBtn0.Text = "Horizontal right to left";
            this.m_rdBtn0.UseVisualStyleBackColor = true;
            this.m_rdBtn0.CheckedChanged += new System.EventHandler(this.m_rdBtn_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 2);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 13);
            this.label3.TabIndex = 22;
            this.label3.Text = "Orientation";
            // 
            // CtrlJaugeProperties
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.label3);
            this.Controls.Add(this.m_rdBtn3);
            this.Controls.Add(this.m_rdBtn2);
            this.Controls.Add(this.m_rdBtn1);
            this.Controls.Add(this.m_rdBtn0);
            this.Controls.Add(this.m_BtnSelecMaxColor);
            this.Controls.Add(this.m_TextMaxColor);
            this.Controls.Add(this.m_BtnSelectMinColor);
            this.Controls.Add(this.m_TextMinColor);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "CtrlJaugeProperties";
            this.Size = new System.Drawing.Size(176, 157);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button m_BtnSelecMaxColor;
        private System.Windows.Forms.TextBox m_TextMaxColor;
        private System.Windows.Forms.Button m_BtnSelectMinColor;
        private System.Windows.Forms.TextBox m_TextMinColor;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton m_rdBtn3;
        private System.Windows.Forms.RadioButton m_rdBtn2;
        private System.Windows.Forms.RadioButton m_rdBtn1;
        private System.Windows.Forms.RadioButton m_rdBtn0;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ColorDialog m_clrDlg;

    }
}

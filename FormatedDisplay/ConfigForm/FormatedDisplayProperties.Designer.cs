namespace FormatedDisplay
{
    partial class FormatedDisplayProperties
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
            this.label1 = new System.Windows.Forms.Label();
            this.m_rdBtn0 = new System.Windows.Forms.RadioButton();
            this.m_rdBtn1 = new System.Windows.Forms.RadioButton();
            this.m_rdBtn2 = new System.Windows.Forms.RadioButton();
            this.m_rdBtn3 = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Format";
            // 
            // m_rdBtn0
            // 
            this.m_rdBtn0.AutoSize = true;
            this.m_rdBtn0.Location = new System.Drawing.Point(18, 16);
            this.m_rdBtn0.Name = "m_rdBtn0";
            this.m_rdBtn0.Size = new System.Drawing.Size(55, 17);
            this.m_rdBtn0.TabIndex = 1;
            this.m_rdBtn0.TabStop = true;
            this.m_rdBtn0.Text = "12345";
            this.m_rdBtn0.UseVisualStyleBackColor = true;
            this.m_rdBtn0.CheckedChanged += new System.EventHandler(this.m_rdBtn_CheckedChanged);
            // 
            // m_rdBtn1
            // 
            this.m_rdBtn1.AutoSize = true;
            this.m_rdBtn1.Location = new System.Drawing.Point(79, 16);
            this.m_rdBtn1.Name = "m_rdBtn1";
            this.m_rdBtn1.Size = new System.Drawing.Size(58, 17);
            this.m_rdBtn1.TabIndex = 1;
            this.m_rdBtn1.TabStop = true;
            this.m_rdBtn1.Text = "1234.5";
            this.m_rdBtn1.UseVisualStyleBackColor = true;
            this.m_rdBtn1.CheckedChanged += new System.EventHandler(this.m_rdBtn_CheckedChanged);
            // 
            // m_rdBtn2
            // 
            this.m_rdBtn2.AutoSize = true;
            this.m_rdBtn2.Location = new System.Drawing.Point(143, 16);
            this.m_rdBtn2.Name = "m_rdBtn2";
            this.m_rdBtn2.Size = new System.Drawing.Size(58, 17);
            this.m_rdBtn2.TabIndex = 1;
            this.m_rdBtn2.TabStop = true;
            this.m_rdBtn2.Text = "123.45";
            this.m_rdBtn2.UseVisualStyleBackColor = true;
            this.m_rdBtn2.CheckedChanged += new System.EventHandler(this.m_rdBtn_CheckedChanged);
            // 
            // m_rdBtn3
            // 
            this.m_rdBtn3.AutoSize = true;
            this.m_rdBtn3.Location = new System.Drawing.Point(207, 16);
            this.m_rdBtn3.Name = "m_rdBtn3";
            this.m_rdBtn3.Size = new System.Drawing.Size(58, 17);
            this.m_rdBtn3.TabIndex = 1;
            this.m_rdBtn3.TabStop = true;
            this.m_rdBtn3.Text = "12.345";
            this.m_rdBtn3.UseVisualStyleBackColor = true;
            this.m_rdBtn3.CheckedChanged += new System.EventHandler(this.m_rdBtn_CheckedChanged);
            // 
            // FormatedDisplayProperties
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.m_rdBtn3);
            this.Controls.Add(this.m_rdBtn2);
            this.Controls.Add(this.m_rdBtn1);
            this.Controls.Add(this.m_rdBtn0);
            this.Controls.Add(this.label1);
            this.Name = "FormatedDisplayProperties";
            this.Size = new System.Drawing.Size(283, 45);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton m_rdBtn0;
        private System.Windows.Forms.RadioButton m_rdBtn1;
        private System.Windows.Forms.RadioButton m_rdBtn2;
        private System.Windows.Forms.RadioButton m_rdBtn3;

    }
}

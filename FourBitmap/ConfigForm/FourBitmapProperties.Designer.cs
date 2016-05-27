namespace FourBitmap
{
    partial class FourBitmapProperties
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
            this.m_txtBoxImg1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.m_btnImg1 = new CommonLib.BrowseFileBtn();
            this.m_txtBoxImg0 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.m_btnImg0 = new CommonLib.BrowseFileBtn();
            this.m_txtBoxImg3 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.m_btnImg3 = new CommonLib.BrowseFileBtn();
            this.m_txtBoxImg2 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.m_btnImg2 = new CommonLib.BrowseFileBtn();
            this.SuspendLayout();
            // 
            // m_txtBoxImg1
            // 
            this.m_txtBoxImg1.Location = new System.Drawing.Point(4, 61);
            this.m_txtBoxImg1.Name = "m_txtBoxImg1";
            this.m_txtBoxImg1.ReadOnly = true;
            this.m_txtBoxImg1.Size = new System.Drawing.Size(163, 20);
            this.m_txtBoxImg1.TabIndex = 7;
            this.m_txtBoxImg1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_txtBoxImgn_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(1, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Bitmap 1";
            // 
            // m_btnImg1
            // 
            this.m_btnImg1.Location = new System.Drawing.Point(173, 61);
            this.m_btnImg1.Name = "m_btnImg1";
            this.m_btnImg1.Size = new System.Drawing.Size(34, 20);
            this.m_btnImg1.TabIndex = 4;
            this.m_btnImg1.Text = "...";
            this.m_btnImg1.UseVisualStyleBackColor = true;
            this.m_btnImg1.OnBrowseFrom += new CommonLib.BrowseFileBtn.BrowseFromEvent(this.m_btnImg1_Click);
            // 
            // m_txtBoxImg0
            // 
            this.m_txtBoxImg0.Location = new System.Drawing.Point(4, 20);
            this.m_txtBoxImg0.Name = "m_txtBoxImg0";
            this.m_txtBoxImg0.ReadOnly = true;
            this.m_txtBoxImg0.Size = new System.Drawing.Size(163, 20);
            this.m_txtBoxImg0.TabIndex = 8;
            this.m_txtBoxImg0.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_txtBoxImgn_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Bitmap 0";
            // 
            // m_btnImg0
            // 
            this.m_btnImg0.Location = new System.Drawing.Point(173, 20);
            this.m_btnImg0.Name = "m_btnImg0";
            this.m_btnImg0.Size = new System.Drawing.Size(34, 20);
            this.m_btnImg0.TabIndex = 3;
            this.m_btnImg0.Text = "...";
            this.m_btnImg0.UseVisualStyleBackColor = true;
            this.m_btnImg0.OnBrowseFrom += new CommonLib.BrowseFileBtn.BrowseFromEvent(this.m_btnImg0_Click);
            // 
            // m_txtBoxImg3
            // 
            this.m_txtBoxImg3.Location = new System.Drawing.Point(4, 144);
            this.m_txtBoxImg3.Name = "m_txtBoxImg3";
            this.m_txtBoxImg3.ReadOnly = true;
            this.m_txtBoxImg3.Size = new System.Drawing.Size(163, 20);
            this.m_txtBoxImg3.TabIndex = 13;
            this.m_txtBoxImg3.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_txtBoxImgn_KeyDown);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(1, 128);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "Bitmap 3";
            // 
            // m_btnImg3
            // 
            this.m_btnImg3.Location = new System.Drawing.Point(173, 144);
            this.m_btnImg3.Name = "m_btnImg3";
            this.m_btnImg3.Size = new System.Drawing.Size(34, 20);
            this.m_btnImg3.TabIndex = 9;
            this.m_btnImg3.Text = "...";
            this.m_btnImg3.UseVisualStyleBackColor = true;
            this.m_btnImg3.OnBrowseFrom += new CommonLib.BrowseFileBtn.BrowseFromEvent(this.m_btnImg3_Click);
            // 
            // m_txtBoxImg2
            // 
            this.m_txtBoxImg2.Location = new System.Drawing.Point(4, 103);
            this.m_txtBoxImg2.Name = "m_txtBoxImg2";
            this.m_txtBoxImg2.ReadOnly = true;
            this.m_txtBoxImg2.Size = new System.Drawing.Size(163, 20);
            this.m_txtBoxImg2.TabIndex = 14;
            this.m_txtBoxImg2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_txtBoxImgn_KeyDown);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(1, 87);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(48, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Bitmap 2";
            // 
            // m_btnImg2
            // 
            this.m_btnImg2.Location = new System.Drawing.Point(173, 103);
            this.m_btnImg2.Name = "m_btnImg2";
            this.m_btnImg2.Size = new System.Drawing.Size(34, 20);
            this.m_btnImg2.TabIndex = 10;
            this.m_btnImg2.Text = "...";
            this.m_btnImg2.UseVisualStyleBackColor = true;
            this.m_btnImg2.OnBrowseFrom += new CommonLib.BrowseFileBtn.BrowseFromEvent(this.m_btnImg2_Click);
            // 
            // FourBitmapProperties
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.m_txtBoxImg3);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.m_btnImg3);
            this.Controls.Add(this.m_txtBoxImg2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.m_btnImg2);
            this.Controls.Add(this.m_txtBoxImg1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.m_btnImg1);
            this.Controls.Add(this.m_txtBoxImg0);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.m_btnImg0);
            this.Name = "FourBitmapProperties";
            this.Size = new System.Drawing.Size(208, 172);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox m_txtBoxImg1;
        private System.Windows.Forms.Label label2;
        private CommonLib.BrowseFileBtn m_btnImg1;
        private System.Windows.Forms.TextBox m_txtBoxImg0;
        private System.Windows.Forms.Label label1;
        private CommonLib.BrowseFileBtn m_btnImg0;
        private System.Windows.Forms.TextBox m_txtBoxImg3;
        private System.Windows.Forms.Label label3;
        private CommonLib.BrowseFileBtn m_btnImg3;
        private System.Windows.Forms.TextBox m_txtBoxImg2;
        private System.Windows.Forms.Label label4;
        private CommonLib.BrowseFileBtn m_btnImg2;

    }
}

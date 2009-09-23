namespace CtrlTwoBitmap
{
    partial class TwoBitmapProperties
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
            this.m_btnImg1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.m_txtBoxImg1 = new System.Windows.Forms.TextBox();
            this.m_openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.m_btnImg2 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.m_txtBoxImg2 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // m_btnImg1
            // 
            this.m_btnImg1.Location = new System.Drawing.Point(170, 18);
            this.m_btnImg1.Name = "m_btnImg1";
            this.m_btnImg1.Size = new System.Drawing.Size(34, 20);
            this.m_btnImg1.TabIndex = 0;
            this.m_btnImg1.Text = "...";
            this.m_btnImg1.UseVisualStyleBackColor = true;
            this.m_btnImg1.Click += new System.EventHandler(this.m_btnImg1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(-2, 2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Inactive Bitmap";
            // 
            // m_txtBoxImg1
            // 
            this.m_txtBoxImg1.Location = new System.Drawing.Point(1, 18);
            this.m_txtBoxImg1.Name = "m_txtBoxImg1";
            this.m_txtBoxImg1.ReadOnly = true;
            this.m_txtBoxImg1.Size = new System.Drawing.Size(163, 20);
            this.m_txtBoxImg1.TabIndex = 2;
            // 
            // m_openFileDialog
            // 
            this.m_openFileDialog.FileName = "";
            // 
            // m_btnImg2
            // 
            this.m_btnImg2.Location = new System.Drawing.Point(170, 59);
            this.m_btnImg2.Name = "m_btnImg2";
            this.m_btnImg2.Size = new System.Drawing.Size(34, 20);
            this.m_btnImg2.TabIndex = 0;
            this.m_btnImg2.Text = "...";
            this.m_btnImg2.UseVisualStyleBackColor = true;
            this.m_btnImg2.Click += new System.EventHandler(this.m_btnImg2_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(-2, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Active Bitmap";
            // 
            // m_txtBoxImg2
            // 
            this.m_txtBoxImg2.Location = new System.Drawing.Point(1, 59);
            this.m_txtBoxImg2.Name = "m_txtBoxImg2";
            this.m_txtBoxImg2.ReadOnly = true;
            this.m_txtBoxImg2.Size = new System.Drawing.Size(163, 20);
            this.m_txtBoxImg2.TabIndex = 2;
            // 
            // TwoBitmapProperties
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.m_txtBoxImg2);
            this.Controls.Add(this.m_txtBoxImg1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.m_btnImg2);
            this.Controls.Add(this.m_btnImg1);
            this.Name = "TwoBitmapProperties";
            this.Size = new System.Drawing.Size(208, 84);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button m_btnImg1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox m_txtBoxImg1;
        private System.Windows.Forms.OpenFileDialog m_openFileDialog;
        private System.Windows.Forms.Button m_btnImg2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox m_txtBoxImg2;
    }
}

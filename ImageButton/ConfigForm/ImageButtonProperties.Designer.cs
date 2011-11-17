namespace ImageButton
{
    partial class ImageButtonProperties
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
            this.label2 = new System.Windows.Forms.Label();
            this.m_btnImg2 = new System.Windows.Forms.Button();
            this.m_btnImg1 = new System.Windows.Forms.Button();
            this.m_openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.m_txtBoxImg2 = new System.Windows.Forms.TextBox();
            this.m_txtBoxImg1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.chkBistable = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(1, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Pressed image";
            // 
            // m_btnImg2
            // 
            this.m_btnImg2.Location = new System.Drawing.Point(173, 61);
            this.m_btnImg2.Name = "m_btnImg2";
            this.m_btnImg2.Size = new System.Drawing.Size(34, 20);
            this.m_btnImg2.TabIndex = 4;
            this.m_btnImg2.Text = "...";
            this.m_btnImg2.UseVisualStyleBackColor = true;
            this.m_btnImg2.Click += new System.EventHandler(this.m_btnImg2_Click);
            // 
            // m_btnImg1
            // 
            this.m_btnImg1.Location = new System.Drawing.Point(173, 20);
            this.m_btnImg1.Name = "m_btnImg1";
            this.m_btnImg1.Size = new System.Drawing.Size(34, 20);
            this.m_btnImg1.TabIndex = 3;
            this.m_btnImg1.Text = "...";
            this.m_btnImg1.UseVisualStyleBackColor = true;
            this.m_btnImg1.Click += new System.EventHandler(this.m_btnImg1_Click);
            // 
            // m_txtBoxImg2
            // 
            this.m_txtBoxImg2.Location = new System.Drawing.Point(4, 61);
            this.m_txtBoxImg2.Name = "m_txtBoxImg2";
            this.m_txtBoxImg2.ReadOnly = true;
            this.m_txtBoxImg2.Size = new System.Drawing.Size(163, 20);
            this.m_txtBoxImg2.TabIndex = 7;
            // 
            // m_txtBoxImg1
            // 
            this.m_txtBoxImg1.Location = new System.Drawing.Point(4, 20);
            this.m_txtBoxImg1.Name = "m_txtBoxImg1";
            this.m_txtBoxImg1.ReadOnly = true;
            this.m_txtBoxImg1.Size = new System.Drawing.Size(163, 20);
            this.m_txtBoxImg1.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Released image";
            // 
            // chkBistable
            // 
            this.chkBistable.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkBistable.AutoSize = true;
            this.chkBistable.Location = new System.Drawing.Point(4, 88);
            this.chkBistable.Name = "chkBistable";
            this.chkBistable.Size = new System.Drawing.Size(54, 23);
            this.chkBistable.TabIndex = 9;
            this.chkBistable.Text = "Bistable";
            this.chkBistable.UseVisualStyleBackColor = true;
            // 
            // ImageButtonProperties
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.chkBistable);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.m_btnImg2);
            this.Controls.Add(this.m_btnImg1);
            this.Controls.Add(this.m_txtBoxImg2);
            this.Controls.Add(this.m_txtBoxImg1);
            this.Controls.Add(this.label1);
            this.Name = "ImageButtonProperties";
            this.Size = new System.Drawing.Size(208, 116);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button m_btnImg2;
        private System.Windows.Forms.Button m_btnImg1;
        private System.Windows.Forms.OpenFileDialog m_openFileDialog;
        private System.Windows.Forms.TextBox m_txtBoxImg2;
        private System.Windows.Forms.TextBox m_txtBoxImg1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkBistable;


    }
}

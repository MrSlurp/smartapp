namespace SoundPlayer
{
    partial class SoundPlayerProperties
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
            this.m_txtBoxFile = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.m_btnBrowse = new CommonLib.BrowseFileBtn();
            this.SuspendLayout();
            // 
            // m_txtBoxFile
            // 
            this.m_txtBoxFile.Location = new System.Drawing.Point(4, 21);
            this.m_txtBoxFile.Name = "m_txtBoxFile";
            this.m_txtBoxFile.ReadOnly = true;
            this.m_txtBoxFile.Size = new System.Drawing.Size(163, 20);
            this.m_txtBoxFile.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Sound File";
            // 
            // m_btnBrowse
            // 
            this.m_btnBrowse.Location = new System.Drawing.Point(173, 21);
            this.m_btnBrowse.Name = "m_btnBrowse";
            this.m_btnBrowse.Size = new System.Drawing.Size(34, 20);
            this.m_btnBrowse.TabIndex = 3;
            this.m_btnBrowse.Text = "...";
            this.m_btnBrowse.UseVisualStyleBackColor = true;
            this.m_btnBrowse.OnBrowseFrom += new CommonLib.BrowseFileBtn.BrowseFromEvent(this.m_btnBrowse_Click);
            // 
            // SoundPlayerProperties
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.m_txtBoxFile);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.m_btnBrowse);
            this.Name = "SoundPlayerProperties";
            this.Size = new System.Drawing.Size(208, 64);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox m_txtBoxFile;
        private System.Windows.Forms.Label label1;
        private CommonLib.BrowseFileBtn m_btnBrowse;

    }
}

namespace ScreenItemLocker
{
    partial class ScreenItemLockerProperties
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
            this.btncfg = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btncfg
            // 
            this.btncfg.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btncfg.Location = new System.Drawing.Point(3, 3);
            this.btncfg.Name = "btncfg";
            this.btncfg.Size = new System.Drawing.Size(202, 23);
            this.btncfg.TabIndex = 1;
            this.btncfg.Text = "Configure";
            this.btncfg.UseVisualStyleBackColor = true;
            this.btncfg.Click += new System.EventHandler(this.btncfg_Click);
            // 
            // ScreenItemLockerProperties
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btncfg);
            this.Name = "ScreenItemLockerProperties";
            this.Size = new System.Drawing.Size(208, 84);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btncfg;

    }
}

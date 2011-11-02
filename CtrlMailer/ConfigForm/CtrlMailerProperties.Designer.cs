namespace CtrlMailer
{
    partial class CtrlMailerProperties
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
            this.btnCfgMail = new System.Windows.Forms.Button();
            this.btnCfgSMTP = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnCfgMail
            // 
            this.btnCfgMail.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCfgMail.Location = new System.Drawing.Point(4, 4);
            this.btnCfgMail.Name = "btnCfgMail";
            this.btnCfgMail.Size = new System.Drawing.Size(201, 23);
            this.btnCfgMail.TabIndex = 0;
            this.btnCfgMail.Text = "Configure mail";
            this.btnCfgMail.UseVisualStyleBackColor = true;
            this.btnCfgMail.Click += new System.EventHandler(this.btnCfgMail_Click);
            // 
            // btnCfgSMTP
            // 
            this.btnCfgSMTP.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCfgSMTP.Location = new System.Drawing.Point(4, 33);
            this.btnCfgSMTP.Name = "btnCfgSMTP";
            this.btnCfgSMTP.Size = new System.Drawing.Size(201, 23);
            this.btnCfgSMTP.TabIndex = 0;
            this.btnCfgSMTP.Text = "Configure SMTP";
            this.btnCfgSMTP.UseVisualStyleBackColor = true;
            this.btnCfgSMTP.Click += new System.EventHandler(this.btnCfgSMTP_Click);
            // 
            // CtrlMailerProperties
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnCfgSMTP);
            this.Controls.Add(this.btnCfgMail);
            this.Name = "CtrlMailerProperties";
            this.Size = new System.Drawing.Size(208, 84);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCfgMail;
        private System.Windows.Forms.Button btnCfgSMTP;

    }
}

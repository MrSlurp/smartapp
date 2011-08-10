namespace PasswordControler
{
    partial class PasswordControlerProperties
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
            this.edtPassword1 = new System.Windows.Forms.TextBox();
            this.edtPassword2 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblPasswdExist = new System.Windows.Forms.Label();
            this.btnValidate = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // edtPassword1
            // 
            this.edtPassword1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.edtPassword1.Location = new System.Drawing.Point(0, 20);
            this.edtPassword1.Name = "edtPassword1";
            this.edtPassword1.PasswordChar = '*';
            this.edtPassword1.Size = new System.Drawing.Size(182, 20);
            this.edtPassword1.TabIndex = 0;
            // 
            // edtPassword2
            // 
            this.edtPassword2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.edtPassword2.Location = new System.Drawing.Point(0, 64);
            this.edtPassword2.Name = "edtPassword2";
            this.edtPassword2.PasswordChar = '*';
            this.edtPassword2.Size = new System.Drawing.Size(182, 20);
            this.edtPassword2.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Enter Password";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Confirm Password";
            // 
            // lblPasswdExist
            // 
            this.lblPasswdExist.AutoSize = true;
            this.lblPasswdExist.Location = new System.Drawing.Point(3, 116);
            this.lblPasswdExist.Name = "lblPasswdExist";
            this.lblPasswdExist.Size = new System.Drawing.Size(86, 13);
            this.lblPasswdExist.TabIndex = 2;
            this.lblPasswdExist.Text = "A password exist";
            // 
            // btnValidate
            // 
            this.btnValidate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btnValidate.Location = new System.Drawing.Point(0, 90);
            this.btnValidate.Name = "btnValidate";
            this.btnValidate.Size = new System.Drawing.Size(182, 23);
            this.btnValidate.TabIndex = 3;
            this.btnValidate.Text = "Validate password";
            this.btnValidate.UseVisualStyleBackColor = true;
            this.btnValidate.Click += new System.EventHandler(this.btnValidate_Click);
            // 
            // PasswordControlerProperties
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnValidate);
            this.Controls.Add(this.lblPasswdExist);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.edtPassword2);
            this.Controls.Add(this.edtPassword1);
            this.Name = "PasswordControlerProperties";
            this.Size = new System.Drawing.Size(185, 140);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox edtPassword1;
        private System.Windows.Forms.TextBox edtPassword2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblPasswdExist;
        private System.Windows.Forms.Button btnValidate;

    }
}

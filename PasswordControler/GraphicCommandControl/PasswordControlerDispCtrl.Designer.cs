namespace PasswordControler
{
    partial class PasswordControlerDispCtrl
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
            this.txtPasswd = new System.Windows.Forms.TextBox();
            this.btnLock = new System.Windows.Forms.Button();
            this.btnUnlock = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtPasswd
            // 
            this.txtPasswd.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPasswd.Location = new System.Drawing.Point(6, 5);
            this.txtPasswd.Name = "txtPasswd";
            this.txtPasswd.PasswordChar = '*';
            this.txtPasswd.Size = new System.Drawing.Size(225, 20);
            this.txtPasswd.TabIndex = 6;
            this.txtPasswd.Text = "blabla";
            // 
            // btnLock
            // 
            this.btnLock.Location = new System.Drawing.Point(131, 31);
            this.btnLock.Name = "btnLock";
            this.btnLock.Size = new System.Drawing.Size(100, 23);
            this.btnLock.TabIndex = 5;
            this.btnLock.Text = "Lock";
            this.btnLock.UseVisualStyleBackColor = true;
            this.btnLock.Click += new System.EventHandler(this.btnLock_Click);
            // 
            // btnUnlock
            // 
            this.btnUnlock.Location = new System.Drawing.Point(6, 31);
            this.btnUnlock.Name = "btnUnlock";
            this.btnUnlock.Size = new System.Drawing.Size(101, 23);
            this.btnUnlock.TabIndex = 4;
            this.btnUnlock.Text = "Unlock";
            this.btnUnlock.UseVisualStyleBackColor = true;
            this.btnUnlock.Click += new System.EventHandler(this.btnUnlock_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 62);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Invalid password";
            this.label1.Visible = false;
            // 
            // PasswordControlerDispCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtPasswd);
            this.Controls.Add(this.btnLock);
            this.Controls.Add(this.btnUnlock);
            this.Name = "PasswordControlerDispCtrl";
            this.Size = new System.Drawing.Size(236, 82);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtPasswd;
        private System.Windows.Forms.Button btnLock;
        private System.Windows.Forms.Button btnUnlock;
        private System.Windows.Forms.Label label1;

    }
}

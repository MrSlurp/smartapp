namespace PasswordControler
{
    partial class CmdPasswordControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnUnlock = new System.Windows.Forms.Button();
            this.btnLock = new System.Windows.Forms.Button();
            this.txtPasswd = new System.Windows.Forms.TextBox();
            this.picLock = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.picLock)).BeginInit();
            this.SuspendLayout();
            // 
            // btnUnlock
            // 
            this.btnUnlock.Location = new System.Drawing.Point(3, 29);
            this.btnUnlock.Name = "btnUnlock";
            this.btnUnlock.Size = new System.Drawing.Size(116, 23);
            this.btnUnlock.TabIndex = 0;
            this.btnUnlock.Text = "Unlock";
            this.btnUnlock.UseVisualStyleBackColor = true;
            // 
            // btnLock
            // 
            this.btnLock.Location = new System.Drawing.Point(3, 58);
            this.btnLock.Name = "btnLock";
            this.btnLock.Size = new System.Drawing.Size(116, 23);
            this.btnLock.TabIndex = 1;
            this.btnLock.Text = "Lock";
            this.btnLock.UseVisualStyleBackColor = true;
            // 
            // txtPasswd
            // 
            this.txtPasswd.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPasswd.Location = new System.Drawing.Point(3, 3);
            this.txtPasswd.Name = "txtPasswd";
            this.txtPasswd.PasswordChar = '*';
            this.txtPasswd.Size = new System.Drawing.Size(225, 20);
            this.txtPasswd.TabIndex = 3;
            this.txtPasswd.Text = "blabla";
            // 
            // picLock
            // 
            this.picLock.Location = new System.Drawing.Point(138, 29);
            this.picLock.Name = "picLock";
            this.picLock.Size = new System.Drawing.Size(85, 52);
            this.picLock.TabIndex = 4;
            this.picLock.TabStop = false;
            // 
            // UserControl1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.picLock);
            this.Controls.Add(this.txtPasswd);
            this.Controls.Add(this.btnLock);
            this.Controls.Add(this.btnUnlock);
            this.Name = "UserControl1";
            this.Size = new System.Drawing.Size(231, 89);
            ((System.ComponentModel.ISupportInitialize)(this.picLock)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnUnlock;
        private System.Windows.Forms.Button btnLock;
        private System.Windows.Forms.TextBox txtPasswd;
        private System.Windows.Forms.PictureBox picLock;
    }
}

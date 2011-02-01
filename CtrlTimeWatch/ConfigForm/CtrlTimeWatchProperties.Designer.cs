namespace CtrlTimeWatch
{
    partial class CtrlTimeWatchProperties
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
            this.btnPickMinutes = new System.Windows.Forms.Button();
            this.btnPickHours = new System.Windows.Forms.Button();
            this.edtMinutes = new System.Windows.Forms.TextBox();
            this.edtHours = new System.Windows.Forms.TextBox();
            this.edtSeconds = new System.Windows.Forms.TextBox();
            this.btnPickSecond = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnPickMinutes
            // 
            this.btnPickMinutes.Location = new System.Drawing.Point(219, 55);
            this.btnPickMinutes.Name = "btnPickMinutes";
            this.btnPickMinutes.Size = new System.Drawing.Size(57, 23);
            this.btnPickMinutes.TabIndex = 9;
            this.btnPickMinutes.Text = "Pick";
            this.btnPickMinutes.UseVisualStyleBackColor = true;
            this.btnPickMinutes.Click += new System.EventHandler(this.btnPickMinutes_Click);
            // 
            // btnPickHours
            // 
            this.btnPickHours.Location = new System.Drawing.Point(218, 14);
            this.btnPickHours.Name = "btnPickHours";
            this.btnPickHours.Size = new System.Drawing.Size(58, 23);
            this.btnPickHours.TabIndex = 8;
            this.btnPickHours.Text = "Pick";
            this.btnPickHours.UseVisualStyleBackColor = true;
            this.btnPickHours.Click += new System.EventHandler(this.btnPickHours_Click);
            // 
            // edtMinutes
            // 
            this.edtMinutes.Location = new System.Drawing.Point(3, 55);
            this.edtMinutes.Name = "edtMinutes";
            this.edtMinutes.Size = new System.Drawing.Size(209, 20);
            this.edtMinutes.TabIndex = 6;
            // 
            // edtHours
            // 
            this.edtHours.Location = new System.Drawing.Point(3, 16);
            this.edtHours.Name = "edtHours";
            this.edtHours.Size = new System.Drawing.Size(209, 20);
            this.edtHours.TabIndex = 7;
            // 
            // edtSeconds
            // 
            this.edtSeconds.Location = new System.Drawing.Point(3, 94);
            this.edtSeconds.Name = "edtSeconds";
            this.edtSeconds.Size = new System.Drawing.Size(209, 20);
            this.edtSeconds.TabIndex = 6;
            // 
            // btnPickSecond
            // 
            this.btnPickSecond.Location = new System.Drawing.Point(219, 94);
            this.btnPickSecond.Name = "btnPickSecond";
            this.btnPickSecond.Size = new System.Drawing.Size(57, 23);
            this.btnPickSecond.TabIndex = 9;
            this.btnPickSecond.Text = "Pick";
            this.btnPickSecond.UseVisualStyleBackColor = true;
            this.btnPickSecond.Click += new System.EventHandler(this.btnPickSecond_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Hours data";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Minutes data";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 78);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Seconds data";
            // 
            // CtrlTimeWatchProperties
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnPickSecond);
            this.Controls.Add(this.btnPickMinutes);
            this.Controls.Add(this.edtSeconds);
            this.Controls.Add(this.btnPickHours);
            this.Controls.Add(this.edtMinutes);
            this.Controls.Add(this.edtHours);
            this.Name = "CtrlTimeWatchProperties";
            this.Size = new System.Drawing.Size(280, 127);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnPickMinutes;
        private System.Windows.Forms.Button btnPickHours;
        private System.Windows.Forms.TextBox edtMinutes;
        private System.Windows.Forms.TextBox edtHours;
        private System.Windows.Forms.TextBox edtSeconds;
        private System.Windows.Forms.Button btnPickSecond;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;

    }
}

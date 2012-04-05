namespace CtrlCnxManager
{
    partial class CtrlCnxManagerProperties
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
            this.edtDelay = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.edtDelay)).BeginInit();
            this.SuspendLayout();
            // 
            // edtDelay
            // 
            this.edtDelay.Location = new System.Drawing.Point(4, 29);
            this.edtDelay.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.edtDelay.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.edtDelay.Name = "edtDelay";
            this.edtDelay.Size = new System.Drawing.Size(120, 20);
            this.edtDelay.TabIndex = 0;
            this.edtDelay.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(181, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Delay before trying to reconnect (mn)";
            // 
            // CtrlCnxManagerProperties
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.edtDelay);
            this.Name = "CtrlCnxManagerProperties";
            this.Size = new System.Drawing.Size(208, 58);
            ((System.ComponentModel.ISupportInitialize)(this.edtDelay)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown edtDelay;
        private System.Windows.Forms.Label label1;

    }
}

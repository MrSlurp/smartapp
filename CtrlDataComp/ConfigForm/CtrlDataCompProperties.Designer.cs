namespace CtrlDataComp
{
    partial class CtrlDataCompProperties
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
            this.btnPickB = new System.Windows.Forms.Button();
            this.btnPickA = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.edtDataB = new System.Windows.Forms.TextBox();
            this.edtDataA = new System.Windows.Forms.TextBox();
            this.rdoASupB = new System.Windows.Forms.RadioButton();
            this.rdoAInfB = new System.Windows.Forms.RadioButton();
            this.rdoASupBSupC = new System.Windows.Forms.RadioButton();
            this.rdoASupEqB = new System.Windows.Forms.RadioButton();
            this.rdoASupEqBSupEqC = new System.Windows.Forms.RadioButton();
            this.rdoAInfEqB = new System.Windows.Forms.RadioButton();
            this.edtDataC = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnPickC = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnPickB
            // 
            this.btnPickB.Location = new System.Drawing.Point(220, 191);
            this.btnPickB.Name = "btnPickB";
            this.btnPickB.Size = new System.Drawing.Size(57, 23);
            this.btnPickB.TabIndex = 12;
            this.btnPickB.Text = "Pick";
            this.btnPickB.UseVisualStyleBackColor = true;
            this.btnPickB.Click += new System.EventHandler(this.btnPickB_Click);
            // 
            // btnPickA
            // 
            this.btnPickA.Location = new System.Drawing.Point(219, 150);
            this.btnPickA.Name = "btnPickA";
            this.btnPickA.Size = new System.Drawing.Size(58, 23);
            this.btnPickA.TabIndex = 11;
            this.btnPickA.Text = "Pick";
            this.btnPickA.UseVisualStyleBackColor = true;
            this.btnPickA.Click += new System.EventHandler(this.btnPickA_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 175);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(126, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "b (can be integer or data)";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 136);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(126, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "a (can be integer or data)";
            // 
            // edtDataB
            // 
            this.edtDataB.Location = new System.Drawing.Point(4, 191);
            this.edtDataB.Name = "edtDataB";
            this.edtDataB.Size = new System.Drawing.Size(209, 20);
            this.edtDataB.TabIndex = 7;
            // 
            // edtDataA
            // 
            this.edtDataA.Location = new System.Drawing.Point(4, 152);
            this.edtDataA.Name = "edtDataA";
            this.edtDataA.Size = new System.Drawing.Size(209, 20);
            this.edtDataA.TabIndex = 8;
            // 
            // rdoASupB
            // 
            this.rdoASupB.Appearance = System.Windows.Forms.Appearance.Button;
            this.rdoASupB.Location = new System.Drawing.Point(6, 19);
            this.rdoASupB.Name = "rdoASupB";
            this.rdoASupB.Size = new System.Drawing.Size(75, 43);
            this.rdoASupB.TabIndex = 13;
            this.rdoASupB.TabStop = true;
            this.rdoASupB.Text = "a > b";
            this.rdoASupB.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rdoASupB.UseVisualStyleBackColor = true;
            this.rdoASupB.CheckedChanged += new System.EventHandler(this.rdoCompMode_CheckedChanged);
            // 
            // rdoAInfB
            // 
            this.rdoAInfB.Appearance = System.Windows.Forms.Appearance.Button;
            this.rdoAInfB.Location = new System.Drawing.Point(87, 19);
            this.rdoAInfB.Name = "rdoAInfB";
            this.rdoAInfB.Size = new System.Drawing.Size(75, 43);
            this.rdoAInfB.TabIndex = 14;
            this.rdoAInfB.TabStop = true;
            this.rdoAInfB.Text = "a < b";
            this.rdoAInfB.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rdoAInfB.UseVisualStyleBackColor = true;
            this.rdoAInfB.CheckedChanged += new System.EventHandler(this.rdoCompMode_CheckedChanged);
            // 
            // rdoASupBSupC
            // 
            this.rdoASupBSupC.Appearance = System.Windows.Forms.Appearance.Button;
            this.rdoASupBSupC.Location = new System.Drawing.Point(168, 19);
            this.rdoASupBSupC.Name = "rdoASupBSupC";
            this.rdoASupBSupC.Size = new System.Drawing.Size(75, 43);
            this.rdoASupBSupC.TabIndex = 15;
            this.rdoASupBSupC.TabStop = true;
            this.rdoASupBSupC.Text = "a > b > c";
            this.rdoASupBSupC.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rdoASupBSupC.UseVisualStyleBackColor = true;
            this.rdoASupBSupC.CheckedChanged += new System.EventHandler(this.rdoCompMode_CheckedChanged);
            // 
            // rdoASupEqB
            // 
            this.rdoASupEqB.Appearance = System.Windows.Forms.Appearance.Button;
            this.rdoASupEqB.Location = new System.Drawing.Point(6, 68);
            this.rdoASupEqB.Name = "rdoASupEqB";
            this.rdoASupEqB.Size = new System.Drawing.Size(75, 43);
            this.rdoASupEqB.TabIndex = 16;
            this.rdoASupEqB.TabStop = true;
            this.rdoASupEqB.Text = "a >= b";
            this.rdoASupEqB.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rdoASupEqB.UseVisualStyleBackColor = true;
            this.rdoASupEqB.CheckedChanged += new System.EventHandler(this.rdoCompMode_CheckedChanged);
            // 
            // rdoASupEqBSupEqC
            // 
            this.rdoASupEqBSupEqC.Appearance = System.Windows.Forms.Appearance.Button;
            this.rdoASupEqBSupEqC.Location = new System.Drawing.Point(168, 68);
            this.rdoASupEqBSupEqC.Name = "rdoASupEqBSupEqC";
            this.rdoASupEqBSupEqC.Size = new System.Drawing.Size(75, 43);
            this.rdoASupEqBSupEqC.TabIndex = 18;
            this.rdoASupEqBSupEqC.TabStop = true;
            this.rdoASupEqBSupEqC.Text = "a >= b >= c";
            this.rdoASupEqBSupEqC.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rdoASupEqBSupEqC.UseVisualStyleBackColor = true;
            this.rdoASupEqBSupEqC.CheckedChanged += new System.EventHandler(this.rdoCompMode_CheckedChanged);
            // 
            // rdoAInfEqB
            // 
            this.rdoAInfEqB.Appearance = System.Windows.Forms.Appearance.Button;
            this.rdoAInfEqB.Location = new System.Drawing.Point(87, 68);
            this.rdoAInfEqB.Name = "rdoAInfEqB";
            this.rdoAInfEqB.Size = new System.Drawing.Size(75, 43);
            this.rdoAInfEqB.TabIndex = 17;
            this.rdoAInfEqB.TabStop = true;
            this.rdoAInfEqB.Text = "a <= b";
            this.rdoAInfEqB.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rdoAInfEqB.UseVisualStyleBackColor = true;
            this.rdoAInfEqB.CheckedChanged += new System.EventHandler(this.rdoCompMode_CheckedChanged);
            // 
            // edtDataC
            // 
            this.edtDataC.Location = new System.Drawing.Point(4, 230);
            this.edtDataC.Name = "edtDataC";
            this.edtDataC.Size = new System.Drawing.Size(209, 20);
            this.edtDataC.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 214);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(126, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "c (can be integer or data)";
            // 
            // btnPickC
            // 
            this.btnPickC.Location = new System.Drawing.Point(220, 230);
            this.btnPickC.Name = "btnPickC";
            this.btnPickC.Size = new System.Drawing.Size(57, 23);
            this.btnPickC.TabIndex = 12;
            this.btnPickC.Text = "Pick";
            this.btnPickC.UseVisualStyleBackColor = true;
            this.btnPickC.Click += new System.EventHandler(this.btnPickC_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rdoASupBSupC);
            this.groupBox1.Controls.Add(this.rdoASupEqBSupEqC);
            this.groupBox1.Controls.Add(this.rdoASupB);
            this.groupBox1.Controls.Add(this.rdoAInfB);
            this.groupBox1.Controls.Add(this.rdoASupEqB);
            this.groupBox1.Controls.Add(this.rdoAInfEqB);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(249, 120);
            this.groupBox1.TabIndex = 16;
            this.groupBox1.TabStop = false;
            // 
            // CtrlDataCompProperties
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnPickC);
            this.Controls.Add(this.btnPickB);
            this.Controls.Add(this.btnPickA);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.edtDataC);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.edtDataB);
            this.Controls.Add(this.edtDataA);
            this.Name = "CtrlDataCompProperties";
            this.Size = new System.Drawing.Size(293, 268);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnPickB;
        private System.Windows.Forms.Button btnPickA;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox edtDataB;
        private System.Windows.Forms.TextBox edtDataA;
        private System.Windows.Forms.RadioButton rdoASupB;
        private System.Windows.Forms.RadioButton rdoAInfB;
        private System.Windows.Forms.RadioButton rdoASupBSupC;
        private System.Windows.Forms.RadioButton rdoASupEqB;
        private System.Windows.Forms.RadioButton rdoASupEqBSupEqC;
        private System.Windows.Forms.RadioButton rdoAInfEqB;
        private System.Windows.Forms.TextBox edtDataC;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnPickC;
        private System.Windows.Forms.GroupBox groupBox1;

    }
}

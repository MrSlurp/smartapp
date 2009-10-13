namespace CtrlDataTrigger
{
    partial class CtrlDataTriggerProperties
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
            this.cbxSchmitt = new System.Windows.Forms.CheckBox();
            this.edtOnToOff = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.edtOffToOn = new System.Windows.Forms.TextBox();
            this.btnOnToOffScript = new System.Windows.Forms.Button();
            this.btnOffToOnScript = new System.Windows.Forms.Button();
            this.btnPickOnToOff = new System.Windows.Forms.Button();
            this.btnPickOffToOn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cbxSchmitt
            // 
            this.cbxSchmitt.AutoSize = true;
            this.cbxSchmitt.Location = new System.Drawing.Point(4, 4);
            this.cbxSchmitt.Name = "cbxSchmitt";
            this.cbxSchmitt.Size = new System.Drawing.Size(156, 17);
            this.cbxSchmitt.TabIndex = 0;
            this.cbxSchmitt.Text = "Behave like Schmitt Trigger";
            this.cbxSchmitt.UseVisualStyleBackColor = true;
            this.cbxSchmitt.CheckedChanged += new System.EventHandler(this.cbxSchmitt_CheckedChanged);
            // 
            // edtOnToOff
            // 
            this.edtOnToOff.Location = new System.Drawing.Point(4, 40);
            this.edtOnToOff.Name = "edtOnToOff";
            this.edtOnToOff.Size = new System.Drawing.Size(209, 20);
            this.edtOnToOff.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(201, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "On to off setpoint (can be integer or data)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(201, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Off to on setpoint (can be integer or data)";
            // 
            // edtOffToOn
            // 
            this.edtOffToOn.Location = new System.Drawing.Point(4, 79);
            this.edtOffToOn.Name = "edtOffToOn";
            this.edtOffToOn.Size = new System.Drawing.Size(209, 20);
            this.edtOffToOn.TabIndex = 1;
            // 
            // btnOnToOffScript
            // 
            this.btnOnToOffScript.Location = new System.Drawing.Point(4, 106);
            this.btnOnToOffScript.Name = "btnOnToOffScript";
            this.btnOnToOffScript.Size = new System.Drawing.Size(95, 23);
            this.btnOnToOffScript.TabIndex = 3;
            this.btnOnToOffScript.Text = "On to off Script";
            this.btnOnToOffScript.UseVisualStyleBackColor = true;
            this.btnOnToOffScript.Click += new System.EventHandler(this.btnOnToOffScript_Click);
            // 
            // btnOffToOnScript
            // 
            this.btnOffToOnScript.Location = new System.Drawing.Point(105, 105);
            this.btnOffToOnScript.Name = "btnOffToOnScript";
            this.btnOffToOnScript.Size = new System.Drawing.Size(95, 23);
            this.btnOffToOnScript.TabIndex = 3;
            this.btnOffToOnScript.Text = "Off to on Script";
            this.btnOffToOnScript.UseVisualStyleBackColor = true;
            this.btnOffToOnScript.Click += new System.EventHandler(this.btnOffToOnScript_Click);
            // 
            // btnPickOnToOff
            // 
            this.btnPickOnToOff.Location = new System.Drawing.Point(219, 35);
            this.btnPickOnToOff.Name = "btnPickOnToOff";
            this.btnPickOnToOff.Size = new System.Drawing.Size(38, 23);
            this.btnPickOnToOff.TabIndex = 4;
            this.btnPickOnToOff.Text = "Pick";
            this.btnPickOnToOff.UseVisualStyleBackColor = true;
            this.btnPickOnToOff.Click += new System.EventHandler(this.btnPickOnToOff_Click);
            // 
            // btnPickOffToOn
            // 
            this.btnPickOffToOn.Location = new System.Drawing.Point(220, 76);
            this.btnPickOffToOn.Name = "btnPickOffToOn";
            this.btnPickOffToOn.Size = new System.Drawing.Size(38, 23);
            this.btnPickOffToOn.TabIndex = 4;
            this.btnPickOffToOn.Text = "Pick";
            this.btnPickOffToOn.UseVisualStyleBackColor = true;
            this.btnPickOffToOn.Click += new System.EventHandler(this.btnPickOffToOn_Click);
            // 
            // CtrlDataTriggerProperties
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnPickOffToOn);
            this.Controls.Add(this.btnPickOnToOff);
            this.Controls.Add(this.btnOffToOnScript);
            this.Controls.Add(this.btnOnToOffScript);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.edtOffToOn);
            this.Controls.Add(this.edtOnToOff);
            this.Controls.Add(this.cbxSchmitt);
            this.Name = "CtrlDataTriggerProperties";
            this.Size = new System.Drawing.Size(258, 139);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox cbxSchmitt;
        private System.Windows.Forms.TextBox edtOnToOff;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox edtOffToOn;
        private System.Windows.Forms.Button btnOnToOffScript;
        private System.Windows.Forms.Button btnOffToOnScript;
        private System.Windows.Forms.Button btnPickOnToOff;
        private System.Windows.Forms.Button btnPickOffToOn;

    }
}

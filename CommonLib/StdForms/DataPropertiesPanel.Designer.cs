namespace CommonLib
{
    partial class DataPropertiesPanel
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
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.m_numUDDefault = new System.Windows.Forms.NumericUpDown();
            this.m_checkConstant = new System.Windows.Forms.CheckBox();
            this.m_numUDMax = new System.Windows.Forms.NumericUpDown();
            this.m_numUDMin = new System.Windows.Forms.NumericUpDown();
            this.m_labelMin = new System.Windows.Forms.Label();
            this.m_cboSize = new System.Windows.Forms.ComboBox();
            this.m_labelSize = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.m_numUDDefault)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_numUDMax)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_numUDMin)).BeginInit();
            this.SuspendLayout();
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 90);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(27, 13);
            this.label5.TabIndex = 1;
            this.label5.Text = "Max";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 129);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 13);
            this.label6.TabIndex = 1;
            this.label6.Text = "Default";
            // 
            // m_numUDDefault
            // 
            this.m_numUDDefault.Location = new System.Drawing.Point(6, 145);
            this.m_numUDDefault.Name = "m_numUDDefault";
            this.m_numUDDefault.Size = new System.Drawing.Size(110, 20);
            this.m_numUDDefault.TabIndex = 4;
            // 
            // m_checkConstant
            // 
            this.m_checkConstant.AutoSize = true;
            this.m_checkConstant.Location = new System.Drawing.Point(6, 172);
            this.m_checkConstant.Name = "m_checkConstant";
            this.m_checkConstant.Size = new System.Drawing.Size(68, 17);
            this.m_checkConstant.TabIndex = 5;
            this.m_checkConstant.Text = "Constant";
            this.m_checkConstant.UseVisualStyleBackColor = true;
            // 
            // m_numUDMax
            // 
            this.m_numUDMax.Location = new System.Drawing.Point(6, 106);
            this.m_numUDMax.Name = "m_numUDMax";
            this.m_numUDMax.Size = new System.Drawing.Size(110, 20);
            this.m_numUDMax.TabIndex = 4;
            // 
            // m_numUDMin
            // 
            this.m_numUDMin.Location = new System.Drawing.Point(6, 65);
            this.m_numUDMin.Name = "m_numUDMin";
            this.m_numUDMin.Size = new System.Drawing.Size(110, 20);
            this.m_numUDMin.TabIndex = 4;
            // 
            // m_labelMin
            // 
            this.m_labelMin.AutoSize = true;
            this.m_labelMin.Location = new System.Drawing.Point(3, 49);
            this.m_labelMin.Name = "m_labelMin";
            this.m_labelMin.Size = new System.Drawing.Size(24, 13);
            this.m_labelMin.TabIndex = 1;
            this.m_labelMin.Text = "Min";
            // 
            // m_cboSize
            // 
            this.m_cboSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cboSize.FormattingEnabled = true;
            this.m_cboSize.Location = new System.Drawing.Point(3, 16);
            this.m_cboSize.Name = "m_cboSize";
            this.m_cboSize.Size = new System.Drawing.Size(192, 21);
            this.m_cboSize.TabIndex = 3;
            this.m_cboSize.SelectedIndexChanged += new System.EventHandler(this.OnSizeSelectedIndexChanged);
            // 
            // m_labelSize
            // 
            this.m_labelSize.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_labelSize.AutoSize = true;
            this.m_labelSize.Location = new System.Drawing.Point(3, 0);
            this.m_labelSize.Name = "m_labelSize";
            this.m_labelSize.Size = new System.Drawing.Size(27, 13);
            this.m_labelSize.TabIndex = 1;
            this.m_labelSize.Text = "Size";
            // 
            // DataPropertiesControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.m_checkConstant);
            this.Controls.Add(this.m_numUDDefault);
            this.Controls.Add(this.m_numUDMax);
            this.Controls.Add(this.m_numUDMin);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.m_cboSize);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.m_labelMin);
            this.Controls.Add(this.m_labelSize);
            this.Name = "DataPropertiesControl";
            this.Size = new System.Drawing.Size(202, 206);
            ((System.ComponentModel.ISupportInitialize)(this.m_numUDDefault)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_numUDMax)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_numUDMin)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown m_numUDDefault;
        private System.Windows.Forms.CheckBox m_checkConstant;
        private System.Windows.Forms.NumericUpDown m_numUDMax;
        private System.Windows.Forms.NumericUpDown m_numUDMin;
        private System.Windows.Forms.Label m_labelMin;
        private System.Windows.Forms.ComboBox m_cboSize;
        private System.Windows.Forms.Label m_labelSize;
    }
}

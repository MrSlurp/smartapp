namespace ImageButton
{
    partial class ImageButtonProperties
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
            this.label2 = new System.Windows.Forms.Label();
            this.m_btnImg2 = new CommonLib.BrowseFileBtn();
            this.m_btnImg1 = new CommonLib.BrowseFileBtn();
            this.m_txtBoxImg2 = new System.Windows.Forms.TextBox();
            this.m_txtBoxImg1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.chkBistable = new System.Windows.Forms.CheckBox();
            this.cboStyle = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnPickInput = new System.Windows.Forms.Button();
            this.edtInputData = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Pressed image";
            // 
            // m_btnImg2
            // 
            this.m_btnImg2.Location = new System.Drawing.Point(266, 61);
            this.m_btnImg2.Name = "m_btnImg2";
            this.m_btnImg2.Size = new System.Drawing.Size(34, 20);
            this.m_btnImg2.TabIndex = 4;
            this.m_btnImg2.Text = "...";
            this.m_btnImg2.UseVisualStyleBackColor = true;
            this.m_btnImg2.OnBrowseFrom += new CommonLib.BrowseFileBtn.BrowseFromEvent(this.m_btnImg2_Click);
            // 
            // m_btnImg1
            // 
            this.m_btnImg1.Location = new System.Drawing.Point(266, 20);
            this.m_btnImg1.Name = "m_btnImg1";
            this.m_btnImg1.Size = new System.Drawing.Size(34, 20);
            this.m_btnImg1.TabIndex = 3;
            this.m_btnImg1.Text = "...";
            this.m_btnImg1.UseVisualStyleBackColor = true;
            this.m_btnImg1.OnBrowseFrom += new CommonLib.BrowseFileBtn.BrowseFromEvent(this.m_btnImg1_Click);
            // 
            // m_txtBoxImg2
            // 
            this.m_txtBoxImg2.Location = new System.Drawing.Point(10, 61);
            this.m_txtBoxImg2.Name = "m_txtBoxImg2";
            this.m_txtBoxImg2.ReadOnly = true;
            this.m_txtBoxImg2.Size = new System.Drawing.Size(250, 20);
            this.m_txtBoxImg2.TabIndex = 7;
            this.m_txtBoxImg2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_txtBoxImg2_KeyDown);
            // 
            // m_txtBoxImg1
            // 
            this.m_txtBoxImg1.Location = new System.Drawing.Point(10, 20);
            this.m_txtBoxImg1.Name = "m_txtBoxImg1";
            this.m_txtBoxImg1.ReadOnly = true;
            this.m_txtBoxImg1.Size = new System.Drawing.Size(250, 20);
            this.m_txtBoxImg1.TabIndex = 8;
            this.m_txtBoxImg1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_txtBoxImg1_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Released image";
            // 
            // chkBistable
            // 
            this.chkBistable.AutoSize = true;
            this.chkBistable.Location = new System.Drawing.Point(4, 88);
            this.chkBistable.Name = "chkBistable";
            this.chkBistable.Size = new System.Drawing.Size(63, 17);
            this.chkBistable.TabIndex = 9;
            this.chkBistable.Text = "Bistable";
            this.chkBistable.UseVisualStyleBackColor = true;
            // 
            // cboStyle
            // 
            this.cboStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboStyle.FormattingEnabled = true;
            this.cboStyle.Location = new System.Drawing.Point(4, 131);
            this.cboStyle.Name = "cboStyle";
            this.cboStyle.Size = new System.Drawing.Size(121, 21);
            this.cboStyle.TabIndex = 10;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(4, 112);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "Appearence";
            // 
            // btnPickInput
            // 
            this.btnPickInput.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPickInput.Location = new System.Drawing.Point(306, 180);
            this.btnPickInput.Name = "btnPickInput";
            this.btnPickInput.Size = new System.Drawing.Size(58, 23);
            this.btnPickInput.TabIndex = 13;
            this.btnPickInput.Text = "Pick";
            this.btnPickInput.UseVisualStyleBackColor = true;
            this.btnPickInput.Click += new System.EventHandler(this.btnPickInput_Click);
            // 
            // edtInputData
            // 
            this.edtInputData.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.edtInputData.Location = new System.Drawing.Point(4, 182);
            this.edtInputData.Name = "edtInputData";
            this.edtInputData.ReadOnly = true;
            this.edtInputData.Size = new System.Drawing.Size(296, 20);
            this.edtInputData.TabIndex = 12;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 162);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(157, 13);
            this.label4.TabIndex = 14;
            this.label4.Text = "Input state data (not mandatory)";
            // 
            // ImageButtonProperties
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnPickInput);
            this.Controls.Add(this.edtInputData);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cboStyle);
            this.Controls.Add(this.chkBistable);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.m_btnImg2);
            this.Controls.Add(this.m_btnImg1);
            this.Controls.Add(this.m_txtBoxImg2);
            this.Controls.Add(this.m_txtBoxImg1);
            this.Controls.Add(this.label1);
            this.Name = "ImageButtonProperties";
            this.Size = new System.Drawing.Size(367, 210);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private CommonLib.BrowseFileBtn m_btnImg2;
        private CommonLib.BrowseFileBtn m_btnImg1;
        private System.Windows.Forms.TextBox m_txtBoxImg2;
        private System.Windows.Forms.TextBox m_txtBoxImg1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkBistable;
        private System.Windows.Forms.ComboBox cboStyle;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnPickInput;
        private System.Windows.Forms.TextBox edtInputData;
        private System.Windows.Forms.Label label4;


    }
}

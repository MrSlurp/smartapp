namespace CommonLib
{
    partial class DocumentProprtiesDialog
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupStyle = new System.Windows.Forms.GroupBox();
            this.chkShowInTaskBar = new System.Windows.Forms.CheckBox();
            this.chkShowTitleBar = new System.Windows.Forms.CheckBox();
            this.groupPos = new System.Windows.Forms.GroupBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.edtPosX = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.edtPosY = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.edtSizeW = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.edtSizeH = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.chkDocumentUseMainContainer = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.edtProjectCnx = new System.Windows.Forms.TextBox();
            this.btnCfgCom = new System.Windows.Forms.Button();
            this.txtMainContainerTitle = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupStyle.SuspendLayout();
            this.groupPos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.edtPosX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.edtPosY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.edtSizeW)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.edtSizeH)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(420, 53);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(80, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(420, 24);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(80, 23);
            this.btnOK.TabIndex = 4;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Location = new System.Drawing.Point(-1, 2);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(415, 390);
            this.tabControl1.TabIndex = 2;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(407, 364);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Globals properties";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.txtMainContainerTitle);
            this.groupBox2.Controls.Add(this.groupStyle);
            this.groupBox2.Controls.Add(this.groupPos);
            this.groupBox2.Controls.Add(this.chkDocumentUseMainContainer);
            this.groupBox2.Location = new System.Drawing.Point(9, 74);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(392, 282);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Main container configuration";
            // 
            // groupStyle
            // 
            this.groupStyle.Controls.Add(this.chkShowInTaskBar);
            this.groupStyle.Controls.Add(this.chkShowTitleBar);
            this.groupStyle.Location = new System.Drawing.Point(178, 86);
            this.groupStyle.Name = "groupStyle";
            this.groupStyle.Size = new System.Drawing.Size(197, 69);
            this.groupStyle.TabIndex = 17;
            this.groupStyle.TabStop = false;
            this.groupStyle.Text = "Visual style";
            // 
            // chkShowInTaskBar
            // 
            this.chkShowInTaskBar.AutoSize = true;
            this.chkShowInTaskBar.Location = new System.Drawing.Point(6, 19);
            this.chkShowInTaskBar.Name = "chkShowInTaskBar";
            this.chkShowInTaskBar.Size = new System.Drawing.Size(102, 17);
            this.chkShowInTaskBar.TabIndex = 14;
            this.chkShowInTaskBar.Text = "Show in taskbar";
            this.chkShowInTaskBar.UseVisualStyleBackColor = true;
            // 
            // chkShowTitleBar
            // 
            this.chkShowTitleBar.AutoSize = true;
            this.chkShowTitleBar.Location = new System.Drawing.Point(6, 45);
            this.chkShowTitleBar.Name = "chkShowTitleBar";
            this.chkShowTitleBar.Size = new System.Drawing.Size(90, 17);
            this.chkShowTitleBar.TabIndex = 14;
            this.chkShowTitleBar.Text = "Show title bar";
            this.chkShowTitleBar.UseVisualStyleBackColor = true;
            // 
            // groupPos
            // 
            this.groupPos.Controls.Add(this.label9);
            this.groupPos.Controls.Add(this.label1);
            this.groupPos.Controls.Add(this.edtPosX);
            this.groupPos.Controls.Add(this.label2);
            this.groupPos.Controls.Add(this.edtPosY);
            this.groupPos.Controls.Add(this.label8);
            this.groupPos.Controls.Add(this.edtSizeW);
            this.groupPos.Controls.Add(this.label6);
            this.groupPos.Controls.Add(this.edtSizeH);
            this.groupPos.Controls.Add(this.label7);
            this.groupPos.Controls.Add(this.label5);
            this.groupPos.Location = new System.Drawing.Point(7, 86);
            this.groupPos.Name = "groupPos";
            this.groupPos.Size = new System.Drawing.Size(165, 181);
            this.groupPos.TabIndex = 16;
            this.groupPos.TabStop = false;
            this.groupPos.Text = "Position and size";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(4, 162);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(99, 13);
            this.label9.TabIndex = 14;
            this.label9.Text = "-1 means automatic";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "Screen position";
            // 
            // edtPosX
            // 
            this.edtPosX.Location = new System.Drawing.Point(69, 37);
            this.edtPosX.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.edtPosX.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.edtPosX.Name = "edtPosX";
            this.edtPosX.Size = new System.Drawing.Size(65, 20);
            this.edtPosX.TabIndex = 12;
            this.edtPosX.Value = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 92);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "Screen size";
            // 
            // edtPosY
            // 
            this.edtPosY.Location = new System.Drawing.Point(69, 63);
            this.edtPosY.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.edtPosY.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.edtPosY.Name = "edtPosY";
            this.edtPosY.Size = new System.Drawing.Size(65, 20);
            this.edtPosY.TabIndex = 12;
            this.edtPosY.Value = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(10, 135);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(36, 13);
            this.label8.TabIndex = 13;
            this.label8.Text = "height";
            // 
            // edtSizeW
            // 
            this.edtSizeW.Location = new System.Drawing.Point(69, 108);
            this.edtSizeW.Maximum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.edtSizeW.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.edtSizeW.Name = "edtSizeW";
            this.edtSizeW.Size = new System.Drawing.Size(65, 20);
            this.edtSizeW.TabIndex = 12;
            this.edtSizeW.Value = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(10, 65);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(12, 13);
            this.label6.TabIndex = 13;
            this.label6.Text = "y";
            // 
            // edtSizeH
            // 
            this.edtSizeH.Location = new System.Drawing.Point(69, 133);
            this.edtSizeH.Maximum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.edtSizeH.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.edtSizeH.Name = "edtSizeH";
            this.edtSizeH.Size = new System.Drawing.Size(65, 20);
            this.edtSizeH.TabIndex = 12;
            this.edtSizeH.Value = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(10, 110);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(32, 13);
            this.label7.TabIndex = 13;
            this.label7.Text = "width";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 39);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(12, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "x";
            // 
            // chkDocumentUseMainContainer
            // 
            this.chkDocumentUseMainContainer.AutoSize = true;
            this.chkDocumentUseMainContainer.Location = new System.Drawing.Point(7, 20);
            this.chkDocumentUseMainContainer.Name = "chkDocumentUseMainContainer";
            this.chkDocumentUseMainContainer.Size = new System.Drawing.Size(283, 17);
            this.chkDocumentUseMainContainer.TabIndex = 0;
            this.chkDocumentUseMainContainer.Text = "Open all project\'s screens in a single container window";
            this.chkDocumentUseMainContainer.UseVisualStyleBackColor = true;
            this.chkDocumentUseMainContainer.CheckedChanged += new System.EventHandler(this.chkDocumentUseMainContainer_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.edtProjectCnx);
            this.groupBox1.Controls.Add(this.btnCfgCom);
            this.groupBox1.Location = new System.Drawing.Point(9, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(392, 62);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Connexion Info";
            // 
            // edtProjectCnx
            // 
            this.edtProjectCnx.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.edtProjectCnx.Location = new System.Drawing.Point(7, 22);
            this.edtProjectCnx.Name = "edtProjectCnx";
            this.edtProjectCnx.ReadOnly = true;
            this.edtProjectCnx.Size = new System.Drawing.Size(242, 20);
            this.edtProjectCnx.TabIndex = 1;
            // 
            // btnCfgCom
            // 
            this.btnCfgCom.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCfgCom.Location = new System.Drawing.Point(255, 19);
            this.btnCfgCom.Name = "btnCfgCom";
            this.btnCfgCom.Size = new System.Drawing.Size(131, 23);
            this.btnCfgCom.TabIndex = 0;
            this.btnCfgCom.Text = "Configure / Select";
            this.btnCfgCom.UseVisualStyleBackColor = true;
            this.btnCfgCom.Click += new System.EventHandler(this.btnCfgCom_Click);
            // 
            // txtMainContainerTitle
            // 
            this.txtMainContainerTitle.Location = new System.Drawing.Point(7, 60);
            this.txtMainContainerTitle.Name = "txtMainContainerTitle";
            this.txtMainContainerTitle.Size = new System.Drawing.Size(328, 20);
            this.txtMainContainerTitle.TabIndex = 18;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 41);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(96, 13);
            this.label3.TabIndex = 19;
            this.label3.Text = "Main container title";
            // 
            // DocumentProprtiesDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(502, 392);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.tabControl1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DocumentProprtiesDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Project properties";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupStyle.ResumeLayout(false);
            this.groupStyle.PerformLayout();
            this.groupPos.ResumeLayout(false);
            this.groupPos.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.edtPosX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.edtPosY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.edtSizeW)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.edtSizeH)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnCfgCom;
        private System.Windows.Forms.TextBox edtProjectCnx;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox chkDocumentUseMainContainer;
        private System.Windows.Forms.GroupBox groupPos;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown edtPosX;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown edtPosY;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown edtSizeW;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown edtSizeH;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupStyle;
        private System.Windows.Forms.CheckBox chkShowInTaskBar;
        private System.Windows.Forms.CheckBox chkShowTitleBar;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtMainContainerTitle;

    }
}
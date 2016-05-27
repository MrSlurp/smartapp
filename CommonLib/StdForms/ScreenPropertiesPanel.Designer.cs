namespace CommonLib
{
    partial class ScreenPropertiesPanel
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
            this.label3 = new System.Windows.Forms.Label();
            this.m_textBoxTitle = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.m_textBkgndFile = new System.Windows.Forms.TextBox();
            this.m_btnBrowseBkFile = new CommonLib.BrowseFileBtn();
            this.m_btnRemoveFile = new System.Windows.Forms.Button();
            this.edtPosX = new System.Windows.Forms.NumericUpDown();
            this.edtPosY = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.edtSizeW = new System.Windows.Forms.NumericUpDown();
            this.edtSizeH = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.chkShowInTaskBar = new System.Windows.Forms.CheckBox();
            this.chkShowTitleBar = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label9 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.edtPosX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.edtPosY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.edtSizeW)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.edtSizeH)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(27, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Title";
            // 
            // m_textBoxTitle
            // 
            this.m_textBoxTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_textBoxTitle.Location = new System.Drawing.Point(0, 16);
            this.m_textBoxTitle.Name = "m_textBoxTitle";
            this.m_textBoxTitle.Size = new System.Drawing.Size(383, 20);
            this.m_textBoxTitle.TabIndex = 10;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 43);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(97, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Background Image";
            // 
            // m_textBkgndFile
            // 
            this.m_textBkgndFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_textBkgndFile.Location = new System.Drawing.Point(0, 59);
            this.m_textBkgndFile.Name = "m_textBkgndFile";
            this.m_textBkgndFile.ReadOnly = true;
            this.m_textBkgndFile.Size = new System.Drawing.Size(347, 20);
            this.m_textBkgndFile.TabIndex = 10;
            // 
            // m_btnBrowseBkFile
            // 
            this.m_btnBrowseBkFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnBrowseBkFile.Location = new System.Drawing.Point(353, 58);
            this.m_btnBrowseBkFile.Name = "m_btnBrowseBkFile";
            this.m_btnBrowseBkFile.Size = new System.Drawing.Size(30, 20);
            this.m_btnBrowseBkFile.TabIndex = 11;
            this.m_btnBrowseBkFile.Text = "...";
            this.m_btnBrowseBkFile.UseVisualStyleBackColor = true;
            this.m_btnBrowseBkFile.OnBrowseFrom += new CommonLib.BrowseFileBtn.BrowseFromEvent(this.m_btnBrowseBkFile_Click);
            // 
            // m_btnRemoveFile
            // 
            this.m_btnRemoveFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnRemoveFile.Location = new System.Drawing.Point(0, 85);
            this.m_btnRemoveFile.Name = "m_btnRemoveFile";
            this.m_btnRemoveFile.Size = new System.Drawing.Size(347, 23);
            this.m_btnRemoveFile.TabIndex = 11;
            this.m_btnRemoveFile.Text = "Remove background image";
            this.m_btnRemoveFile.UseVisualStyleBackColor = true;
            this.m_btnRemoveFile.Click += new System.EventHandler(this.m_btnRemoveFile_Click);
            // 
            // edtPosX
            // 
            this.edtPosX.Location = new System.Drawing.Point(65, 37);
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
            // edtPosY
            // 
            this.edtPosY.Location = new System.Drawing.Point(65, 63);
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
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "Screen position";
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
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 39);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(12, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "x";
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
            // edtSizeW
            // 
            this.edtSizeW.Location = new System.Drawing.Point(65, 108);
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
            // edtSizeH
            // 
            this.edtSizeH.Location = new System.Drawing.Point(65, 133);
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
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(10, 135);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(36, 13);
            this.label8.TabIndex = 13;
            this.label8.Text = "height";
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
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkShowInTaskBar);
            this.groupBox1.Controls.Add(this.chkShowTitleBar);
            this.groupBox1.Location = new System.Drawing.Point(163, 117);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(184, 69);
            this.groupBox1.TabIndex = 15;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Visual style";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.edtPosX);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.edtPosY);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.edtSizeW);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.edtSizeH);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Location = new System.Drawing.Point(0, 117);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(157, 181);
            this.groupBox2.TabIndex = 15;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Positions and size";
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
            // ScreenPropertiesPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.m_btnRemoveFile);
            this.Controls.Add(this.m_btnBrowseBkFile);
            this.Controls.Add(this.m_textBkgndFile);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.m_textBoxTitle);
            this.Controls.Add(this.label3);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "ScreenPropertiesPanel";
            this.Size = new System.Drawing.Size(383, 308);
            ((System.ComponentModel.ISupportInitialize)(this.edtPosX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.edtPosY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.edtSizeW)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.edtSizeH)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox m_textBoxTitle;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox m_textBkgndFile;
        private CommonLib.BrowseFileBtn m_btnBrowseBkFile;
        private System.Windows.Forms.Button m_btnRemoveFile;
        private System.Windows.Forms.NumericUpDown edtPosX;
        private System.Windows.Forms.NumericUpDown edtPosY;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown edtSizeW;
        private System.Windows.Forms.NumericUpDown edtSizeH;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.CheckBox chkShowInTaskBar;
        private System.Windows.Forms.CheckBox chkShowTitleBar;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label9;
    }
}

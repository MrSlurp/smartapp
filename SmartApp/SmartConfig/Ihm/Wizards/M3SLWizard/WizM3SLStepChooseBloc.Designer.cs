namespace SmartApp.Ihm.Wizards
{
    partial class WizM3SLStepChooseBloc
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cboSLIN3 = new System.Windows.Forms.CheckBox();
            this.cboSLIN2 = new System.Windows.Forms.CheckBox();
            this.cboSLIN1 = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cboSLOUT3 = new System.Windows.Forms.CheckBox();
            this.cboSLOUT2 = new System.Windows.Forms.CheckBox();
            this.cboSLOUT1 = new System.Windows.Forms.CheckBox();
            this.btnAllSLIN = new System.Windows.Forms.Button();
            this.btnAllSLOUT = new System.Windows.Forms.Button();
            this.btnAll = new System.Windows.Forms.Button();
            this.btnNone = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cboSLIN3);
            this.groupBox1.Controls.Add(this.cboSLIN2);
            this.groupBox1.Controls.Add(this.cboSLIN1);
            this.groupBox1.Location = new System.Drawing.Point(3, 54);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(245, 92);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "SL IN blocs";
            // 
            // cboSLIN3
            // 
            this.cboSLIN3.AutoSize = true;
            this.cboSLIN3.Location = new System.Drawing.Point(6, 66);
            this.cboSLIN3.Name = "cboSLIN3";
            this.cboSLIN3.Size = new System.Drawing.Size(168, 17);
            this.cboSLIN3.TabIndex = 0;
            this.cboSLIN3.Text = "SL IN 3 (range address 17-24)";
            this.cboSLIN3.UseVisualStyleBackColor = true;
            this.cboSLIN3.CheckedChanged += new System.EventHandler(this.cboCheckedChanged);
            // 
            // cboSLIN2
            // 
            this.cboSLIN2.AutoSize = true;
            this.cboSLIN2.Location = new System.Drawing.Point(7, 43);
            this.cboSLIN2.Name = "cboSLIN2";
            this.cboSLIN2.Size = new System.Drawing.Size(162, 17);
            this.cboSLIN2.TabIndex = 0;
            this.cboSLIN2.Text = "SL IN 2 (range address 9-16)";
            this.cboSLIN2.UseVisualStyleBackColor = true;
            this.cboSLIN2.CheckedChanged += new System.EventHandler(this.cboCheckedChanged);
            // 
            // cboSLIN1
            // 
            this.cboSLIN1.AutoSize = true;
            this.cboSLIN1.Location = new System.Drawing.Point(7, 20);
            this.cboSLIN1.Name = "cboSLIN1";
            this.cboSLIN1.Size = new System.Drawing.Size(156, 17);
            this.cboSLIN1.TabIndex = 0;
            this.cboSLIN1.Text = "SL IN 1 (range address 1-8)";
            this.cboSLIN1.UseVisualStyleBackColor = true;
            this.cboSLIN1.CheckedChanged += new System.EventHandler(this.cboCheckedChanged);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(479, 34);
            this.label1.TabIndex = 1;
            this.label1.Text = "In this first step, you msut select wich SL blocs are used in your M3 program";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cboSLOUT3);
            this.groupBox2.Controls.Add(this.cboSLOUT2);
            this.groupBox2.Controls.Add(this.cboSLOUT1);
            this.groupBox2.Location = new System.Drawing.Point(254, 54);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(245, 92);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "SL OUT blocs";
            // 
            // cboSLOUT3
            // 
            this.cboSLOUT3.AutoSize = true;
            this.cboSLOUT3.Location = new System.Drawing.Point(6, 66);
            this.cboSLOUT3.Name = "cboSLOUT3";
            this.cboSLOUT3.Size = new System.Drawing.Size(180, 17);
            this.cboSLOUT3.TabIndex = 0;
            this.cboSLOUT3.Text = "SL OUT 3 (range address 41-48)";
            this.cboSLOUT3.UseVisualStyleBackColor = true;
            this.cboSLOUT3.CheckedChanged += new System.EventHandler(this.cboCheckedChanged);
            // 
            // cboSLOUT2
            // 
            this.cboSLOUT2.AutoSize = true;
            this.cboSLOUT2.Location = new System.Drawing.Point(7, 43);
            this.cboSLOUT2.Name = "cboSLOUT2";
            this.cboSLOUT2.Size = new System.Drawing.Size(180, 17);
            this.cboSLOUT2.TabIndex = 0;
            this.cboSLOUT2.Text = "SL OUT 2 (range address 33-40)";
            this.cboSLOUT2.UseVisualStyleBackColor = true;
            this.cboSLOUT2.CheckedChanged += new System.EventHandler(this.cboCheckedChanged);
            // 
            // cboSLOUT1
            // 
            this.cboSLOUT1.AutoSize = true;
            this.cboSLOUT1.Location = new System.Drawing.Point(7, 20);
            this.cboSLOUT1.Name = "cboSLOUT1";
            this.cboSLOUT1.Size = new System.Drawing.Size(180, 17);
            this.cboSLOUT1.TabIndex = 0;
            this.cboSLOUT1.Text = "SL OUT 1 (range address 25-32)";
            this.cboSLOUT1.UseVisualStyleBackColor = true;
            this.cboSLOUT1.CheckedChanged += new System.EventHandler(this.cboCheckedChanged);
            // 
            // btnAllSLIN
            // 
            this.btnAllSLIN.Location = new System.Drawing.Point(3, 152);
            this.btnAllSLIN.Name = "btnAllSLIN";
            this.btnAllSLIN.Size = new System.Drawing.Size(245, 27);
            this.btnAllSLIN.TabIndex = 3;
            this.btnAllSLIN.Text = "Check all SL IN";
            this.btnAllSLIN.UseVisualStyleBackColor = true;
            this.btnAllSLIN.Click += new System.EventHandler(this.btnAllSLIN_Click);
            // 
            // btnAllSLOUT
            // 
            this.btnAllSLOUT.Location = new System.Drawing.Point(254, 152);
            this.btnAllSLOUT.Name = "btnAllSLOUT";
            this.btnAllSLOUT.Size = new System.Drawing.Size(245, 27);
            this.btnAllSLOUT.TabIndex = 4;
            this.btnAllSLOUT.Text = "Check all SL OUT";
            this.btnAllSLOUT.UseVisualStyleBackColor = true;
            this.btnAllSLOUT.Click += new System.EventHandler(this.btnAllSLOUT_Click);
            // 
            // btnAll
            // 
            this.btnAll.Location = new System.Drawing.Point(149, 185);
            this.btnAll.Name = "btnAll";
            this.btnAll.Size = new System.Drawing.Size(208, 23);
            this.btnAll.TabIndex = 5;
            this.btnAll.Text = "Check all";
            this.btnAll.UseVisualStyleBackColor = true;
            this.btnAll.Click += new System.EventHandler(this.btnAll_Click);
            // 
            // btnNone
            // 
            this.btnNone.Location = new System.Drawing.Point(149, 214);
            this.btnNone.Name = "btnNone";
            this.btnNone.Size = new System.Drawing.Size(208, 23);
            this.btnNone.TabIndex = 5;
            this.btnNone.Text = "Uncheck all";
            this.btnNone.UseVisualStyleBackColor = true;
            this.btnNone.Click += new System.EventHandler(this.btnNone_Click);
            // 
            // WizM3SLStepChooseBloc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnNone);
            this.Controls.Add(this.btnAll);
            this.Controls.Add(this.btnAllSLOUT);
            this.Controls.Add(this.btnAllSLIN);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.Name = "WizM3SLStepChooseBloc";
            this.Size = new System.Drawing.Size(506, 258);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox cboSLIN3;
        private System.Windows.Forms.CheckBox cboSLIN2;
        private System.Windows.Forms.CheckBox cboSLIN1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox cboSLOUT3;
        private System.Windows.Forms.CheckBox cboSLOUT2;
        private System.Windows.Forms.CheckBox cboSLOUT1;
        private System.Windows.Forms.Button btnAllSLIN;
        private System.Windows.Forms.Button btnAllSLOUT;
        private System.Windows.Forms.Button btnAll;
        private System.Windows.Forms.Button btnNone;

    }
}

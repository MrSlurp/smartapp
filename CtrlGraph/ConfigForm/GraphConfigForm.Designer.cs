namespace CtrlGraph
{
    partial class GraphConfigForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.cboDispPeriod = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cboLogPeriod = new System.Windows.Forms.ComboBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.edtTitle = new System.Windows.Forms.RichTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.edtXAxis = new System.Windows.Forms.TextBox();
            this.edtYAxis = new System.Windows.Forms.TextBox();
            this.uscPanelCurveCfg = new System.Windows.Forms.UserControl();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 328);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Save Period";
            // 
            // cboDispPeriod
            // 
            this.cboDispPeriod.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cboDispPeriod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDispPeriod.FormattingEnabled = true;
            this.cboDispPeriod.Location = new System.Drawing.Point(12, 345);
            this.cboDispPeriod.Name = "cboDispPeriod";
            this.cboDispPeriod.Size = new System.Drawing.Size(121, 21);
            this.cboDispPeriod.TabIndex = 16;
            this.cboDispPeriod.SelectedIndexChanged += new System.EventHandler(this.cboDispPeriod_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(205, 328);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Log Period";
            // 
            // cboLogPeriod
            // 
            this.cboLogPeriod.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cboLogPeriod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLogPeriod.FormattingEnabled = true;
            this.cboLogPeriod.Location = new System.Drawing.Point(205, 345);
            this.cboLogPeriod.Name = "cboLogPeriod";
            this.cboLogPeriod.Size = new System.Drawing.Size(121, 21);
            this.cboLogPeriod.TabIndex = 17;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(286, 377);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 18;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(367, 377);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 19;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Graphic Title";
            // 
            // edtTitle
            // 
            this.edtTitle.Location = new System.Drawing.Point(10, 26);
            this.edtTitle.Name = "edtTitle";
            this.edtTitle.Size = new System.Drawing.Size(275, 50);
            this.edtTitle.TabIndex = 1;
            this.edtTitle.Text = "";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(11, 79);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "X Axis Title";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(208, 79);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Y Axis Title";
            // 
            // edtXAxis
            // 
            this.edtXAxis.Location = new System.Drawing.Point(10, 95);
            this.edtXAxis.Name = "edtXAxis";
            this.edtXAxis.Size = new System.Drawing.Size(173, 20);
            this.edtXAxis.TabIndex = 2;
            // 
            // edtYAxis
            // 
            this.edtYAxis.Location = new System.Drawing.Point(206, 95);
            this.edtYAxis.Name = "edtYAxis";
            this.edtYAxis.Size = new System.Drawing.Size(173, 20);
            this.edtYAxis.TabIndex = 3;
            // 
            // uscPanelCurveCfg
            // 
            this.uscPanelCurveCfg.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.uscPanelCurveCfg.AutoScroll = true;
            this.uscPanelCurveCfg.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.uscPanelCurveCfg.Location = new System.Drawing.Point(8, 120);
            this.uscPanelCurveCfg.Name = "uscPanelCurveCfg";
            this.uscPanelCurveCfg.Size = new System.Drawing.Size(447, 200);
            this.uscPanelCurveCfg.TabIndex = 0;
            // 
            // GraphConfigForm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(462, 412);
            this.ControlBox = false;
            this.Controls.Add(this.uscPanelCurveCfg);
            this.Controls.Add(this.edtYAxis);
            this.Controls.Add(this.edtXAxis);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.edtTitle);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.cboLogPeriod);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cboDispPeriod);
            this.Controls.Add(this.label1);
            this.MaximumSize = new System.Drawing.Size(470, 1000);
            this.MinimumSize = new System.Drawing.Size(470, 420);
            this.Name = "GraphConfigForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.ShowInTaskbar = false;
            this.Text = "Graphic parameters";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.UserControl uscPanelCurveCfg; 
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboDispPeriod;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cboLogPeriod;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RichTextBox edtTitle;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox edtXAxis;
        private System.Windows.Forms.TextBox edtYAxis;
    }
}
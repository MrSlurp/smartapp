namespace CtrlDataGrid
{
    partial class CtrlDataGridProperties
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
            this.uscPanelCurveCfg = new System.Windows.Forms.UserControl();
            this.cboLogPeriod = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cboDispPeriod = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // uscPanelCurveCfg
            // 
            this.uscPanelCurveCfg.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.uscPanelCurveCfg.AutoScroll = true;
            this.uscPanelCurveCfg.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.uscPanelCurveCfg.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.uscPanelCurveCfg.Location = new System.Drawing.Point(0, 56);
            this.uscPanelCurveCfg.Name = "uscPanelCurveCfg";
            this.uscPanelCurveCfg.Size = new System.Drawing.Size(401, 173);
            this.uscPanelCurveCfg.TabIndex = 20;
            // 
            // cboLogPeriod
            // 
            this.cboLogPeriod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLogPeriod.FormattingEnabled = true;
            this.cboLogPeriod.Location = new System.Drawing.Point(208, 26);
            this.cboLogPeriod.Name = "cboLogPeriod";
            this.cboLogPeriod.Size = new System.Drawing.Size(121, 21);
            this.cboLogPeriod.TabIndex = 26;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(210, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 24;
            this.label2.Text = "Log Period";
            // 
            // cboDispPeriod
            // 
            this.cboDispPeriod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDispPeriod.FormattingEnabled = true;
            this.cboDispPeriod.Location = new System.Drawing.Point(12, 26);
            this.cboDispPeriod.Name = "cboDispPeriod";
            this.cboDispPeriod.Size = new System.Drawing.Size(121, 21);
            this.cboDispPeriod.TabIndex = 25;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 13);
            this.label1.TabIndex = 23;
            this.label1.Text = "Save Period";
            // 
            // CtrlDataGridProperties
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.Controls.Add(this.cboLogPeriod);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cboDispPeriod);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.uscPanelCurveCfg);
            this.Name = "CtrlDataGridProperties";
            this.Size = new System.Drawing.Size(404, 232);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.UserControl uscPanelCurveCfg;
        private System.Windows.Forms.ComboBox cboLogPeriod;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cboDispPeriod;
        private System.Windows.Forms.Label label1;
    }
}
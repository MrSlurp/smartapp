namespace SmartApp.Ihm.Wizards
{
    partial class WizM3Z2StepConfigIOSplit
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
            this.m_dataGrid = new System.Windows.Forms.DataGridView();
            this.colSLBloc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colIO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSplitType = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblTypeSplit1 = new System.Windows.Forms.Label();
            this.picTypeSplit1 = new System.Windows.Forms.PictureBox();
            this.lblTypeSplit2 = new System.Windows.Forms.Label();
            this.picTypeSplit2 = new System.Windows.Forms.PictureBox();
            this.lblTypeSplit3 = new System.Windows.Forms.Label();
            this.picTypeSplit3 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.m_dataGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picTypeSplit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picTypeSplit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picTypeSplit3)).BeginInit();
            this.SuspendLayout();
            // 
            // m_dataGrid
            // 
            this.m_dataGrid.AllowUserToAddRows = false;
            this.m_dataGrid.AllowUserToDeleteRows = false;
            this.m_dataGrid.AllowUserToResizeRows = false;
            this.m_dataGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_dataGrid.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.m_dataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.m_dataGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colSLBloc,
            this.colIO,
            this.colSplitType});
            this.m_dataGrid.Location = new System.Drawing.Point(3, 41);
            this.m_dataGrid.MultiSelect = false;
            this.m_dataGrid.Name = "m_dataGrid";
            this.m_dataGrid.Size = new System.Drawing.Size(502, 217);
            this.m_dataGrid.TabIndex = 1;
            this.m_dataGrid.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.m_dataGrid_DataError);
            // 
            // colSLBloc
            // 
            this.colSLBloc.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.colSLBloc.Frozen = true;
            this.colSLBloc.HeaderText = "SL Bloc";
            this.colSLBloc.MinimumWidth = 120;
            this.colSLBloc.Name = "colSLBloc";
            this.colSLBloc.ReadOnly = true;
            this.colSLBloc.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.colSLBloc.Width = 120;
            // 
            // colIO
            // 
            this.colIO.Frozen = true;
            this.colIO.HeaderText = "Input/Ouput";
            this.colIO.MinimumWidth = 120;
            this.colIO.Name = "colIO";
            this.colIO.ReadOnly = true;
            this.colIO.Width = 120;
            // 
            // colSplitType
            // 
            this.colSplitType.HeaderText = "Split type";
            this.colSplitType.Items.AddRange(new object[] {
            "SERIAL",
            "ETHERNET",
            "VIRTUAL"});
            this.colSplitType.MinimumWidth = 150;
            this.colSplitType.Name = "colSplitType";
            this.colSplitType.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.colSplitType.Width = 150;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Location = new System.Drawing.Point(3, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(35, 13);
            this.lblTitle.TabIndex = 2;
            this.lblTitle.Text = "label1";
            // 
            // lblTypeSplit1
            // 
            this.lblTypeSplit1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblTypeSplit1.AutoSize = true;
            this.lblTypeSplit1.Location = new System.Drawing.Point(6, 265);
            this.lblTypeSplit1.Name = "lblTypeSplit1";
            this.lblTypeSplit1.Size = new System.Drawing.Size(35, 13);
            this.lblTypeSplit1.TabIndex = 3;
            this.lblTypeSplit1.Text = "label1";
            // 
            // picTypeSplit1
            // 
            this.picTypeSplit1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.picTypeSplit1.Location = new System.Drawing.Point(6, 282);
            this.picTypeSplit1.Name = "picTypeSplit1";
            this.picTypeSplit1.Size = new System.Drawing.Size(150, 115);
            this.picTypeSplit1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.picTypeSplit1.TabIndex = 4;
            this.picTypeSplit1.TabStop = false;
            // 
            // lblTypeSplit2
            // 
            this.lblTypeSplit2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblTypeSplit2.AutoSize = true;
            this.lblTypeSplit2.Location = new System.Drawing.Point(178, 265);
            this.lblTypeSplit2.Name = "lblTypeSplit2";
            this.lblTypeSplit2.Size = new System.Drawing.Size(35, 13);
            this.lblTypeSplit2.TabIndex = 3;
            this.lblTypeSplit2.Text = "label1";
            // 
            // picTypeSplit2
            // 
            this.picTypeSplit2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.picTypeSplit2.Location = new System.Drawing.Point(178, 282);
            this.picTypeSplit2.Name = "picTypeSplit2";
            this.picTypeSplit2.Size = new System.Drawing.Size(150, 115);
            this.picTypeSplit2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.picTypeSplit2.TabIndex = 4;
            this.picTypeSplit2.TabStop = false;
            // 
            // lblTypeSplit3
            // 
            this.lblTypeSplit3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblTypeSplit3.AutoSize = true;
            this.lblTypeSplit3.Location = new System.Drawing.Point(350, 265);
            this.lblTypeSplit3.Name = "lblTypeSplit3";
            this.lblTypeSplit3.Size = new System.Drawing.Size(35, 13);
            this.lblTypeSplit3.TabIndex = 3;
            this.lblTypeSplit3.Text = "label1";
            // 
            // picTypeSplit3
            // 
            this.picTypeSplit3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.picTypeSplit3.Location = new System.Drawing.Point(350, 282);
            this.picTypeSplit3.Name = "picTypeSplit3";
            this.picTypeSplit3.Size = new System.Drawing.Size(150, 115);
            this.picTypeSplit3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.picTypeSplit3.TabIndex = 4;
            this.picTypeSplit3.TabStop = false;
            // 
            // WizM3Z2StepConfigIOSplit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.picTypeSplit3);
            this.Controls.Add(this.lblTypeSplit3);
            this.Controls.Add(this.picTypeSplit2);
            this.Controls.Add(this.lblTypeSplit2);
            this.Controls.Add(this.picTypeSplit1);
            this.Controls.Add(this.lblTypeSplit1);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.m_dataGrid);
            this.Name = "WizM3Z2StepConfigIOSplit";
            this.Size = new System.Drawing.Size(508, 410);
            ((System.ComponentModel.ISupportInitialize)(this.m_dataGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picTypeSplit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picTypeSplit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picTypeSplit3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView m_dataGrid;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSLBloc;
        private System.Windows.Forms.DataGridViewTextBoxColumn colIO;
        private System.Windows.Forms.DataGridViewComboBoxColumn colSplitType;
        private System.Windows.Forms.Label lblTypeSplit1;
        private System.Windows.Forms.PictureBox picTypeSplit1;
        private System.Windows.Forms.Label lblTypeSplit2;
        private System.Windows.Forms.PictureBox picTypeSplit2;
        private System.Windows.Forms.Label lblTypeSplit3;
        private System.Windows.Forms.PictureBox picTypeSplit3;
    }
}

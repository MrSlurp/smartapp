namespace SmartApp.Ihm.Wizards
{
    partial class WiM3SLStepConfigIOSplit
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
            ((System.ComponentModel.ISupportInitialize)(this.m_dataGrid)).BeginInit();
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
            this.m_dataGrid.Size = new System.Drawing.Size(576, 217);
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
            this.lblTitle.Location = new System.Drawing.Point(4, 4);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(35, 13);
            this.lblTitle.TabIndex = 2;
            this.lblTitle.Text = "label1";
            // 
            // WiM3SLStepConfigIOSplit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.m_dataGrid);
            this.Name = "WiM3SLStepConfigIOSplit";
            this.Size = new System.Drawing.Size(582, 410);
            ((System.ComponentModel.ISupportInitialize)(this.m_dataGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView m_dataGrid;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSLBloc;
        private System.Windows.Forms.DataGridViewTextBoxColumn colIO;
        private System.Windows.Forms.DataGridViewComboBoxColumn colSplitType;
    }
}

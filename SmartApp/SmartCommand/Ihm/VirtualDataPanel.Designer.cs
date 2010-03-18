namespace SmartApp
{
    partial class VirtualDataPanel
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
            this.ColVirtualDataName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColMin = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColMax = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Value = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.m_colSaved = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.btn_checkAll = new System.Windows.Forms.Button();
            this.btn_uncheckAll = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.m_dataGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // m_dataGrid
            // 
            this.m_dataGrid.AllowUserToAddRows = false;
            this.m_dataGrid.AllowUserToDeleteRows = false;
            this.m_dataGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_dataGrid.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.m_dataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.m_dataGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColVirtualDataName,
            this.ColDescription,
            this.ColMin,
            this.ColMax,
            this.Value,
            this.m_colSaved});
            this.m_dataGrid.Location = new System.Drawing.Point(3, 0);
            this.m_dataGrid.Name = "m_dataGrid";
            this.m_dataGrid.Size = new System.Drawing.Size(614, 255);
            this.m_dataGrid.TabIndex = 1;
            this.m_dataGrid.CellMouseUp += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.m_dataGrid_CellMouseClick);
            this.m_dataGrid.CellValidated += new System.Windows.Forms.DataGridViewCellEventHandler(this.m_dataGrid_CellValidated);
            this.m_dataGrid.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.m_dataGrid_CellValidating);
            this.m_dataGrid.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.m_dataGrid_CellEnter);
            // 
            // ColVirtualDataName
            // 
            this.ColVirtualDataName.HeaderText = "Data Name";
            this.ColVirtualDataName.MinimumWidth = 20;
            this.ColVirtualDataName.Name = "ColVirtualDataName";
            this.ColVirtualDataName.ReadOnly = true;
            this.ColVirtualDataName.Width = 200;
            // 
            // ColDescription
            // 
            this.ColDescription.HeaderText = "Description";
            this.ColDescription.Name = "ColDescription";
            this.ColDescription.ReadOnly = true;
            this.ColDescription.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ColDescription.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ColDescription.Width = 160;
            // 
            // ColMin
            // 
            this.ColMin.HeaderText = "Min";
            this.ColMin.MaxInputLength = 10;
            this.ColMin.MinimumWidth = 50;
            this.ColMin.Name = "ColMin";
            this.ColMin.ReadOnly = true;
            this.ColMin.Width = 55;
            // 
            // ColMax
            // 
            this.ColMax.HeaderText = "Max";
            this.ColMax.MaxInputLength = 10;
            this.ColMax.MinimumWidth = 50;
            this.ColMax.Name = "ColMax";
            this.ColMax.ReadOnly = true;
            this.ColMax.Width = 55;
            // 
            // Value
            // 
            this.Value.HeaderText = "Value";
            this.Value.MinimumWidth = 50;
            this.Value.Name = "Value";
            this.Value.Width = 55;
            // 
            // m_colSaved
            // 
            this.m_colSaved.FalseValue = "False";
            this.m_colSaved.HeaderText = "Saved in cliche";
            this.m_colSaved.Name = "m_colSaved";
            this.m_colSaved.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.m_colSaved.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.m_colSaved.TrueValue = "True";
            this.m_colSaved.Width = 40;
            // 
            // btn_checkAll
            // 
            this.btn_checkAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_checkAll.Location = new System.Drawing.Point(4, 262);
            this.btn_checkAll.Name = "btn_checkAll";
            this.btn_checkAll.Size = new System.Drawing.Size(122, 23);
            this.btn_checkAll.TabIndex = 2;
            this.btn_checkAll.Text = "Check All";
            this.btn_checkAll.UseVisualStyleBackColor = true;
            this.btn_checkAll.Click += new System.EventHandler(this.btn_checkAll_Click);
            // 
            // btn_uncheckAll
            // 
            this.btn_uncheckAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_uncheckAll.Location = new System.Drawing.Point(132, 261);
            this.btn_uncheckAll.Name = "btn_uncheckAll";
            this.btn_uncheckAll.Size = new System.Drawing.Size(122, 23);
            this.btn_uncheckAll.TabIndex = 3;
            this.btn_uncheckAll.Text = "Uncheck All";
            this.btn_uncheckAll.UseVisualStyleBackColor = true;
            this.btn_uncheckAll.Click += new System.EventHandler(this.btn_uncheckAll_Click);
            // 
            // VirtualDataPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.btn_uncheckAll);
            this.Controls.Add(this.btn_checkAll);
            this.Controls.Add(this.m_dataGrid);
            this.Name = "VirtualDataPanel";
            this.Size = new System.Drawing.Size(620, 290);
            ((System.ComponentModel.ISupportInitialize)(this.m_dataGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView m_dataGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColVirtualDataName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColDescription;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColMin;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColMax;
        private System.Windows.Forms.DataGridViewTextBoxColumn Value;
        private System.Windows.Forms.DataGridViewCheckBoxColumn m_colSaved;
        private System.Windows.Forms.Button btn_checkAll;
        private System.Windows.Forms.Button btn_uncheckAll;
    }
}

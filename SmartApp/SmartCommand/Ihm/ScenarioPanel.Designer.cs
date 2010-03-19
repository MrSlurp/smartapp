namespace SmartApp
{
    partial class ScenarioPanel
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
            this.ColStep = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColFileName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColFilePath = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColButtonEdit = new System.Windows.Forms.DataGridViewButtonColumn(); 
            ((System.ComponentModel.ISupportInitialize)(this.m_dataGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // m_dataGrid
            // 
            this.m_dataGrid.AllowUserToAddRows = false;
            this.m_dataGrid.AllowUserToDeleteRows = false;
            this.m_dataGrid.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.m_dataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.m_dataGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColStep,
            this.ColFileName,
            this.ColFilePath,
            this.ColButtonEdit});
            this.m_dataGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_dataGrid.Location = new System.Drawing.Point(0, 0);
            this.m_dataGrid.MultiSelect = false;
            this.m_dataGrid.Name = "m_dataGrid";
            this.m_dataGrid.ReadOnly = true;
            this.m_dataGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.m_dataGrid.Size = new System.Drawing.Size(609, 290);
            this.m_dataGrid.TabIndex = 1;
            this.m_dataGrid.DoubleClick += new System.EventHandler(this.m_dataGrid_DoubleClick);
            this.m_dataGrid.SelectionChanged += new System.EventHandler(this.m_dataGrid_SelectionChanged);
            this.m_dataGrid.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.m_dataGrid_CellMouseClick);
            // 
            // ColStep
            // 
            this.ColStep.HeaderText = "Step";
            this.ColStep.Name = "ColStep";
            this.ColStep.ReadOnly = true;
            this.ColStep.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ColStep.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ColStep.Width = 40;
            // 
            // ColFileName
            // 
            this.ColFileName.HeaderText = "File Name";
            this.ColFileName.MinimumWidth = 20;
            this.ColFileName.Name = "ColFileName";
            this.ColFileName.ReadOnly = true;
            this.ColFileName.Width = 150;
            // 
            // ColFilePath
            // 
            this.ColFilePath.HeaderText = "File Path";
            this.ColFilePath.MinimumWidth = 50;
            this.ColFilePath.Name = "ColFilePath";
            this.ColFilePath.ReadOnly = true;
            this.ColFilePath.Width = 300;
            
            // 
            // ColButtonEdit
            // 
            this.ColButtonEdit.HeaderText = "View/Edit";
            this.ColButtonEdit.MinimumWidth = 50;
            this.ColButtonEdit.Name = "ColButtonEdit";
            this.ColButtonEdit.Width = 100;
            this.ColButtonEdit.UseColumnTextForButtonValue = true;
            // 
            // ScenarioPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.m_dataGrid);
            this.Name = "ScenarioPanel";
            this.Size = new System.Drawing.Size(609, 290);
            ((System.ComponentModel.ISupportInitialize)(this.m_dataGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView m_dataGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColStep;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColFileName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColFilePath;
        private System.Windows.Forms.DataGridViewButtonColumn ColButtonEdit;
    }
}

namespace SmartApp.Wizards
{
    partial class SJWizSelectDataStep
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
            this.dataGrid = new System.Windows.Forms.DataGridView();
            this.ColDataSymbol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColDataSize = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.lblRequiredSelectionCount = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnResetSelection = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGrid
            // 
            this.dataGrid.AllowUserToAddRows = false;
            this.dataGrid.AllowUserToDeleteRows = false;
            this.dataGrid.AllowUserToResizeRows = false;
            this.dataGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColDataSymbol,
            this.ColDataSize});
            this.dataGrid.Location = new System.Drawing.Point(3, 41);
            this.dataGrid.Name = "dataGrid";
            this.dataGrid.ReadOnly = true;
            this.dataGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGrid.Size = new System.Drawing.Size(441, 188);
            this.dataGrid.TabIndex = 0;
            this.dataGrid.SelectionChanged += new System.EventHandler(this.dataGrid_SelectionChanged);
            // 
            // ColDataSymbol
            // 
            this.ColDataSymbol.HeaderText = "Data";
            this.ColDataSymbol.MinimumWidth = 200;
            this.ColDataSymbol.Name = "ColDataSymbol";
            this.ColDataSymbol.ReadOnly = true;
            this.ColDataSymbol.Width = 200;
            // 
            // ColDataSize
            // 
            this.ColDataSize.HeaderText = "Size";
            this.ColDataSize.MinimumWidth = 150;
            this.ColDataSize.Name = "ColDataSize";
            this.ColDataSize.ReadOnly = true;
            this.ColDataSize.Width = 150;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(126, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Select data(s) to split/join";
            // 
            // lblRequiredSelectionCount
            // 
            this.lblRequiredSelectionCount.AutoSize = true;
            this.lblRequiredSelectionCount.Location = new System.Drawing.Point(6, 22);
            this.lblRequiredSelectionCount.Name = "lblRequiredSelectionCount";
            this.lblRequiredSelectionCount.Size = new System.Drawing.Size(132, 13);
            this.lblRequiredSelectionCount.TabIndex = 2;
            this.lblRequiredSelectionCount.Text = "lblRequiredSelectionCount";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 232);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(363, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Tips: use Ctrl + Click or Shift + click  to select / deselect multiple lines in g" +
                "rid";
            // 
            // btnResetSelection
            // 
            this.btnResetSelection.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnResetSelection.Location = new System.Drawing.Point(9, 249);
            this.btnResetSelection.Name = "btnResetSelection";
            this.btnResetSelection.Size = new System.Drawing.Size(149, 23);
            this.btnResetSelection.TabIndex = 4;
            this.btnResetSelection.Text = "Reset Selection";
            this.btnResetSelection.UseVisualStyleBackColor = true;
            this.btnResetSelection.Click += new System.EventHandler(this.btnResetSelection_Click);
            // 
            // SJWizSelectDataStep
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnResetSelection);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblRequiredSelectionCount);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGrid);
            this.Name = "SJWizSelectDataStep";
            this.Size = new System.Drawing.Size(447, 276);
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGrid;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblRequiredSelectionCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColDataSymbol;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColDataSize;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnResetSelection;

    }
}

namespace SmartApp
{
    partial class CommConfiguration
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
            this.m_dataGrid = new System.Windows.Forms.DataGridView();
            this.ColConnectionName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColConnectionType = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.ColConnectionParam = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.m_dataGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // m_dataGrid
            // 
            this.m_dataGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_dataGrid.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.m_dataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.m_dataGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColConnectionName,
            this.ColConnectionType,
            this.ColConnectionParam});
            this.m_dataGrid.Location = new System.Drawing.Point(12, 30);
            this.m_dataGrid.Name = "m_dataGrid";
            this.m_dataGrid.Size = new System.Drawing.Size(596, 232);
            this.m_dataGrid.TabIndex = 0;
            this.m_dataGrid.CancelRowEdit += new System.Windows.Forms.QuestionEventHandler(this.m_dataGrid_CancelRowEdit);
            this.m_dataGrid.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.m_dataGrid_CellValidating);
            this.m_dataGrid.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.m_dataGrid_CellEndEdit);
            this.m_dataGrid.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.m_dataGrid_CellValueChanged);
            this.m_dataGrid.Validating += new System.ComponentModel.CancelEventHandler(this.m_dataGrid_Validating);
            // 
            // ColConnectionName
            // 
            this.ColConnectionName.HeaderText = "Connection name";
            this.ColConnectionName.MinimumWidth = 20;
            this.ColConnectionName.Name = "ColConnectionName";
            this.ColConnectionName.Width = 200;
            // 
            // ColConnectionType
            // 
            this.ColConnectionType.HeaderText = "Type";
            this.ColConnectionType.Name = "ColConnectionType";
            this.ColConnectionType.Width = 150;
            // 
            // ColConnectionParam
            // 
            this.ColConnectionParam.HeaderText = "Parameters";
            this.ColConnectionParam.Name = "ColConnectionParam";
            this.ColConnectionParam.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ColConnectionParam.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ColConnectionParam.Width = 200;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 268);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(110, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Ok";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(119, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Configured connections";
            // 
            // CommConfiguration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(620, 297);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.m_dataGrid);
            this.Name = "CommConfiguration";
            this.Text = "CommConfiguration";
            ((System.ComponentModel.ISupportInitialize)(this.m_dataGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView m_dataGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColConnectionName;
        private System.Windows.Forms.DataGridViewComboBoxColumn ColConnectionType;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColConnectionParam;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
    }
}
using CommonLib;

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
            this.btnOK = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.m_dataGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // m_dataGrid
            // 
            this.m_dataGrid.AllowUserToResizeRows = false;
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
            this.m_dataGrid.MultiSelect = false;
            this.m_dataGrid.Name = "m_dataGrid";
            this.m_dataGrid.Size = new System.Drawing.Size(596, 232);
            this.m_dataGrid.TabIndex = 0;
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
            this.ColConnectionType.Items.AddRange(new object[] {
            "SERIAL",
            "ETHERNET",
            "VIRTUAL"});
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
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(12, 268);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(110, 23);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.button1_Click);
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
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(128, 268);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(110, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // CommConfiguration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(620, 297);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.m_dataGrid);
            this.Name = "CommConfiguration";
            this.Text = "CommConfiguration";
            this.Load += new System.EventHandler(this.CommConfiguration_Load);
            this.Shown += new System.EventHandler(this.CommConfiguration_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.m_dataGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView m_dataGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColConnectionName;
        private System.Windows.Forms.DataGridViewComboBoxColumn ColConnectionType;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColConnectionParam;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnCancel;
    }
}
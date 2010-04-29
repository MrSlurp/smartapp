namespace CtrlDataGrid
{
    partial class InteractiveCtrlDataGridDllControl
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
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.ColTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colV1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colV2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEtc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.AllowUserToDeleteRows = false;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColTime,
            this.colV1,
            this.colV2,
            this.colEtc});
            this.dataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView.Enabled = false;
            this.dataGridView.Location = new System.Drawing.Point(0, 0);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.ReadOnly = true;
            this.dataGridView.Size = new System.Drawing.Size(226, 136);
            this.dataGridView.TabIndex = 1;
            // 
            // ColTime
            // 
            this.ColTime.Frozen = true;
            this.ColTime.HeaderText = "Time";
            this.ColTime.Name = "ColTime";
            this.ColTime.ReadOnly = true;
            this.ColTime.Width = 40;
            // 
            // colV1
            // 
            this.colV1.HeaderText = "Data 1";
            this.colV1.Name = "colV1";
            this.colV1.ReadOnly = true;
            this.colV1.Width = 50;
            // 
            // colV2
            // 
            this.colV2.HeaderText = "Data 2";
            this.colV2.Name = "colV2";
            this.colV2.ReadOnly = true;
            this.colV2.Width = 50;
            // 
            // colEtc
            // 
            this.colEtc.HeaderText = "Etc...";
            this.colEtc.Name = "colEtc";
            this.colEtc.ReadOnly = true;
            this.colEtc.Width = 40;
            // 
            // InteractiveCtrlDataGridDllControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dataGridView);
            this.Name = "InteractiveCtrlDataGridDllControl";
            this.Size = new System.Drawing.Size(226, 136);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn colV1;
        private System.Windows.Forms.DataGridViewTextBoxColumn colV2;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEtc;
    }
}

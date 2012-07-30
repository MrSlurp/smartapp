namespace SmartApp
{
    partial class BridgeEditorForm
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
            this.numBridgePeriod = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.gridExchangeTable = new System.Windows.Forms.DataGridView();
            this.colSrcProj = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.colSrcData = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.colDstProj = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.colDstData = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.gridPostScript = new System.Windows.Forms.DataGridView();
            this.colScriptProj = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.colScriptFunction = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numBridgePeriod)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridExchangeTable)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridPostScript)).BeginInit();
            this.SuspendLayout();
            // 
            // numBridgePeriod
            // 
            this.numBridgePeriod.Location = new System.Drawing.Point(16, 19);
            this.numBridgePeriod.Name = "numBridgePeriod";
            this.numBridgePeriod.Size = new System.Drawing.Size(120, 20);
            this.numBridgePeriod.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Bridge period";
            // 
            // gridExchangeTable
            // 
            this.gridExchangeTable.AllowUserToResizeRows = false;
            this.gridExchangeTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridExchangeTable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colSrcProj,
            this.colSrcData,
            this.colDstProj,
            this.colDstData});
            this.gridExchangeTable.Location = new System.Drawing.Point(12, 69);
            this.gridExchangeTable.Name = "gridExchangeTable";
            this.gridExchangeTable.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.gridExchangeTable.Size = new System.Drawing.Size(726, 241);
            this.gridExchangeTable.TabIndex = 2;
            this.gridExchangeTable.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridExchangeTable_CellValueChanged);
            this.gridExchangeTable.UserAddedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.gridExchangeTable_UserAddedRow);
            this.gridExchangeTable.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.gridExchangeTable_EditingControlShowing);
            // 
            // colSrcProj
            // 
            this.colSrcProj.HeaderText = "Source Project";
            this.colSrcProj.Name = "colSrcProj";
            this.colSrcProj.Width = 160;
            // 
            // colSrcData
            // 
            this.colSrcData.HeaderText = "Source Data";
            this.colSrcData.Name = "colSrcData";
            this.colSrcData.Width = 160;
            // 
            // colDstProj
            // 
            this.colDstProj.HeaderText = "Destination Project";
            this.colDstProj.Name = "colDstProj";
            this.colDstProj.Width = 160;
            // 
            // colDstData
            // 
            this.colDstData.HeaderText = "Destination Data";
            this.colDstData.Name = "colDstData";
            this.colDstData.Width = 160;
            // 
            // gridPostScript
            // 
            this.gridPostScript.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridPostScript.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colScriptProj,
            this.colScriptFunction});
            this.gridPostScript.Location = new System.Drawing.Point(12, 348);
            this.gridPostScript.Name = "gridPostScript";
            this.gridPostScript.Size = new System.Drawing.Size(687, 153);
            this.gridPostScript.TabIndex = 2;
            // 
            // colScriptProj
            // 
            this.colScriptProj.HeaderText = "Project";
            this.colScriptProj.Name = "colScriptProj";
            this.colScriptProj.Width = 300;
            // 
            // colScriptFunction
            // 
            this.colScriptFunction.HeaderText = "Function";
            this.colScriptFunction.Name = "colScriptFunction";
            this.colScriptFunction.Width = 300;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(705, 389);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(33, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "U";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(705, 418);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(33, 23);
            this.button2.TabIndex = 3;
            this.button2.Text = "D";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(582, 507);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 4;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(663, 507);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(106, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Data exchange table";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 329);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(157, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Document post exchange script";
            // 
            // BridgeEditorForm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(750, 542);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.gridPostScript);
            this.Controls.Add(this.gridExchangeTable);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numBridgePeriod);
            this.Name = "BridgeEditorForm";
            this.Text = "BridgeEditorForm";
            ((System.ComponentModel.ISupportInitialize)(this.numBridgePeriod)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridExchangeTable)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridPostScript)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown numBridgePeriod;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView gridExchangeTable;
        private System.Windows.Forms.DataGridView gridPostScript;
        private System.Windows.Forms.DataGridViewComboBoxColumn colSrcProj;
        private System.Windows.Forms.DataGridViewComboBoxColumn colSrcData;
        private System.Windows.Forms.DataGridViewComboBoxColumn colDstProj;
        private System.Windows.Forms.DataGridViewComboBoxColumn colDstData;
        private System.Windows.Forms.DataGridViewComboBoxColumn colScriptProj;
        private System.Windows.Forms.DataGridViewComboBoxColumn colScriptFunction;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}
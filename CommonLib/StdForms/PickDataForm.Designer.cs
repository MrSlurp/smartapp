namespace CommonLib
{
    partial class PickDataForm
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
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.m_listViewData = new System.Windows.Forms.ListView();
            this.m_ColHeadSymbol = new System.Windows.Forms.ColumnHeader();
            this.m_ColHeadSize = new System.Windows.Forms.ColumnHeader();
            this.cboShowConst = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(190, 369);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(271, 369);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // m_listViewData
            // 
            this.m_listViewData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_listViewData.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.m_ColHeadSymbol,
            this.m_ColHeadSize});
            this.m_listViewData.FullRowSelect = true;
            this.m_listViewData.GridLines = true;
            this.m_listViewData.HideSelection = false;
            this.m_listViewData.Location = new System.Drawing.Point(12, 12);
            this.m_listViewData.MultiSelect = false;
            this.m_listViewData.Name = "m_listViewData";
            this.m_listViewData.Size = new System.Drawing.Size(334, 342);
            this.m_listViewData.TabIndex = 8;
            this.m_listViewData.UseCompatibleStateImageBehavior = false;
            this.m_listViewData.View = System.Windows.Forms.View.Details;
            this.m_listViewData.SelectedIndexChanged += new System.EventHandler(this.m_listViewData_SelectedIndexChanged);
            // 
            // m_ColHeadSymbol
            // 
            this.m_ColHeadSymbol.Text = "Symbol";
            this.m_ColHeadSymbol.Width = 244;
            // 
            // m_ColHeadSize
            // 
            this.m_ColHeadSize.Text = "Size";
            this.m_ColHeadSize.Width = 58;
            // 
            // cboShowConst
            // 
            this.cboShowConst.AutoSize = true;
            this.cboShowConst.Location = new System.Drawing.Point(12, 369);
            this.cboShowConst.Name = "cboShowConst";
            this.cboShowConst.Size = new System.Drawing.Size(126, 17);
            this.cboShowConst.TabIndex = 9;
            this.cboShowConst.Text = "Show constants data";
            this.cboShowConst.UseVisualStyleBackColor = true;
            this.cboShowConst.CheckedChanged += new System.EventHandler(this.cboShowConst_CheckedChanged);
            // 
            // PickDataForm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(358, 404);
            this.ControlBox = false;
            this.Controls.Add(this.cboShowConst);
            this.Controls.Add(this.m_listViewData);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.MinimumSize = new System.Drawing.Size(374, 442);
            this.Name = "PickDataForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Pick Data";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ListView m_listViewData;
        private System.Windows.Forms.ColumnHeader m_ColHeadSymbol;
        private System.Windows.Forms.ColumnHeader m_ColHeadSize;
        private System.Windows.Forms.CheckBox cboShowConst;
    }
}
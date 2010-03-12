namespace CtrlDemux
{
    partial class DemuxConfigForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.edtAddrData = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.edtValueData = new System.Windows.Forms.TextBox();
            this.btnPickAddr = new System.Windows.Forms.Button();
            this.btnPickValue = new System.Windows.Forms.Button();
            this.m_listViewData = new System.Windows.Forms.ListView();
            this.m_ColHeadSymbol = new System.Windows.Forms.ColumnHeader();
            this.m_ColHeadSize = new System.Windows.Forms.ColumnHeader();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Adress Data";
            // 
            // edtAddrData
            // 
            this.edtAddrData.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.edtAddrData.Location = new System.Drawing.Point(12, 26);
            this.edtAddrData.Name = "edtAddrData";
            this.edtAddrData.Size = new System.Drawing.Size(231, 20);
            this.edtAddrData.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Value Data";
            // 
            // edtValueData
            // 
            this.edtValueData.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.edtValueData.Location = new System.Drawing.Point(12, 70);
            this.edtValueData.Name = "edtValueData";
            this.edtValueData.Size = new System.Drawing.Size(231, 20);
            this.edtValueData.TabIndex = 1;
            // 
            // btnPickAddr
            // 
            this.btnPickAddr.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPickAddr.Location = new System.Drawing.Point(252, 24);
            this.btnPickAddr.Name = "btnPickAddr";
            this.btnPickAddr.Size = new System.Drawing.Size(44, 23);
            this.btnPickAddr.TabIndex = 2;
            this.btnPickAddr.Text = "Pick";
            this.btnPickAddr.UseVisualStyleBackColor = true;
            this.btnPickAddr.Click += new System.EventHandler(this.btnPickAddr_Click);
            // 
            // btnPickValue
            // 
            this.btnPickValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPickValue.Location = new System.Drawing.Point(252, 68);
            this.btnPickValue.Name = "btnPickValue";
            this.btnPickValue.Size = new System.Drawing.Size(44, 23);
            this.btnPickValue.TabIndex = 2;
            this.btnPickValue.Text = "Pick";
            this.btnPickValue.UseVisualStyleBackColor = true;
            this.btnPickValue.Click += new System.EventHandler(this.btnPickValue_Click);
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
            this.m_listViewData.Location = new System.Drawing.Point(12, 126);
            this.m_listViewData.MultiSelect = false;
            this.m_listViewData.Name = "m_listViewData";
            this.m_listViewData.Size = new System.Drawing.Size(284, 199);
            this.m_listViewData.TabIndex = 9;
            this.m_listViewData.UseCompatibleStateImageBehavior = false;
            this.m_listViewData.View = System.Windows.Forms.View.Details;
            this.m_listViewData.SelectedIndexChanged += new System.EventHandler(this.m_listViewData_SelectedIndexChanged);
            this.m_listViewData.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_listViewData_KeyDown);
            // 
            // m_ColHeadSymbol
            // 
            this.m_ColHeadSymbol.Text = "Symbol";
            this.m_ColHeadSymbol.Width = 190;
            // 
            // m_ColHeadSize
            // 
            this.m_ColHeadSize.Text = "Size";
            this.m_ColHeadSize.Width = 40;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(12, 97);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(109, 23);
            this.btnAdd.TabIndex = 10;
            this.btnAdd.Text = "Add Data";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemove.Location = new System.Drawing.Point(194, 97);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(102, 23);
            this.btnRemove.TabIndex = 10;
            this.btnRemove.Text = "Remove Data";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(13, 331);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 11;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(219, 331);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 11;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // DemuxConfigForm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(306, 366);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.m_listViewData);
            this.Controls.Add(this.btnPickValue);
            this.Controls.Add(this.btnPickAddr);
            this.Controls.Add(this.edtValueData);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.edtAddrData);
            this.Controls.Add(this.label1);
            this.MinimumSize = new System.Drawing.Size(270, 400);
            this.Name = "DemuxConfigForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Demux Configuration";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox edtAddrData;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox edtValueData;
        private System.Windows.Forms.Button btnPickAddr;
        private System.Windows.Forms.Button btnPickValue;
        private System.Windows.Forms.ListView m_listViewData;
        private System.Windows.Forms.ColumnHeader m_ColHeadSymbol;
        private System.Windows.Forms.ColumnHeader m_ColHeadSize;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
    }
}
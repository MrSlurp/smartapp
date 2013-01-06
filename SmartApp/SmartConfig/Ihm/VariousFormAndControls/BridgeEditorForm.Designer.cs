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
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.cboSourceProj = new System.Windows.Forms.ComboBox();
            this.cboTargetProj = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnUpData = new System.Windows.Forms.Button();
            this.btnDownData = new System.Windows.Forms.Button();
            this.btnAddDstData = new System.Windows.Forms.Button();
            this.btnRemDstData = new System.Windows.Forms.Button();
            this.btnAddSrcData = new System.Windows.Forms.Button();
            this.btnRemSrcData = new System.Windows.Forms.Button();
            this.lstViewSourceDatas = new System.Windows.Forms.ListView();
            this.m_ColHeadSymbol = new System.Windows.Forms.ColumnHeader();
            this.m_ColHeadSize = new System.Windows.Forms.ColumnHeader();
            this.lstViewTargetDatas = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cboSourceGrpFilter = new System.Windows.Forms.ComboBox();
            this.cboTargetGrpFilter = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.gridViewBridge = new System.Windows.Forms.DataGridView();
            this.colSrcData = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDstDatas = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cboPostFunc = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.txtSymbol = new CommonLib.SymbolTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.numBridgePeriod)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewBridge)).BeginInit();
            this.SuspendLayout();
            // 
            // numBridgePeriod
            // 
            this.numBridgePeriod.Location = new System.Drawing.Point(701, 23);
            this.numBridgePeriod.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numBridgePeriod.Minimum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.numBridgePeriod.Name = "numBridgePeriod";
            this.numBridgePeriod.Size = new System.Drawing.Size(120, 20);
            this.numBridgePeriod.TabIndex = 0;
            this.numBridgePeriod.Value = new decimal(new int[] {
            200,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(698, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Bridge period";
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(358, 526);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 4;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(478, 526);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // cboSourceProj
            // 
            this.cboSourceProj.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSourceProj.FormattingEnabled = true;
            this.cboSourceProj.Location = new System.Drawing.Point(16, 69);
            this.cboSourceProj.Name = "cboSourceProj";
            this.cboSourceProj.Size = new System.Drawing.Size(210, 21);
            this.cboSourceProj.TabIndex = 7;
            this.cboSourceProj.SelectedIndexChanged += new System.EventHandler(this.cboSourceProj_SelectedIndexChanged);
            // 
            // cboTargetProj
            // 
            this.cboTargetProj.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTargetProj.FormattingEnabled = true;
            this.cboTargetProj.Location = new System.Drawing.Point(701, 69);
            this.cboTargetProj.Name = "cboTargetProj";
            this.cboTargetProj.Size = new System.Drawing.Size(210, 21);
            this.cboTargetProj.TabIndex = 7;
            this.cboTargetProj.SelectedIndexChanged += new System.EventHandler(this.cboTargetProj_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Source project";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(698, 53);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Target project";
            // 
            // btnUpData
            // 
            this.btnUpData.Location = new System.Drawing.Point(420, 460);
            this.btnUpData.Name = "btnUpData";
            this.btnUpData.Size = new System.Drawing.Size(33, 30);
            this.btnUpData.TabIndex = 3;
            this.btnUpData.UseVisualStyleBackColor = true;
            this.btnUpData.Click += new System.EventHandler(this.btnUpDownData_Click);
            // 
            // btnDownData
            // 
            this.btnDownData.Location = new System.Drawing.Point(459, 460);
            this.btnDownData.Name = "btnDownData";
            this.btnDownData.Size = new System.Drawing.Size(33, 30);
            this.btnDownData.TabIndex = 3;
            this.btnDownData.UseVisualStyleBackColor = true;
            this.btnDownData.Click += new System.EventHandler(this.btnUpDownData_Click);
            // 
            // btnAddDstData
            // 
            this.btnAddDstData.Location = new System.Drawing.Point(662, 218);
            this.btnAddDstData.Name = "btnAddDstData";
            this.btnAddDstData.Size = new System.Drawing.Size(33, 30);
            this.btnAddDstData.TabIndex = 3;
            this.btnAddDstData.UseVisualStyleBackColor = true;
            this.btnAddDstData.Click += new System.EventHandler(this.btnAddRemDstData_Click);
            // 
            // btnRemDstData
            // 
            this.btnRemDstData.Location = new System.Drawing.Point(662, 260);
            this.btnRemDstData.Name = "btnRemDstData";
            this.btnRemDstData.Size = new System.Drawing.Size(33, 30);
            this.btnRemDstData.TabIndex = 3;
            this.btnRemDstData.UseVisualStyleBackColor = true;
            this.btnRemDstData.Click += new System.EventHandler(this.btnAddRemDstData_Click);
            // 
            // btnAddSrcData
            // 
            this.btnAddSrcData.Location = new System.Drawing.Point(232, 218);
            this.btnAddSrcData.Name = "btnAddSrcData";
            this.btnAddSrcData.Size = new System.Drawing.Size(33, 30);
            this.btnAddSrcData.TabIndex = 3;
            this.btnAddSrcData.UseVisualStyleBackColor = true;
            this.btnAddSrcData.Click += new System.EventHandler(this.btnAddRemSrcData_Click);
            // 
            // btnRemSrcData
            // 
            this.btnRemSrcData.Location = new System.Drawing.Point(232, 260);
            this.btnRemSrcData.Name = "btnRemSrcData";
            this.btnRemSrcData.Size = new System.Drawing.Size(33, 30);
            this.btnRemSrcData.TabIndex = 3;
            this.btnRemSrcData.UseVisualStyleBackColor = true;
            this.btnRemSrcData.Click += new System.EventHandler(this.btnAddRemSrcData_Click);
            // 
            // lstViewSourceDatas
            // 
            this.lstViewSourceDatas.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.m_ColHeadSymbol,
            this.m_ColHeadSize});
            this.lstViewSourceDatas.FullRowSelect = true;
            this.lstViewSourceDatas.GridLines = true;
            this.lstViewSourceDatas.HideSelection = false;
            this.lstViewSourceDatas.Location = new System.Drawing.Point(16, 161);
            this.lstViewSourceDatas.Name = "lstViewSourceDatas";
            this.lstViewSourceDatas.Size = new System.Drawing.Size(210, 322);
            this.lstViewSourceDatas.TabIndex = 10;
            this.lstViewSourceDatas.UseCompatibleStateImageBehavior = false;
            this.lstViewSourceDatas.View = System.Windows.Forms.View.Details;
            // 
            // m_ColHeadSymbol
            // 
            this.m_ColHeadSymbol.Text = "Symbol";
            this.m_ColHeadSymbol.Width = 125;
            // 
            // m_ColHeadSize
            // 
            this.m_ColHeadSize.Text = "Size";
            this.m_ColHeadSize.Width = 40;
            // 
            // lstViewTargetDatas
            // 
            this.lstViewTargetDatas.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.lstViewTargetDatas.FullRowSelect = true;
            this.lstViewTargetDatas.GridLines = true;
            this.lstViewTargetDatas.HideSelection = false;
            this.lstViewTargetDatas.Location = new System.Drawing.Point(701, 161);
            this.lstViewTargetDatas.Name = "lstViewTargetDatas";
            this.lstViewTargetDatas.Size = new System.Drawing.Size(210, 322);
            this.lstViewTargetDatas.TabIndex = 10;
            this.lstViewTargetDatas.UseCompatibleStateImageBehavior = false;
            this.lstViewTargetDatas.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Symbol";
            this.columnHeader1.Width = 143;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Size";
            this.columnHeader2.Width = 40;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(698, 97);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Group filter";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 97);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(58, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Group filter";
            // 
            // cboSourceGrpFilter
            // 
            this.cboSourceGrpFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSourceGrpFilter.FormattingEnabled = true;
            this.cboSourceGrpFilter.Location = new System.Drawing.Point(16, 113);
            this.cboSourceGrpFilter.Name = "cboSourceGrpFilter";
            this.cboSourceGrpFilter.Size = new System.Drawing.Size(210, 21);
            this.cboSourceGrpFilter.TabIndex = 7;
            this.cboSourceGrpFilter.SelectedIndexChanged += new System.EventHandler(this.cboSourceGrpFilter_SelectedIndexChanged);
            // 
            // cboTargetGrpFilter
            // 
            this.cboTargetGrpFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTargetGrpFilter.FormattingEnabled = true;
            this.cboTargetGrpFilter.Location = new System.Drawing.Point(701, 113);
            this.cboTargetGrpFilter.Name = "cboTargetGrpFilter";
            this.cboTargetGrpFilter.Size = new System.Drawing.Size(210, 21);
            this.cboTargetGrpFilter.TabIndex = 7;
            this.cboTargetGrpFilter.SelectedIndexChanged += new System.EventHandler(this.cboTargetGrpFilter_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 145);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(105, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "Source project datas";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(698, 145);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(102, 13);
            this.label7.TabIndex = 11;
            this.label7.Text = "Target project datas";
            // 
            // gridViewBridge
            // 
            this.gridViewBridge.AllowUserToAddRows = false;
            this.gridViewBridge.AllowUserToDeleteRows = false;
            this.gridViewBridge.AllowUserToResizeRows = false;
            this.gridViewBridge.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridViewBridge.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colSrcData,
            this.colDstDatas});
            this.gridViewBridge.Location = new System.Drawing.Point(271, 69);
            this.gridViewBridge.MultiSelect = false;
            this.gridViewBridge.Name = "gridViewBridge";
            this.gridViewBridge.ReadOnly = true;
            this.gridViewBridge.RowHeadersVisible = false;
            this.gridViewBridge.Size = new System.Drawing.Size(385, 385);
            this.gridViewBridge.TabIndex = 12;
            this.gridViewBridge.SelectionChanged += new System.EventHandler(this.gridViewBridge_SelectionChanged);
            // 
            // colSrcData
            // 
            this.colSrcData.HeaderText = "Source Datas";
            this.colSrcData.Name = "colSrcData";
            this.colSrcData.ReadOnly = true;
            this.colSrcData.Width = 180;
            // 
            // colDstDatas
            // 
            this.colDstDatas.HeaderText = "Target Datas";
            this.colDstDatas.Name = "colDstDatas";
            this.colDstDatas.ReadOnly = true;
            this.colDstDatas.Width = 180;
            // 
            // cboPostFunc
            // 
            this.cboPostFunc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPostFunc.FormattingEnabled = true;
            this.cboPostFunc.Location = new System.Drawing.Point(701, 506);
            this.cboPostFunc.Name = "cboPostFunc";
            this.cboPostFunc.Size = new System.Drawing.Size(210, 21);
            this.cboPostFunc.TabIndex = 13;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(701, 490);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(135, 13);
            this.label8.TabIndex = 14;
            this.label8.Text = "Post bridge executed script";
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(271, 25);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(385, 41);
            this.label9.TabIndex = 15;
            this.label9.Text = "At each bridge period, values from source datas are copied to targets datas. Note" +
                " that it can trigger scripted events in target supervision.";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(12, 7);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(72, 13);
            this.label10.TabIndex = 1;
            this.label10.Text = "Bridge symbol";
            // 
            // txtSymbol
            // 
            this.txtSymbol.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtSymbol.Location = new System.Drawing.Point(16, 24);
            this.txtSymbol.Name = "txtSymbol";
            this.txtSymbol.Size = new System.Drawing.Size(210, 20);
            this.txtSymbol.TabIndex = 16;
            // 
            // BridgeEditorForm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(928, 559);
            this.Controls.Add(this.txtSymbol);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.cboPostFunc);
            this.Controls.Add(this.gridViewBridge);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lstViewTargetDatas);
            this.Controls.Add(this.lstViewSourceDatas);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cboTargetGrpFilter);
            this.Controls.Add(this.cboTargetProj);
            this.Controls.Add(this.cboSourceGrpFilter);
            this.Controls.Add(this.cboSourceProj);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnDownData);
            this.Controls.Add(this.btnUpData);
            this.Controls.Add(this.btnRemSrcData);
            this.Controls.Add(this.btnRemDstData);
            this.Controls.Add(this.btnAddSrcData);
            this.Controls.Add(this.btnAddDstData);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numBridgePeriod);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "BridgeEditorForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "BridgeEditorForm";
            ((System.ComponentModel.ISupportInitialize)(this.numBridgePeriod)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewBridge)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown numBridgePeriod;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ComboBox cboSourceProj;
        private System.Windows.Forms.ComboBox cboTargetProj;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnUpData;
        private System.Windows.Forms.Button btnDownData;
        private System.Windows.Forms.Button btnAddDstData;
        private System.Windows.Forms.Button btnRemDstData;
        private System.Windows.Forms.Button btnAddSrcData;
        private System.Windows.Forms.Button btnRemSrcData;
        private System.Windows.Forms.ListView lstViewSourceDatas;
        private System.Windows.Forms.ColumnHeader m_ColHeadSymbol;
        private System.Windows.Forms.ColumnHeader m_ColHeadSize;
        private System.Windows.Forms.ListView lstViewTargetDatas;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cboSourceGrpFilter;
        private System.Windows.Forms.ComboBox cboTargetGrpFilter;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DataGridView gridViewBridge;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSrcData;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDstDatas;
        private System.Windows.Forms.ComboBox cboPostFunc;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private CommonLib.SymbolTextBox txtSymbol;
    }
}
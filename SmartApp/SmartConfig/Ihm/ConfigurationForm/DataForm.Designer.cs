namespace SmartApp.Ihm
{
    partial class DataForm
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.m_btnManageGroups = new System.Windows.Forms.Button();
            this.m_btnNew = new System.Windows.Forms.Button();
            this.m_cboGroups = new System.Windows.Forms.ComboBox();
            this.m_listViewData = new System.Windows.Forms.ListView();
            this.m_ColHeadSymbol = new System.Windows.Forms.ColumnHeader();
            this.m_ColHeadConstant = new System.Windows.Forms.ColumnHeader();
            this.m_ColHeadSize = new System.Windows.Forms.ColumnHeader();
            this.m_ColHeadSumm = new System.Windows.Forms.ColumnHeader();
            this.m_DataPropertyPage = new SmartApp.Ihm.DataPropertiesControl();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.MinimumSize = new System.Drawing.Size(600, 400);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.m_btnManageGroups);
            this.splitContainer1.Panel1.Controls.Add(this.m_btnNew);
            this.splitContainer1.Panel1.Controls.Add(this.m_cboGroups);
            this.splitContainer1.Panel1.Controls.Add(this.m_listViewData);
            this.splitContainer1.Panel1MinSize = 0;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.m_DataPropertyPage);
            this.splitContainer1.Panel2MinSize = 0;
            this.splitContainer1.Size = new System.Drawing.Size(636, 441);
            this.splitContainer1.SplitterDistance = 395;
            this.splitContainer1.TabIndex = 0;
            // 
            // m_btnManageGroups
            // 
            this.m_btnManageGroups.Location = new System.Drawing.Point(181, 40);
            this.m_btnManageGroups.Name = "m_btnManageGroups";
            this.m_btnManageGroups.Size = new System.Drawing.Size(168, 23);
            this.m_btnManageGroups.TabIndex = 11;
            this.m_btnManageGroups.Text = "Manage Groups";
            this.m_btnManageGroups.UseVisualStyleBackColor = true;
            this.m_btnManageGroups.Click += new System.EventHandler(this.OnManageGroupsClick);
            // 
            // m_btnNew
            // 
            this.m_btnNew.Location = new System.Drawing.Point(7, 40);
            this.m_btnNew.Name = "m_btnNew";
            this.m_btnNew.Size = new System.Drawing.Size(168, 23);
            this.m_btnNew.TabIndex = 9;
            this.m_btnNew.Text = "New Data";
            this.m_btnNew.UseVisualStyleBackColor = true;
            this.m_btnNew.Click += new System.EventHandler(this.OnBtnNewClick);
            // 
            // m_cboGroups
            // 
            this.m_cboGroups.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cboGroups.FormattingEnabled = true;
            this.m_cboGroups.Location = new System.Drawing.Point(6, 12);
            this.m_cboGroups.Name = "m_cboGroups";
            this.m_cboGroups.Size = new System.Drawing.Size(343, 21);
            this.m_cboGroups.TabIndex = 8;
            this.m_cboGroups.SelectedIndexChanged += new System.EventHandler(this.OnSelectednGroupsChanged);
            // 
            // m_listViewData
            // 
            this.m_listViewData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_listViewData.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.m_ColHeadSymbol,
            this.m_ColHeadConstant,
            this.m_ColHeadSize,
            this.m_ColHeadSumm});
            this.m_listViewData.FullRowSelect = true;
            this.m_listViewData.GridLines = true;
            this.m_listViewData.HideSelection = false;
            this.m_listViewData.Location = new System.Drawing.Point(3, 69);
            this.m_listViewData.MultiSelect = false;
            this.m_listViewData.Name = "m_listViewData";
            this.m_listViewData.Size = new System.Drawing.Size(385, 211);
            this.m_listViewData.TabIndex = 7;
            this.m_listViewData.UseCompatibleStateImageBehavior = false;
            this.m_listViewData.View = System.Windows.Forms.View.Details;
            this.m_listViewData.SelectedIndexChanged += new System.EventHandler(this.listViewSelectedDataChanged);
            this.m_listViewData.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnListViewDataKeyDown);
            this.m_listViewData.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.OnlistViewItemDrag);
            // 
            // m_ColHeadSymbol
            // 
            this.m_ColHeadSymbol.Text = "Symbol";
            this.m_ColHeadSymbol.Width = 101;
            // 
            // m_ColHeadConstant
            // 
            this.m_ColHeadConstant.DisplayIndex = 2;
            this.m_ColHeadConstant.Text = "Constant";
            this.m_ColHeadConstant.Width = 54;
            // 
            // m_ColHeadSize
            // 
            this.m_ColHeadSize.DisplayIndex = 1;
            this.m_ColHeadSize.Text = "Size";
            this.m_ColHeadSize.Width = 40;
            // 
            // m_ColHeadSumm
            // 
            this.m_ColHeadSumm.Text = "Values";
            this.m_ColHeadSumm.Width = 157;
            // 
            // m_DataPropertyPage
            // 
            this.m_DataPropertyPage.BackColor = System.Drawing.Color.Transparent;
            this.m_DataPropertyPage.Data = null;
            this.m_DataPropertyPage.DataSizeAndSign = 0;
            this.m_DataPropertyPage.DefaultValue = 0;
            this.m_DataPropertyPage.Description = "";
            this.m_DataPropertyPage.Doc = null;
            this.m_DataPropertyPage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_DataPropertyPage.Enabled = false;
            this.m_DataPropertyPage.IsConstant = false;
            this.m_DataPropertyPage.Location = new System.Drawing.Point(0, 0);
            this.m_DataPropertyPage.MaxValue = 0;
            this.m_DataPropertyPage.MinValue = 0;
            this.m_DataPropertyPage.Name = "m_DataPropertyPage";
            this.m_DataPropertyPage.Size = new System.Drawing.Size(233, 437);
            this.m_DataPropertyPage.Symbol = "";
            this.m_DataPropertyPage.TabIndex = 11;
            // 
            // DataForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(636, 441);
            this.Controls.Add(this.splitContainer1);
            this.DoubleBuffered = true;
            this.MinimumSize = new System.Drawing.Size(604, 450);
            this.Name = "DataForm";
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Text = "Datas configuration";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OnFormClosing);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button m_btnManageGroups;
        private System.Windows.Forms.Button m_btnNew;
        private System.Windows.Forms.ComboBox m_cboGroups;
        private System.Windows.Forms.ListView m_listViewData;
        private System.Windows.Forms.ColumnHeader m_ColHeadSymbol;
        private System.Windows.Forms.ColumnHeader m_ColHeadConstant;
        private System.Windows.Forms.ColumnHeader m_ColHeadSize;
        private System.Windows.Forms.ColumnHeader m_ColHeadSumm;
        private DataPropertiesControl m_DataPropertyPage;

    }
}
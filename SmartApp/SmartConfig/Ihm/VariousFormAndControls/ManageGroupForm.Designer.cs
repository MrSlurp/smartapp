namespace SmartApp
{
    partial class ManageGroupForm
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
            this.m_listViewDataSrc = new System.Windows.Forms.ListView();
            this.m_ColHeadSymbol = new System.Windows.Forms.ColumnHeader();
            this.m_ColHeadConstant = new System.Windows.Forms.ColumnHeader();
            this.m_ColHeadSize = new System.Windows.Forms.ColumnHeader();
            this.m_ColHeadSumm = new System.Windows.Forms.ColumnHeader();
            this.m_ListViewDataDest = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.m_BtnSelectColor = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.m_TextGroupColor = new System.Windows.Forms.TextBox();
            this.m_TextGroupText = new System.Windows.Forms.TextBox();
            this.m_btnNewGroup = new System.Windows.Forms.Button();
            this.m_btnDeleteGroup = new System.Windows.Forms.Button();
            this.m_cboGroupSrc = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.m_cboGroupDest = new System.Windows.Forms.ComboBox();
            this.m_clrDlg = new System.Windows.Forms.ColorDialog();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_listViewDataSrc
            // 
            this.m_listViewDataSrc.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_listViewDataSrc.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.m_ColHeadSymbol,
            this.m_ColHeadConstant,
            this.m_ColHeadSize,
            this.m_ColHeadSumm});
            this.m_listViewDataSrc.FullRowSelect = true;
            this.m_listViewDataSrc.GridLines = true;
            this.m_listViewDataSrc.HideSelection = false;
            this.m_listViewDataSrc.Location = new System.Drawing.Point(3, 111);
            this.m_listViewDataSrc.MultiSelect = false;
            this.m_listViewDataSrc.Name = "m_listViewDataSrc";
            this.m_listViewDataSrc.Size = new System.Drawing.Size(374, 314);
            this.m_listViewDataSrc.TabIndex = 1;
            this.m_listViewDataSrc.UseCompatibleStateImageBehavior = false;
            this.m_listViewDataSrc.View = System.Windows.Forms.View.Details;
            this.m_listViewDataSrc.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.OnlistViewSrcItemDrag);
            // 
            // m_ColHeadSymbol
            // 
            this.m_ColHeadSymbol.Text = "Symbol";
            this.m_ColHeadSymbol.Width = 115;
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
            // m_ListViewDataDest
            // 
            this.m_ListViewDataDest.AllowDrop = true;
            this.m_ListViewDataDest.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_ListViewDataDest.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
            this.m_ListViewDataDest.FullRowSelect = true;
            this.m_ListViewDataDest.GridLines = true;
            this.m_ListViewDataDest.HideSelection = false;
            this.m_ListViewDataDest.Location = new System.Drawing.Point(3, 30);
            this.m_ListViewDataDest.MultiSelect = false;
            this.m_ListViewDataDest.Name = "m_ListViewDataDest";
            this.m_ListViewDataDest.Size = new System.Drawing.Size(371, 369);
            this.m_ListViewDataDest.TabIndex = 1;
            this.m_ListViewDataDest.UseCompatibleStateImageBehavior = false;
            this.m_ListViewDataDest.View = System.Windows.Forms.View.Details;
            this.m_ListViewDataDest.DragDrop += new System.Windows.Forms.DragEventHandler(this.OnListViewFrameDataDragDrop);
            this.m_ListViewDataDest.DragEnter += new System.Windows.Forms.DragEventHandler(this.OnListViewDestDragEnter);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Symbol";
            this.columnHeader1.Width = 106;
            // 
            // columnHeader2
            // 
            this.columnHeader2.DisplayIndex = 2;
            this.columnHeader2.Text = "Constant";
            this.columnHeader2.Width = 54;
            // 
            // columnHeader3
            // 
            this.columnHeader3.DisplayIndex = 1;
            this.columnHeader3.Text = "Size";
            this.columnHeader3.Width = 40;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Values";
            this.columnHeader4.Width = 163;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.m_BtnSelectColor);
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.m_TextGroupColor);
            this.splitContainer1.Panel1.Controls.Add(this.m_TextGroupText);
            this.splitContainer1.Panel1.Controls.Add(this.m_btnNewGroup);
            this.splitContainer1.Panel1.Controls.Add(this.m_btnDeleteGroup);
            this.splitContainer1.Panel1.Controls.Add(this.m_listViewDataSrc);
            this.splitContainer1.Panel1.Controls.Add(this.m_cboGroupSrc);
            this.splitContainer1.Panel1MinSize = 350;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.button1);
            this.splitContainer1.Panel2.Controls.Add(this.m_cboGroupDest);
            this.splitContainer1.Panel2.Controls.Add(this.m_ListViewDataDest);
            this.splitContainer1.Panel2MinSize = 350;
            this.splitContainer1.Size = new System.Drawing.Size(761, 430);
            this.splitContainer1.SplitterDistance = 380;
            this.splitContainer1.TabIndex = 2;
            // 
            // m_BtnSelectColor
            // 
            this.m_BtnSelectColor.Location = new System.Drawing.Point(47, 85);
            this.m_BtnSelectColor.Name = "m_BtnSelectColor";
            this.m_BtnSelectColor.Size = new System.Drawing.Size(68, 20);
            this.m_BtnSelectColor.TabIndex = 7;
            this.m_BtnSelectColor.Text = "Select";
            this.m_BtnSelectColor.UseVisualStyleBackColor = true;
            this.m_BtnSelectColor.Click += new System.EventHandler(this.OnBtnSelectColorClick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Color";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(28, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Text";
            // 
            // m_TextGroupColor
            // 
            this.m_TextGroupColor.BackColor = System.Drawing.Color.White;
            this.m_TextGroupColor.Location = new System.Drawing.Point(12, 85);
            this.m_TextGroupColor.Name = "m_TextGroupColor";
            this.m_TextGroupColor.ReadOnly = true;
            this.m_TextGroupColor.Size = new System.Drawing.Size(28, 20);
            this.m_TextGroupColor.TabIndex = 5;
            // 
            // m_TextGroupText
            // 
            this.m_TextGroupText.Location = new System.Drawing.Point(12, 46);
            this.m_TextGroupText.Name = "m_TextGroupText";
            this.m_TextGroupText.Size = new System.Drawing.Size(194, 20);
            this.m_TextGroupText.TabIndex = 5;
            this.m_TextGroupText.Validating += new System.ComponentModel.CancelEventHandler(this.OnTextGroupTextValidating);
            // 
            // m_btnNewGroup
            // 
            this.m_btnNewGroup.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnNewGroup.Location = new System.Drawing.Point(272, 59);
            this.m_btnNewGroup.Name = "m_btnNewGroup";
            this.m_btnNewGroup.Size = new System.Drawing.Size(105, 23);
            this.m_btnNewGroup.TabIndex = 4;
            this.m_btnNewGroup.Text = "New group";
            this.m_btnNewGroup.UseVisualStyleBackColor = true;
            this.m_btnNewGroup.Click += new System.EventHandler(this.OnbtnNewGroupClick);
            // 
            // m_btnDeleteGroup
            // 
            this.m_btnDeleteGroup.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnDeleteGroup.Location = new System.Drawing.Point(272, 30);
            this.m_btnDeleteGroup.Name = "m_btnDeleteGroup";
            this.m_btnDeleteGroup.Size = new System.Drawing.Size(105, 23);
            this.m_btnDeleteGroup.TabIndex = 4;
            this.m_btnDeleteGroup.Text = "Delete group";
            this.m_btnDeleteGroup.UseVisualStyleBackColor = true;
            this.m_btnDeleteGroup.Click += new System.EventHandler(this.OnbtnDeleteGroupClick);
            // 
            // m_cboGroupSrc
            // 
            this.m_cboGroupSrc.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_cboGroupSrc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cboGroupSrc.FormattingEnabled = true;
            this.m_cboGroupSrc.Location = new System.Drawing.Point(3, 3);
            this.m_cboGroupSrc.Name = "m_cboGroupSrc";
            this.m_cboGroupSrc.Size = new System.Drawing.Size(374, 21);
            this.m_cboGroupSrc.TabIndex = 3;
            this.m_cboGroupSrc.SelectedIndexChanged += new System.EventHandler(this.OnGroupSrcSelectedIndexChanged);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button1.Location = new System.Drawing.Point(4, 405);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(370, 22);
            this.button1.TabIndex = 4;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // m_cboGroupDest
            // 
            this.m_cboGroupDest.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_cboGroupDest.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cboGroupDest.FormattingEnabled = true;
            this.m_cboGroupDest.Location = new System.Drawing.Point(3, 3);
            this.m_cboGroupDest.Name = "m_cboGroupDest";
            this.m_cboGroupDest.Size = new System.Drawing.Size(371, 21);
            this.m_cboGroupDest.TabIndex = 3;
            this.m_cboGroupDest.SelectedIndexChanged += new System.EventHandler(this.OnGroupDestSelectedIndexChanged);
            // 
            // ManageGroupForm
            // 
            this.AcceptButton = this.button1;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(761, 430);
            this.Controls.Add(this.splitContainer1);
            this.DoubleBuffered = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(760, 460);
            this.Name = "ManageGroupForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Manage Groups";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView m_listViewDataSrc;
        private System.Windows.Forms.ColumnHeader m_ColHeadSymbol;
        private System.Windows.Forms.ColumnHeader m_ColHeadConstant;
        private System.Windows.Forms.ColumnHeader m_ColHeadSize;
        private System.Windows.Forms.ColumnHeader m_ColHeadSumm;
        private System.Windows.Forms.ListView m_ListViewDataDest;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ComboBox m_cboGroupSrc;
        private System.Windows.Forms.ComboBox m_cboGroupDest;
        private System.Windows.Forms.Button m_btnDeleteGroup;
        private System.Windows.Forms.TextBox m_TextGroupText;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox m_TextGroupColor;
        private System.Windows.Forms.Button m_BtnSelectColor;
        private System.Windows.Forms.ColorDialog m_clrDlg;
        private System.Windows.Forms.Button m_btnNewGroup;
        private System.Windows.Forms.Button button1;
    }
}
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
            this.m_clrDlg = new System.Windows.Forms.ColorDialog();
            this.m_BtnSelectColor = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.m_TextGroupColor = new System.Windows.Forms.TextBox();
            this.m_TextGroupText = new System.Windows.Forms.TextBox();
            this.m_btnNewGroup = new System.Windows.Forms.Button();
            this.m_ListViewDataDest = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
            this.m_btnDeleteGroup = new System.Windows.Forms.Button();
            this.m_cboGroupDest = new System.Windows.Forms.ComboBox();
            this.m_listViewDataSrc = new System.Windows.Forms.ListView();
            this.m_ColHeadSymbol = new System.Windows.Forms.ColumnHeader();
            this.m_ColHeadConstant = new System.Windows.Forms.ColumnHeader();
            this.m_ColHeadSize = new System.Windows.Forms.ColumnHeader();
            this.m_ColHeadSumm = new System.Windows.Forms.ColumnHeader();
            this.m_cboGroupSrc = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.btnMoveRight = new System.Windows.Forms.Button();
            this.btnMoveLeft = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // m_BtnSelectColor
            // 
            this.m_BtnSelectColor.Location = new System.Drawing.Point(47, 93);
            this.m_BtnSelectColor.Name = "m_BtnSelectColor";
            this.m_BtnSelectColor.Size = new System.Drawing.Size(68, 20);
            this.m_BtnSelectColor.TabIndex = 19;
            this.m_BtnSelectColor.Text = "Select";
            this.m_BtnSelectColor.UseVisualStyleBackColor = true;
            this.m_BtnSelectColor.Click += new System.EventHandler(this.OnBtnSelectColorClick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 77);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 13);
            this.label2.TabIndex = 17;
            this.label2.Text = "Color";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(28, 13);
            this.label1.TabIndex = 18;
            this.label1.Text = "Text";
            // 
            // m_TextGroupColor
            // 
            this.m_TextGroupColor.BackColor = System.Drawing.Color.White;
            this.m_TextGroupColor.Location = new System.Drawing.Point(12, 93);
            this.m_TextGroupColor.Name = "m_TextGroupColor";
            this.m_TextGroupColor.ReadOnly = true;
            this.m_TextGroupColor.Size = new System.Drawing.Size(28, 20);
            this.m_TextGroupColor.TabIndex = 16;
            // 
            // m_TextGroupText
            // 
            this.m_TextGroupText.Location = new System.Drawing.Point(12, 54);
            this.m_TextGroupText.Name = "m_TextGroupText";
            this.m_TextGroupText.Size = new System.Drawing.Size(194, 20);
            this.m_TextGroupText.TabIndex = 15;
            this.m_TextGroupText.Validating += new System.ComponentModel.CancelEventHandler(this.OnTextGroupTextValidating);
            // 
            // m_btnNewGroup
            // 
            this.m_btnNewGroup.Location = new System.Drawing.Point(236, 68);
            this.m_btnNewGroup.Name = "m_btnNewGroup";
            this.m_btnNewGroup.Size = new System.Drawing.Size(146, 23);
            this.m_btnNewGroup.TabIndex = 14;
            this.m_btnNewGroup.Text = "Add new group";
            this.m_btnNewGroup.UseVisualStyleBackColor = true;
            this.m_btnNewGroup.Click += new System.EventHandler(this.OnbtnNewGroupClick);
            // 
            // m_ListViewDataDest
            // 
            this.m_ListViewDataDest.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
            this.m_ListViewDataDest.FullRowSelect = true;
            this.m_ListViewDataDest.GridLines = true;
            this.m_ListViewDataDest.HideSelection = false;
            this.m_ListViewDataDest.Location = new System.Drawing.Point(469, 38);
            this.m_ListViewDataDest.Name = "m_ListViewDataDest";
            this.m_ListViewDataDest.Size = new System.Drawing.Size(367, 395);
            this.m_ListViewDataDest.TabIndex = 9;
            this.m_ListViewDataDest.UseCompatibleStateImageBehavior = false;
            this.m_ListViewDataDest.View = System.Windows.Forms.View.Details;
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
            // m_btnDeleteGroup
            // 
            this.m_btnDeleteGroup.Location = new System.Drawing.Point(236, 39);
            this.m_btnDeleteGroup.Name = "m_btnDeleteGroup";
            this.m_btnDeleteGroup.Size = new System.Drawing.Size(146, 23);
            this.m_btnDeleteGroup.TabIndex = 12;
            this.m_btnDeleteGroup.Text = "Delete current group";
            this.m_btnDeleteGroup.UseVisualStyleBackColor = true;
            this.m_btnDeleteGroup.Click += new System.EventHandler(this.OnbtnDeleteGroupClick);
            // 
            // m_cboGroupDest
            // 
            this.m_cboGroupDest.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cboGroupDest.FormattingEnabled = true;
            this.m_cboGroupDest.Location = new System.Drawing.Point(469, 12);
            this.m_cboGroupDest.Name = "m_cboGroupDest";
            this.m_cboGroupDest.Size = new System.Drawing.Size(367, 21);
            this.m_cboGroupDest.TabIndex = 11;
            this.m_cboGroupDest.SelectedIndexChanged += new System.EventHandler(this.OnGroupDestSelectedIndexChanged);
            // 
            // m_listViewDataSrc
            // 
            this.m_listViewDataSrc.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.m_ColHeadSymbol,
            this.m_ColHeadConstant,
            this.m_ColHeadSize,
            this.m_ColHeadSumm});
            this.m_listViewDataSrc.FullRowSelect = true;
            this.m_listViewDataSrc.GridLines = true;
            this.m_listViewDataSrc.HideSelection = false;
            this.m_listViewDataSrc.Location = new System.Drawing.Point(12, 119);
            this.m_listViewDataSrc.Name = "m_listViewDataSrc";
            this.m_listViewDataSrc.Size = new System.Drawing.Size(370, 314);
            this.m_listViewDataSrc.TabIndex = 8;
            this.m_listViewDataSrc.UseCompatibleStateImageBehavior = false;
            this.m_listViewDataSrc.View = System.Windows.Forms.View.Details;
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
            // m_cboGroupSrc
            // 
            this.m_cboGroupSrc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cboGroupSrc.FormattingEnabled = true;
            this.m_cboGroupSrc.Location = new System.Drawing.Point(12, 12);
            this.m_cboGroupSrc.Name = "m_cboGroupSrc";
            this.m_cboGroupSrc.Size = new System.Drawing.Size(370, 21);
            this.m_cboGroupSrc.TabIndex = 10;
            this.m_cboGroupSrc.SelectedIndexChanged += new System.EventHandler(this.OnGroupSrcSelectedIndexChanged);
            // 
            // button1
            // 
            this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button1.Location = new System.Drawing.Point(236, 439);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(366, 22);
            this.button1.TabIndex = 13;
            this.button1.Text = "Close";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // btnMoveRight
            // 
            this.btnMoveRight.Location = new System.Drawing.Point(388, 253);
            this.btnMoveRight.Name = "btnMoveRight";
            this.btnMoveRight.Size = new System.Drawing.Size(75, 23);
            this.btnMoveRight.TabIndex = 20;
            this.btnMoveRight.Text = "==>";
            this.btnMoveRight.UseVisualStyleBackColor = true;
            this.btnMoveRight.Click += new System.EventHandler(this.btnMoveRight_Click);
            // 
            // btnMoveLeft
            // 
            this.btnMoveLeft.Location = new System.Drawing.Point(388, 282);
            this.btnMoveLeft.Name = "btnMoveLeft";
            this.btnMoveLeft.Size = new System.Drawing.Size(75, 23);
            this.btnMoveLeft.TabIndex = 20;
            this.btnMoveLeft.Text = "<==";
            this.btnMoveLeft.UseVisualStyleBackColor = true;
            this.btnMoveLeft.Click += new System.EventHandler(this.btnMoveLeft_Click);
            // 
            // ManageGroupForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(845, 468);
            this.Controls.Add(this.btnMoveLeft);
            this.Controls.Add(this.btnMoveRight);
            this.Controls.Add(this.m_BtnSelectColor);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.m_TextGroupColor);
            this.Controls.Add(this.m_TextGroupText);
            this.Controls.Add(this.m_btnNewGroup);
            this.Controls.Add(this.m_ListViewDataDest);
            this.Controls.Add(this.m_btnDeleteGroup);
            this.Controls.Add(this.m_cboGroupDest);
            this.Controls.Add(this.m_listViewDataSrc);
            this.Controls.Add(this.m_cboGroupSrc);
            this.Controls.Add(this.button1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(760, 460);
            this.Name = "ManageGroupForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Manage Groups";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ColorDialog m_clrDlg;
        private System.Windows.Forms.Button m_BtnSelectColor;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox m_TextGroupColor;
        private System.Windows.Forms.TextBox m_TextGroupText;
        private System.Windows.Forms.Button m_btnNewGroup;
        private System.Windows.Forms.ListView m_ListViewDataDest;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.Button m_btnDeleteGroup;
        private System.Windows.Forms.ComboBox m_cboGroupDest;
        private System.Windows.Forms.ListView m_listViewDataSrc;
        private System.Windows.Forms.ColumnHeader m_ColHeadSymbol;
        private System.Windows.Forms.ColumnHeader m_ColHeadConstant;
        private System.Windows.Forms.ColumnHeader m_ColHeadSize;
        private System.Windows.Forms.ColumnHeader m_ColHeadSumm;
        private System.Windows.Forms.ComboBox m_cboGroupSrc;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnMoveRight;
        private System.Windows.Forms.Button btnMoveLeft;
    }
}
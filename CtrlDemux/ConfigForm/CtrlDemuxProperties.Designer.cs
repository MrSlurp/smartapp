namespace CtrlDemux
{
    partial class CtrlDemuxProperties
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
            this.btnMoveDown = new System.Windows.Forms.Button();
            this.btnMoveUp = new System.Windows.Forms.Button();
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
            this.edtAddrData.Location = new System.Drawing.Point(12, 26);
            this.edtAddrData.Name = "edtAddrData";
            this.edtAddrData.Size = new System.Drawing.Size(215, 20);
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
            this.edtValueData.Location = new System.Drawing.Point(12, 70);
            this.edtValueData.Name = "edtValueData";
            this.edtValueData.Size = new System.Drawing.Size(215, 20);
            this.edtValueData.TabIndex = 1;
            // 
            // btnPickAddr
            // 
            this.btnPickAddr.Location = new System.Drawing.Point(233, 24);
            this.btnPickAddr.Name = "btnPickAddr";
            this.btnPickAddr.Size = new System.Drawing.Size(69, 23);
            this.btnPickAddr.TabIndex = 2;
            this.btnPickAddr.Text = "Pick";
            this.btnPickAddr.UseVisualStyleBackColor = true;
            this.btnPickAddr.Click += new System.EventHandler(this.btnPickAddr_Click);
            // 
            // btnPickValue
            // 
            this.btnPickValue.Location = new System.Drawing.Point(233, 68);
            this.btnPickValue.Name = "btnPickValue";
            this.btnPickValue.Size = new System.Drawing.Size(69, 23);
            this.btnPickValue.TabIndex = 2;
            this.btnPickValue.Text = "Pick";
            this.btnPickValue.UseVisualStyleBackColor = true;
            this.btnPickValue.Click += new System.EventHandler(this.btnPickValue_Click);
            // 
            // m_listViewData
            // 
            this.m_listViewData.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.m_listViewData.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.m_ColHeadSymbol,
            this.m_ColHeadSize});
            this.m_listViewData.FullRowSelect = true;
            this.m_listViewData.GridLines = true;
            this.m_listViewData.HideSelection = false;
            this.m_listViewData.Location = new System.Drawing.Point(12, 126);
            this.m_listViewData.MultiSelect = false;
            this.m_listViewData.Name = "m_listViewData";
            this.m_listViewData.Size = new System.Drawing.Size(365, 212);
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
            this.m_ColHeadSize.Width = 51;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(383, 126);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(87, 23);
            this.btnAdd.TabIndex = 10;
            this.btnAdd.Text = "Add Data";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.Location = new System.Drawing.Point(382, 315);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(84, 23);
            this.btnRemove.TabIndex = 10;
            this.btnRemove.Text = "Remove Data";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // btnMoveDown
            // 
            this.btnMoveDown.Location = new System.Drawing.Point(383, 233);
            this.btnMoveDown.Name = "btnMoveDown";
            this.btnMoveDown.Size = new System.Drawing.Size(87, 23);
            this.btnMoveDown.TabIndex = 12;
            this.btnMoveDown.Text = "Move Down";
            this.btnMoveDown.UseVisualStyleBackColor = true;
            this.btnMoveDown.Click += new System.EventHandler(this.btnMoveDown_Click);
            // 
            // btnMoveUp
            // 
            this.btnMoveUp.Location = new System.Drawing.Point(382, 204);
            this.btnMoveUp.Name = "btnMoveUp";
            this.btnMoveUp.Size = new System.Drawing.Size(87, 23);
            this.btnMoveUp.TabIndex = 12;
            this.btnMoveUp.Text = "Move Up";
            this.btnMoveUp.UseVisualStyleBackColor = true;
            this.btnMoveUp.Click += new System.EventHandler(this.btnMoveUp_Click);
            // 
            // CtrlDemuxProperties
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnMoveUp);
            this.Controls.Add(this.btnMoveDown);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.m_listViewData);
            this.Controls.Add(this.btnPickValue);
            this.Controls.Add(this.btnPickAddr);
            this.Controls.Add(this.edtValueData);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.edtAddrData);
            this.Controls.Add(this.label1);
            this.Name = "CtrlDemuxProperties";
            this.Size = new System.Drawing.Size(481, 354);
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
        private System.Windows.Forms.Button btnMoveDown;
        private System.Windows.Forms.Button btnMoveUp;
    }
}
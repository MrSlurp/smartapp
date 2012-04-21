namespace SmartApp.Ihm
{
    partial class FrameForm
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
            this.m_MainSplitter = new System.Windows.Forms.SplitContainer();
            this.m_btnNew = new System.Windows.Forms.Button();
            this.m_listViewFrames = new System.Windows.Forms.ListView();
            this.m_ColSymbol = new System.Windows.Forms.ColumnHeader();
            this.m_colDataCount = new System.Windows.Forms.ColumnHeader();
            this.m_TabCtrl = new System.Windows.Forms.TabControl();
            this.m_TabPageFrameProp = new System.Windows.Forms.TabPage();
            this.m_TabPageFrameData = new System.Windows.Forms.TabPage();
            this.m_ListViewFrameData = new System.Windows.Forms.ListView();
            this.m_colDataSymbol = new System.Windows.Forms.ColumnHeader();
            this.m_colDataSize = new System.Windows.Forms.ColumnHeader();
            this.m_colConst = new System.Windows.Forms.ColumnHeader();
            this.m_colDefVal = new System.Windows.Forms.ColumnHeader();
            this.m_MainSplitter.Panel1.SuspendLayout();
            this.m_MainSplitter.Panel2.SuspendLayout();
            this.m_MainSplitter.SuspendLayout();
            this.m_TabCtrl.SuspendLayout();
            this.m_TabPageFrameData.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_MainSplitter
            // 
            this.m_MainSplitter.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.m_MainSplitter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_MainSplitter.Location = new System.Drawing.Point(0, 0);
            this.m_MainSplitter.MinimumSize = new System.Drawing.Size(680, 420);
            this.m_MainSplitter.Name = "m_MainSplitter";
            // 
            // m_MainSplitter.Panel1
            // 
            this.m_MainSplitter.Panel1.Controls.Add(this.m_btnNew);
            this.m_MainSplitter.Panel1.Controls.Add(this.m_listViewFrames);
            this.m_MainSplitter.Panel1MinSize = 240;
            // 
            // m_MainSplitter.Panel2
            // 
            this.m_MainSplitter.Panel2.Controls.Add(this.m_TabCtrl);
            this.m_MainSplitter.Panel2MinSize = 436;
            this.m_MainSplitter.Size = new System.Drawing.Size(680, 420);
            this.m_MainSplitter.SplitterDistance = 240;
            this.m_MainSplitter.TabIndex = 0;
            // 
            // m_btnNew
            // 
            this.m_btnNew.Location = new System.Drawing.Point(3, 10);
            this.m_btnNew.Name = "m_btnNew";
            this.m_btnNew.Size = new System.Drawing.Size(168, 23);
            this.m_btnNew.TabIndex = 3;
            this.m_btnNew.Text = "New Frame";
            this.m_btnNew.UseVisualStyleBackColor = true;
            this.m_btnNew.Click += new System.EventHandler(this.OnbtnNewClick);
            // 
            // m_listViewFrames
            // 
            this.m_listViewFrames.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_listViewFrames.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.m_ColSymbol,
            this.m_colDataCount});
            this.m_listViewFrames.FullRowSelect = true;
            this.m_listViewFrames.GridLines = true;
            this.m_listViewFrames.HideSelection = false;
            this.m_listViewFrames.LabelWrap = false;
            this.m_listViewFrames.Location = new System.Drawing.Point(0, 40);
            this.m_listViewFrames.MultiSelect = false;
            this.m_listViewFrames.Name = "m_listViewFrames";
            this.m_listViewFrames.Size = new System.Drawing.Size(234, 376);
            this.m_listViewFrames.TabIndex = 2;
            this.m_listViewFrames.UseCompatibleStateImageBehavior = false;
            this.m_listViewFrames.View = System.Windows.Forms.View.Details;
            this.m_listViewFrames.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnListViewKeyDown);
            // 
            // m_ColSymbol
            // 
            this.m_ColSymbol.Text = "Symbol";
            this.m_ColSymbol.Width = 134;
            // 
            // m_colDataCount
            // 
            this.m_colDataCount.Text = "Number of data";
            this.m_colDataCount.Width = 94;
            // 
            // m_TabCtrl
            // 
            this.m_TabCtrl.Controls.Add(this.m_TabPageFrameProp);
            this.m_TabCtrl.Controls.Add(this.m_TabPageFrameData);
            this.m_TabCtrl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_TabCtrl.Location = new System.Drawing.Point(0, 0);
            this.m_TabCtrl.Name = "m_TabCtrl";
            this.m_TabCtrl.SelectedIndex = 0;
            this.m_TabCtrl.Size = new System.Drawing.Size(432, 416);
            this.m_TabCtrl.TabIndex = 3;
            // 
            // m_TabPageFrameProp
            // 
            this.m_TabPageFrameProp.Location = new System.Drawing.Point(4, 22);
            this.m_TabPageFrameProp.Name = "m_TabPageFrameProp";
            this.m_TabPageFrameProp.Padding = new System.Windows.Forms.Padding(3);
            this.m_TabPageFrameProp.Size = new System.Drawing.Size(424, 390);
            this.m_TabPageFrameProp.TabIndex = 0;
            this.m_TabPageFrameProp.Text = "Frame properties";
            this.m_TabPageFrameProp.UseVisualStyleBackColor = true;
            // 
            // m_TabPageFrameData
            // 
            this.m_TabPageFrameData.Controls.Add(this.m_ListViewFrameData);
            this.m_TabPageFrameData.Location = new System.Drawing.Point(4, 22);
            this.m_TabPageFrameData.Name = "m_TabPageFrameData";
            this.m_TabPageFrameData.Padding = new System.Windows.Forms.Padding(3);
            this.m_TabPageFrameData.Size = new System.Drawing.Size(424, 390);
            this.m_TabPageFrameData.TabIndex = 1;
            this.m_TabPageFrameData.Text = "Frame datas";
            this.m_TabPageFrameData.UseVisualStyleBackColor = true;
            // 
            // m_ListViewFrameData
            // 
            this.m_ListViewFrameData.AllowDrop = true;
            this.m_ListViewFrameData.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.m_colDataSymbol,
            this.m_colDataSize,
            this.m_colConst,
            this.m_colDefVal});
            this.m_ListViewFrameData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_ListViewFrameData.FullRowSelect = true;
            this.m_ListViewFrameData.GridLines = true;
            this.m_ListViewFrameData.HideSelection = false;
            this.m_ListViewFrameData.Location = new System.Drawing.Point(3, 3);
            this.m_ListViewFrameData.MultiSelect = false;
            this.m_ListViewFrameData.Name = "m_ListViewFrameData";
            this.m_ListViewFrameData.Size = new System.Drawing.Size(418, 384);
            this.m_ListViewFrameData.TabIndex = 0;
            this.m_ListViewFrameData.UseCompatibleStateImageBehavior = false;
            this.m_ListViewFrameData.View = System.Windows.Forms.View.Details;
            this.m_ListViewFrameData.DragDrop += new System.Windows.Forms.DragEventHandler(this.OnListViewFrameDataDragDrop);
            this.m_ListViewFrameData.DragEnter += new System.Windows.Forms.DragEventHandler(this.OnListViewFrameDataDragEnter);
            this.m_ListViewFrameData.DragLeave += new System.EventHandler(this.OnListViewFrameDataDragLeave);
            this.m_ListViewFrameData.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnListViewFrameDataKeyDown);
            this.m_ListViewFrameData.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.OnListViewFrameDataItemDrag);
            this.m_ListViewFrameData.DragOver += new System.Windows.Forms.DragEventHandler(this.OnListViewFrameDataDragOver);
            // 
            // m_colDataSymbol
            // 
            this.m_colDataSymbol.Text = "Data Symbol";
            this.m_colDataSymbol.Width = 176;
            // 
            // m_colDataSize
            // 
            this.m_colDataSize.Text = "Size";
            // 
            // m_colConst
            // 
            this.m_colConst.Text = "Constant";
            this.m_colConst.Width = 78;
            // 
            // m_colDefVal
            // 
            this.m_colDefVal.Text = "Default Value";
            this.m_colDefVal.Width = 102;
            // 
            // FrameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(678, 420);
            this.Controls.Add(this.m_MainSplitter);
            this.MinimumSize = new System.Drawing.Size(686, 440);
            this.Name = "FrameForm";
            this.ShowIcon = false;
            this.Text = "Frames configuration";
            this.m_MainSplitter.Panel1.ResumeLayout(false);
            this.m_MainSplitter.Panel2.ResumeLayout(false);
            this.m_MainSplitter.ResumeLayout(false);
            this.m_TabCtrl.ResumeLayout(false);
            this.m_TabPageFrameData.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer m_MainSplitter;
        private System.Windows.Forms.ListView m_listViewFrames;
        private System.Windows.Forms.TabControl m_TabCtrl;
        private System.Windows.Forms.TabPage m_TabPageFrameProp;
        private System.Windows.Forms.TabPage m_TabPageFrameData;
        private System.Windows.Forms.ListView m_ListViewFrameData;
        private System.Windows.Forms.ColumnHeader m_ColSymbol;
        private System.Windows.Forms.ColumnHeader m_colDataCount;
        private System.Windows.Forms.ColumnHeader m_colDataSymbol;
        private System.Windows.Forms.ColumnHeader m_colDataSize;
        private System.Windows.Forms.ColumnHeader m_colConst;
        private System.Windows.Forms.ColumnHeader m_colDefVal;
        private System.Windows.Forms.Button m_btnNew;

    }
}
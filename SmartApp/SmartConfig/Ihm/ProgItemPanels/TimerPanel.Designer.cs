namespace SmartApp.Ihm.ProgItemPanels
{
    partial class TimerPanel
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.m_btnNew = new System.Windows.Forms.Button();
            this.m_listView = new System.Windows.Forms.ListView();
            this.m_colHeadSymbol = new System.Windows.Forms.ColumnHeader();
            this.m_colPeriod = new System.Windows.Forms.ColumnHeader();
            this.m_PanelTimerProp = new SmartApp.Ihm.TimerPropertiesControl();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.m_btnNew);
            this.splitContainer1.Panel1.Controls.Add(this.m_listView);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.m_PanelTimerProp);
            this.splitContainer1.Size = new System.Drawing.Size(610, 351);
            this.splitContainer1.SplitterDistance = 203;
            this.splitContainer1.TabIndex = 1;
            // 
            // m_btnNew
            // 
            this.m_btnNew.Location = new System.Drawing.Point(3, 4);
            this.m_btnNew.Name = "m_btnNew";
            this.m_btnNew.Size = new System.Drawing.Size(88, 21);
            this.m_btnNew.TabIndex = 2;
            this.m_btnNew.Text = "new";
            this.m_btnNew.UseVisualStyleBackColor = true;
            this.m_btnNew.Click += new System.EventHandler(this.OnbtnNewClick);
            // 
            // m_listView
            // 
            this.m_listView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_listView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.m_colHeadSymbol,
            this.m_colPeriod});
            this.m_listView.FullRowSelect = true;
            this.m_listView.GridLines = true;
            this.m_listView.HideSelection = false;
            this.m_listView.Location = new System.Drawing.Point(0, 31);
            this.m_listView.Name = "m_listView";
            this.m_listView.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.m_listView.Size = new System.Drawing.Size(199, 316);
            this.m_listView.TabIndex = 0;
            this.m_listView.UseCompatibleStateImageBehavior = false;
            this.m_listView.View = System.Windows.Forms.View.Details;
            this.m_listView.SelectedIndexChanged += new System.EventHandler(this.listViewSelectedTimerChanged);
            this.m_listView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnListViewKeyDown);
            // 
            // m_colHeadSymbol
            // 
            this.m_colHeadSymbol.Text = "Symbol";
            this.m_colHeadSymbol.Width = 130;
            // 
            // m_colPeriod
            // 
            this.m_colPeriod.Text = "Period";
            this.m_colPeriod.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.m_colPeriod.Width = 45;
            // 
            // m_PanelTimerProp
            // 
            this.m_PanelTimerProp.BackColor = System.Drawing.Color.Transparent;
            this.m_PanelTimerProp.Description = "";
            this.m_PanelTimerProp.Doc = null;
            this.m_PanelTimerProp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_PanelTimerProp.Location = new System.Drawing.Point(0, 0);
            this.m_PanelTimerProp.Name = "m_PanelTimerProp";
            this.m_PanelTimerProp.Period = 50;
            this.m_PanelTimerProp.ScriptLines = new string[0];
            this.m_PanelTimerProp.Size = new System.Drawing.Size(399, 347);
            this.m_PanelTimerProp.Symbol = "";
            this.m_PanelTimerProp.TabIndex = 0;
            this.m_PanelTimerProp.Timer = null;
            // 
            // TimerPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "TimerPanel";
            this.Size = new System.Drawing.Size(610, 351);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ListView m_listView;
        private TimerPropertiesControl m_PanelTimerProp;
        private System.Windows.Forms.ColumnHeader m_colHeadSymbol;
        private System.Windows.Forms.ColumnHeader m_colPeriod;
        private System.Windows.Forms.Button m_btnNew;
    }
}

namespace SmartApp.Ihm.ProgItemPanels
{
    partial class LoggerPanel
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
            this.m_listViewLogger = new System.Windows.Forms.ListView();
            this.m_ColSymbol = new System.Windows.Forms.ColumnHeader();
            this.m_panelLogProp = new SmartApp.Ihm.LoggerPropertiesControl();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.m_btnNew);
            this.splitContainer1.Panel1.Controls.Add(this.m_listViewLogger);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.m_panelLogProp);
            this.splitContainer1.Size = new System.Drawing.Size(628, 363);
            this.splitContainer1.SplitterDistance = 209;
            this.splitContainer1.TabIndex = 1;
            // 
            // m_btnNew
            // 
            this.m_btnNew.Location = new System.Drawing.Point(3, 5);
            this.m_btnNew.Name = "m_btnNew";
            this.m_btnNew.Size = new System.Drawing.Size(75, 23);
            this.m_btnNew.TabIndex = 1;
            this.m_btnNew.Text = "New";
            this.m_btnNew.UseVisualStyleBackColor = true;
            this.m_btnNew.Click += new System.EventHandler(this.OnbtnNewClick);
            // 
            // m_listViewLogger
            // 
            this.m_listViewLogger.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_listViewLogger.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.m_ColSymbol});
            this.m_listViewLogger.FullRowSelect = true;
            this.m_listViewLogger.GridLines = true;
            this.m_listViewLogger.HideSelection = false;
            this.m_listViewLogger.Location = new System.Drawing.Point(0, 34);
            this.m_listViewLogger.MultiSelect = false;
            this.m_listViewLogger.Name = "m_listViewLogger";
            this.m_listViewLogger.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.m_listViewLogger.Size = new System.Drawing.Size(209, 329);
            this.m_listViewLogger.TabIndex = 0;
            this.m_listViewLogger.UseCompatibleStateImageBehavior = false;
            this.m_listViewLogger.View = System.Windows.Forms.View.Details;
            this.m_listViewLogger.SelectedIndexChanged += new System.EventHandler(this.listViewSelectedLoggerChanged);
            this.m_listViewLogger.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnListViewKeyDown);
            // 
            // m_ColSymbol
            // 
            this.m_ColSymbol.Text = "Logger Symbol";
            this.m_ColSymbol.Width = 150;
            // 
            // m_panelLogProp
            // 
            this.m_panelLogProp.BackColor = System.Drawing.Color.Transparent;
            this.m_panelLogProp.Description = "";
            this.m_panelLogProp.Doc = null;
            this.m_panelLogProp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_panelLogProp.Location = new System.Drawing.Point(0, 0);
            this.m_panelLogProp.LogFile = ".csv";
            this.m_panelLogProp.Logger = null;
            this.m_panelLogProp.LogType = "STANDARD";
            this.m_panelLogProp.Name = "m_panelLogProp";
            this.m_panelLogProp.Period = 200;
            this.m_panelLogProp.Size = new System.Drawing.Size(415, 363);
            this.m_panelLogProp.Symbol = "";
            this.m_panelLogProp.TabIndex = 0;
            // 
            // LoggerPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "LoggerPanel";
            this.Size = new System.Drawing.Size(628, 363);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ListView m_listViewLogger;
        private System.Windows.Forms.Button m_btnNew;
        private SmartApp.Ihm.LoggerPropertiesControl m_panelLogProp;
        private System.Windows.Forms.ColumnHeader m_ColSymbol;
    }
}

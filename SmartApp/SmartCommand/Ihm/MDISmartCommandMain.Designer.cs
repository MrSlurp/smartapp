using CommonLib;
namespace SmartApp
{
    partial class MDISmartCommandMain
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MDISmartCommandMain));
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemTraceConfig = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemOpenDebugConsole = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.m_MruFiles = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.menuItemExit = new System.Windows.Forms.ToolStripMenuItem();
            this.viewMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.toolBarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.indexToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pluginsVersionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.openToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.m_StatusBar = new System.Windows.Forms.StatusStrip();
            this.dataGridMonitor = new System.Windows.Forms.DataGridView();
            this.colProjName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colProjCnxStatus = new System.Windows.Forms.DataGridViewImageColumn();
            this.colRunStatus = new System.Windows.Forms.DataGridViewImageColumn();
            this.colBtnConnectStart = new System.Windows.Forms.DataGridViewButtonColumn();
            this.m_trayIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.appEventLogPanel = new SmartApp.AppEventLogPanel();
            this.menuStrip.SuspendLayout();
            this.toolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridMonitor)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileMenu,
            this.viewMenu,
            this.toolsMenu,
            this.helpMenu});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(496, 24);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "MenuStrip";
            // 
            // fileMenu
            // 
            this.fileMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemOpen,
            this.menuItemTraceConfig,
            this.menuItemOpenDebugConsole,
            this.toolStripSeparator3,
            this.m_MruFiles,
            this.toolStripSeparator1,
            this.menuItemExit});
            this.fileMenu.ImageTransparentColor = System.Drawing.SystemColors.ActiveBorder;
            this.fileMenu.Name = "fileMenu";
            this.fileMenu.Size = new System.Drawing.Size(35, 20);
            this.fileMenu.Text = "&File";
            // 
            // menuItemOpen
            // 
            this.menuItemOpen.Image = ((System.Drawing.Image)(resources.GetObject("menuItemOpen.Image")));
            this.menuItemOpen.ImageTransparentColor = System.Drawing.Color.Black;
            this.menuItemOpen.Name = "menuItemOpen";
            this.menuItemOpen.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.menuItemOpen.Size = new System.Drawing.Size(192, 22);
            this.menuItemOpen.Text = "&Open Solution";
            this.menuItemOpen.Click += new System.EventHandler(this.menuItemOpen_Click);
            // 
            // menuItemTraceConfig
            // 
            this.menuItemTraceConfig.Name = "menuItemTraceConfig";
            this.menuItemTraceConfig.Size = new System.Drawing.Size(192, 22);
            this.menuItemTraceConfig.Text = "Trace Config";
            this.menuItemTraceConfig.Visible = false;
            this.menuItemTraceConfig.Click += new System.EventHandler(this.menuItemLogConfig_Click);
            // 
            // menuItemOpenDebugConsole
            // 
            this.menuItemOpenDebugConsole.Name = "menuItemOpenDebugConsole";
            this.menuItemOpenDebugConsole.Size = new System.Drawing.Size(192, 22);
            this.menuItemOpenDebugConsole.Text = "Open Debug Console";
            this.menuItemOpenDebugConsole.Visible = false;
            this.menuItemOpenDebugConsole.Click += new System.EventHandler(this.menuItemOpenDebugConsole_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(189, 6);
            // 
            // m_MruFiles
            // 
            this.m_MruFiles.Enabled = false;
            this.m_MruFiles.Name = "m_MruFiles";
            this.m_MruFiles.Size = new System.Drawing.Size(192, 22);
            this.m_MruFiles.Text = "Recent Files";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(189, 6);
            // 
            // menuItemExit
            // 
            this.menuItemExit.Name = "menuItemExit";
            this.menuItemExit.Size = new System.Drawing.Size(192, 22);
            this.menuItemExit.Text = "E&xit";
            this.menuItemExit.Click += new System.EventHandler(this.menuItemExit_Click);
            // 
            // viewMenu
            // 
            this.viewMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolBarToolStripMenuItem});
            this.viewMenu.Name = "viewMenu";
            this.viewMenu.Size = new System.Drawing.Size(41, 20);
            this.viewMenu.Text = "&View";
            // 
            // toolBarToolStripMenuItem
            // 
            this.toolBarToolStripMenuItem.Checked = true;
            this.toolBarToolStripMenuItem.CheckOnClick = true;
            this.toolBarToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.toolBarToolStripMenuItem.Name = "toolBarToolStripMenuItem";
            this.toolBarToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            this.toolBarToolStripMenuItem.Text = "&Toolbar";
            this.toolBarToolStripMenuItem.Click += new System.EventHandler(this.ToolBarToolStripMenuItem_Click);
            // 
            // toolsMenu
            // 
            this.toolsMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.optionsToolStripMenuItem});
            this.toolsMenu.Name = "toolsMenu";
            this.toolsMenu.Size = new System.Drawing.Size(44, 20);
            this.toolsMenu.Text = "&Tools";
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.optionsToolStripMenuItem.Text = "&Options";
            this.optionsToolStripMenuItem.Click += new System.EventHandler(this.menuItemOptions_Click);
            // 
            // helpMenu
            // 
            this.helpMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.indexToolStripMenuItem,
            this.aboutToolStripMenuItem,
            this.pluginsVersionsToolStripMenuItem});
            this.helpMenu.Name = "helpMenu";
            this.helpMenu.Size = new System.Drawing.Size(40, 20);
            this.helpMenu.Text = "&Help";
            // 
            // indexToolStripMenuItem
            // 
            this.indexToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("indexToolStripMenuItem.Image")));
            this.indexToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Black;
            this.indexToolStripMenuItem.Name = "indexToolStripMenuItem";
            this.indexToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.indexToolStripMenuItem.Text = "&Help";
            this.indexToolStripMenuItem.Visible = false;
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.aboutToolStripMenuItem.Text = "&About ...";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // pluginsVersionsToolStripMenuItem
            // 
            this.pluginsVersionsToolStripMenuItem.Name = "pluginsVersionsToolStripMenuItem";
            this.pluginsVersionsToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.pluginsVersionsToolStripMenuItem.Text = "Plugins versions";
            this.pluginsVersionsToolStripMenuItem.Click += new System.EventHandler(this.pluginsVersionsToolStripMenuItem_Click);
            // 
            // toolStrip
            // 
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripButton,
            this.toolStripSeparator2});
            this.toolStrip.Location = new System.Drawing.Point(0, 24);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(496, 25);
            this.toolStrip.TabIndex = 1;
            this.toolStrip.Text = "ToolStrip";
            // 
            // openToolStripButton
            // 
            this.openToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.openToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("openToolStripButton.Image")));
            this.openToolStripButton.ImageTransparentColor = System.Drawing.Color.Black;
            this.openToolStripButton.Name = "openToolStripButton";
            this.openToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.openToolStripButton.Text = "Open";
            this.openToolStripButton.Click += new System.EventHandler(this.menuItemOpen_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // m_StatusBar
            // 
            this.m_StatusBar.Location = new System.Drawing.Point(0, 644);
            this.m_StatusBar.Name = "m_StatusBar";
            this.m_StatusBar.Size = new System.Drawing.Size(992, 22);
            this.m_StatusBar.TabIndex = 3;
            this.m_StatusBar.Text = "statusStrip1";
            this.m_StatusBar.Visible = false;
            // 
            // dataGridMonitor
            // 
            this.dataGridMonitor.AllowUserToAddRows = false;
            this.dataGridMonitor.AllowUserToDeleteRows = false;
            this.dataGridMonitor.AllowUserToOrderColumns = true;
            this.dataGridMonitor.AllowUserToResizeRows = false;
            this.dataGridMonitor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridMonitor.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridMonitor.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colProjName,
            this.colProjCnxStatus,
            this.colRunStatus,
            this.colBtnConnectStart});
            this.dataGridMonitor.Location = new System.Drawing.Point(0, 52);
            this.dataGridMonitor.Name = "dataGridMonitor";
            this.dataGridMonitor.Size = new System.Drawing.Size(496, 164);
            this.dataGridMonitor.TabIndex = 4;
            // 
            // colProjName
            // 
            this.colProjName.HeaderText = "Project Name";
            this.colProjName.Name = "colProjName";
            this.colProjName.ReadOnly = true;
            this.colProjName.Width = 130;
            // 
            // colProjCnxStatus
            // 
            this.colProjCnxStatus.HeaderText = "Connexion Status";
            this.colProjCnxStatus.Name = "colProjCnxStatus";
            this.colProjCnxStatus.ReadOnly = true;
            // 
            // colRunStatus
            // 
            this.colRunStatus.HeaderText = "Run Status";
            this.colRunStatus.Name = "colRunStatus";
            this.colRunStatus.ReadOnly = true;
            // 
            // colBtnConnectStart
            // 
            this.colBtnConnectStart.HeaderText = "Connect / Start";
            this.colBtnConnectStart.Name = "colBtnConnectStart";
            // 
            // m_trayIcon
            // 
            this.m_trayIcon.Text = "notifyIcon1";
            this.m_trayIcon.Visible = true;
            // 
            // appEventLogPanel
            // 
            this.appEventLogPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.appEventLogPanel.Location = new System.Drawing.Point(0, 222);
            this.appEventLogPanel.Name = "appEventLogPanel";
            this.appEventLogPanel.Size = new System.Drawing.Size(496, 200);
            this.appEventLogPanel.TabIndex = 5;
            // 
            // MDISmartCommandMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(496, 423);
            this.Controls.Add(this.appEventLogPanel);
            this.Controls.Add(this.dataGridMonitor);
            this.Controls.Add(this.m_StatusBar);
            this.Controls.Add(this.toolStrip);
            this.Controls.Add(this.menuStrip);
            this.DoubleBuffered = true;
            this.MainMenuStrip = this.menuStrip;
            this.Name = "MDISmartCommandMain";
            this.Load += new System.EventHandler(this.MDISmartCommandMain_Load);
            this.SizeChanged += new System.EventHandler(this.MDISmartCommandMain_SizeChanged);
            this.Shown += new System.EventHandler(this.MDISmartCommandMain_Shown);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MDISmartCommandMain_FormClosed);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridMonitor)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion


        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fileMenu;
        private System.Windows.Forms.ToolStripMenuItem menuItemOpen;
        private System.Windows.Forms.ToolStripMenuItem menuItemExit;
        private System.Windows.Forms.ToolStripMenuItem viewMenu;
        private System.Windows.Forms.ToolStripMenuItem toolBarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsMenu;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpMenu;
        private System.Windows.Forms.ToolStripMenuItem indexToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton openToolStripButton;
        private System.Windows.Forms.ToolTip ToolTip;
        private System.Windows.Forms.StatusStrip m_StatusBar;
        private System.Windows.Forms.ToolStripMenuItem pluginsVersionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem m_MruFiles;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem menuItemTraceConfig;
        private System.Windows.Forms.ToolStripMenuItem menuItemOpenDebugConsole;
        private System.Windows.Forms.DataGridView dataGridMonitor;
        private System.Windows.Forms.DataGridViewTextBoxColumn colProjName;
        private System.Windows.Forms.DataGridViewImageColumn colProjCnxStatus;
        private System.Windows.Forms.DataGridViewImageColumn colRunStatus;
        private System.Windows.Forms.DataGridViewButtonColumn colBtnConnectStart;
        private AppEventLogPanel appEventLogPanel;
        private System.Windows.Forms.NotifyIcon m_trayIcon;
    }
}




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
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.toolBarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.indexToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.openToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.m_tsBtnConnexion = new System.Windows.Forms.ToolStripButton();
            this.m_tsBtnStartStop = new System.Windows.Forms.ToolStripButton();
            this.m_tsCboCurConnection = new System.Windows.Forms.ToolStripComboBox();
            this.m_tsBtnConfigComm = new System.Windows.Forms.ToolStripButton();
            this.m_tsBtnFullScreen = new System.Windows.Forms.ToolStripButton();
            this.ToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.m_StatusBar = new System.Windows.Forms.StatusStrip();
            this.pluginsVersionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip.SuspendLayout();
            this.toolStrip.SuspendLayout();
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
            this.menuStrip.Size = new System.Drawing.Size(992, 24);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "MenuStrip";
            // 
            // fileMenu
            // 
            this.fileMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.toolStripSeparator3,
            this.exitToolStripMenuItem});
            this.fileMenu.ImageTransparentColor = System.Drawing.SystemColors.ActiveBorder;
            this.fileMenu.Name = "fileMenu";
            this.fileMenu.Size = new System.Drawing.Size(35, 20);
            this.fileMenu.Text = "&File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("openToolStripMenuItem.Image")));
            this.openToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Black;
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.openToolStripMenuItem.Text = "&Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.OpenFile);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(148, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolsStripMenuItem_Click);
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
            this.optionsToolStripMenuItem.Click += new System.EventHandler(this.optionsToolStripMenuItem_Click);
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
            // toolStrip
            // 
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripButton,
            this.toolStripSeparator2,
            this.m_tsBtnConnexion,
            this.m_tsBtnStartStop,
            this.m_tsCboCurConnection,
            this.m_tsBtnConfigComm,
            this.m_tsBtnFullScreen});
            this.toolStrip.Location = new System.Drawing.Point(0, 24);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(992, 25);
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
            this.openToolStripButton.Click += new System.EventHandler(this.OpenFile);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // m_tsBtnConnexion
            // 
            this.m_tsBtnConnexion.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.m_tsBtnConnexion.Name = "m_tsBtnConnexion";
            this.m_tsBtnConnexion.Size = new System.Drawing.Size(75, 22);
            this.m_tsBtnConnexion.Text = "Disconnected";
            this.m_tsBtnConnexion.Click += new System.EventHandler(this.m_tsBtnConnexion_Click);
            // 
            // m_tsBtnStartStop
            // 
            this.m_tsBtnStartStop.Enabled = false;
            this.m_tsBtnStartStop.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.m_tsBtnStartStop.Name = "m_tsBtnStartStop";
            this.m_tsBtnStartStop.Size = new System.Drawing.Size(51, 22);
            this.m_tsBtnStartStop.Text = "Stopped";
            this.m_tsBtnStartStop.Click += new System.EventHandler(this.m_tsBtnStartStop_Click);
            // 
            // m_tsCboCurConnection
            // 
            this.m_tsCboCurConnection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_tsCboCurConnection.Name = "m_tsCboCurConnection";
            this.m_tsCboCurConnection.Size = new System.Drawing.Size(350, 25);
            this.m_tsCboCurConnection.SelectedIndexChanged += new System.EventHandler(this.m_tsCboCurConnection_SelectedIndexChanged);
            // 
            // m_tsBtnConfigComm
            // 
            this.m_tsBtnConfigComm.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.m_tsBtnConfigComm.Image = ((System.Drawing.Image)(resources.GetObject("m_tsBtnConfigComm.Image")));
            this.m_tsBtnConfigComm.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.m_tsBtnConfigComm.Name = "m_tsBtnConfigComm";
            this.m_tsBtnConfigComm.Size = new System.Drawing.Size(118, 22);
            this.m_tsBtnConfigComm.Text = "Configure connections";
            this.m_tsBtnConfigComm.Click += new System.EventHandler(this.m_tsBtnConfigComm_Click);
            // 
            // m_tsBtnFullScreen
            // 
            this.m_tsBtnFullScreen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.m_tsBtnFullScreen.Image = ((System.Drawing.Image)(resources.GetObject("m_tsBtnFullScreen.Image")));
            this.m_tsBtnFullScreen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.m_tsBtnFullScreen.Name = "m_tsBtnFullScreen";
            this.m_tsBtnFullScreen.Size = new System.Drawing.Size(98, 22);
            this.m_tsBtnFullScreen.Text = "Toggle Full Screen";
            this.m_tsBtnFullScreen.Click += new System.EventHandler(this.m_tsBtnFullScreen_Click);
            // 
            // m_StatusBar
            // 
            this.m_StatusBar.Location = new System.Drawing.Point(0, 644);
            this.m_StatusBar.Name = "m_StatusBar";
            this.m_StatusBar.Size = new System.Drawing.Size(992, 22);
            this.m_StatusBar.TabIndex = 3;
            this.m_StatusBar.Text = "statusStrip1";
            // 
            // pluginsVersionsToolStripMenuItem
            // 
            this.pluginsVersionsToolStripMenuItem.Name = "pluginsVersionsToolStripMenuItem";
            this.pluginsVersionsToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.pluginsVersionsToolStripMenuItem.Text = "Plugins versions";
            this.pluginsVersionsToolStripMenuItem.Click += new System.EventHandler(this.pluginsVersionsToolStripMenuItem_Click);
            // 
            // MDISmartCommandMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(992, 666);
            this.Controls.Add(this.m_StatusBar);
            this.Controls.Add(this.toolStrip);
            this.Controls.Add(this.menuStrip);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip;
            this.Name = "MDISmartCommandMain";
            this.Text = "SmartCommand";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MDISmartCommandMain_FormClosed);
            this.Shown += new System.EventHandler(this.MDISmartCommandMain_Shown);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OnFormClosing);
            this.Load += new System.EventHandler(this.MDISmartCommandMain_Load);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
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
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewMenu;
        private System.Windows.Forms.ToolStripMenuItem toolBarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsMenu;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpMenu;
        private System.Windows.Forms.ToolStripMenuItem indexToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton openToolStripButton;
        private System.Windows.Forms.ToolTip ToolTip;
        private System.Windows.Forms.StatusStrip m_StatusBar;
        private System.Windows.Forms.ToolStripButton m_tsBtnConnexion;
        private System.Windows.Forms.ToolStripButton m_tsBtnStartStop;
        private System.Windows.Forms.ToolStripComboBox m_tsCboCurConnection;
        private System.Windows.Forms.ToolStripButton m_tsBtnConfigComm;
        private System.Windows.Forms.ToolStripButton m_tsBtnFullScreen;
        private System.Windows.Forms.ToolStripMenuItem pluginsVersionsToolStripMenuItem;
    }
}




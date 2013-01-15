namespace SmartApp
{
    partial class VirtualCnxContainer
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
            this.m_menuStrip = new System.Windows.Forms.MenuStrip();
            this.menuWindows = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemCascade = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemTileVertical = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemTileHorizontal = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemArrangeIcons = new System.Windows.Forms.ToolStripMenuItem();
            this.m_menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_menuStrip
            // 
            this.m_menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuWindows});
            this.m_menuStrip.Location = new System.Drawing.Point(0, 0);
            this.m_menuStrip.MdiWindowListItem = this.menuWindows;
            this.m_menuStrip.Name = "m_menuStrip";
            this.m_menuStrip.Size = new System.Drawing.Size(561, 24);
            this.m_menuStrip.TabIndex = 1;
            this.m_menuStrip.Text = "MenuStrip";
            // 
            // menuWindows
            // 
            this.menuWindows.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemCascade,
            this.menuItemTileVertical,
            this.menuItemTileHorizontal,
            this.menuItemArrangeIcons});
            this.menuWindows.Name = "menuWindows";
            this.menuWindows.Size = new System.Drawing.Size(62, 20);
            this.menuWindows.Text = "&Windows";
            // 
            // menuItemCascade
            // 
            this.menuItemCascade.Name = "menuItemCascade";
            this.menuItemCascade.Size = new System.Drawing.Size(153, 22);
            this.menuItemCascade.Text = "&Cascade";
            this.menuItemCascade.Click += new System.EventHandler(this.menuItemCascade_Click);
            // 
            // menuItemTileVertical
            // 
            this.menuItemTileVertical.Name = "menuItemTileVertical";
            this.menuItemTileVertical.Size = new System.Drawing.Size(153, 22);
            this.menuItemTileVertical.Text = "Tile &Vertical";
            this.menuItemTileVertical.Click += new System.EventHandler(this.menuItemTileVertical_Click);
            // 
            // menuItemTileHorizontal
            // 
            this.menuItemTileHorizontal.Name = "menuItemTileHorizontal";
            this.menuItemTileHorizontal.Size = new System.Drawing.Size(153, 22);
            this.menuItemTileHorizontal.Text = "Tile &Horizontal";
            this.menuItemTileHorizontal.Click += new System.EventHandler(this.menuItemTileHorizontal_Click);
            // 
            // menuItemArrangeIcons
            // 
            this.menuItemArrangeIcons.Name = "menuItemArrangeIcons";
            this.menuItemArrangeIcons.Size = new System.Drawing.Size(153, 22);
            this.menuItemArrangeIcons.Text = "&Arrange Icons";
            this.menuItemArrangeIcons.Click += new System.EventHandler(this.menuItemArrangeIcons_Click);
            // 
            // VirtualCnxContainer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(561, 412);
            this.Controls.Add(this.m_menuStrip);
            this.IsMdiContainer = true;
            this.Name = "VirtualCnxContainer";
            this.Text = "Virtual connexions container";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.VirtualCnxContainer_FormClosing);
            this.m_menuStrip.ResumeLayout(false);
            this.m_menuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip m_menuStrip;
        private System.Windows.Forms.ToolStripMenuItem menuWindows;
        private System.Windows.Forms.ToolStripMenuItem menuItemCascade;
        private System.Windows.Forms.ToolStripMenuItem menuItemTileVertical;
        private System.Windows.Forms.ToolStripMenuItem menuItemTileHorizontal;
        private System.Windows.Forms.ToolStripMenuItem menuItemArrangeIcons;
    }
}
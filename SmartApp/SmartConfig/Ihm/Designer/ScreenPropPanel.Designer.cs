namespace SmartApp.Ihm
{
    partial class ScreenPropPanel
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
            this.m_btnNewScreen = new System.Windows.Forms.Button();
            this.m_ListViewScreens = new System.Windows.Forms.ListView();
            this.m_colSymbol = new System.Windows.Forms.ColumnHeader();
            this.m_colTitle = new System.Windows.Forms.ColumnHeader();
            this.m_PanelScreenProperties = new SmartApp.Ihm.ScreenPropertiesControl();
            this.SuspendLayout();
            // 
            // m_btnNewScreen
            // 
            this.m_btnNewScreen.Location = new System.Drawing.Point(3, 463);
            this.m_btnNewScreen.Name = "m_btnNewScreen";
            this.m_btnNewScreen.Size = new System.Drawing.Size(75, 23);
            this.m_btnNewScreen.TabIndex = 3;
            this.m_btnNewScreen.Text = "New";
            this.m_btnNewScreen.UseVisualStyleBackColor = true;
            this.m_btnNewScreen.Click += new System.EventHandler(this.OnbtnNewScreenClick);
            // 
            // m_ListViewScreens
            // 
            this.m_ListViewScreens.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.m_colSymbol,
            this.m_colTitle});
            this.m_ListViewScreens.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_ListViewScreens.FullRowSelect = true;
            this.m_ListViewScreens.GridLines = true;
            this.m_ListViewScreens.HideSelection = false;
            this.m_ListViewScreens.Location = new System.Drawing.Point(0, 0);
            this.m_ListViewScreens.MultiSelect = false;
            this.m_ListViewScreens.Name = "m_ListViewScreens";
            this.m_ListViewScreens.Size = new System.Drawing.Size(230, 224);
            this.m_ListViewScreens.TabIndex = 6;
            this.m_ListViewScreens.UseCompatibleStateImageBehavior = false;
            this.m_ListViewScreens.View = System.Windows.Forms.View.Details;
            this.m_ListViewScreens.SelectedIndexChanged += new System.EventHandler(this.listViewSelectedScreenChanged);
            this.m_ListViewScreens.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnListViewKeyDown);
            // 
            // m_colSymbol
            // 
            this.m_colSymbol.Text = "Symbol";
            this.m_colSymbol.Width = 120;
            // 
            // m_colTitle
            // 
            this.m_colTitle.Text = "Title";
            this.m_colTitle.Width = 150;
            // 
            // m_PanelScreenProperties
            // 
            this.m_PanelScreenProperties.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_PanelScreenProperties.BackColor = System.Drawing.Color.Transparent;
            this.m_PanelScreenProperties.BackPictureFile = "";
            this.m_PanelScreenProperties.BTScreen = null;
            this.m_PanelScreenProperties.Description = "";
            this.m_PanelScreenProperties.Doc = null;
            this.m_PanelScreenProperties.Location = new System.Drawing.Point(0, 230);
            this.m_PanelScreenProperties.Margin = new System.Windows.Forms.Padding(0);
            this.m_PanelScreenProperties.Name = "m_PanelScreenProperties";
            this.m_PanelScreenProperties.Size = new System.Drawing.Size(227, 230);
            this.m_PanelScreenProperties.Symbol = "";
            this.m_PanelScreenProperties.TabIndex = 5;
            this.m_PanelScreenProperties.Title = "";
            // 
            // ScreenPropPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.m_ListViewScreens);
            this.Controls.Add(this.m_btnNewScreen);
            this.Controls.Add(this.m_PanelScreenProperties);
            this.Name = "ScreenPropPanel";
            this.Size = new System.Drawing.Size(230, 490);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button m_btnNewScreen;
        private ScreenPropertiesControl m_PanelScreenProperties;
        private System.Windows.Forms.ListView m_ListViewScreens;
        private System.Windows.Forms.ColumnHeader m_colSymbol;
        private System.Windows.Forms.ColumnHeader m_colTitle;
    }
}

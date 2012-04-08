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
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // m_btnNewScreen
            // 
            this.m_btnNewScreen.Location = new System.Drawing.Point(0, 299);
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
            this.m_ListViewScreens.Size = new System.Drawing.Size(230, 254);
            this.m_ListViewScreens.TabIndex = 6;
            this.m_ListViewScreens.UseCompatibleStateImageBehavior = false;
            this.m_ListViewScreens.View = System.Windows.Forms.View.Details;
            this.m_ListViewScreens.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.m_ListViewScreens_MouseDoubleClick);
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
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(0, 261);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(227, 35);
            this.label1.TabIndex = 7;
            this.label1.Text = "Double click on a screen in the list above, or a tool in the screen to edit it\'s " +
                "properties";
            // 
            // ScreenPropPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.m_ListViewScreens);
            this.Controls.Add(this.m_btnNewScreen);
            this.Name = "ScreenPropPanel";
            this.Size = new System.Drawing.Size(230, 325);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button m_btnNewScreen;
        private System.Windows.Forms.ListView m_ListViewScreens;
        private System.Windows.Forms.ColumnHeader m_colSymbol;
        private System.Windows.Forms.ColumnHeader m_colTitle;
        private System.Windows.Forms.Label label1;
    }
}

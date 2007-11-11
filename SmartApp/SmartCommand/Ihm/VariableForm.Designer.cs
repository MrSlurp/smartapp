namespace SmartApp
{
    partial class VariableForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MainMenu mainMenu1;

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
            this.mainMenu1 = new System.Windows.Forms.MainMenu(this.components);
            this.m_DataslistView = new System.Windows.Forms.ListView();
            this.m_HeaderSymbol = new System.Windows.Forms.ColumnHeader();
            this.m_HeaderValue = new System.Windows.Forms.ColumnHeader();
            this.SuspendLayout();
            // 
            // m_DataslistView
            // 
            this.m_DataslistView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_DataslistView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.m_HeaderSymbol,
            this.m_HeaderValue});
            this.m_DataslistView.Location = new System.Drawing.Point(0, 0);
            this.m_DataslistView.Name = "m_DataslistView";
            this.m_DataslistView.Size = new System.Drawing.Size(309, 455);
            this.m_DataslistView.TabIndex = 0;
            this.m_DataslistView.UseCompatibleStateImageBehavior = false;
            this.m_DataslistView.View = System.Windows.Forms.View.Details;
            // 
            // m_HeaderSymbol
            // 
            this.m_HeaderSymbol.Text = "Data symbol";
            this.m_HeaderSymbol.Width = 193;
            // 
            // m_HeaderValue
            // 
            this.m_HeaderValue.Text = "Data value";
            this.m_HeaderValue.Width = 113;
            // 
            // VariableForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(309, 455);
            this.Controls.Add(this.m_DataslistView);
            this.MaximizeBox = false;
            this.Menu = this.mainMenu1;
            this.Name = "VariableForm";
            this.Text = "Datas";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OnFormClosing);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView m_DataslistView;
        private System.Windows.Forms.ColumnHeader m_HeaderSymbol;
        private System.Windows.Forms.ColumnHeader m_HeaderValue;
    }
}
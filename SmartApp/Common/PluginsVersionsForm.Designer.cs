namespace SmartApp
{
    partial class PluginsVersionsForm
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
            this.m_listViewDll = new System.Windows.Forms.ListView();
            this.m_ColDllName = new System.Windows.Forms.ColumnHeader();
            this.m_ColVersion = new System.Windows.Forms.ColumnHeader();
            this.btnOK = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // m_listViewDll
            // 
            this.m_listViewDll.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_listViewDll.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.m_ColDllName,
            this.m_ColVersion});
            this.m_listViewDll.FullRowSelect = true;
            this.m_listViewDll.GridLines = true;
            this.m_listViewDll.HideSelection = false;
            this.m_listViewDll.Location = new System.Drawing.Point(12, 4);
            this.m_listViewDll.MultiSelect = false;
            this.m_listViewDll.Name = "m_listViewDll";
            this.m_listViewDll.Size = new System.Drawing.Size(254, 269);
            this.m_listViewDll.TabIndex = 9;
            this.m_listViewDll.UseCompatibleStateImageBehavior = false;
            this.m_listViewDll.View = System.Windows.Forms.View.Details;
            // 
            // m_ColDllName
            // 
            this.m_ColDllName.Text = "Dll Name";
            this.m_ColDllName.Width = 150;
            // 
            // m_ColVersion
            // 
            this.m_ColVersion.Text = "Version";
            this.m_ColVersion.Width = 100;
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(101, 279);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 10;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // PluginsVersionsForm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(278, 310);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.m_listViewDll);
            this.Name = "PluginsVersionsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.ShowInTaskbar = false;
            this.Text = "Plugins Versions";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView m_listViewDll;
        private System.Windows.Forms.ColumnHeader m_ColDllName;
        private System.Windows.Forms.ColumnHeader m_ColVersion;
        private System.Windows.Forms.Button btnOK;
    }
}
namespace SmartApp.Ihm.ProgItemPanels
{
    partial class FunctionPanel
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
            this.m_splitContainer = new System.Windows.Forms.SplitContainer();
            this.m_btnNew = new System.Windows.Forms.Button();
            this.m_listViewFunc = new System.Windows.Forms.ListView();
            this.m_colHeadSymbol = new System.Windows.Forms.ColumnHeader();
            this.m_panelFunc = new SmartApp.Ihm.FunctionPropertiesControl();
            this.m_splitContainer.Panel1.SuspendLayout();
            this.m_splitContainer.Panel2.SuspendLayout();
            this.m_splitContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_splitContainer
            // 
            this.m_splitContainer.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.m_splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_splitContainer.Location = new System.Drawing.Point(0, 0);
            this.m_splitContainer.Name = "m_splitContainer";
            // 
            // m_splitContainer.Panel1
            // 
            this.m_splitContainer.Panel1.Controls.Add(this.m_btnNew);
            this.m_splitContainer.Panel1.Controls.Add(this.m_listViewFunc);
            // 
            // m_splitContainer.Panel2
            // 
            this.m_splitContainer.Panel2.Controls.Add(this.m_panelFunc);
            this.m_splitContainer.Size = new System.Drawing.Size(648, 355);
            this.m_splitContainer.SplitterDistance = 216;
            this.m_splitContainer.TabIndex = 0;
            // 
            // m_btnNew
            // 
            this.m_btnNew.Location = new System.Drawing.Point(3, 3);
            this.m_btnNew.Name = "m_btnNew";
            this.m_btnNew.Size = new System.Drawing.Size(88, 21);
            this.m_btnNew.TabIndex = 1;
            this.m_btnNew.Text = "new";
            this.m_btnNew.UseVisualStyleBackColor = true;
            this.m_btnNew.Click += new System.EventHandler(this.OnbtnNewClick);
            // 
            // m_listViewFunc
            // 
            this.m_listViewFunc.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_listViewFunc.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.m_colHeadSymbol});
            this.m_listViewFunc.FullRowSelect = true;
            this.m_listViewFunc.GridLines = true;
            this.m_listViewFunc.HideSelection = false;
            this.m_listViewFunc.Location = new System.Drawing.Point(0, 39);
            this.m_listViewFunc.Name = "m_listViewFunc";
            this.m_listViewFunc.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.m_listViewFunc.Size = new System.Drawing.Size(216, 316);
            this.m_listViewFunc.TabIndex = 0;
            this.m_listViewFunc.UseCompatibleStateImageBehavior = false;
            this.m_listViewFunc.View = System.Windows.Forms.View.Details;
            this.m_listViewFunc.SelectedIndexChanged += new System.EventHandler(this.listViewSelectedFunctionChanged);
            this.m_listViewFunc.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnListViewKeyDown);
            // 
            // m_colHeadSymbol
            // 
            this.m_colHeadSymbol.Text = "Symbol";
            this.m_colHeadSymbol.Width = 200;
            // 
            // m_panelFunc
            // 
            this.m_panelFunc.BackColor = System.Drawing.Color.Transparent;
            this.m_panelFunc.Description = "";
            this.m_panelFunc.Doc = null;
            this.m_panelFunc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_panelFunc.Function = null;
            this.m_panelFunc.Location = new System.Drawing.Point(0, 0);
            this.m_panelFunc.Name = "m_panelFunc";
            this.m_panelFunc.ScriptLines = new string[0];
            this.m_panelFunc.Size = new System.Drawing.Size(424, 351);
            this.m_panelFunc.Symbol = "";
            this.m_panelFunc.TabIndex = 0;
            // 
            // FunctionPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.m_splitContainer);
            this.DoubleBuffered = true;
            this.Name = "FunctionPanel";
            this.Size = new System.Drawing.Size(648, 355);
            this.m_splitContainer.Panel1.ResumeLayout(false);
            this.m_splitContainer.Panel2.ResumeLayout(false);
            this.m_splitContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer m_splitContainer;
        private System.Windows.Forms.ListView m_listViewFunc;
        private System.Windows.Forms.Button m_btnNew;
        private System.Windows.Forms.ColumnHeader m_colHeadSymbol;
        private SmartApp.Ihm.FunctionPropertiesControl m_panelFunc;

    }
}

namespace SmartApp.Ihm.Wizards
{
    partial class WizM3Z2StepChooseBloc
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
            this.label1 = new System.Windows.Forms.Label();
            this.btnAll = new System.Windows.Forms.Button();
            this.btnNone = new System.Windows.Forms.Button();
            this.m_dataGridIn = new System.Windows.Forms.DataGridView();
            this.colSLBloc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colUsed = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.m_dataGridOut = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewCheckBoxColumn1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            ((System.ComponentModel.ISupportInitialize)(this.m_dataGridIn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_dataGridOut)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(521, 34);
            this.label1.TabIndex = 1;
            this.label1.Text = "In this first step, you msut select wich blocs are used in your M3 program";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnAll
            // 
            this.btnAll.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAll.Location = new System.Drawing.Point(160, 268);
            this.btnAll.Name = "btnAll";
            this.btnAll.Size = new System.Drawing.Size(208, 23);
            this.btnAll.TabIndex = 5;
            this.btnAll.Text = "Check all";
            this.btnAll.UseVisualStyleBackColor = true;
            this.btnAll.Click += new System.EventHandler(this.btnAll_Click);
            // 
            // btnNone
            // 
            this.btnNone.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNone.Location = new System.Drawing.Point(160, 297);
            this.btnNone.Name = "btnNone";
            this.btnNone.Size = new System.Drawing.Size(208, 23);
            this.btnNone.TabIndex = 5;
            this.btnNone.Text = "Uncheck all";
            this.btnNone.UseVisualStyleBackColor = true;
            this.btnNone.Click += new System.EventHandler(this.btnNone_Click);
            // 
            // m_dataGridIn
            // 
            this.m_dataGridIn.AllowUserToAddRows = false;
            this.m_dataGridIn.AllowUserToDeleteRows = false;
            this.m_dataGridIn.AllowUserToResizeRows = false;
            this.m_dataGridIn.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.m_dataGridIn.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.m_dataGridIn.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colSLBloc,
            this.colUsed});
            this.m_dataGridIn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_dataGridIn.Location = new System.Drawing.Point(0, 0);
            this.m_dataGridIn.MultiSelect = false;
            this.m_dataGridIn.Name = "m_dataGridIn";
            this.m_dataGridIn.Size = new System.Drawing.Size(259, 225);
            this.m_dataGridIn.TabIndex = 6;
            // 
            // colSLBloc
            // 
            this.colSLBloc.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.colSLBloc.Frozen = true;
            this.colSLBloc.HeaderText = "Bloc";
            this.colSLBloc.MinimumWidth = 120;
            this.colSLBloc.Name = "colSLBloc";
            this.colSLBloc.ReadOnly = true;
            this.colSLBloc.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.colSLBloc.Width = 120;
            // 
            // colUsed
            // 
            this.colUsed.HeaderText = "Used";
            this.colUsed.MinimumWidth = 40;
            this.colUsed.Name = "colUsed";
            this.colUsed.Width = 40;
            // 
            // m_dataGridOut
            // 
            this.m_dataGridOut.AllowUserToAddRows = false;
            this.m_dataGridOut.AllowUserToDeleteRows = false;
            this.m_dataGridOut.AllowUserToResizeRows = false;
            this.m_dataGridOut.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.m_dataGridOut.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.m_dataGridOut.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewCheckBoxColumn1});
            this.m_dataGridOut.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_dataGridOut.Location = new System.Drawing.Point(0, 0);
            this.m_dataGridOut.MultiSelect = false;
            this.m_dataGridOut.Name = "m_dataGridOut";
            this.m_dataGridOut.Size = new System.Drawing.Size(255, 225);
            this.m_dataGridOut.TabIndex = 7;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn1.Frozen = true;
            this.dataGridViewTextBoxColumn1.HeaderText = "Bloc";
            this.dataGridViewTextBoxColumn1.MinimumWidth = 120;
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewTextBoxColumn1.Width = 120;
            // 
            // dataGridViewCheckBoxColumn1
            // 
            this.dataGridViewCheckBoxColumn1.HeaderText = "Used";
            this.dataGridViewCheckBoxColumn1.MinimumWidth = 40;
            this.dataGridViewCheckBoxColumn1.Name = "dataGridViewCheckBoxColumn1";
            this.dataGridViewCheckBoxColumn1.Width = 40;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(3, 37);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.m_dataGridIn);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.m_dataGridOut);
            this.splitContainer1.Size = new System.Drawing.Size(518, 225);
            this.splitContainer1.SplitterDistance = 259;
            this.splitContainer1.TabIndex = 8;
            // 
            // WizM3Z2StepChooseBloc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.btnNone);
            this.Controls.Add(this.btnAll);
            this.Controls.Add(this.label1);
            this.Name = "WizM3Z2StepChooseBloc";
            this.Size = new System.Drawing.Size(524, 330);
            ((System.ComponentModel.ISupportInitialize)(this.m_dataGridIn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_dataGridOut)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnAll;
        private System.Windows.Forms.Button btnNone;
        private System.Windows.Forms.DataGridView m_dataGridIn;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSLBloc;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colUsed;
        private System.Windows.Forms.DataGridView m_dataGridOut;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn1;
        private System.Windows.Forms.SplitContainer splitContainer1;

    }
}

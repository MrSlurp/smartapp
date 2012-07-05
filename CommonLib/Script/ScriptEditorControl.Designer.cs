namespace CommonLib
{
    partial class ScriptEditorControl
    {
        /// <summary> 
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur de composants

        /// <summary> 
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas 
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.m_labelScript = new System.Windows.Forms.Label();
            this.m_EditScript = new System.Windows.Forms.RichTextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.m_listViewError = new System.Windows.Forms.ListView();
            this.m_colErrorType = new System.Windows.Forms.ColumnHeader();
            this.m_ColLine = new System.Windows.Forms.ColumnHeader();
            this.m_colMessage = new System.Windows.Forms.ColumnHeader();
            this.m_AutoComplListBox = new System.Windows.Forms.ListBox();
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
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.m_labelScript);
            this.splitContainer1.Panel1.Controls.Add(this.m_EditScript);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.button1);
            this.splitContainer1.Panel2.Controls.Add(this.m_listViewError);
            this.splitContainer1.Size = new System.Drawing.Size(576, 399);
            this.splitContainer1.SplitterDistance = 225;
            this.splitContainer1.TabIndex = 1;
            // 
            // m_labelScript
            // 
            this.m_labelScript.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_labelScript.AutoSize = true;
            this.m_labelScript.Location = new System.Drawing.Point(13, 9);
            this.m_labelScript.Name = "m_labelScript";
            this.m_labelScript.Size = new System.Drawing.Size(34, 13);
            this.m_labelScript.TabIndex = 18;
            this.m_labelScript.Text = "Script";
            // 
            // m_EditScript
            // 
            this.m_EditScript.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_EditScript.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_EditScript.Location = new System.Drawing.Point(10, 25);
            this.m_EditScript.Name = "m_EditScript";
            this.m_EditScript.Size = new System.Drawing.Size(555, 192);
            this.m_EditScript.TabIndex = 17;
            this.m_EditScript.Text = "";
            this.m_EditScript.WordWrap = false;
            this.m_EditScript.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnEditScriptKeyDown);
            this.m_EditScript.Leave += new System.EventHandler(this.m_EditScript_Leave);
            this.m_EditScript.TextChanged += new System.EventHandler(this.OnEditScriptTextChanged);
            this.m_EditScript.Click += new System.EventHandler(this.OnEditScriptClick);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(451, 135);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(114, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Check Script";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.OnScriptCheck);
            // 
            // m_listViewError
            // 
            this.m_listViewError.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_listViewError.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.m_colErrorType,
            this.m_ColLine,
            this.m_colMessage});
            this.m_listViewError.Location = new System.Drawing.Point(10, 4);
            this.m_listViewError.Name = "m_listViewError";
            this.m_listViewError.Size = new System.Drawing.Size(555, 125);
            this.m_listViewError.TabIndex = 0;
            this.m_listViewError.UseCompatibleStateImageBehavior = false;
            this.m_listViewError.View = System.Windows.Forms.View.Details;
            // 
            // m_colErrorType
            // 
            this.m_colErrorType.Text = "Error type";
            this.m_colErrorType.Width = 100;
            // 
            // m_ColLine
            // 
            this.m_ColLine.Text = "Line";
            // 
            // m_colMessage
            // 
            this.m_colMessage.Text = "Informations";
            this.m_colMessage.Width = 490;
            // 
            // m_AutoComplListBox
            // 
            this.m_AutoComplListBox.BackColor = System.Drawing.SystemColors.Control;
            this.m_AutoComplListBox.FormattingEnabled = true;
            this.m_AutoComplListBox.HorizontalScrollbar = true;
            this.m_AutoComplListBox.Location = new System.Drawing.Point(408, 2);
            this.m_AutoComplListBox.Name = "m_AutoComplListBox";
            this.m_AutoComplListBox.Size = new System.Drawing.Size(139, 17);
            this.m_AutoComplListBox.Sorted = true;
            this.m_AutoComplListBox.TabIndex = 19;
            this.m_AutoComplListBox.TabStop = false;
            this.m_AutoComplListBox.Visible = false;
            this.m_AutoComplListBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnAutoComplListBoxKeyDown);
            this.m_AutoComplListBox.Click += new System.EventHandler(this.OnAutoComplListBoxClick);
            // 
            // ScriptEditorControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.m_AutoComplListBox);
            this.Controls.Add(this.splitContainer1);
            this.Name = "ScriptEditorControl";
            this.Size = new System.Drawing.Size(576, 399);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ListBox m_AutoComplListBox;
        private System.Windows.Forms.Label m_labelScript;
        private System.Windows.Forms.RichTextBox m_EditScript;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ListView m_listViewError;
        private System.Windows.Forms.ColumnHeader m_colErrorType;
        private System.Windows.Forms.ColumnHeader m_ColLine;
        private System.Windows.Forms.ColumnHeader m_colMessage;
    }
}

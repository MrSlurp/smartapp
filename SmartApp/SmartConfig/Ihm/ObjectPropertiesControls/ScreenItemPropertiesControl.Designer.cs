namespace SmartApp.Ihm
{
    partial class ScreenItemPropertiesControl
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
            this.m_textSymbol = new SmartApp.Ihm.SymbolTextBox();
            this.m_labelSymbol = new System.Windows.Forms.Label();
            this.m_labelDesc = new System.Windows.Forms.Label();
            this.m_richTextBoxDesc = new System.Windows.Forms.RichTextBox();
            this.m_labelAssoData = new System.Windows.Forms.Label();
            this.m_checkScreenEvent = new System.Windows.Forms.CheckBox();
            this.m_checkReadOnly = new System.Windows.Forms.CheckBox();
            this.m_EditText = new System.Windows.Forms.TextBox();
            this.labelText = new System.Windows.Forms.Label();
            this.m_EditAssociateData = new SmartApp.Ihm.SymbolTextBox();
            this.m_LabelCurControl = new System.Windows.Forms.Label();
            this.m_FilledRectPropPanel = new SmartApp.Ihm.FilledRectProperties();
            this.SuspendLayout();
            // 
            // m_textSymbol
            // 
            this.m_textSymbol.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_textSymbol.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.m_textSymbol.Location = new System.Drawing.Point(3, 147);
            this.m_textSymbol.Name = "m_textSymbol";
            this.m_textSymbol.Size = new System.Drawing.Size(193, 20);
            this.m_textSymbol.TabIndex = 6;
            // 
            // m_labelSymbol
            // 
            this.m_labelSymbol.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_labelSymbol.AutoSize = true;
            this.m_labelSymbol.Location = new System.Drawing.Point(3, 131);
            this.m_labelSymbol.Name = "m_labelSymbol";
            this.m_labelSymbol.Size = new System.Drawing.Size(41, 13);
            this.m_labelSymbol.TabIndex = 5;
            this.m_labelSymbol.Text = "Symbol";
            // 
            // m_labelDesc
            // 
            this.m_labelDesc.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_labelDesc.AutoSize = true;
            this.m_labelDesc.Location = new System.Drawing.Point(3, 27);
            this.m_labelDesc.Name = "m_labelDesc";
            this.m_labelDesc.Size = new System.Drawing.Size(60, 13);
            this.m_labelDesc.TabIndex = 4;
            this.m_labelDesc.Text = "Description";
            // 
            // m_richTextBoxDesc
            // 
            this.m_richTextBoxDesc.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_richTextBoxDesc.Location = new System.Drawing.Point(3, 43);
            this.m_richTextBoxDesc.Name = "m_richTextBoxDesc";
            this.m_richTextBoxDesc.Size = new System.Drawing.Size(193, 79);
            this.m_richTextBoxDesc.TabIndex = 3;
            this.m_richTextBoxDesc.Text = "";
            // 
            // m_labelAssoData
            // 
            this.m_labelAssoData.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_labelAssoData.AutoSize = true;
            this.m_labelAssoData.Location = new System.Drawing.Point(3, 170);
            this.m_labelAssoData.Name = "m_labelAssoData";
            this.m_labelAssoData.Size = new System.Drawing.Size(79, 13);
            this.m_labelAssoData.TabIndex = 7;
            this.m_labelAssoData.Text = "Associate Data";
            // 
            // m_checkScreenEvent
            // 
            this.m_checkScreenEvent.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_checkScreenEvent.AutoSize = true;
            this.m_checkScreenEvent.Location = new System.Drawing.Point(7, 252);
            this.m_checkScreenEvent.Name = "m_checkScreenEvent";
            this.m_checkScreenEvent.Size = new System.Drawing.Size(113, 17);
            this.m_checkScreenEvent.TabIndex = 9;
            this.m_checkScreenEvent.Text = "Use Screen Event";
            this.m_checkScreenEvent.UseVisualStyleBackColor = true;
            // 
            // m_checkReadOnly
            // 
            this.m_checkReadOnly.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_checkReadOnly.AutoSize = true;
            this.m_checkReadOnly.Location = new System.Drawing.Point(7, 275);
            this.m_checkReadOnly.Name = "m_checkReadOnly";
            this.m_checkReadOnly.Size = new System.Drawing.Size(76, 17);
            this.m_checkReadOnly.TabIndex = 10;
            this.m_checkReadOnly.Text = "Read Only";
            this.m_checkReadOnly.UseVisualStyleBackColor = true;
            this.m_checkReadOnly.Visible = false;
            // 
            // m_EditText
            // 
            this.m_EditText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_EditText.Location = new System.Drawing.Point(3, 226);
            this.m_EditText.Name = "m_EditText";
            this.m_EditText.Size = new System.Drawing.Size(193, 20);
            this.m_EditText.TabIndex = 12;
            // 
            // labelText
            // 
            this.labelText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.labelText.AutoSize = true;
            this.labelText.Location = new System.Drawing.Point(3, 210);
            this.labelText.Name = "labelText";
            this.labelText.Size = new System.Drawing.Size(28, 13);
            this.labelText.TabIndex = 11;
            this.labelText.Text = "Text";
            // 
            // m_EditAssociateData
            // 
            this.m_EditAssociateData.AllowDrop = true;
            this.m_EditAssociateData.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_EditAssociateData.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.m_EditAssociateData.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.m_EditAssociateData.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.m_EditAssociateData.Location = new System.Drawing.Point(4, 187);
            this.m_EditAssociateData.Name = "m_EditAssociateData";
            this.m_EditAssociateData.Size = new System.Drawing.Size(192, 20);
            this.m_EditAssociateData.TabIndex = 13;
            this.m_EditAssociateData.DragDrop += new System.Windows.Forms.DragEventHandler(this.OnEditAssociateDataDragDrop);
            this.m_EditAssociateData.DragEnter += new System.Windows.Forms.DragEventHandler(this.OnEditAssociateDataDragEnter);
            this.m_EditAssociateData.TextChanged += new System.EventHandler(this.OnEditAssocDataEdited);
            // 
            // m_LabelCurControl
            // 
            this.m_LabelCurControl.AutoSize = true;
            this.m_LabelCurControl.Location = new System.Drawing.Point(4, 4);
            this.m_LabelCurControl.Name = "m_LabelCurControl";
            this.m_LabelCurControl.Size = new System.Drawing.Size(66, 13);
            this.m_LabelCurControl.TabIndex = 14;
            this.m_LabelCurControl.Text = "No selection";
            // 
            // panel1
            // 
            this.m_FilledRectPropPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_FilledRectPropPanel.Location = new System.Drawing.Point(7, 299);
            this.m_FilledRectPropPanel.Name = "panel1";
            this.m_FilledRectPropPanel.Size = new System.Drawing.Size(189, 50);
            this.m_FilledRectPropPanel.TabIndex = 15;
            // 
            // ScreenItemPropertiesControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.m_FilledRectPropPanel);
            this.Controls.Add(this.m_LabelCurControl);
            this.Controls.Add(this.m_EditAssociateData);
            this.Controls.Add(this.m_EditText);
            this.Controls.Add(this.labelText);
            this.Controls.Add(this.m_checkReadOnly);
            this.Controls.Add(this.m_checkScreenEvent);
            this.Controls.Add(this.m_labelAssoData);
            this.Controls.Add(this.m_textSymbol);
            this.Controls.Add(this.m_labelSymbol);
            this.Controls.Add(this.m_labelDesc);
            this.Controls.Add(this.m_richTextBoxDesc);
            this.Name = "ScreenItemPropertiesControl";
            this.Size = new System.Drawing.Size(201, 375);
            this.Validating += new System.ComponentModel.CancelEventHandler(this.PropertiesControlValidating);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private SmartApp.Ihm.SymbolTextBox m_textSymbol;
        private System.Windows.Forms.Label m_labelSymbol;
        private System.Windows.Forms.Label m_labelDesc;
        private System.Windows.Forms.RichTextBox m_richTextBoxDesc;
        private System.Windows.Forms.Label m_labelAssoData;
        private System.Windows.Forms.CheckBox m_checkScreenEvent;
        private System.Windows.Forms.CheckBox m_checkReadOnly;
        private System.Windows.Forms.TextBox m_EditText;
        private System.Windows.Forms.Label labelText;
        private SmartApp.Ihm.SymbolTextBox m_EditAssociateData;
        private System.Windows.Forms.Label m_LabelCurControl;
        private SmartApp.Ihm.FilledRectProperties m_FilledRectPropPanel;
    }
}

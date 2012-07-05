namespace CommonLib
{
    partial class ScreenItemStdPropertiesPanel
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
            this.m_checkScreenEvent = new System.Windows.Forms.CheckBox();
            this.m_checkReadOnly = new System.Windows.Forms.CheckBox();
            this.m_EditText = new System.Windows.Forms.TextBox();
            this.labelText = new System.Windows.Forms.Label();
            this.m_panelPlaceSpec = new System.Windows.Forms.Panel();
            this.btn_pickdata = new System.Windows.Forms.Button();
            this.btn_SelFont = new System.Windows.Forms.Button();
            this.lbl_Font = new System.Windows.Forms.Label();
            this.btn_FontColor = new System.Windows.Forms.Button();
            this.panel_fontColor = new System.Windows.Forms.Panel();
            this.m_EditAssociateData = new CommonLib.SymbolTextBox();
            this.m_labelAssoData = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // m_checkScreenEvent
            // 
            this.m_checkScreenEvent.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_checkScreenEvent.AutoSize = true;
            this.m_checkScreenEvent.Location = new System.Drawing.Point(7, 137);
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
            this.m_checkReadOnly.Location = new System.Drawing.Point(145, 136);
            this.m_checkReadOnly.Name = "m_checkReadOnly";
            this.m_checkReadOnly.Size = new System.Drawing.Size(76, 17);
            this.m_checkReadOnly.TabIndex = 10;
            this.m_checkReadOnly.Text = "Read Only";
            this.m_checkReadOnly.UseVisualStyleBackColor = true;
            this.m_checkReadOnly.Visible = false;
            // 
            // m_EditText
            // 
            this.m_EditText.Location = new System.Drawing.Point(3, 57);
            this.m_EditText.Name = "m_EditText";
            this.m_EditText.Size = new System.Drawing.Size(342, 20);
            this.m_EditText.TabIndex = 12;
            // 
            // labelText
            // 
            this.labelText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.labelText.AutoSize = true;
            this.labelText.Location = new System.Drawing.Point(3, 41);
            this.labelText.Name = "labelText";
            this.labelText.Size = new System.Drawing.Size(28, 13);
            this.labelText.TabIndex = 11;
            this.labelText.Text = "Text";
            // 
            // m_panelPlaceSpec
            // 
            this.m_panelPlaceSpec.Location = new System.Drawing.Point(7, 160);
            this.m_panelPlaceSpec.Name = "m_panelPlaceSpec";
            this.m_panelPlaceSpec.Size = new System.Drawing.Size(37, 20);
            this.m_panelPlaceSpec.TabIndex = 15;
            this.m_panelPlaceSpec.Visible = false;
            // 
            // btn_pickdata
            // 
            this.btn_pickdata.Location = new System.Drawing.Point(264, 17);
            this.btn_pickdata.Name = "btn_pickdata";
            this.btn_pickdata.Size = new System.Drawing.Size(78, 23);
            this.btn_pickdata.TabIndex = 16;
            this.btn_pickdata.Text = "Pick";
            this.btn_pickdata.UseVisualStyleBackColor = true;
            this.btn_pickdata.Click += new System.EventHandler(this.btn_pickdata_Click);
            // 
            // btn_SelFont
            // 
            this.btn_SelFont.Location = new System.Drawing.Point(4, 84);
            this.btn_SelFont.Name = "btn_SelFont";
            this.btn_SelFont.Size = new System.Drawing.Size(66, 23);
            this.btn_SelFont.TabIndex = 17;
            this.btn_SelFont.Text = "Font";
            this.btn_SelFont.UseVisualStyleBackColor = true;
            this.btn_SelFont.Click += new System.EventHandler(this.btn_SelFont_Click);
            // 
            // lbl_Font
            // 
            this.lbl_Font.AutoSize = true;
            this.lbl_Font.Location = new System.Drawing.Point(76, 89);
            this.lbl_Font.Name = "lbl_Font";
            this.lbl_Font.Size = new System.Drawing.Size(28, 13);
            this.lbl_Font.TabIndex = 18;
            this.lbl_Font.Text = "Font";
            // 
            // btn_FontColor
            // 
            this.btn_FontColor.Location = new System.Drawing.Point(4, 109);
            this.btn_FontColor.Name = "btn_FontColor";
            this.btn_FontColor.Size = new System.Drawing.Size(100, 23);
            this.btn_FontColor.TabIndex = 17;
            this.btn_FontColor.Text = "Font color";
            this.btn_FontColor.UseVisualStyleBackColor = true;
            this.btn_FontColor.Click += new System.EventHandler(this.btn_FontColor_Click);
            // 
            // panel_fontColor
            // 
            this.panel_fontColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel_fontColor.Location = new System.Drawing.Point(110, 112);
            this.panel_fontColor.Name = "panel_fontColor";
            this.panel_fontColor.Size = new System.Drawing.Size(19, 17);
            this.panel_fontColor.TabIndex = 19;
            // 
            // m_EditAssociateData
            // 
            this.m_EditAssociateData.AllowDrop = true;
            this.m_EditAssociateData.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.m_EditAssociateData.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.m_EditAssociateData.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.m_EditAssociateData.Location = new System.Drawing.Point(4, 17);
            this.m_EditAssociateData.Name = "m_EditAssociateData";
            this.m_EditAssociateData.Size = new System.Drawing.Size(254, 20);
            this.m_EditAssociateData.TabIndex = 13;
            this.m_EditAssociateData.DragDrop += new System.Windows.Forms.DragEventHandler(this.OnEditAssociateDataDragDrop);
            this.m_EditAssociateData.DragEnter += new System.Windows.Forms.DragEventHandler(this.OnEditAssociateDataDragEnter);
            // 
            // m_labelAssoData
            // 
            this.m_labelAssoData.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_labelAssoData.AutoSize = true;
            this.m_labelAssoData.Location = new System.Drawing.Point(3, 0);
            this.m_labelAssoData.Name = "m_labelAssoData";
            this.m_labelAssoData.Size = new System.Drawing.Size(79, 13);
            this.m_labelAssoData.TabIndex = 7;
            this.m_labelAssoData.Text = "Associate Data";
            // 
            // ScreenItemStdPropertiesPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.panel_fontColor);
            this.Controls.Add(this.lbl_Font);
            this.Controls.Add(this.btn_FontColor);
            this.Controls.Add(this.btn_SelFont);
            this.Controls.Add(this.btn_pickdata);
            this.Controls.Add(this.m_panelPlaceSpec);
            this.Controls.Add(this.m_EditAssociateData);
            this.Controls.Add(this.m_EditText);
            this.Controls.Add(this.labelText);
            this.Controls.Add(this.m_checkReadOnly);
            this.Controls.Add(this.m_checkScreenEvent);
            this.Controls.Add(this.m_labelAssoData);
            this.Name = "ScreenItemStdPropertiesPanel";
            this.Size = new System.Drawing.Size(350, 194);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox m_checkScreenEvent;
        private System.Windows.Forms.CheckBox m_checkReadOnly;
        private System.Windows.Forms.TextBox m_EditText;
        private System.Windows.Forms.Label labelText;
        private CommonLib.SymbolTextBox m_EditAssociateData;
        private System.Windows.Forms.Panel m_panelPlaceSpec;
        private System.Windows.Forms.Button btn_pickdata;
        private System.Windows.Forms.Button btn_SelFont;
        private System.Windows.Forms.Label lbl_Font;
        private System.Windows.Forms.Button btn_FontColor;
        private System.Windows.Forms.Panel panel_fontColor;
        private System.Windows.Forms.Label m_labelAssoData;
    }
}

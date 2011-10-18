namespace SmartApp.Ihm
{
    partial class LoggerPropertiesControl
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
            this.m_LoggerPeriod = new System.Windows.Forms.NumericUpDown();
            this.m_cboLogType = new System.Windows.Forms.ComboBox();
            this.m_cboSeparator = new System.Windows.Forms.ComboBox();
            this.m_ListViewData = new System.Windows.Forms.ListView();
            this.m_colDataSymb = new System.Windows.Forms.ColumnHeader();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.m_labelSymbol = new System.Windows.Forms.Label();
            this.m_labelDesc = new System.Windows.Forms.Label();
            this.m_richTextDesc = new System.Windows.Forms.RichTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.m_chkAutoStart = new System.Windows.Forms.CheckBox();
            this.m_chkNotKeepFileOpen = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.m_cboNaming = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.edtDateFormat = new System.Windows.Forms.TextBox();
            this.btnAddData = new System.Windows.Forms.Button();
            this.edtFormatHelp = new System.Windows.Forms.TextBox();
            this.m_txtFileName = new SmartApp.Ihm.SymbolTextBox();
            this.m_textSymbol = new SmartApp.Ihm.SymbolTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.m_LoggerPeriod)).BeginInit();
            this.SuspendLayout();
            // 
            // m_LoggerPeriod
            // 
            this.m_LoggerPeriod.Location = new System.Drawing.Point(221, 66);
            this.m_LoggerPeriod.Name = "m_LoggerPeriod";
            this.m_LoggerPeriod.Size = new System.Drawing.Size(81, 20);
            this.m_LoggerPeriod.TabIndex = 22;
            // 
            // m_cboLogType
            // 
            this.m_cboLogType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cboLogType.FormattingEnabled = true;
            this.m_cboLogType.Location = new System.Drawing.Point(221, 26);
            this.m_cboLogType.Name = "m_cboLogType";
            this.m_cboLogType.Size = new System.Drawing.Size(195, 21);
            this.m_cboLogType.TabIndex = 21;
            this.m_cboLogType.SelectedIndexChanged += new System.EventHandler(this.m_cboLogType_SelectedIndexChanged);
            // 
            // m_cboSeparator
            // 
            this.m_cboSeparator.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cboSeparator.FormattingEnabled = true;
            this.m_cboSeparator.Location = new System.Drawing.Point(221, 167);
            this.m_cboSeparator.Name = "m_cboSeparator";
            this.m_cboSeparator.Size = new System.Drawing.Size(108, 21);
            this.m_cboSeparator.TabIndex = 23;
            // 
            // m_ListViewData
            // 
            this.m_ListViewData.AllowDrop = true;
            this.m_ListViewData.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.m_colDataSymb});
            this.m_ListViewData.FullRowSelect = true;
            this.m_ListViewData.GridLines = true;
            this.m_ListViewData.HideSelection = false;
            this.m_ListViewData.Location = new System.Drawing.Point(0, 127);
            this.m_ListViewData.MultiSelect = false;
            this.m_ListViewData.Name = "m_ListViewData";
            this.m_ListViewData.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.m_ListViewData.Size = new System.Drawing.Size(212, 248);
            this.m_ListViewData.TabIndex = 20;
            this.m_ListViewData.UseCompatibleStateImageBehavior = false;
            this.m_ListViewData.View = System.Windows.Forms.View.Details;
            this.m_ListViewData.SelectedIndexChanged += new System.EventHandler(this.OnListViewDataDragLeave);
            this.m_ListViewData.DragDrop += new System.Windows.Forms.DragEventHandler(this.OnListViewDataDragDrop);
            this.m_ListViewData.DragEnter += new System.Windows.Forms.DragEventHandler(this.OnListViewDataDragEnter);
            this.m_ListViewData.DragLeave += new System.EventHandler(this.OnListViewDataDragLeave);
            this.m_ListViewData.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnListViewDataKeyDown);
            this.m_ListViewData.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.OnListViewDataItemDrag);
            this.m_ListViewData.DragOver += new System.Windows.Forms.DragEventHandler(this.OnListViewDataDragOver);
            // 
            // m_colDataSymb
            // 
            this.m_colDataSymb.Text = "Data Symbol";
            this.m_colDataSymb.Width = 200;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(221, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 13);
            this.label1.TabIndex = 15;
            this.label1.Text = "Logger Period";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(218, 112);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(84, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = "Output file name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(221, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 13);
            this.label2.TabIndex = 17;
            this.label2.Text = "Logger Type";
            // 
            // m_labelSymbol
            // 
            this.m_labelSymbol.AutoSize = true;
            this.m_labelSymbol.Location = new System.Drawing.Point(3, 85);
            this.m_labelSymbol.Name = "m_labelSymbol";
            this.m_labelSymbol.Size = new System.Drawing.Size(41, 13);
            this.m_labelSymbol.TabIndex = 16;
            this.m_labelSymbol.Text = "Symbol";
            // 
            // m_labelDesc
            // 
            this.m_labelDesc.AutoSize = true;
            this.m_labelDesc.Location = new System.Drawing.Point(3, 10);
            this.m_labelDesc.Name = "m_labelDesc";
            this.m_labelDesc.Size = new System.Drawing.Size(60, 13);
            this.m_labelDesc.TabIndex = 13;
            this.m_labelDesc.Text = "Description";
            // 
            // m_richTextDesc
            // 
            this.m_richTextDesc.Location = new System.Drawing.Point(3, 26);
            this.m_richTextDesc.Name = "m_richTextDesc";
            this.m_richTextDesc.Size = new System.Drawing.Size(209, 53);
            this.m_richTextDesc.TabIndex = 12;
            this.m_richTextDesc.Text = "";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(360, 133);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(27, 13);
            this.label4.TabIndex = 24;
            this.label4.Text = ".csv";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(303, 68);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(20, 13);
            this.label5.TabIndex = 24;
            this.label5.Text = "ms";
            // 
            // m_chkAutoStart
            // 
            this.m_chkAutoStart.AutoSize = true;
            this.m_chkAutoStart.Location = new System.Drawing.Point(331, 67);
            this.m_chkAutoStart.Name = "m_chkAutoStart";
            this.m_chkAutoStart.Size = new System.Drawing.Size(73, 17);
            this.m_chkAutoStart.TabIndex = 29;
            this.m_chkAutoStart.Text = "Auto Start";
            this.m_chkAutoStart.UseVisualStyleBackColor = true;
            // 
            // m_chkNotKeepFileOpen
            // 
            this.m_chkNotKeepFileOpen.AutoSize = true;
            this.m_chkNotKeepFileOpen.Location = new System.Drawing.Point(221, 92);
            this.m_chkNotKeepFileOpen.Name = "m_chkNotKeepFileOpen";
            this.m_chkNotKeepFileOpen.Size = new System.Drawing.Size(128, 17);
            this.m_chkNotKeepFileOpen.TabIndex = 0;
            this.m_chkNotKeepFileOpen.Text = "Do not keep file open";
            this.m_chkNotKeepFileOpen.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(218, 151);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(73, 13);
            this.label6.TabIndex = 17;
            this.label6.Text = "csv Separator";
            // 
            // m_cboNaming
            // 
            this.m_cboNaming.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cboNaming.FormattingEnabled = true;
            this.m_cboNaming.Location = new System.Drawing.Point(221, 207);
            this.m_cboNaming.Name = "m_cboNaming";
            this.m_cboNaming.Size = new System.Drawing.Size(195, 21);
            this.m_cboNaming.TabIndex = 23;
            this.m_cboNaming.SelectedIndexChanged += new System.EventHandler(this.m_cboNaming_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(218, 191);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(78, 13);
            this.label7.TabIndex = 17;
            this.label7.Text = "Log file naming";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(221, 231);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(62, 13);
            this.label8.TabIndex = 30;
            this.label8.Text = "Date format";
            // 
            // edtDateFormat
            // 
            this.edtDateFormat.Location = new System.Drawing.Point(221, 247);
            this.edtDateFormat.Name = "edtDateFormat";
            this.edtDateFormat.Size = new System.Drawing.Size(195, 20);
            this.edtDateFormat.TabIndex = 31;
            // 
            // btnAddData
            // 
            this.btnAddData.Location = new System.Drawing.Point(0, 381);
            this.btnAddData.Name = "btnAddData";
            this.btnAddData.Size = new System.Drawing.Size(212, 23);
            this.btnAddData.TabIndex = 32;
            this.btnAddData.Text = "Add Datas";
            this.btnAddData.UseVisualStyleBackColor = true;
            this.btnAddData.Click += new System.EventHandler(this.btnAddData_Click);
            // 
            // edtFormatHelp
            // 
            this.edtFormatHelp.AcceptsReturn = true;
            this.edtFormatHelp.Location = new System.Drawing.Point(221, 273);
            this.edtFormatHelp.Multiline = true;
            this.edtFormatHelp.Name = "edtFormatHelp";
            this.edtFormatHelp.ReadOnly = true;
            this.edtFormatHelp.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.edtFormatHelp.Size = new System.Drawing.Size(195, 131);
            this.edtFormatHelp.TabIndex = 33;
            // 
            // m_txtFileName
            // 
            this.m_txtFileName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.m_txtFileName.Location = new System.Drawing.Point(221, 128);
            this.m_txtFileName.Name = "m_txtFileName";
            this.m_txtFileName.Size = new System.Drawing.Size(137, 20);
            this.m_txtFileName.TabIndex = 18;
            // 
            // m_textSymbol
            // 
            this.m_textSymbol.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.m_textSymbol.Location = new System.Drawing.Point(3, 101);
            this.m_textSymbol.Name = "m_textSymbol";
            this.m_textSymbol.Size = new System.Drawing.Size(209, 20);
            this.m_textSymbol.TabIndex = 19;
            // 
            // LoggerPropertiesControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.edtFormatHelp);
            this.Controls.Add(this.btnAddData);
            this.Controls.Add(this.edtDateFormat);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.m_chkAutoStart);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.m_LoggerPeriod);
            this.Controls.Add(this.m_cboLogType);
            this.Controls.Add(this.m_ListViewData);
            this.Controls.Add(this.m_txtFileName);
            this.Controls.Add(this.m_textSymbol);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.m_labelSymbol);
            this.Controls.Add(this.m_labelDesc);
            this.Controls.Add(this.m_richTextDesc);
            this.Controls.Add(this.m_cboNaming);
            this.Controls.Add(this.m_cboSeparator);
            this.Controls.Add(this.m_chkNotKeepFileOpen);
            this.Name = "LoggerPropertiesControl";
            this.Size = new System.Drawing.Size(541, 416);
            this.Validating += new System.ComponentModel.CancelEventHandler(this.PropertiesControlValidating);
            ((System.ComponentModel.ISupportInitialize)(this.m_LoggerPeriod)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown m_LoggerPeriod;
        private System.Windows.Forms.ComboBox m_cboLogType;
        private System.Windows.Forms.ListView m_ListViewData;
        private SymbolTextBox m_txtFileName;
        private SymbolTextBox m_textSymbol;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label m_labelSymbol;
        private System.Windows.Forms.Label m_labelDesc;
        private System.Windows.Forms.RichTextBox m_richTextDesc;
        private System.Windows.Forms.ColumnHeader m_colDataSymb;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox m_chkAutoStart;
        private System.Windows.Forms.CheckBox m_chkNotKeepFileOpen;
        private System.Windows.Forms.ComboBox m_cboSeparator;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox m_cboNaming;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox edtDateFormat;
        private System.Windows.Forms.Button btnAddData;
        private System.Windows.Forms.TextBox edtFormatHelp;
    }
}

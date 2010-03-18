namespace SmartApp.Ihm.Wizards
{
    partial class WizardSLFormZ2
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
            this.label1 = new System.Windows.Forms.Label();
            this.m_cboFrameType = new System.Windows.Forms.ComboBox();
            this.m_cboAdressRange = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.m_ListViewDatas = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.label3 = new System.Windows.Forms.Label();
            this.m_cboReadWrite = new System.Windows.Forms.ComboBox();
            this.m_btnSplit = new System.Windows.Forms.Button();
            this.m_btnJoin = new System.Windows.Forms.Button();
            this.m_textFramesymb = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.m_textGeneFramesymb = new System.Windows.Forms.TextBox();
            this.m_checkGenerateResp = new System.Windows.Forms.CheckBox();
            this.m_btnOk = new System.Windows.Forms.Button();
            this.m_btnCancel = new System.Windows.Forms.Button();
            this.m_PicSLBloc = new System.Windows.Forms.PictureBox();
            this.label6 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.m_PicSLBloc)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Frame type";
            // 
            // m_cboFrameType
            // 
            this.m_cboFrameType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cboFrameType.FormattingEnabled = true;
            this.m_cboFrameType.Location = new System.Drawing.Point(16, 30);
            this.m_cboFrameType.Name = "m_cboFrameType";
            this.m_cboFrameType.Size = new System.Drawing.Size(213, 21);
            this.m_cboFrameType.TabIndex = 1;
            this.m_cboFrameType.SelectedIndexChanged += new System.EventHandler(this.OncboFrameTypeSelectedIndexChanged);
            // 
            // m_cboAdressRange
            // 
            this.m_cboAdressRange.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cboAdressRange.FormattingEnabled = true;
            this.m_cboAdressRange.Location = new System.Drawing.Point(16, 110);
            this.m_cboAdressRange.Name = "m_cboAdressRange";
            this.m_cboAdressRange.Size = new System.Drawing.Size(213, 21);
            this.m_cboAdressRange.TabIndex = 1;
            this.m_cboAdressRange.SelectedIndexChanged += new System.EventHandler(this.OncboAdressRangeSelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 94);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Address range";
            // 
            // m_ListViewDatas
            // 
            this.m_ListViewDatas.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_ListViewDatas.AutoArrange = false;
            this.m_ListViewDatas.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.m_ListViewDatas.FullRowSelect = true;
            this.m_ListViewDatas.GridLines = true;
            this.m_ListViewDatas.HideSelection = false;
            this.m_ListViewDatas.Location = new System.Drawing.Point(235, 12);
            this.m_ListViewDatas.Name = "m_ListViewDatas";
            this.m_ListViewDatas.ShowGroups = false;
            this.m_ListViewDatas.Size = new System.Drawing.Size(370, 373);
            this.m_ListViewDatas.TabIndex = 2;
            this.m_ListViewDatas.UseCompatibleStateImageBehavior = false;
            this.m_ListViewDatas.View = System.Windows.Forms.View.Details;
            this.m_ListViewDatas.SelectedIndexChanged += new System.EventHandler(this.m_ListViewDatas_SelectedIndexChanged);
            this.m_ListViewDatas.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.m_ListViewDatas_KeyPress);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Data symbol";
            this.columnHeader1.Width = 183;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Data size";
            this.columnHeader2.Width = 62;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "SL Input/Ouput n°";
            this.columnHeader3.Width = 113;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 54);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Read / Write";
            // 
            // m_cboReadWrite
            // 
            this.m_cboReadWrite.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cboReadWrite.FormattingEnabled = true;
            this.m_cboReadWrite.Location = new System.Drawing.Point(16, 70);
            this.m_cboReadWrite.Name = "m_cboReadWrite";
            this.m_cboReadWrite.Size = new System.Drawing.Size(213, 21);
            this.m_cboReadWrite.TabIndex = 1;
            this.m_cboReadWrite.SelectedIndexChanged += new System.EventHandler(this.OncboReadWriteSelectedIndexChanged);
            // 
            // m_btnSplit
            // 
            this.m_btnSplit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.m_btnSplit.Location = new System.Drawing.Point(282, 392);
            this.m_btnSplit.Name = "m_btnSplit";
            this.m_btnSplit.Size = new System.Drawing.Size(113, 23);
            this.m_btnSplit.TabIndex = 3;
            this.m_btnSplit.Text = "Split selected data";
            this.m_btnSplit.UseVisualStyleBackColor = true;
            this.m_btnSplit.Click += new System.EventHandler(this.m_btnSplit_Click);
            // 
            // m_btnJoin
            // 
            this.m_btnJoin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.m_btnJoin.Location = new System.Drawing.Point(428, 392);
            this.m_btnJoin.Name = "m_btnJoin";
            this.m_btnJoin.Size = new System.Drawing.Size(113, 23);
            this.m_btnJoin.TabIndex = 3;
            this.m_btnJoin.Text = "Join selected datas";
            this.m_btnJoin.UseVisualStyleBackColor = true;
            this.m_btnJoin.Click += new System.EventHandler(this.m_btnJoin_Click);
            // 
            // m_textFramesymb
            // 
            this.m_textFramesymb.Location = new System.Drawing.Point(16, 150);
            this.m_textFramesymb.Name = "m_textFramesymb";
            this.m_textFramesymb.Size = new System.Drawing.Size(213, 20);
            this.m_textFramesymb.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 134);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Frame symbol";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 195);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(106, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Answer frame symbol";
            // 
            // m_textGeneFramesymb
            // 
            this.m_textGeneFramesymb.Location = new System.Drawing.Point(16, 211);
            this.m_textGeneFramesymb.Name = "m_textGeneFramesymb";
            this.m_textGeneFramesymb.Size = new System.Drawing.Size(213, 20);
            this.m_textGeneFramesymb.TabIndex = 4;
            // 
            // m_checkGenerateResp
            // 
            this.m_checkGenerateResp.AutoSize = true;
            this.m_checkGenerateResp.Location = new System.Drawing.Point(16, 175);
            this.m_checkGenerateResp.Name = "m_checkGenerateResp";
            this.m_checkGenerateResp.Size = new System.Drawing.Size(136, 17);
            this.m_checkGenerateResp.TabIndex = 5;
            this.m_checkGenerateResp.Text = "Generate answer frame";
            this.m_checkGenerateResp.UseVisualStyleBackColor = true;
            this.m_checkGenerateResp.CheckedChanged += new System.EventHandler(this.OncheckGenerateRespCheckedChanged);
            // 
            // m_btnOk
            // 
            this.m_btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.m_btnOk.Location = new System.Drawing.Point(12, 392);
            this.m_btnOk.Name = "m_btnOk";
            this.m_btnOk.Size = new System.Drawing.Size(87, 23);
            this.m_btnOk.TabIndex = 6;
            this.m_btnOk.Text = "OK";
            this.m_btnOk.UseVisualStyleBackColor = true;
            this.m_btnOk.Click += new System.EventHandler(this.OnbtnOkClick);
            // 
            // m_btnCancel
            // 
            this.m_btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.m_btnCancel.Location = new System.Drawing.Point(105, 392);
            this.m_btnCancel.Name = "m_btnCancel";
            this.m_btnCancel.Size = new System.Drawing.Size(87, 23);
            this.m_btnCancel.TabIndex = 6;
            this.m_btnCancel.Text = "Cancel";
            this.m_btnCancel.UseVisualStyleBackColor = true;
            this.m_btnCancel.Click += new System.EventHandler(this.OnbtnCancelClick);
            // 
            // m_PicSLBloc
            // 
            this.m_PicSLBloc.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.m_PicSLBloc.ErrorImage = null;
            this.m_PicSLBloc.InitialImage = null;
            this.m_PicSLBloc.Location = new System.Drawing.Point(21, 251);
            this.m_PicSLBloc.Name = "m_PicSLBloc";
            this.m_PicSLBloc.Size = new System.Drawing.Size(61, 129);
            this.m_PicSLBloc.TabIndex = 7;
            this.m_PicSLBloc.TabStop = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(18, 235);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(95, 13);
            this.label6.TabIndex = 8;
            this.label6.Text = "Z2 SL Bloc Bitmap";
            // 
            // WizardSLFormZ2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(617, 422);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.m_PicSLBloc);
            this.Controls.Add(this.m_btnCancel);
            this.Controls.Add(this.m_btnOk);
            this.Controls.Add(this.m_checkGenerateResp);
            this.Controls.Add(this.m_textGeneFramesymb);
            this.Controls.Add(this.m_textFramesymb);
            this.Controls.Add(this.m_btnJoin);
            this.Controls.Add(this.m_btnSplit);
            this.Controls.Add(this.m_ListViewDatas);
            this.Controls.Add(this.m_cboReadWrite);
            this.Controls.Add(this.m_cboAdressRange);
            this.Controls.Add(this.m_cboFrameType);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "WizardSLFormZ2";
            this.Text = "Wizard SL Bloc Z2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.ShowInTaskbar = false;
            ((System.ComponentModel.ISupportInitialize)(this.m_PicSLBloc)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox m_cboFrameType;
        private System.Windows.Forms.ComboBox m_cboAdressRange;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListView m_ListViewDatas;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox m_cboReadWrite;
        private System.Windows.Forms.Button m_btnSplit;
        private System.Windows.Forms.Button m_btnJoin;
        private System.Windows.Forms.TextBox m_textFramesymb;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox m_textGeneFramesymb;
        private System.Windows.Forms.CheckBox m_checkGenerateResp;
        private System.Windows.Forms.Button m_btnOk;
        private System.Windows.Forms.Button m_btnCancel;
        private System.Windows.Forms.PictureBox m_PicSLBloc;
        private System.Windows.Forms.Label label6;
    }
}
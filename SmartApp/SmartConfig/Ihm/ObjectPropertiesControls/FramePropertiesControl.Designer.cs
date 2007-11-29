namespace SmartApp.Ihm
{
    partial class FramePropertiesControl
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.m_cboConvTo = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.m_cboConvFrom = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.m_cboConvType = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.m_cboCtrlDataSize = new System.Windows.Forms.ComboBox();
            this.m_cboCtrlDataTo = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.m_cboCtrlDataFrom = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.m_cboCtrlDataType = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.m_textSymbol = new SmartApp.Ihm.SymbolTextBox();
            this.m_richTextDesc = new System.Windows.Forms.RichTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.m_cboConvTo);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.m_cboConvFrom);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.m_cboConvType);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Location = new System.Drawing.Point(216, 121);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(200, 151);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Conversion";
            // 
            // m_cboConvTo
            // 
            this.m_cboConvTo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cboConvTo.FormattingEnabled = true;
            this.m_cboConvTo.Location = new System.Drawing.Point(15, 118);
            this.m_cboConvTo.Name = "m_cboConvTo";
            this.m_cboConvTo.Size = new System.Drawing.Size(170, 21);
            this.m_cboConvTo.TabIndex = 3;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(15, 18);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(31, 13);
            this.label7.TabIndex = 0;
            this.label7.Text = "Type";
            // 
            // m_cboConvFrom
            // 
            this.m_cboConvFrom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cboConvFrom.FormattingEnabled = true;
            this.m_cboConvFrom.Location = new System.Drawing.Point(15, 74);
            this.m_cboConvFrom.Name = "m_cboConvFrom";
            this.m_cboConvFrom.Size = new System.Drawing.Size(170, 21);
            this.m_cboConvFrom.TabIndex = 3;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(15, 103);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(20, 13);
            this.label9.TabIndex = 0;
            this.label9.Text = "To";
            // 
            // m_cboConvType
            // 
            this.m_cboConvType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cboConvType.FormattingEnabled = true;
            this.m_cboConvType.Location = new System.Drawing.Point(15, 33);
            this.m_cboConvType.Name = "m_cboConvType";
            this.m_cboConvType.Size = new System.Drawing.Size(170, 21);
            this.m_cboConvType.TabIndex = 3;
            this.m_cboConvType.SelectedIndexChanged += new System.EventHandler(this.OnComboConvChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(15, 59);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(30, 13);
            this.label10.TabIndex = 0;
            this.label10.Text = "From";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.m_cboCtrlDataSize);
            this.groupBox1.Controls.Add(this.m_cboCtrlDataTo);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.m_cboCtrlDataFrom);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.m_cboCtrlDataType);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Location = new System.Drawing.Point(3, 121);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 202);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Control Data";
            // 
            // m_cboCtrlDataSize
            // 
            this.m_cboCtrlDataSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cboCtrlDataSize.FormattingEnabled = true;
            this.m_cboCtrlDataSize.Location = new System.Drawing.Point(13, 77);
            this.m_cboCtrlDataSize.Name = "m_cboCtrlDataSize";
            this.m_cboCtrlDataSize.Size = new System.Drawing.Size(170, 21);
            this.m_cboCtrlDataSize.TabIndex = 3;
            // 
            // m_cboCtrlDataTo
            // 
            this.m_cboCtrlDataTo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cboCtrlDataTo.FormattingEnabled = true;
            this.m_cboCtrlDataTo.Location = new System.Drawing.Point(13, 165);
            this.m_cboCtrlDataTo.Name = "m_cboCtrlDataTo";
            this.m_cboCtrlDataTo.Size = new System.Drawing.Size(170, 21);
            this.m_cboCtrlDataTo.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 18);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(31, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Type";
            // 
            // m_cboCtrlDataFrom
            // 
            this.m_cboCtrlDataFrom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cboCtrlDataFrom.FormattingEnabled = true;
            this.m_cboCtrlDataFrom.Location = new System.Drawing.Point(13, 121);
            this.m_cboCtrlDataFrom.Name = "m_cboCtrlDataFrom";
            this.m_cboCtrlDataFrom.Size = new System.Drawing.Size(170, 21);
            this.m_cboCtrlDataFrom.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 60);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(27, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Size";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 148);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(20, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "To";
            // 
            // m_cboCtrlDataType
            // 
            this.m_cboCtrlDataType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cboCtrlDataType.FormattingEnabled = true;
            this.m_cboCtrlDataType.Location = new System.Drawing.Point(13, 35);
            this.m_cboCtrlDataType.Name = "m_cboCtrlDataType";
            this.m_cboCtrlDataType.Size = new System.Drawing.Size(170, 21);
            this.m_cboCtrlDataType.TabIndex = 3;
            this.m_cboCtrlDataType.SelectedIndexChanged += new System.EventHandler(this.OnComboDataCtrlChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 104);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(30, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "From";
            // 
            // m_textSymbol
            // 
            this.m_textSymbol.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.m_textSymbol.Location = new System.Drawing.Point(3, 95);
            this.m_textSymbol.Name = "m_textSymbol";
            this.m_textSymbol.Size = new System.Drawing.Size(331, 20);
            this.m_textSymbol.TabIndex = 8;
            // 
            // m_richTextDesc
            // 
            this.m_richTextDesc.Location = new System.Drawing.Point(3, 17);
            this.m_richTextDesc.Name = "m_richTextDesc";
            this.m_richTextDesc.Size = new System.Drawing.Size(413, 58);
            this.m_richTextDesc.TabIndex = 7;
            this.m_richTextDesc.Text = "";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 78);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Symbol";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Description";
            // 
            // FramePropertiesControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.m_textSymbol);
            this.Controls.Add(this.m_richTextDesc);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "FramePropertiesControl";
            this.Size = new System.Drawing.Size(441, 340);
            this.Validating += new System.ComponentModel.CancelEventHandler(this.PropertiesControlValidating);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox m_cboConvTo;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox m_cboConvFrom;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox m_cboConvType;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox m_cboCtrlDataSize;
        private System.Windows.Forms.ComboBox m_cboCtrlDataTo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox m_cboCtrlDataFrom;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox m_cboCtrlDataType;
        private System.Windows.Forms.Label label5;
        private SmartApp.Ihm.SymbolTextBox m_textSymbol;
        private System.Windows.Forms.RichTextBox m_richTextDesc;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}

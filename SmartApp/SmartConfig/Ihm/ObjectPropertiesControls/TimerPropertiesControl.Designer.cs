namespace SmartApp.Ihm
{
    partial class TimerPropertiesControl
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
            this.m_BtnEditScript = new System.Windows.Forms.Button();
            this.m_labelScript = new System.Windows.Forms.Label();
            this.m_textSymbol = new CommonLib.SymbolTextBox();
            this.m_labelSymbol = new System.Windows.Forms.Label();
            this.m_labelDesc = new System.Windows.Forms.Label();
            this.m_richTextDesc = new System.Windows.Forms.RichTextBox();
            this.m_EditScript = new System.Windows.Forms.RichTextBox();
            this.m_NumPeriod = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.m_chkAutoStart = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumPeriod)).BeginInit();
            this.SuspendLayout();
            // 
            // m_BtnEditScript
            // 
            this.m_BtnEditScript.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.m_BtnEditScript.Location = new System.Drawing.Point(3, 305);
            this.m_BtnEditScript.Name = "m_BtnEditScript";
            this.m_BtnEditScript.Size = new System.Drawing.Size(139, 24);
            this.m_BtnEditScript.TabIndex = 23;
            this.m_BtnEditScript.Text = "Edit Script";
            this.m_BtnEditScript.UseVisualStyleBackColor = true;
            this.m_BtnEditScript.Click += new System.EventHandler(this.OnBtnEditScriptClick);
            // 
            // m_labelScript
            // 
            this.m_labelScript.AutoSize = true;
            this.m_labelScript.Location = new System.Drawing.Point(3, 153);
            this.m_labelScript.Name = "m_labelScript";
            this.m_labelScript.Size = new System.Drawing.Size(34, 13);
            this.m_labelScript.TabIndex = 22;
            this.m_labelScript.Text = "Script";
            // 
            // m_textSymbol
            // 
            this.m_textSymbol.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.m_textSymbol.Location = new System.Drawing.Point(6, 91);
            this.m_textSymbol.Name = "m_textSymbol";
            this.m_textSymbol.Size = new System.Drawing.Size(180, 20);
            this.m_textSymbol.TabIndex = 21;
            // 
            // m_labelSymbol
            // 
            this.m_labelSymbol.AutoSize = true;
            this.m_labelSymbol.Location = new System.Drawing.Point(3, 75);
            this.m_labelSymbol.Name = "m_labelSymbol";
            this.m_labelSymbol.Size = new System.Drawing.Size(41, 13);
            this.m_labelSymbol.TabIndex = 20;
            this.m_labelSymbol.Text = "Symbol";
            // 
            // m_labelDesc
            // 
            this.m_labelDesc.AutoSize = true;
            this.m_labelDesc.Location = new System.Drawing.Point(3, 0);
            this.m_labelDesc.Name = "m_labelDesc";
            this.m_labelDesc.Size = new System.Drawing.Size(60, 13);
            this.m_labelDesc.TabIndex = 19;
            this.m_labelDesc.Text = "Description";
            // 
            // m_richTextDesc
            // 
            this.m_richTextDesc.Location = new System.Drawing.Point(6, 16);
            this.m_richTextDesc.Name = "m_richTextDesc";
            this.m_richTextDesc.Size = new System.Drawing.Size(384, 53);
            this.m_richTextDesc.TabIndex = 18;
            this.m_richTextDesc.Text = "";
            // 
            // m_EditScript
            // 
            this.m_EditScript.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_EditScript.Location = new System.Drawing.Point(3, 169);
            this.m_EditScript.Name = "m_EditScript";
            this.m_EditScript.ReadOnly = true;
            this.m_EditScript.Size = new System.Drawing.Size(387, 130);
            this.m_EditScript.TabIndex = 17;
            this.m_EditScript.Text = "";
            // 
            // m_NumPeriod
            // 
            this.m_NumPeriod.Location = new System.Drawing.Point(209, 91);
            this.m_NumPeriod.Name = "m_NumPeriod";
            this.m_NumPeriod.Size = new System.Drawing.Size(120, 20);
            this.m_NumPeriod.TabIndex = 25;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(206, 75);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 13);
            this.label1.TabIndex = 24;
            this.label1.Text = "Timer Period";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(335, 93);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(20, 13);
            this.label5.TabIndex = 26;
            this.label5.Text = "ms";
            // 
            // m_chkAutoStart
            // 
            this.m_chkAutoStart.AutoSize = true;
            this.m_chkAutoStart.Location = new System.Drawing.Point(6, 124);
            this.m_chkAutoStart.Name = "m_chkAutoStart";
            this.m_chkAutoStart.Size = new System.Drawing.Size(73, 17);
            this.m_chkAutoStart.TabIndex = 27;
            this.m_chkAutoStart.Text = "Auto Start";
            this.m_chkAutoStart.UseVisualStyleBackColor = true;
            // 
            // TimerPropertiesControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.m_chkAutoStart);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.m_NumPeriod);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.m_BtnEditScript);
            this.Controls.Add(this.m_labelScript);
            this.Controls.Add(this.m_textSymbol);
            this.Controls.Add(this.m_labelSymbol);
            this.Controls.Add(this.m_labelDesc);
            this.Controls.Add(this.m_richTextDesc);
            this.Controls.Add(this.m_EditScript);
            this.Name = "TimerPropertiesControl";
            this.Size = new System.Drawing.Size(399, 332);
            this.Validating += new System.ComponentModel.CancelEventHandler(this.PropertiesControlValidating);
            ((System.ComponentModel.ISupportInitialize)(this.m_NumPeriod)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button m_BtnEditScript;
        private System.Windows.Forms.Label m_labelScript;
        private CommonLib.SymbolTextBox m_textSymbol;
        private System.Windows.Forms.Label m_labelSymbol;
        private System.Windows.Forms.Label m_labelDesc;
        private System.Windows.Forms.RichTextBox m_richTextDesc;
        private System.Windows.Forms.RichTextBox m_EditScript;
        private System.Windows.Forms.NumericUpDown m_NumPeriod;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox m_chkAutoStart;
    }
}

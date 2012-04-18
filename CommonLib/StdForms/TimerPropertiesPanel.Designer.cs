namespace CommonLib
{
    partial class TimerPropertiesPanel
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
            this.m_NumPeriod = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.m_chkAutoStart = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumPeriod)).BeginInit();
            this.SuspendLayout();
            // 
            // m_NumPeriod
            // 
            this.m_NumPeriod.Location = new System.Drawing.Point(0, 16);
            this.m_NumPeriod.Name = "m_NumPeriod";
            this.m_NumPeriod.Size = new System.Drawing.Size(120, 20);
            this.m_NumPeriod.TabIndex = 25;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(-3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 13);
            this.label1.TabIndex = 24;
            this.label1.Text = "Timer Period";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(126, 18);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(20, 13);
            this.label5.TabIndex = 26;
            this.label5.Text = "ms";
            // 
            // m_chkAutoStart
            // 
            this.m_chkAutoStart.AutoSize = true;
            this.m_chkAutoStart.Location = new System.Drawing.Point(0, 42);
            this.m_chkAutoStart.Name = "m_chkAutoStart";
            this.m_chkAutoStart.Size = new System.Drawing.Size(73, 17);
            this.m_chkAutoStart.TabIndex = 27;
            this.m_chkAutoStart.Text = "Auto Start";
            this.m_chkAutoStart.UseVisualStyleBackColor = true;
            // 
            // TimerPropertiesPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.m_chkAutoStart);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.m_NumPeriod);
            this.Controls.Add(this.label1);
            this.Name = "TimerPropertiesPanel";
            this.Size = new System.Drawing.Size(153, 65);
            ((System.ComponentModel.ISupportInitialize)(this.m_NumPeriod)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown m_NumPeriod;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox m_chkAutoStart;
    }
}

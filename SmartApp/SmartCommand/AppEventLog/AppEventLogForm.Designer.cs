namespace SmartApp.AppEventLog
{
    partial class AppEventLogForm
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
            this.m_lvEvent = new System.Windows.Forms.ListView();
            this.m_colTime = new System.Windows.Forms.ColumnHeader();
            this.m_colType = new System.Windows.Forms.ColumnHeader();
            this.m_colMessage = new System.Windows.Forms.ColumnHeader();
            this.label1 = new System.Windows.Forms.Label();
            this.m_btnClearLog = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // m_lvEvent
            // 
            this.m_lvEvent.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_lvEvent.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.m_colTime,
            this.m_colType,
            this.m_colMessage});
            this.m_lvEvent.FullRowSelect = true;
            this.m_lvEvent.Location = new System.Drawing.Point(12, 42);
            this.m_lvEvent.Name = "m_lvEvent";
            this.m_lvEvent.Size = new System.Drawing.Size(664, 166);
            this.m_lvEvent.TabIndex = 0;
            this.m_lvEvent.UseCompatibleStateImageBehavior = false;
            this.m_lvEvent.View = System.Windows.Forms.View.Details;
            // 
            // m_colTime
            // 
            this.m_colTime.Text = "Time";
            this.m_colTime.Width = 100;
            // 
            // m_colType
            // 
            this.m_colType.Text = "Type";
            this.m_colType.Width = 80;
            // 
            // m_colMessage
            // 
            this.m_colMessage.Text = "Message";
            this.m_colMessage.Width = 373;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Events";
            // 
            // m_btnClearLog
            // 
            this.m_btnClearLog.Location = new System.Drawing.Point(566, 9);
            this.m_btnClearLog.Name = "m_btnClearLog";
            this.m_btnClearLog.Size = new System.Drawing.Size(110, 23);
            this.m_btnClearLog.TabIndex = 2;
            this.m_btnClearLog.Text = "Clear";
            this.m_btnClearLog.UseVisualStyleBackColor = true;
            this.m_btnClearLog.Click += new System.EventHandler(this.m_btnClearLog_Click);
            // 
            // AppEventLogForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(688, 220);
            this.Controls.Add(this.m_btnClearLog);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.m_lvEvent);
            this.DoubleBuffered = true;
            this.Name = "AppEventLogForm";
            this.Text = "Application log";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AppEventLogForm_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView m_lvEvent;
        private System.Windows.Forms.ColumnHeader m_colTime;
        private System.Windows.Forms.ColumnHeader m_colType;
        private System.Windows.Forms.ColumnHeader m_colMessage;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button m_btnClearLog;
    }
}
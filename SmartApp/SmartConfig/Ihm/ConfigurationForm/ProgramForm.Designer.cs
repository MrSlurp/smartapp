namespace SmartApp.Ihm
{
    partial class ProgramForm
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
            this.m_TabCtrlProgramItems = new System.Windows.Forms.TabControl();
            this.m_TabFunction = new System.Windows.Forms.TabPage();
            this.m_PanelProgFunc = new SmartApp.Ihm.ProgItemPanels.FunctionPanel();
            this.m_TabTimer = new System.Windows.Forms.TabPage();
            this.m_PanelProgTimer = new SmartApp.Ihm.ProgItemPanels.TimerPanel();
            this.m_TabLogger = new System.Windows.Forms.TabPage();
            this.m_PanelProgLogger = new SmartApp.Ihm.ProgItemPanels.LoggerPanel();
            this.m_TabCtrlProgramItems.SuspendLayout();
            this.m_TabFunction.SuspendLayout();
            this.m_TabTimer.SuspendLayout();
            this.m_TabLogger.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_TabCtrlProgramItems
            // 
            this.m_TabCtrlProgramItems.Controls.Add(this.m_TabTimer);
            this.m_TabCtrlProgramItems.Controls.Add(this.m_TabLogger);
            this.m_TabCtrlProgramItems.Controls.Add(this.m_TabFunction);
            this.m_TabCtrlProgramItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_TabCtrlProgramItems.Location = new System.Drawing.Point(0, 0);
            this.m_TabCtrlProgramItems.Name = "m_TabCtrlProgramItems";
            this.m_TabCtrlProgramItems.SelectedIndex = 0;
            this.m_TabCtrlProgramItems.Size = new System.Drawing.Size(662, 391);
            this.m_TabCtrlProgramItems.TabIndex = 0;
            // 
            // m_TabFunction
            // 
            this.m_TabFunction.Controls.Add(this.m_PanelProgFunc);
            this.m_TabFunction.Location = new System.Drawing.Point(4, 22);
            this.m_TabFunction.Name = "m_TabFunction";
            this.m_TabFunction.Padding = new System.Windows.Forms.Padding(3);
            this.m_TabFunction.Size = new System.Drawing.Size(654, 365);
            this.m_TabFunction.TabIndex = 0;
            this.m_TabFunction.Text = "Functions";
            this.m_TabFunction.UseVisualStyleBackColor = true;
            // 
            // m_PanelProgFunc
            // 
            this.m_PanelProgFunc.Doc = null;
            this.m_PanelProgFunc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_PanelProgFunc.Location = new System.Drawing.Point(3, 3);
            this.m_PanelProgFunc.Name = "m_PanelProgFunc";
            this.m_PanelProgFunc.Size = new System.Drawing.Size(648, 359);
            this.m_PanelProgFunc.TabIndex = 0;
            // 
            // m_TabTimer
            // 
            this.m_TabTimer.Controls.Add(this.m_PanelProgTimer);
            this.m_TabTimer.Location = new System.Drawing.Point(4, 22);
            this.m_TabTimer.Name = "m_TabTimer";
            this.m_TabTimer.Padding = new System.Windows.Forms.Padding(3);
            this.m_TabTimer.Size = new System.Drawing.Size(654, 365);
            this.m_TabTimer.TabIndex = 1;
            this.m_TabTimer.Text = "Timers";
            this.m_TabTimer.UseVisualStyleBackColor = true;
            // 
            // m_PanelProgTimer
            // 
            this.m_PanelProgTimer.Doc = null;
            this.m_PanelProgTimer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_PanelProgTimer.Location = new System.Drawing.Point(3, 3);
            this.m_PanelProgTimer.Name = "m_PanelProgTimer";
            this.m_PanelProgTimer.Size = new System.Drawing.Size(648, 359);
            this.m_PanelProgTimer.TabIndex = 0;
            // 
            // m_TabLogger
            // 
            this.m_TabLogger.Controls.Add(this.m_PanelProgLogger);
            this.m_TabLogger.Location = new System.Drawing.Point(4, 22);
            this.m_TabLogger.Name = "m_TabLogger";
            this.m_TabLogger.Padding = new System.Windows.Forms.Padding(3);
            this.m_TabLogger.Size = new System.Drawing.Size(654, 365);
            this.m_TabLogger.TabIndex = 2;
            this.m_TabLogger.Text = "loggers";
            this.m_TabLogger.UseVisualStyleBackColor = true;
            // 
            // m_PanelProgLogger
            // 
            this.m_PanelProgLogger.Doc = null;
            this.m_PanelProgLogger.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_PanelProgLogger.Location = new System.Drawing.Point(3, 3);
            this.m_PanelProgLogger.Name = "m_PanelProgLogger";
            this.m_PanelProgLogger.Size = new System.Drawing.Size(648, 359);
            this.m_PanelProgLogger.TabIndex = 0;
            // 
            // ProgramForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(662, 391);
            this.Controls.Add(this.m_TabCtrlProgramItems);
            this.DoubleBuffered = true;
            this.MinimumSize = new System.Drawing.Size(670, 417);
            this.Name = "ProgramForm";
            this.ShowIcon = false;
            this.Text = "Program";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OnFormClosing);
            this.m_TabCtrlProgramItems.ResumeLayout(false);
            this.m_TabFunction.ResumeLayout(false);
            this.m_TabTimer.ResumeLayout(false);
            this.m_TabLogger.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl m_TabCtrlProgramItems;
        private System.Windows.Forms.TabPage m_TabFunction;
        private System.Windows.Forms.TabPage m_TabTimer;
        private System.Windows.Forms.TabPage m_TabLogger;
        private SmartApp.Ihm.ProgItemPanels.FunctionPanel m_PanelProgFunc;
        private SmartApp.Ihm.ProgItemPanels.TimerPanel m_PanelProgTimer;
        private SmartApp.Ihm.ProgItemPanels.LoggerPanel m_PanelProgLogger;
    }
}
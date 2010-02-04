namespace SmartApp
{
    partial class VirtualDataForm
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
            this.m_MainTabControl = new System.Windows.Forms.TabControl();
            this.m_tabControlDataPanels = new System.Windows.Forms.TabControl();
            this.m_BtnSaveCliche = new System.Windows.Forms.Button();
            this.m_BtnLoadCliche = new System.Windows.Forms.Button();
            this.m_PanelScenario = new SmartApp.ScenarioPanel();
            this.m_TabPageScenario = new System.Windows.Forms.TabPage();
            this.m_TabPageVData = new System.Windows.Forms.TabPage();
            this.SuspendLayout();
            this.m_MainTabControl.SuspendLayout();
            this.m_TabPageVData.SuspendLayout();
            this.m_TabPageScenario.SuspendLayout();
            this.m_PanelScenario.SuspendLayout();

            //
            // m_PanelScenario
            //
            this.m_PanelScenario.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_PanelScenario.Location = new System.Drawing.Point(0, 0);
            this.m_PanelScenario.Name = "m_tabControlDataPanels";
            this.m_tabControlDataPanels.TabIndex = 5;
            // 
            // m_tabControlDataPanels
            // 
            this.m_tabControlDataPanels.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left |
                                           System.Windows.Forms.AnchorStyles.Right | System.Windows.Forms.AnchorStyles.Bottom);
            this.m_tabControlDataPanels.Location = new System.Drawing.Point(0, 30);
            this.m_tabControlDataPanels.Name = "m_tabControlDataPanels";
            this.m_tabControlDataPanels.SelectedIndex = 0;
            this.m_tabControlDataPanels.Size = new System.Drawing.Size(635, 262);
            this.m_tabControlDataPanels.TabIndex = 3;
            // 
            // m_MainTabControl
            //
            this.m_MainTabControl.Controls.Add(m_TabPageScenario); 
            this.m_MainTabControl.Controls.Add(m_TabPageVData);
            this.m_MainTabControl.Dock = System.Windows.Forms.DockStyle.Fill; 
            //this.m_MainTabControl.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left |
            //                               System.Windows.Forms.AnchorStyles.Right | System.Windows.Forms.AnchorStyles.Bottom);
            this.m_MainTabControl.Location = new System.Drawing.Point(0, 0);
            this.m_MainTabControl.Name = "m_MainTabControl";
            this.m_MainTabControl.SelectedIndex = 0;
            this.m_MainTabControl.Size = new System.Drawing.Size(635, 262);
            this.m_MainTabControl.TabIndex = 0;
            
            // 
            // m_TabPageScenario
            // 
            this.m_TabPageScenario.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_TabPageScenario.Location = new System.Drawing.Point(0, 0);
            this.m_TabPageScenario.Name = "m_TabPageScenario";
            this.m_TabPageScenario.Padding = new System.Windows.Forms.Padding(3);
            this.m_TabPageScenario.TabIndex = 1;
            this.m_TabPageScenario.Text = "Cliche/Scenario";
            this.m_TabPageScenario.UseVisualStyleBackColor = true;

            // 
            // m_TabPageVData
            // 
            this.m_TabPageVData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_TabPageVData.Location = new System.Drawing.Point(0, 0);
            this.m_TabPageVData.Name = "m_TabPageVData";
            this.m_TabPageVData.Padding = new System.Windows.Forms.Padding(3);
            this.m_TabPageVData.TabIndex = 2;
            this.m_TabPageVData.Text = "Virtual Datas";
            this.m_TabPageVData.UseVisualStyleBackColor = true;
            
            // 
            // m_BtnSaveCliche
            // 
            this.m_BtnSaveCliche.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left);
            this.m_BtnSaveCliche.Location = new System.Drawing.Point(120, 4);
            this.m_BtnSaveCliche.Name = "m_BtnSaveCliche";
            this.m_BtnSaveCliche.Size = new System.Drawing.Size(100, 22);
            this.m_BtnSaveCliche.Text = "Save Cliche";
            this.m_BtnSaveCliche.TabIndex = 3;
            this.m_BtnSaveCliche.Click += new System.EventHandler(this.SaveDataCliche);
            // 
            // m_BtnLoadCliche
            // 
            this.m_BtnLoadCliche.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left);
            this.m_BtnLoadCliche.Location = new System.Drawing.Point(10, 4);
            this.m_BtnLoadCliche.Name = "m_BtnLoadCliche";
            this.m_BtnLoadCliche.Size = new System.Drawing.Size(100, 22);
            this.m_BtnLoadCliche.Text = "Load Cliche";
            this.m_BtnLoadCliche.TabIndex = 4;
            this.m_BtnLoadCliche.Click += new System.EventHandler(this.LoadDataCliche);
            // 
            // VirtualDataForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(636, 300);
            this.ControlBox = false;
            this.m_TabPageVData.Controls.Add(this.m_tabControlDataPanels);
            this.m_TabPageVData.Controls.Add(this.m_BtnSaveCliche);
            this.m_TabPageVData.Controls.Add(this.m_BtnLoadCliche);
            this.m_TabPageScenario.Controls.Add(this.m_PanelScenario);
            this.Controls.Add(m_MainTabControl);
            this.MinimumSize = new System.Drawing.Size(636, 300);
            this.Name = "VirtualDataForm";
            this.Text = "Virtual Data and Scenario";
            this.Load += new System.EventHandler(this.VirtualDataForm_Load);
            this.m_PanelScenario.ResumeLayout(false);
            this.m_MainTabControl.ResumeLayout(false);
            this.m_TabPageVData.ResumeLayout(false);
            this.m_TabPageScenario.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.TabControl m_MainTabControl;
        private System.Windows.Forms.TabControl m_tabControlDataPanels;
        private System.Windows.Forms.Button m_BtnSaveCliche;
        private System.Windows.Forms.Button m_BtnLoadCliche;
        private SmartApp.ScenarioPanel m_PanelScenario;
        private System.Windows.Forms.TabPage m_TabPageScenario;
        private System.Windows.Forms.TabPage m_TabPageVData;
    }
}
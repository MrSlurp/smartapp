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
            this.m_TabPageScenario = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.m_NumPeriod = new System.Windows.Forms.NumericUpDown();
            this.m_checkPlayLoop = new System.Windows.Forms.CheckBox();
            this.m_btnLoadPrev = new System.Windows.Forms.Button();
            this.m_btnStartStopPlay = new System.Windows.Forms.Button();
            this.m_btnLoadNext = new System.Windows.Forms.Button();
            this.m_btnSaveScen = new System.Windows.Forms.Button();
            this.m_btnLoadScen = new System.Windows.Forms.Button();
            this.m_btnRemoveCliche = new System.Windows.Forms.Button();
            this.m_btnDown = new System.Windows.Forms.Button();
            this.m_btnUp = new System.Windows.Forms.Button();
            this.m_btnAddCliche = new System.Windows.Forms.Button();
            this.m_PanelScenario = new SmartApp.ScenarioPanel();
            this.m_TabPageVData = new System.Windows.Forms.TabPage();
            this.m_tabControlDataPanels = new System.Windows.Forms.TabControl();
            this.m_BtnSaveCliche = new System.Windows.Forms.Button();
            this.m_BtnLoadCliche = new System.Windows.Forms.Button();
            this.m_MainTabControl.SuspendLayout();
            this.m_TabPageScenario.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumPeriod)).BeginInit();
            this.m_TabPageVData.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_MainTabControl
            // 
            this.m_MainTabControl.Controls.Add(this.m_TabPageVData);
            this.m_MainTabControl.Controls.Add(this.m_TabPageScenario);
            this.m_MainTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_MainTabControl.Location = new System.Drawing.Point(0, 0);
            this.m_MainTabControl.Name = "m_MainTabControl";
            this.m_MainTabControl.SelectedIndex = 0;
            this.m_MainTabControl.Size = new System.Drawing.Size(628, 293);
            this.m_MainTabControl.TabIndex = 0;
            // 
            // m_TabPageScenario
            // 
            this.m_TabPageScenario.Controls.Add(this.label1);
            this.m_TabPageScenario.Controls.Add(this.m_NumPeriod);
            this.m_TabPageScenario.Controls.Add(this.m_checkPlayLoop);
            this.m_TabPageScenario.Controls.Add(this.m_btnLoadPrev);
            this.m_TabPageScenario.Controls.Add(this.m_btnStartStopPlay);
            this.m_TabPageScenario.Controls.Add(this.m_btnLoadNext);
            this.m_TabPageScenario.Controls.Add(this.m_btnSaveScen);
            this.m_TabPageScenario.Controls.Add(this.m_btnLoadScen);
            this.m_TabPageScenario.Controls.Add(this.m_btnRemoveCliche);
            this.m_TabPageScenario.Controls.Add(this.m_btnDown);
            this.m_TabPageScenario.Controls.Add(this.m_btnUp);
            this.m_TabPageScenario.Controls.Add(this.m_btnAddCliche);
            this.m_TabPageScenario.Controls.Add(this.m_PanelScenario);
            this.m_TabPageScenario.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_TabPageScenario.Location = new System.Drawing.Point(4, 22);
            this.m_TabPageScenario.Name = "m_TabPageScenario";
            this.m_TabPageScenario.Padding = new System.Windows.Forms.Padding(3);
            this.m_TabPageScenario.Size = new System.Drawing.Size(620, 267);
            this.m_TabPageScenario.TabIndex = 2;
            this.m_TabPageScenario.Text = "Cliche/Scenario";
            this.m_TabPageScenario.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(499, 210);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(115, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Wait between steps (s)";
            // 
            // m_NumPeriod
            // 
            this.m_NumPeriod.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_NumPeriod.Location = new System.Drawing.Point(424, 205);
            this.m_NumPeriod.Maximum = new decimal(new int[] {
            600,
            0,
            0,
            0});
            this.m_NumPeriod.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.m_NumPeriod.Name = "m_NumPeriod";
            this.m_NumPeriod.Size = new System.Drawing.Size(69, 20);
            this.m_NumPeriod.TabIndex = 3;
            this.m_NumPeriod.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.m_NumPeriod.ValueChanged += new System.EventHandler(this.m_NumPeriod_ValueChanged);
            // 
            // m_checkPlayLoop
            // 
            this.m_checkPlayLoop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_checkPlayLoop.AutoSize = true;
            this.m_checkPlayLoop.Location = new System.Drawing.Point(424, 238);
            this.m_checkPlayLoop.Name = "m_checkPlayLoop";
            this.m_checkPlayLoop.Size = new System.Drawing.Size(69, 17);
            this.m_checkPlayLoop.TabIndex = 2;
            this.m_checkPlayLoop.Text = "Play loop";
            this.m_checkPlayLoop.UseVisualStyleBackColor = true;
            // 
            // m_btnLoadPrev
            // 
            this.m_btnLoadPrev.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.m_btnLoadPrev.Location = new System.Drawing.Point(110, 205);
            this.m_btnLoadPrev.Name = "m_btnLoadPrev";
            this.m_btnLoadPrev.Size = new System.Drawing.Size(114, 23);
            this.m_btnLoadPrev.TabIndex = 1;
            this.m_btnLoadPrev.Text = "Load previous cliche";
            this.m_btnLoadPrev.UseVisualStyleBackColor = true;
            this.m_btnLoadPrev.Click += new System.EventHandler(this.m_btnLoadPrev_Click);
            // 
            // m_btnStartStopPlay
            // 
            this.m_btnStartStopPlay.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnStartStopPlay.Location = new System.Drawing.Point(502, 234);
            this.m_btnStartStopPlay.Name = "m_btnStartStopPlay";
            this.m_btnStartStopPlay.Size = new System.Drawing.Size(96, 23);
            this.m_btnStartStopPlay.TabIndex = 1;
            this.m_btnStartStopPlay.Text = "Play scenario";
            this.m_btnStartStopPlay.UseVisualStyleBackColor = true;
            this.m_btnStartStopPlay.Click += new System.EventHandler(this.m_btnStartStopPlay_Click);
            // 
            // m_btnLoadNext
            // 
            this.m_btnLoadNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.m_btnLoadNext.Location = new System.Drawing.Point(110, 234);
            this.m_btnLoadNext.Name = "m_btnLoadNext";
            this.m_btnLoadNext.Size = new System.Drawing.Size(114, 23);
            this.m_btnLoadNext.TabIndex = 1;
            this.m_btnLoadNext.Text = "Load next cliche";
            this.m_btnLoadNext.UseVisualStyleBackColor = true;
            this.m_btnLoadNext.Click += new System.EventHandler(this.m_btnLoadNext_Click);
            // 
            // m_btnSaveScen
            // 
            this.m_btnSaveScen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnSaveScen.Location = new System.Drawing.Point(322, 234);
            this.m_btnSaveScen.Name = "m_btnSaveScen";
            this.m_btnSaveScen.Size = new System.Drawing.Size(96, 23);
            this.m_btnSaveScen.TabIndex = 1;
            this.m_btnSaveScen.Text = "Save scenario";
            this.m_btnSaveScen.UseVisualStyleBackColor = true;
            this.m_btnSaveScen.Click += new System.EventHandler(this.m_btnSaveScen_Click);
            // 
            // m_btnLoadScen
            // 
            this.m_btnLoadScen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnLoadScen.Location = new System.Drawing.Point(322, 205);
            this.m_btnLoadScen.Name = "m_btnLoadScen";
            this.m_btnLoadScen.Size = new System.Drawing.Size(96, 23);
            this.m_btnLoadScen.TabIndex = 1;
            this.m_btnLoadScen.Text = "Load scenario";
            this.m_btnLoadScen.UseVisualStyleBackColor = true;
            this.m_btnLoadScen.Click += new System.EventHandler(this.m_btnLoadScen_Click);
            // 
            // m_btnRemoveCliche
            // 
            this.m_btnRemoveCliche.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.m_btnRemoveCliche.Location = new System.Drawing.Point(8, 234);
            this.m_btnRemoveCliche.Name = "m_btnRemoveCliche";
            this.m_btnRemoveCliche.Size = new System.Drawing.Size(96, 23);
            this.m_btnRemoveCliche.TabIndex = 1;
            this.m_btnRemoveCliche.Text = "Remove cliche file";
            this.m_btnRemoveCliche.UseVisualStyleBackColor = true;
            this.m_btnRemoveCliche.Click += new System.EventHandler(this.m_btnRemoveCliche_Click);
            // 
            // m_btnDown
            // 
            this.m_btnDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.m_btnDown.Location = new System.Drawing.Point(230, 234);
            this.m_btnDown.Name = "m_btnDown";
            this.m_btnDown.Size = new System.Drawing.Size(86, 23);
            this.m_btnDown.TabIndex = 1;
            this.m_btnDown.Text = "Move Down";
            this.m_btnDown.UseVisualStyleBackColor = true;
            this.m_btnDown.Click += new System.EventHandler(this.m_btnDown_Click);
            // 
            // m_btnUp
            // 
            this.m_btnUp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.m_btnUp.Location = new System.Drawing.Point(230, 205);
            this.m_btnUp.Name = "m_btnUp";
            this.m_btnUp.Size = new System.Drawing.Size(86, 23);
            this.m_btnUp.TabIndex = 1;
            this.m_btnUp.Text = "Move Up";
            this.m_btnUp.UseVisualStyleBackColor = true;
            this.m_btnUp.Click += new System.EventHandler(this.m_btnUp_Click);
            // 
            // m_btnAddCliche
            // 
            this.m_btnAddCliche.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.m_btnAddCliche.Location = new System.Drawing.Point(8, 205);
            this.m_btnAddCliche.Name = "m_btnAddCliche";
            this.m_btnAddCliche.Size = new System.Drawing.Size(96, 23);
            this.m_btnAddCliche.TabIndex = 1;
            this.m_btnAddCliche.Text = "Add cliche file";
            this.m_btnAddCliche.UseVisualStyleBackColor = true;
            this.m_btnAddCliche.Click += new System.EventHandler(this.m_btnAddCliche_Click);
            // 
            // m_PanelScenario
            // 
            this.m_PanelScenario.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_PanelScenario.Location = new System.Drawing.Point(0, 3);
            this.m_PanelScenario.Name = "m_PanelScenario";
            this.m_PanelScenario.Size = new System.Drawing.Size(620, 196);
            this.m_PanelScenario.TabIndex = 0;
            this.m_PanelScenario.SelectionChange += new System.EventHandler(this.UpdateBtnState);
            // 
            // m_TabPageVData
            // 
            this.m_TabPageVData.Controls.Add(this.m_tabControlDataPanels);
            this.m_TabPageVData.Controls.Add(this.m_BtnSaveCliche);
            this.m_TabPageVData.Controls.Add(this.m_BtnLoadCliche);
            this.m_TabPageVData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_TabPageVData.Location = new System.Drawing.Point(4, 22);
            this.m_TabPageVData.Name = "m_TabPageVData";
            this.m_TabPageVData.Padding = new System.Windows.Forms.Padding(3);
            this.m_TabPageVData.Size = new System.Drawing.Size(620, 267);
            this.m_TabPageVData.TabIndex = 1;
            this.m_TabPageVData.Text = "Virtual Datas";
            this.m_TabPageVData.UseVisualStyleBackColor = true;
            // 
            // m_tabControlDataPanels
            // 
            this.m_tabControlDataPanels.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_tabControlDataPanels.Location = new System.Drawing.Point(0, 0);
            this.m_tabControlDataPanels.Name = "m_tabControlDataPanels";
            this.m_tabControlDataPanels.SelectedIndex = 0;
            this.m_tabControlDataPanels.Size = new System.Drawing.Size(628, 238);
            this.m_tabControlDataPanels.TabIndex = 3;
            // 
            // m_BtnSaveCliche
            // 
            this.m_BtnSaveCliche.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.m_BtnSaveCliche.Location = new System.Drawing.Point(114, 244);
            this.m_BtnSaveCliche.Name = "m_BtnSaveCliche";
            this.m_BtnSaveCliche.Size = new System.Drawing.Size(100, 22);
            this.m_BtnSaveCliche.TabIndex = 3;
            this.m_BtnSaveCliche.Text = "Save Cliche";
            this.m_BtnSaveCliche.Click += new System.EventHandler(this.SaveDataCliche_click);
            // 
            // m_BtnLoadCliche
            // 
            this.m_BtnLoadCliche.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.m_BtnLoadCliche.Location = new System.Drawing.Point(8, 244);
            this.m_BtnLoadCliche.Name = "m_BtnLoadCliche";
            this.m_BtnLoadCliche.Size = new System.Drawing.Size(100, 22);
            this.m_BtnLoadCliche.TabIndex = 4;
            this.m_BtnLoadCliche.Text = "Load Cliche";
            this.m_BtnLoadCliche.Click += new System.EventHandler(this.LoadDataCliche_click);
            // 
            // VirtualDataForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(628, 293);
            this.ControlBox = false;
            this.Controls.Add(this.m_MainTabControl);
            this.MinimumSize = new System.Drawing.Size(636, 300);
            this.Name = "VirtualDataForm";
            this.Text = "Virtual Data and Scenario";
            this.Load += new System.EventHandler(this.VirtualDataForm_Load);
            this.m_MainTabControl.ResumeLayout(false);
            this.m_TabPageScenario.ResumeLayout(false);
            this.m_TabPageScenario.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumPeriod)).EndInit();
            this.m_TabPageVData.ResumeLayout(false);
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
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown m_NumPeriod;
        private System.Windows.Forms.CheckBox m_checkPlayLoop;
        private System.Windows.Forms.Button m_btnLoadPrev;
        private System.Windows.Forms.Button m_btnLoadNext;
        private System.Windows.Forms.Button m_btnSaveScen;
        private System.Windows.Forms.Button m_btnLoadScen;
        private System.Windows.Forms.Button m_btnRemoveCliche;
        private System.Windows.Forms.Button m_btnAddCliche;
        private System.Windows.Forms.Button m_btnStartStopPlay;
        private System.Windows.Forms.Button m_btnDown;
        private System.Windows.Forms.Button m_btnUp;
    }
}
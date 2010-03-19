namespace SmartApp
{
    partial class LogCatForm
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
            this.m_btnOK = new System.Windows.Forms.Button();
            this.m_btnCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.m_cboLevel = new System.Windows.Forms.ComboBox();
            this.m_grAllCat = new System.Windows.Forms.GroupBox();
            this.m_grExecCat = new System.Windows.Forms.GroupBox();
            this.m_chkCatExecLogger = new System.Windows.Forms.CheckBox();
            this.m_chkCatExecTimer = new System.Windows.Forms.CheckBox();
            this.m_chkCatExecScreen = new System.Windows.Forms.CheckBox();
            this.m_chkCatExecFunc = new System.Windows.Forms.CheckBox();
            this.m_chkCatExecLogic = new System.Windows.Forms.CheckBox();
            this.m_chkCatExecMaths = new System.Windows.Forms.CheckBox();
            this.m_chkCatExecFrame = new System.Windows.Forms.CheckBox();
            this.m_chkCatDocument = new System.Windows.Forms.CheckBox();
            this.m_chkCatOther = new System.Windows.Forms.CheckBox();
            this.m_chkCatComm = new System.Windows.Forms.CheckBox();
            this.m_chkCatPlugin = new System.Windows.Forms.CheckBox();
            this.m_chkCatSerialize = new System.Windows.Forms.CheckBox();
            this.m_chkCatCommonLib = new System.Windows.Forms.CheckBox();
            this.m_chkCatSmartCmd = new System.Windows.Forms.CheckBox();
            this.m_chkCatSmartCfg = new System.Windows.Forms.CheckBox();
            this.m_chkCatLang = new System.Windows.Forms.CheckBox();
            this.m_chkCatExec = new System.Windows.Forms.CheckBox();
            this.m_chkCatParser = new System.Windows.Forms.CheckBox();
            this.m_chkCatScriptEditor = new System.Windows.Forms.CheckBox();
            this.m_chkCatPerf = new System.Windows.Forms.CheckBox();
            this.m_chkLogToFile = new System.Windows.Forms.CheckBox();
            this.m_grAllCat.SuspendLayout();
            this.m_grExecCat.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_btnOK
            // 
            this.m_btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.m_btnOK.Location = new System.Drawing.Point(172, 361);
            this.m_btnOK.Name = "m_btnOK";
            this.m_btnOK.Size = new System.Drawing.Size(75, 23);
            this.m_btnOK.TabIndex = 0;
            this.m_btnOK.Text = "OK";
            this.m_btnOK.UseVisualStyleBackColor = true;
            // 
            // m_btnCancel
            // 
            this.m_btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_btnCancel.Location = new System.Drawing.Point(253, 361);
            this.m_btnCancel.Name = "m_btnCancel";
            this.m_btnCancel.Size = new System.Drawing.Size(75, 23);
            this.m_btnCancel.TabIndex = 1;
            this.m_btnCancel.Text = "Cancel";
            this.m_btnCancel.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Traces level";
            // 
            // m_cboLevel
            // 
            this.m_cboLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cboLevel.FormattingEnabled = true;
            this.m_cboLevel.Location = new System.Drawing.Point(15, 26);
            this.m_cboLevel.Name = "m_cboLevel";
            this.m_cboLevel.Size = new System.Drawing.Size(121, 21);
            this.m_cboLevel.TabIndex = 4;
            this.m_cboLevel.SelectedIndexChanged += new System.EventHandler(this.m_cboLevel_SelectedIndexChanged);
            // 
            // m_grAllCat
            // 
            this.m_grAllCat.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_grAllCat.Controls.Add(this.m_grExecCat);
            this.m_grAllCat.Controls.Add(this.m_chkCatPerf);
            this.m_grAllCat.Controls.Add(this.m_chkCatScriptEditor);
            this.m_grAllCat.Controls.Add(this.m_chkCatDocument);
            this.m_grAllCat.Controls.Add(this.m_chkCatOther);
            this.m_grAllCat.Controls.Add(this.m_chkCatComm);
            this.m_grAllCat.Controls.Add(this.m_chkCatPlugin);
            this.m_grAllCat.Controls.Add(this.m_chkCatSerialize);
            this.m_grAllCat.Controls.Add(this.m_chkCatCommonLib);
            this.m_grAllCat.Controls.Add(this.m_chkCatSmartCmd);
            this.m_grAllCat.Controls.Add(this.m_chkCatSmartCfg);
            this.m_grAllCat.Controls.Add(this.m_chkCatLang);
            this.m_grAllCat.Controls.Add(this.m_chkCatExec);
            this.m_grAllCat.Controls.Add(this.m_chkCatParser);
            this.m_grAllCat.Location = new System.Drawing.Point(15, 53);
            this.m_grAllCat.Name = "m_grAllCat";
            this.m_grAllCat.Size = new System.Drawing.Size(311, 302);
            this.m_grAllCat.TabIndex = 5;
            this.m_grAllCat.TabStop = false;
            this.m_grAllCat.Text = "Traces Categories";
            // 
            // m_grExecCat
            // 
            this.m_grExecCat.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_grExecCat.Controls.Add(this.m_chkCatExecLogger);
            this.m_grExecCat.Controls.Add(this.m_chkCatExecTimer);
            this.m_grExecCat.Controls.Add(this.m_chkCatExecScreen);
            this.m_grExecCat.Controls.Add(this.m_chkCatExecFunc);
            this.m_grExecCat.Controls.Add(this.m_chkCatExecLogic);
            this.m_grExecCat.Controls.Add(this.m_chkCatExecMaths);
            this.m_grExecCat.Controls.Add(this.m_chkCatExecFrame);
            this.m_grExecCat.Location = new System.Drawing.Point(148, 42);
            this.m_grExecCat.Name = "m_grExecCat";
            this.m_grExecCat.Size = new System.Drawing.Size(153, 254);
            this.m_grExecCat.TabIndex = 3;
            this.m_grExecCat.TabStop = false;
            this.m_grExecCat.Text = "Executer Sub Categories";
            // 
            // m_chkCatExecLogger
            // 
            this.m_chkCatExecLogger.AutoSize = true;
            this.m_chkCatExecLogger.Location = new System.Drawing.Point(12, 157);
            this.m_chkCatExecLogger.Name = "m_chkCatExecLogger";
            this.m_chkCatExecLogger.Size = new System.Drawing.Size(98, 17);
            this.m_chkCatExecLogger.TabIndex = 2;
            this.m_chkCatExecLogger.Text = "ExecuteLogger";
            this.m_chkCatExecLogger.UseVisualStyleBackColor = true;
            // 
            // m_chkCatExecTimer
            // 
            this.m_chkCatExecTimer.AutoSize = true;
            this.m_chkCatExecTimer.Location = new System.Drawing.Point(12, 134);
            this.m_chkCatExecTimer.Name = "m_chkCatExecTimer";
            this.m_chkCatExecTimer.Size = new System.Drawing.Size(91, 17);
            this.m_chkCatExecTimer.TabIndex = 2;
            this.m_chkCatExecTimer.Text = "ExecuteTimer";
            this.m_chkCatExecTimer.UseVisualStyleBackColor = true;
            // 
            // m_chkCatExecScreen
            // 
            this.m_chkCatExecScreen.AutoSize = true;
            this.m_chkCatExecScreen.Location = new System.Drawing.Point(12, 111);
            this.m_chkCatExecScreen.Name = "m_chkCatExecScreen";
            this.m_chkCatExecScreen.Size = new System.Drawing.Size(99, 17);
            this.m_chkCatExecScreen.TabIndex = 2;
            this.m_chkCatExecScreen.Text = "ExecuteScreen";
            this.m_chkCatExecScreen.UseVisualStyleBackColor = true;
            // 
            // m_chkCatExecFunc
            // 
            this.m_chkCatExecFunc.AutoSize = true;
            this.m_chkCatExecFunc.Location = new System.Drawing.Point(12, 88);
            this.m_chkCatExecFunc.Name = "m_chkCatExecFunc";
            this.m_chkCatExecFunc.Size = new System.Drawing.Size(89, 17);
            this.m_chkCatExecFunc.TabIndex = 2;
            this.m_chkCatExecFunc.Text = "ExecuteFunc";
            this.m_chkCatExecFunc.UseVisualStyleBackColor = true;
            // 
            // m_chkCatExecLogic
            // 
            this.m_chkCatExecLogic.AutoSize = true;
            this.m_chkCatExecLogic.Location = new System.Drawing.Point(12, 65);
            this.m_chkCatExecLogic.Name = "m_chkCatExecLogic";
            this.m_chkCatExecLogic.Size = new System.Drawing.Size(91, 17);
            this.m_chkCatExecLogic.TabIndex = 2;
            this.m_chkCatExecLogic.Text = "ExecuteLogic";
            this.m_chkCatExecLogic.UseVisualStyleBackColor = true;
            // 
            // m_chkCatExecMaths
            // 
            this.m_chkCatExecMaths.AutoSize = true;
            this.m_chkCatExecMaths.Location = new System.Drawing.Point(12, 42);
            this.m_chkCatExecMaths.Name = "m_chkCatExecMaths";
            this.m_chkCatExecMaths.Size = new System.Drawing.Size(89, 17);
            this.m_chkCatExecMaths.TabIndex = 2;
            this.m_chkCatExecMaths.Text = "ExecuteMath";
            this.m_chkCatExecMaths.UseVisualStyleBackColor = true;
            // 
            // m_chkCatExecFrame
            // 
            this.m_chkCatExecFrame.AutoSize = true;
            this.m_chkCatExecFrame.Location = new System.Drawing.Point(12, 19);
            this.m_chkCatExecFrame.Name = "m_chkCatExecFrame";
            this.m_chkCatExecFrame.Size = new System.Drawing.Size(94, 17);
            this.m_chkCatExecFrame.TabIndex = 2;
            this.m_chkCatExecFrame.Text = "ExecuteFrame";
            this.m_chkCatExecFrame.UseVisualStyleBackColor = true;
            // 
            // m_chkCatDocument
            // 
            this.m_chkCatDocument.AutoSize = true;
            this.m_chkCatDocument.Location = new System.Drawing.Point(6, 226);
            this.m_chkCatDocument.Name = "m_chkCatDocument";
            this.m_chkCatDocument.Size = new System.Drawing.Size(75, 17);
            this.m_chkCatDocument.TabIndex = 2;
            this.m_chkCatDocument.Text = "Document";
            this.m_chkCatDocument.UseVisualStyleBackColor = true;
            // 
            // m_chkCatOther
            // 
            this.m_chkCatOther.AutoSize = true;
            this.m_chkCatOther.Location = new System.Drawing.Point(6, 203);
            this.m_chkCatOther.Name = "m_chkCatOther";
            this.m_chkCatOther.Size = new System.Drawing.Size(57, 17);
            this.m_chkCatOther.TabIndex = 2;
            this.m_chkCatOther.Text = "Others";
            this.m_chkCatOther.UseVisualStyleBackColor = true;
            // 
            // m_chkCatComm
            // 
            this.m_chkCatComm.AutoSize = true;
            this.m_chkCatComm.Location = new System.Drawing.Point(6, 180);
            this.m_chkCatComm.Name = "m_chkCatComm";
            this.m_chkCatComm.Size = new System.Drawing.Size(98, 17);
            this.m_chkCatComm.TabIndex = 2;
            this.m_chkCatComm.Text = "Communication";
            this.m_chkCatComm.UseVisualStyleBackColor = true;
            // 
            // m_chkCatPlugin
            // 
            this.m_chkCatPlugin.AutoSize = true;
            this.m_chkCatPlugin.Location = new System.Drawing.Point(6, 157);
            this.m_chkCatPlugin.Name = "m_chkCatPlugin";
            this.m_chkCatPlugin.Size = new System.Drawing.Size(55, 17);
            this.m_chkCatPlugin.TabIndex = 2;
            this.m_chkCatPlugin.Text = "Plugin";
            this.m_chkCatPlugin.UseVisualStyleBackColor = true;
            // 
            // m_chkCatSerialize
            // 
            this.m_chkCatSerialize.AutoSize = true;
            this.m_chkCatSerialize.Location = new System.Drawing.Point(6, 134);
            this.m_chkCatSerialize.Name = "m_chkCatSerialize";
            this.m_chkCatSerialize.Size = new System.Drawing.Size(82, 17);
            this.m_chkCatSerialize.TabIndex = 2;
            this.m_chkCatSerialize.Text = "Serialization";
            this.m_chkCatSerialize.UseVisualStyleBackColor = true;
            // 
            // m_chkCatCommonLib
            // 
            this.m_chkCatCommonLib.AutoSize = true;
            this.m_chkCatCommonLib.Location = new System.Drawing.Point(6, 111);
            this.m_chkCatCommonLib.Name = "m_chkCatCommonLib";
            this.m_chkCatCommonLib.Size = new System.Drawing.Size(81, 17);
            this.m_chkCatCommonLib.TabIndex = 2;
            this.m_chkCatCommonLib.Text = "CommonLib";
            this.m_chkCatCommonLib.UseVisualStyleBackColor = true;
            // 
            // m_chkCatSmartCmd
            // 
            this.m_chkCatSmartCmd.AutoSize = true;
            this.m_chkCatSmartCmd.Location = new System.Drawing.Point(6, 88);
            this.m_chkCatSmartCmd.Name = "m_chkCatSmartCmd";
            this.m_chkCatSmartCmd.Size = new System.Drawing.Size(100, 17);
            this.m_chkCatSmartCmd.TabIndex = 2;
            this.m_chkCatSmartCmd.Text = "SmartCommand";
            this.m_chkCatSmartCmd.UseVisualStyleBackColor = true;
            // 
            // m_chkCatSmartCfg
            // 
            this.m_chkCatSmartCfg.AutoSize = true;
            this.m_chkCatSmartCfg.Location = new System.Drawing.Point(6, 65);
            this.m_chkCatSmartCfg.Name = "m_chkCatSmartCfg";
            this.m_chkCatSmartCfg.Size = new System.Drawing.Size(83, 17);
            this.m_chkCatSmartCfg.TabIndex = 2;
            this.m_chkCatSmartCfg.Text = "SmartConfig";
            this.m_chkCatSmartCfg.UseVisualStyleBackColor = true;
            // 
            // m_chkCatLang
            // 
            this.m_chkCatLang.AutoSize = true;
            this.m_chkCatLang.Location = new System.Drawing.Point(6, 42);
            this.m_chkCatLang.Name = "m_chkCatLang";
            this.m_chkCatLang.Size = new System.Drawing.Size(50, 17);
            this.m_chkCatLang.TabIndex = 2;
            this.m_chkCatLang.Text = "Lang";
            this.m_chkCatLang.UseVisualStyleBackColor = true;
            // 
            // m_chkCatExec
            // 
            this.m_chkCatExec.AutoSize = true;
            this.m_chkCatExec.Location = new System.Drawing.Point(148, 19);
            this.m_chkCatExec.Name = "m_chkCatExec";
            this.m_chkCatExec.Size = new System.Drawing.Size(68, 17);
            this.m_chkCatExec.TabIndex = 2;
            this.m_chkCatExec.Text = "Executer";
            this.m_chkCatExec.UseVisualStyleBackColor = true;
            this.m_chkCatExec.CheckedChanged += new System.EventHandler(this.m_chkCatExec_CheckedChanged);
            // 
            // m_chkCatParser
            // 
            this.m_chkCatParser.AutoSize = true;
            this.m_chkCatParser.Location = new System.Drawing.Point(6, 19);
            this.m_chkCatParser.Name = "m_chkCatParser";
            this.m_chkCatParser.Size = new System.Drawing.Size(56, 17);
            this.m_chkCatParser.TabIndex = 2;
            this.m_chkCatParser.Text = "Parser";
            this.m_chkCatParser.UseVisualStyleBackColor = true;
            // 
            // m_chkCatScriptEditor
            // 
            this.m_chkCatScriptEditor.AutoSize = true;
            this.m_chkCatScriptEditor.Location = new System.Drawing.Point(6, 249);
            this.m_chkCatScriptEditor.Name = "m_chkCatScriptEditor";
            this.m_chkCatScriptEditor.Size = new System.Drawing.Size(80, 17);
            this.m_chkCatScriptEditor.TabIndex = 2;
            this.m_chkCatScriptEditor.Text = "ScriptEditor";
            this.m_chkCatScriptEditor.UseVisualStyleBackColor = true;
            // 
            // m_chkCatPerf
            // 
            this.m_chkCatPerf.AutoSize = true;
            this.m_chkCatPerf.Location = new System.Drawing.Point(6, 272);
            this.m_chkCatPerf.Name = "m_chkCatPerf";
            this.m_chkCatPerf.Size = new System.Drawing.Size(79, 17);
            this.m_chkCatPerf.TabIndex = 2;
            this.m_chkCatPerf.Text = "PerfChrono";
            this.m_chkCatPerf.UseVisualStyleBackColor = true;
            // 
            // m_chkLogToFile
            // 
            this.m_chkLogToFile.AutoSize = true;
            this.m_chkLogToFile.Location = new System.Drawing.Point(163, 26);
            this.m_chkLogToFile.Name = "m_chkLogToFile";
            this.m_chkLogToFile.Size = new System.Drawing.Size(76, 17);
            this.m_chkLogToFile.TabIndex = 2;
            this.m_chkLogToFile.Text = "Log To file";
            this.m_chkLogToFile.UseVisualStyleBackColor = true;
            // 
            // LogCatForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(337, 396);
            this.Controls.Add(this.m_grAllCat);
            this.Controls.Add(this.m_chkLogToFile);
            this.Controls.Add(this.m_cboLevel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.m_btnCancel);
            this.Controls.Add(this.m_btnOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "LogCatForm";
            this.Text = "Traces configuration";
            this.m_grAllCat.ResumeLayout(false);
            this.m_grAllCat.PerformLayout();
            this.m_grExecCat.ResumeLayout(false);
            this.m_grExecCat.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button m_btnOK;
        private System.Windows.Forms.Button m_btnCancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox m_cboLevel;
        private System.Windows.Forms.GroupBox m_grAllCat;
        private System.Windows.Forms.CheckBox m_chkCatSerialize;
        private System.Windows.Forms.CheckBox m_chkCatCommonLib;
        private System.Windows.Forms.CheckBox m_chkCatSmartCmd;
        private System.Windows.Forms.CheckBox m_chkCatSmartCfg;
        private System.Windows.Forms.CheckBox m_chkCatLang;
        private System.Windows.Forms.CheckBox m_chkCatExec;
        private System.Windows.Forms.CheckBox m_chkCatParser;
        private System.Windows.Forms.CheckBox m_chkCatPlugin;
        private System.Windows.Forms.CheckBox m_chkCatComm;
        private System.Windows.Forms.GroupBox m_grExecCat;
        private System.Windows.Forms.CheckBox m_chkCatOther;
        private System.Windows.Forms.CheckBox m_chkCatExecScreen;
        private System.Windows.Forms.CheckBox m_chkCatExecFunc;
        private System.Windows.Forms.CheckBox m_chkCatExecLogic;
        private System.Windows.Forms.CheckBox m_chkCatExecMaths;
        private System.Windows.Forms.CheckBox m_chkCatExecFrame;
        private System.Windows.Forms.CheckBox m_chkCatExecLogger;
        private System.Windows.Forms.CheckBox m_chkCatExecTimer;
        private System.Windows.Forms.CheckBox m_chkCatDocument;
        private System.Windows.Forms.CheckBox m_chkCatPerf;
        private System.Windows.Forms.CheckBox m_chkCatScriptEditor;
        private System.Windows.Forms.CheckBox m_chkLogToFile;
    }
}
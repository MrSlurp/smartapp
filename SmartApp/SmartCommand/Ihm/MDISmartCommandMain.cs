using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CommonLib;

using System.IO;
using System.Reflection;

namespace SmartApp
{
    public partial class MDISmartCommandMain : Form
    {
        private const string APP_TITLE = "SmartCommand - Monitor";

        #region données membres
        protected TraceConsole m_TraceConsole;
        // fenêtre de log des évènements
        private static AppEventLogPanel m_EventLogPanel;
        // fichier d'ini des options
        private AppOptions m_Option = new AppOptions();
        // chemin du dossier de log utilisateurs
        private string m_strLogFilePath;
        // stocke temporairement le nom de fichier passé par la ligne de commande
        private string m_strAutoOpenFileName = "";
        //
        protected MruStripMenuInline m_mruStripMenu;
        //
        SolutionGest m_GestSolution;
        #endregion

        #region attributs
        /// <summary>
        /// accesseur de la fenêtre event log. Défini comme étant static (un seul event log)
        /// </summary>
        public static AppEventLogPanel EventLogger
        {
            get
            {
                return m_EventLogPanel;
            }
        }
        #endregion

        #region constructeurs et init
        /// <summary>
        /// constructeur par défaut
        /// </summary>
        public MDISmartCommandMain()
        {
            Program.LangSys.Initialize(this);
            InitializeComponent();
            CommonConstructorInit();
        }

        /// <summary>
        /// constructeur avec nom de fichier (ouvre le fichier passé en paramètre)
        /// </summary>
        /// <param name="strFileName"></param>
        public MDISmartCommandMain(string strFileName)
        {
            Program.LangSys.Initialize(this);
            InitializeComponent();
            CommonConstructorInit();
            m_strAutoOpenFileName = strFileName;
        }

        /// <summary>
        /// Initialisations communes aux deux constructeurs
        /// </summary>
        public void CommonConstructorInit()
        {
            m_EventLogPanel = appEventLogPanel;
            this.Text = APP_TITLE;
            this.Icon = CommonLib.Resources.AppIcon;
            string strAppDir = Application.StartupPath;
            string strIniFilePath = PathTranslator.LinuxVsWindowsPathUse(strAppDir + @"\" + Cste.STR_FORMPOSINI_FILENAME);
            m_mruStripMenu = new MruStripMenuInline(this.fileMenu, this.m_MruFiles, new MruStripMenu.ClickedHandler(OnMruFile), strIniFilePath);
            if (LaunchArgParser.DevMode)
            {
                menuItemTraceConfig.Visible = true;
                menuItemOpenDebugConsole.Visible = true;
            }
            CentralizedFileDlg.InitPrjFileDialog(Application.StartupPath);
            dataGridMonitor.CellClick += new DataGridViewCellEventHandler(dataGridMonitor_CellClick);
            InitTrayIcon();
        }

        /// <summary>
        /// initialise l'icone du systray
        /// </summary>
        private void InitTrayIcon()
        {
            m_trayIcon.Text = APP_TITLE;
            m_trayIcon.Icon = CommonLib.Resources.AppIcon;
            m_trayIcon.ContextMenuStrip = new ContextMenuStrip();
            m_trayIcon.ContextMenuStrip.AutoSize = true;
            ToolStripMenuItem tsItemShowMonitor = new ToolStripMenuItem();
            tsItemShowMonitor.Text = Program.LangSys.C("Show monitor");
            tsItemShowMonitor.Checked = true;
            tsItemShowMonitor.Name = "ShowMonTrayMenu";
            tsItemShowMonitor.Click += new EventHandler(trayShowMonitor_Click);
            ToolStripMenuItem tsItemExit = new ToolStripMenuItem();
            tsItemExit.Text = Program.LangSys.C("Quit");
            tsItemExit.Name = "ExitTrayMenu";
            tsItemExit.Click += new EventHandler(trayExit_Click);

            m_trayIcon.ContextMenuStrip.Items.Add(tsItemShowMonitor);
            m_trayIcon.ContextMenuStrip.Items.Add(tsItemExit);
        }
        #endregion

        #region handler d'event du menu du systray
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void trayExit_Click(object sender, EventArgs e)
        {
            ExitSmartCommand();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void trayShowMonitor_Click(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                this.Visible = false;
            }
            else
            {
                this.Visible = true;
            }
            UpdateTrayMenuFromState();
        }

        /// <summary>
        /// 
        /// </summary>
        void UpdateTrayMenuFromState()
        {
            ToolStripMenuItem trayMenuItem = m_trayIcon.ContextMenuStrip.Items["ShowMonTrayMenu"] as ToolStripMenuItem;
            if (this.Visible)
            {
                trayMenuItem.Checked = true;
            }
            else
            {
                trayMenuItem.Checked = false;
            }
        }
        #endregion

        #region handler d'event des document et MAJ de la grille

        /// <summary>
        /// handler d'event du changement de status de la comm d'un document
        /// </summary>
        /// <param name="document"></param>
        void OnDocument_CommStateChange(BTDoc document)
        {
            if (this.InvokeRequired)
            {
                DocComStateChange AsyncCall = new DocComStateChange(AsyncComStateUpdater);
                this.Invoke(AsyncCall, document);
            }
            else
            {
                AsyncComStateUpdater(document);
            }
        }

        /// <summary>
        /// handler d'event du changement de status run d'un document
        /// </summary>
        /// <param name="document"></param>
        void OnDocument_RunStateChange(BTDoc document)
        {
            if (this.InvokeRequired)
            {
                RunStateChangeEvent AsyncCall = new RunStateChangeEvent(AsyncRunStateUpdater);
                this.Invoke(AsyncCall, document);
            }
            else
            {
                AsyncRunStateUpdater(document);
            }
        }

        /// <summary>
        /// Met a jour la grid view avec le status actuel de la com du document
        /// </summary>
        /// <param name="doc"></param>
        protected void AsyncComStateUpdater(BTDoc doc)
        {
            foreach (DataGridViewRow row in dataGridMonitor.Rows)
            {
                if (row.Tag == doc)
                {
                    Image img = Resources.CxnOff;
                    if (doc.Communication.IsOpen)
                        img = Resources.CxnOn;
                    DataGridViewImageCell projectCnxStatuxCell = row.Cells[this.colProjCnxStatus.Name] as DataGridViewImageCell;
                    projectCnxStatuxCell.Value = img;
                    break;
                }
            }
        }

        /// <summary>
        /// Met a jour la grid view avec le status actuel du "run" du document
        /// </summary>
        /// <param name="doc"></param>
        protected void AsyncRunStateUpdater(BTDoc doc)
        {
            foreach (DataGridViewRow row in dataGridMonitor.Rows)
            {
                if (row.Tag == doc)
                {
                    Image img = Resources.CxnOff;
                    if (doc.IsRunning)
                        img = Resources.CxnOn;
                    DataGridViewImageCell projectRunStatuxCell = row.Cells[this.colRunStatus.Name] as DataGridViewImageCell;
                    projectRunStatuxCell.Value = img;
                    break;
                }
            }
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        private void StopActiveDocuments()
        {
            foreach (DataGridViewRow row in dataGridMonitor.Rows)
            {
                if (row.Tag is BTDoc)
                {
                    BTDoc doc = row.Tag as BTDoc;
                    if (doc.IsRunning)
                        doc.TraiteMessage(MESSAGE.MESS_CMD_STOP, null, Program.TypeApp);
                    if (doc.Communication.IsOpen)
                        doc.Communication.CloseComm();
                }
            }
            // petit attente d'une seconde en forçant le traitement
            // des évènement fenêtre
            DateTime enWaitTime = DateTime.Now.AddSeconds(1);
            do
            {
                Application.DoEvents();
                System.Threading.Thread.Sleep(10);
            } while (enWaitTime < DateTime.Now);
        }

        /// <summary>
        /// 
        /// </summary>
        private void ExitSmartCommand()
        {
            StopActiveDocuments();
            SolutionClose();
            this.Close();
        }

        #region Handlers du menu fichier
        /// <summary>
        /// handler du menu "Ouvrir", ou du bouton ouvrir de la toolbar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuItemOpen_Click(object sender, EventArgs e)
        {
            SolutionOpen(null);
            this.BringToFront();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="number"></param>
        /// <param name="filename"></param>
        private void OnMruFile(int number, String filename)
        {
            if (!File.Exists(filename))
            {
                MessageBox.Show(string.Format(Program.LangSys.C("The file '{0}' cannot be opened and will be removed from the recent list"), filename)
                    , Program.LangSys.C("Error")
                    , MessageBoxButtons.OK
                    , MessageBoxIcon.Error);
                m_mruStripMenu.RemoveFile(number);
            }
            else
            {
                SolutionClose();
                SolutionOpen(filename);
                this.BringToFront();
                m_mruStripMenu.SetFirstFile(number);
            }
        }

        /// <summary>
        ///  handler du menu "Exit"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuItemExit_Click(object sender, EventArgs e)
        {
            ExitSmartCommand();
        }

        #endregion

        #region handlers du menu View
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStrip.Visible = toolBarToolStripMenuItem.Checked;
        }
        #endregion

        #region fonction d'ouverture sauvegarde et fermeture de la solutions
        /// <summary>
        /// 
        /// </summary>
        /// <param name="strSolutionPath"></param>
        private void SolutionOpen(string strSolutionPath)
        {
            dataGridMonitor.Rows.Clear();
            if (string.IsNullOrEmpty(strSolutionPath))
            {
                DialogResult dlgRes = CentralizedFileDlg.ShowOpenSolFileDilaog();
                if (dlgRes == DialogResult.OK)
                    strSolutionPath = CentralizedFileDlg.SolOpenFileName;
            }
            if (m_GestSolution != null)
            {
                System.Diagnostics.Debug.Assert(false);
            }
            if (!string.IsNullOrEmpty(strSolutionPath))
            {
                m_GestSolution = new SolutionGest(Program.TypeApp, Program.DllGest);
                string DossierFichier = Path.GetDirectoryName(strSolutionPath);
                CentralizedFileDlg.InitSolFileDialog(DossierFichier);
                m_GestSolution.OnDocOpened += new SolutionGest.DocumentOpenCloseEventHandler(GestSolution_OnDocOpened);
                m_GestSolution.OnDocClosed += new SolutionGest.DocumentOpenCloseEventHandler(GestSolution_OnDocClosed);
                m_GestSolution.ReadInSolution(strSolutionPath);
                
                m_mruStripMenu.AddFile(strSolutionPath);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="document"></param>
        void GestSolution_OnDocOpened(BTDoc doc)
        {
            doc.LogFilePath = m_strLogFilePath;
            if (doc.FinalizeRead(this))
            {
                if (doc.OpenDocumentForCommand())
                {
                    doc.OnCommStateChange += new DocComStateChange(OnDocument_CommStateChange);
                    doc.OnRunStateChange += new RunStateChangeEvent(OnDocument_RunStateChange);
                    doc.EventAddLogEvent += new AddLogEventDelegate(AddLogEvent);
                    AddDocToMonitorList(doc);
                }
                else
                {
                    SolutionClose();
                }
            }
            else
            {
                Traces.LogAddDebug(TraceCat.SmartCommand, "MDICommand", "Erreur lors du FinalizeRead()");
                MessageBox.Show(Program.LangSys.C("Can't initialize run mode datas. Please contact support"),
                                Program.LangSys.C("Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="doc"></param>
        void GestSolution_OnDocClosed(BTDoc doc)
        {
            doc.CloseSupervisionForms();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void dataGridMonitor_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1 || e.ColumnIndex == -1)
                return;

            if (e.ColumnIndex == colBtnConnectStart.Index)
            {
                DataGridViewRow row = dataGridMonitor.Rows[e.RowIndex];
                BTDoc doc = row.Tag as BTDoc;
                if (doc.Communication.IsOpen && doc.IsRunning)
                {
                    doc.TraiteMessage(MESSAGE.MESS_CMD_STOP, null, Program.TypeApp);
                    doc.Communication.CloseComm();
                }
                else
                {
                    if (!doc.Communication.IsOpen)
                    {
                        doc.OpenDocumentComm();
                    }
                    if (doc.Communication.IsOpen && !doc.IsRunning)
                    {
                        doc.TraiteMessage(MESSAGE.MESS_CMD_RUN, null, Program.TypeApp);
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="doc"></param>
        void AddDocToMonitorList(BTDoc doc)
        {
            int indexRow = dataGridMonitor.Rows.Add(1);
            DataGridViewRow docRow = dataGridMonitor.Rows[indexRow];
            docRow.Tag = doc;
            DataGridViewTextBoxCell projectNameCell = docRow.Cells[this.colProjName.Name] as DataGridViewTextBoxCell;
            projectNameCell.Value = Path.GetFileNameWithoutExtension(doc.FileName);
            DataGridViewImageCell projectCnxStatuxCell = docRow.Cells[this.colProjCnxStatus.Name] as DataGridViewImageCell;
            projectCnxStatuxCell.ImageLayout = DataGridViewImageCellLayout.Normal;
            projectCnxStatuxCell.Value = Resources.CxnOff;
            DataGridViewImageCell projectRunStatuxCell = docRow.Cells[this.colRunStatus.Name] as DataGridViewImageCell;
            projectRunStatuxCell.Value = Resources.CxnOff;
            projectRunStatuxCell.ImageLayout = DataGridViewImageCellLayout.Normal;
            DataGridViewButtonCell projectBtnRunStartCell = docRow.Cells[this.colBtnConnectStart.Name] as DataGridViewButtonCell;
            //projectBtnRunStartCell.
            projectBtnRunStartCell.Value = Program.LangSys.C("Connect / Start");
        }

        /// <summary>
        /// 
        /// </summary>
        private void SolutionClose()
        {
            m_GestSolution = null;
            //UpdateFileMenu();
        }

        #endregion

        #region Handlers d'event de la form
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MDISmartCommandMain_Load(object sender, EventArgs e)
        {
            string IniOptionFileName = PathTranslator.LinuxVsWindowsPathUse(Application.StartupPath + @"\" + Cste.STR_OPTINI_FILENAME);
            m_Option.Load(IniOptionFileName);
            m_strLogFilePath = m_Option.LogDir;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MDISmartCommandMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            m_Option.LogDir = m_strLogFilePath;
            StopActiveDocuments(); 
            SolutionClose();
            m_Option.Save();
            m_mruStripMenu.SaveToFile();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MDISmartCommandMain_Shown(object sender, EventArgs e)
        {
            TryAutoOpenDoc();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MDISmartCommandMain_SizeChanged(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                this.Visible = false;
                UpdateTrayMenuFromState();
            }
        }

        #endregion

        #region handler du menu tool
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuItemOptions_Click(object sender, EventArgs e)
        {
            OptionForm optForm = new OptionForm();
            optForm.LogFileDirectory = m_strLogFilePath;
            //optForm.SaveFileComm = m_bSaveFileComm;
            if (optForm.ShowDialog() == DialogResult.OK)
            {
                m_strLogFilePath = optForm.LogFileDirectory;
                //m_bSaveFileComm = optForm.SaveFileComm;
            }
        }
        #endregion

        #region handler du menu ?
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutForm.ShowAbout();
        }
        #endregion

        #region methodes divers
        /// <summary>
        /// 
        /// </summary>
        public void TryAutoOpenDoc()
        {
            if (!string.IsNullOrEmpty(m_strAutoOpenFileName))
            {
                if (File.Exists(m_strAutoOpenFileName))
                {
                    SolutionOpen(m_strAutoOpenFileName);
                    this.BringToFront();
                }
                else
                {
                    MessageBox.Show(string.Format(Program.LangSys.C("File {0} does not exists"), m_strAutoOpenFileName), Program.LangSys.C("Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else // chaine vide
                return;

            if (LaunchArgParser.AutoConnect)
            {
                foreach (string docName in m_GestSolution.Keys)
                {
                    BTDoc doc = m_GestSolution[docName];
                    doc.OpenDocumentComm();
                    if (doc.Communication.IsOpen && LaunchArgParser.AutoStart)
                    {
                        doc.TraiteMessage(MESSAGE.MESS_CMD_RUN, null, Program.TypeApp);
                    }
                    else if (! doc.Communication.IsOpen && !LaunchArgParser.AutoStart)
                    {
                        LogEvent log = new LogEvent(LOG_EVENT_TYPE.ERROR, Program.LangSys.C("Failed to connect"));
                        MDISmartCommandMain.EventLogger.AddLogEvent(log);
                    }
                    else if (!doc.Communication.IsOpen && LaunchArgParser.AutoStart)
                    {
                        LogEvent log = new LogEvent(LOG_EVENT_TYPE.ERROR, Program.LangSys.C("Failed to connect. Application not started"));
                        MDISmartCommandMain.EventLogger.AddLogEvent(log);
                    }
                }
            }
            if (LaunchArgParser.OperatorMode)
            {
                this.menuStrip.Visible = false;
                this.m_StatusBar.Visible = false;
                this.dataGridMonitor.Enabled = false;
                // on enlève les bouton des la barre de titre
                this.ControlBox = false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Event"></param>
        protected void AddLogEvent(LogEvent Event)
        {
            m_EventLogPanel.AddLogEvent(Event);
        }
        #endregion

        #region handlers du menu ?
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pluginsVersionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PluginsVersionsForm plVer = new PluginsVersionsForm();
            plVer.ShowDialog();
        }
        #endregion

        #region handler des menu caché du développeur
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuItemLogConfig_Click(object sender, EventArgs e)
        {
            LogCatForm LogForm = new LogCatForm();
            LogForm.Level = (TracesLevel)SmartApp.Properties.Settings.Default.LogLevel;
            LogForm.ActiveCats = (TraceCat)Convert.ToInt32(SmartApp.Properties.Settings.Default.LogCat, 16);
            LogForm.LogToFile = SmartApp.Properties.Settings.Default.LogToFile;
            if (LogForm.ShowDialog() == DialogResult.OK)
            {
                SmartApp.Properties.Settings.Default.LogLevel = (int)LogForm.Level;
                SmartApp.Properties.Settings.Default.LogCat = Convert.ToString((int)LogForm.ActiveCats, 16);
                SmartApp.Properties.Settings.Default.LogToFile = LogForm.LogToFile;
                SmartApp.Properties.Settings.Default.Save();
                Traces.Cats = LogForm.ActiveCats;
                Traces.Level = LogForm.Level;
                Traces.LogToFile = LogForm.LogToFile;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuItemOpenDebugConsole_Click(object sender, EventArgs e)
        {
            if (m_TraceConsole == null)
                m_TraceConsole = new TraceConsole();
            else
            {
                m_TraceConsole.Dispose();
                m_TraceConsole = new TraceConsole();
            }

            m_TraceConsole.Show();
        }
        #endregion

    }
}

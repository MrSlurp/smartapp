using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SmartApp.AppEventLog;
using CommonLib;

using System.IO;
using System.Reflection;

namespace SmartApp
{
    public partial class MDISmartCommandMain : Form
    {
#if KENNEN
        private const string APP_TITLE = "Kennen";
#else
        private const string APP_TITLE = "SmartCommand";
#endif

        #region données membres
        // fenêtre des variables (Watch)
        private VariableForm m_VariableForm;
        // fenêtre des données virtuelles (affichée que si on utilise une connexion virtuell
        private VirtualDataForm m_VirtualDataForm;
        // Document chargé par l'application
        private BTDoc m_Document = null;
        // fenêtre de log des évènements
        private static AppEventLogForm m_EventLog;
        // fenêtre de configuration des connexions
        private CommConfiguration m_CommConfigPage;
        // Liste des pages "utilisateur" ==> une par BTScreen présent dans le document
        private List<DynamicPanelForm> m_FormList = new List<DynamicPanelForm>();
        // fichier d'ini des options
        private AppOptions m_Option = new AppOptions();
        // chemin du dossier de log utilisateurs
        private string m_strLogFilePath;
        // booléen indiquant si il faut mémoriser les connexion utilisées pour chaque fichier
        private bool m_bSaveFileComm = true;
        // stocke temporairement le nom de fichier passé par la ligne de commande
        private string m_strAutoOpenFileName = "";

        private bool m_bFullScreenMode = false;
        private FormWindowState m_PrevFullScreenState = FormWindowState.Normal;

        protected MruStripMenuInline m_mruStripMenu;

        #endregion

        #region attributs
        //*****************************************************************************************************
        // Description: accesseur de la fenêtre event log. Défini comme étant static (un seul event log)
        // Return: /
        //*****************************************************************************************************
        public static AppEventLogForm EventLogger
        {
            get
            {
                return m_EventLog;
            }
        }
        #endregion

        #region constructeurs
        //*****************************************************************************************************
        // Description: constructeur par défaut
        // Return: /
        //*****************************************************************************************************
        public MDISmartCommandMain()
        {
            Program.LangSys.Initialize(this, Cste.STR_DEV_LANG, SmartApp.Properties.Settings.Default.Lang, "SmartApp");
            InitializeComponent();
            CommonConstructorInit();
        }

        //*****************************************************************************************************
        // Description: constructeur avec nom de fichier (ouvre le fichier passé en paramètre)
        // Return: /
        //*****************************************************************************************************
        public MDISmartCommandMain(string strFileName)
        {
            InitializeComponent();
            CommonConstructorInit();
            m_strAutoOpenFileName = strFileName;
        }

        //*****************************************************************************************************
        // Description: Initialisations communes aux deux constructeurs
        // Return: /
        //*****************************************************************************************************
        public void CommonConstructorInit()
        {
            m_EventLog = new AppEventLogForm();
            m_CommConfigPage = new CommConfiguration();
            this.Text = APP_TITLE;
            this.Icon = CommonLib.Resources.AppIcon;
            m_EventLog.MdiParent = this;
            m_tsBtnStartStop.Enabled = false;
            m_tsBtnConnexion.Enabled = false;
            string strAppDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetModules()[0].FullyQualifiedName);
            string strIniFilePath = PathTranslator.LinuxVsWindowsPathUse(strAppDir + @"\" + Cste.STR_FORMPOSINI_FILENAME);
            m_mruStripMenu = new MruStripMenuInline(this.fileMenu, this.m_MruFiles, new MruStripMenu.ClickedHandler(OnMruFile), strIniFilePath);
            UpdateToolBarCxnItemState();
            AsyncUpdater();
            InitCboComms();
        }
        #endregion

        #region Update de la toolbar
        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************      
        public void UpdateToolBarCxnItemState()
        {
            if (m_Document != null && !m_Document.m_Comm.IsOpen)
                m_tsCboCurConnection.Enabled = true;
            else
                m_tsCboCurConnection.Enabled = false;

            if (m_Document == null || (m_Document != null && !m_Document.m_Comm.IsOpen))
                m_tsBtnConfigComm.Enabled = true;
            else
                m_tsBtnConfigComm.Enabled = false;
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************      
        void OnCommStateChange()
        {
            if (m_Document != null)
            {
                if (this.InvokeRequired)
                {
                    CommOpenedStateChange AsyncCall = new CommOpenedStateChange(AsyncUpdater);
                    this.Invoke(AsyncCall);
                }
                else
                {
                    AsyncUpdater();
                }
            }
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************      
        protected void AsyncUpdater()
        {
            if (m_Document != null && m_Document.m_Comm != null)
            {
                if (m_Document.m_Comm.IsOpen)
                {
                    m_tsBtnConnexion.Checked = true;
                    m_tsBtnConnexion.Text = "Connected";
                    m_tsBtnConnexion.Image = Resources.CxnOn;
                    m_tsBtnStartStop.Enabled = true;
                    UpdateToolBarCxnItemState();
                }
                else
                {
                    m_tsBtnConnexion.Checked = false;
                    m_tsBtnConnexion.Text = "Disconnected";
                    m_tsBtnConnexion.Image = Resources.CxnOff;
                    m_tsBtnStartStop.Enabled = false;
                    m_Document.TraiteMessage(MESSAGE.MESS_CMD_STOP, null, Program.TypeApp);
                    for (int i = 0; i < m_FormList.Count; i++)
                    {
                        m_FormList[i].DynamicPanelEnabled = false;
                    }

                    m_tsBtnStartStop.Checked = false;
                    UpdateStartStopButtonState();
                    UpdateToolBarCxnItemState();
                }
            }
            else
            {
                m_tsBtnConnexion.Checked = false;
                m_tsBtnConnexion.Text = "Disconnected";
                m_tsBtnConnexion.Image = Resources.CxnOff;
                m_tsBtnStartStop.Enabled = false;
                UpdateStartStopButtonState();
            }
        }

        #endregion

        #region Handlers du menu fichier
        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        private void OpenFile(object sender, EventArgs e)
        {
            this.CloseDoc();
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "SmartApp File (*.saf)|*.saf";
            openFileDialog.InitialDirectory = Application.StartupPath;
            DialogResult dlgRes = openFileDialog.ShowDialog();
            if (dlgRes == DialogResult.OK)
            {
                string strFileFullName = openFileDialog.FileName;
                if (!OpenDoc(strFileFullName))
                {
                    MessageBox.Show("Error while reading file. File is corrupted", "Error");
                    this.CloseDoc();
                }
            }
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            this.CloseDoc();
            Application.Exit();
        }

        private void OnMruFile(int number, String filename)
        {
            if (!File.Exists(filename))
            {
                MessageBox.Show("The file '" + filename + "'cannot be opened and will be removed from the Recent list(s)"
                    , "MruToolStripMenu Demo"
                    , MessageBoxButtons.OK
                    , MessageBoxIcon.Error);
                m_mruStripMenu.RemoveFile(number);
            }
            else
            {
                OpenDoc(filename);
                m_mruStripMenu.SetFirstFile(number);
            }
        }

        #endregion

        #region handlers du menu View
        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        private void ToolBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStrip.Visible = toolBarToolStripMenuItem.Checked;
        }
        #endregion

        #region fonction d'ouverture sauvegarde et fermeture du document
        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        private bool OpenDoc(string strFullFileName)
        {
            m_Document = new BTDoc(Program.TypeApp);
            m_Document.OnCommStateChange += new CommOpenedStateChange(OnCommStateChange);
            m_Document.EventAddLogEvent += new AddLogEventDelegate(AddLogEvent);
#if LINUX
            int lastindex = strFullFileName.LastIndexOf(@"/");
#else
            int lastindex = strFullFileName.LastIndexOf(@"\");
#endif
            string DossierFichier = strFullFileName.Substring(0, strFullFileName.Length - (strFullFileName.Length - lastindex));
            PathTranslator.BTDocPath = DossierFichier;
            if (m_Document.ReadConfigDocument(strFullFileName, Program.TypeApp, Program.DllGest))
            {
                if (OpenDocument(m_Document))
                {
                    string strFileName = strFullFileName.Substring(lastindex + 1);
                    this.Text += " - " + strFileName;
                    m_tsBtnConnexion.Enabled = true;
                    UpdateToolBarCxnItemState();
                    if (m_tsCboCurConnection.Items.Count != 0 && m_tsCboCurConnection.SelectedIndex == -1)
                    {
                        m_tsCboCurConnection.SelectedIndex = 0;
                    }
                    SetTypeComeAndParamFromCbo();
                    return true;
                }
                else
                    return false;
            }
            else
                return false;
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        private bool OpenDocument(BTDoc Doc)
        {
            Doc.LogFilePath = m_strLogFilePath;
            m_EventLog.Show();
            if (!Doc.FinalizeRead(this))
            {
                Console.WriteLine("Erreur lors du FinalizeRead()");
                MessageBox.Show("Can't initialize run mode datas. Please contact support", "Error");
                CloseDoc();
                return false;
            }
            string strTypeComm = m_Option.GetFileCommType(Doc.FileName);
            string strCommParam = m_Option.GetFileCommParam(Doc.FileName);
            SelectCommInComboOrCreateTemp(strTypeComm, strCommParam);

            for (int i = 0; i < Doc.GestScreen.Count; i++)
            {
                BTScreen Scr = (BTScreen)Doc.GestScreen[i];
                Scr.Panel.SetMeToTop += new DynamicPanel.SetMeToTopEvent(SetClientFormToTop);
                DynamicPanelForm Frm = new DynamicPanelForm(Scr.Panel);
                Scr.Panel.Location = new Point(10, 10);
                Frm.ClientSize = new System.Drawing.Size(Scr.Panel.Width + Scr.Panel.Left + 10,
                                                    Scr.Panel.Height + Scr.Panel.Top + 10);
                Frm.Text = Scr.Title;
                Frm.MdiParent = this;
                Frm.Show();
                Frm.DynamicPanelEnabled = false;
                m_FormList.Add(Frm);
            }
            if (m_VariableForm != null)
            {
                m_VariableForm.Hide();
                m_VariableForm = null;
            }
            m_VariableForm = new VariableForm(m_Document.GestData);
            m_VariableForm.MdiParent = this;
            m_VariableForm.Show();
            if (!m_EventLog.IsEmpty)
            {
                m_EventLog.BringToFront();
            }
            return true;
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        private bool CloseDoc()
        {
            if (m_Document != null)
            {
                if (m_bSaveFileComm)
                {
                    string strTypeComm = m_Document.m_Comm.CommType.ToString();
                    string strCommParam = m_Document.m_Comm.CommParam;
                    m_Option.SetFileCommType(m_Document.FileName, strTypeComm);
                    m_Option.SetFileCommParam(m_Document.FileName, strCommParam);
                }
                m_Document.TraiteMessage(MESSAGE.MESS_CMD_STOP, null, Program.TypeApp);
                m_Document.DetachCommEventHandler(OnCommStateChange);
                m_Document.CloseDocumentComm();
                TraiteCommStateVirtualDataForm();
            }
            m_Document = null;

            for (int i = 0; i < this.MdiChildren.Length; i++)
            {
                this.MdiChildren[i].Hide();
            }
            m_EventLog.Hide();
            m_tsBtnConnexion.Enabled = false;
            m_tsBtnStartStop.Enabled = false;
            UpdateToolBarCxnItemState();
            m_FormList.Clear();
            if (m_VariableForm != null)
            {
                m_VariableForm.Hide();
                m_VariableForm = null;
            }

            return true;
        }
        #endregion

        #region Handlers d'event de la form
        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************      
        private void OnFormClosing(object sender, FormClosingEventArgs e)
        {
            if (m_Document != null)
            {
                this.CloseDoc();
            }
            m_mruStripMenu.SaveToFile();
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************      
        private void MDISmartCommandMain_Load(object sender, EventArgs e)
        {
            string IniOptionFileName = PathTranslator.LinuxVsWindowsPathUse(Application.StartupPath + @"\" + Cste.STR_OPTINI_FILENAME);
            m_Option.Load(IniOptionFileName);

            m_strLogFilePath = m_Option.LogDir;
            m_bSaveFileComm = m_Option.SaveFileComParam;

        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************      
        private void MDISmartCommandMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            m_Option.LogDir = m_strLogFilePath;
            m_Option.SaveFileComParam = m_bSaveFileComm;
            m_Option.Save();
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************      
        private void MDISmartCommandMain_Shown(object sender, EventArgs e)
        {
            TryAutoOpenDoc();
        }
        #endregion

        #region Handlers de la tool bar
        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************      
        private void m_tsBtnConnexion_Click(object sender, EventArgs e)
        {
            if (m_Document != null)
            {
                if (m_Document.m_Comm.IsOpen)
                {
                    m_Document.CloseDocumentComm();
                    TraiteCommStateVirtualDataForm();
                }
                else
                {
                    m_Document.OpenDocumentComm();
                    TraiteCommStateVirtualDataForm();
                }
            }
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************      
        private void m_tsBtnStartStop_Click(object sender, EventArgs e)
        {
            if (m_Document != null)
            {
                if (m_tsBtnStartStop.Checked == false)
                {
                    for (int i = 0; i < m_FormList.Count; i++)
                    {
                        m_FormList[i].DynamicPanelEnabled = true;
                    }
                    m_tsBtnStartStop.Checked = true;
                    UpdateStartStopButtonState();
                    m_Document.TraiteMessage(MESSAGE.MESS_PRE_PARSE, null, Program.TypeApp);
                    m_Document.TraiteMessage(MESSAGE.MESS_CMD_RUN, null, Program.TypeApp);
                    UpdateToolBarCxnItemState();
                }
                else
                {
                    for (int i = 0; i < m_FormList.Count; i++)
                    {
                        m_FormList[i].DynamicPanelEnabled = false;
                    }
                    m_tsBtnStartStop.Checked = false;
                    UpdateStartStopButtonState();
                    m_Document.TraiteMessage(MESSAGE.MESS_CMD_STOP, null, Program.TypeApp);
                    UpdateToolBarCxnItemState();
                }
            }
            else
            {
                for (int i = 0; i < m_FormList.Count; i++)
                {
                    m_FormList[i].DynamicPanelEnabled = false;
                }
                UpdateStartStopButtonState();
                m_tsBtnStartStop.Image = Resources.CxnOff;
            }

        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************      
        private void UpdateStartStopButtonState()
        {
            if (m_tsBtnStartStop.Checked == true)
            {
                m_tsBtnStartStop.Text = "Running";
                m_tsBtnStartStop.Image = Resources.CxnOn;
            }
            else
            {
                m_tsBtnStartStop.Text = "Stoppped";
                m_tsBtnStartStop.Image = Resources.CxnOff;
            }
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************      
        private void m_tsBtnConfigComm_Click(object sender, EventArgs e)
        {
            m_CommConfigPage.ShowDialog();
            InitCboComms();
        }
        #endregion

        #region Gestion de la combo de connexion
        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        protected void SelectCommInComboOrCreateTemp(string strTypeComm, string strCommParam)
        {
            if (!string.IsNullOrEmpty(strTypeComm) && !string.IsNullOrEmpty(strCommParam))
            {
                bool bCxnExist = false;
                for (int i = 0; i < m_tsCboCurConnection.Items.Count; i++)
                {
                    string comm = m_tsCboCurConnection.Items[i].ToString();
                    string[] TabComm = comm.Split('/');
                    if (TabComm[1].Trim() == strTypeComm && TabComm[2].Trim() == strCommParam)
                    {
                        bCxnExist = true;
                        m_tsCboCurConnection.SelectedIndex = i;
                        break;
                    }
                }
                if (!bCxnExist)
                {
                    int index = m_tsCboCurConnection.Items.Add("New connection /" + strTypeComm + "/" + strCommParam);
                    m_tsCboCurConnection.SelectedIndex = index;
                }
            }
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************      
        private void InitCboComms()
        {
            m_tsCboCurConnection.Items.Clear();
            for (int i = 0; i < Cste.NB_MAX_COMM; i++)
            {
                string strSection = string.Format(Cste.STR_FILE_DESC_HEADER_FORMAT, i);
                string strName = m_CommConfigPage.IniFile.GetValue(strSection, Cste.STR_FILE_DESC_NAME);
                string strCommType = m_CommConfigPage.IniFile.GetValue(strSection, Cste.STR_FILE_DESC_COMM);
                string strCommParam = m_CommConfigPage.IniFile.GetValue(strSection, Cste.STR_FILE_DESC_ADDR);
                if (!string.IsNullOrEmpty(strCommType)
                    && !string.IsNullOrEmpty(strCommParam)
                    )
                {
                    if (string.IsNullOrEmpty(strName))
                        strName = string.Format("Connection {0}", i);
                    AddStringToCombo(strName, strCommType, strCommParam, strSection);
                }
            }
            if (m_tsCboCurConnection.Items.Count > 0)
                m_tsCboCurConnection.SelectedIndex = 0;
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************      
        private void AddStringToCombo(string Name, string CommType, string CommParam, string Section)
        {
            m_tsCboCurConnection.Items.Add(Name + "/" + CommType + "/" + CommParam);
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************      
        private void m_tsCboCurConnection_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetTypeComeAndParamFromCbo();
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************      
        private void SetTypeComeAndParamFromCbo()
        {
            if (m_tsCboCurConnection.SelectedItem != null)
            {
                string comm = m_tsCboCurConnection.SelectedItem.ToString();
                if (!string.IsNullOrEmpty(comm))
                {
                    string[] TabComm = comm.Split('/');
                    if (m_Document != null)
                    {
                        if (!m_Document.m_Comm.IsOpen)
                        {
                            TYPE_COMM type = (TYPE_COMM)Enum.Parse(typeof(TYPE_COMM), TabComm[1].Trim());
                            m_Document.m_Comm.SetCommTypeAndParam(type, TabComm[2]);
                        }
                    }
                }
            }
        }
        #endregion

        #region handler du menu tool
        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************      
        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OptionForm optForm = new OptionForm();
            optForm.LogFileDirectory = m_strLogFilePath;
            optForm.SaveFileComm = m_bSaveFileComm;
            if (optForm.ShowDialog() == DialogResult.OK)
            {
                m_strLogFilePath = optForm.LogFileDirectory;
                m_bSaveFileComm = optForm.SaveFileComm;
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
        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public void TryAutoOpenDoc()
        {
            if (!string.IsNullOrEmpty(m_strAutoOpenFileName))
            {
                if (File.Exists(m_strAutoOpenFileName))
                {
                    if (!OpenDoc(m_strAutoOpenFileName))
                    {
                        MessageBox.Show("Error while reading file. File is corrupted", "Error");
                        this.CloseDoc();
                        return;
                    }
                }
                else
                {
                    MessageBox.Show(string.Format("File \"{0}\" does not exists", m_strAutoOpenFileName), "Error");
                    return;
                }
                UpdateToolBarCxnItemState();
            }
            else // chaine vide
                return;
            if (LaunchArgParser.AutoConnect)
            {
                if (m_Document.m_Comm.SetCommTypeAndParam(LaunchArgParser.CommType, LaunchArgParser.CommParam))
                {
                    SelectCommInComboOrCreateTemp(LaunchArgParser.CommType.ToString(), LaunchArgParser.CommParam);
                    m_Document.OpenDocumentComm();
                    TraiteCommStateVirtualDataForm();
                    if (m_Document.m_Comm.IsOpen && LaunchArgParser.AutoStart)
                    {
                        m_tsBtnStartStop_Click(null, null);
                    }
                    else if (!m_Document.m_Comm.IsOpen && LaunchArgParser.AutoStart == false)
                    {
                        LogEvent log = new LogEvent(LOG_EVENT_TYPE.ERROR, "Failed to connect");
                        MDISmartCommandMain.EventLogger.AddLogEvent(log);
                    }
                    else if (!m_Document.m_Comm.IsOpen && LaunchArgParser.AutoStart == true)
                    {
                        LogEvent log = new LogEvent(LOG_EVENT_TYPE.ERROR, "Failed to connect. Application not started");
                        MDISmartCommandMain.EventLogger.AddLogEvent(log);
                    }
                }
            }
            if (LaunchArgParser.OperatorMode)
            {
                this.menuStrip.Visible = false;
                this.m_StatusBar.Visible = false;
                this.m_tsBtnFullScreen.Visible = false;
                this.m_tsBtnConfigComm.Visible = false;
                this.m_tsCboCurConnection.Visible = false;
                this.openToolStripButton.Visible = false;
                // on enlève les bouton des la barre de titre
                this.ControlBox = false;
                // on enlève les bord
                this.FormBorderStyle = FormBorderStyle.None;
            }
            if (LaunchArgParser.AutoPosition)
            {
                this.Location = LaunchArgParser.StartupBound.Location;
                this.Size = LaunchArgParser.StartupBound.Size;
            }
            if (LaunchArgParser.AutoMaximizeFirstScreen)
            {
                if (m_FormList.Count >=1)
                {
                    m_FormList[0].WindowState = FormWindowState.Maximized;
                }
            }
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        private void TraiteCommStateVirtualDataForm()
        {
            if (m_Document != null)
            {
                if (m_Document.m_Comm.IsOpen)
                {
                    if (m_Document.TypeComm == TYPE_COMM.VIRTUAL && m_VirtualDataForm == null)
                    {
                        m_VirtualDataForm = new VirtualDataForm(m_Document.GestDataVirtual, m_Document.GestData);
                        m_VirtualDataForm.Show();
                        m_VirtualDataForm.BringToFront();
                    }
                }
                else
                {
                    if (m_VirtualDataForm != null)
                    {
                        m_VirtualDataForm.Hide();
                        m_VirtualDataForm = null;
                    }
                }
            }
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        protected void AddLogEvent(LogEvent Event)
        {
            m_EventLog.AddLogEvent(Event);
        }
        #endregion


        private void m_tsBtnFullScreen_Click(object sender, EventArgs e)
        {
            if (!m_bFullScreenMode)
                m_PrevFullScreenState = this.WindowState;

            m_bFullScreenMode = !m_bFullScreenMode;
            if (m_bFullScreenMode)
            {
#if !LINUX
                this.SuspendLayout();
                // on cache le menu, la bare de status
                this.menuStrip.Visible = false;
                this.m_StatusBar.Visible = false;
                // on enlève les bouton des la barre de titre
                this.ControlBox = false;
                // on maximise la fenêtre principale 
                this.WindowState = FormWindowState.Maximized;
                // on enlève les bord
                this.FormBorderStyle = FormBorderStyle.None;
                this.ResumeLayout();
#else
                this.menuStrip.Visible = false;
                this.m_StatusBar.Visible = false;
                this.WindowState = FormWindowState.Maximized;
#endif
            }
            else
            {
#if !LINUX
                this.SuspendLayout();
                // tout l'inverse de ce qui est fait au dessus
                this.menuStrip.Visible = true;
                this.m_StatusBar.Visible = true;
                this.WindowState = m_PrevFullScreenState;
                this.ControlBox = true;
                this.FormBorderStyle = FormBorderStyle.Sizable;
                this.MaximizeBox = true;
                this.ResumeLayout();
#else
                this.menuStrip.Visible = true;
                this.m_StatusBar.Visible = true;
#endif
            }
        }

        private void SetClientFormToTop(Form ClientForm)
        {
#if LINUX
            this.ActivateMdiChild(ClientForm);
#else
            ClientForm.BringToFront();
#endif
        }

        private void pluginsVersionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PluginsVersionsForm plVer = new PluginsVersionsForm();
            plVer.ShowDialog();
        }
    }
}

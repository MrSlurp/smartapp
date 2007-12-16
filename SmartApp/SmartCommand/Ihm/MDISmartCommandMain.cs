using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SmartApp.AppEventLog;
using SmartApp.Datas;
using SmartApp.Comm;
using System.IO;
using System.Reflection;

namespace SmartApp
{
    public partial class MDISmartCommandMain : Form
    {
        private VariableForm m_VariableForm;
        private BTDoc m_Document = null;
        private static AppEventLogForm m_EventLog = new AppEventLogForm();
        CommConfiguration m_CommConfigPage = new CommConfiguration();
        List<DynamicPanelForm> m_FormList = new List<DynamicPanelForm>();
        IniFileParser m_IniFile = new IniFileParser();
        private string m_strIniFilePath;
        private string m_strLogFilePath;
        private bool m_bSaveFileComm = true;

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public static AppEventLogForm EventLogger
        {
            get
            {
                return m_EventLog;
            }
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public MDISmartCommandMain()
        {
            InitializeComponent();
            m_EventLog.MdiParent = this;
            m_tsBtnStartStop.Enabled = false;
            m_tsBtnConnexion.Enabled = false;
            UpdateToolBarCxnItemState();
            InitCboComms();
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public MDISmartCommandMain(string strFileName)
        {
            InitializeComponent();
            m_EventLog.MdiParent = this;
            m_tsBtnStartStop.Enabled = false;
            m_tsBtnConnexion.Enabled = false;
            UpdateToolBarCxnItemState();
            if (!string.IsNullOrEmpty(strFileName))
            {
                OpenDoc(strFileName);
                InitCboComms();
                UpdateToolBarCxnItemState();
            }
            if (LaunchArgParser.AutoConnect)
            {
                if (m_Document.m_Comm.SetCommTypeAndParam(LaunchArgParser.CommType, LaunchArgParser.CommParam))
                {
                    SelectCommInComboOrCreateTemp(LaunchArgParser.CommType.ToString(), LaunchArgParser.CommParam);
                    m_Document.OpenDocumentComm();
                    if (m_Document.m_Comm.IsOpen && LaunchArgParser.AutoStart)
                    {
                        m_tsBtnStartStop_Click(null, null);
                    }
                }
            }
        }

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
        private void OpenFile(object sender, EventArgs e)
        {
            this.CloseDoc();
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "BTApp2 File (*.saf)|*.saf";
            openFileDialog.InitialDirectory = Application.StartupPath;
            DialogResult dlgRes = openFileDialog.ShowDialog();
            if (dlgRes == DialogResult.OK)
            {
                string strFileFullName = openFileDialog.FileName;
                if (!OpenDoc(strFileFullName))
                {
                    MessageBox.Show("Erreur lors de l'ouverture du fichier", "Erreur");
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
            Application.Exit();
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        private void ToolBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStrip.Visible = toolBarToolStripMenuItem.Checked;
        }

        #region fonction d'ouverture sauvegarde et fermeture du document
        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        private bool OpenDoc(string strFullFileName)
        {
            m_Document = new BTDoc();
            m_Document.OnCommStateChange += new SmartApp.Comm.CommOpenedStateChange(OnCommStateChange);
            if (m_Document.ReadConfigDocument(strFullFileName))
            {
                if (OpenDocument(m_Document))
                {
                    int lastindex = strFullFileName.LastIndexOf(@"\");
                    string strFileName = strFullFileName.Substring(lastindex + 1);
                    this.Text += " - " + strFileName;
                    m_tsBtnConnexion.Enabled = true;
                    UpdateToolBarCxnItemState();
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
        void OnCommStateChange()
        {
            if (m_Document != null)
            {
                if (this.InvokeRequired)
                {
                    SmartApp.Comm.CommOpenedStateChange AsyncCall = new SmartApp.Comm.CommOpenedStateChange(AsyncUpdater);
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
            if (m_Document.m_Comm.IsOpen)
            {
                m_tsBtnConnexion.Checked = true;
                m_tsBtnConnexion.Text = "Connected";
                m_tsBtnConnexion.Image = global::SmartApp.Properties.Resources.CxnOn;
                m_tsBtnStartStop.Enabled = true;
                UpdateToolBarCxnItemState();
            }
            else
            {
                m_tsBtnConnexion.Checked = false;
                m_tsBtnConnexion.Text = "Disconnected";
                m_tsBtnConnexion.Image = global::SmartApp.Properties.Resources.CxnOff;
                m_tsBtnStartStop.Enabled = false;
                m_Document.TraiteMessage(MESSAGE.MESS_CMD_STOP, null);
                m_tsBtnStartStop.Checked = false;
                UpdateToolBarCxnItemState();
            }
        }


        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        private bool OpenDocument(BTDoc Doc)
        {
            Doc.LogFilePath = m_strLogFilePath;
            if (!Doc.FinalizeRead(this))
            {
                Console.WriteLine("Erreur lors du FinalizeRead()");
                MessageBox.Show("Can't initialize run mode datas. Please contact developper", "Error");
                CloseDoc();
                return false;
            }
            string strTypeComm = m_IniFile.GetValue(Doc.FileName, Cste.STR_FILE_DESC_COMM);
            string strCommParam = m_IniFile.GetValue(Doc.FileName, Cste.STR_FILE_DESC_ADDR);
            SelectCommInComboOrCreateTemp(strTypeComm, strCommParam);
            m_EventLog.Show();

            for (int i = 0; i < Doc.GestScreen.Count; i++)
            {
                BTScreen Scr = (BTScreen)Doc.GestScreen[i];
                DynamicPanelForm Frm = new DynamicPanelForm(Scr.Panel);
                Scr.Panel.Location = new Point(10, 10);                
                Frm.ClientSize = new System.Drawing.Size(Scr.Panel.Width + Scr.Panel.Left+10,
                                                    Scr.Panel.Height + Scr.Panel.Top+10);
                Frm.Text = Scr.Title;
                Frm.MdiParent = this;
                Frm.Show();
                Frm.DynamicPanelEnabled = false;
                m_FormList.Add(Frm);
                if (!string.IsNullOrEmpty(m_tsCboCurConnection.SelectedText))
                {
                    SetTypeComeAndParamFromCbo();
                }
            }
            if (m_VariableForm != null)
            {
                m_VariableForm.Hide();
                m_VariableForm = null;
            }
            m_VariableForm = new VariableForm(m_Document.GestData);
            m_VariableForm.MdiParent = this;
            m_VariableForm.Show();
            return true;
        }

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
        private bool CloseDoc()
        {
            if (m_Document != null)
            {
                if (m_bSaveFileComm)
                {
                    string strTypeComm = m_Document.m_Comm.CommType.ToString();
                    string strCommParam = m_Document.m_Comm.CommParam;
                    m_IniFile.SetValue(m_Document.FileName, Cste.STR_FILE_DESC_COMM, strTypeComm);
                    m_IniFile.SetValue(m_Document.FileName, Cste.STR_FILE_DESC_ADDR, strCommParam);
                }
                m_Document.TraiteMessage(MESSAGE.MESS_CMD_STOP, null);
                m_Document.DetachCommEventHandler(OnCommStateChange);
                m_Document.CloseDocumentComm();
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
                }
                else
                    m_Document.OpenDocumentComm();
            }
        }

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
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************      
        private void MDISmartCommandMain_Load(object sender, EventArgs e)
        {
            string strAppDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetModules()[0].FullyQualifiedName);
            m_strIniFilePath = strAppDir + @"\" + Cste.STR_OPTINI_FILENAME;
            m_IniFile.Load(m_strIniFilePath);

            m_strLogFilePath = m_IniFile.GetValue(Cste.STR_FILE_DESC_HEADER_OPT, Cste.STR_FILE_DESC_LOGDIR);
            string strSaveFileComm = m_IniFile.GetValue(Cste.STR_FILE_DESC_HEADER_OPT, Cste.STR_FILE_DESC_SAVE_PREF_COMM);
            bool.TryParse(strSaveFileComm, out m_bSaveFileComm);
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************      
        private void MDISmartCommandMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            m_IniFile.SetValue(Cste.STR_FILE_DESC_HEADER_OPT, Cste.STR_FILE_DESC_LOGDIR, m_strLogFilePath);
            m_IniFile.SetValue(Cste.STR_FILE_DESC_HEADER_OPT, Cste.STR_FILE_DESC_SAVE_PREF_COMM, m_bSaveFileComm.ToString());
            m_IniFile.Save(m_strIniFilePath);
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
                    m_tsBtnStartStop.Text = "Running";
                    m_tsBtnStartStop.Image = global::SmartApp.Properties.Resources.CxnOn;
                    m_Document.TraiteMessage(MESSAGE.MESS_CMD_RUN, null);
                    UpdateToolBarCxnItemState();
                }
                else
                {
                    for (int i = 0; i < m_FormList.Count; i++)
                    {
                        m_FormList[i].DynamicPanelEnabled = false;
                    }
                    m_tsBtnStartStop.Checked = false;
                    m_tsBtnStartStop.Text = "Stoppped";
                    m_tsBtnStartStop.Image = global::SmartApp.Properties.Resources.CxnOff;
                    m_Document.TraiteMessage(MESSAGE.MESS_CMD_STOP, null);
                    UpdateToolBarCxnItemState();
                }
            }
            else
            {
                for (int i = 0; i < m_FormList.Count; i++)
                {
                    m_FormList[i].DynamicPanelEnabled = false;
                }
                m_tsBtnStartStop.Checked = false;
                m_tsBtnStartStop.Text = "Stoppped";
                m_tsBtnStartStop.Image = global::SmartApp.Properties.Resources.CxnOff;
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
            if (m_tsCboCurConnection.Items.Count >0)
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
            string comm = m_tsCboCurConnection.SelectedItem.ToString();
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

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutForm.ShowAbout();
        }
    }
}

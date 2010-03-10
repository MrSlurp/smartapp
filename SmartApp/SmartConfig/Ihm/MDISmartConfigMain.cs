using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SmartApp.Ihm;
using SmartApp.Ihm.Wizards;
using Microsoft.Win32;
using System.Reflection;
using CommonLib;

namespace SmartApp.Ihm
{
    public partial class MDISmartConfigMain : Form
    {
#if KENNEN
        private const string APP_TITLE = "Kennen";
#else
        private const string APP_TITLE = "SmartConfig";
#endif
        protected delegate void UpdateTitleDg(string str);

        #region données membres
        protected DataForm m_DataForm;
        protected DesignerForm m_DesignForm;
        protected FrameForm m_FrameForm;
        protected ProgramForm m_ProgForm;
        protected BTDoc m_Document = null;
        IniFileParser m_IniFile = new IniFileParser();
        private string m_strIniFilePath;
        protected string m_strDocumentName = "";

        protected MruStripMenuInline m_mruStripMenu;
        #endregion

        #region constructeurs
        //*****************************************************************************************************
        // Description: constructeur par défaut
        // Return: /
        //*****************************************************************************************************
        public MDISmartConfigMain()
        {
            DoFileFormatRegistration();
            Program.LangSys.Initialize(this);
            InitializeComponent();
            CommonConstructorInit();
        }
        //*****************************************************************************************************
        // Description: constructeur ouvrant le fichier passé en paramètre dès la fin de l'initialisation
        // Return: /
        //*****************************************************************************************************
        public MDISmartConfigMain(string FileName)
        {
            DoFileFormatRegistration();
            Program.LangSys.Initialize(this);
            InitializeComponent();
            CommonConstructorInit();
            OpenDoc(FileName);
        }

        private void CommonConstructorInit()
        {
            this.Text = APP_TITLE;
            this.Icon = CommonLib.Resources.AppIcon;
            string strAppDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetModules()[0].FullyQualifiedName);
            m_strIniFilePath = PathTranslator.LinuxVsWindowsPathUse(strAppDir + @"\" + Cste.STR_FORMPOSINI_FILENAME);
            m_IniFile.Load(m_strIniFilePath);
            m_DataForm = new DataForm();
            m_DesignForm = new DesignerForm();
            m_FrameForm = new FrameForm();
            m_ProgForm = new ProgramForm();
            m_DataForm.MdiParent = this;
            m_DesignForm.MdiParent = this;
            m_FrameForm.MdiParent = this;
            m_ProgForm.MdiParent = this;
            m_mruStripMenu = new MruStripMenuInline(this.m_fileMenu, this.m_MruFiles, new MruStripMenu.ClickedHandler(OnMruFile), m_strIniFilePath);
            UpdateFileCommand(null, null);
        }
        #endregion

        #region menu edition
        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        private void CutToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        private void ToolBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_toolStrip.Visible = m_toolBarToolStripMenuItem.Checked;
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        private void StatusBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            statusStrip.Visible = m_statusBarToolStripMenuItem.Checked;
        }
        #endregion

        #region réarangement des fenêtres
        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        private void CascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        private void TileVerticleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        private void TileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        private void ArrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }
        #endregion

        #region menu File
        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        private void OnNewMenuItemClick(object sender, EventArgs e)
        {
            if (m_Document == null)
            {
                m_Document = new BTDoc(Program.TypeApp);
                OpenDocument(m_Document);
                // on ne donne pas de nom au document, comme ca on peux savoir qu'il n'a jamais été sauvé
                m_strDocumentName = "Untitled.scf";
                UpdateTitle();
            }
            else
            {
                if (m_Document.Modified)
                {
                    DialogResult res = MessageBox.Show("File Have been modified\nDo you want to save it?", "Warning",
                                                            MessageBoxButtons.YesNoCancel);
                    if (res == DialogResult.Yes)
                    {
                        DoSaveDocument();
                    }
                    if (res == DialogResult.Cancel)
                    {
                        return;
                    }
                    this.CloseDoc();

                    m_Document = new BTDoc(Program.TypeApp);
                    OpenDocument(m_Document);
                    // on ne donne pas de nom au document, comme ca on peux savoir qu'il n'a jamais été sauvé
                    m_strDocumentName = "Untitled.scf";
                    UpdateTitle();
                }
                else
                {
                    this.CloseDoc();
                    m_Document = new BTDoc(Program.TypeApp);
                    OpenDocument(m_Document);
                    // on ne donne pas de nom au document, comme ca on peux savoir qu'il n'a jamais été sauvé
                    m_strDocumentName = "Untitled.scf";
                    UpdateTitle();
                }
            }
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        private void OpenFile(object sender, EventArgs e)
        {
            if (m_Document != null && m_Document.Modified)
            {
                DialogResult res = MessageBox.Show("File Have been modified\nDo you want to save it?", "Warning",
                                                    MessageBoxButtons.YesNoCancel);
                if (res == DialogResult.Yes)
                {
                    DoSaveDocument();
                }
                if (res == DialogResult.Cancel)
                {
                    return;
                }
                this.CloseDoc();
            } 
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
        private void OnSaveAsMenuItemClick(object sender, EventArgs e)
        {
            OnSaveAsClick();
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        private void OnExitMenuItemClick(object sender, EventArgs e)
        {
            if (m_Document != null)
            {
                if (m_Document.Modified)
                {
                    DialogResult res = MessageBox.Show("File Have been modified\nDo you want to save it?", "Warning",
                                                            MessageBoxButtons.YesNoCancel);
                    if (res == DialogResult.Yes)
                    {
                        DoSaveDocument();
                    }
                    if (res == DialogResult.Cancel)
                    {
                        return;
                    }
                }
                this.CloseDoc();
            }
            this.Close();
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        private void OnSaveMenuItemClick(object sender, EventArgs e)
        {
            DoSaveDocument();
            UpdateTitle();
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        private void DoSaveDocument()
        {
            if (m_Document != null)
            {
                if (string.IsNullOrEmpty(m_Document.FileName))
                    OnSaveAsClick();
                else
                {
                    m_Document.WriteConfigDocument(true);
                    m_Document.Modified = false;
                }
            }
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        private void OnSaveAsClick()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "SmartApp File (*.saf)|*.saf";
            saveFileDialog.InitialDirectory = Application.StartupPath;
            DialogResult dlgRes = saveFileDialog.ShowDialog();
            if (dlgRes == DialogResult.OK)
            {
                string strFileFullName = saveFileDialog.FileName;
#if LINUX
                int idxOfLastAntiSlash = strFileFullName.LastIndexOf(@"/");
#else
                int idxOfLastAntiSlash = strFileFullName.LastIndexOf(@"\");
#endif
                string DossierFichier = strFileFullName.Substring(0, strFileFullName.Length - (strFileFullName.Length - idxOfLastAntiSlash));
                PathTranslator.BTDocPath = DossierFichier;
                m_Document.WriteConfigDocument(strFileFullName, true);
#if LINUX
                int lastindex = strFileFullName.LastIndexOf(@"/");
#else
                int lastindex = strFileFullName.LastIndexOf(@"\");
#endif
                m_strDocumentName = strFileFullName.Substring(lastindex + 1);
                this.m_mruStripMenu.AddFile(strFileFullName);
                m_Document.Modified = false;
            }
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************      
        private void m_MenuItemClose_Click(object sender, EventArgs e)
        {
            if (m_Document != null && m_Document.Modified)
            {
                DialogResult res = MessageBox.Show("File Have been modified\nDo you want to save it?", "Warning",
                                                    MessageBoxButtons.YesNoCancel);
                if (res == DialogResult.Yes)
                {
                    DoSaveDocument();
                }
                if (res == DialogResult.Cancel)
                {
                    return;
                }
            }
            this.CloseDoc();
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

        #region fonction d'ouverture sauvegarde et fermeture du document
        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        private bool OpenDoc(string strFullFileName)
        {
            m_Document = new BTDoc(Program.TypeApp);
            //m_Document.UpdateDocumentFrame += new NeedRefreshHMI(OnNeedUpdateHMI);
            //m_Document.OnDocumentModified += new DocumentModifiedEvent(UpdateModifiedFlag);
            if (m_Document.ReadConfigDocument(strFullFileName, Program.TypeApp, Program.DllGest))
            {
                if (OpenDocument(m_Document))
                {
#if LINUX
                    int idxOfLastAntiSlash = strFullFileName.LastIndexOf(@"/");
#else
                    int idxOfLastAntiSlash = strFullFileName.LastIndexOf(@"\");
#endif
                    string DossierFichier = strFullFileName.Substring(0, strFullFileName.Length - (strFullFileName.Length - idxOfLastAntiSlash));
                    PathTranslator.BTDocPath = DossierFichier;
#if LINUX
                    int lastindex = strFullFileName.LastIndexOf(@"/");
#else
                    int lastindex = strFullFileName.LastIndexOf(@"\");
#endif
                    m_strDocumentName = strFullFileName.Substring(lastindex+1);
                    UpdateTitle();
                    m_mruStripMenu.AddFile(strFullFileName);
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
            m_DataForm.Doc = Doc;
            m_DataForm.Initialize();
            m_DesignForm.Doc = Doc;
            m_DesignForm.Initialize();
            m_FrameForm.Doc = Doc;
            m_FrameForm.Initialize();
            m_ProgForm.Doc = Doc;
            m_ProgForm.Initialize();

            m_DesignForm.Show();
            m_DataForm.Show();
            m_FrameForm.Show();
            m_ProgForm.Show();
            for (int i = 0; i < this.MdiChildren.Length; i++)
            {
                string strFramePosvalues = m_IniFile.GetValue(MdiChildren[i].Name, "Position");
                if (!string.IsNullOrEmpty(strFramePosvalues))
                {
                    string[] TabPos = strFramePosvalues.Split(',');
                    Rectangle rcForm = new Rectangle(int.Parse(TabPos[0]),
                        int.Parse(TabPos[1]),
                        int.Parse(TabPos[2]),
                        int.Parse(TabPos[3]));
                    MdiChildren[i].Location = new Point(rcForm.X, rcForm.Y);
                    MdiChildren[i].Size = new Size(rcForm.Width, rcForm.Height);
                }
            }

            m_windowsMenu.Enabled = true;
            m_jumpTotCmdMenuItem.Enabled = true;
            m_MenuItemM3SLWiz.Enabled = true;
            m_MenuItemTCPMBWiz.Enabled = true;
            m_MenuItemZ2SLWiz.Enabled = true;
            m_Document.UpdateDocumentFrame += new NeedRefreshHMI(OnNeedUpdateHMI);
            m_Document.OnDocumentModified += new DocumentModifiedEvent(UpdateModifiedFlag);
            UpdateFileCommand(null, null);
            UpdateTitle();
            return true;
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        private bool CloseDoc()
        {
            m_strDocumentName = "";
            m_DataForm.Hide();
            m_DesignForm.Hide();
            m_FrameForm.Hide();
            m_ProgForm.Hide();
            m_windowsMenu.Enabled = false;
            m_jumpTotCmdMenuItem.Enabled = false;
            m_MenuItemM3SLWiz.Enabled = false;
            m_MenuItemZ2SLWiz.Enabled = false;
            m_MenuItemTCPMBWiz.Enabled = false;
            m_Document = null;
            UpdateFileCommand(null, null);
            return true;
        }
        #endregion

        #region Update divers
        //*****************************************************************************************************
        // Description: met a jour le titre de l'application en fonction du nom du document et de son état 
        // modifié ou non
        // Return: /
        //*****************************************************************************************************
        public void UpdateTitle()
        {
            string strTitle = APP_TITLE;
            strTitle += " - ";
            strTitle += m_strDocumentName;
            if (m_Document != null)
            {
                if (m_Document.Modified)
                {
                    strTitle += "*";
                }
            }
            if (this.InvokeRequired)
            {
                UpdateTitleDg dg = new UpdateTitleDg(SetTitle);
                this.Invoke(dg, strTitle);

            }
            else
            {
                this.Text = strTitle;
            }
        }


        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************      
        private void SetTitle(string str)
        {
            this.Text = str;
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************      
        protected void OnNeedUpdateHMI(MessNeedUpdate Mess)
        {
            if (this.InvokeRequired)
            {
                AsyncUpdateHMI AsyncCall = new AsyncUpdateHMI(AsyncUpdater);
                this.Invoke(AsyncCall, Mess);
            }
            else
            {
                AsyncUpdater(Mess);
            }
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************      
        protected void AsyncUpdater(MessNeedUpdate Mess)
        {
            if (Mess == null)
            {
                m_DataForm.Initialize();
                m_FrameForm.Initialize();
                m_DesignForm.Initialize();
                m_ProgForm.Initialize();
            }
            else
            {
                if (Mess.bUpdateDataForm)
                    m_DataForm.Initialize();

                if (Mess.bUpdateFrameForm)
                    m_FrameForm.Initialize();

                if (Mess.bUpdateProgramForm)
                    m_ProgForm.Initialize();

                if (Mess.bUpdateScreenForm)
                    m_DesignForm.Initialize();
            }
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************      
        private void UpdateFileCommand(object sender, EventArgs e)
        {
            if (m_Document == null)
            {
                m_saveToolStripMenuItem.Enabled = false;
                m_saveToolStripButton.Enabled = false;
                m_saveAsToolStripMenuItem.Enabled = false;
                m_MenuItemClose.Enabled = false;
            }
            else
            {
                m_saveToolStripMenuItem.Enabled = true;
                m_saveToolStripButton.Enabled = true;
                m_saveAsToolStripMenuItem.Enabled = true;
                m_MenuItemClose.Enabled = true;
            }
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************      
        protected void UpdateModifiedFlag()
        {
            UpdateTitle();
        }

        #endregion

        #region handler's devent
        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************      
        private void OnFormClosing(object sender, FormClosingEventArgs e)
        {
            if (m_Document != null && m_Document.Modified)
            {
                DialogResult res = MessageBox.Show("File Have been modified\nDo you want to save it?", "Warning",
                                                    MessageBoxButtons.YesNoCancel);
                if (res == DialogResult.Yes)
                {
                    DoSaveDocument();
                }
                else if (res == DialogResult.Cancel)
                {
                    e.Cancel = true;
                    return;
                }
            }
            for (int i = 0; i < this.MdiChildren.Length; i++)
            {
                string strValue = MdiChildren[i].Location.X.ToString() + ',' +
                    MdiChildren[i].Location.Y.ToString() + ',' +
                    MdiChildren[i].Size.Width.ToString() + ',' +
                    MdiChildren[i].Size.Height.ToString();
                m_IniFile.SetValue(MdiChildren[i].Name, "Position", strValue);
            }
            m_IniFile.Save(m_strIniFilePath);
            m_mruStripMenu.SaveToFile();

        }
        #endregion

        #region Base de Registre
        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************      
        private void DoFileFormatRegistration()
        {
            try
            {
                RegistryKey Extkey = Registry.ClassesRoot.OpenSubKey(".saf");
                if (Extkey == null)
                {
                    Extkey = Registry.ClassesRoot.CreateSubKey(".saf");
                }
                RegistryKey ShellKey = Extkey.OpenSubKey("shell");
                if (ShellKey == null)
                {
                    ShellKey = Extkey.CreateSubKey("shell");
                }
                RegistryKey AppKey = ShellKey.OpenSubKey("SmartApp");
                if (AppKey == null)
                {
                    AppKey = ShellKey.CreateSubKey("SmartApp");
                }
                RegistryKey CmdKey = AppKey.OpenSubKey("command");
                string existingCommandValue = "";
                if (CmdKey == null)
                {
                    CmdKey = AppKey.CreateSubKey("command");
                }
                else
                {
                    existingCommandValue = (string)CmdKey.GetValue("", "");
                }
                string value = Application.StartupPath + @"\SmartApp.exe" + " -Cfg \"%1\"";
                if (existingCommandValue != value)
                    CmdKey.DeleteValue("");

                CmdKey.SetValue("", value);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        #endregion

        #region menu ?
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutForm.ShowAbout();
        }
        #endregion

        #region Commandes du menu Tool
        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************      
        private void JumpToSmartCommandMenuItemClick(object sender, EventArgs e)
        {
            if (m_Document != null)
            {
                if (string.IsNullOrEmpty(m_Document.FileName))
                    OnSaveAsClick();
                else
                {
                    m_Document.WriteConfigDocument(false);
                    m_Document.Modified = false;

                    if (File.Exists(m_Document.FileName))
                    {
                        this.Hide();
			            Application.DoEvents();
                        System.Diagnostics.Process proc = new System.Diagnostics.Process();
                        string Arguments = "-Cmd \"" + m_Document.FileName + "\"";
                        proc.StartInfo = new System.Diagnostics.ProcessStartInfo(Application.ExecutablePath, Arguments);
                        proc.StartInfo.UseShellExecute = true;

                        if (proc.Start())
                        {
                            proc.WaitForExit();
                        }
                        else
                        {
                            MessageBox.Show(Program.LangSys.C("Application fail on startup."), Program.LangSys.C("Error"));
                        }
                        this.Show();
                    }
                }
            }
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************      
        private void m_MenuItemZ2SLWiz_Click(object sender, EventArgs e)
        {
            if (m_Document != null)
            {
                WizardSLFormZ2 wiz = new WizardSLFormZ2();
                wiz.m_Document = m_Document;
                if (wiz.ShowDialog() == DialogResult.OK)
                {
                    this.OnNeedUpdateHMI(null);
                    m_Document.Modified = true;
                }
            }
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************      
        private void m_MenuItemTCPMBWiz_Click(object sender, EventArgs e)
        {
            if (m_Document != null)
            {
                WizardTcpModbusForm wiz = new WizardTcpModbusForm();
                wiz.m_Document = m_Document;
                if (wiz.ShowDialog() == DialogResult.OK)
                {
                    this.OnNeedUpdateHMI(null);
                    m_Document.Modified = true;
                }
            }
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************      
        private void OnMenuItemM3SLWizClick(object sender, EventArgs e)
        {
            if (m_Document != null)
            {
                WizardSLFormM3 wiz = new WizardSLFormM3();
                wiz.m_Document = m_Document;
                if (wiz.ShowDialog() == DialogResult.OK)
                {
                    this.OnNeedUpdateHMI(null);
                    m_Document.Modified = true;
                }
            }
        }
        #endregion

        private void pluginsVersionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PluginsVersionsForm plVer = new PluginsVersionsForm();
            plVer.ShowDialog();
        }

        private void m_tsItemPref_Click(object sender, EventArgs e)
        {
            PreferencesForm prfForm = new PreferencesForm();
            prfForm.SelectedLang = SmartApp.Properties.Settings.Default.Lang;
            if (prfForm.ShowDialog() == DialogResult.OK)
            {
                if (prfForm.SelectedLang != SmartApp.Properties.Settings.Default.Lang)
                {
                    //MessageBox.Show(Program.LangSys.C("Please restart the application in order apply language change"), Program.LangSys.C("Information"), MessageBoxButtons.OK);
                    SmartApp.Properties.Settings.Default.Lang = prfForm.SelectedLang;
                    SmartApp.Properties.Settings.Default.Save();
                    Lang.LangSys.ChangeLangage(prfForm.SelectedLang);
                    Program.ChangePluginLang(prfForm.SelectedLang);
                    Program.LangSys.ChangeLangage(prfForm.SelectedLang);
                }
            }
        }
    }
}

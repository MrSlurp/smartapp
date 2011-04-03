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
using SmartApp.Wizards;
using System.Reflection;
using CommonLib;

namespace SmartApp.Ihm
{
    /// <summary>
    /// fenêtre principale de SmartConfig
    /// </summary>
    public partial class MDISmartConfigMain : Form
    {
        private const string APP_TITLE = "SmartConfig";
        protected delegate void UpdateTitleDg(string str);

        #region données membres
        protected TraceConsole m_TraceConsole;
        protected DataForm m_DataForm;
        protected DesignerForm m_DesignForm;
        protected FrameForm m_FrameForm;
        protected ProgramForm m_ProgForm;
        protected BTDoc m_Document = null;
        protected FormsOptions m_FrmOpt = new FormsOptions(); 
        protected string m_strDocumentName = "";
        
        private OpenFileDialog m_openFileDiag = new OpenFileDialog(); 

        protected MruStripMenuInline m_mruStripMenu;
        #endregion

        #region constructeurs
        //*****************************************************************************************************
        // Description: constructeur par défaut
        // Return: /
        //*****************************************************************************************************
        public MDISmartConfigMain()
        {
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
            Program.LangSys.Initialize(this);
            InitializeComponent();
            CommonConstructorInit();
            OpenDoc(FileName);
        }

        /// <summary>
        /// 
        /// </summary>
        private void CommonConstructorInit()
        {
            this.Text = APP_TITLE;
            this.Icon = CommonLib.Resources.AppIcon;
            string strAppDir = Application.StartupPath;
            string strIniFilePath = PathTranslator.LinuxVsWindowsPathUse(strAppDir + @"\" + Cste.STR_FORMPOSINI_FILENAME);
            m_FrmOpt.Load(strIniFilePath);
            m_DataForm = new DataForm();
            m_DesignForm = new DesignerForm();
            m_FrameForm = new FrameForm();
            m_ProgForm = new ProgramForm();
            m_DataForm.MdiParent = this;
            m_DesignForm.MdiParent = this;
            m_FrameForm.MdiParent = this;
            m_ProgForm.MdiParent = this;
            m_mruStripMenu = new MruStripMenuInline(this.m_fileMenu, this.m_MruFiles, new MruStripMenu.ClickedHandler(OnMruFile), strIniFilePath);
            if (LaunchArgParser.DevMode)
            {
                m_tsMenuLogConfig.Visible = true;
                m_tsMenuOpenDebugConsole.Visible = true;
            }
            m_openFileDiag.Filter = Program.LangSys.C("SmartApp File (*.saf)|*.saf");
            m_openFileDiag.InitialDirectory = Application.StartupPath;
        

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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TileVerticleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ArrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }
        #endregion

        #region menu File
        /// <summary>
        /// affiche une message box indiquant que le fichier a été modifié
        /// et propose le choix de sauvegarder/annuler/ignorer
        /// </summary>
        /// <returns></returns>
        private DialogResult ShowFileModifiedMessagebox()
        {
            return MessageBox.Show(Program.LangSys.C("File Have been modified") 
                                                   + "\n" + 
                                                   Program.LangSys.C("Do you want to save it?"), 
                                                   Program.LangSys.C("Warning"),
                                                   MessageBoxButtons.YesNoCancel,
                                                   MessageBoxIcon.Question);
        }

        /// <summary>
        /// handler du menu New File
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                    DialogResult res = ShowFileModifiedMessagebox();
                                                            
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

        /// <summary>
        /// handler du menu Open
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenFile(object sender, EventArgs e)
        {
            if (m_Document != null && m_Document.Modified)
            {
                DialogResult res = ShowFileModifiedMessagebox();
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
            DialogResult dlgRes = m_openFileDiag.ShowDialog();
            if (dlgRes == DialogResult.OK)
            {
                string strFileFullName = m_openFileDiag.FileName;
                if (!OpenDoc(strFileFullName))
                {
                    MessageBox.Show(Program.LangSys.C("Error while reading file. File is corrupted"), Program.LangSys.C("Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.CloseDoc();
                }
            }
        }

        /// <summary>
        /// handler du menu save as
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnSaveAsMenuItemClick(object sender, EventArgs e)
        {
            OnSaveAsClick();
        }

        /// <summary>
        /// handler du menu exit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnExitMenuItemClick(object sender, EventArgs e)
        {
            if (m_Document != null)
            {
                if (m_Document.Modified)
                {
                    DialogResult res = ShowFileModifiedMessagebox();
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

        /// <summary>
        /// handler du menu save
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnSaveMenuItemClick(object sender, EventArgs e)
        {
            DoSaveDocument();
            UpdateTitle();
        }

        /// <summary>
        /// effectue la sauvegarde du document en cours d'édition
        /// </summary>
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

        /// <summary>
        /// gestion du SaveAs
        /// </summary>
        private void OnSaveAsClick()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = Program.LangSys.C("SmartApp File (*.saf)|*.saf");
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

        /// <summary>
        /// handler du menu de fermeture du fichier
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void m_MenuItemClose_Click(object sender, EventArgs e)
        {
            if (m_Document != null && m_Document.Modified)
            {
                DialogResult res = ShowFileModifiedMessagebox();
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

        /// <summary>
        /// gestion des most recent used
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
                OpenDoc(filename);
                m_mruStripMenu.SetFirstFile(number);
            }
        }

        /// <summary>
        /// affiche le panel de préférence de l'application
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void m_tsItemPref_Click(object sender, EventArgs e)
        {
            PreferencesForm prfForm = new PreferencesForm();
            prfForm.SelectedLang = SmartApp.Properties.Settings.Default.Lang;
            if (prfForm.ShowDialog() == DialogResult.OK)
            {
                if (prfForm.SelectedLang != SmartApp.Properties.Settings.Default.Lang)
                {
                    SmartApp.Properties.Settings.Default.Lang = prfForm.SelectedLang;
                    SmartApp.Properties.Settings.Default.Save();
                    OnNeedUpdateHMI(null);
                    //MessageBox.Show(Program.LangSys.C("Please restart the application in order apply language change"), Program.LangSys.C("Informations"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Lang.LangSys.ChangeLangage(prfForm.SelectedLang);
                    Program.ChangePluginLang(prfForm.SelectedLang);
                    Program.LangSys.ChangeLangage(prfForm.SelectedLang);
                }
            }
        }

        /// <summary>
        /// affiche le panel de configuration des logs
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void m_tsMenuLogConfig_Click(object sender, EventArgs e)
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
        /// ouvre la console d'affichage des logs
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void m_tsMenuOpenDebugConsole_Click(object sender, EventArgs e)
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
                MdiChildren[i].Size = m_FrmOpt.GetFormSize(MdiChildren[i]);
                FormWindowState state = m_FrmOpt.GetFormState(MdiChildren[i]); 
                MdiChildren[i].WindowState = state;
                if (state != FormWindowState.Minimized) 
                    MdiChildren[i].Location = m_FrmOpt.GetFormPos(MdiChildren[i]);
            }

            m_windowsMenu.Enabled = true;
            m_jumpTotCmdMenuItem.Enabled = true;
            m_MenuItemM3SLWiz.Enabled = true;
            m_MenuItemTCPMBWiz.Enabled = true;
            m_MenuItemZ2SLWiz.Enabled = true;
            tsmi3SLProjectWizard.Enabled = false;
            tsmi3XN05ProjectWizard.Enabled = false;
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
            tsmi3SLProjectWizard.Enabled = true;
            tsmi3XN05ProjectWizard.Enabled = true;
            m_Document = null;
            SaveFormsPos();
            UpdateFileCommand(null, null);
            return true;
        }
        #endregion

        #region Update divers
        /// <summary>
        /// Met a jour le titre de l'application avec le nom du document courant
        /// (si il y a) et le flag de modification
        /// </summary>
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

        /// <summary>
        /// fonction qui n'a lieu d'être qu'en cas d'appel asynchrone
        /// </summary>
        /// <param name="str"></param>
        private void SetTitle(string str)
        {
            this.Text = str;
        }

        /// <summary>
        /// appelé lorsqu'une opération nécessite la mise a jour de l'intégralité des IHM
        /// (modification ayant un impacte dans toutes les formes)
        /// </summary>
        /// <param name="Mess"></param>
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

        /// <summary>
        /// met a jour l'état des différentes formes de l'appplication
        /// </summary>
        /// <param name="Mess"></param>
        protected void AsyncUpdater(MessNeedUpdate Mess)
        {
            if (Mess == null)
            {
                if (m_DataForm != null)
                    m_DataForm.Initialize();

                if (m_FrameForm != null)
                    m_FrameForm.Initialize();

                if (m_DesignForm != null)
                    m_DesignForm.Initialize();

                if (m_ProgForm != null)
                    m_ProgForm.Initialize();
            }
            else
            {
                if (Mess.bUpdateDataForm && m_DataForm != null)
                    m_DataForm.Initialize();

                if (Mess.bUpdateFrameForm && m_FrameForm != null)
                    m_FrameForm.Initialize();

                if (Mess.bUpdateProgramForm && m_ProgForm != null)
                    m_ProgForm.Initialize();

                if (Mess.bUpdateScreenForm && m_DesignForm != null)
                    m_DesignForm.Initialize();
            }
        }

        /// <summary>
        /// met a jour l'état des commandes du menu fichier
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// met a jour le titre en fonction de l'état de modification du document
        /// </summary>
        protected void UpdateModifiedFlag()
        {
            UpdateTitle();
        }

        /// <summary>
        /// sauvegarde la position des différents MdiChilds
        /// </summary>
        private void SaveFormsPos()
        {
            for (int i = 0; i < this.MdiChildren.Length; i++)
            {
                m_FrmOpt.SetFormPos(MdiChildren[i]);
                m_FrmOpt.SetFormSize(MdiChildren[i]);
                m_FrmOpt.SetFormState(MdiChildren[i]);
            }
            m_FrmOpt.Save();
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
                DialogResult res = ShowFileModifiedMessagebox();
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
            SaveFormsPos();
            m_mruStripMenu.SaveToFile();

        }
        #endregion

        #region menu ?
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutForm.ShowAbout();
        }

        /// <summary>
        /// affiche le panel de version des plugins
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pluginsVersionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PluginsVersionsForm plVer = new PluginsVersionsForm();
            plVer.ShowDialog();
        }
        #endregion

        #region toolbar de navigation
        /// <summary>
        /// met la fenêtre de configuration des écran au premier plan
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbtn_gotoScreen_Click(object sender, EventArgs e)
        {
            if (this.ActiveMdiChild != m_DesignForm)
            {
                if (m_DesignForm.WindowState == FormWindowState.Minimized)
                    m_DesignForm.WindowState =
                        this.ActiveMdiChild.WindowState == FormWindowState.Maximized ?
                        FormWindowState.Maximized : FormWindowState.Normal;

                m_DesignForm.BringToFront();
            }
        }

        /// <summary>
        /// met la fenêtre de programmation au premier plan
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbtn_gotoProgram_Click(object sender, EventArgs e)
        {
            if (this.ActiveMdiChild != m_ProgForm)
            {
                if (m_ProgForm.WindowState == FormWindowState.Minimized)
                    m_ProgForm.WindowState = 
                        this.ActiveMdiChild.WindowState == FormWindowState.Maximized?
                        FormWindowState.Maximized : FormWindowState.Normal;

                m_ProgForm.BringToFront();
            }
        }

        /// <summary>
        /// met la fenêtre de configuration des donnée au premier plan
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbtn_gotoData_Click(object sender, EventArgs e)
        {
            if (this.ActiveMdiChild != m_DataForm)
            {
                if (m_DataForm.WindowState == FormWindowState.Minimized)
                    m_DataForm.WindowState =
                        this.ActiveMdiChild.WindowState == FormWindowState.Maximized ?
                        FormWindowState.Maximized : FormWindowState.Normal;

                m_DataForm.BringToFront();
            }
        }

        /// <summary>
        /// met la fenêtre de configuration des trames au premier plan
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbtn_gotoFrame_Click(object sender, EventArgs e)
        {
            if (this.ActiveMdiChild != m_FrameForm)
            {
                if (m_FrameForm.WindowState == FormWindowState.Minimized)
                    m_FrameForm.WindowState =
                        this.ActiveMdiChild.WindowState == FormWindowState.Maximized ?
                        FormWindowState.Maximized : FormWindowState.Normal;

                m_FrameForm.BringToFront();
            }
        }
        #endregion

        #region Menu Tools
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void JumpToSmartCommandMenuItemClick(object sender, EventArgs e)
        {
            if (m_Document != null)
            {
                // si le document n'est pas sauvegardé, on demande de le faire
                if (string.IsNullOrEmpty(m_Document.FileName))
                    OnSaveAsClick();

                // on vérifie si il a été sauvegardé
                if (!string.IsNullOrEmpty(m_Document.FileName))
                {
                    m_Document.WriteConfigDocument(false);
                    m_Document.Modified = false;

                    if (File.Exists(m_Document.FileName))
                    {
                        this.Hide();
                        Application.DoEvents();
                        System.Diagnostics.Process proc = new System.Diagnostics.Process();
                        string Arguments = "-Cmd \"" + m_Document.FileName + "\"";
                        if (LaunchArgParser.DevMode)
                            Arguments += " -dev";
                        proc.StartInfo = new System.Diagnostics.ProcessStartInfo(Application.ExecutablePath, Arguments);
                        proc.StartInfo.UseShellExecute = true;

                        if (proc.Start())
                        {
                            proc.WaitForExit();
                        }
                        else
                        {
                            MessageBox.Show(Program.LangSys.C("Application fail on startup."), Program.LangSys.C("Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        this.Show();
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// lance le wizard de projet SL M3
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmi3SLProjectWizard_Click(object sender, EventArgs e)
        {
            if (m_Document == null)
            {
                WizardM3Z2ProjectForm wiz = new WizardM3Z2ProjectForm(new SLWizardConfigData());
                if (wiz.ShowDialog() == DialogResult.OK)
                {
                    OnNewMenuItemClick(null, null);
                    wiz.CreateAllFromWizardData(new SLM3ProjectCreator(m_Document));
                    this.OnNeedUpdateHMI(null);
                    m_Document.Modified = true;
                }
            }
        }

        /// <summary>
        /// lance le wizard de projet ETH M3
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmi3XN05ProjectWizard_Click(object sender, EventArgs e)
        {
            if (m_Document == null)
            {
                WizardM3Z2ProjectForm wiz = new WizardM3Z2ProjectForm(new M3XN05WizardConfigData());
                if (wiz.ShowDialog() == DialogResult.OK)
                {
                    OnNewMenuItemClick(null, null);
                    wiz.CreateAllFromWizardData(new ETHM3ProjectCreator(m_Document));
                    this.OnNeedUpdateHMI(null);
                    m_Document.Modified = true;
                }
            }

        }
        #endregion
    }
}

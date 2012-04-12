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
using SmartApp.Ihm.Designer;
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

        private DragItemPanel m_panelToolDragItem;
        #region données membres
        protected TraceConsole m_TraceConsole;
        protected List<DesignerForm> m_ListDesignForm = new List<DesignerForm>();
        protected SolutionGest m_GestSolution;
        protected FormsOptions m_FrmOpt = new FormsOptions(); 
        
        protected MruStripMenuInline m_mruStripMenu;
        #endregion

        #region constructeurs
        /// <summary>
        /// 
        /// </summary>
        public MDISmartConfigMain()
        {
            Program.LangSys.Initialize(this);
            InitializeComponent();
            CommonConstructorInit();
#if !_SMARTAPP_MULTICO
            tsbtnConfigCom.Visible = false;
#endif

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="FileName"></param>
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
            if (Program.DllGest != null)
            {
                m_GestSolution = new SolutionGest(Program.TypeApp, Program.DllGest);
                m_GestSolution.OnDocScreenEdit += new SolutionGest.DocumentScreenEditHandler(GestSolution_OnDocScreenEdit);
            }

            this.Icon = CommonLib.Resources.AppIcon;
            string strAppDir = Application.StartupPath;
            string strIniFilePath = PathTranslator.LinuxVsWindowsPathUse(strAppDir + @"\" + Cste.STR_FORMPOSINI_FILENAME);
            m_FrmOpt.Load(strIniFilePath);
            m_mruStripMenu = new MruStripMenuInline(this.m_fileMenu, this.m_MruFiles, new MruStripMenu.ClickedHandler(OnMruFile), strIniFilePath);
            if (LaunchArgParser.DevMode)
            {
                m_tsMenuLogConfig.Visible = true;
                m_tsMenuOpenDebugConsole.Visible = true;
            }
            CentralizedFileDlg.InitPrjFileDialog(Application.StartupPath);
            CentralizedFileDlg.InitSolFileDialog(Application.StartupPath);
            this.solutionTreeView.SolutionGest = m_GestSolution;
            UpdateFileCommand(null, null);

            m_panelToolDragItem = new DragItemPanel();
            // 
            // m_panelToolDragItem
            // 
            m_panelToolDragItem.AutoScroll = true;
            m_panelToolDragItem.BackColor = System.Drawing.Color.Transparent;
            m_panelToolDragItem.Dock = System.Windows.Forms.DockStyle.Fill;
            m_panelToolDragItem.Name = "m_panelToolDragItem";
            panel1.Controls.Add(m_panelToolDragItem);
        }

        void GestSolution_OnDocScreenEdit(string screenName, BTDoc document)
        {
            BTScreen scr = document.GestScreen.GetFromSymbol(screenName) as BTScreen;
            foreach (DesignerForm form in this.MdiChildren)
            {
                if (form.Doc == document && form.CurrentScreen == scr)
                {
                    form.BringToFront();
                    return;
                }
            }
            DesignerForm DesignForm = new DesignerForm();
            m_ListDesignForm.Add(DesignForm);
            DesignForm.FormClosed += new FormClosedEventHandler(DesignFormClosed);
            DesignForm.MdiParent = this;
            DesignForm.Doc = document;
            DesignForm.CurrentScreen = scr ;
            DesignForm.Show();
        }

        void DesignFormClosed(object sender, FormClosedEventArgs e)
        {
            DesignerForm frm = sender as DesignerForm;
            if (frm != null)
            {
                m_ListDesignForm.Remove(frm);
                frm.Dispose();
            }
            
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
            return MessageBox.Show(Program.LangSys.C("File have been modified") 
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
            /*
            if (m_Document == null)
            {
                m_Document = new BTDoc(Program.TypeApp);
                OpenDocument(m_Document);
                // on ne donne pas de nom au document, comme ca on peux savoir qu'il n'a jamais été sauvé
                m_strDocumentName = "Untitled.saf";
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
                    this.CloseSolution();

                    m_Document = new BTDoc(Program.TypeApp);
                    OpenDocument(m_Document);
                    // on ne donne pas de nom au document, comme ca on peux savoir qu'il n'a jamais été sauvé
                    m_strDocumentName = "Untitled.saf";
                    UpdateTitle();
                }
                else
                {
                    this.CloseSolution();
                    m_Document = new BTDoc(Program.TypeApp);
                    OpenDocument(m_Document);
                    // on ne donne pas de nom au document, comme ca on peux savoir qu'il n'a jamais été sauvé
                    m_strDocumentName = "Untitled.saf";
                    UpdateTitle();
                }
            }
             * */
        }

        /// <summary>
        /// handler du menu Open
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenFile(object sender, EventArgs e)
        {
            
            if (m_GestSolution != null && m_GestSolution.HaveModifiedDocument)
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
                this.CloseSolution();
            }
            DialogResult dlgRes = CentralizedFileDlg.ShowOpenSolFileDilaog();
            if (dlgRes == DialogResult.OK)
            {
                if (!m_GestSolution.ReadInSolution(CentralizedFileDlg.SolOpenFileName))
                {
                    MessageBox.Show(Program.LangSys.C("Error while reading file. File is corrupted"),
                                    Program.LangSys.C("Error"),
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                    this.CloseSolution();
                }
                else
                    m_mruStripMenu.AddFile(CentralizedFileDlg.SolOpenFileName);

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
            if (m_GestSolution != null)
            {
                if (m_GestSolution.HaveModifiedDocument)
                {
                    DialogResult res = ShowFileModifiedMessagebox();
                    if (res == DialogResult.Yes)
                    {
                        m_GestSolution.SaveAllDocumentsAndSolution();
                    }
                    if (res == DialogResult.Cancel)
                    {
                        return;
                    }
                }
                this.CloseSolution();
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

            if (m_GestSolution != null)
            {
                if (string.IsNullOrEmpty(m_GestSolution.FilePath))
                    OnSaveAsClick();
                else
                {
                    m_GestSolution.SaveAllDocumentsAndSolution();
                    m_GestSolution.HaveModifiedDocument = false;
                }
            }
        }

        /// <summary>
        /// gestion du SaveAs
        /// </summary>
        private void OnSaveAsClick()
        {
            DialogResult dlgRes = CentralizedFileDlg.ShowSavePrjFileDilaog();
            if (dlgRes == DialogResult.OK)
            {
                string strFileFullName = CentralizedFileDlg.PrjSaveFileName;
#if LINUX
                int idxOfLastAntiSlash = strFileFullName.LastIndexOf(@"/");
#else
                int idxOfLastAntiSlash = strFileFullName.LastIndexOf(@"\");
#endif
                string DossierFichier = Path.GetDirectoryName(strFileFullName);
                CentralizedFileDlg.InitImgFileDialog(DossierFichier);
                CentralizedFileDlg.InitPrjFileDialog(DossierFichier);
                CentralizedFileDlg.InitSolFileDialog(DossierFichier);
                m_GestSolution.WriteOutSolution(strFileFullName);
                m_GestSolution.SaveAllDocumentsAndSolution();
                this.m_mruStripMenu.AddFile(strFileFullName);
                m_GestSolution.HaveModifiedDocument = false;
                //#if LINUX
//                int lastindex = strFileFullName.LastIndexOf(@"/");
//#else
//                int lastindex = strFileFullName.LastIndexOf(@"\");
//#endif
            }
        }

        /// <summary>
        /// handler du menu de fermeture du fichier
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void m_MenuItemClose_Click(object sender, EventArgs e)
        {
            if (m_GestSolution != null && m_GestSolution.HaveModifiedDocument)
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
            this.CloseSolution();
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
            m_GestSolution.ReadInSolution(strFullFileName);
            UpdateTitle();
            m_mruStripMenu.AddFile(strFullFileName);
            m_GestSolution.OpenDocument(strFullFileName);
            //solutionTreeView.AddDocument(m_Document);
            //m_GestSolution.OpenDocument(strFullFileName);
            //m_Document = new BTDoc(Program.TypeApp);
            //m_Document.UpdateDocumentFrame += new NeedRefreshHMI(OnNeedUpdateHMI);
            //m_Document.OnDocumentModified += new DocumentModifiedEvent(UpdateModifiedFlag);
            
            /*
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
                    CentralizedFileDlg.InitImgFileDialog(DossierFichier);
                    CentralizedFileDlg.InitPrjFileDialog(DossierFichier);
#if LINUX
                    int lastindex = strFullFileName.LastIndexOf(@"/");
#else
                    int lastindex = strFullFileName.LastIndexOf(@"\");
#endif
                    m_strDocumentName = strFullFileName.Substring(lastindex+1);
                    return true;
                }
                else
                    return false;
            }
            else
             * */
            return false;
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        private bool OpenDocument(BTDoc Doc)
        {
            /*
            m_DesignForm.Doc = Doc;
            m_DesignForm.Initialize();

            m_DesignForm.Show();
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
            tsmiM3SLProjectWizard.Enabled = false;
            tsmiM3XN05ProjectWizard.Enabled = false;
            tsmiZ2SR3NETProjectWizard.Enabled = false;
            tsmiZ2SLProjectWizard.Enabled = false;
            m_Document.UpdateDocumentFrame += new NeedRefreshHMI(OnNeedUpdateHMI);
            m_Document.OnDocumentModified += new DocumentModifiedEvent(UpdateModifiedFlag);
            UpdateFileCommand(null, null);
            UpdateTitle();
             * */
            return true;
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        private bool CloseSolution()
        {
            //m_strDocumentName = "";
            //m_DesignForm.Hide();
            //m_windowsMenu.Enabled = false;
            //m_jumpTotCmdMenuItem.Enabled = false;
            //m_MenuItemM3SLWiz.Enabled = false;
            //m_MenuItemZ2SLWiz.Enabled = false;
            //m_MenuItemTCPMBWiz.Enabled = false;
            //tsmiM3SLProjectWizard.Enabled = true;
            //tsmiM3XN05ProjectWizard.Enabled = true;
            //tsmiZ2SR3NETProjectWizard.Enabled = true;
            //tsmiZ2SLProjectWizard.Enabled = true;
            //m_Document = null;
            //SaveFormsPos();
            //UpdateFileCommand(null, null);
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
            if (m_GestSolution != null)
            {
                if (m_GestSolution.HaveModifiedDocument)
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
                // TODO
                //if (m_DesignForm != null)
                //    m_DesignForm.Initialize();
            }
            else
            {
                //if (Mess.bUpdateScreenForm && m_DesignForm != null)
                //    m_DesignForm.Initialize();
            }
        }

        /// <summary>
        /// met a jour l'état des commandes du menu fichier
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateFileCommand(object sender, EventArgs e)
        {
            /*
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
            }*/
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
            
            if (m_GestSolution != null && m_GestSolution.HaveModifiedDocument)
            {
                DialogResult res = ShowFileModifiedMessagebox();
                if (res == DialogResult.Yes)
                {
                    m_GestSolution.SaveAllDocumentsAndSolution();
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

        #region Menu Tools
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void JumpToSmartCommandMenuItemClick(object sender, EventArgs e)
        {
            /*
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
            }*/
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void m_MenuItemZ2SLWiz_Click(object sender, EventArgs e)
        {
            // TODO
            /*
            if (m_Document != null)
            {
                WizardSLFormZ2 wiz = new WizardSLFormZ2();
                wiz.m_Document = m_Document;
                if (wiz.ShowDialog() == DialogResult.OK)
                {
                    this.OnNeedUpdateHMI(null);
                    m_Document.Modified = true;
                }
            }*/
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void m_MenuItemTCPMBWiz_Click(object sender, EventArgs e)
        {
            // TODO
            /*
            if (m_Document != null)
            {
                WizardTcpModbusForm wiz = new WizardTcpModbusForm();
                wiz.m_Document = m_Document;
                if (wiz.ShowDialog() == DialogResult.OK)
                {
                    this.OnNeedUpdateHMI(null);
                    m_Document.Modified = true;
                }
            }*/
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnMenuItemM3SLWizClick(object sender, EventArgs e)
        {
            // TODO
            /*
            if (m_Document != null)
            {
                WizardSLFormM3 wiz = new WizardSLFormM3();
                wiz.m_Document = m_Document;
                if (wiz.ShowDialog() == DialogResult.OK)
                {
                    this.OnNeedUpdateHMI(null);
                    m_Document.Modified = true;
                }
            }*/
        }

        /// <summary>
        /// lance le wizard de projet SL M3
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiM3SLProjectWizard_Click(object sender, EventArgs e)
        {
            // TODO
            /*
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
            }*/
        }

        /// <summary>
        /// lance le wizard de projet SL Z2
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiZ2SLProjectWizard_Click(object sender, EventArgs e)
        {
            // TODO
            /*
            if (m_Document == null)
            {
                WizardM3Z2ProjectForm wiz = new WizardM3Z2ProjectForm(new SLZ2WizardConfigData());
                if (wiz.ShowDialog() == DialogResult.OK)
                {
                    OnNewMenuItemClick(null, null);
                    wiz.CreateAllFromWizardData(new SLZ2ProjectCreator(m_Document));
                    this.OnNeedUpdateHMI(null);
                    m_Document.Modified = true;
                }
            }*/
        }

        /// <summary>
        /// lance le wizard de projet ETH M3
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiM3XN05ProjectWizard_Click(object sender, EventArgs e)
        {
            // TODO
            /*
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
            */
        }

        /// <summary>
        /// lance le wizard de projet ETH Z2
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiZ2SR3NETProjectWizard_Click(object sender, EventArgs e)
        {
            // TODO
            /*
            if (m_Document == null)
            {
                WizardM3Z2ProjectForm wiz = new WizardM3Z2ProjectForm(new Z2SR3NETWizardConfigData());
                if (wiz.ShowDialog() == DialogResult.OK)
                {
                    OnNewMenuItemClick(null, null);
                    wiz.CreateAllFromWizardData(new ETHZ2ProjectCreator(m_Document));
                    this.OnNeedUpdateHMI(null);
                    m_Document.Modified = true;
                }
            }*/

        }

        #endregion

        private void tsbtnConfigCom_Click(object sender, EventArgs e)
        {
            // TODO
            /*
            if (m_Document != null)
            {
                CommConfiguration commCfgPage = new CommConfiguration();
                commCfgPage.AllowRowSelect = true;
                commCfgPage.CurComParam = m_Document.m_Comm.CommParam;
                commCfgPage.CurTypeCom = m_Document.m_Comm.CommType;
                DialogResult dlgRes = commCfgPage.ShowDialog();
                if (dlgRes == DialogResult.OK)
                {
                    m_Document.m_Comm.SetCommTypeAndParam(commCfgPage.CurTypeCom, commCfgPage.CurComParam);
                }
            }*/
        }

        private void btnHideShowSolution_Click(object sender, EventArgs e)
        {
            if (this.lblSolutionView.Visible)
            {
                this.solutionTreeView.Visible = false;
                this.solutionPanel.Width = 40;
                this.btnHideShowSolution.Left = 2;
                this.lblSolutionView.Visible = false;
            }
            else
            {
                this.solutionTreeView.Visible = true;
                this.solutionPanel.Width = 250;
                this.btnHideShowSolution.Left = 210;
                this.lblSolutionView.Visible = true;
            }
        }
    }
}

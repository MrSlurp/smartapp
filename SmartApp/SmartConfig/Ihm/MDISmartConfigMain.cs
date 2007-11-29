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

namespace SmartApp.Ihm
{
    public partial class MDISmartConfigMain : Form
    {
        protected DataForm m_DataForm = new DataForm();
        protected DesignerForm m_DesignForm = new DesignerForm();
        protected FrameForm m_FrameForm = new FrameForm();
        protected ProgramForm m_ProgForm = new ProgramForm();
        protected BTDoc m_Document = null;
        protected string m_strDocumentName = "";

        //*****************************************************************************************************
        // Description: constructeur par défaut
        // Return: /
        //*****************************************************************************************************
        public MDISmartConfigMain()
        {
            DoFileFormatRegistration();
            InitializeComponent();
            m_DataForm.MdiParent = this;
            m_DesignForm.MdiParent = this;
            m_FrameForm.MdiParent = this;
            m_ProgForm.MdiParent = this;
            UpdateFileCommand(null, null);
        }
        //*****************************************************************************************************
        // Description: constructeur ouvrant le fichier passé en paramètre dès la fin de l'initialisation
        // Return: /
        //*****************************************************************************************************
        public MDISmartConfigMain(string FileName)
        {
            DoFileFormatRegistration();
            InitializeComponent();
            m_DataForm.MdiParent = this;
            m_DesignForm.MdiParent = this;
            m_FrameForm.MdiParent = this;
            m_ProgForm.MdiParent = this;
            UpdateFileCommand(null, null);
            OpenDoc(FileName);
        }

        #region menu edition
        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        private void CutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // TODO: Use System.Windows.Forms.Clipboard to insert the selected text or images into the clipboard
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // TODO: Use System.Windows.Forms.Clipboard to insert the selected text or images into the clipboard
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // TODO: Use System.Windows.Forms.Clipboard.GetText() or System.Windows.Forms.GetData to retrieve information from the clipboard.
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

        //*****************************************************************************************************
        // Description: met a jour le titre de l'application en fonction du nom du document et de son état 
        // modifié ou non
        // Return: /
        //*****************************************************************************************************
        public void UpdateTitle()
        {
            string strTitle = "SmartConfig";
            strTitle += " - ";
            strTitle += m_strDocumentName;
            if (m_Document != null)
            {
                if (m_Document.Modified)
                {
                    strTitle += "*";
                }
            }
            this.Text = strTitle;
        }

        #region menu File
        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        private void OnNewMenuItemClick(object sender, EventArgs e)
        {
            if (m_Document == null)
            {
                m_Document = new BTDoc();
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

                    m_Document = new BTDoc();
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
            Application.Exit();
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
            saveFileDialog.Filter = "BTApp2 File (*.saf)|*.saf";
            saveFileDialog.InitialDirectory = Application.StartupPath;
            DialogResult dlgRes = saveFileDialog.ShowDialog();
            if (dlgRes == DialogResult.OK)
            {
                string strFileFullName = saveFileDialog.FileName;
                m_Document.WriteConfigDocument(strFileFullName, true);
                int lastindex = strFileFullName.LastIndexOf(@"\");
                m_strDocumentName = strFileFullName.Substring(lastindex + 1);
                m_Document.Modified = false;
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
            m_Document = new BTDoc();
            //m_Document.UpdateDocumentFrame += new NeedRefreshHMI(OnNeedUpdateHMI);
            //m_Document.OnDocumentModified += new DocumentModifiedEvent(UpdateModifiedFlag);
            if (m_Document.ReadConfigDocument(strFullFileName))
            {
                if (OpenDocument(m_Document))
                {
                    int lastindex = strFullFileName.LastIndexOf(@"\");
                    m_strDocumentName = strFullFileName.Substring(lastindex+1);
                    UpdateTitle();
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

            m_windowsMenu.Enabled = true;
            m_jumpTotCmdMenuItem.Enabled = true;
            m_MenuItemM3SLWiz.Enabled = true;
            m_MenuItemTCPMBWiz.Enabled = true;
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
            m_MenuItemTCPMBWiz.Enabled = false;
            m_Document = null;
            UpdateFileCommand(null, null);
            return true;
        }
        #endregion

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************      
        protected void OnNeedUpdateHMI(MessNeedUpdate Mess)
        {
            if (this.InvokeRequired)
            {
                AsyncUpdateHMI AsyncCall = new AsyncUpdateHMI(AsyncUpdater);
                this.Invoke(AsyncCall);
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
                if (res == DialogResult.Cancel)
                {
                    e.Cancel = true;
                    return;
                }
            }
        }

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
                            MessageBox.Show("Application fail on startup.", "Error");
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
        protected void UpdateModifiedFlag()
        {
            UpdateTitle();
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************      
        private void OnMenuItemM3SLWizClick(object sender, EventArgs e)
        {
            if (m_Document != null)
            {
                WizardSLForm wiz = new WizardSLForm();
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
        private void m_MenuItemClose_Click(object sender, EventArgs e)
        {
            this.CloseDoc();
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

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutForm.ShowAbout();
        }

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
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Xml;

namespace CommonLib
{
    public partial class VirtualDataForm : Form
    {
        private Timer m_TimerScenPlayer = new Timer();
        private bool m_bIsScenRunning = false;
        private GestDataVirtual m_GestVirtualData = null;
        private GestData m_GestData = null;
        
        private SaveFileDialog m_CliSavedlg = new SaveFileDialog();
        private OpenFileDialog m_CliOpendlg = new OpenFileDialog();
        
        private SaveFileDialog m_ScnSavedlg = new SaveFileDialog();
        private OpenFileDialog m_ScnOpendlg = new OpenFileDialog();
        public VirtualDataForm(BTDoc Document)
        {
            Lang.LangSys.Initialize(this);
            InitializeComponent();
            m_PanelScenario.Document = Document;
            m_GestVirtualData = Document.GestDataVirtual;
            m_GestData = Document.GestData;
            m_TimerScenPlayer.Tick += new EventHandler(m_TimerScenPlayer_Tick);
            m_CliSavedlg.Filter = Lang.LangSys.C("Data cliche File (*.sac)|*.sac");
            m_CliSavedlg.InitialDirectory = Path.GetDirectoryName(Document.FileName);
            m_CliOpendlg.Filter = Lang.LangSys.C("Data cliche File (*.sac)|*.sac");
            m_CliOpendlg.InitialDirectory = Path.GetDirectoryName(Document.FileName);
            m_ScnOpendlg.Filter = Lang.LangSys.C("Scenario cliche File (*.sas)|*.sas");
            m_ScnOpendlg.InitialDirectory = Path.GetDirectoryName(Document.FileName);
            m_ScnSavedlg.Filter = Lang.LangSys.C("Scenario cliche File (*.sas)|*.sas");
            m_ScnSavedlg.InitialDirectory = Path.GetDirectoryName(Document.FileName);
        }

        private void m_TimerScenPlayer_Tick(object sender, EventArgs e)
        {
            if (m_bIsScenRunning)
            {
                if (!m_PanelScenario.LoadNextLine(m_checkPlayLoop.Checked))
                    m_btnStartStopPlay_Click(null, null);
            }
        }

        private void Initialize()
        {
            m_tabControlDataPanels.SuspendLayout();
            for (int i = 0; i < m_GestVirtualData.GroupCount; i++)
            {
                BaseGestGroup.Group group = m_GestVirtualData.Groups[i];
                if (!group.IsEmpty && !group.OwnOnlyConstData)
                {
                    TabPage GroupTabPage = new TabPage(group.GroupName);
                    m_tabControlDataPanels.TabPages.Add(GroupTabPage);
                    GroupTabPage.SuspendLayout();
                    VirtualDataPanel vdPanel = new VirtualDataPanel(m_GestVirtualData, m_GestData, group.GroupSymbol);
                    Size tmpSz = GroupTabPage.Size;
                    tmpSz.Width = tmpSz.Width-5; 
                    vdPanel.Size = tmpSz;
                    //vdPanel.Dock = DockStyle.Fill;
                    vdPanel.Anchor = AnchorStyles.Bottom | AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
                    GroupTabPage.Controls.Add(vdPanel);
                    GroupTabPage.ResumeLayout();
                    vdPanel.Initialize();
                }
            }
            m_tabControlDataPanels.ResumeLayout();
        }

        private void VirtualDataForm_Load(object sender, EventArgs e)
        {
            Initialize();
            m_NumPeriod_ValueChanged(null, null);
        }

        private void UpdateBtnState(object sender, EventArgs e)
        {
            if (m_bIsScenRunning)
            {
                m_btnAddCliche.Enabled = false;
                m_btnRemoveCliche.Enabled = false;
                m_btnLoadPrev.Enabled = false;
                m_btnLoadNext.Enabled = false;
                m_btnLoadScen.Enabled = false;
                m_btnSaveScen.Enabled = false;
                m_btnUp.Enabled = false;
                m_btnDown.Enabled = false;
            }
            else
            {
                m_btnAddCliche.Enabled = true;
                m_btnRemoveCliche.Enabled = true;
                m_btnLoadPrev.Enabled = true;
                m_btnLoadNext.Enabled = true;
                m_btnLoadScen.Enabled = true;
                m_btnSaveScen.Enabled = true;
                m_btnUp.Enabled = true;
                m_btnDown.Enabled = true;
            }
        }
    
        private void SaveDataCliche_click(object sender, EventArgs e)
        {
            if (m_CliSavedlg.ShowDialog() == DialogResult.OK)
            {
                XmlDocument XmlDoc = new XmlDocument();
                XmlDoc.LoadXml("<Root></Root>");

                XmlNode NodeDataSection = XmlDoc.CreateElement(XML_CF_TAG.DataSection.ToString());
                XmlDoc.DocumentElement.AppendChild(NodeDataSection);

                m_GestVirtualData.WriteOutInstantImage(XmlDoc, NodeDataSection);
                m_GestVirtualData.SetAllSaveInCliche(false);
                string strfileFullName = m_CliSavedlg.FileName;
                XmlDoc.Save(strfileFullName);
            }   
        }

        private void LoadDataCliche_click(object sender, EventArgs e)
        {
            m_CliOpendlg.Multiselect = false;
            if (m_CliOpendlg.ShowDialog() == DialogResult.OK)
            {
                LoadCliche(m_CliOpendlg.FileName);
            }
        }

        public void LoadCliche(string strfileFullName)
        {
            XmlDocument XmlDoc = new XmlDocument();
            try
            {
                XmlDoc.Load(strfileFullName);
            }
            catch (Exception ex)
            {
                string strErr = Lang.LangSys.C("The file is corrupted");
                strErr += ex.Message;
                Traces.LogAddDebug(TraceCat.SmartCommand, "Load Cliche", strErr);
                return;
            }

            XmlNode RootNode = XmlDoc.FirstChild;
            if (RootNode.Name != XML_CF_TAG.Root.ToString())
                return;

            XmlNode Node = RootNode.FirstChild;
            while (Node != null)
            {
                XML_CF_TAG Id = XML_CF_TAG.Root;
                try
                {
                    Id = (XML_CF_TAG)Enum.Parse(typeof(XML_CF_TAG), Node.Name, true);
                }
                catch (Exception)
                {
                    // le fait de parser sur un enum peu lever une exception si la valeur n'appartiens pas a l'enum
                    Node = Node.NextSibling;
                    continue;
                }
                // on charge juste la section Data
                switch (Id)
                {
                    case XML_CF_TAG.DataSection:
                        this.m_GestVirtualData.ReadInInstantImage(Node, TYPE_APP.SMART_COMMAND);
                        break;
                    default:
                        break;
                }
                Node = Node.NextSibling;
            }
            return;
        }

        private void m_btnAddCliche_Click(object sender, EventArgs e)
        {
            m_CliOpendlg.Multiselect = true;
            if (m_CliOpendlg.ShowDialog() == DialogResult.OK)
            {
                m_PanelScenario.AddClicheFiles(m_CliOpendlg.FileNames);
            }
        }

        private void m_btnRemoveCliche_Click(object sender, EventArgs e)
        {
            m_PanelScenario.RemoveSelected();
        }

        private void m_btnLoadPrev_Click(object sender, EventArgs e)
        {
            m_PanelScenario.LoadPrevLine(m_checkPlayLoop.Checked);
        }

        private void m_btnLoadNext_Click(object sender, EventArgs e)
        {
            m_PanelScenario.LoadNextLine(m_checkPlayLoop.Checked);
        }

        private void m_btnLoadScen_Click(object sender, EventArgs e)
        {
            if (m_ScnOpendlg.ShowDialog() == DialogResult.OK)
            {
                string strfileFullName = m_ScnOpendlg.FileName;
                XmlDocument XmlDoc = new XmlDocument();
                try
                {
                    XmlDoc.Load(strfileFullName);
                }
                catch (Exception ex)
                {
                    string strErr = Lang.LangSys.C("The file is corrupted");
                    strErr += ex.Message;
                    Traces.LogAddDebug(TraceCat.SmartCommand, "Load Scen", strErr);
                    return;
                }
                XmlNode RootNode = XmlDoc.FirstChild;
                if (RootNode.Name != XML_CF_TAG.Root.ToString())
                    return;

                XmlNode Node = RootNode.FirstChild;
                m_PanelScenario.LoadScenario(Node);
                return;
            } 
        }

        private void m_btnSaveScen_Click(object sender, EventArgs e)
        {
            if (m_ScnSavedlg.ShowDialog() == DialogResult.OK)
            {
                XmlDocument XmlDoc = new XmlDocument();
                XmlDoc.LoadXml("<Root></Root>");
                XmlNode NodeFileListe = XmlDoc.CreateElement(XML_CF_TAG.FileList.ToString());
                XmlDoc.DocumentElement.AppendChild(NodeFileListe);
                m_PanelScenario.SaveScenario(XmlDoc, NodeFileListe);
                string strfileFullName = m_ScnSavedlg.FileName;
                XmlDoc.Save(strfileFullName);
            }   
        }

        private void m_btnStartStopPlay_Click(object sender, EventArgs e)
        {
            if (!m_bIsScenRunning)
            {
                m_bIsScenRunning = true;
                UpdateBtnState(null, null);
                m_PanelScenario.LoadSelectedLine();
                m_PanelScenario.ClicheGridEnabled = false;
                m_TimerScenPlayer.Start();
            }
            else
            {
                m_bIsScenRunning = false;
                UpdateBtnState(null, null);
                m_PanelScenario.ClicheGridEnabled = true;
                m_TimerScenPlayer.Stop();
            }
        }

        private void m_NumPeriod_ValueChanged(object sender, EventArgs e)
        {
            m_TimerScenPlayer.Interval = (int)m_NumPeriod.Value * 1000;
        }

        private void m_btnUp_Click(object sender, EventArgs e)
        {
            m_PanelScenario.MoveSelectedUp();
        }

        private void m_btnDown_Click(object sender, EventArgs e)
        {
            m_PanelScenario.MoveSelectedDown();
        }
    }
}
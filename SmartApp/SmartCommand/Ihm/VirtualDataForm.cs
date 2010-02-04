using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using CommonLib;

namespace SmartApp
{
    public partial class VirtualDataForm : Form
    {
        private GestDataVirtual m_GestVirtualData = null;
        private GestData m_GestData = null;
        public VirtualDataForm(GestDataVirtual GestVData, GestData GestData)
        {
            InitializeComponent();
            m_GestVirtualData = GestVData;
            m_GestData = GestData;
        }

        private void Initialize()
        {
            for (int i = 0; i < m_GestVirtualData.GroupCount; i++)
            {
                BaseGestGroup.Group group = m_GestVirtualData.Groups[i];
                TabPage GroupTabPage = new TabPage(group.GroupName);
                m_tabControlDataPanels.TabPages.Add(GroupTabPage);
                GroupTabPage.SuspendLayout();
                VirtualDataPanel vdPanel = new VirtualDataPanel(m_GestVirtualData, m_GestData, group.GroupSymbol);
                vdPanel.Size = GroupTabPage.Size;
                vdPanel.Dock = DockStyle.Fill;
                vdPanel.Anchor = AnchorStyles.Bottom | AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
                GroupTabPage.Controls.Add(vdPanel);
                GroupTabPage.ResumeLayout();
                vdPanel.Initialize();
            }
        }

        private void VirtualDataForm_Load(object sender, EventArgs e)
        {
            Initialize();
        }
    
        private void SaveDataCliche(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "Data cliche File (*.sac)|*.sac";
            dlg.InitialDirectory = Application.StartupPath;
            if (dlg.ShowDialog() == DialogResult.OK)
            {
              XmlDocument XmlDoc = new XmlDocument();
              XmlDoc.LoadXml("<Root></Root>");

              XmlNode NodeDataSection = XmlDoc.CreateElement(XML_CF_TAG.DataSection.ToString());
              XmlDoc.DocumentElement.AppendChild(NodeDataSection);
            
              m_GestVirtualData.WriteOutInstantImage(XmlDoc, NodeDataSection);
              string strfileFullName = dlg.FileName;
              XmlDoc.Save(strfileFullName);
            }   
        }
    
        private void LoadDataCliche(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Data cliche File (*.sac)|*.sac";
            dlg.InitialDirectory = Application.StartupPath;
            if (dlg.ShowDialog() == DialogResult.OK)
            {
              string strfileFullName = dlg.FileName;
              XmlDocument XmlDoc = new XmlDocument();
              try
              {
                  XmlDoc.Load(strfileFullName);
              }
              catch (Exception ex)
              {
                  string strErr = "The file is corrupted";
                  strErr += ex.Message;
                  Console.WriteLine(strErr);
                  return ;
              }

              XmlNode RootNode = XmlDoc.FirstChild;
              if (RootNode.Name != XML_CF_TAG.Root.ToString())
                  return ;
  
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
              return ;                
            }   
        }
    }
}
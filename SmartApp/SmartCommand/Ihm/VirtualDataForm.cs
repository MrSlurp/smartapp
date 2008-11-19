using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
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
                m_tabControl.TabPages.Add(GroupTabPage);
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
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.IO;

namespace SmartApp
{
    public partial class PluginsVersionsForm : Form
    {
        public PluginsVersionsForm()
        {
            InitializeComponent();
            LoadExistingDlls();
        }

        public void LoadExistingDlls()
        {
            string strAppDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetModules()[0].FullyQualifiedName);
            string[] filenames = Directory.GetFiles(strAppDir, "*.dll", SearchOption.TopDirectoryOnly);
            for (int i = 0; i < filenames.Length; i++)
            {
                System.Diagnostics.FileVersionInfo ver = System.Diagnostics.FileVersionInfo.GetVersionInfo(filenames[i]);
                ListViewItem lviData = new ListViewItem(ver.InternalName);
                lviData.SubItems.Add(ver.FileVersion);
                m_listViewDll.Items.Add(lviData);
            }
        }
    }
}
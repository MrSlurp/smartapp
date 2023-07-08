/*
    This file is part of SmartApp.
    Copyright (C) 2007-2016  Pascal Bigot

    SmartApp is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    SmartApp is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with Foobar.  If not, see <http://www.gnu.org/licenses/>.
*/
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
            Program.LangSys.Initialize(this);
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
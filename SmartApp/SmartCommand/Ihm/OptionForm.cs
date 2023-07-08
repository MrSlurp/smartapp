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

namespace SmartApp
{
    public partial class OptionForm : Form
    {
        public OptionForm()
        {
            Program.LangSys.Initialize(this);
            InitializeComponent();
        }

        public string LogFileDirectory
        {
            get
            {
                return m_textLogDir.Text;
            }
            set
            {
                m_textLogDir.Text = value;
            }
        }

        public bool AutoStartProjOnOpen
        {
            get
            {
                return m_checkAutoStartProjOnOpen.Checked;
            }
            set
            {
                m_checkAutoStartProjOnOpen.Checked = value;
            }
        }

        public bool HideMonitorAfterPrjStart
        {
            get
            {
                return m_checkHideMonitorOnStart.Checked;
            }
            set
            {
                m_checkHideMonitorOnStart.Checked = value;
            }
        }


        private void m_btnBrowse_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog Browser = new FolderBrowserDialog();
            Browser.RootFolder = Environment.SpecialFolder.MyComputer;
            if (!string.IsNullOrEmpty(m_textLogDir.Text))
            {
                Browser.SelectedPath = m_textLogDir.Text;
            }

            if (Browser.ShowDialog() == DialogResult.OK)
            {
                m_textLogDir.Text = Browser.SelectedPath;
            }
        }
    }
}
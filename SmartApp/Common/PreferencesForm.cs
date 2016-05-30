/*
    This file is part of SmartApp.

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
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Globalization;
using CommonLib;

namespace SmartApp
{
    public partial class PreferencesForm : Form
    {
        public PreferencesForm()
        {
            Program.LangSys.Initialize(this);
            InitializeComponent();
            CComboData[] TabLang = BuildListLang();


            m_cboLang.ValueMember = "Object";
            m_cboLang.DisplayMember = "DisplayedString";
            m_cboLang.DataSource = TabLang;
            m_cboLang.SelectedIndex = 0;
        }

        public string SelectedLang
        {
            get
            {
                return (string)m_cboLang.SelectedValue;
            }
            set
            {
                m_cboLang.SelectedValue = value;
            }
        }

        private CComboData[] BuildListLang()
        {
            string Dir = Application.StartupPath + Path.DirectorySeparatorChar + Lang.LANG_DIRECTORY_NAME;
            string[] LangFileList = Directory.GetFiles(Dir, "*.po", SearchOption.TopDirectoryOnly);
            StringCollection ListLang = new StringCollection();
            for (int i = 0; i< LangFileList.Length; i++)
            {
                string FileName = Path.GetFileName(LangFileList[i]);
                string LangCode = FileName.Split('.')[0];
                if (!ListLang.Contains(LangCode))
                {
                    ListLang.Add(LangCode);
                }
            }
            CComboData[] ListCboData = new CComboData[ListLang.Count];
            for (int i = 0; i < ListLang.Count; i++)
            {
                CultureInfo Culture = CultureInfo.CreateSpecificCulture(ListLang[i]);
                ListCboData[i] = new CComboData(Culture.NativeName, ListLang[i]);
            }

            return ListCboData;
        }
    }
}
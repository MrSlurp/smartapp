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
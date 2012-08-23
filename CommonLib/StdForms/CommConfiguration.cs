using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Reflection; 
using System.Net;

namespace CommonLib
{
    public partial class CommConfiguration : Form
    {
        protected IniFileParser m_IniFile = new IniFileParser();
        protected string m_strAppDir;
        protected bool m_bAllowRowSelect = false;

        protected TYPE_COMM m_CurTypeCom = TYPE_COMM.SERIAL;
        protected string m_CurComParam = string.Empty;

        public IniFileParser IniFile
        {
            get
            {
                return m_IniFile;
            }
        }

        public bool AllowRowSelect
        {
            set 
            { 
                m_bAllowRowSelect = value;
                if (m_bAllowRowSelect)
                    m_dataGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                else
                    m_dataGrid.SelectionMode = DataGridViewSelectionMode.RowHeaderSelect;
            }
        }

        public TYPE_COMM CurTypeCom
        {
            get
            {
                return m_CurTypeCom;
            }
            set
            {
                m_CurTypeCom = value;
            }
        }

        public string CurComParam
        {
            get
            {
                return m_CurComParam;
            }
            set
            {
                m_CurComParam = value;
            }
        }

        public CommConfiguration()
        {
            Lang.LangSys.Initialize(this);
            InitializeComponent();
            m_strAppDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetModules()[0].FullyQualifiedName);
            string strIniFilePath = PathTranslator.LinuxVsWindowsPathUse(m_strAppDir + @"\" + Cste.STR_COMINI_FILENAME);
            m_IniFile.Load(strIniFilePath);
        }

        public void InitCommList()
        {
            for (int i = 0; i < Cste.NB_MAX_COMM; i++)
            {
                string strSection = string.Format(Cste.STR_FILE_DESC_HEADER_FORMAT, i);
                string strName = m_IniFile.GetValue(strSection, Cste.STR_FILE_DESC_NAME);
                string strCommType = m_IniFile.GetValue(strSection, Cste.STR_FILE_DESC_COMM);
                string strCommParam = m_IniFile.GetValue(strSection, Cste.STR_FILE_DESC_ADDR);
                if (!string.IsNullOrEmpty(strCommType)
                    && !string.IsNullOrEmpty(strCommParam)
                    )
                {
                    if (string.IsNullOrEmpty(strName))
                        strName = "";

                    AddCommToDataGrid(strName, strCommType, strCommParam);
                }
            }
        }

        protected void SelectCurCom()
        {
            bool bCurCommFound = false;
            if (m_CurTypeCom != TYPE_COMM.NONE)
            {
                for (int i = 0; i < m_dataGrid.RowCount; i++)
                {
                    if ((m_dataGrid.Rows[i].Cells[1].Value != null && m_dataGrid.Rows[i].Cells[2].Value != null)
                        && m_dataGrid.Rows[i].Cells[1].Value.ToString() == m_CurTypeCom.ToString()
                        && m_dataGrid.Rows[i].Cells[2].Value.ToString() == m_CurComParam)
                    {
                        m_dataGrid.Rows[i].Selected = true;
                        bCurCommFound = true;
                        break;
                    }
                }
            }
            if (!bCurCommFound)
            {
                AddCommToDataGrid(Lang.LangSys.C("Document connection"), m_CurTypeCom.ToString(), m_CurComParam);
            }

        }

        protected void AddCommToDataGrid(string Name, string CommType, string CommParam)
        {
            string[] TabValues = new string[3];
            TabValues[0] = Name;
            TabValues[1] = CommType;
            TabValues[2] = CommParam;
            m_dataGrid.Rows.Add(TabValues);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string strErr;
            if (IsDataGridViewValid(out strErr))
            {
                UpdateInitFile();
                if (m_bAllowRowSelect)
                    MemCurSelection();
                DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show(strErr, Lang.LangSys.C("Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
		
        private bool IsDataGridViewValid(out string strErr)
        {
            for (int i = 0; i < m_dataGrid.Rows.Count; i++)
            {
                //ttes les comms doivent être valides
                DataGridViewRow dtgrRow = m_dataGrid.Rows[i];
                string strSection = string.Format(Cste.STR_FILE_DESC_HEADER_FORMAT, i);
                string Name = (string)dtgrRow.Cells[0].Value;
                string Type = (string)dtgrRow.Cells[1].Value;
                string Param = (string)dtgrRow.Cells[2].Value;
                if (string.IsNullOrEmpty(Type))
				{
                    continue;
				}

                if (TYPE_COMM.ETHERNET.ToString() == Type)
                {
                    if (string.IsNullOrEmpty(Param))
					{
                        strErr = Lang.LangSys.C("Invalid Parameters");
                        return false;
					}

                    if (Param.IndexOf(':') == -1)
                    {
                        strErr = Lang.LangSys.C("Invalid host address or host (no port specified)");
                        return false;
                    }

                    string[] TabIpAndPort = Param.Split(':');
                    IPAddress IpAddr = new IPAddress(0);
                    // si le parsing d'adresse IP echoue, on tente une resolution DNS
                    if (!IPAddress.TryParse(TabIpAndPort[0], out IpAddr))
                    {
                        // on place le message d'avance
                        IPHostEntry hostEntry = Dns.GetHostEntry(TabIpAndPort[0]);
                        if (hostEntry.AddressList.Length == 0)
                        {
                            strErr = string.Format(Lang.LangSys.C("Can't resolve hostname {0} or invalide IP address"), TabIpAndPort[0]);
                            return false;
                        }
                        else if (hostEntry.AddressList.Length > 1)
                        {
                            strErr = string.Format(Lang.LangSys.C("More than one IP is associated to hostname {0}"), TabIpAndPort[0]);
                            return false;
                        }
                    }
                }
                else if (TYPE_COMM.SERIAL.ToString() == Type)
                {
                    if (string.IsNullOrEmpty(Param))
					{
                        strErr = Lang.LangSys.C("Invalid com port");
                        return false;
					}

                    if (!Param.StartsWith("COM"))
                    {
                        strErr = Lang.LangSys.C("Invalid Serial port");
                        return false;
                    }
                }
                else if (TYPE_COMM.VIRTUAL.ToString() == Type)
                {
                    // toujours OK
                    dtgrRow.Cells[2].Value = "NA";
                }
                else
                {
                    strErr = Lang.LangSys.C("Invalid Communication, select a type and enter parameters");
                    return false; ;
                }
            }
            strErr = "";
            return true;
        }

        protected void UpdateInitFile()
        {
            m_IniFile.ClearAll();
            for (int i = 0; i < m_dataGrid.Rows.Count; i++)
            {
                //ttes les comms doivent être valides
                DataGridViewRow dtgrRow = m_dataGrid.Rows[i];
                string strSection = string.Format(Cste.STR_FILE_DESC_HEADER_FORMAT, i);
                string Name = (string)dtgrRow.Cells[0].Value;
                string Type = (string)dtgrRow.Cells[1].Value;
                string Param = (string)dtgrRow.Cells[2].Value;
                if (string.IsNullOrEmpty(Type))
                    continue;
                if (string.IsNullOrEmpty(Name))
                    Name = "";
                if (string.IsNullOrEmpty(Param))
                    Param = "void";
                m_IniFile.SetValue(strSection, Cste.STR_FILE_DESC_NAME, Name);
                m_IniFile.SetValue(strSection, Cste.STR_FILE_DESC_COMM, Type);
                m_IniFile.SetValue(strSection, Cste.STR_FILE_DESC_ADDR, Param);
            }
            Traces.LogAddDebug(TraceCat.SmartCommand, "CommConfig", "le fichier à bien été sauvegardé");
            m_IniFile.Save();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void MemCurSelection()
        {
            if (m_bAllowRowSelect)
            {
                if (m_dataGrid.SelectedRows.Count == 1)
                {
                    string strErr;
                    if (IsDataGridViewValid(out strErr) && m_dataGrid.SelectedRows[0].Cells[1].Value != null)
                    {
                        m_CurTypeCom = (TYPE_COMM)Enum.Parse(typeof(TYPE_COMM), (string)m_dataGrid.SelectedRows[0].Cells[1].Value) ;
                        m_CurComParam = m_dataGrid.SelectedRows[0].Cells[2].Value as string;
                    }
                }
            }
        }

        private void CommConfiguration_Load(object sender, EventArgs e)
        {
            m_dataGrid.Rows.Clear();
            InitCommList();

        }

        private void CommConfiguration_Shown(object sender, EventArgs e)
        {
            SelectCurCom();
        }
    }
}

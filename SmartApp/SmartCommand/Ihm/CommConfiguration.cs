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
using CommonLib;

namespace SmartApp
{
    public partial class CommConfiguration : Form
    {
        protected IniFileParser m_IniFile = new IniFileParser();
        string m_strAppDir;
        public IniFileParser IniFile
        {
            get
            {
                return m_IniFile;
            }
        }

        public CommConfiguration()
        {
            Program.LangSys.Initialize(this);
            InitializeComponent();
            m_strAppDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetModules()[0].FullyQualifiedName);
            string strIniFilePath = PathTranslator.LinuxVsWindowsPathUse(m_strAppDir + @"\" + Cste.STR_COMINI_FILENAME);
            m_IniFile.Load(strIniFilePath);
            InitCommList();
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

                    AddCommToDataGrid(strName, strCommType, strCommParam, strSection);
                }
            }
        }

        protected void AddCommToDataGrid(string Name, string CommType, string CommParam, string Section)
        {
            string[] TabValues = new string[3];
            TabValues[0] = Name;
            TabValues[1] = CommType;
            TabValues[2] = CommParam;
            m_dataGrid.Rows.Add(TabValues);
        }

        private void button1_Click(object sender, EventArgs e)
        {
			Console.WriteLine("Le clique bouton à bien été executé");
            string strErr;
            if (IsDataGridViewValid(out strErr))
            {
                UpdateInitFile();
                DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show(strErr, "Error");
            }
        }
		
        private bool IsDataGridViewValid(out string strErr)
        {
			Console.WriteLine(string.Format("nombre de ligne dans la gridview {0}", m_dataGrid.Rows.Count));
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
					Console.WriteLine("Ligne type vide");
                    continue;
				}

                if (TYPE_COMM.ETHERNET.ToString() == Type)
                {
                    if (string.IsNullOrEmpty(Param))
					{
                        strErr = "Invalid Parameters";
                        return false;
					}


                    if (Param.IndexOf(':') == -1)
                    {
                        strErr = "Invalid IP Address";
                        return false;
                    }

                    string[] TabIpAndPort = Param.Split(':');
                    IPAddress IpAddr = new IPAddress(0);
                    if (!IPAddress.TryParse(TabIpAndPort[0], out IpAddr))
                    {
                        strErr = "Invalid IP Address";
                        return false;
                    }
                }
                else if (TYPE_COMM.SERIAL.ToString() == Type)
                {
                    if (string.IsNullOrEmpty(Param))
					{
                        strErr = "Invalid com port";
                        return false;
					}

                    if (!Param.StartsWith("COM"))
                    {
                        strErr = "Invalid Serial port";
                        return false;
                    }
                }
                else if (TYPE_COMM.VIRTUAL.ToString() == Type)
                {
                    // toujours OK
                }
                else
                {
                    strErr = "Invalid Communication, select a type and enter parameters";
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
			Console.WriteLine("le fichier à bien été sauvegardé");
            m_IniFile.Save();
        }
    }
}

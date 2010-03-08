using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using CommonLib;

namespace SmartApp
{
    class AppOptions
    {
        private IniFileParser m_IniOptionFile = new IniFileParser();

        public AppOptions()
        {
        }

        public void Load(string OptionFileName)
        {
            m_IniOptionFile.Load(OptionFileName);
        }

        public void Save()
        {
            m_IniOptionFile.Save();
        }

        public string LogDir
        {
            get
            {
                return m_IniOptionFile.GetValue(Cste.STR_FILE_DESC_HEADER_OPT, Cste.STR_FILE_DESC_LOGDIR);
            }
            set
            {
                m_IniOptionFile.SetValue(Cste.STR_FILE_DESC_HEADER_OPT, Cste.STR_FILE_DESC_LOGDIR, value);
            }
        }

        public bool SaveFileComParam
        {
            get
            {
                bool bRes = true;
                string strSaveFileComParam = m_IniOptionFile.GetValue(Cste.STR_FILE_DESC_HEADER_OPT, 
                                                                      Cste.STR_FILE_DESC_SAVE_PREF_COMM);
                bool.TryParse(strSaveFileComParam, out bRes);
                return bRes;
            }
            set
            {
                m_IniOptionFile.SetValue(Cste.STR_FILE_DESC_HEADER_OPT, Cste.STR_FILE_DESC_SAVE_PREF_COMM, value.ToString());
            }
        }

        public string GetFileCommType(string FileName)
        {
            string file = Path.GetFileName(FileName);
            return m_IniOptionFile.GetValue(file, Cste.STR_FILE_DESC_COMM);
        }

        public string GetFileCommParam(string FileName)
        {
            string file = Path.GetFileName(FileName);
            return m_IniOptionFile.GetValue(file, Cste.STR_FILE_DESC_ADDR);
        }

        public void SetFileCommType(string FileName, string commType)
        {
            string file = Path.GetFileName(FileName);
            m_IniOptionFile.SetValue(file, Cste.STR_FILE_DESC_COMM, commType);
        }

        public void SetFileCommParam(string FileName, string commParam)
        {
            string file = Path.GetFileName(FileName);
            m_IniOptionFile.SetValue(file, Cste.STR_FILE_DESC_ADDR, commParam);
        }
    }
}

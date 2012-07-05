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

        public bool AutoStartProjOnOpen
        {
            get
            {
                bool bRes = true;
                string strAutoStartProjOnOpen = m_IniOptionFile.GetValue(Cste.STR_FILE_DESC_HEADER_OPT, 
                                                                      Cste.STR_FILE_DESC_AUTO_START);
                bool.TryParse(strAutoStartProjOnOpen, out bRes);
                return bRes;
            }
            set
            {
                m_IniOptionFile.SetValue(Cste.STR_FILE_DESC_HEADER_OPT, Cste.STR_FILE_DESC_AUTO_START, value.ToString());
            }
        }

        public bool HideMonitorAfterPrjStart
        {
            get
            {
                bool bRes = true;
                string strValue = m_IniOptionFile.GetValue(Cste.STR_FILE_DESC_HEADER_OPT,
                                                           Cste.STR_FILE_DESC_HIDE_MON);
                bool.TryParse(strValue, out bRes);
                return bRes;
            }
            set
            {
                m_IniOptionFile.SetValue(Cste.STR_FILE_DESC_HEADER_OPT, Cste.STR_FILE_DESC_HIDE_MON, value.ToString());
            }
        }

    }
}

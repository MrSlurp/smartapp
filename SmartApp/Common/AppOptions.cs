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

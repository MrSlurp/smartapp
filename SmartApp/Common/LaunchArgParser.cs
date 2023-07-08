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
using System.Drawing;
using System.Text;
using CommonLib;
 

namespace SmartApp
{
    public static class LaunchArgParser : Object
    {
        private static string m_strFileName;
        private static TYPE_APP m_TypeApp = TYPE_APP.NONE;
        private static bool m_bAutoConnect = false;
        private static bool m_bAutoStart = false;
        
        private static bool m_OperatorMode = false;

        private static bool m_DevMode = false;

        public static bool AutoConnect
        {
            get
            {
                return m_bAutoConnect;
            }
        }

        public static bool AutoStart
        {
            get
            {
                return m_bAutoStart;
            }
        }

        public static bool OperatorMode
        {
            get
            {
                return m_OperatorMode;
            }
        }

        public static bool DevMode
        {
            get
            {
                return m_DevMode;
            }
        }

        public static string File
        {
            get
            {
                return m_strFileName;
            }
        }

        public static void ParseArguments(string[] strArgsList)
        {
            // le premier argument doit toujorus être le type de l'appli
            m_TypeApp = GetTypeApp(strArgsList);

            for (int i = 0; i < strArgsList.Length; i++)
            {
                if (strArgsList[i].EndsWith(".slt"))
                {
                    m_strFileName = strArgsList[i];
                }
                else if (strArgsList[i].EndsWith(".saf"))
                {
                    // fichier plus pris en charge à l'ouverture
                    //m_strFileName = strArgsList[i];
                }
                else if (strArgsList[i].StartsWith("-"))
                {
                    if (strArgsList[i].ToUpper() == Cste.STRCMD_AUTOCONNECT)
                    {
                        m_bAutoConnect = true;
                    }
                    else if (strArgsList[i].ToUpper() == Cste.STRCMD_AUTOSTART)
                    {
                        m_bAutoStart = true;
                    }
                    else if (strArgsList[i].ToUpper() == Cste.STRCMD_OPMOD)
                    {
                        m_OperatorMode = true;
                    }
                    else if (strArgsList[i].ToUpper() == Cste.STRCMD_DEV)
                    {
                        m_DevMode = true;
                    }
                }
                else
                    System.Diagnostics.Debug.Assert(false);// argument non valide

            }

        }

        //*****************************************************************************************************
        // Description: renvoie le type d'application en fonction des arguments en ligne de commande
        // Return: /
        //*****************************************************************************************************
        public static TYPE_APP GetTypeApp(string[] strArgsList)
        {
            if (strArgsList.Length == 0)
            {
                return TYPE_APP.SMART_CONFIG;
            }
            else if (strArgsList[0].ToUpper() == Cste.STRCMD_CFG)
            {
                return TYPE_APP.SMART_CONFIG;
            }
            else if (strArgsList[0].ToUpper() == Cste.STRCMD_CMD)
            {
                return TYPE_APP.SMART_COMMAND;
            }
            else
                return TYPE_APP.NONE;
        }

        //*****************************************************************************************************
        // Description: renvoie un le nom du fichier
        // Return: /
        //*****************************************************************************************************
        public static string GetFileName(string[] strArgsList)
        {
            if (strArgsList.Length == 2 && strArgsList[1].EndsWith(".saf", StringComparison.OrdinalIgnoreCase))
                return strArgsList[1];
            else
                return null;
        }
    }
}

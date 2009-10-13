using System;
using System.Collections.Generic;
using System.Text;
using CommonLib;
 

namespace SmartApp
{
    public static class LaunchArgParser : Object
    {
        private static string m_strFileName;
        private static TYPE_APP m_TypeApp = TYPE_APP.NONE;
        private static bool m_bAutoConnect = false;
        private static TYPE_COMM m_Comtype = TYPE_COMM.SERIAL;
        private static string m_CommParam;
        private static bool m_bAutoStart = false;

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

        public static TYPE_COMM CommType
        {
            get
            {
                return m_Comtype;
            }
        }

        public static string CommParam
        {
            get
            {
                return m_CommParam;
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
                if (strArgsList[i].EndsWith(".saf"))
                {
                    m_strFileName = strArgsList[i];
                }
                else if (strArgsList[i].StartsWith("-"))
                {
                    if (strArgsList[i].ToUpper() == Cste.STRCMD_AUTOCONNECT && strArgsList.Length >= (i+2))
                    {
                        string strComm = strArgsList[i + 1];
                        string[] TabComm = strComm.Split('/');
                        if (TabComm.Length == 2 && TabComm[0].ToUpper() == TYPE_COMM.ETHERNET.ToString())
                        {
                            m_Comtype = TYPE_COMM.ETHERNET;
                            m_CommParam = TabComm[1];
                        }
                        else if (TabComm.Length == 2 && TabComm[0].ToUpper() == TYPE_COMM.SERIAL.ToString())
                        {
                            m_Comtype = TYPE_COMM.SERIAL;
                            m_CommParam = TabComm[1];
                        }
                        else if (TabComm.Length == 1 && TabComm[0].ToUpper() == TYPE_COMM.VIRTUAL.ToString())
                        {
                            m_Comtype = TYPE_COMM.VIRTUAL;
                            m_CommParam = "Virtual";
                        }
                        m_bAutoConnect = true;
                        i++;
                    }
                    else if (strArgsList[i].ToUpper() == Cste.STRCMD_AUTOSTART)
                    {
                        m_bAutoStart = true;
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

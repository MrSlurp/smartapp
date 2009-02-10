using System;
using System.Collections.Generic;
using System.Windows.Forms;
using SmartApp.Ihm;
using CommonLib;

namespace SmartApp
{
    static class Program
    {
        static MDISmartConfigMain m_ConfigApp = null;
        static MDISmartCommandMain m_CommandApp = null;
        static Form m_CurrentMainForm = null;
        static DllControlGest m_GestDlls = new DllControlGest();

        private static TYPE_APP m_TypeApp = TYPE_APP.NONE;

        //*****************************************************************************************************
        // Description: attribut permettant de connaitre le type de soft executé
        // cet attribut n'est pas read only car SmartConfig peux avoir a le changer temporairement
        // lors du "saut a SmartCommand"
        // Return: /
        //*****************************************************************************************************
        public static TYPE_APP TypeApp
        {
            get
            {
                return m_TypeApp;
            }
        }

        public static DllControlGest DllGest
        {
            get
            {
                return m_GestDlls;
            }
        }

        //*****************************************************************************************************
        // Description: 
        // Return: /
        //*****************************************************************************************************
        public static Form CurrentMainForm
        {
            get
            {
                return m_CurrentMainForm;
            }
            set
            {
                m_CurrentMainForm = value;
            }
        }

        //*****************************************************************************************************
        // Description: 
        // Return: /
        //*****************************************************************************************************
        [STAThread]
        static void Main(string[] strArgsList)
        {
            Traces.Initialize(Application.StartupPath, "TraceSmartApp.txt", Traces.LOG_LEVEL_NOLOG);
            CommonLib.Resources.InitializeBitmap();
            m_GestDlls.LoadExistingDlls();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            LaunchArgParser.ParseArguments(strArgsList);
            m_TypeApp = LaunchArgParser.GetTypeApp(strArgsList);
            //string file = LaunchArgParser.GetFileName(strArgsList);
            switch (m_TypeApp)
            {
                case TYPE_APP.NONE:
                case TYPE_APP.SMART_CONFIG:
                    m_TypeApp = TYPE_APP.SMART_CONFIG;
                    if (LaunchArgParser.File != null)
                        m_ConfigApp = new MDISmartConfigMain(LaunchArgParser.File);
                    else
                        m_ConfigApp = new MDISmartConfigMain();
                    Application.Run(m_ConfigApp);
                    break;
                case TYPE_APP.SMART_COMMAND:
                    m_TypeApp = TYPE_APP.SMART_COMMAND;
                    if (LaunchArgParser.File != null)
                        m_CommandApp = new MDISmartCommandMain(LaunchArgParser.File);
                    else
                        m_CommandApp = new MDISmartCommandMain();
                    Application.Run(m_CommandApp);
                    break;
            }
            
        }
    }
}
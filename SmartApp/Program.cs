using System;
using System.Collections.Generic;
using System.Windows.Forms;
using SmartApp.Ihm;
using CommonLib;

namespace SmartApp
{
    static class Program
    {
#if BUILD_LANG
#if TEST_LANG
        static Lang m_SingLangSys = new Lang(true, true);
#else
        static Lang m_SingLangSys = new Lang(true, false);
#endif
#else
        static Lang m_SingLangSys = new Lang();
#endif
        public static Lang LangSys
        {
            get { return m_SingLangSys; }
        }
        static MDISmartConfigMain m_ConfigApp = null;
        static MDISmartCommandMain m_CommandApp = null;
        static Form m_CurrentMainForm = null;
        static DllControlGest m_GestDlls = new DllControlGest();

        public static void ChangePluginLang(string Lang)
        {
            m_GestDlls.ChangeLang(Lang);
        }

        private static TYPE_APP m_TypeApp = TYPE_APP.NONE;

        public static bool SetDefaultTextRenderingCalled = false;

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
            Application.EnableVisualStyles();
            // attention, cette ligne est toujours appelé, mais dans la création du singleton de la fenêtre principale
            // car les membres étant statiques, il provoquent la création de la fenêtre pendant le chargement même de l'application
            Application.SetCompatibleTextRenderingDefault(false);
            
            Traces.Initialize(Application.StartupPath, 
                              "TraceSmartApp.txt", 
                              (TracesLevel)SmartApp.Properties.Settings.Default.LogLevel, 
                              (TraceCat)Convert.ToInt32(SmartApp.Properties.Settings.Default.LogCat, 16),
                              SmartApp.Properties.Settings.Default.LogToFile
                              );
            LangSys.Initialize(Cste.STR_DEV_LANG, SmartApp.Properties.Settings.Default.Lang, "SmartApp");
            CommonLib.Resources.InitializeBitmap();
            CommonLib.Lang.InitCommonLibLang(Cste.STR_DEV_LANG, SmartApp.Properties.Settings.Default.Lang);
            m_GestDlls.CurrentLang = SmartApp.Properties.Settings.Default.Lang;
            m_GestDlls.LoadExistingDlls();
            LaunchArgParser.ParseArguments(strArgsList);
            m_TypeApp = LaunchArgParser.GetTypeApp(strArgsList);
            switch (m_TypeApp)
            {
                case TYPE_APP.NONE:
                case TYPE_APP.SMART_CONFIG:
                    m_TypeApp = TYPE_APP.SMART_CONFIG;
                    if (LaunchArgParser.File != null)
                        m_CurrentMainForm = m_ConfigApp = new MDISmartConfigMain(LaunchArgParser.File);
                    else
                        m_CurrentMainForm = m_ConfigApp = new MDISmartConfigMain();
                    Application.Run(m_ConfigApp);
                    break;
                case TYPE_APP.SMART_COMMAND:
                    m_TypeApp = TYPE_APP.SMART_COMMAND;
                    if (LaunchArgParser.File != null)
                        m_CurrentMainForm = m_CommandApp = new MDISmartCommandMain(LaunchArgParser.File);
                    else
                        m_CurrentMainForm = m_CommandApp = new MDISmartCommandMain();

                    Application.Run(m_CommandApp);
                    break;
            }
            
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.Drawing;
using System.Xml;
using CommonLib;

namespace GradientBaloon
{
    public class DllEntryClass : IDllControlInterface
    {
        // changes ici l'identifiant unique de la DLL
        public const uint DLL_Control_ID = 130;

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

        string m_CurLang;
        public string CurrentLang
        {
            get { return m_CurLang; }
            set
            {
                m_CurLang = value;
                if (!LangSys.InitDone)
                    LangSys.Initialize(Cste.STR_DEV_LANG, m_CurLang, "GradientBaloon");
                else
                    LangSys.ChangeLangage(value);
            }
        }

        public string PluginType
        {
            get { return LangSys.C("2 - Graphic"); }
        }

        public DllEntryClass()
        {
            GradientBaloonRes.InitializeBitmap();
        }

        public uint DllID
        {
            get
            {
                return DLL_Control_ID;
            }
        }

        public BTControl CreateBTControl()
        {
            return new BTDllGradientBaloonControl();
        }

        public BTControl CreateBTControl(InteractiveControl iCtrl)
        {
            return new BTDllGradientBaloonControl(iCtrl);
        }
        public BTControl CreateCommandBTControl()
        {
            return new GradientBaloonCmdControl();
        }

        public InteractiveControl CreateInteractiveControl()
        {
            return new InteractiveGradientBaloonDllControl();
        }

        public Size ToolWindSize
        {
            get
            {
                // modifiez ici la taille que le control aura dans la fenêtre d'outil
                return new Size(130, 30);
            }
        }

        public string DefaultControlName
        {
            get
            {
                // modifiez ici le nom par défaut de l'objet lors de sa création
                return "GradientBaloon";
            }
        }

        public bool ReadInModuleGlobalInfo(XmlNode DllInfoNode)
        {
            return true;
        }

        public bool WriteOutModuleGlobalInfo(XmlDocument document, XmlNode XmlGlobalNode)
        {
            return true;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.Drawing;
using System.Xml;
using CommonLib;

namespace CtrlTwoBitmap
{
    public class DllEntryClass : IDllControlInterface
    {
        public const uint TwoBitmap_Control_ID = 110;

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
                    LangSys.Initialize(Cste.STR_DEV_LANG, m_CurLang, "CtrlTwoBitmap");
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
            TwoImageRes.InitializeBitmap();
        }

        public uint DllID
        {
            get
            {
                return TwoBitmap_Control_ID;
            }
        }

        public BTControl CreateBTControl(BTDoc document)
        {
            return new BTTwoBitmapControl(document);
        }

        public BTControl CreateBTControl(BTDoc document, InteractiveControl iCtrl)
        {
            return new BTTwoBitmapControl(document, iCtrl);
        }
        public BTControl CreateCommandBTControl(BTDoc document)
        {
            return new TwoBitmapControl(document);
        }

        public InteractiveControl CreateInteractiveControl()
        {
            return new InteractiveTwoBitmap();
        }

        public Size ToolWindSize
        {
            get
            {
                return new Size(130, 30);
            }
        }
        public string DefaultControlName 
        {
            get
            {
                return "TwoBitmap";
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

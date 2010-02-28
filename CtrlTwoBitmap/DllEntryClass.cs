using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.Drawing;
using CommonLib;

namespace CtrlTwoBitmap
{
    [Serializable]
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
                LangSys.Initialize("EN", m_CurLang, "CtrlTwoBitmap");
            }
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

        public BTControl CreateBTControl()
        {
            return new BTTwoBitmapControl();
        }

        public BTControl CreateBTControl(InteractiveControl iCtrl)
        {
            return new BTTwoBitmapControl(iCtrl);
        }
        public BTControl CreateCommandBTControl()
        {
            return new TwoBitmapControl();
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

    }
}

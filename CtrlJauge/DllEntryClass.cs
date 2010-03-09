using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.Drawing;
using CommonLib;

namespace CtrlJauge
{
    public class DllEntryClass : IDllControlInterface
    {
        // changes ici l'identifiant unique de la DLL
        public const uint DLL_Control_ID = 150;

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
                    LangSys.Initialize(Cste.STR_DEV_LANG, m_CurLang, "CtrlJauge");
                else
                    LangSys.ChangeLangage(value);
            }
        }

        public DllEntryClass()
        {
            CtrlJaugeRes.InitializeBitmap();
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
            return new BTDllCtrlJaugeControl();
        }

        public BTControl CreateBTControl(InteractiveControl iCtrl)
        {
            return new BTDllCtrlJaugeControl(iCtrl);
        }
        public BTControl CreateCommandBTControl()
        {
            return new CtrlJaugeCmdControl();
        }

        public InteractiveControl CreateInteractiveControl()
        {
            return new InteractiveCtrlJaugeDllControl();
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
                return "Gauge";
            }
        }

    }
}

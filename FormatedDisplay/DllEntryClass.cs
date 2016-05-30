/*
    This file is part of SmartApp.

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
using System.Windows.Forms;
using System.Reflection;
using System.Drawing;
using System.Xml;
using CommonLib;

namespace FormatedDisplay
{
    public class DllEntryClass : IDllControlInterface
    {
        // changes ici l'identifiant unique de la DLL
        public const uint DLL_Control_ID = 140;

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
                    LangSys.Initialize(Cste.STR_DEV_LANG, m_CurLang, "CtrlFormatedDisplay");
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
            FormatedDisplayRes.InitializeBitmap();
        }

        public uint DllID
        {
            get
            {
                return DLL_Control_ID;
            }
        }

        public BTControl CreateBTControl(BTDoc document)
        {
            return new BTDllFormatedDisplayControl(document);
        }

        public BTControl CreateBTControl(BTDoc document, InteractiveControl iCtrl)
        {
            return new BTDllFormatedDisplayControl(document, iCtrl);
        }
        public BTControl CreateCommandBTControl(BTDoc document)
        {
            return new FormatedDisplayCmdControl(document);
        }

        public InteractiveControl CreateInteractiveControl()
        {
            return new InteractiveFormatedDisplayDllControl();
        }

        public Size ToolWindSize
        {
            get
            {
                // modifiez ici la taille que le control aura dans la fenêtre d'outil
                return new Size(130, 20);
            }
        }

        public string DefaultControlName
        {
            get
            {
                // modifiez ici le nom par défaut de l'objet lors de sa création
                return "FormatedDiplay";
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

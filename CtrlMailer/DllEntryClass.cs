﻿/*
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
using System.Windows.Forms;
using System.Reflection;
using System.Drawing;
using System.Xml;
using System.Web;
using CommonLib;

namespace CtrlMailer
{
    public class DllEntryClass : IDllControlInterface
    {
        // changes ici l'identifiant unique de la DLL
        public const uint DLL_Control_ID = 250;

        static MailerGlobals gSMTP_Param = new MailerGlobals();
#if BUILD_LANG
#if TEST_LANG
        static Lang m_SingLangSys = new Lang(true, true);
#else
        static Lang m_SingLangSys = new Lang(true, false);
#endif
#else
        static Lang m_SingLangSys = new Lang();
#endif

        internal static MailerGlobals SMTP_Param
        {
            get { return gSMTP_Param; }
        }

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
                    LangSys.Initialize(Cste.STR_DEV_LANG, m_CurLang, "CtrlMailer");
                else
                    LangSys.ChangeLangage(value);
            }
        }

        public string PluginType
        {
            get { return LangSys.C("3 - Functional"); }
        }


        public DllEntryClass()
        {
            CtrlMailerRes.InitializeBitmap();
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
            return new BTDllCtrlMailerControl(document);
        }

        public BTControl CreateBTControl(BTDoc document, InteractiveControl iCtrl)
        {
            return new BTDllCtrlMailerControl(document , iCtrl);
        }
        public BTControl CreateCommandBTControl(BTDoc document)
        {
            return new CtrlMailerCmdControl(document);
        }

        public InteractiveControl CreateInteractiveControl()
        {
            return new InteractiveCtrlMailerDllControl();
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
                return "CtrlMailer";
            }
        }

        public bool ReadInModuleGlobalInfo(XmlNode DllInfoNode)
        {
            if (DllInfoNode.ChildNodes.Count == 1 && DllInfoNode.ChildNodes[0].Name == "smtpInfo")
            {
                XmlNode smtpNode = DllInfoNode.ChildNodes[0];
                XmlNode attrHost = smtpNode.Attributes.GetNamedItem("Host");
                XmlNode attrPort = smtpNode.Attributes.GetNamedItem("Port");
                XmlNode attrMail = smtpNode.Attributes.GetNamedItem("Mail");
                XmlNode attrPass = smtpNode.Attributes.GetNamedItem("Passwd");
                XmlNode attrSSL = smtpNode.Attributes.GetNamedItem("useSSL");
                gSMTP_Param.SMTP_host = attrHost.Value;
                gSMTP_Param.SMTP_port = int.Parse(attrPort.Value);
                gSMTP_Param.userMail = attrMail.Value;
                // TODO, decrypter le pass
                gSMTP_Param.userPassword = HttpUtility.HtmlDecode(gSMTP_Param.Decrypt(attrPass.Value));
                gSMTP_Param.useSSL = bool.Parse(attrSSL.Value);
            }
            return true;
        }

        public bool WriteOutModuleGlobalInfo(XmlDocument document, XmlNode XmlGlobalNode)
        {
            if (gSMTP_Param.IsConfigured())
            {
                XmlElement dllNode = document.CreateElement(XML_CF_TAG.Plugin.ToString());
                XmlGlobalNode.AppendChild(dllNode);
                XmlAttribute attrID = document.CreateAttribute(XML_CF_ATTRIB.DllID.ToString());
                attrID.Value = DLL_Control_ID.ToString();
                dllNode.Attributes.Append(attrID);
                // les données doivent être ajouté sous le noeud "dllNode"
                XmlElement nodeSMTP = document.CreateElement("smtpInfo");
                dllNode.AppendChild(nodeSMTP);
                XmlAttribute attrHost = document.CreateAttribute("Host");
                XmlAttribute attrPort = document.CreateAttribute("Port");
                XmlAttribute attrMail = document.CreateAttribute("Mail");
                XmlAttribute attrPass = document.CreateAttribute("Passwd");
                XmlAttribute attrSSL = document.CreateAttribute("useSSL");
                attrHost.Value = gSMTP_Param.SMTP_host;
                attrPort.Value = gSMTP_Param.SMTP_port.ToString();
                attrMail.Value = gSMTP_Param.userMail;
                // TODO, crypter le pass
                attrPass.Value = HttpUtility.HtmlEncode(gSMTP_Param.Encrypt(gSMTP_Param.userPassword));
                attrSSL.Value = gSMTP_Param.useSSL.ToString();
                nodeSMTP.Attributes.Append(attrHost);
                nodeSMTP.Attributes.Append(attrPort);
                nodeSMTP.Attributes.Append(attrMail);
                nodeSMTP.Attributes.Append(attrPass);
                nodeSMTP.Attributes.Append(attrSSL);

            }
            return true;
        }

    }
}

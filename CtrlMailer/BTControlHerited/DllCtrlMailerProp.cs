using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Xml;
using System.IO;
using System.Windows.Forms;
using System.Web;
using CommonLib;


namespace CtrlMailer
{
    public class DllCtrlMailerProp : SpecificControlProp
    {
        private const string Tag_Section = "MailerParam";
        private const string Tag_MailTo = "MailTo";
        private const string Tag_MailSubject = "Subject";
        private const string Tag_MailBody = "Body";

        // ajouter ici les données membres des propriété
        string m_ListToMail;
        string m_MailSubject;
        string m_MailBody;

        public DllCtrlMailerProp(ItemScriptsConainter scriptContainter) : base(scriptContainter) { }

        // ajouter ici les accesseur vers les données membres des propriété
        public string ListToMail
        {
            get { return m_ListToMail; }
            set { m_ListToMail = value; }
        }

        public string MailSubject
        {
            get { return m_MailSubject; }
            set { m_MailSubject = value; }
        }

        public string MailBody
        {
            get { return m_MailBody; }
            set { m_MailBody = value; }
        }


        /// <summary>
        /// Lecture des paramètres depuis le fichier XML
        /// </summary>
        /// <param name="Node">noeud du control a qui appartiens les propriété </param>
        /// <returns>true en cas de succès de la lecture</returns>
        public override bool ReadIn(XmlNode Node, BTDoc document)
        {
            for (int iChild = 0; iChild < Node.ChildNodes.Count; iChild ++)
            {
                if (Node.ChildNodes[iChild].Name == Tag_Section)
                {
                    XmlNode SectionNode = Node.ChildNodes[iChild];
                    for (int ch = 0; ch < SectionNode.ChildNodes.Count; ch++)
                    {
                        if (SectionNode.ChildNodes[ch].Name == Tag_MailTo)
                        {
                            m_ListToMail = getXmlTextNodeValue(SectionNode.ChildNodes[ch]);
                        }
                        if (SectionNode.ChildNodes[ch].Name == Tag_MailSubject)
                        {
                            m_MailSubject = HttpUtility.HtmlDecode(getXmlTextNodeValue(SectionNode.ChildNodes[ch]));
                        }
                        if (SectionNode.ChildNodes[ch].Name == Tag_MailBody)
                        {
                            m_MailBody = HttpUtility.HtmlDecode(getXmlTextNodeValue(SectionNode.ChildNodes[ch]));
                        }
                    }
                }
            }

            return true;
        }

        private string getXmlTextNodeValue(XmlNode node)
        {
            if (node.FirstChild is XmlText)
            {
                return node.FirstChild.Value;
            }
            return string.Empty;
        }

        /// <summary>
        /// écriture des paramètres dans le fichier XML
        /// </summary>
        /// <param name="XmlDoc">Document XML</param>
        /// <param name="Node">noeud du control a qui appartiens les propriété</param>
        /// <returns>true en cas de succès de l'écriture</returns>
        public override bool WriteOut(XmlDocument XmlDoc, XmlNode Node, BTDoc document)
        {
            XmlNode ParamNode = XmlDoc.CreateElement(Tag_Section);
            XmlNode MailToNode = XmlDoc.CreateElement(Tag_MailTo);
            XmlNode SubjectNode = XmlDoc.CreateElement(Tag_MailSubject);
            XmlNode BodyNode = XmlDoc.CreateElement(Tag_MailBody);
            Node.AppendChild(ParamNode);
            ParamNode.AppendChild(MailToNode);
            ParamNode.AppendChild(SubjectNode);
            ParamNode.AppendChild(BodyNode);
            MailToNode.AppendChild(XmlDoc.CreateTextNode(m_ListToMail));
            SubjectNode.AppendChild(XmlDoc.CreateTextNode(HttpUtility.HtmlEncode(m_MailSubject)));
            BodyNode.AppendChild(XmlDoc.CreateTextNode(HttpUtility.HtmlEncode(m_MailBody)));
            return true;
        }

        /// <summary>
        /// Recopie les paramètres d'un control source du même type vers les paramètres courants
        /// </summary>
        /// <param name="SrcSpecificProp">Paramètres sources</param>
        public override void CopyParametersFrom(SpecificControlProp SrcSpecificProp, bool bFromOtherInstance, BTDoc document)
        {
            DllCtrlMailerProp SrcProp = (DllCtrlMailerProp)SrcSpecificProp;
            m_ListToMail = SrcProp.m_ListToMail;
            m_MailSubject = SrcProp.m_MailSubject;
            m_MailBody = SrcProp.m_MailBody;
        }

        /// <summary>
        /// Traite les message intra applicatif de SmartConfig
        /// Ces messages informes de : 
        /// - demande de suppression (non confirmée) : il faut créer un message pour informer l'utlisateur
        /// - Supression de confirmée : il faut supprimer le paramètre concerné
        /// - renommage : il faut mettre a jour le paramètre concerné
        /// </summary>
        /// <param name="Mess">Type du message</param>
        /// <param name="obj">paramètre du message (objet paramètre du message de type 
        /// MessAskDelete / MessDeleted / MessItemRenamed)</param>
        /// <param name="TypeApp">Type de l'application courante (SmartConfig / SmartCommand)</param>
        /// <param name="PropOwner">control propriétaire des propriété spécifique</param>
        public override void TraiteMessage(MESSAGE Mess, object obj, TYPE_APP TypeApp, BTControl PropOwner)
        {
            if (TypeApp == TYPE_APP.SMART_CONFIG)
            {
                switch (Mess)
                {
                    case MESSAGE.MESS_ASK_ITEM_DELETE:
                        if (((MessAskDelete)obj).TypeOfItem == typeof(Data))
                        {
                            MessAskDelete MessParam = (MessAskDelete)obj;
                            if (m_MailBody.Contains(MessParam.WantDeletetItemSymbol))
                            {
                                string strMess = string.Format("Mailer {0} : Data is present in mail body and will be removed", PropOwner.Symbol);
                                MessParam.ListStrReturns.Add(strMess);
                            }
                        }
                        break;
                    case MESSAGE.MESS_ITEM_DELETED:
                        if (((MessDeleted)obj).TypeOfItem == typeof(Data))
                        {
                            MessDeleted MessParam = (MessDeleted)obj;
                            if (m_MailBody.Contains(MessParam.DeletetedItemSymbol))
                            {
                                m_MailBody.Replace(MessParam.DeletetedItemSymbol, "");
                            }
                        }
                        break;
                    case MESSAGE.MESS_ITEM_RENAMED:
                        if (((MessItemRenamed)obj).TypeOfItem == typeof(Data))
                        {
                            MessItemRenamed MessParam = (MessItemRenamed)obj;
                            if (m_MailBody.Contains(MessParam.OldItemSymbol))
                            {
                                m_MailBody.Replace(MessParam.OldItemSymbol, MessParam.NewItemSymbol);
                            }
                        }
                        break;
                    default:
                        break;
                }
            }
        }

    }
}

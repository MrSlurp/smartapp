using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;
using System.Windows.Forms;
using CommonLib;


namespace PasswordControler
{
    internal class DllPasswordControlerProp : SpecificControlProp
    {
        // ajouter ici les données membres des propriété
        string m_PasswordHash = string.Empty;
        private const string NOM_ATTIB_0 = "Hash";

        // ajouter ici les accesseur vers les données membres des propriété
        public string PasswordHash
        {
            get { return m_PasswordHash; }
            set { m_PasswordHash = value; }
        }

        /// <summary>
        /// Lecture des paramètres depuis le fichier XML
        /// </summary>
        /// <param name="Node">noeud du control a qui appartiens les propriété </param>
        /// <returns>true en cas de succès de la lecture</returns>
        public override bool ReadIn(XmlNode Node)
        {
            XmlNode Attr0 = Node.Attributes.GetNamedItem(NOM_ATTIB_0);
            if (Attr0 == null )
                return false;
            PasswordHash = Attr0.Value;
            return true;
        }

        /// <summary>
        /// écriture des paramètres dans le fichier XML
        /// </summary>
        /// <param name="XmlDoc">Document XML</param>
        /// <param name="Node">noeud du control a qui appartiens les propriété</param>
        /// <returns>true en cas de succès de l'écriture</returns>
        public override bool WriteOut(XmlDocument XmlDoc, XmlNode Node)
        {
            XmlAttribute Attr0 = XmlDoc.CreateAttribute(NOM_ATTIB_0);
            Attr0.Value = PathTranslator.AbsolutePathToRelative(PasswordHash);
            Node.Attributes.Append(Attr0);
            return true;
        }

        /// <summary>
        /// Recopie les paramètres d'un control source du même type vers les paramètres courants
        /// </summary>
        /// <param name="SrcSpecificProp">Paramètres sources</param>
        public override void CopyParametersFrom(SpecificControlProp SrcSpecificProp, bool bFromOtherInstance)
        {
            DllPasswordControlerProp SrcProp = (DllPasswordControlerProp)SrcSpecificProp;
            this.PasswordHash = SrcProp.PasswordHash;
            if (bFromOtherInstance)
            {
                //rien a copier
            }
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
                        // exemple de traitement de la demande de supression d'une donnée
                        // m_strDataOffToOn est le symbol d'une donnée
                        /*
                        if (((MessAskDelete)obj).TypeOfItem == typeof(Data))
                        {
                            if (MessParam.WantDeletetItemSymbol == m_strDataOffToOn)
                            {
                                strMess = string.Format("Data Trigger {0} : Data \"Off to On\" will be removed", PropOwner.Symbol);
                                MessParam.ListStrReturns.Add(strMess);
                            }
                        }
                         * */
                        break;
                    case MESSAGE.MESS_ITEM_DELETED:
                        // exemple de traitement de la supression d'une donnée
                        // m_strDataOffToOn est le symbol d'une donnée
                        /*
                        if (((MessDeleted)obj).TypeOfItem == typeof(Data))
                        {
                            MessDeleted MessParam = (MessDeleted)obj;
                            if (MessParam.DeletetedItemSymbol == m_strDataOffToOn)
                            {
                                m_strDataOffToOn = string.Empty;
                            }
                        }
                         * */
                        break;
                    case MESSAGE.MESS_ITEM_RENAMED:
                        // exemple de traitement du renommage d'une donnée
                        // m_strDataOffToOn est le symbol d'une donnée
                        /*
                        if (((MessItemRenamed)obj).TypeOfItem == typeof(Data))
                        {
                            MessItemRenamed MessParam = (MessItemRenamed)obj;
                            if (MessParam.OldItemSymbol == m_strDataOffToOn)
                            {
                                m_strDataOffToOn = MessParam.NewItemSymbol;
                            }
                        }*/
                        break;
                    default:
                        break;
                }
            }
        }

    }
}

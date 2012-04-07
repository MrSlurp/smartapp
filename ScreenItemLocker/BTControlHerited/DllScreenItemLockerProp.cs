using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Xml;
using System.IO;
using System.Windows.Forms;
using CommonLib;


namespace ScreenItemLocker
{
    internal class DllScreenItemLockerProp : SpecificControlProp
    {
        #region constantes
        private const string NODE_ITEM_LIST = "ITEM_LIST";
        private const string NODE_SCREEN_ITEM = "SCREEN_ITEM";
        #endregion

        // ajouter ici les données membres des propriété
        StringCollection m_ListItemSymbol = new StringCollection();

        // ajouter ici les accesseur vers les données membres des propriété

        public DllScreenItemLockerProp(ItemScriptsConainter scriptContainter) : base(scriptContainter) { }

        public StringCollection ListItemSymbol
        {
            get { return m_ListItemSymbol; }
            set { m_ListItemSymbol = value; }
        }

        /// <summary>
        /// Lecture des paramètres depuis le fichier XML
        /// </summary>
        /// <param name="Node">noeud du control a qui appartiens les propriété </param>
        /// <returns>true en cas de succès de la lecture</returns>
        public override bool ReadIn(XmlNode Node)
        {
            if (Node.FirstChild != null)
            {
                for (int i = 0; i < Node.ChildNodes.Count ; i++)
                {
                    if (Node.ChildNodes[i].Name == NODE_ITEM_LIST)
                    {
                        XmlNode NodeList = Node.ChildNodes[i];
                        for (int NbItem = 0; NbItem < NodeList.ChildNodes.Count; NbItem++)
                        {
                            if (NodeList.ChildNodes[NbItem].Name == NODE_SCREEN_ITEM)
                            {
                                XmlNode AttrSymbol = NodeList.ChildNodes[NbItem].Attributes.GetNamedItem(XML_CF_ATTRIB.strSymbol.ToString());
                                m_ListItemSymbol.Add(AttrSymbol.Value);
                            }
                        }
                    }
                    // il pourrai y avoir d'autres types de noeuds dans le future
                }
            }
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
            XmlNode ItemListNodes = XmlDoc.CreateElement(NODE_ITEM_LIST);
            for (int i = 0; i < m_ListItemSymbol.Count; i++)
            {
                XmlNode ScreenItemNodes = XmlDoc.CreateElement(NODE_SCREEN_ITEM);
                XmlAttribute AttrSymbol = XmlDoc.CreateAttribute(XML_CF_ATTRIB.strSymbol.ToString());
                AttrSymbol.Value = m_ListItemSymbol[i];
                ScreenItemNodes.Attributes.Append(AttrSymbol);
                ItemListNodes.AppendChild(ScreenItemNodes);
            }
            Node.AppendChild(ItemListNodes);
            return true;
        }

        /// <summary>
        /// Recopie les paramètres d'un control source du même type vers les paramètres courants
        /// </summary>
        /// <param name="SrcSpecificProp">Paramètres sources</param>
        public override void CopyParametersFrom(SpecificControlProp SrcSpecificProp, bool bFromOtherInstance)
        {
            DllScreenItemLockerProp SrcProp = (DllScreenItemLockerProp)SrcSpecificProp;
            if (bFromOtherInstance)
                return;
            m_ListItemSymbol.Clear();
            foreach (string st in SrcProp.ListItemSymbol)
                m_ListItemSymbol.Add(st);
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
                        if (((MessAskDelete)obj).TypeOfItem == typeof(BTControl))
                        {
                            MessAskDelete MessParam = obj as MessAskDelete;
                            if (m_ListItemSymbol.Contains(MessParam.WantDeletetItemSymbol))
                            {
                                string strMess = string.Format("Screen item locker {0} : controled item will be removed", PropOwner.Symbol);
                                MessParam.ListStrReturns.Add(strMess);
                            }
                        }
                        break;
                    case MESSAGE.MESS_ITEM_DELETED:
                        if (((MessDeleted)obj).TypeOfItem == typeof(BTControl))
                        {
                            MessDeleted MessParam = (MessDeleted)obj;
                            if (m_ListItemSymbol.Contains(MessParam.DeletetedItemSymbol))
                            {
                                m_ListItemSymbol.Remove(MessParam.DeletetedItemSymbol);
                            }
                        }
                        break;
                    case MESSAGE.MESS_ITEM_RENAMED:
                        if (((MessItemRenamed)obj).TypeOfItem == typeof(BTControl))
                        {
                            MessItemRenamed MessParam = (MessItemRenamed)obj;
                            if (m_ListItemSymbol.Contains(MessParam.OldItemSymbol))
                            {
                                m_ListItemSymbol.Remove(MessParam.OldItemSymbol);
                                m_ListItemSymbol.Add(MessParam.NewItemSymbol);
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

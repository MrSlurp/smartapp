using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;
using System.Windows.Forms;
using CommonLib;


namespace CtrlDataComp
{
    internal enum eCompareMode
    {
        cmp_ASupB,
        cmp_ASupEqB,
        cmp_AInfB,
        cmp_AInfEqB,
        cmp_ASupBSupC,
        cmp_ASupEqBSupEqC
    }

    internal class DllCtrlDataCompProp : SpecificControlProp
    {
        private const string NODE_SECTION_COMP = "CompParam";
        private const string ATTR_MODE_COMP = "CompMode";
        private const string ATTR_DATA_A = "DataA";
        private const string ATTR_DATA_B = "DataB";
        private const string ATTR_DATA_C = "DataC";

        protected string m_sDataA;
        protected string m_sDataB;
        protected string m_sDataC;
        protected eCompareMode m_CompMode = eCompareMode.cmp_ASupB;

        public string DataA
        {
            get { return m_sDataA; }
            set { m_sDataA = value; }
        }
        public string DataB
        {
            get { return m_sDataB; }
            set { m_sDataB = value; }
        }
        public string DataC
        {
            get { return m_sDataC; }
            set { m_sDataC = value; }
        }

        public eCompareMode CompMode
        {
            get { return m_CompMode; }
            set { m_CompMode = value; }
        }

        public DllCtrlDataCompProp(ItemScriptsConainter scriptContainter)
            :base(scriptContainter)
        {

        }

        /// <summary>
        /// Lecture des paramètres depuis le fichier XML
        /// </summary>
        /// <param name="Node">noeud du control a qui appartiens les propriété </param>
        /// <returns>true en cas de succès de la lecture</returns>
        public override bool ReadIn(XmlNode Node, BTDoc document)
        {
            for (int ch = 0; ch < Node.ChildNodes.Count; ch++)
            {
                if (Node.ChildNodes[ch].Name == NODE_SECTION_COMP)
                {
                    XmlNode AttrCompMode = Node.ChildNodes[ch].Attributes.GetNamedItem(ATTR_MODE_COMP);
                    XmlNode AttrDataA = Node.ChildNodes[ch].Attributes.GetNamedItem(ATTR_DATA_A);
                    XmlNode AttrDataB = Node.ChildNodes[ch].Attributes.GetNamedItem(ATTR_DATA_B);
                    XmlNode AttrDataC = Node.ChildNodes[ch].Attributes.GetNamedItem(ATTR_DATA_C);


                    m_CompMode = (eCompareMode)int.Parse(AttrCompMode.Value);
                    m_sDataA = AttrDataA.Value;
                    m_sDataB = AttrDataB.Value;
                    m_sDataC = AttrDataC.Value;
                    break;
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
        public override bool WriteOut(XmlDocument XmlDoc, XmlNode Node, BTDoc document)
        {
            XmlNode ElemSpecSection = XmlDoc.CreateElement(NODE_SECTION_COMP);
            XmlAttribute AttrModeComp = XmlDoc.CreateAttribute(ATTR_MODE_COMP);
            XmlAttribute AttrDataA = XmlDoc.CreateAttribute(ATTR_DATA_A);
            XmlAttribute AttrDataB = XmlDoc.CreateAttribute(ATTR_DATA_B);
            XmlAttribute AttrDataC = XmlDoc.CreateAttribute(ATTR_DATA_C);
            AttrModeComp.Value = ((int)m_CompMode).ToString();
            AttrDataA.Value = m_sDataA;
            AttrDataB.Value = m_sDataB;
            AttrDataC.Value = m_sDataC;

            ElemSpecSection.Attributes.Append(AttrModeComp);
            ElemSpecSection.Attributes.Append(AttrDataA);
            ElemSpecSection.Attributes.Append(AttrDataB);
            ElemSpecSection.Attributes.Append(AttrDataC);
            Node.AppendChild(ElemSpecSection);
            return true;
        }

        /// <summary>
        /// Recopie les paramètres d'un control source du même type vers les paramètres courants
        /// </summary>
        /// <param name="SrcSpecificProp">Paramètres sources</param>
        public override void  CopyParametersFrom(SpecificControlProp SrcSpecificProp, bool bFromOtherInstance, BTDoc document)
        {
            if (SrcSpecificProp is DllCtrlDataCompProp)
            {
                DllCtrlDataCompProp SrcProp = SrcSpecificProp as DllCtrlDataCompProp;
                m_CompMode = SrcProp.m_CompMode;
                m_sDataA = BTControl.CheckAndDoAssociateDataCopy(document, SrcProp.m_sDataA);
                m_sDataB = BTControl.CheckAndDoAssociateDataCopy(document, SrcProp.m_sDataB);
                m_sDataC = BTControl.CheckAndDoAssociateDataCopy(document, SrcProp.m_sDataC);
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
                        if (((MessAskDelete)obj).TypeOfItem == typeof(Data))
                        {
                            MessAskDelete MessParam = (MessAskDelete)obj;
                            string strMess = string.Empty;
                            if (MessParam.WantDeletetItemSymbol == m_sDataA)
                            {
                                strMess = string.Format("Data Comparer {0} : Data A will be removed", PropOwner.Symbol);
                                MessParam.ListStrReturns.Add(strMess);
                            }
                            if (MessParam.WantDeletetItemSymbol == m_sDataB)
                            {
                                strMess = string.Format("Data Comparer {0} : Data A will be removed", PropOwner.Symbol);
                                MessParam.ListStrReturns.Add(strMess);
                            }
                            if (MessParam.WantDeletetItemSymbol == m_sDataB)
                            {
                                strMess = string.Format("Data Comparer {0} : Data A will be removed", PropOwner.Symbol);
                                MessParam.ListStrReturns.Add(strMess);
                            }
                        }
                        break;
                    case MESSAGE.MESS_ITEM_DELETED:
                        if (((MessDeleted)obj).TypeOfItem == typeof(Data))
                        {
                            MessDeleted MessParam = (MessDeleted)obj;
                            if (MessParam.DeletetedItemSymbol == m_sDataA)
                            {
                                m_sDataA = string.Empty;
                            }
                            if (MessParam.DeletetedItemSymbol == m_sDataB)
                            {
                                m_sDataB = string.Empty;
                            }
                            if (MessParam.DeletetedItemSymbol == m_sDataC)
                            {
                                m_sDataC = string.Empty;
                            }
                        }
                        break;
                    case MESSAGE.MESS_ITEM_RENAMED:
                        if (((MessItemRenamed)obj).TypeOfItem == typeof(Data))
                        {
                            MessItemRenamed MessParam = (MessItemRenamed)obj;
                            if (MessParam.OldItemSymbol == m_sDataA)
                            {
                                m_sDataA = MessParam.NewItemSymbol;
                            }
                            if (MessParam.OldItemSymbol == m_sDataB)
                            {
                                m_sDataB = MessParam.NewItemSymbol;
                            }
                            if (MessParam.OldItemSymbol == m_sDataC)
                            {
                                m_sDataC = MessParam.NewItemSymbol;
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

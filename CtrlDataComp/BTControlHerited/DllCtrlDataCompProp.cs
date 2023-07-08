/*
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
                BTControl.TraiteMessageDataDelete(Mess, obj, m_sDataA, PropOwner, DllEntryClass.LangSys.C("Data Comparer {0} : Data A will be removed"));
                BTControl.TraiteMessageDataDelete(Mess, obj, m_sDataB, PropOwner, DllEntryClass.LangSys.C("Data Comparer {0} : Data B will be removed"));
                BTControl.TraiteMessageDataDelete(Mess, obj, m_sDataC, PropOwner, DllEntryClass.LangSys.C("Data Comparer {0} : Data C will be removed"));

                m_sDataA = BTControl.TraiteMessageDataDeleted(Mess, obj, m_sDataA);
                m_sDataB = BTControl.TraiteMessageDataDeleted(Mess, obj, m_sDataB);
                m_sDataC = BTControl.TraiteMessageDataDeleted(Mess, obj, m_sDataC);

                m_sDataA = BTControl.TraiteMessageDataRenamed(Mess, obj, m_sDataA);
                m_sDataB = BTControl.TraiteMessageDataRenamed(Mess, obj, m_sDataB);
                m_sDataC = BTControl.TraiteMessageDataRenamed(Mess, obj, m_sDataC);

            }
        }
    }
}

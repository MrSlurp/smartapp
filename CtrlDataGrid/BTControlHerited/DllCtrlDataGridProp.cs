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
using System.Xml;
using System.IO;
using System.Windows.Forms;
using CommonLib;


namespace CtrlDataGrid
{
    #region enums
    public enum SAVE_PERIOD
    {
        SAVE_5_min = 15,
        SAVE_10_min = 10,
        SAVE_15_min = 15,
        SAVE_1_h = 60,
        SAVE_2_h = 120,
        SAVE_6_h = 360,
        SAVE_12_h = 720,
    }

    public enum LOG_PERIOD
    {
        LOG_1_sec = 1,
        LOG_10_sec = 10,
        LOG_30_sec = 30,
        LOG_1_min = 60,
        LOG_2_min = 120,
        LOG_5_min = 300,
        LOG_10_min = 600,
        LOG_15_min = 900,
    }
    #endregion

    public class DllCtrlDataGridProp : SpecificControlProp
    {
        #region constantes
        public const int NB_DATA = 8;
        private const string NODE_DATA_ITEM = "DATA_ITEM_{0}";
        private const string ATTR_SAVE_PERIOD = "SavePeriod";
        private const string ATTR_LOG_PERIOD = "LogPeriod";
        #endregion

        // ajouter ici les données membres des propriété
        SAVE_PERIOD m_SavePeriod = SAVE_PERIOD.SAVE_1_h;
        LOG_PERIOD m_LoggingPeriod = LOG_PERIOD.LOG_1_min;
        string[] ListDataSymbol = new string[NB_DATA];
        string[] ListDataAlias = new string[NB_DATA];

        public DllCtrlDataGridProp(ItemScriptsConainter scriptContainter) : base(scriptContainter) { }

        // ajouter ici les accesseur vers les données membres des propriété
        #region attributs
        public SAVE_PERIOD SavePeriod
        {
            get
            {
                return m_SavePeriod;
            }
            set
            {
                m_SavePeriod = value;
            }
        }

        public LOG_PERIOD LogPeriod
        {
            get
            {
                return m_LoggingPeriod;
            }
            set
            {
                m_LoggingPeriod = value;
            }
        }
        #endregion

        #region accesseur des tableaux
        public string GetSymbol(int index)
        {
            return ListDataSymbol[index];
        }

        public string GetAlias(int index)
        {
            return ListDataAlias[index];
        }

        public void SetSymbol(int index, string Symbol)
        {
            ListDataSymbol[index] = Symbol;
        }

        public void SetAlias(int index, string Alias)
        {
            ListDataAlias[index] = Alias;
        }
        #endregion

        /// <summary>
        /// Lecture des paramètres depuis le fichier XML
        /// </summary>
        /// <param name="Node">noeud du control a qui appartiens les propriété </param>
        /// <returns>true en cas de succès de la lecture</returns>
        public override bool ReadIn(XmlNode Node, BTDoc document)
        {
            int NodeGraphItemCount = 0;
            if (Node.FirstChild != null)
            {
                for (int ch = 0; ch < Node.ChildNodes.Count && NodeGraphItemCount < NB_DATA; ch++)
                {
                    string strNodeItemName = string.Format(NODE_DATA_ITEM, NodeGraphItemCount);
                    if (Node.ChildNodes[ch].Name == strNodeItemName)
                    {
                        XmlNode AttrSymbol = Node.ChildNodes[ch].Attributes.GetNamedItem(XML_CF_ATTRIB.strSymbol.ToString());
                        XmlNode AttrText = Node.ChildNodes[ch].Attributes.GetNamedItem(XML_CF_ATTRIB.Text.ToString());
                        ListDataSymbol[NodeGraphItemCount] = AttrSymbol.Value;
                        ListDataAlias[NodeGraphItemCount] = AttrText.Value;

                        NodeGraphItemCount++;
                        // on reprend à 0 si les items ne sont pas dans le bon ordre
                        ch = 0;
                    }
                }
            }
            XmlNode AttrSavePeriod = Node.Attributes.GetNamedItem(ATTR_SAVE_PERIOD);
            XmlNode AttrLogPeriod = Node.Attributes.GetNamedItem(ATTR_LOG_PERIOD);
            if (AttrLogPeriod != null)
                m_LoggingPeriod = (LOG_PERIOD)int.Parse(AttrLogPeriod.Value);
            if (AttrSavePeriod != null)
                m_SavePeriod = (SAVE_PERIOD)int.Parse(AttrSavePeriod.Value);

            return true;        }

        /// <summary>
        /// écriture des paramètres dans le fichier XML
        /// </summary>
        /// <param name="XmlDoc">Document XML</param>
        /// <param name="Node">noeud du control a qui appartiens les propriété</param>
        /// <returns>true en cas de succès de l'écriture</returns>
        public override bool WriteOut(XmlDocument XmlDoc, XmlNode Node, BTDoc document)
        {
            for (int i = 0; i < NB_DATA; i++)
            {
                XmlNode CurvPropNode = XmlDoc.CreateElement(string.Format(NODE_DATA_ITEM, i));
                Traces.LogAddDebug(TraceCat.Serialization, "Ecriture de la donnée" + i + "du DataGrid");
                XmlAttribute AttrSymbol = XmlDoc.CreateAttribute(XML_CF_ATTRIB.strSymbol.ToString());
                XmlAttribute AttrText = XmlDoc.CreateAttribute(XML_CF_ATTRIB.Text.ToString());
                AttrSymbol.Value = ListDataSymbol[i];
                AttrText.Value = ListDataAlias[i];
                CurvPropNode.Attributes.Append(AttrSymbol);
                CurvPropNode.Attributes.Append(AttrText);
                Node.AppendChild(CurvPropNode);
            }
            XmlAttribute AttrSavePeriod = XmlDoc.CreateAttribute(ATTR_SAVE_PERIOD);
            XmlAttribute AttrLogPeriod = XmlDoc.CreateAttribute(ATTR_LOG_PERIOD);
            AttrSavePeriod.Value = ((int)m_SavePeriod).ToString();
            AttrLogPeriod.Value = ((int)m_LoggingPeriod).ToString();

            Node.Attributes.Append(AttrSavePeriod);
            Node.Attributes.Append(AttrLogPeriod);

            return true;
        }

        /// <summary>
        /// Recopie les paramètres d'un control source du même type vers les paramètres courants
        /// </summary>
        /// <param name="SrcSpecificProp">Paramètres sources</param>
        public override void CopyParametersFrom(SpecificControlProp SrcSpecificProp, bool bFromOtherInstance, BTDoc document)
        {
            if (SrcSpecificProp is DllCtrlDataGridProp)
            {
                DllCtrlDataGridProp SpecProp = SrcSpecificProp as DllCtrlDataGridProp;
                for (int i = 0; i < NB_DATA; i++)
                {
                    ListDataSymbol[i] = BTControl.CheckAndDoAssociateDataCopy(document, SpecProp.ListDataSymbol[i]);
                    if (!string.IsNullOrEmpty(ListDataSymbol[i]))
                        ListDataAlias[i] = SpecProp.ListDataAlias[i];
                }
                m_SavePeriod = SpecProp.m_SavePeriod;
                m_LoggingPeriod = SpecProp.m_LoggingPeriod;
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
                for (int i = 0; i < NB_DATA; i++)
                {
                    BTControl.TraiteMessageDataDelete(Mess, obj, ListDataSymbol[i], PropOwner, DllEntryClass.LangSys.C("DataGrid {0} will lost data"));
                    ListDataSymbol[i] = BTControl.TraiteMessageDataDeleted(Mess, obj, ListDataSymbol[i]);
                    ListDataSymbol[i] = BTControl.TraiteMessageDataRenamed(Mess, obj, ListDataSymbol[i]);
                }
            }
        }
    }
}

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
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Xml;
using System.IO;
using System.Windows.Forms;
using CommonLib;


namespace CtrlGraph
{
    #region enums
    public enum SAVE_PERIOD
    {
        SAVE_10_min = 10,
        SAVE_1_h = 60,
        SAVE_2_h = 120,
        SAVE_6_h = 360,
        SAVE_12_h = 720,
        SAVE_1_j = 1440,
        SAVE_2_j = 2880,
        SAVE_4_j = 5760,
        SAVE_7_j = 10080,
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
    }
    #endregion

    public class DllCtrlGraphProp : SpecificControlProp
    {
        #region constantes
        public const int NB_CURVE = 8;
        private const string NODE_GRAPH_ITEM = "GRAPH_ITEM_{0}";
        private const string NODE_TITLE_ITEM = "GRAPH_TITLES";
        private const string ATTR_SAVE_PERIOD = "SavePeriod";
        private const string ATTR_LOG_PERIOD = "LogPeriod";
        private const string ATTR_MTITLE = "GraphTitle";
        private const string ATTR_XTITLE = "XTitle";
        private const string ATTR_YTITLE = "YTitle";
        #endregion

        #region données membres
        string strGraphTitle;
        string strXAxisTitle;
        string strYAxisTitle;

        // ajouter ici les données membres des propriété
        string[] ListDataSymbol = new string[NB_CURVE];
        string[] ListDataAlias = new string[NB_CURVE];
        int[] ListDataDivisor = new int[NB_CURVE];
        Color[] ListCurveColor = new Color[NB_CURVE];

        SAVE_PERIOD m_SavePeriod= SAVE_PERIOD.SAVE_1_h;
        LOG_PERIOD m_LoggingPeriod = LOG_PERIOD.LOG_1_min;
        #endregion

        public DllCtrlGraphProp(ItemScriptsConainter scriptContainter) : base(scriptContainter) { }

        #region attributs
        public string GraphTitle
        {
            get
            {
                return strGraphTitle;
            }
            set
            {
                strGraphTitle = value;
            }
        }

        public string XAxisTitle
        {
            get
            {
                return strXAxisTitle;
            }
            set
            {
                strXAxisTitle = value;
            }
        }

        public string YAxisTitle
        {
            get
            {
                return strYAxisTitle;
            }
            set
            {
                strYAxisTitle = value;
            }
        }

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

        public Color GetColor(int index)
        {
            return ListCurveColor[index];
        }

        public int GetDataDivisor(int index)
        {

            return ListDataDivisor[index];
        }
        public void SetDataDivisor(int index, int value)
        {
            ListDataDivisor[index] = value;
        }

        public void SetSymbol(int index, string Symbol)
        {
            ListDataSymbol[index] = Symbol;
        }

        public void SetAlias(int index, string Alias)
        {
            ListDataAlias[index] = Alias;
        }

        public void SetColor(int index, Color cr)
        {
            ListCurveColor[index] = cr;
        }
        #endregion

        #region ReadIn / WriteOut
        public override bool ReadIn(XmlNode Node, BTDoc document)
        {
            int NodeGraphItemCount = 0;
            if (Node.FirstChild != null)
            {
                for (int ch = 0; ch < Node.ChildNodes.Count && NodeGraphItemCount < NB_CURVE; ch++)
                {
                    string strNodeItemName = string.Format(NODE_GRAPH_ITEM, NodeGraphItemCount);
                    if (Node.ChildNodes[ch].Name == strNodeItemName)
                    {
                        XmlNode AttrSymbol = Node.ChildNodes[ch].Attributes.GetNamedItem(XML_CF_ATTRIB.strSymbol.ToString());
                        XmlNode AttrText = Node.ChildNodes[ch].Attributes.GetNamedItem(XML_CF_ATTRIB.Text.ToString());
                        XmlNode AttrColor = Node.ChildNodes[ch].Attributes.GetNamedItem(XML_CF_ATTRIB.bkColor.ToString());
                        XmlNode AttrDivisor = Node.ChildNodes[ch].Attributes.GetNamedItem("Divisor");
                        ListDataSymbol[NodeGraphItemCount] = AttrSymbol.Value;
                        ListDataAlias[NodeGraphItemCount] = AttrText.Value;
                        ListCurveColor[NodeGraphItemCount] = ColorTranslate.StringToColor(AttrColor.Value);
                        if (AttrDivisor != null)
                            ListDataDivisor[NodeGraphItemCount] = int.Parse(AttrDivisor.Value);

                        NodeGraphItemCount++;
                        // on reprend à 0 si les items ne sont pas dans le bon ordre
                        ch = 0;
                    }
                }
                for (int ch = 0; ch < Node.ChildNodes.Count; ch++)
                {
                    if (Node.ChildNodes[ch].Name == NODE_TITLE_ITEM)
                    {
                        XmlNode AttrMTitle = Node.ChildNodes[ch].Attributes.GetNamedItem(ATTR_MTITLE);
                        XmlNode AttrXTitle = Node.ChildNodes[ch].Attributes.GetNamedItem(ATTR_XTITLE);
                        XmlNode AttrYTitle = Node.ChildNodes[ch].Attributes.GetNamedItem(ATTR_YTITLE);
                        this.strGraphTitle = AttrMTitle.Value;
                        this.strXAxisTitle = AttrXTitle.Value;
                        this.strYAxisTitle = AttrYTitle.Value;
                        break;
                    }
                }
            }
            XmlNode AttrSavePeriod = Node.Attributes.GetNamedItem(ATTR_SAVE_PERIOD);
            XmlNode AttrLogPeriod = Node.Attributes.GetNamedItem(ATTR_LOG_PERIOD);
            m_SavePeriod = (SAVE_PERIOD)int.Parse(AttrSavePeriod.Value);
            m_LoggingPeriod = (LOG_PERIOD)int.Parse(AttrLogPeriod.Value);

            return true;
        }

        public override bool WriteOut(XmlDocument XmlDoc, XmlNode Node, BTDoc document)
        {
            for (int i = 0; i < NB_CURVE; i++)
            {
                XmlNode CurvPropNode = XmlDoc.CreateElement(string.Format(NODE_GRAPH_ITEM, i));

                XmlAttribute AttrSymbol = XmlDoc.CreateAttribute(XML_CF_ATTRIB.strSymbol.ToString());
                XmlAttribute AttrText = XmlDoc.CreateAttribute(XML_CF_ATTRIB.Text.ToString());
                XmlAttribute AttrColor = XmlDoc.CreateAttribute(XML_CF_ATTRIB.bkColor.ToString());
                XmlAttribute AttrDivisor = XmlDoc.CreateAttribute("Divisor");
                AttrSymbol.Value = ListDataSymbol[i];
                AttrText.Value = ListDataAlias[i];
                AttrColor.Value = ColorTranslate.ColorToString(ListCurveColor[i]);
                AttrDivisor.Value = ListDataDivisor[i].ToString();
                CurvPropNode.Attributes.Append(AttrSymbol);
                CurvPropNode.Attributes.Append(AttrText);
                CurvPropNode.Attributes.Append(AttrColor);
                CurvPropNode.Attributes.Append(AttrDivisor);
                Node.AppendChild(CurvPropNode);
            }
            XmlNode Titles = XmlDoc.CreateElement(NODE_TITLE_ITEM);

            XmlAttribute AttrMTitle = XmlDoc.CreateAttribute(ATTR_MTITLE);
            XmlAttribute AttrXTitle = XmlDoc.CreateAttribute(ATTR_XTITLE);
            XmlAttribute AttrYTitle = XmlDoc.CreateAttribute(ATTR_YTITLE);
            AttrMTitle.Value = this.strGraphTitle;
            AttrXTitle.Value = this.strXAxisTitle;
            AttrYTitle.Value = this.strYAxisTitle;
            Titles.Attributes.Append(AttrMTitle);
            Titles.Attributes.Append(AttrXTitle);
            Titles.Attributes.Append(AttrYTitle);
            Node.AppendChild(Titles);

            XmlAttribute AttrSavePeriod = XmlDoc.CreateAttribute(ATTR_SAVE_PERIOD);
            XmlAttribute AttrLogPeriod = XmlDoc.CreateAttribute(ATTR_LOG_PERIOD);
            AttrSavePeriod.Value = ((int)m_SavePeriod).ToString();
            AttrLogPeriod.Value = ((int)m_LoggingPeriod).ToString();

            Node.Attributes.Append(AttrSavePeriod);
            Node.Attributes.Append(AttrLogPeriod);

            return true;
        }
        #endregion

        #region copie des paramètres
        /// <summary>
        /// 
        /// </summary>
        /// <param name="SrcSpecificProp"></param>
        public override void CopyParametersFrom(SpecificControlProp SrcSpecificProp, bool bFromOtherInstance, BTDoc document)
        {
            if (SrcSpecificProp is DllCtrlGraphProp)
            {
                DllCtrlGraphProp specProps = SrcSpecificProp as DllCtrlGraphProp;
                for (int i = 0; i < NB_CURVE; i++)
                {
                    ListDataSymbol[i] = BTControl.CheckAndDoAssociateDataCopy(document, specProps.ListDataSymbol[i]);
                    if (!string.IsNullOrEmpty(ListDataSymbol[i]))
                    {
                        ListDataAlias[i] = specProps.ListDataAlias[i];
                        ListCurveColor[i] = specProps.ListCurveColor[i];
                        ListDataDivisor[i] = specProps.ListDataDivisor[i];
                    }
                }
                m_SavePeriod = specProps.m_SavePeriod;
                m_LoggingPeriod = specProps.m_LoggingPeriod;
                strGraphTitle = specProps.strGraphTitle;
                strXAxisTitle = specProps.strXAxisTitle;
                strYAxisTitle = specProps.strYAxisTitle;
            }
        }
        #endregion

        public override void TraiteMessage(MESSAGE Mess, object obj, TYPE_APP TypeApp, BTControl PropOwner)
        {
            if (TypeApp == TYPE_APP.SMART_CONFIG)
            {
                for (int i = 0; i < DllCtrlGraphProp.NB_CURVE; i++)
                {
                    BTControl.TraiteMessageDataDelete(Mess, obj, ListDataSymbol[i], PropOwner, DllEntryClass.LangSys.C("Graphic {0} will lost data"));
                    ListDataSymbol[i] = BTControl.TraiteMessageDataDeleted(Mess, obj, ListDataSymbol[i] );
                    ListDataSymbol[i] = BTControl.TraiteMessageDataRenamed(Mess, obj, ListDataSymbol[i]);
                }

            }
        }
    }
}

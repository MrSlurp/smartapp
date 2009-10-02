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
        public const int NB_CURVE = 4;
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
        Color[] ListCurveColor = new Color[NB_CURVE];

        SAVE_PERIOD m_SavePeriod;
        LOG_PERIOD m_LoggingPeriod;
        #endregion

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
        public override bool ReadIn(XmlNode Node)
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
                        ListDataSymbol[NodeGraphItemCount] = AttrSymbol.Value;
                        ListDataAlias[NodeGraphItemCount] = AttrText.Value;
                        ListCurveColor[NodeGraphItemCount] = ColorTranslate.StringToColor(AttrColor.Value);

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

        public override bool WriteOut(XmlDocument XmlDoc, XmlNode Node)
        {
            for (int i = 0; i < NB_CURVE; i++)
            {
                XmlNode CurvPropNode = XmlDoc.CreateElement(string.Format(NODE_GRAPH_ITEM, i));

                XmlAttribute AttrSymbol = XmlDoc.CreateAttribute(XML_CF_ATTRIB.strSymbol.ToString());
                XmlAttribute AttrText = XmlDoc.CreateAttribute(XML_CF_ATTRIB.Text.ToString());
                XmlAttribute AttrColor = XmlDoc.CreateAttribute(XML_CF_ATTRIB.bkColor.ToString());
                AttrSymbol.Value = ListDataSymbol[i];
                AttrText.Value = ListDataAlias[i];
                AttrColor.Value = ColorTranslate.ColorToString(ListCurveColor[i]);
                CurvPropNode.Attributes.Append(AttrSymbol);
                CurvPropNode.Attributes.Append(AttrText);
                CurvPropNode.Attributes.Append(AttrColor);
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
        public override void CopyParametersFrom(SpecificControlProp SrcSpecificProp)
        {
            if (SrcSpecificProp.GetType() == typeof(DllCtrlGraphProp))
            {
                for (int i = 0; i < NB_CURVE; i++)
                {
                    ListDataSymbol[i] = ((DllCtrlGraphProp)SrcSpecificProp).ListDataSymbol[i];
                    ListDataAlias[i] = ((DllCtrlGraphProp)SrcSpecificProp).ListDataAlias[i];
                    ListCurveColor[i] = ((DllCtrlGraphProp)SrcSpecificProp).ListCurveColor[i];
                }
                m_SavePeriod = ((DllCtrlGraphProp)SrcSpecificProp).m_SavePeriod;
                m_LoggingPeriod = ((DllCtrlGraphProp)SrcSpecificProp).m_LoggingPeriod;
                strGraphTitle = ((DllCtrlGraphProp)SrcSpecificProp).strGraphTitle;
                strXAxisTitle = ((DllCtrlGraphProp)SrcSpecificProp).strXAxisTitle;
                strYAxisTitle = ((DllCtrlGraphProp)SrcSpecificProp).strYAxisTitle;
            }
        }
        #endregion

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
                            for (int i = 0; i < DllCtrlGraphProp.NB_CURVE; i++)
                            {
                                if (ListDataSymbol[i] == MessParam.WantDeletetItemSymbol)
                                {
                                    string strMess = string.Format("Graphic {0} will lost data", PropOwner.Symbol);
                                    MessParam.ListStrReturns.Add(strMess);
                                }
                            }
                        }
                        break;
                    case MESSAGE.MESS_ITEM_DELETED:
                        if (((MessDeleted)obj).TypeOfItem == typeof(Data))
                        {
                            MessDeleted MessParam = (MessDeleted)obj;
                            for (int i = 0; i < DllCtrlGraphProp.NB_CURVE; i++)
                            {
                                if (ListDataSymbol[i] == MessParam.DeletetedItemSymbol)
                                {
                                    ListDataSymbol[i] = string.Empty;
                                }
                            }
                        }
                        break;
                    case MESSAGE.MESS_ITEM_RENAMED:
                        if (((MessItemRenamed)obj).TypeOfItem == typeof(Data))
                        {
                            MessItemRenamed MessParam = (MessItemRenamed)obj;
                            for (int i = 0; i < DllCtrlGraphProp.NB_CURVE; i++)
                            {
                                if (ListDataSymbol[i] == MessParam.OldItemSymbol)
                                {
                                    ListDataSymbol[i] = MessParam.NewItemSymbol;
                                }
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

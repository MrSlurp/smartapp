using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace CommonLib
{
    public class GestTimer : BaseGest
    {
        #region fonction "utilitaires"
        //*****************************************************************************************************
        // Description: renvoie le prochain symbol par défaut pour une donnée
        // Return: /
        //*****************************************************************************************************
        public override string GetNextDefaultSymbol()
        {
            for (int i = 0; i < MAX_DEFAULT_ITEM_SYMBOL; i++)
            {
                string strSymbolTemp = string.Format(DEFAULT_TIMER_SYMB, i);
                if (GetFromSymbol(strSymbolTemp) == null)
                {
                    return strSymbolTemp;
                }
            }
            return "";
        }
        #endregion

        #region ReadIn  / WriteOut
        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public override bool ReadIn(XmlNode Node, TYPE_APP TypeApp)
        {
            XmlNode NodeTimerSection = null;
            for (int i = 0; i < Node.ChildNodes.Count; i++)
            {
                if (Node.ChildNodes[i].Name == XML_CF_TAG.TimerSection.ToString())
                    NodeTimerSection = Node.ChildNodes[i];
            }
            if (NodeTimerSection == null)
                return false;

            for (int i = 0; i < NodeTimerSection.ChildNodes.Count; i++)
            {
                XmlNode ChildNode = NodeTimerSection.ChildNodes[i];
                if (ChildNode.Name != XML_CF_TAG.Timer.ToString())
                    continue;

                BTTimer NewTimer = new BTTimer();
                if (NewTimer != null)
                {
                    if (!NewTimer.ReadIn(ChildNode, TypeApp))
                        return false;

                    this.AddObj(NewTimer);
                }
            }
            return true;
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public override bool WriteOut(XmlDocument XmlDoc, XmlNode Node)
        {
            XmlNode NodeTimerSection = XmlDoc.CreateElement(XML_CF_TAG.TimerSection.ToString());
            Node.AppendChild(NodeTimerSection);
            for (int i = 0; i < this.m_ListObject.Count; i++)
            {
                BTTimer dt = (BTTimer)m_ListObject[i];

                XmlNode XmlTimer = XmlDoc.CreateElement(XML_CF_TAG.Timer.ToString());
                dt.WriteOut(XmlDoc, XmlTimer);
                NodeTimerSection.AppendChild(XmlTimer);
            }
            return true;
        }
        #endregion
    }
}

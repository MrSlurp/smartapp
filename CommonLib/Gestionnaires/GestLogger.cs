using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace CommonLib
{
    public class GestLogger : BaseGest
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
                string strSymbolTemp = string.Format(DEFAULT_LOGGER_SYMB, i);
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
            XmlNode NodeLoggerSection = null;
            for (int i = 0; i < Node.ChildNodes.Count; i++)
            {
                if (Node.ChildNodes[i].Name == XML_CF_TAG.LoggerSection.ToString())
                    NodeLoggerSection = Node.ChildNodes[i];
            }
            if (NodeLoggerSection == null)
                return false;

            for (int i = 0; i < NodeLoggerSection.ChildNodes.Count; i++)
            {
                XmlNode ChildNode = NodeLoggerSection.ChildNodes[i];
                if (ChildNode.Name != XML_CF_TAG.Logger.ToString())
                    continue;

                Logger NewLogger = new Logger();
                if (NewLogger != null)
                {
                    if (!NewLogger.ReadIn(ChildNode, TypeApp))
                        return false;

                    this.AddObj(NewLogger);
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
            XmlNode NodeLoggerSection = XmlDoc.CreateElement(XML_CF_TAG.LoggerSection.ToString());
            Node.AppendChild(NodeLoggerSection);
            for (int i = 0; i < this.m_ListObject.Count; i++)
            {
                Logger dt = (Logger)m_ListObject[i];

                XmlNode XmlLogger = XmlDoc.CreateElement(XML_CF_TAG.Logger.ToString());
                dt.WriteOut(XmlDoc, XmlLogger);
                NodeLoggerSection.AppendChild(XmlLogger);
            }
            return true;
        }
        #endregion
    }
}

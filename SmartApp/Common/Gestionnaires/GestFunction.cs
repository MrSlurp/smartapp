using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using SmartApp.Datas;

namespace SmartApp.Gestionnaires
{
    public class GestFunction : BaseGest
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
                string strSymbolTemp = string.Format(DEFAULT_FUNCTION_SYMB, i);
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
        public override bool ReadIn(XmlNode Node)
        {
            XmlNode NodeFunctionSection = null;
            for (int i = 0; i < Node.ChildNodes.Count; i++)
            {
                if (Node.ChildNodes[i].Name == XML_CF_TAG.FunctionSection.ToString())
                    NodeFunctionSection = Node.ChildNodes[i];
            }
            if (NodeFunctionSection == null)
                return false;

            for (int i = 0; i < NodeFunctionSection.ChildNodes.Count; i++)
            {
                XmlNode ChildNode = NodeFunctionSection.ChildNodes[i];
                if (ChildNode.Name != XML_CF_TAG.Function.ToString())
                    continue;

                Function NewFunc = new Function();
                if (NewFunc != null)
                {
                    if (!NewFunc.ReadIn(ChildNode))
                        return false;

                    this.AddObj(NewFunc);
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
            XmlNode NodeFuncSection = XmlDoc.CreateElement(XML_CF_TAG.FunctionSection.ToString());
            Node.AppendChild(NodeFuncSection);
            for (int i = 0; i < this.m_ListObject.Count; i++)
            {
                Function dt = (Function)m_ListObject[i];

                XmlNode XmlFunc = XmlDoc.CreateElement(XML_CF_TAG.Function.ToString());
                dt.WriteOut(XmlDoc, XmlFunc);
                NodeFuncSection.AppendChild(XmlFunc);
            }
            return true;
        }
        #endregion
    }
}

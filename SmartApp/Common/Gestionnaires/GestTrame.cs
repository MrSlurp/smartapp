/***************************************************************************/
// PROJET : BTCommand : system de commande paramétrable pour équipement
// ayant une mécanisme de commande par liaison série/ethernet/http
/***************************************************************************/
// Fichier : 
/***************************************************************************/
// description :
// 
/***************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using SmartApp.Datas;

namespace SmartApp.Gestionnaires
{
    public class GestTrame : BaseGest
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
                string strSymbolTemp = string.Format(DEFAULT_FRAME_SYMB, i);
                if (GetFromSymbol(strSymbolTemp) == null)
                {
                    return strSymbolTemp;
                }
            }
            return "";
        }
        #endregion

        #region ReadIn / WriteOut
        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public override bool ReadIn(XmlNode Node)
        {
            for (int i = 0; i < Node.ChildNodes.Count; i++)
            {
                XmlNode ChildNode = Node.ChildNodes[i];
                if (ChildNode.Name != XML_CF_TAG.Trame.ToString())
                    continue;

                Trame NewTrame = new Trame();
                if (NewTrame != null)
                {
                    if (!NewTrame.ReadIn(ChildNode))
                        return false;

                    this.AddObj(NewTrame);
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
            for (int i = 0; i < this.m_ListObject.Count; i++)
            {
                Trame Tr = (Trame)m_ListObject[i];

                XmlNode XmlTrame = XmlDoc.CreateElement(XML_CF_TAG.Trame.ToString());
                Tr.WriteOut(XmlDoc, XmlTrame);
                Node.AppendChild(XmlTrame);
            }
            base.WriteOut(XmlDoc, Node);
            return true;
        }
        #endregion
    }
}

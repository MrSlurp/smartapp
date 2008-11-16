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
using System.Windows.Forms;

namespace CommonLib
{
    public class GestScreen : BaseGest
    {
        #region constructeur
        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public GestScreen()
        {

        }
        #endregion

        //*****************************************************************************************************
        // Description: renvoie le prochain symbol par défaut pour une donnée
        // Return: /
        //*****************************************************************************************************
        public override string GetNextDefaultSymbol()
        {
            for (int i = 0; i < MAX_DEFAULT_ITEM_SYMBOL; i++)
            {
                string strSymbolTemp = string.Format(DEFAULT_SCREEN_SYMB, i);
                if (GetFromSymbol(strSymbolTemp) == null)
                {
                    return strSymbolTemp;
                }
            }
            return "";
        }

        #region ReadIn / WriteOut
        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public override bool ReadIn(XmlNode Node, TYPE_APP TypeApp)
        {
            System.Diagnostics.Debug.Assert(false);
            return false;
        }

        public bool ReadIn(XmlNode Node, TYPE_APP TypeApp, DllControlGest GestDLL)
        {
            for (int i = 0; i < Node.ChildNodes.Count; i++)
            {
                XmlNode ChildNode = Node.ChildNodes[i];
                if (ChildNode.Name != XML_CF_TAG.Screen.ToString())
                    continue;
                
                BTScreen NewScreen = new BTScreen();
                if (NewScreen != null)
                {
                    if (!NewScreen.ReadIn(ChildNode, TypeApp, GestDLL))
                        return false;

                    this.AddObj(NewScreen);
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
                BTScreen dt = (BTScreen)m_ListObject[i];
                XmlNode XmlScreen = XmlDoc.CreateElement(XML_CF_TAG.Screen.ToString());
                dt.WriteOut(XmlDoc, XmlScreen);
                Node.AppendChild(XmlScreen);
            }
            base.WriteOut(XmlDoc, Node);
            return true;
        }
        #endregion
    }
}

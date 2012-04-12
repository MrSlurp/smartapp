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

namespace CommonLib
{
    public class GestControl : BaseGest
    {
        #region fonction "utilitaires"
        public override BaseObject AddNewObject(BTDoc document)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// renvoie le prochain symbol par défaut pour une donnée
        /// </summary>
        /// <returns>symbol du nouvel objet</returns>
        public override string GetNextDefaultSymbol()
        {
            for (int i = 0; i < MAX_DEFAULT_ITEM_SYMBOL; i++)
            {
                string strSymbolTemp = string.Format(DEFAULT_CTRL_SYMB, i);
                if (GetFromSymbol(strSymbolTemp) == null)
                {
                    return strSymbolTemp;
                }
            }
            return "";
        }

        /// <summary>
        /// change la position du control donné afin qu'il passe au premier plan
        /// </summary>
        /// <param name="ctrl"></param>
        public void BringControlToTop(BTControl ctrl)
        {
            if (m_ListObject.Contains((BaseObject)ctrl))
            {
                m_ListObject.Remove(ctrl);
                m_ListObject.Insert(m_ListObject.Count, ctrl);
            }
        }

        public bool ReadInForClipBoard(XmlNode Node, DllControlGest GestDLL, BTDoc document)
        {
            for (int j = 0; j < Node.ChildNodes.Count; j++)
            {
                XmlNode NodeControl = Node.ChildNodes[j];
                if (NodeControl.Name == XML_CF_TAG.Control.ToString())
                {
                    BTControl Control = new BTControl(document);
                    if (!Control.ReadIn(NodeControl, document))
                        return false;

                    AddObj(Control);
                }
                else if (NodeControl.Name == XML_CF_TAG.SpecificControl.ToString())
                {
                    BTControl Control = SpecificControlParser.ParseAndCreateSpecificControl(NodeControl, document);
                    if (Control != null)
                    {
                        if (!Control.ReadIn(NodeControl, document))
                            return false;
                        AddObj(Control);
                    }
                }
                else if (NodeControl.Name == XML_CF_TAG.DllControl.ToString())
                {
                    uint DllID = SpecificControlParser.ParseDllID(NodeControl);
                    if (GestDLL[DllID] != null)
                    {
                        BTControl Control = GestDLL[DllID].CreateBTControl(document);
                        if (Control != null)
                        {
                            if (!Control.ReadIn(NodeControl, document))
                                return false;
                            AddObj(Control);
                        }
                        else
                        {
                            System.Diagnostics.Debug.Assert(false);
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    //System.Diagnostics.Debug.Assert(false);
                }
            }
            return true;
        }

        public void WriteOutForClipBoard(XmlDocument XmlDoc, XmlNode Node, BTDoc document)
        {
            for (int i = 0; i < m_ListObject.Count; i++)
            {
                BTControl dt = (BTControl)m_ListObject[i];
                dt.WriteOut(XmlDoc, Node, document);
            }
        }
        #endregion
    }
}

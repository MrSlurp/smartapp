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
        //*****************************************************************************************************
        // Description: renvoie le prochain symbol par défaut pour une donnée
        // Return: /
        //*****************************************************************************************************
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
        #endregion

        public void BringControlToTop(BTControl ctrl)
        {
            if (m_ListObject.Contains((BaseObject)ctrl))
            {
                m_ListObject.Remove(ctrl);
                m_ListObject.Insert(m_ListObject.Count, ctrl);
            }
        }
    }
}

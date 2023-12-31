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
        #region fonctions utilitaires
        public override BaseObject AddNewObject(BTDoc document)
        {
            BTScreen dat = new BTScreen(document);
            dat.Symbol = GetNextDefaultSymbol();
            this.AddObj(dat);
            return dat;
        }
        /// <summary>
        /// renvoie le prochain symbol libre pour une nouvelle donnée
        /// </summary>
        /// <returns>prochain symbol libre</returns>
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
        #endregion

        #region ReadIn / WriteOut
        /// <summary>
        /// NE PAS UTILISER
        /// </summary>
        /// <param name="Node"></param>
        /// <param name="TypeApp"></param>
        /// <returns></returns>
        public override bool ReadIn(XmlNode Node, BTDoc document)
        {
            System.Diagnostics.Debug.Assert(false);
            return false;
        }

        /// <summary>
        /// Lit les données de l'objet a partir de son noeud XML
        /// </summary>
        /// <param name="Node">Noeud Xml de l'objet</param>
        /// <param name="TypeApp">type d'application courante</param>
        /// <param name="GestDLL">Getsionnaire des DLL plugin</param>
        /// <returns>true si la lecture s'est bien passé</returns>
        public bool ReadIn(XmlNode Node, BTDoc document, DllControlGest GestDLL)
        {
            for (int i = 0; i < Node.ChildNodes.Count; i++)
            {
                XmlNode ChildNode = Node.ChildNodes[i];
                if (ChildNode.Name != XML_CF_TAG.Screen.ToString())
                    continue;
                
                BTScreen NewScreen = new BTScreen(document);
                if (NewScreen != null)
                {
                    if (!NewScreen.ReadIn(ChildNode, document, GestDLL))
                        return false;

                    this.AddObj(NewScreen);
                }
            }
            return true;
        }

        /// <summary>
        /// Lit les données de l'objet a partir de son noeud XML
        /// </summary>
        /// <param name="Node">Noeud Xml de l'objet</param>
        /// <param name="TypeApp">type d'application courante</param>
        /// <returns>true si la lecture s'est bien passé</returns>
        public override bool WriteOut(XmlDocument XmlDoc, XmlNode Node, BTDoc document)
        {
            for (int i = 0; i < this.m_ListObject.Count; i++)
            {
                BTScreen dt = (BTScreen)m_ListObject[i];
                XmlNode XmlScreen = XmlDoc.CreateElement(XML_CF_TAG.Screen.ToString());
                dt.WriteOut(XmlDoc, XmlScreen, document);
                Node.AppendChild(XmlScreen);
            }
            base.WriteOut(XmlDoc, Node, document);
            return true;
        }
        #endregion
    }
}

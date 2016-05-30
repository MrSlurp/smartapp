/*
    This file is part of SmartApp.

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
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace CommonLib
{
    public class GestFunction : BaseGest
    {
        #region fonction "utilitaires"
        public override BaseObject AddNewObject(BTDoc document)
        {
            Function dat = new Function();
            dat.Symbol = GetNextDefaultSymbol();
            this.AddObj(dat);
            return dat;
        }

        /// <summary>
        /// renvoie le prochain symbol libre pour une nouvelle donnée
        /// </summary>
        /// <returns>prochaine symbol libre</returns>
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
        /// <summary>
        /// Lit les données de l'objet a partir de son noeud XML
        /// </summary>
        /// <param name="Node">Noeud Xml de l'objet</param>
        /// <param name="TypeApp">type d'application courante</param>
        /// <returns>true si la lecture s'est bien passé</returns>
        public override bool ReadIn(XmlNode Node, BTDoc document)
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
                    if (!NewFunc.ReadIn(ChildNode, document))
                        return false;

                    this.AddObj(NewFunc);
                }
            }
            return true;
        }

        /// <summary>
        /// écrit les données de l'objet dans le fichier XML
        /// </summary>
        /// <param name="XmlDoc">Document XML courant</param>
        /// <param name="Node">Noeud parent du controle dans le document</param>
        /// <returns>true si l'écriture s'est déroulée avec succès</returns>
        public override bool WriteOut(XmlDocument XmlDoc, XmlNode Node, BTDoc document)
        {
            XmlNode NodeFuncSection = XmlDoc.CreateElement(XML_CF_TAG.FunctionSection.ToString());
            Node.AppendChild(NodeFuncSection);
            for (int i = 0; i < this.m_ListObject.Count; i++)
            {
                Function dt = (Function)m_ListObject[i];

                XmlNode XmlFunc = XmlDoc.CreateElement(XML_CF_TAG.Function.ToString());
                dt.WriteOut(XmlDoc, XmlFunc, document);
                NodeFuncSection.AppendChild(XmlFunc);
            }
            return true;
        }
        #endregion
    }
}

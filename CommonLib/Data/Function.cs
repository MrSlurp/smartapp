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
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Xml;

namespace CommonLib
{
    public class Function : BaseObject, IScriptable
    {
        #region Déclaration des données de la classe
        // script de la fonction
        protected ItemScriptsConainter m_ScriptContainer = new ItemScriptsConainter();

        protected QuickExecuter m_Executer = null;

        public Function()
        {
            m_ScriptContainer["FuncScript"] = new string[1];
        }

        #endregion

        #region propriétées de la classe
        /// <summary>
        /// obtient ou assigne le script du controle
        /// </summary>
        public ItemScriptsConainter ItemScripts
        {
            get
            {
                return m_ScriptContainer;
            }
        }
        #endregion

        #region ReadIn / WriteOut
        /// <summary>
        /// Lit les données de l'objet a partir de son noeud XML
        /// </summary>
        /// <param name="Node">Noeud Xml de l'objet</param>
        /// <param name="TypeApp">type d'application courante</param>
        /// <returns>true si la lecture s'est bien passé</returns>
        public override bool ReadIn(XmlNode Node, BTDoc document)
        {
            bool bRet = base.ReadIn(Node, document);
            List<string> listScriptLines = new List<string>();
            for (int i = 0; i < Node.ChildNodes.Count; i++)
            {
                if (Node.ChildNodes[i].Name == XML_CF_TAG.Line.ToString()
                        && Node.ChildNodes[i].FirstChild != null)
                {
                    listScriptLines.Add(Node.ChildNodes[i].FirstChild.Value);
                }
            }
            m_ScriptContainer["FuncScript"] = listScriptLines.ToArray();
            return bRet;
        }

        /// <summary>
        /// écrit les données de l'objet dans le fichier XML
        /// </summary>
        /// <param name="XmlDoc">Document XML courant</param>
        /// <param name="Node">Noeud parent du controle dans le document</param>
        /// <returns>true si l'écriture s'est déroulée avec succès</returns>
        public override bool WriteOut(XmlDocument XmlDoc, XmlNode Node, BTDoc document)
        {
            base.WriteOut(XmlDoc, Node, document);
            for (int i = 0; i < m_ScriptContainer["FuncScript"].Length; i++)
            {
                if (!string.IsNullOrEmpty(m_ScriptContainer["FuncScript"][i]))
                {
                    XmlNode NodeLine = XmlDoc.CreateElement(XML_CF_TAG.Line.ToString());
                    XmlNode NodeText = XmlDoc.CreateTextNode(m_ScriptContainer["FuncScript"][i]);
                    NodeLine.AppendChild(NodeText);
                    Node.AppendChild(NodeLine);
                }
            }
            return true;
        }

        /// <summary>
        /// termine la lecture de l'objet. utilisé en mode Commande initialiser les donnes spécifiques 
        /// au mode SmartCommand
        /// </summary>
        /// <param name="Doc">Document courant</param>
        /// <returns>true si tout s'est bien passé</returns>
        public override bool FinalizeRead(BTDoc Doc)
        {
            m_Executer = Doc.Executer;
            return true;
        }

        #endregion

        #region Gestion des AppMessages
        /// <summary>
        /// effectue les opération nécessaire lors de la récéption d'un message
        /// </summary>
        /// <param name="Mess">Type de message</param>
        /// <param name="obj">objet contenant les paramètres du messages</param>
        /// <param name="TypeApp">Type d'application courante</param>
        public override void TraiteMessage(MESSAGE Mess, object obj, TYPE_APP TypeApp)
        {
            ScriptTraiteMessage(this, Mess, m_ScriptContainer, obj);
            if (Mess == MESSAGE.MESS_PRE_PARSE)
            {
                if (this.m_ScriptContainer["FuncScript"].Length != 0)
                    this.m_iQuickScriptID = m_Executer.PreParseScript(m_ScriptContainer["FuncScript"]);    
            }
        }
        #endregion
    }
}

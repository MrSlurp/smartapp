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
        private StringCollection m_ScriptLines = new StringCollection();

#if QUICK_MOTOR
        protected QuickExecuter m_Executer = null;
#endif

        #endregion

        #region propriétées de la classe
        /// <summary>
        /// obtient ou assigne le script du controle
        /// </summary>
        public string[] ScriptLines
        {
            get 
            {
                string[] TabLines = new string[m_ScriptLines.Count];
                for (int i = 0; i < m_ScriptLines.Count; i++)
                {
                    TabLines[i] = m_ScriptLines[i];
                }
                return TabLines;
            }
            set
            {
                m_ScriptLines.Clear();
                for (int i = 0; i < value.Length; i++)
                {
                    m_ScriptLines.Add(value[i]);
                }
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
        public override bool ReadIn(XmlNode Node, TYPE_APP TypeApp)
        {
            bool bRet = base.ReadIn(Node, TypeApp);
            for (int i = 0; i < Node.ChildNodes.Count; i++)
            {
                if (Node.ChildNodes[i].Name == XML_CF_TAG.Line.ToString()
                        && Node.ChildNodes[i].FirstChild != null)
                {
                    m_ScriptLines.Add(Node.ChildNodes[i].FirstChild.Value);
                }
            }
            return bRet;
        }

        /// <summary>
        /// écrit les données de l'objet dans le fichier XML
        /// </summary>
        /// <param name="XmlDoc">Document XML courant</param>
        /// <param name="Node">Noeud parent du controle dans le document</param>
        /// <returns>true si l'écriture s'est déroulée avec succès</returns>
        public override bool WriteOut(XmlDocument XmlDoc, XmlNode Node)
        {
            base.WriteOut(XmlDoc, Node);
            for (int i = 0; i < m_ScriptLines.Count; i++)
            {
                XmlNode NodeLine = XmlDoc.CreateElement(XML_CF_TAG.Line.ToString());
                XmlNode NodeText = XmlDoc.CreateTextNode(m_ScriptLines[i]);
                NodeLine.AppendChild(NodeText);
                Node.AppendChild(NodeLine);
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
#if QUICK_MOTOR
            m_Executer = Doc.Executer;
#endif 
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
            ScriptTraiteMessage(this, Mess, m_ScriptLines, obj);
#if QUICK_MOTOR
            if (Mess == MESSAGE.MESS_PRE_PARSE)
            {
                if (this.ScriptLines.Length != 0)
                    m_Executer.PreParseScript((IScriptable) this);    
            }
#endif
        }
        #endregion
    }
}

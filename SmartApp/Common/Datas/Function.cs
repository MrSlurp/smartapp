using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Xml;

namespace SmartApp.Datas
{
    public class Function : BaseObject, IScriptable
    {
        #region Déclaration des données de la classe
        // script de la fonction
        private StringCollection m_ScriptLines = new StringCollection();
        #endregion

        #region propriétées de la classe
        //*****************************************************************************************************
        // Description: accesseur sur le script
        // Return: /
        //*****************************************************************************************************
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
        //*****************************************************************************************************
        // Description: ecrit les données de l'objet a partir de son noeud XML
        // Return: /
        //*****************************************************************************************************
        public override bool ReadIn(XmlNode Node)
        {
            bool bRet = base.ReadIn(Node);
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

        //*****************************************************************************************************
        // Description: ecrit les données de l'objet a partir de son noeud XML
        // Return: /
        //*****************************************************************************************************
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

        //*****************************************************************************************************
        // Description: termine la lecture de l'objet. utilisé en mode Commande pour récupérer les référence
        // vers les objets utilisés
        // Return: /
        //*****************************************************************************************************
        public override bool FinalizeRead(BTDoc Doc)
        {
            return true;
        }

        #endregion

        #region Gestion des AppMessages
        //*****************************************************************************************************
        // Description: effectue les opération nécessaire lors de la récéption d'un message
        // Return: /
        //*****************************************************************************************************
        public override void TraiteMessage(MESSAGE Mess, object obj)
        {
            ScriptTraiteMessage(Mess, m_ScriptLines, obj);
        }

        //*****************************************************************************************************
        // Description: effectue le traitement specifique aux script
        // Return: /
        //*****************************************************************************************************
        protected void ScriptTraiteMessage(MESSAGE Mess, StringCollection Script, object obj)
        {
            switch (Mess)
            {
                case MESSAGE.MESS_ASK_ITEM_DELETE:
                    if (((MessAskDelete)obj).TypeOfItem == typeof(Trame)
                        || ((MessAskDelete)obj).TypeOfItem == typeof(Function)
                        || ((MessAskDelete)obj).TypeOfItem == typeof(Logger)
                        )
                    {
                        MessAskDelete MessParam = (MessAskDelete)obj;
                        for (int i = 0; i < Script.Count; i++)
                        {
                            string stritem = SmartApp.Scripts.ScriptParser.GetLineToken(Script[i], SmartApp.Scripts.ScriptParser.INDEX_TOKEN_SYMBOL);
                            if (stritem == MessParam.WantDeletetItemSymbol)
                            {
                                string strMess = string.Format("Function {0} Script: Line {1} will be removed", Symbol, i + 1);
                                MessParam.ListStrReturns.Add(strMess);
                            }
                        }
                    }
                    break;
                case MESSAGE.MESS_ITEM_DELETED:
                    if (((MessAskDelete)obj).TypeOfItem == typeof(Trame)
                        || ((MessAskDelete)obj).TypeOfItem == typeof(Function)
                        || ((MessAskDelete)obj).TypeOfItem == typeof(Logger)
                        )
                    {
                        MessDeleted MessParam = (MessDeleted)obj;
                        for (int i = 0; i < Script.Count; i++)
                        {
                            string stritem = SmartApp.Scripts.ScriptParser.GetLineToken(Script[i], SmartApp.Scripts.ScriptParser.INDEX_TOKEN_SYMBOL);
                            if (stritem == MessParam.DeletetedItemSymbol)
                            {
                                Script.RemoveAt(i);
                            }
                        }
                    }
                    break;
                case MESSAGE.MESS_ITEM_RENAMED:
                    if (((MessAskDelete)obj).TypeOfItem == typeof(Trame)
                        || ((MessAskDelete)obj).TypeOfItem == typeof(Function)
                        || ((MessAskDelete)obj).TypeOfItem == typeof(Logger)
                        )
                    {
                        MessItemRenamed MessParam = (MessItemRenamed)obj;
                        for (int i = 0; i < Script.Count; i++)
                        {
                            string stritem = SmartApp.Scripts.ScriptParser.GetLineToken(Script[i], SmartApp.Scripts.ScriptParser.INDEX_TOKEN_SYMBOL);
                            if (stritem == MessParam.OldItemSymbol)
                            {
                                Script[i].Replace(MessParam.OldItemSymbol, MessParam.NewItemSymbol);
                            }
                        }
                    }
                    break;
                default:
                    break;
            }
        }
        #endregion
    }
}

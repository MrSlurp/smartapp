/***************************************************************************/
// PROJET : 
// 
/***************************************************************************/
// Fichier : 
/***************************************************************************/
// description :
// 
/***************************************************************************/
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Xml;
using System.Diagnostics;

namespace CommonLib
{
    #region interface pour les objets utilisants du script
    // objet qui possède du script
    public interface IScriptable
    {
        string[] ScriptLines { get; set; }
    }
    // objet qui possède du script a l'init
    public interface IInitScriptable
    {
        string[] InitScriptLines { get; set; }
    }

    #endregion

    public abstract class BaseObject: Object
    {
        #region Déclaration des données de la classe
        // symbol de l'objet
        protected string m_strSymbol;
        // description associée a l'objet
        protected string m_strName;
        #endregion

        #region events
        // event pour l'envoie de message
        public event SendMessage DoSendMessage;
        public event AddLogEventDelegate EventAddLogEvent;
        #endregion

        #region constructeur
        //*****************************************************************************************************
        // Description: constructeur
        // Return: /
        //*****************************************************************************************************
        public BaseObject()
        {
            m_strSymbol = "";
            m_strName = "";
        }
        #endregion

        #region propriétées de la classe
        //*****************************************************************************************************
        // Description: accesseur pour la description de l'objet
        // Return: /
        //*****************************************************************************************************
        public String Description
        {
            get
            {
                return m_strName;
            }
            set
            {
                m_strName = value;
            }
        }
        //*****************************************************************************************************
        // Description: accesseur pour le symbol de l'objet
        // Return: /
        //*****************************************************************************************************
        public String Symbol
        {
            get
            {
                return m_strSymbol;
            }
            set
            {
                string strOldSymbol = m_strSymbol;
                m_strSymbol = value;
                if (DoSendMessage != null && m_strSymbol != strOldSymbol)
                {
                    MessItemRenamed MessParam = new MessItemRenamed();
                    MessParam.TypeOfItem = this.GetType();
                    MessParam.OldItemSymbol = strOldSymbol;
                    MessParam.NewItemSymbol = m_strSymbol;
                    ProcessSendMessage(MESSAGE.MESS_ITEM_RENAMED, MessParam, TYPE_APP.SMART_CONFIG);
                }
            }
        }
        #endregion

        #region ReadIn / WriteOut
        //*****************************************************************************************************
        // Description: Lit les données de l'objet a partir de son noeud XML
        // Return: /
        //*****************************************************************************************************
        public virtual bool ReadIn(XmlNode Node, TYPE_APP TypeApp)
        {
            return ReadInBaseObject(Node);
        }

        protected bool ReadInBaseObject(XmlNode Node)
        {
            // lecture de l'attribut "nom" (description)
            XmlNode NameAttrib = Node.Attributes.GetNamedItem(XML_CF_ATTRIB.strNom.ToString());
            if (NameAttrib == null)
                return false;
            m_strName = NameAttrib.Value;
            // lectrure de l'attribut "symbol"
            XmlNode SymbolAttrib = Node.Attributes.GetNamedItem(XML_CF_ATTRIB.strSymbol.ToString());
            if (SymbolAttrib == null)
                return false;

            m_strName = NameAttrib.Value;
            m_strSymbol = SymbolAttrib.Value;
            return true;
        }
        //*****************************************************************************************************
        // Description: ecrit les données de l'objet a partir de son noeud XML
        // Return: /
        //*****************************************************************************************************
        public virtual bool WriteOut(XmlDocument XmlDoc, XmlNode Node)
        {
            return WriteOutBaseObject(XmlDoc, Node);
        }

        protected bool WriteOutBaseObject(XmlDocument XmlDoc, XmlNode Node)
        {
            // écriture de l'attribut "nom" (description)
            XmlAttribute AttrName = XmlDoc.CreateAttribute(XML_CF_ATTRIB.strNom.ToString());
            AttrName.Value = m_strName;
            Node.Attributes.Append(AttrName);
            // écriture de l'attribut "symbol"
            XmlAttribute AttrSymb = XmlDoc.CreateAttribute(XML_CF_ATTRIB.strSymbol.ToString());
            AttrSymb.Value = m_strSymbol;
            Node.Attributes.Append(AttrSymb);
            return true;
        }

        //*****************************************************************************************************
        // Description: termine la lecture de l'objet. utilisé en mode Commande pour récupérer les référence
        // vers les objets utilisés
        // Return: /
        //*****************************************************************************************************
        public virtual bool FinalizeRead(BTDoc Doc)
        {
            return true;
        }
        #endregion

        #region Gestion des AppMessages
        //*****************************************************************************************************
        // Description: effectue les opération nécessaire lors de la récéption d'un message
        // Return: /
        //*****************************************************************************************************
        public abstract void TraiteMessage(MESSAGE Mess, object obj, TYPE_APP TypeApp);

        //*****************************************************************************************************
        // Description: appel l'évènement DoSendMessage. cette methode permet aux classes filles 
        // d'utiliser l'event de la classe de base
        // Return: /
        //*****************************************************************************************************
        protected void ProcessSendMessage(MESSAGE Mess, object Param, TYPE_APP TypeApp)
        {
            if (this.DoSendMessage != null)
            {
                DoSendMessage.BeginInvoke(Mess, Param, TypeApp, null, null);
            }
        }
        #endregion

        #region Méthode de tratement des messages pour les objets scrptables
        protected void ScriptTraiteMessage(BaseObject Sender, MESSAGE Mess, StringCollection Script, object obj)
        {
            switch (Mess)
            {
                case MESSAGE.MESS_ASK_ITEM_DELETE:
                    if (((MessAskDelete)obj).TypeOfItem == typeof(Trame)
                        || ((MessAskDelete)obj).TypeOfItem == typeof(Function)
                        || ((MessAskDelete)obj).TypeOfItem == typeof(Logger)
                        || ((MessAskDelete)obj).TypeOfItem == typeof(BTTimer)
                        || ((MessAskDelete)obj).TypeOfItem == typeof(Data)
                        )
                    {
                        MessAskDelete MessParam = (MessAskDelete)obj;
                        for (int i = 0; i < Script.Count; i++)
                        {
                            string stritem = "";
                            stritem = ScriptParser.GetLineToken(Script[i], ScriptParser.INDEX_TOKEN_SYMBOL);

                            if (stritem == MessParam.WantDeletetItemSymbol 
                                || (((MessAskDelete)obj).TypeOfItem == typeof(Data) && Script[i].Contains(MessParam.WantDeletetItemSymbol))
                                )
                            {
                                Type tp = Sender.GetType();
                                string strMess = "";
                                if (tp == typeof(BTTimer))
                                {
                                    strMess = string.Format("Timer {0} Script: Line {1} will be removed", Symbol, i + 1);
                                }
                                else if (tp == typeof(BTScreen))
                                {
                                    strMess = string.Format("Screen {0} Script: Line {1} will be removed", Symbol, i + 1);
                                }
                                else if (tp == typeof(Function))
                                {
                                    strMess = string.Format("Function {0} Script: Line {1} will be removed", Symbol, i + 1);
                                }
                                else if (tp == typeof(BTControl))
                                {
                                    strMess = string.Format("Control {0} Script: Line {1} will be removed", Symbol, i + 1);
                                }
                                else
                                    System.Diagnostics.Debug.Assert(false);
                                MessParam.ListStrReturns.Add(strMess);
                            }
                        }
                    }
                    break;
                case MESSAGE.MESS_ITEM_DELETED:
                    if (((MessDeleted)obj).TypeOfItem == typeof(Trame)
                        || ((MessDeleted)obj).TypeOfItem == typeof(Function)
                        || ((MessDeleted)obj).TypeOfItem == typeof(Logger)
                        || ((MessDeleted)obj).TypeOfItem == typeof(BTTimer)
                        )
                    {
                        MessDeleted MessParam = (MessDeleted)obj;
                        for (int i = Script.Count-1; i >=0 ; i--)
                        {
                            string stritem = ScriptParser.GetLineToken(Script[i], ScriptParser.INDEX_TOKEN_SYMBOL);
                            if (stritem == MessParam.DeletetedItemSymbol)
                            {
                                Script.RemoveAt(i);
                            }
                        }
                    }
                    else if (((MessDeleted)obj).TypeOfItem == typeof(Data))
                    {
                        MessDeleted MessParam = (MessDeleted)obj;
                        for (int i = Script.Count - 1; i >= 0; i--)
                        {
                            if (Script[i].Contains(MessParam.DeletetedItemSymbol))
                            {
                                Script.RemoveAt(i);
                            }
                        }
                    }
                    break;
                case MESSAGE.MESS_ITEM_RENAMED:
                    if (((MessItemRenamed)obj).TypeOfItem == typeof(Trame)
                        || ((MessItemRenamed)obj).TypeOfItem == typeof(Function)
                        || ((MessItemRenamed)obj).TypeOfItem == typeof(Logger)
                        || ((MessItemRenamed)obj).TypeOfItem == typeof(BTTimer)
                        )
                    {
                        MessItemRenamed MessParam = (MessItemRenamed)obj;
                        for (int i = 0; i < Script.Count; i++)
                        {
                            string stritem = ScriptParser.GetLineToken(Script[i], ScriptParser.INDEX_TOKEN_SYMBOL);
                            if (stritem == MessParam.OldItemSymbol)
                            {
                                Script[i] = Script[i].Replace(MessParam.OldItemSymbol, MessParam.NewItemSymbol);
                            }
                        }
                    }
                    else if (((MessDeleted)obj).TypeOfItem == typeof(Data))
                    {
                        MessItemRenamed MessParam = (MessItemRenamed)obj;
                        for (int i = 0; i < Script.Count; i++)
                        {
                            if (Script[i].Contains(MessParam.OldItemSymbol))
                            {
                                Script[i] = Script[i].Replace(MessParam.OldItemSymbol, MessParam.NewItemSymbol);
                            }
                        }
                    }
                    break;
                default:
                    break;
            }
        }
        #endregion

        #region méthodes divers
        private void AddLogEvent(LogEvent Event)
        {
            if (EventAddLogEvent != null)
            {
                EventAddLogEvent(Event);
            }
        }
        #endregion
    }
}

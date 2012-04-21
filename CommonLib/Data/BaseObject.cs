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
using System.Collections;
using System.Text;
using System.Xml;
using System.Windows.Forms;
using System.Diagnostics;

namespace CommonLib
{
    #region interface pour les objets utilisants du script
    // objet qui possède du script
    public class ItemScriptsConainter
    {
        Hashtable m_mapScripts = new Hashtable();
        public string[] this[string str]
        {
            get
            {
                return m_mapScripts[str] as string[];
            }
            set
            {
                if (m_mapScripts.Contains(str))
                    m_mapScripts[str] = value;
                else
                    m_mapScripts.Add(str, value);
            }
        }

        public int Count
        {
            get { return m_mapScripts.Count; }
        }

        public ICollection ScriptKeys
        {
            get { return m_mapScripts.Keys; }
        }
    }

    public interface IScriptable
    {
        ItemScriptsConainter ItemScripts { get; }
    }

    #endregion

    public abstract class BaseObject : Object
    {
        #region Déclaration des données de la classe
        // symbol de l'objet
        protected string m_strSymbol;
        // description associée a l'objet
        protected string m_strName;

        /// <summary>
        /// identifiant quickscript utilisé par les objets scriptables
        /// </summary>
        protected int m_iQuickScriptID;
        #endregion

        #region events
        // event pour l'envoie de message
        public event SendMessage DoSendMessage;
        public event AddLogEventDelegate EventAddLogEvent;
        public event BaseObjectPropertiesChangedEvent PropertiesChanged;
        #endregion

        #region constructeur
        /// <summary>
        /// constructeur par défaut
        /// </summary>
        public BaseObject()
        {
            m_strSymbol = "";
            m_strName = "";
        }
        #endregion

        #region propriétées de la classe
        /// <summary>
        /// assigne ou obtient la description de l'objet
        /// </summary>
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

        /// <summary>
        /// assigne ou obtient le symbol de l'objet
        /// </summary>
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

        /// <summary>
        /// obtient ou assigne si la valeur est visible à l'utilisateur
        /// (les données de controle des protocoles ne sont pas visibles)
        /// </summary>
        public virtual bool IsUserVisible
        {
            get { return true; }
            set {  }
        }


        public int QuickScriptID
        {
            get
            {
                return m_iQuickScriptID;
            }
        }

        public virtual Control StdConfigPanel
        {
            get { return null; }
        }

        #endregion

        #region ReadIn / WriteOut
        /// <summary>
        /// Lit les données de l'objet a partir de son noeud XML
        /// </summary>
        /// <param name="Node">Noeud Xml de l'objet</param>
        /// <param name="TypeApp">type d'application courante</param>
        /// <returns>true si la lecture s'est bien passé</returns>
        public virtual bool ReadIn(XmlNode Node, BTDoc document)
        {
            return ReadInBaseObject(Node);
        }

        /// <summary>
        /// lit les paramètres de l'objet de base
        /// </summary>
        /// <param name="Node">noeud de l'objet</param>
        /// <returns>true si tout s'est bien passé</returns>
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

        /// <summary>
        /// écrit les données de l'objet dans le fichier XML
        /// </summary>
        /// <param name="XmlDoc">Document XML courant</param>
        /// <param name="Node">Noeud parent du controle dans le document</param>
        /// <returns>true si l'écriture s'est déroulée avec succès</returns>
        public virtual bool WriteOut(XmlDocument XmlDoc, XmlNode Node, BTDoc document)
        {
            return WriteOutBaseObject(XmlDoc, Node);
        }

        /// <summary>
        /// écrit les paramètres de l'objet de base
        /// </summary>
        /// <param name="XmlDoc">document XML</param>
        /// <param name="Node">noeud de l'objet courant</param>
        /// <returns>true si tout s'est bien passé</returns>
        protected bool WriteOutBaseObject(XmlDocument XmlDoc, XmlNode Node)
        {
            // écriture de l'attribut "nom" (description)
            XmlAttribute AttrName = XmlDoc.CreateAttribute(XML_CF_ATTRIB.strNom.ToString());
            AttrName.Value = m_strName;
            // écriture de l'attribut "symbol"
            XmlAttribute AttrSymb = XmlDoc.CreateAttribute(XML_CF_ATTRIB.strSymbol.ToString());
            AttrSymb.Value = m_strSymbol;
            Node.Attributes.Append(AttrSymb);
            Node.Attributes.Append(AttrName);
            return true;
        }

        /// <summary>
        /// termine la lecture de l'objet. utilisé en mode Commande pour récupérer les référence
        /// vers les objets utilisés
        /// </summary>
        /// <param name="Doc">Document courant</param>
        /// <returns>true si tout s'est bien passé</returns>
        public virtual bool FinalizeRead(BTDoc Doc)
        {
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
        public abstract void TraiteMessage(MESSAGE Mess, object obj, TYPE_APP TypeApp);

        /// <summary>
        /// effectue l'envoie d'un message vers les objet s'étant enregistré comme récépeteur (i.e. les gestionnaires)
        /// </summary>
        /// <param name="Mess">Type de message</param>
        /// <param name="Param">objet contenant les paramètres du messages</param>
        /// <param name="TypeApp">Type d'application courante</param>
        protected void ProcessSendMessage(MESSAGE Mess, object Param, TYPE_APP TypeApp)
        {
            if (this.DoSendMessage != null)
            {
                DoSendMessage(Mess, Param, TypeApp);
                //DoSendMessage.BeginInvoke(Mess, Param, TypeApp, null, null);
                //IAsyncResult 
                //DoSendMessage.EndInvoke(null);
                //DoSendMessage.BeginInvoke(Mess, Param, TypeApp, null, null);
            }
        }
        #endregion

        #region Méthode de tratement des messages pour les objets scrptables
        /// <summary>
        /// traite les message dans les scripts de l'objet
        /// </summary>
        /// <param name="Sender">objet possédant le script</param>
        /// <param name="Mess">Type de message</param>
        /// <param name="Script">script à traiter</param>
        /// <param name="obj">objet contenant les paramètres du messages</param>
        protected void ScriptTraiteMessage(BaseObject Sender, MESSAGE Mess, ItemScriptsConainter Scripts, object obj)
        {
            foreach (string ScriptType in Scripts.ScriptKeys)
            {

                switch (Mess)
                {
                    case MESSAGE.MESS_ASK_ITEM_DELETE:
                        if (
                               ((MessAskDelete)obj).TypeOfItem == typeof(Trame)
                            || ((MessAskDelete)obj).TypeOfItem == typeof(Function)
                            || ((MessAskDelete)obj).TypeOfItem == typeof(Logger)
                            || ((MessAskDelete)obj).TypeOfItem == typeof(BTTimer)
                            || ((MessAskDelete)obj).TypeOfItem == typeof(Data)
                            || ((MessAskDelete)obj).TypeOfItem == typeof(BTScreen)
                            )
                        {
                            MessAskDelete MessParam = (MessAskDelete)obj;
                            for (int i = 0; i < Scripts[ScriptType].Length; i++)
                            {
                                string stritem = "";
                                stritem = ScriptParser.GetLineToken(Scripts[ScriptType][i], ScriptParser.INDEX_TOKEN_SYMBOL);

                                if (stritem == MessParam.WantDeletetItemSymbol
                                    || (((MessAskDelete)obj).TypeOfItem == typeof(Data) && Scripts[ScriptType][i].Contains(MessParam.WantDeletetItemSymbol))
                                    )
                                {
                                    Type tp = Sender.GetType();
                                    string strMess = "";
                                    if (tp == typeof(BTTimer))
                                    {
                                        strMess = string.Format(Lang.LangSys.C("Timer {0} Script: Line {1} will be removed"), Symbol, i + 1);
                                    }
                                    else if (tp == typeof(BTScreen))
                                    {
                                        strMess = string.Format(Lang.LangSys.C("Screen {0} Script: Line {1} will be removed"), Symbol, i + 1);
                                    }
                                    else if (tp == typeof(Function))
                                    {
                                        strMess = string.Format(Lang.LangSys.C("Function {0} Script: Line {1} will be removed"), Symbol, i + 1);
                                    }
                                    else if (tp == typeof(BTControl))
                                    {
                                        strMess = string.Format(Lang.LangSys.C("Control {0} Script: Line {1} will be removed"), Symbol, i + 1);
                                    }
                                    else
                                        System.Diagnostics.Debug.Assert(false);

                                    MessParam.ListStrReturns.Add(strMess);
                                }
                            }
                        }
                        break;
                    case MESSAGE.MESS_ITEM_DELETED:
                        if (
                               ((MessDeleted)obj).TypeOfItem == typeof(Trame)
                            || ((MessDeleted)obj).TypeOfItem == typeof(Function)
                            || ((MessDeleted)obj).TypeOfItem == typeof(Logger)
                            || ((MessDeleted)obj).TypeOfItem == typeof(BTTimer)
                            //|| ((MessDeleted)obj).TypeOfItem == typeof(Data) voir le else if
                            || ((MessDeleted)obj).TypeOfItem == typeof(BTScreen)
                            )
                        {
                            MessDeleted MessParam = (MessDeleted)obj;
                            for (int i = Scripts[ScriptType].Length - 1; i >= 0; i--)
                            {
                                string stritem = ScriptParser.GetLineToken(Scripts[ScriptType][i], ScriptParser.INDEX_TOKEN_SYMBOL);
                                if (stritem == MessParam.DeletetedItemSymbol)
                                {
                                    Scripts[ScriptType][i] = string.Empty;
                                }
                            }
                        }
                        else if (((MessDeleted)obj).TypeOfItem == typeof(Data))
                        {
                            MessDeleted MessParam = (MessDeleted)obj;
                            for (int i = Scripts[ScriptType].Length - 1; i >= 0; i--)
                            {
                                if (Scripts[ScriptType][i].Contains(MessParam.DeletetedItemSymbol))
                                {
                                    Scripts[ScriptType][i] = string.Empty;
                                }
                            }
                        }
                        break;
                    case MESSAGE.MESS_ITEM_RENAMED:
                        if (
                               ((MessItemRenamed)obj).TypeOfItem == typeof(Trame)
                            || ((MessItemRenamed)obj).TypeOfItem == typeof(Function)
                            || ((MessItemRenamed)obj).TypeOfItem == typeof(Logger)
                            || ((MessItemRenamed)obj).TypeOfItem == typeof(BTTimer)
                            //|| ((MessItemRenamed)obj).TypeOfItem == typeof(Data) voir le else if 
                            || ((MessItemRenamed)obj).TypeOfItem == typeof(BTScreen)
                            )
                        {
                            MessItemRenamed MessParam = (MessItemRenamed)obj;
                            for (int i = 0; i < Scripts[ScriptType].Length; i++)
                            {
                                string stritem = ScriptParser.GetLineToken(Scripts[ScriptType][i], ScriptParser.INDEX_TOKEN_SYMBOL);
                                if (stritem == MessParam.OldItemSymbol)
                                {
                                    Scripts[ScriptType][i] = Scripts[ScriptType][i].Replace(MessParam.OldItemSymbol, MessParam.NewItemSymbol);
                                }
                            }
                        }
                        else if (((MessItemRenamed)obj).TypeOfItem == typeof(Data))
                        {
                            MessItemRenamed MessParam = (MessItemRenamed)obj;
                            for (int i = 0; i < Scripts[ScriptType].Length; i++)
                            {
                                if (Scripts[ScriptType][i].Contains(MessParam.OldItemSymbol))
                                {
                                    Scripts[ScriptType][i] = Scripts[ScriptType][i].Replace(MessParam.OldItemSymbol, MessParam.NewItemSymbol);
                                }
                            }
                        }
                        break;
                    default:
                        break;
                }
            }
        }
        #endregion

        #region méthodes divers
        /// <summary>
        /// ajoute un évènement dans le logger d'event de SmartCommand
        /// </summary>
        /// <param name="Event">évènement à loguer</param>
        protected void AddLogEvent(LogEvent Event)
        {
            if (EventAddLogEvent != null)
            {
                EventAddLogEvent(Event);
            }
        }

        public virtual string GetToolTipText()
        {
            string returnedText = string.Empty;
            returnedText = Lang.LangSys.C("Symbol : ") +  this.Symbol + "\n" ;
            returnedText += Lang.LangSys.C("Description : ") + this.Description;
            returnedText += "\n";
            return returnedText;
        }

        public virtual void NotifyPropertiesChanged()
        {
            if (PropertiesChanged != null)
                PropertiesChanged(this);
        }
        #endregion
    }
}

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
using SmartApp.Ihm;

namespace SmartApp.Datas
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

    #region delegate pour les évènement de changement de propriété des objets divers
    public delegate void DataPropertiesChange(Data Obj);
    public delegate void ScreenPropertiesChange(BTScreen Obj);
    public delegate void ControlPropertiesChange(BTControl Obj);
    public delegate void TramePropertiesChange(Trame Trame);
    public delegate void FunctionPropertiesChange(Function Function);
    public delegate void TimerPropertiesChange(BTTimer Timer);
    public delegate void LoggerPropertiesChange(Logger Logger);
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
                    ProcessSendMessage(MESSAGE.MESS_ITEM_RENAMED, MessParam);
                }
            }
        }
        #endregion

        #region ReadIn / WriteOut
        //*****************************************************************************************************
        // Description: Lit les données de l'objet a partir de son noeud XML
        // Return: /
        //*****************************************************************************************************
        public virtual bool ReadIn(XmlNode Node)
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
        public abstract void TraiteMessage(MESSAGE Mess, object obj);

        //*****************************************************************************************************
        // Description: appel l'évènement DoSendMessage. cette methode permet aux classes filles 
        // d'utiliser l'event de la classe de base
        // Return: /
        //*****************************************************************************************************
        protected void ProcessSendMessage(MESSAGE Mess, object Param)
        {
            if (this.DoSendMessage != null)
            {
                DoSendMessage.BeginInvoke(Mess, Param, null, null);
            }
        }
        #endregion
    }
}

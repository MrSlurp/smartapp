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
    // objet qui poss�de du script
    public interface IScriptable
    {
        string[] ScriptLines { get; set; }
    }
    // objet qui poss�de du script a l'init
    public interface IInitScriptable
    {
        string[] InitScriptLines { get; set; }
    }

    #endregion

    #region delegate pour les �v�nement de changement de propri�t� des objets divers
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
        #region D�claration des donn�es de la classe
        // symbol de l'objet
        protected string m_strSymbol;
        // description associ�e a l'objet
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

        #region propri�t�es de la classe
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
        // Description: Lit les donn�es de l'objet a partir de son noeud XML
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
        // Description: ecrit les donn�es de l'objet a partir de son noeud XML
        // Return: /
        //*****************************************************************************************************
        public virtual bool WriteOut(XmlDocument XmlDoc, XmlNode Node)
        {
            // �criture de l'attribut "nom" (description)
            XmlAttribute AttrName = XmlDoc.CreateAttribute(XML_CF_ATTRIB.strNom.ToString());
            AttrName.Value = m_strName;
            Node.Attributes.Append(AttrName);
            // �criture de l'attribut "symbol"
            XmlAttribute AttrSymb = XmlDoc.CreateAttribute(XML_CF_ATTRIB.strSymbol.ToString());
            AttrSymb.Value = m_strSymbol;
            Node.Attributes.Append(AttrSymb);
            return true;
        }

        //*****************************************************************************************************
        // Description: termine la lecture de l'objet. utilis� en mode Commande pour r�cup�rer les r�f�rence
        // vers les objets utilis�s
        // Return: /
        //*****************************************************************************************************
        public virtual bool FinalizeRead(BTDoc Doc)
        {
            return true;
        }
        #endregion

        #region Gestion des AppMessages
        //*****************************************************************************************************
        // Description: effectue les op�ration n�cessaire lors de la r�c�ption d'un message
        // Return: /
        //*****************************************************************************************************
        public abstract void TraiteMessage(MESSAGE Mess, object obj);

        //*****************************************************************************************************
        // Description: appel l'�v�nement DoSendMessage. cette methode permet aux classes filles 
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

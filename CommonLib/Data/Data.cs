/***************************************************************************/
// PROJET : BTCommand : system de commande param�trable pour �quipement
// ayant une m�canisme de commande par liaison s�rie/ethernet/http
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
    public delegate void EventDataValueChange();
    public class Data : BaseObject
    {
        #region D�claration des donn�es de la classe
        // valeur minimale
        protected int m_MinVal;
        // valeur maximale
        protected int m_MaxVal;
        // valeur par d�faut
        protected int m_DefVal;
        // valeur courante
        protected int m_CurrentVal;
        // taille en bits
        private int m_Size;
        // donn�e constante??
        private bool m_bConstant;
        // visible a l'utilisateur?
        private bool m_bIsUserVisible = true;

        private bool m_bIsSaturationNotified = false;
        #endregion

        #region Events
        // �v�nement lev� lorsque la valeur change.
        // utilis� en mode commande pour que les controles rafrachissent leur affichage
        public event EventDataValueChange DataValueChanged;
        #endregion

        #region constructeur et init
        /// <summary>
        /// constructeur par d�faut
        /// </summary>
        public Data()
        {
            m_MinVal = 0;
            m_MaxVal = 255;
            m_DefVal = 0;
            m_Size = (int)DATA_SIZE.DATA_SIZE_8B;
            m_bConstant = false;
        }

        /// <summary>
        /// constructeur "param�tr�" principalement utilis� par les wizards
        /// </summary>
        /// <param name="strSymbol">Symbol de la donn�e</param>
        /// <param name="DefaultValue">valeur par d�faut</param>
        /// <param name="size">Taille de la donn�e</param>
        /// <param name="bIsConstant">indique si la donn�e est une constante</param>
        public Data(string strSymbol, int DefaultValue, int size, bool bIsConstant)
        {
            m_strSymbol = strSymbol;
            m_DefVal = DefaultValue;
            m_Size = size;
            m_bConstant = bIsConstant;
            switch ((DATA_SIZE)this.SizeInBits)
            {
                case DATA_SIZE.DATA_SIZE_1B:
                    m_MaxVal = 1;
                    m_MinVal = 0;
                    break;
                case DATA_SIZE.DATA_SIZE_2B:
                    m_MaxVal = 3;
                    m_MinVal = 0;
                    break;
                case DATA_SIZE.DATA_SIZE_4B:
                    m_MaxVal = 15;
                    m_MinVal = 0;
                    break;
                case DATA_SIZE.DATA_SIZE_8B:
                    m_MaxVal = 255;
                    m_MinVal = 0;
                    break;
                case DATA_SIZE.DATA_SIZE_16B:
                    m_MaxVal = 32767;
                    m_MinVal = -32768;
                    break;
                case DATA_SIZE.DATA_SIZE_16BU:
                    m_MaxVal = 0xFFFF;
                    m_MinVal = 0;
                    break;
                case DATA_SIZE.DATA_SIZE_32B:
                    m_MaxVal = int.MaxValue;
                    m_MinVal = int.MinValue;
                    break;
                default:
                    System.Diagnostics.Debug.Assert(false);
                    break;
            }
            this.UpdateUserVisibility();
        }

        /// <summary>
        /// constructeur "param�tr�" principalement utilis� par les wizards
        /// </summary>
        /// <param name="strSymbol">Symbol de la donn�e</param>
        /// <param name="DefaultValue">valeur par d�faut</param>
        /// <param name="size">Taille de la donn�e</param>
        /// <param name="bIsConstant">indique si la donn�e est une constante</param>
        public Data(string strSymbol, int DefaultValue, DATA_SIZE size, bool bIsConstant)
        {
            m_strSymbol = strSymbol;
            m_DefVal = DefaultValue;
            m_Size = (int)size;
            m_bConstant = bIsConstant;
            switch ((DATA_SIZE)this.SizeInBits)
            {
                case DATA_SIZE.DATA_SIZE_1B:
                    m_MaxVal = 1;
                    m_MinVal = 0;
                    break;
                case DATA_SIZE.DATA_SIZE_2B:
                    m_MaxVal = 3;
                    m_MinVal = 0;
                    break;
                case DATA_SIZE.DATA_SIZE_4B:
                    m_MaxVal = 15;
                    m_MinVal = 0;
                    break;
                case DATA_SIZE.DATA_SIZE_8B:
                    m_MaxVal = 255;
                    m_MinVal = 0;
                    break;
                case DATA_SIZE.DATA_SIZE_16B:
                    m_MaxVal = 32767;
                    m_MinVal = -32768;
                    break;
                case DATA_SIZE.DATA_SIZE_16BU:
                    m_MaxVal = 0xFFFF;
                    m_MinVal = 0;
                    break;
                case DATA_SIZE.DATA_SIZE_32B:
                    m_MaxVal = int.MaxValue;
                    m_MinVal = int.MinValue;
                    break;
                default:
                    System.Diagnostics.Debug.Assert(false);
                    break;
            }
            this.UpdateUserVisibility();

        }

        /// <summary>
        /// appel� au lancement en mode Commande, initialise la valeur courante � la valeur par d�faut
        /// </summary>
        public void InitVal()
        {
            m_CurrentVal = m_DefVal;
        }
        #endregion

        #region attributs
        /// <summary>
        /// obtient ou assigne la valeur par d�faut
        /// </summary>
        public int DefaultValue
        {
            get
            {
                return m_DefVal;
            }
            set
            {
                m_DefVal = value;
            }
        }

        /// <summary>
        /// assigne ou obtient la valeur courante de la donn�e
        /// prot�g� en �criture par les bornes min et max, et lance des event en cas de saturation
        /// </summary>
        public virtual int Value
        {
            get
            {
                return m_CurrentVal ;
            }
            set
            {
                // on test d'abord savoir si la valeur a chang�
                bool bValueChange = false;
                bool bIsInSaturation = false;
                if (m_CurrentVal != value)
                    bValueChange = true;
                if (bValueChange)
                {
                    // selon les bornes et si la donn�e est constante, on effectue l'assignation...
                    if (value >= m_MinVal && value <= m_MaxVal && !this.IsConstant)
                    {
                        m_CurrentVal = value;
                    }

                    if (!this.IsConstant)
                    {
                        // ...ou la saturation en cas de d�passement des bornes
                        if (value < m_MinVal)
                        {
                            m_CurrentVal = m_MinVal;
                            if (!m_bIsSaturationNotified)
                            {
                                LogEvent log = new LogEvent(LOG_EVENT_TYPE.INFO, string.Format("Data {0} : Enter min value saturation ({1})", Symbol, m_MinVal));
                                AddLogEvent(log);
                                bValueChange = false;
                                m_bIsSaturationNotified = true;
                            }
                            bIsInSaturation = true;
                        }
                        else if (value > m_MaxVal)
                        {
                            if (!m_bIsSaturationNotified)
                            {
                                m_CurrentVal = m_MaxVal;
                                LogEvent log = new LogEvent(LOG_EVENT_TYPE.INFO, string.Format("Data {0} : Enter max value saturation ({1})", Symbol, m_MaxVal));
                                AddLogEvent(log);
                                bValueChange = false;
                                m_bIsSaturationNotified = true;
                            }
                            bIsInSaturation = true;
                        }

                        if (m_bIsSaturationNotified && !bIsInSaturation)
                        {
                            LogEvent log = new LogEvent(LOG_EVENT_TYPE.INFO, string.Format("Data {0} : Leave saturation", Symbol));
                            AddLogEvent(log);
                            m_bIsSaturationNotified = false;
                        }
                    }
                    else
                    {
                        LogEvent log = new LogEvent(LOG_EVENT_TYPE.WARNING, string.Format("Data {0} is constant, value can't be set", Symbol));
                        AddLogEvent(log);
                    }
                }
                // si la valeur de la donn�e a chang� on le notifie
                if (bValueChange && DataValueChanged != null)
                    DataValueChanged();
            }
        }

        /// <summary>
        /// accesseur sur la valeur minimum
        /// </summary>
        public int Minimum
        {
            get
            {
                return m_MinVal;
            }
            set
            {
                m_MinVal = value;
            }

        }

        /// <summary>
        /// accesseur sur la valeur maximum
        /// </summary>
        public int Maximum
        {
            get
            {
                return m_MaxVal;
            }
            set
            {
                m_MaxVal = value;
            }
        }

        /// <summary>
        /// obtient la taille en bit de la donn�e
        /// </summary>
        public int SizeInBits
        {
            get
            {
                // on ne prend que le poid faible, le poid fort indiquant si la donn�e est 
                // sign�e ou non
                return m_Size & 0xFF;
            }
        }

        /// <summary>
        /// obtient ou assigne la taille et le signage de la donn�e
        /// </summary>
        public int SizeAndSign
        {
            get
            {
                // on ne prend que le poid faible, le poid fort indiquant si la donn�e est 
                // sign�e ou non
                return m_Size;
            }
            set
            {
                m_Size = value;
            }
        }

        /// <summary>
        /// obtient ou d�finit si la valeur est constante
        /// </summary>
        public bool IsConstant
        {
            get
            {
                return m_bConstant;
            }
            set
            {
                m_bConstant = value;
            }
        }

        /// <summary>
        /// obtient ou assigne si la valeur est visible � l'utilisateur
        /// (les donn�es de controle des protocoles ne sont pas visibles)
        /// </summary>
        public bool IsUserVisible
        {
            get
            {
                return m_bIsUserVisible;
            }
            set
            {
                m_bIsUserVisible = value;
            }
        }
        #endregion

        #region ReadIn / WriteOut
        /// <summary>
        /// Lit les donn�es de l'objet a partir de son noeud XML
        /// </summary>
        /// <param name="Node">Noeud Xml de l'objet</param>
        /// <param name="TypeApp">type d'application courante</param>
        /// <returns>true si la lecture s'est bien pass�</returns>
        public override bool ReadIn(XmlNode Node, TYPE_APP TypeApp)
        {
            if (!base.ReadIn(Node, TypeApp))
                return false;
            // on lit les propri�t� de l'objet
            XmlNode AttrMin = Node.Attributes.GetNamedItem(XML_CF_ATTRIB.Min.ToString());
            XmlNode AttrMax = Node.Attributes.GetNamedItem(XML_CF_ATTRIB.Max.ToString());
            XmlNode AttrDefVal = Node.Attributes.GetNamedItem(XML_CF_ATTRIB.DefVal.ToString());
            XmlNode AttrSize = Node.Attributes.GetNamedItem(XML_CF_ATTRIB.size.ToString());
            XmlNode AttrConstant = Node.Attributes.GetNamedItem(XML_CF_ATTRIB.Constant.ToString());
            if (AttrMin == null 
                || AttrMax == null 
                || AttrDefVal == null 
                || AttrSize == null 
                )
                return false;
            m_MinVal = int.Parse(AttrMin.Value);
            m_MaxVal = int.Parse(AttrMax.Value);
            m_DefVal = int.Parse(AttrDefVal.Value);
            m_Size = int.Parse(AttrSize.Value);
            m_CurrentVal = m_DefVal;
            if (AttrConstant != null)
                m_bConstant = bool.Parse(AttrConstant.Value);

            return true;
        }

        /// <summary>
        /// �crit les donn�es de l'objet dans le fichier XML
        /// </summary>
        /// <param name="XmlDoc">Document XML courant</param>
        /// <param name="Node">Noeud parent du controle dans le document</param>
        /// <returns>true si l'�criture s'est d�roul�e avec succ�s</returns>
        public override bool WriteOut(XmlDocument XmlDoc, XmlNode Node)
        {
            base.WriteOut(XmlDoc, Node);
            // on �crit les propri�t� de l'objet
            XmlAttribute AttrMin = XmlDoc.CreateAttribute(XML_CF_ATTRIB.Min.ToString());
            XmlAttribute AttrMax = XmlDoc.CreateAttribute(XML_CF_ATTRIB.Max.ToString());
            XmlAttribute AttrDefVal = XmlDoc.CreateAttribute(XML_CF_ATTRIB.DefVal.ToString());
            XmlAttribute AttrSize = XmlDoc.CreateAttribute(XML_CF_ATTRIB.size.ToString());
            XmlAttribute AttrConstant = XmlDoc.CreateAttribute(XML_CF_ATTRIB.Constant.ToString());
            AttrMin.Value = m_MinVal.ToString();
            AttrMax.Value = m_MaxVal.ToString();
            AttrDefVal.Value = m_DefVal.ToString();
            AttrSize.Value = m_Size.ToString();
            AttrConstant.Value = m_bConstant.ToString();
            Node.Attributes.Append(AttrMin);
            Node.Attributes.Append(AttrMax);
            Node.Attributes.Append(AttrDefVal);
            Node.Attributes.Append(AttrSize);
            Node.Attributes.Append(AttrConstant);
            return true;
        }

        /// <summary>
        /// termine la lecture de l'objet. utilis� en mode Commande pour r�cup�rer les r�f�rence
        /// vers les objets utilis�s
        /// </summary>
        /// <param name="Doc">Document courant</param>
        /// <returns>true si tout s'est bien pass�</returns>
        public override bool FinalizeRead(BTDoc Doc)
        {
            //  on a juste a initialiser la valeur courante avec la valeur par d�faut
            InitVal();
            return true;
        }

        #endregion

        #region fonctions specifiques aux donn�es
        /// <summary>
        /// utilis� pour mettre a jour l'attribut "user visible" de la donn�es
        /// les donn�es de controls sont invisibles en dehors de la liste des donn�es de la trame
        /// </summary>
        public void UpdateUserVisibility()
        {
            // met a jour l'attribut UserVisible en fonction du symbol (donn�es de control invisible)
            if (this.Symbol.EndsWith(Cste.STR_SUFFIX_CTRLDATA))
                IsUserVisible = false;
        }
        #endregion

        #region Gestion des AppMessages
        /// <summary>
        /// effectue les op�ration n�cessaire lors de la r�c�ption d'un message
        /// </summary>
        /// <param name="Mess">Type de message</param>
        /// <param name="obj">objet contenant les param�tres du messages</param>
        /// <param name="TypeApp">Type d'application courante</param>
        public override void TraiteMessage(MESSAGE Mess, object obj, TYPE_APP TypeApp)
        {
            //  rien a faire
            // les donn�es sont l'objet du plus bas niveau
            // il n'utilise personne donc rien a mettre a jour
        }
        #endregion

    }
}

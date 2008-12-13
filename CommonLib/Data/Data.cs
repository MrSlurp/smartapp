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

namespace CommonLib
{
    public delegate void EventDataValueChange();
    public class Data : BaseObject
    {
        #region Déclaration des données de la classe
        // valeur minimale
        protected int m_MinVal;
        // valeur maximale
        protected int m_MaxVal;
        // valeur par défaut
        protected int m_DefVal;
        // valeur courante
        protected int m_CurrentVal;
        // taille en bits
        private int m_Size;
        // donnée constante??
        private bool m_bConstant;
        // visible a l'utilisateur?
        private bool m_bIsUserVisible = true;

        private bool m_bIsSaturationNotified = false;
        #endregion

        #region Events
        // évènement levé lorsque la valeur change.
        // utilisé en mode commande pour que les controles rafrachissent leur affichage
        public event EventDataValueChange DataValueChanged;
        #endregion

        #region constructeur et init
        //*****************************************************************************************************
        // Description: constructeur par défaut
        // Return: /
        //*****************************************************************************************************
        public Data()
        {
            m_MinVal = 0;
            m_MaxVal = 255;
            m_DefVal = 0;
            m_Size = (int)DATA_SIZE.DATA_SIZE_8B;
            m_bConstant = false;
        }

        //*****************************************************************************************************
        // Description: constructeur "paramétré" principalement utilisé par les wizards
        // Return: /
        //*****************************************************************************************************
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
        //*****************************************************************************************************
        // Description: appelé au lancement en mode Commande, initialise la valeur courante
        // Return: /
        //*****************************************************************************************************
        public void InitVal()
        {
            m_CurrentVal = m_DefVal;
        }
        #endregion

        #region attributs
        //*****************************************************************************************************
        // Description: accesseur de la valeur par défaut
        // Return: /
        //*****************************************************************************************************
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

        //*****************************************************************************************************
        // Description: accesseur de la valeur courante
        // protégé en écriture par les bornes de la donnée ou l'attribut "constant"
        // Return: /
        //*****************************************************************************************************
        public virtual int Value
        {
            get
            {
                return m_CurrentVal ;
            }
            set
            {
                // on test d'abord savoir si la valeur a changé
                bool bValueChange = false;
                bool bIsInSaturation = false;
                if (m_CurrentVal != value)
                    bValueChange = true;
                if (bValueChange)
                {
                    // selon les bornes et si la donnée est constante, on effectue l'assignation...
                    if (value >= m_MinVal && value <= m_MaxVal && !this.IsConstant)
                    {
                        m_CurrentVal = value;
                    }

                    if (!this.IsConstant)
                    {
                        // ...ou la saturation en cas de dépassement des bornes
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
                // si la valeur de la donnée a changé on le notifie
                if (bValueChange && DataValueChanged != null)
                    DataValueChanged();
            }
        }

        //*****************************************************************************************************
        // Description: accesseur sur le minimum
        // Return: /
        //*****************************************************************************************************
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

        //*****************************************************************************************************
        // Description: accesseur sur le maximum
        // Return: /
        //*****************************************************************************************************
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

        //*****************************************************************************************************
        // Description: acceseur sur la Taille de la donnée en bits
        // Return: /
        //*****************************************************************************************************
        public int SizeInBits
        {
            get
            {
                // on ne prend que le poid faible, le poid fort indiquant si la donnée est 
                // signée ou non
                return m_Size & 0xFF;
            }
        }

        public int SizeAndSign
        {
            get
            {
                // on ne prend que le poid faible, le poid fort indiquant si la donnée est 
                // signée ou non
                return m_Size;
            }
            set
            {
                m_Size = value;
            }
        }

        //*****************************************************************************************************
        // Description: accesseur de la propriété "Constant"
        // Return: /
        //*****************************************************************************************************
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

        //*****************************************************************************************************
        // Description: accesseur de l'attribut "UserVisible"
        // Return: /
        //*****************************************************************************************************
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
        //*****************************************************************************************************
        // Description: Lit les données de l'objet a partir de son noeud XML
        // Return: /
        //*****************************************************************************************************
        public override bool ReadIn(XmlNode Node, TYPE_APP TypeApp)
        {
            if (!base.ReadIn(Node, TypeApp))
                return false;
            // on lit les propriété de l'objet
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

        //*****************************************************************************************************
        // Description: ecrit les données de l'objet a partir de son noeud XML
        // Return: /
        //*****************************************************************************************************
        public override bool WriteOut(XmlDocument XmlDoc, XmlNode Node)
        {
            base.WriteOut(XmlDoc, Node);
            // on écrit les propriété de l'objet
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

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public override bool FinalizeRead(BTDoc Doc)
        {
            //  on a juste a initialiser la valeur courante avec la valeur par défaut
            InitVal();
            return true;
        }

        #endregion

        #region fonctions specifiques aux données
        //*****************************************************************************************************
        // Description: utilisé pour mettre a jour l'attribut "user visible" de la données
        // les données de controls sont invisibles en dehors de la liste des données de la trame
        // Return: /
        //*****************************************************************************************************
        public void UpdateUserVisibility()
        {
            // met a jour l'attribut UserVisible en fonction du symbol (données de control invisible)
            if (this.Symbol.EndsWith(Cste.STR_SUFFIX_CTRLDATA))
                IsUserVisible = false;
        }
        #endregion

        #region Gestion des AppMessages
        //*****************************************************************************************************
        // Description: termine la lecture de l'objet. utilisé en mode Commande pour récupérer les référence
        // vers les objets utilisés et effectuer d'autre traitements
        // Return: /
        //*****************************************************************************************************
        public override void TraiteMessage(MESSAGE Mess, object obj, TYPE_APP TypeApp)
        {
            //  rien a faire
            // les données sont l'objet du plus bas niveau
            // il n'utilise personne donc rien a mettre a jour
        }
        #endregion

    }
}

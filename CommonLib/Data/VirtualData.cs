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
    public delegate void EventVirtualDataValueChange(VirtualData vData);
    public delegate void EventVirtualDataSaveStateChange(VirtualData vData);
    public class VirtualData : Data
    {
        private bool m_bSaveInCliche = false;
        
        public bool SaveInCliche
        {
            get
            {
                return m_bSaveInCliche;
            }
            set
            {
                bool bOldState = m_bSaveInCliche; 
                m_bSaveInCliche = value;
                if (VirtualDataSaveStateChange != null && bOldState != m_bSaveInCliche)
                    VirtualDataSaveStateChange(this);                
            }
            
        }  
        #region Events
        public event EventVirtualDataValueChange VirtualDataValueChanged;
        public event EventVirtualDataSaveStateChange VirtualDataSaveStateChange;
        #endregion

        #region attributs
        /// <summary>
        /// surchargé de la classe Data pour appeler le bon évènements
        /// oui c'est juste pour ca
        /// </summary>
        public override int Value
        {
            get
            {
                return m_CurrentVal;
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
                                LogEvent log = new LogEvent(LOG_EVENT_TYPE.INFO, string.Format(Lang.LangSys.C("Data {0} : Enter min value saturation ({1})"), Symbol, m_MinVal));
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
                                LogEvent log = new LogEvent(LOG_EVENT_TYPE.INFO, string.Format(Lang.LangSys.C("Data {0} : Enter max value saturation ({1})"), Symbol, m_MaxVal));
                                AddLogEvent(log);
                                bValueChange = false;
                                m_bIsSaturationNotified = true;
                            }
                            bIsInSaturation = true;
                        }

                        if (m_bIsSaturationNotified && !bIsInSaturation)
                        {
                            LogEvent log = new LogEvent(LOG_EVENT_TYPE.INFO, string.Format(Lang.LangSys.C("Data {0} : Leave saturation"), Symbol));
                            AddLogEvent(log);
                            m_bIsSaturationNotified = false;
                        }
                    }
                    else
                    {
                        LogEvent log = new LogEvent(LOG_EVENT_TYPE.WARNING, string.Format(Lang.LangSys.C("Data {0} is constant, value can't be set"), Symbol));
                        AddLogEvent(log);
                    }
                }
                // si la valeur de la donnée a changé on le notifie
                if (bValueChange && VirtualDataValueChanged != null)
                    VirtualDataValueChanged(this);

            }
        }
        #endregion

        #region méthodes publiques
        public bool TestValue(int Value)
        {
            // ...ou la saturation en cas de dépassement des bornes
            if (Value < m_MinVal)
            {
                return false;
            }
            else if (Value > m_MaxVal)
            {
                return false;
            }
            return true;
        }
        #endregion

        #region Read In / Write Out
        /// <summary>
        /// Lit les données de l'objet a partir de son noeud XML
        /// </summary>
        /// <param name="Node">Noeud Xml de l'objet</param>
        /// <param name="TypeApp">type d'application courante</param>
        /// <returns>true si la lecture s'est bien passé</returns>
        public override bool ReadIn(XmlNode Node, BTDoc document)
        {
            // en cas de lecture des clichés pour affichage du contenu/edition
            return base.ReadIn(Node, document);           
        }
    
        /// <summary>
        /// NE JAMAIS UTILISER
        /// </summary>
        /// <param name="XmlDoc"></param>
        /// <param name="Node"></param>
        /// <returns></returns>
        public override bool WriteOut(XmlDocument XmlDoc, XmlNode Node, BTDoc document)
        {
            // il est interdit de faire une écriture complète des paramètres pour une données virtuelle
            // enfin si on peu mais ca sert à rien puisqu'on ne mémorise pas la valeur
            return false;
        }

        /// <summary>
        /// lit un "cliché" de la valeur (juste la valeur de la données)
        /// </summary>
        /// <param name="Node">Noeud de a donnée</param>
        /// <param name="TypeApp">Type de l'application</param>
        /// <returns>true en cas de succès</returns>
        public bool ReadInInstantImage(XmlNode Node, TYPE_APP TypeApp)
        {
            // on ne relit que la valeur par défaut
            XmlNode AttrDefVal = Node.Attributes.GetNamedItem(XML_CF_ATTRIB.DefVal.ToString());
            if (AttrDefVal == null)
                return false;
                
            // et on fait ce cette valeur par defaut la valeur courante
            DefaultValue = int.Parse(AttrDefVal.Value);
            this.VirtualInitVal();
            return true;
        }

        /// <summary>
        /// appelé au lancement en mode Commande, initialise la valeur courante à la valeur par défaut
        /// </summary>
        public void VirtualInitVal()
        {
            Value = DefaultValue;
        }
    

        /// <summary>
        /// écrit un "cliché" de la valeur 
        /// on écrit l'équivalent de l'enregistrement afin de pouvoir relire 
        /// le cliché dans l'editeur de cliché en ayant la taille, le min et le max
        /// </summary>
        /// <param name="XmlDoc"></param>
        /// <param name="Node"></param>
        /// <returns></returns>
        public bool WriteOutInstantImage(XmlDocument XmlDoc, XmlNode Node)
        {
            // on écrit la base pour avoir le symbole
            this.DefaultValue = this.Value;
            //WriteOutBaseObject(XmlDoc, Node);
            return base.WriteOut(XmlDoc, Node, null);
        }
        #endregion

    }
}

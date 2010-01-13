using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Xml;
using System.Windows.Forms;

namespace CommonLib
{
    public class BTTimer : BaseObject, IScriptable
    {
        #region Déclaration des données de la classe
        // script executé par le timer
        private StringCollection m_ScriptLines = new StringCollection();
        // période du timer
        int m_iPeriod = 1000;

        private bool m_bAutoStart = true;
        #endregion

        #region donnée spécifiques aux fonctionement en mode Command
        // objet Timer utilisé en mode Command
        Timer m_Timer = new Timer();
        bool m_bTimerEnabled = false;
        // executer de script du document
        ScriptExecuter m_Executer = null;
        #endregion

        #region propriétées de la classe
        /// <summary>
        /// obtient ou assigne la période du timer en ms
        /// </summary>
        public int Period
        {
            get
            {
                return m_iPeriod;
            }
            set
            {
                m_iPeriod = value;
            }
        }

        /// <summary>
        /// obtient ou assigne le script executé par le timer
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

        /// <summary>
        /// obtient ou assigne la booléen indiquant si le timer est activé automatiquement au démarrage
        /// </summary>
        public bool AutoStart
        {
            get
            {
                return m_bAutoStart;
            }
            set
            {
                m_bAutoStart = value;
            }
        }

        /// <summary>
        /// obtient ou assigne l'executer de l'écran
        /// </summary>
        public ScriptExecuter Executer
        {
            get
            {
                return m_Executer;
            }
            set
            {
                m_Executer = value;
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
            // on lit l'attribut
            XmlNode PeriodAttrib = Node.Attributes.GetNamedItem(XML_CF_ATTRIB.Period.ToString());
            if (PeriodAttrib == null)
                return false;

            // et le script
            m_iPeriod = int.Parse(PeriodAttrib.Value);

            for (int i = 0; i < Node.ChildNodes.Count; i++)
            {
                if (Node.ChildNodes[i].Name == XML_CF_TAG.Line.ToString()
                        && Node.ChildNodes[i].FirstChild != null)
                {
                    m_ScriptLines.Add(Node.ChildNodes[i].FirstChild.Value);
                }
            }
            XmlNode AutoStartAttrib = Node.Attributes.GetNamedItem(XML_CF_ATTRIB.AutoStart.ToString());
            if (AutoStartAttrib != null)
                m_bAutoStart = bool.Parse(AutoStartAttrib.Value);

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
            // on écrit la période
            XmlAttribute AttrPeriod = XmlDoc.CreateAttribute(XML_CF_ATTRIB.Period.ToString());
            XmlAttribute AutoStartAttrib = XmlDoc.CreateAttribute(XML_CF_ATTRIB.AutoStart.ToString());
            AttrPeriod.Value = m_iPeriod.ToString();
            AutoStartAttrib.Value = m_bAutoStart.ToString();
            Node.Attributes.Append(AttrPeriod);
            Node.Attributes.Append(AutoStartAttrib);
            // et le script
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
            // initialisation de l'objet Timer
            this.m_Executer = Doc.Executer;
            m_Timer.Interval = this.m_iPeriod;
            m_Timer.Tick += new EventHandler(OnTimerTick);
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
            // en mode config, on execute les différents traitements sur les scripts
            if (TypeApp == TYPE_APP.SMART_CONFIG)
            {
                ScriptTraiteMessage(this, Mess, m_ScriptLines, obj);
            }
            else
            {
                // on traite les message de mise en marche ou d'arret
                switch (Mess)
                {
                    case MESSAGE.MESS_CMD_RUN:
                        if (m_bAutoStart)
                            this.StartTimer();
                        break;
                    case MESSAGE.MESS_CMD_STOP:
                        this.StopTimer();
                        break;
                    default:
                        break;
                }
            }
        }


        #endregion

        #region fonction d'execution en mode Command
        /// <summary>
        /// démarre le timer
        /// </summary>
        public void StartTimer()
        {
            m_Timer.Start();
            m_bTimerEnabled = true;
        }

        /// <summary>
        /// stop le timer
        /// </summary>
        public void StopTimer()
        {
            m_Timer.Stop();
            m_bTimerEnabled = false;
        }

        /// <summary>
        /// execute le script lors que l'évènement de l'objet timer est levé
        /// </summary>
        /// <param name="sender">timer ayant levé l'évènement</param>
        /// <param name="e">argument de l'event</param>
        private void OnTimerTick(object sender, EventArgs e)
        {
            if (m_bTimerEnabled)
            {
                m_Timer.Stop();

                CommonLib.PerfChrono theChrono = new PerfChrono();
                this.m_Executer.ExecuteScript(this.m_ScriptLines);
                theChrono.EndMeasure("InstanceName = " + this.Symbol);
                m_Timer.Start();
            }
        }
        #endregion
    }
}

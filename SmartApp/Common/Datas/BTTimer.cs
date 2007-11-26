using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Xml;
using System.Windows.Forms;
using SmartApp.Scripts;

namespace SmartApp.Datas
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
        //*****************************************************************************************************
        // Description: accesseur sur la période
        // Return: /
        //*****************************************************************************************************
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

        //*****************************************************************************************************
        // Description: accesseur de l'executeur de script
        // Return: /
        //*****************************************************************************************************
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
        //*****************************************************************************************************
        // Description: ecrit les données de l'objet a partir de son noeud XML
        // Return: /
        //*****************************************************************************************************
        public override bool ReadIn(System.Xml.XmlNode Node)
        {
            bool bRet = base.ReadIn(Node);
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

        //*****************************************************************************************************
        // Description: ecrit les données de l'objet a partir de son noeud XML
        // Return: /
        //*****************************************************************************************************
        public override bool WriteOut(System.Xml.XmlDocument XmlDoc, System.Xml.XmlNode Node)
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

        //*****************************************************************************************************
        // Description: termine la lecture de l'objet. utilisé en mode Commande pour récupérer les référence
        // vers les objets utilisés
        // Return: /
        //*****************************************************************************************************
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
        //*****************************************************************************************************
        // Description: effectue les opération nécessaire lors de la récéption d'un message
        // Return: /
        //*****************************************************************************************************
        public override void TraiteMessage(MESSAGE Mess, object obj)
        {
            // en mode config, on execute les différents traitements sur les scripts
            if (Program.TypeApp == TYPE_APP.SMART_CONFIG)
            {
                ScriptTraiteMessage(Mess, m_ScriptLines, obj);
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
                        || ((MessAskDelete)obj).TypeOfItem == typeof(BTTimer)
                        )
                    {
                        MessAskDelete MessParam = (MessAskDelete)obj;
                        for (int i = 0; i < Script.Count; i++)
                        {
                            string stritem = SmartApp.Scripts.ScriptParser.GetLineToken(Script[i], SmartApp.Scripts.ScriptParser.INDEX_TOKEN_SYMBOL);
                            if (stritem == MessParam.WantDeletetItemSymbol)
                            {
                                string strMess = string.Format("Timer {0} Script: Line {1} will be removed", Symbol, i + 1);
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
                    if (((MessItemRenamed)obj).TypeOfItem == typeof(Trame)
                        || ((MessItemRenamed)obj).TypeOfItem == typeof(Function)
                        || ((MessItemRenamed)obj).TypeOfItem == typeof(Logger)
                        || ((MessItemRenamed)obj).TypeOfItem == typeof(BTTimer)
                        )
                    {
                        MessItemRenamed MessParam = (MessItemRenamed)obj;
                        for (int i = 0; i < Script.Count; i++)
                        {
                            string stritem = SmartApp.Scripts.ScriptParser.GetLineToken(Script[i], SmartApp.Scripts.ScriptParser.INDEX_TOKEN_SYMBOL);
                            if (stritem == MessParam.OldItemSymbol)
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

        #region fonction d'execution en mode Command
        //*****************************************************************************************************
        // Description: démmare le Timer
        // Return: /
        //*****************************************************************************************************
        public void StartTimer()
        {
            m_Timer.Start();
            m_bTimerEnabled = true;
        }

        //*****************************************************************************************************
        // Description: Arrète le timer
        // Return: /
        //*****************************************************************************************************
        public void StopTimer()
        {
            m_Timer.Stop();
            m_bTimerEnabled = false;
        }

        //*****************************************************************************************************
        // Description: execute le script lors que l'évènement de l'objet timer est levé
        // Return: /
        //*****************************************************************************************************
        private void OnTimerTick(object sender, EventArgs e)
        {
            if (m_bTimerEnabled)
            {
                m_Timer.Stop();
                this.m_Executer.ExecuteScript(this.m_ScriptLines);
                m_Timer.Start();
            }
        }
        #endregion
    }
}

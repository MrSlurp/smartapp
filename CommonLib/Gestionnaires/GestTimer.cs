using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace CommonLib
{
    public class GestTimer : BaseGest
    {
        #region fonction "utilitaires"
        public override BaseObject AddNewObject()
        {
            BTTimer dat = new BTTimer();
            dat.Symbol = GetNextDefaultSymbol();
            this.AddObj(dat);
            return dat;
        }

        /// <summary>
        /// renvoie le prochain symbol libre pour une nouvelle donnée
        /// </summary>
        /// <returns>prochain symbol libre</returns>
        public override string GetNextDefaultSymbol()
        {
            for (int i = 0; i < MAX_DEFAULT_ITEM_SYMBOL; i++)
            {
                string strSymbolTemp = string.Format(DEFAULT_TIMER_SYMB, i);
                if (GetFromSymbol(strSymbolTemp) == null)
                {
                    return strSymbolTemp;
                }
            }
            return "";
        }
        #endregion

        #region ReadIn  / WriteOut
        /// <summary>
        /// Lit les données de l'objet a partir de son noeud XML
        /// </summary>
        /// <param name="Node">Noeud Xml de l'objet</param>
        /// <param name="TypeApp">type d'application courante</param>
        /// <returns>true si la lecture s'est bien passé</returns>
        public override bool ReadIn(XmlNode Node, TYPE_APP TypeApp)
        {
            XmlNode NodeTimerSection = null;
            for (int i = 0; i < Node.ChildNodes.Count; i++)
            {
                if (Node.ChildNodes[i].Name == XML_CF_TAG.TimerSection.ToString())
                    NodeTimerSection = Node.ChildNodes[i];
            }
            if (NodeTimerSection == null)
                return false;

            for (int i = 0; i < NodeTimerSection.ChildNodes.Count; i++)
            {
                XmlNode ChildNode = NodeTimerSection.ChildNodes[i];
                if (ChildNode.Name != XML_CF_TAG.Timer.ToString())
                    continue;

                BTTimer NewTimer = new BTTimer();
                if (NewTimer != null)
                {
                    if (!NewTimer.ReadIn(ChildNode, TypeApp))
                        return false;

                    this.AddObj(NewTimer);
                }
            }
            return true;
        }

        /// <summary>
        /// écrit les données de l'objet dans le fichier XML
        /// </summary>
        /// <param name="XmlDoc">Document XML courant</param>
        /// <param name="Node">Noeud parent du controle dans le document</param>
        /// <returns>true si l'écriture s'est déroulée avec succès</returns>
        public override bool WriteOut(XmlDocument XmlDoc, XmlNode Node)
        {
            XmlNode NodeTimerSection = XmlDoc.CreateElement(XML_CF_TAG.TimerSection.ToString());
            Node.AppendChild(NodeTimerSection);
            for (int i = 0; i < this.m_ListObject.Count; i++)
            {
                BTTimer dt = (BTTimer)m_ListObject[i];

                XmlNode XmlTimer = XmlDoc.CreateElement(XML_CF_TAG.Timer.ToString());
                dt.WriteOut(XmlDoc, XmlTimer);
                NodeTimerSection.AppendChild(XmlTimer);
            }
            return true;
        }
        #endregion
    }
}

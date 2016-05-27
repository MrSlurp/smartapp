using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace CommonLib
{
    public class GestLogger : BaseGest
    {
        #region fonction "utilitaires"
        public override BaseObject AddNewObject(BTDoc document)
        {
            Logger dat = new Logger();
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
                string strSymbolTemp = string.Format(DEFAULT_LOGGER_SYMB, i);
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
        public override bool ReadIn(XmlNode Node, BTDoc document)
        {
            XmlNode NodeLoggerSection = null;
            for (int i = 0; i < Node.ChildNodes.Count; i++)
            {
                if (Node.ChildNodes[i].Name == XML_CF_TAG.LoggerSection.ToString())
                    NodeLoggerSection = Node.ChildNodes[i];
            }
            if (NodeLoggerSection == null)
                return false;

            for (int i = 0; i < NodeLoggerSection.ChildNodes.Count; i++)
            {
                XmlNode ChildNode = NodeLoggerSection.ChildNodes[i];
                if (ChildNode.Name != XML_CF_TAG.Logger.ToString())
                    continue;

                Logger NewLogger = new Logger();
                if (NewLogger != null)
                {
                    if (!NewLogger.ReadIn(ChildNode, document))
                        return false;

                    this.AddObj(NewLogger);
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
        public override bool WriteOut(XmlDocument XmlDoc, XmlNode Node, BTDoc document)
        {
            XmlNode NodeLoggerSection = XmlDoc.CreateElement(XML_CF_TAG.LoggerSection.ToString());
            Node.AppendChild(NodeLoggerSection);
            for (int i = 0; i < this.m_ListObject.Count; i++)
            {
                Logger dt = (Logger)m_ListObject[i];

                XmlNode XmlLogger = XmlDoc.CreateElement(XML_CF_TAG.Logger.ToString());
                dt.WriteOut(XmlDoc, XmlLogger,document);
                NodeLoggerSection.AppendChild(XmlLogger);
            }
            return true;
        }
        #endregion
    }
}

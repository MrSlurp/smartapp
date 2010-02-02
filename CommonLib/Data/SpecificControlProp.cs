using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Drawing;
using System.Xml;

namespace CommonLib
{
    /// <summary>
    /// classe abstraite définissant les fonction obligatoire à implémenter par les propriété spécifiques
    /// </summary>
    public abstract class SpecificControlProp
    {
        #region méthodes abstraites de la classe
        /// <summary>
        /// fonction de lecture des propriété spécifiques
        /// </summary>
        /// <param name="Node">Noeud du control</param>
        /// <returns>true si la lecture c'est bien passé, sinon false</returns>
        public abstract bool ReadIn(XmlNode Node);

        /// <summary>
        /// fonction d'écriture des propriété spécifiques
        /// </summary>
        /// <param name="XmlDoc">Document Xml</param>
        /// <param name="Node">Noeud du control</param>
        /// <returns>true si l'écriture c'est bien passé</returns>
        public abstract bool WriteOut(XmlDocument XmlDoc, XmlNode Node);

        /// <summary>
        /// copy les paramètres spécifiques à l'identique
        /// </summary>
        /// <param name="SrcSpecificProp">paramètres sources</param>
        public abstract void CopyParametersFrom(SpecificControlProp SrcSpecificProp);
        #endregion

        #region methodes utiles pour la lecture et l'ecriture de script
        /// <summary>
        /// Lit le script depuis le document XML et le charge dans la string collection passé en paramètre
        /// </summary>
        /// <param name="Script">contiens le script à la sortie</param>
        /// <param name="Node">neud de l'objet (control) courant</param>
        /// <param name="strNameScriptSection">nom du noeud contenant le script</param>
        protected void ReadScript(ref StringCollection Script, XmlNode Node, string strNameScriptSection)
        {
            // on lit le script si il y en a un
            if (Node.FirstChild != null)
            {
                for (int ch = 0; ch < Node.ChildNodes.Count; ch++)
                {
                    if (Node.ChildNodes[ch].Name == strNameScriptSection)
                    {
                        for (int i = 0; i < Node.ChildNodes[ch].ChildNodes.Count; i++)
                        {
                            if (Node.ChildNodes[ch].ChildNodes[i].Name == XML_CF_TAG.Line.ToString()
                                && Node.ChildNodes[ch].ChildNodes[i].FirstChild != null)
                            {

                                Script.Add(Node.ChildNodes[ch].ChildNodes[i].FirstChild.Value);
                            }
                        }
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// écrit le script passé en paramètre dans le document
        /// </summary>
        /// <param name="Script">script à écrire</param>
        /// <param name="XmlDoc">document XML</param>
        /// <param name="NodeControl">noeud du controle</param>
        /// <param name="strNameScriptSection">nom du noeud ou sera stocké le script</param>
        protected void WriteScript(StringCollection Script, XmlDocument XmlDoc, XmlNode NodeControl, string strNameScriptSection)
        {
            XmlNode XmlEventScript = XmlDoc.CreateElement(strNameScriptSection);
            for (int i = 0; i < Script.Count; i++)
            {
                XmlNode NodeLine = XmlDoc.CreateElement(XML_CF_TAG.Line.ToString());
                XmlNode NodeText = XmlDoc.CreateTextNode(Script[i]);
                NodeLine.AppendChild(NodeText);
                XmlEventScript.AppendChild(NodeLine);
            }
            NodeControl.AppendChild(XmlEventScript);
        }
        #endregion

        #region methode utile pour la copie de script
        /// <summary>
        /// fonction utilitaire permettant de copier un script vers un autre
        /// </summary>
        /// <param name="DestScript">script de destination</param>
        /// <param name="SrcScript">script source</param>
        protected void CopyScript(ref StringCollection DestScript, StringCollection SrcScript)
        {
            DestScript.Clear();
            for (int i = 0; i < SrcScript.Count; i++)
            {
                DestScript.Add(SrcScript[i]);
            }
        }
        #endregion

        #region methodes pour le traitement des messages
        /// <summary>
        /// effectue les opération nécessaire lors de la récéption d'un message
        /// </summary>
        /// <param name="Mess">Type de message</param>
        /// <param name="obj">objet contenant les paramètres du messages</param>
        /// <param name="TypeApp">Type d'application courante</param>
        public virtual void TraiteMessage(MESSAGE Mess, object obj, TYPE_APP TypeApp, BTControl PropOwner)
        {

        }
        #endregion

        #region Méthode de tratement des messages pour les objets scriptables
        /// <summary>
        /// traite les message dans les scripts de l'objet
        /// </summary>
        /// <param name="Mess">Type de message</param>
        /// <param name="Script">script à traiter</param>
        /// <param name="obj">objet contenant les paramètres du messages</param>
        /// <param name="PropOwner">control possédant les propriété</param>
        protected void ScriptTraiteMessage(MESSAGE Mess, StringCollection Script, object obj, BTControl PropOwner)
        {
            switch (Mess)
            {
                case MESSAGE.MESS_ASK_ITEM_DELETE:
                    {
                        MessAskDelete MessParam = (MessAskDelete)obj;
                        for (int i = 0; i < Script.Count; i++)
                        {
                            string stritem = "";
                            stritem = ScriptParser.GetLineToken(Script[i], ScriptParser.INDEX_TOKEN_SYMBOL);

                            if (stritem == MessParam.WantDeletetItemSymbol
                                || (((MessAskDelete)obj).TypeOfItem == typeof(Data) && Script[i].Contains(MessParam.WantDeletetItemSymbol))
                                )
                            {
                                string strMess = string.Format("Control {0} Script: Line {1} will be removed", PropOwner.Symbol, i + 1);
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
                        for (int i = Script.Count - 1; i >= 0; i--)
                        {
                            string stritem = ScriptParser.GetLineToken(Script[i], ScriptParser.INDEX_TOKEN_SYMBOL);
                            if (stritem == MessParam.DeletetedItemSymbol)
                            {
                                Script.RemoveAt(i);
                            }
                        }
                    }
                    else if (((MessDeleted)obj).TypeOfItem == typeof(Data))
                    {
                        MessDeleted MessParam = (MessDeleted)obj;
                        for (int i = Script.Count - 1; i >= 0; i--)
                        {
                            if (Script[i].Contains(MessParam.DeletetedItemSymbol))
                            {
                                Script.RemoveAt(i);
                            }
                        }
                    }
                    break;
                case MESSAGE.MESS_ITEM_RENAMED:
                    {
                        if (((MessItemRenamed)obj).TypeOfItem == typeof(Trame)
                            || ((MessItemRenamed)obj).TypeOfItem == typeof(Function)
                            || ((MessItemRenamed)obj).TypeOfItem == typeof(Logger)
                            || ((MessItemRenamed)obj).TypeOfItem == typeof(BTTimer)
                            )
                        {
                            MessItemRenamed MessParam = (MessItemRenamed)obj;
                            for (int i = 0; i < Script.Count; i++)
                            {
                                string stritem = ScriptParser.GetLineToken(Script[i], ScriptParser.INDEX_TOKEN_SYMBOL);
                                if (stritem == MessParam.OldItemSymbol)
                                {
                                    Script[i] = Script[i].Replace(MessParam.OldItemSymbol, MessParam.NewItemSymbol);
                                }
                            }
                        }
                        else if (((MessItemRenamed)obj).TypeOfItem == typeof(Data))
                        {
                            MessItemRenamed MessParam = (MessItemRenamed)obj;
                            for (int i = 0; i < Script.Count; i++)
                            {
                                if (Script[i].Contains(MessParam.OldItemSymbol))
                                {
                                    Script[i] = Script[i].Replace(MessParam.OldItemSymbol, MessParam.NewItemSymbol);
                                }
                            }
                        }
                    }
                    break;
                default:
                    break;
            }
        }
        #endregion
        
        /// <summary>
        /// renvoie true si le control à au moins une donnée associé qui rend son état dynamique
        /// Utilisé pour masquer les controls lors de l'enregistrement de l'image de fond de plan
        /// </summary>
        public virtual bool HaveDataAssociation
        {
            get
            {
                return false;
            }
        } 
    }

    /// <summary>
    /// classe des propriété spécifiques des controles utilisant deux couleurs
    /// </summary>
    public class TwoColorProp : SpecificControlProp
    {
        #region données membres de la classe
        private Color m_ColorInactive = Color.Black;
        private Color m_ColorActive = Color.Blue;
        #endregion

        #region attributs
        /// <summary>
        /// accesseur vers la couleur à l'état inactif
        /// </summary>
        public Color ColorInactive
        {
            get
            {
                return m_ColorInactive;
            }
            set
            {
                m_ColorInactive = value;
            }
        }

        /// <summary>
        /// accesseur vers la couleur à l'état actif
        /// </summary>
        public Color ColorActive
        {
            get
            {
                return m_ColorActive;
            }
            set
            {
                m_ColorActive = value;
            }
        }
        #endregion

        #region Read In / Write Out
        /// <summary>
        /// fonction de lecture des propriété spécifiques
        /// </summary>
        /// <param name="Node">Noeud du control</param>
        /// <returns>true si la lecture c'est bien passé, sinon false</returns>
        public override bool ReadIn(XmlNode Node)
        {
            XmlNode AttrActiveColor = Node.Attributes.GetNamedItem(XML_CF_ATTRIB.ActiveColor.ToString());
            XmlNode AttrInactiveColor = Node.Attributes.GetNamedItem(XML_CF_ATTRIB.InactiveColor.ToString());
            if (AttrActiveColor == null
                || AttrInactiveColor == null)
                return false;

            string[] rgbVal = AttrActiveColor.Value.Split(',');
            int r = int.Parse(rgbVal[0]);
            int g = int.Parse(rgbVal[1]);
            int b = int.Parse(rgbVal[2]);
            this.ColorActive = Color.FromArgb(r, g, b);
            string[] rgbVal2 = AttrInactiveColor.Value.Split(',');
            int r2 = int.Parse(rgbVal2[0]);
            int g2 = int.Parse(rgbVal2[1]);
            int b2 = int.Parse(rgbVal2[2]);
            this.ColorInactive = Color.FromArgb(r2, g2, b2);
            return true;
        }

        /// <summary>
        /// fonction d'écriture des propriété spécifiques
        /// </summary>
        /// <param name="XmlDoc">Document Xml</param>
        /// <param name="Node">Noeud du control</param>
        /// <returns>true si l'écriture c'est bien passé</returns>
        public override bool WriteOut(XmlDocument XmlDoc, XmlNode Node)
        {
            XmlAttribute AttrActColor = XmlDoc.CreateAttribute(XML_CF_ATTRIB.ActiveColor.ToString());
            XmlAttribute AttrInactColor = XmlDoc.CreateAttribute(XML_CF_ATTRIB.InactiveColor.ToString());
            AttrActColor.Value = string.Format("{0}, {1}, {2}", ColorActive.R, ColorActive.G, ColorActive.B);
            AttrInactColor.Value = string.Format("{0}, {1}, {2}", ColorInactive.R, ColorInactive.G, ColorInactive.B);
            Node.Attributes.Append(AttrActColor);
            Node.Attributes.Append(AttrInactColor);
            return true;
        }
        #endregion

        #region copie des paramètres
        /// <summary>
        /// copie les paramètre des propriété spécifiques passé en paramètre
        /// </summary>
        /// <param name="SrcSpecificProp">propriété sources</param>
        public override void CopyParametersFrom(SpecificControlProp SrcSpecificProp)
        {
            ColorInactive = ((TwoColorProp)SrcSpecificProp).ColorInactive;
            ColorActive = ((TwoColorProp)SrcSpecificProp).ColorActive;
        }
        #endregion
    }
}

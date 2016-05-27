using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace CommonLib
{
    /// <summary>
    /// le gestionaire virtuel représente la copie de toutes les données utilisée par le fichier de configuration
    /// c'est dans ce gestionnaire qu'on va venir chercher les donnée pour construire automatiquement les trames de réponses
    /// attendues. Et c'est aussi les valeurs des objet Data contenu dans ce gestionaire qu'on va modifier afin de simuler
    /// un "vrai" (c'est un bien grand mot) retour de la trame par le contoleur distant.
    /// on sera aussi capable de prendre des clichés de valeurs!
    /// putain ca va être beau!!!!!
    /// </summary>
    public class GestDataVirtual : GestData
    {
        #region ReadIn  / WriteOut

        /// <summary>
        /// Lit les données de l'objet a partir de son noeud XML
        /// </summary>
        /// <param name="Node">Noeud Xml de l'objet</param>
        /// <param name="TypeApp">type d'application courante</param>
        /// <returns>true si la lecture s'est bien passé</returns>
        public override bool ReadIn(XmlNode Node, BTDoc document)
        {
            for (int i = 0; i < Node.ChildNodes.Count; i++)
            {
                XmlNode ChildNode = Node.ChildNodes[i];
                if (ChildNode.Name != XML_CF_TAG.Data.ToString())
                    continue;

                VirtualData NewData = new VirtualData();
                if (NewData != null)
                {
                    if (!NewData.ReadIn(ChildNode, document))
                        return false;
                    NewData.UpdateUserVisibility();
                    this.AddObj(NewData);
                }
            }
            if (!ReadGestGroup(Node, document.TypeApp))
                return false;

            return true;
        }

        /// <summary>
        /// lit un "cliché" de la valeur (fichier contenant juste la valeur des données)
        /// </summary>
        /// <param name="Node">Noeud de a donnée</param>
        /// <param name="TypeApp">Type de l'application</param>
        /// <returns>true en cas de succès</returns>
        public bool ReadInInstantImage(XmlNode Node, TYPE_APP TypeApp)
        {
            for (int i = 0; i < Node.ChildNodes.Count; i++)
            {
                XmlNode ChildNode = Node.ChildNodes[i];
                if (ChildNode.Name != XML_CF_TAG.Data.ToString())
                    continue;
                XmlNode AttrSymbol = ChildNode.Attributes.GetNamedItem(XML_CF_ATTRIB.strSymbol.ToString());
                VirtualData OldData = (VirtualData)this.GetFromSymbol(AttrSymbol.Value);
                if (OldData != null)
                {
                    // on fait appel à l'ancienne donnée qui va simplement rafraichir sa valeur depuis
                    // le fichier
                    if (!OldData.ReadInInstantImage(ChildNode, TypeApp))
                        return false;
                }
                else
                {
                    // la donnée à été supprimé du fichier de config, on ne fait rien
                }
            }

            return true;
        }

        /// <summary>
        /// NE JAMAIS UTILISER
        /// </summary>
        /// <param name="XmlDoc"></param>
        /// <param name="Node"></param>
        /// <returns></returns>
        public override bool WriteOut(XmlDocument XmlDoc, XmlNode Node, BTDoc document)
        {
            // le gestionaire virtuel n'a pas le droit de se sauvegarder normalement
            return false;
        }

        /// <summary>
        /// écrit un "cliché" de la valeur (fichier contenant juste la valeur des données)
        /// </summary>
        /// <param name="XmlDoc">document XML</param>
        /// <param name="Node">noeud du gestionnaire</param>
        /// <returns></returns>
        public bool WriteOutInstantImage(XmlDocument XmlDoc, XmlNode Node)
        {
            for (int i = 0; i < this.m_ListObject.Count; i++)
            {
                VirtualData dt = (VirtualData)m_ListObject[i];
                //XML_CF_TAG.DataTemp => on change le type de tag pour bien faire la différence
                //entre le fichier de sauvegarde normal et le fichier de mémorisation temporaire
                if (!dt.IsConstant && dt.SaveInCliche)
                {
                    XmlNode XmlData = XmlDoc.CreateElement(XML_CF_TAG.Data.ToString());
                    dt.WriteOutInstantImage(XmlDoc, XmlData);
                    Node.AppendChild(XmlData);
                }
            }
            return true;
        }
    
        public void SetAllSaveInCliche(bool bSaveInCliche)
        {
            for (int i = 0; i < this.m_ListObject.Count; i++)
            {
                VirtualData dt = (VirtualData)m_ListObject[i];
                if (!dt.IsConstant)
                {
                    dt.SaveInCliche = bSaveInCliche;
                }
            }
        }

        public void SetAllSaveInCliche(string GroupName, bool bSaveInCliche)
        {
            for (int i = 0; i < this.GroupCount; i++)
            {
                BaseGestGroup.Group gr = Groups[i];
                if (gr.GroupSymbol == GroupName)
                {
                    for (int j = 0; j< gr.Items.Count; j++)
                    {
                        VirtualData dt = (VirtualData)gr.Items[j];
                        if (!dt.IsConstant)
                        {
                            dt.SaveInCliche = bSaveInCliche;
                        }
                    }
                    break;
                }
            }
        }

        #endregion
    }
}

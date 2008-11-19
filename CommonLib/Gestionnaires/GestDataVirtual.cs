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
    /// un vrai retour de la trame par le contoleur distant.
    /// on sera aussi capable de prendre des clichés de valeurs!
    /// putain ca va être beau!!!!!
    /// </summary>
    public class GestDataVirtual : GestData
    {
        #region ReadIn  / WriteOut

        public override bool ReadIn(XmlNode Node, TYPE_APP TypeApp)
        {
            for (int i = 0; i < Node.ChildNodes.Count; i++)
            {
                XmlNode ChildNode = Node.ChildNodes[i];
                if (ChildNode.Name != XML_CF_TAG.Data.ToString())
                    continue;

                VirtualData NewData = new VirtualData();
                if (NewData != null)
                {
                    if (!NewData.ReadIn(ChildNode, TypeApp))
                        return false;
                    NewData.UpdateUserVisibility();
                    this.AddObj(NewData);
                }
            }
            if (!ReadGestGroup(Node, TypeApp))
                return false;

            return true;
        }
        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
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
                    // il me faudra des objets virtual data quand même
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
        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public override bool WriteOut(XmlDocument XmlDoc, XmlNode Node)
        {
            // le gestionaire virtuel n'a pas le droit de se sauvegarder normalement
            return false;
        }

        public bool WriteOutInstantImage(XmlDocument XmlDoc, XmlNode Node)
        {
            for (int i = 0; i < this.m_ListObject.Count; i++)
            {
                VirtualData dt = (VirtualData)m_ListObject[i];
                //XML_CF_TAG.DataTemp => on change le type de tag pour bien faire la différence
                //entre le fichier de sauvegarde normal et le fichier de mémorisation temporaire
                XmlNode XmlData = XmlDoc.CreateElement(XML_CF_TAG.Data.ToString());
                dt.WriteOutInstantImage(XmlDoc, XmlData);
                Node.AppendChild(XmlData);
            }
            return true;
        }

        #endregion
    }
}

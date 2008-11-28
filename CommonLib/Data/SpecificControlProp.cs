using System;
using System.Collections.Generic;
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
    }

    /// <summary>
    /// classe des propriété spécifiques des controles utilisant deux couleurs
    /// </summary>
    public class TwoColorProp : SpecificControlProp
    {
        private Color m_ColorInactive = Color.Black;
        private Color m_ColorActive = Color.Blue;

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

        public override void CopyParametersFrom(SpecificControlProp SrcSpecificProp)
        {
            ColorInactive = ((TwoColorProp)SrcSpecificProp).ColorInactive;
            ColorActive = ((TwoColorProp)SrcSpecificProp).ColorActive;
        }

    }
}

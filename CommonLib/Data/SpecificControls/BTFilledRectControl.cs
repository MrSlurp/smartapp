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
using System.Drawing;

namespace CommonLib
{
    public class BTFilledRectControl : BTControl
    {
        #region données membres
        protected SpecificControlProp m_SpecificProp = null;
        #endregion

        #region constructeurs
        /// <summary>
        /// constructeur par défaut
        /// </summary>
        public BTFilledRectControl(BTDoc document) : base(document)
        {
            m_IControl = new TwoColorFilledRect();
            if (m_IControl != null)
                m_IControl.SourceBTControl = this;

            m_SpecificProp = new TwoColorProp(this.ItemScripts);
        }

        /// <summary>
        /// constructeur initialisant l'objet interactif associé
        /// </summary>
        /// <param name="Ctrl">objet interactif</param>
        public BTFilledRectControl(BTDoc document, InteractiveControl Ctrl)
            : base(document, Ctrl)
        {
            m_SpecificProp = new TwoColorProp(this.ItemScripts);
        }
        #endregion

        #region attributs
        /// <summary>
        /// obtiens les propriété spéficiques du controle
        /// </summary>
        public override SpecificControlProp SpecificProp
        {
            get
            {
                return m_SpecificProp;
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
        public override bool ReadIn(XmlNode Node, BTDoc document)
        {
            if (!ReadInBaseObject(Node))
                return false;
            if (!ReadInCommonBTControl(Node))
                return false;
 
            m_SpecificProp.ReadIn(Node, document);
            // on lit le script si il y en a un
            ReadScript(Node);

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
            XmlNode NodeControl = XmlDoc.CreateElement(XML_CF_TAG.SpecificControl.ToString());
            Node.AppendChild(NodeControl);
            WriteOutBaseObject(XmlDoc, NodeControl);
            XmlAttribute AttrSpec = XmlDoc.CreateAttribute(XML_CF_ATTRIB.SpecificType.ToString());
            AttrSpec.Value = SPECIFIC_TYPE.FILLED_RECT.ToString();
            NodeControl.Attributes.Append(AttrSpec);
            // on écrit les différents attributs du control
            if (!WriteOutCommonBTControl(XmlDoc, NodeControl))
                return false;

            if (!m_SpecificProp.WriteOut(XmlDoc, NodeControl, document))
                return false;

            /// on écrit le script
            WriteScript(XmlDoc, NodeControl);
            return true;
        }
        #endregion
    }
}

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
using System.IO;
using System.Windows.Forms;
using CommonLib;


namespace CtrlCnxManager
{
    internal class DllCtrlCnxManagerProp : SpecificControlProp
    {
        private const string Tag_Section = "CnxManager";
        private const string Attr_Delay = "Delay";
        // ajouter ici les données membres des propriété
        int m_iRetryCnxPeriod = 1;

        public DllCtrlCnxManagerProp(ItemScriptsConainter scriptContainter) : base(scriptContainter) { }

        // ajouter ici les accesseur vers les données membres des propriété
        public int RetryCnxPeriod
        {
            get { return m_iRetryCnxPeriod; }
            set { m_iRetryCnxPeriod = value; }
        }

        /// <summary>
        /// Lecture des paramètres depuis le fichier XML
        /// </summary>
        /// <param name="Node">noeud du control a qui appartiens les propriété </param>
        /// <returns>true en cas de succès de la lecture</returns>
        public override bool ReadIn(XmlNode Node, BTDoc document)
        {
            for (int iChild = 0; iChild < Node.ChildNodes.Count; iChild++)
            {
                if (Node.ChildNodes[iChild].Name == Tag_Section)
                {
                    XmlNode SectionNode = Node.ChildNodes[iChild];
                    XmlNode attrDelay = SectionNode.Attributes.GetNamedItem(Attr_Delay);
                    this.m_iRetryCnxPeriod = int.Parse(attrDelay.Value);
                }
            }
            return true;
        }

        /// <summary>
        /// écriture des paramètres dans le fichier XML
        /// </summary>
        /// <param name="XmlDoc">Document XML</param>
        /// <param name="Node">noeud du control a qui appartiens les propriété</param>
        /// <returns>true en cas de succès de l'écriture</returns>
        public override bool WriteOut(XmlDocument XmlDoc, XmlNode Node, BTDoc document)
        {
            XmlNode ParamNode = XmlDoc.CreateElement(Tag_Section);
            Node.AppendChild(ParamNode);
            XmlAttribute attrDelay = XmlDoc.CreateAttribute(Attr_Delay);
            attrDelay.Value = this.m_iRetryCnxPeriod.ToString();
            ParamNode.Attributes.Append(attrDelay);
            return true;
        }

        /// <summary>
        /// Recopie les paramètres d'un control source du même type vers les paramètres courants
        /// </summary>
        /// <param name="SrcSpecificProp">Paramètres sources</param>
        public override void CopyParametersFrom(SpecificControlProp SrcSpecificProp, bool bFromOtherInstance, BTDoc document)
        {
            if (SrcSpecificProp is DllCtrlCnxManagerProp)
            {
                DllCtrlCnxManagerProp SrcProp = SrcSpecificProp as DllCtrlCnxManagerProp;
                this.m_iRetryCnxPeriod = SrcProp.m_iRetryCnxPeriod;
            }
        }

        /// <summary>
        /// Traite les message intra applicatif de SmartConfig
        /// Ces messages informes de : 
        /// - demande de suppression (non confirmée) : il faut créer un message pour informer l'utlisateur
        /// - Supression de confirmée : il faut supprimer le paramètre concerné
        /// - renommage : il faut mettre a jour le paramètre concerné
        /// </summary>
        /// <param name="Mess">Type du message</param>
        /// <param name="obj">paramètre du message (objet paramètre du message de type 
        /// MessAskDelete / MessDeleted / MessItemRenamed)</param>
        /// <param name="TypeApp">Type de l'application courante (SmartConfig / SmartCommand)</param>
        /// <param name="PropOwner">control propriétaire des propriété spécifique</param>
        /*
        public override void TraiteMessage(MESSAGE Mess, object obj, TYPE_APP TypeApp, BTControl PropOwner)
        {

        }
        */
    }
}

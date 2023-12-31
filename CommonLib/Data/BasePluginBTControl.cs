﻿/*
    This file is part of SmartApp.
    Copyright (C) 2007-2016  Pascal Bigot

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

namespace CommonLib
{
    public abstract class BasePluginBTControl : BTControl
    {
        protected SpecificControlProp m_SpecificProp = null;


        public BasePluginBTControl(BTDoc document)
            : base(document)
        {

        }

        /// <summary>
        /// Constructeur de la à partir d'un control interactif
        /// </summary>
        public BasePluginBTControl(BTDoc document, InteractiveControl Ctrl)
            : base(document, Ctrl)
        {
        }

        /// <summary>
        /// Acesseur des propriété spécifiques
        /// </summary>
        public override SpecificControlProp SpecificProp
        {
            get
            {
                return m_SpecificProp;
            }
        }

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
            // crée le noeud du control courant
            XmlNode NodeControl = XmlDoc.CreateElement(XML_CF_TAG.DllControl.ToString());
            // ajoute ce noeud à la liste des controles de l'écran
            Node.AppendChild(NodeControl);
            // écrit les donnée de l'objet de base (symbol et description)
            WriteOutBaseObject(XmlDoc, NodeControl);
            // crée l'attribut DllID et l'ajoute au noeud du control
            XmlAttribute AttrDllId = XmlDoc.CreateAttribute(XML_CF_ATTRIB.DllID.ToString());
            AttrDllId.Value = DllControlID.ToString();
            NodeControl.Attributes.Append(AttrDllId);

            // on écrit les différents attributs du control (position, taille, donnée associée de base, propriété "UseScreenEvent")
            if (!WriteOutCommonBTControl(XmlDoc, NodeControl))
                return false;
            // écrit les paramètres des propriété spécifique
            if (!m_SpecificProp.WriteOut(XmlDoc, NodeControl, document))
                return false;

            /// on écrit le script (UseScreenEvent)
            WriteScript(XmlDoc, NodeControl);
            return true;
        }

        /// <summary>
        /// Surcharge de la classe de base, indique que c'est un plugin DLL
        /// </summary>
        public override bool IsDllControl
        {
            get
            {
                return true;
            }
        }

        /// <summary>
        /// Traite les message intra applicatif de SmartConfig
        /// Ces messages informes de : 
        /// - demande de suppression (non confirmée) : il faut créer un message pour informer l'utlisateur
        /// - Supression de confirmée : il faut supprimer le paramètre concerné
        /// - renommage : il faut mettre a jour le paramètre concerné
        /// NOTE : n'a lieu d'être que si dans le paramétrage on trouve des symboles d'objets
        /// </summary>
        /// <param name="Mess">Type du message</param>
        /// <param name="obj">paramètre du message (objet paramètre du message de type 
        /// MessAskDelete / MessDeleted / MessItemRenamed)</param>
        /// <param name="TypeApp">Type de l'application courante (SmartConfig / SmartCommand)</param>
        public override void TraiteMessage(MESSAGE Mess, object obj, TYPE_APP TypeApp)
        {
            base.TraiteMessage(Mess, obj, TypeApp);
            m_SpecificProp.TraiteMessage(Mess, obj, TypeApp, this);
        }
        #endregion
    }
}

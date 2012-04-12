using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using CommonLib;

namespace PasswordControler
{
    internal class BTDllPasswordControlerControl : BTControl
    {
        protected SpecificControlProp m_SpecificProp = null;

        /// <summary>
        /// Constructeur de la classe
        /// </summary>
        public BTDllPasswordControlerControl(BTDoc document)
            : base(document)
        {
            m_IControl = new InteractivePasswordControlerDllControl();
            if (m_IControl != null)
                m_IControl.SourceBTControl = this;

            m_SpecificProp = new DllPasswordControlerProp(this.ItemScripts);
        }

        /// <summary>
        /// Constructeur de la à partir d'un control interactif
        /// </summary>
        public BTDllPasswordControlerControl(BTDoc document, InteractiveControl Ctrl)
            : base(document, Ctrl)
        {
            m_SpecificProp = new DllPasswordControlerProp(this.ItemScripts);
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
        #endregion

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
        /// Renvoie l'ID de la DLL
        /// </summary>
        public override uint DllControlID
        {
            get
            {
                return PasswordControler.DllEntryClass.DLL_Control_ID;
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

    }
}

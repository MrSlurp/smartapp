﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;
using System.Windows.Forms;
using CommonLib;


namespace ImageButton
{
    internal class DllImageButtonProp : SpecificControlProp
    {
        // ajouter ici les données membres des propriété
        private string m_NomFichierInactif;
        private string m_NomFichierActif;
        private bool m_bIsBistable;
        private const string NOM_ATTIB_REL = "RelImage";
        private const string NOM_ATTIB_PRE = "PreImage";
        private const string NOM_ATTRIB_BISTABLE = "Bistable";
        private const string NOM_NOEUD_PROP = "ImageButtonProp";

        // ajouter ici les accesseur vers les données membres des propriété
        public string ReleasedImage
        {
            get
            {
                return m_NomFichierInactif;
            }
            set
            {
                m_NomFichierInactif = value;
            }
        }

        public string PressedImage
        {
            get
            {
                return m_NomFichierActif;
            }
            set
            {
                m_NomFichierActif = value;
            }
        }

        public bool IsBistable
        {
            get { return m_bIsBistable; }
            set { m_bIsBistable = value; }
        }

        /// <summary>
        /// Lecture des paramètres depuis le fichier XML
        /// </summary>
        /// <param name="Node">noeud du control a qui appartiens les propriété </param>
        /// <returns>true en cas de succès de la lecture</returns>
        public override bool ReadIn(XmlNode Node)
        {
            if (Node.FirstChild != null)
            {
                for (int i = 0; i < Node.ChildNodes.Count; i++)
                {
                    if (Node.ChildNodes[i].Name == NOM_NOEUD_PROP)
                    {
                        XmlNode AttrRel = Node.ChildNodes[i].Attributes.GetNamedItem(NOM_ATTIB_REL);
                        XmlNode AttrPre = Node.ChildNodes[i].Attributes.GetNamedItem(NOM_ATTIB_PRE);
                        XmlNode AttrBistable = Node.ChildNodes[i].Attributes.GetNamedItem(NOM_ATTRIB_BISTABLE);

                        if (AttrRel == null
                            || AttrPre == null)
                            return false;
                        ReleasedImage = AttrRel.Value;
                        PressedImage = AttrPre.Value;
                        if (AttrBistable != null)
                            m_bIsBistable = bool.Parse(AttrBistable.Value);
                    }
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
        public override bool WriteOut(XmlDocument XmlDoc, XmlNode Node)
        {
            XmlNode ItemProp = XmlDoc.CreateElement(NOM_NOEUD_PROP);
            XmlAttribute AttrRel = XmlDoc.CreateAttribute(NOM_ATTIB_REL);
            XmlAttribute AttrPre = XmlDoc.CreateAttribute(NOM_ATTIB_PRE);
            XmlAttribute Attrbistable = XmlDoc.CreateAttribute(NOM_ATTRIB_BISTABLE);
            AttrRel.Value = PathTranslator.AbsolutePathToRelative(m_NomFichierActif);
            AttrPre.Value = PathTranslator.AbsolutePathToRelative(m_NomFichierInactif);
            Attrbistable.Value = m_bIsBistable.ToString();
            ItemProp.Attributes.Append(AttrRel);
            ItemProp.Attributes.Append(AttrPre);
            ItemProp.Attributes.Append(Attrbistable);
            Node.AppendChild(ItemProp);
            return true;
        }

        /// <summary>
        /// Recopie les paramètres d'un control source du même type vers les paramètres courants
        /// </summary>
        /// <param name="SrcSpecificProp">Paramètres sources</param>
        public override void CopyParametersFrom(SpecificControlProp SrcSpecificProp, bool bFromOtherInstance)
        {
            DllImageButtonProp SrcProp = (DllImageButtonProp)SrcSpecificProp;
            if (bFromOtherInstance)
            {
                if (File.Exists(PathTranslator.LinuxVsWindowsPathUse(
                                PathTranslator.RelativePathToAbsolute(
                                SrcProp.ReleasedImage))))
                {
                    ReleasedImage = SrcProp.ReleasedImage;
                }
                if (File.Exists(PathTranslator.LinuxVsWindowsPathUse(
                                PathTranslator.RelativePathToAbsolute(
                                SrcProp.PressedImage))))
                {
                    PressedImage = SrcProp.PressedImage;
                }
            }
            else
            {
                ReleasedImage = SrcProp.ReleasedImage;
                PressedImage = SrcProp.PressedImage;
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
        public override void TraiteMessage(MESSAGE Mess, object obj, TYPE_APP TypeApp, BTControl PropOwner)
        {
            if (TypeApp == TYPE_APP.SMART_CONFIG)
            {
                switch (Mess)
                {
                    case MESSAGE.MESS_ASK_ITEM_DELETE:
                        // exemple de traitement de la demande de supression d'une donnée
                        // m_strDataOffToOn est le symbol d'une donnée
                        /*
                        if (((MessAskDelete)obj).TypeOfItem == typeof(Data))
                        {
                            if (MessParam.WantDeletetItemSymbol == m_strDataOffToOn)
                            {
                                strMess = string.Format("Data Trigger {0} : Data \"Off to On\" will be removed", PropOwner.Symbol);
                                MessParam.ListStrReturns.Add(strMess);
                            }
                        }
                         * */
                        break;
                    case MESSAGE.MESS_ITEM_DELETED:
                        // exemple de traitement de la supression d'une donnée
                        // m_strDataOffToOn est le symbol d'une donnée
                        /*
                        if (((MessDeleted)obj).TypeOfItem == typeof(Data))
                        {
                            MessDeleted MessParam = (MessDeleted)obj;
                            if (MessParam.DeletetedItemSymbol == m_strDataOffToOn)
                            {
                                m_strDataOffToOn = string.Empty;
                            }
                        }
                         * */
                        break;
                    case MESSAGE.MESS_ITEM_RENAMED:
                        // exemple de traitement du renommage d'une donnée
                        // m_strDataOffToOn est le symbol d'une donnée
                        /*
                        if (((MessItemRenamed)obj).TypeOfItem == typeof(Data))
                        {
                            MessItemRenamed MessParam = (MessItemRenamed)obj;
                            if (MessParam.OldItemSymbol == m_strDataOffToOn)
                            {
                                m_strDataOffToOn = MessParam.NewItemSymbol;
                            }
                        }*/
                        break;
                    default:
                        break;
                }
            }
        }

    }
}

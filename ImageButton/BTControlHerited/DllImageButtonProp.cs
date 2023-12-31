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
        private FlatStyle m_Style = FlatStyle.Standard;
        private int m_BorderSize = 1;
        private string m_strInputData = string.Empty;

        private const string NOM_ATTRIB_REL = "RelImage";
        private const string NOM_ATTRIB_PRE = "PreImage";
        private const string NOM_ATTRIB_BISTABLE = "Bistable";
        private const string NOM_NOEUD_PROP = "ImageButtonProp";
        private const string NOM_ATTRIB_STYLE = "Style";
        private const string NOM_ATTRIB_BORDER_SIZE = "BorderSize";
        private const string NOM_ATTRIB_INTPUT_DATA = "InputData";


        public DllImageButtonProp(ItemScriptsConainter scriptContainter) : base(scriptContainter) { }

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

        public FlatStyle Style
        {
            get { return m_Style; }
            set { m_Style = value; }
        }

        public int BorderSize
        {
            get { return m_BorderSize; }
            set { m_BorderSize = value; }
        }

        public bool ImgFromInput
        {
            get { return !string.IsNullOrEmpty(m_strInputData); }
        }

        public string InputData
        {
            get { return m_strInputData; }
            set { m_strInputData = value; }
        }

        /// <summary>
        /// Lecture des paramètres depuis le fichier XML
        /// </summary>
        /// <param name="Node">noeud du control a qui appartiens les propriété </param>
        /// <returns>true en cas de succès de la lecture</returns>
        public override bool ReadIn(XmlNode Node, BTDoc document)
        {
            if (Node.FirstChild != null)
            {
                for (int i = 0; i < Node.ChildNodes.Count; i++)
                {
                    if (Node.ChildNodes[i].Name == NOM_NOEUD_PROP)
                    {
                        XmlNode AttrRel = Node.ChildNodes[i].Attributes.GetNamedItem(NOM_ATTRIB_REL);
                        XmlNode AttrPre = Node.ChildNodes[i].Attributes.GetNamedItem(NOM_ATTRIB_PRE);
                        XmlNode AttrBistable = Node.ChildNodes[i].Attributes.GetNamedItem(NOM_ATTRIB_BISTABLE);
                        XmlNode AttrStyle = Node.ChildNodes[i].Attributes.GetNamedItem(NOM_ATTRIB_STYLE);
                        XmlNode AttrBorder= Node.ChildNodes[i].Attributes.GetNamedItem(NOM_ATTRIB_BORDER_SIZE);
                        XmlNode AttrInputData = Node.ChildNodes[i].Attributes.GetNamedItem(NOM_ATTRIB_INTPUT_DATA);

                        if (AttrRel == null
                            || AttrPre == null)
                            return false;
                        ReleasedImage = AttrRel.Value;
                        PressedImage = AttrPre.Value;
                        if (AttrBistable != null)
                            m_bIsBistable = bool.Parse(AttrBistable.Value);

                        if (AttrStyle != null)
                            m_Style = (FlatStyle)Enum.Parse(typeof(FlatStyle), AttrStyle.Value);

                        if (AttrBorder != null)
                            m_BorderSize = int.Parse(AttrBorder.Value);

                        if (AttrInputData != null)
                            m_strInputData = AttrInputData.Value;
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
        public override bool WriteOut(XmlDocument XmlDoc, XmlNode Node, BTDoc document)
        {
            XmlNode ItemProp = XmlDoc.CreateElement(NOM_NOEUD_PROP);
            XmlAttribute AttrRel = XmlDoc.CreateAttribute(NOM_ATTRIB_REL);
            XmlAttribute AttrPre = XmlDoc.CreateAttribute(NOM_ATTRIB_PRE);
            XmlAttribute Attrbistable = XmlDoc.CreateAttribute(NOM_ATTRIB_BISTABLE);
            XmlAttribute AttrStyle = XmlDoc.CreateAttribute(NOM_ATTRIB_STYLE);
            XmlAttribute AttrBorderSize = XmlDoc.CreateAttribute(NOM_ATTRIB_BORDER_SIZE);
            XmlAttribute AttrInputData = XmlDoc.CreateAttribute(NOM_ATTRIB_INTPUT_DATA);
            AttrRel.Value = document.PathTr.AbsolutePathToRelative(ReleasedImage);
            AttrPre.Value = document.PathTr.AbsolutePathToRelative(PressedImage);
            Attrbistable.Value = m_bIsBistable.ToString();
            AttrStyle.Value = m_Style.ToString();
            AttrBorderSize.Value = m_BorderSize.ToString();
            AttrInputData.Value = m_strInputData;
            ItemProp.Attributes.Append(AttrRel);
            ItemProp.Attributes.Append(AttrPre);
            ItemProp.Attributes.Append(Attrbistable);
            ItemProp.Attributes.Append(AttrStyle);
            ItemProp.Attributes.Append(AttrBorderSize);
            ItemProp.Attributes.Append(AttrInputData);
            Node.AppendChild(ItemProp);
            return true;
        }

        /// <summary>
        /// Recopie les paramètres d'un control source du même type vers les paramètres courants
        /// </summary>
        /// <param name="SrcSpecificProp">Paramètres sources</param>
        public override void CopyParametersFrom(SpecificControlProp SrcSpecificProp, bool bFromOtherInstance, BTDoc document)
        {
            if (SrcSpecificProp is DllImageButtonProp)
            {
                DllImageButtonProp SrcProp = SrcSpecificProp as DllImageButtonProp;
                if (File.Exists(PathTranslator.LinuxVsWindowsPathUse(
                                document.PathTr.RelativePathToAbsolute(
                                SrcProp.ReleasedImage))))
                {
                    ReleasedImage = SrcProp.ReleasedImage;
                }
                if (File.Exists(PathTranslator.LinuxVsWindowsPathUse(
                                document.PathTr.RelativePathToAbsolute(
                                SrcProp.PressedImage))))
                {
                    PressedImage = SrcProp.PressedImage;
                }
                m_bIsBistable = SrcProp.m_bIsBistable;
                m_Style = SrcProp.m_Style;
                m_BorderSize = SrcProp.m_BorderSize;
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
                BTControl.TraiteMessageDataDelete(Mess, obj, m_strInputData, PropOwner, DllEntryClass.LangSys.C("Image Button {0} : Input Data will be removed"));
                m_strInputData = BTControl.TraiteMessageDataDeleted(Mess, obj, m_strInputData);
                m_strInputData = BTControl.TraiteMessageDataRenamed(Mess, obj, m_strInputData);
            }
        }

    }
}

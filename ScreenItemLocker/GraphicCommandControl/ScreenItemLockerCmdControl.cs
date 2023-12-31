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
using System.Collections.Specialized;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using CommonLib;

namespace ScreenItemLocker
{
    /// <summary>
    /// Cette classe serta  définir le comportement du control lorsqu'il est executé dans SmartCommand
    /// </summary>
    internal class ScreenItemLockerCmdControl : BTDllScreenItemLockerControl
    {
        /// <summary>
        /// Constructeur de la classe
        /// </summary>
        public ScreenItemLockerCmdControl(BTDoc document)
            : base(document)
        {

        }

        /// <summary>
        /// Constructeur de l'objet graphique affiché dans les écrans de supervision
        /// </summary>
        public override void CreateControl()
        {
            // on vérifie qu'il n'y a pas déja un control graphique (cette méthode ne doit être appelé qu'une seul fois)
            if (m_Ctrl == null)
            {
                // on crée l'objet graphique qui sera affiché
                m_Ctrl = new ScreenItemLockerDispCtrl();
                // on définit sa position dans l'écran
                m_Ctrl.Location = m_RectControl.Location;
                // son nom est le symbol de l'objet courant
                m_Ctrl.Name = m_strSymbol;
                // on définit sa taille
                m_Ctrl.Size = m_RectControl.Size;
                // on définit son fond comme étant transparent (peut être changé)
                m_Ctrl.BackColor = Color.Transparent;
                // faites ici les initialisation spécifiques du control affiché
                ScreenItemLockerDispCtrl dispCtrl = m_Ctrl as ScreenItemLockerDispCtrl;
                
                DllScreenItemLockerProp props = this.SpecificProp as DllScreenItemLockerProp;
                dispCtrl.InitControledItemList(props.ListItemSymbol);
                // par exemple la liaison du click souris à un handler d'event
                //m_Ctrl.Click += new System.EventHandler(this.OnControlEvent);
            }
        }

        /// <summary>
        /// Fonction existant par défaut pour gérer les évènement déclenché par le control affiché
        /// il est tout a fait possible d'en crée d'autre 
        /// </summary>
        /// <param name="Sender">objet ayant envoyé l'event</param>
        /// <param name="EventArgs">arguments l'event</param>
        public override void OnControlEvent(object Sender, EventArgs Args)
        {
            // traitez ici les évènement déclenché par le control (click souris par exemple)
            return;
        }

        /// <summary>
        /// Handler de l'évènement "DataValueChanged" déclenché par la donnée associée par défaut
        /// permet de mettre a jour l'état du control associé en fonction de celle ci.
        /// Pour chaque donnée qui serait utilisé dans les propriété, il est possible de réutiliser cet handler
        /// ou d'en crée d'autres (voir FinalizeRead pour la liaison des évènements)
        /// </summary>
        public override void UpdateFromData()
        {
            if (m_AssociateData != null && m_Ctrl != null)
            {
                bool bEnableState = false;
                if (m_AssociateData.Value == 0)
                    bEnableState = false;
                else
                    bEnableState = true;

                ScreenItemLockerDispCtrl dispCtrl = m_Ctrl as ScreenItemLockerDispCtrl;
                if (dispCtrl != null)
                {
                    dispCtrl.ChangeEnableStatus(bEnableState);
                }
            }
        }

        /// <summary>
        /// Traite les message intra applicatif de SmartConfig
        /// Ces messages informes de : 
        /// - passage en RUN de la supervision
        /// - passage en STOP de la supervision
        /// </summary>
        /// <param name="Mess">Type du message</param>
        /// <param name="obj">paramètre du message (aucun paramètre dans SmartCommand)</param>
        /// <param name="TypeApp">Type de l'application courante (SmartConfig / SmartCommand)</param>
        public override void TraiteMessage(MESSAGE Mess, object obj, TYPE_APP TypeApp)
        {
            base.TraiteMessage(Mess, obj, TypeApp);
            if (TypeApp == TYPE_APP.SMART_COMMAND)
            {
                switch (Mess)
                {
                    // message de requête sur les conséquence d'une supression
                    case MESSAGE.MESS_CMD_STOP:
                        // traitez ici le passage en mode stop du control si nécessaire
                        break;
                    case MESSAGE.MESS_CMD_RUN:
                        UpdateFromData();
                        // traitez ici le passage en mode run du control si nécessaire
                        break;
                    default:
                        break;
                }
            }
        }
    }

    /// <summary>
    /// classe héritant de UserControl
    /// représente l'objet graphique affiché dans la supervision
    /// on peux en faire a peut près ce qu'on veux :
    /// - du dessin
    /// - une aggregation de plusieurs controls standards, 
    /// - les deux, etc.
    /// </summary>
    public class ScreenItemLockerDispCtrl : UserControl
    {
        // ajouter ici les données membres du control affiché
        StringCollection m_listControledItems = new StringCollection();

        public ScreenItemLockerDispCtrl()
        {
        }

        public void InitControledItemList(StringCollection listItemSymbol)
        {
            foreach (string st in listItemSymbol)
                m_listControledItems.Add(st);

        }

        public void ChangeEnableStatus(bool bEnable)
        {
            for (int i = 0; i < m_listControledItems.Count; i++)
            {
                // attention ceci ne marche que parce que tout les controls sont au même niveau
                Control ctrl = Parent.Controls[m_listControledItems[i]];
                if (ctrl != null)
                    ctrl.Enabled = bEnable;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            // mettez ici le code de dessin du control
            base.OnPaint(e);
        }
    }
}

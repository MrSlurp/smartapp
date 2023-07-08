/*
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
using System.Windows.Forms;

namespace CommonLib
{
    class UserControlInfo : BaseInfo
    {
        private UserControl Me;

        public readonly string FormName = "";

        /// <summary>
        /// Constructeur static
        /// </summary>
        /// <param name="frm">Feuille concernée</param>
        /// <returns>Un objet FormInfo initialisé</returns>
        public static UserControlInfo CreateUserControlInfo(Lang LangSys, UserControl frm)
        {
            return new UserControlInfo(LangSys, frm);
        }

        /// <summary>
        /// Constructeur PRIVé de FormInfo
        /// </summary>
        /// <param name="frm">Objet System.Windows.Form</param>
        private UserControlInfo(Lang LangSys, UserControl frm) : base(LangSys)
        {
            // stocke le nom de la feuille
            FormName = frm.Name;
            Me = frm;
            // attache l'intercepteur d'évènements
            frm.Load += new EventHandler(this.UserControl_Load);
            // Vérification des textes demandés par Lang.C(****)
            if (m_LangSys.CreateOnMissingItem)
            {
                m_LangSys.GetCode_LangC(frm);
            }                                                                           
        }

        /// <summary>
        /// Intercepteur des evènements Load de la feuille concernée
        /// </summary>
        /// <param name="sender">Object System.Windows.Form</param>
        public void UserControl_Load(object sender, EventArgs e)
        {
            if (sender != null)
            {
                Controls.Clear();
                ControlsText.Clear();
                UserControl frm = (UserControl)sender;
                Update_Controls(frm.Controls);
                if (frm.ContextMenu != null)
                    Update_Controls(frm.ContextMenu.MenuItems);
            }
            else
            {
                m_LangSys.LoadLangage(FilePath);

                Update_Controls(Me.Controls);
                foreach (object var in Controls)
                {
                    if (var is ILangReloadable)
                    {
                        ((ILangReloadable)var).LoadNonStandardLang();
                        Traces.LogAddDebug(TraceCat.Lang, "appel à LoadNonStandardLang");
                    }
                }
            }
        }
    }
}

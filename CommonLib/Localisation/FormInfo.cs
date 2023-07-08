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
    /// <summary>
    /// Contient les données d'une IHM (System.Windows.Form)
    /// </summary>
    internal class FormInfo : BaseInfo
    {
        private Form Me;

        public readonly string FormName = "";

        private string m_FormSrcTitle = string.Empty;

        /// <summary>
        /// Constructeur static
        /// </summary>
        /// <param name="frm">Feuille concernée</param>
        /// <returns>Un objet FormInfo initialisé</returns>
        public static FormInfo CreateFormInfo(Lang LangSys, Form frm)
        {
            return new FormInfo(LangSys, frm);
        }

        /// <summary>
        /// Constructeur PRIVé de FormInfo
        /// </summary>
        /// <param name="frm">Objet System.Windows.Form</param>
        private FormInfo(Lang LangSys, Form frm) : base(LangSys)
        {
            // stocke le nom de la feuille
            FormName = frm.Name;
            Me = frm;
            // attache l'intercepteur d'évènements
            // stocke de nom du fichier de langue concerné
            // il servira de clé d'indexation
            //FilePath = m_LangSys.CreateFilePath(frm);
            frm.Load += Form_Load;
            // charge le fichier de langue adapté
            //m_LangSys.LoadLangage(FilePath);
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
        public void Form_Load(object sender, EventArgs e)
        {
            if (sender != null)
            {
                Controls.Clear();
                ControlsText.Clear();
                Form frm = (Form)sender;
                Update_Title(frm);
                Update_Controls(frm.Controls);
                if (frm.ContextMenu != null)
                    Update_Controls(frm.ContextMenu.MenuItems);
            }
            else
            {
                m_LangSys.LoadLangage(FilePath);
                Update_Title(Me);
                Update_Controls(Me.Controls);
                foreach (object var in Controls)
                {
                    if (var is ILangReloadable)
                    {
                        ((ILangReloadable)var).LoadNonStandardLang();
                        Traces.LogAddDebug(TraceCat.Lang, "appel à LoadNonStandardLang sur un objet de type " + var.GetType().ToString());
                    }
                }
            }
        }

        private void Update_Title(Form frm)
        {
            if (string.IsNullOrEmpty(m_FormSrcTitle))
            {
                m_FormSrcTitle = frm.Text; 
                frm.Text = m_LangSys.C(FilePath, frm.Text);
                Traces.LogAddDebug(TraceCat.Lang, "Form traitée = " + m_FormSrcTitle); 
            }
            else
                frm.Text = m_LangSys.C(FilePath, m_FormSrcTitle);
        }
    }
}

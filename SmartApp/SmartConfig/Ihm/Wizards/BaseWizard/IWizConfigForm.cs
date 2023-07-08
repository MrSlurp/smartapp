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

namespace SmartApp.Wizards
{
    /// <summary>
    /// interface des panneau de configuration des project wizard
    /// </summary>
    interface IWizConfigForm
    {
        /// <summary>
        /// donnée de configuration du wizard
        /// </summary>
        WizardConfigData WizConfig
        { set; }

        /// <summary>
        /// fonction effectuant le transfert entre l'IHM et les config du Wizard
        /// </summary>
        void HmiToData();

        /// <summary>
        /// position d'attachement de la form dans le parent
        /// </summary>
        AnchorStyles Anchor
        { get; set;}

        /// <summary>
        /// définit si le panel est visible
        /// </summary>
        bool Visible
        { get; set;}

        /// <summary>
        /// assigne ou obtient la largeur du panel
        /// </summary>
        int Width
        { get; set;}

        /// <summary>
        /// assigne ou obtient la hauteur du panel
        /// </summary>
        int Height
        { get; set;}

        // provision pour plus tard sans doute.
        /*
        bool CanGoBack();

        bool CanGoNext();*/
    }
}

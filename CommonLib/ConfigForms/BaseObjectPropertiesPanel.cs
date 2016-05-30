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
using System.Windows.Forms;

namespace CommonLib
{
    public class BaseObjectPropertiesPanel : UserControl
    {
        // document courant
        protected BTDoc m_Document = null;

        /// <summary>
        /// assigne ou obtiens le document courant
        /// </summary>
        public virtual BTDoc Document
        {
            get
            {
                return m_Document;
            }
            set
            {
                m_Document = value;
            }
        }

        public Control Panel 
        { 
            get { return this; } 
        }

        /// <summary>
        /// attribut en lecture seul qui indique si les propriété courantes sont valides
        /// </summary>
        public virtual bool IsObjectPropertiesValid
        {
            get
            {
                // aucun cas d'invalidité pour cet item
                return true;
            }
        }

        /// <summary>
        /// valide les paramètres et affiche un message en cas d'erreur
        /// </summary>
        /// <returns>true si les paramètres sont valides</returns>
        public virtual bool ValidateProperties()
        {
            // aucun cas d'invalidité pour cet item
            return true;
        }

    }
}

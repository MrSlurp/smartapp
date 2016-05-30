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

namespace SmartApp.Wizards
{
    /// <summary>
    /// cette classe représente la configuration d'un bloc fonction de l'atelier M3 ou Z2
    /// C'est la classe de base qui définit les mécanismes généraux
    /// </summary>
    public class BlocConfig
    {
        /// <summary>
        /// Type de bloc
        /// </summary>
        protected BlocsType m_BlocType;

        /// <summary>
        /// Indice ou numéro de bloc ou de slot ES)
        /// </summary>
        protected int m_Indice;

        /// <summary>
        /// Liste des ES du bloc
        /// </summary>
        protected IOConfig[] m_ListIO;

        /// <summary>
        /// indique si le bloc est utilisé
        /// </summary>
        protected bool m_IsUsed = true;

        /// <summary>
        /// accesseur de la liste des ES du bloc
        /// </summary>
        public IOConfig[] ListIO
        {
            get { return m_ListIO; }
        }

        /// <summary>
        /// accesseur sur le type du bloc
        /// </summary>
        public BlocsType BlocType
        {
            get { return m_BlocType; }
        }

        /// <summary>
        /// accesseur sur l'indice du bloc
        /// </summary>
        public int Indice
        {
            get { return m_Indice; }
        }

        /// <summary>
        /// obtient ou assigne la variable d'utilisation du bloc
        /// </summary>
        public bool IsUsed
        {
            get { return m_IsUsed; }
            set { m_IsUsed = value; }
        }

        /// <summary>
        /// obtient le nom générique du bloc
        /// </summary>
        public virtual string Name
        {
            get { return string.Format("{0} {1}", m_BlocType.ToString(), m_Indice); }
        }
    }
}

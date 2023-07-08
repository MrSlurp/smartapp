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

namespace CommonLib
{
    public delegate void CurrentDataListChanged();
    public delegate void BeforeCurrentDataListChange();

    #region classe CComboData
    /// <summary>
    /// classe "utilitaire" pour afficher les texte de la combo et avoir une valeur associée
    /// (utilisation de l'attribut DataSource)
    /// </summary>
    public class CComboData : Object
    {
        private string m_String;
        //private int m_Value;
        object m_refObject;

        /// <summary>
        /// constructeur par défaut
        /// </summary>
        public CComboData()
        {
            m_String = "";
            m_refObject = null;
        }

        /// <summary>
        /// constructeur paramétré
        /// </summary>
        /// <param name="strToDisplay">chaine à afficher dans la combo</param>
        /// <param name="value">valeur associé au texte</param>
        public CComboData(string strToDisplay, object value)
        {
            m_String = strToDisplay;
            m_refObject = value;
        }

        /// <summary>
        /// accesseur de la chaine affiché dans la combo
        /// </summary>
        public string DisplayedString
        {
            get
            {
                return m_String;
            }
        }

        /// <summary>
        /// accesseur de la valeur associée à la chaine
        /// </summary>
        public object Object
        {
            get
            {
                return m_refObject;
            }
        }
    }
    #endregion
}

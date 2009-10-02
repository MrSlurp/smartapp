using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLib
{
    public delegate void CurrentDataListChanged();
    public delegate void BeforeCurrentDataListChange();

    #region classe CComboData
    /// <summary>
    /// classe "utilitaire" pour afficher les texte de la combo et avoir une valeur associ�e
    /// (utilisation de l'attribut DataSource)
    /// </summary>
    public class CComboData : Object
    {
        private string m_String;
        //private int m_Value;
        object m_refObject;

        /// <summary>
        /// constructeur par d�faut
        /// </summary>
        public CComboData()
        {
            m_String = "";
            m_refObject = null;
        }

        /// <summary>
        /// constructeur param�tr�
        /// </summary>
        /// <param name="strToDisplay">chaine � afficher dans la combo</param>
        /// <param name="value">valeur associ� au texte</param>
        public CComboData(string strToDisplay, object value)
        {
            m_String = strToDisplay;
            m_refObject = value;
        }

        /// <summary>
        /// accesseur de la chaine affich� dans la combo
        /// </summary>
        public string DisplayedString
        {
            get
            {
                return m_String;
            }
        }

        /// <summary>
        /// accesseur de la valeur associ�e � la chaine
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

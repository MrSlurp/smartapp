using System;
using System.Collections.Generic;
using System.Text;

namespace SmartApp.Ihm
{
    public delegate void CurrentDataListChanged();
    public delegate void BeforeCurrentDataListChange();

    #region classe CComboData
    //*****************************************************************************************************
    // Description:
    // classe "utilitaire" pour afficher les texte de la combo et avoir une valeur associée
    // (utilisation de l'attribut DataSource)
    //*****************************************************************************************************
    class CComboData : Object
    {
        private string m_String;
        private int m_Value;
        object m_refObject;

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public CComboData()
        {
            m_String = "";
            m_Value = 0;
            m_refObject = null;
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        /*
        public CComboData(string strToDisplay, int value)
        {
            m_String = strToDisplay;
            m_Value = value;
            m_refObject = null;
        }
        */
        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public CComboData(string strToDisplay, object value)
        {
            m_String = strToDisplay;
            m_Value = 0;
            m_refObject = value;
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public int ItemValue
        {
            get
            {
                return m_Value;
            }
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public string DisplayedString
        {
            get
            {
                return m_String;
            }
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
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

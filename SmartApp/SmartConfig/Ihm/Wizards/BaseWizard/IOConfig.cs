using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;

namespace SmartApp.Wizards
{
    /// <summary>
    /// cette classe représente la configuration d'une ES des bloc SL ou ETH
    /// </summary>
    public class IOConfig
    {
        /// <summary>
        /// format de découpage de l'ES
        /// </summary>
        private IOSplitFormat m_SplitFormat = IOSplitFormat.SplitNone;

        /// <summary>
        /// liste des symbols à associer à l'ES
        /// </summary>
        private StringCollection m_ListSymbol = new StringCollection();

        /// <summary>
        /// Nom de l'ES de la forme Ox/Ix
        /// </summary>
        private string m_IOName = string.Empty;

        /// <summary>
        /// nom court de l'ES
        /// </summary>
        private string m_ShortName = string.Empty;


        /// <summary>
        /// constructeur par défaut
        /// </summary>
        /// <param name="Name">Nom long</param>
        /// <param name="shortName">Nom court</param>
        public IOConfig(string Name, string shortName)
        {
            m_IOName = Name;
            m_ShortName = shortName;
        }

        /// <summary>
        /// accesseur sur le type de splitage de l'ES
        /// </summary>
        public IOSplitFormat SplitFormat
        {
            get { return m_SplitFormat; }
            set { m_SplitFormat = value; }
        }

        /// <summary>
        /// accesseur sur le nom de l'ES
        /// </summary>
        public string Name
        {
            get { return m_IOName; }
        }

        /// <summary>
        /// accesseur sur le nom court de l'ES
        /// </summary>
        public string ShortName
        {
            get { return m_ShortName; }
        }

        /// <summary>
        /// Liste des symboles par défaut ou utilisateur qui se trouvent dans l'ES
        /// (un seul si pas de split, sinon autant qu'il y a de sous donnée)
        /// </summary>
        public StringCollection ListSymbol
        {
            get { return m_ListSymbol; }
        }
    }
}

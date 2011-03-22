using System;
using System.Collections.Generic;
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
        /// Nom de l'ES de la forme Ox/Ix
        /// </summary>
        private string m_IOName = string.Empty;

        /// <summary>
        /// constructeur par défaut
        /// </summary>
        /// <param name="Name"></param>
        public IOConfig(string Name)
        {
            m_IOName = Name;
        }

        /// <summary>
        /// accesseur sur le type de splitage de l'ES
        /// </summary>
        public IOSplitFormat SplitFormat
        {
            get { return m_SplitFormat; }
            set { m_SplitFormat = value; }
        }

        // accesseur sur le nom de l'ES
        public string Name
        {
            get { return m_IOName; }
        }
    }
}

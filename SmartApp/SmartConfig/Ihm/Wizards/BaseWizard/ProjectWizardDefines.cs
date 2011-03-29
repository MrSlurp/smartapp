using System;
using System.Collections.Generic;
using System.Text;

namespace SmartApp.Wizards
{
    /// <summary>
    /// types des blocs
    /// </summary>
    public enum BlocsType
    {
        /// <summary>
        /// bloc d'entrée supervision
        /// </summary>
        IN,
        /// <summary>
        /// bloc de sortie supervision
        /// </summary>
        OUT,
    }

    /// <summary>
    /// type de découpage d'une entrée/sortie
    /// </summary>
    public enum IOSplitFormat
    {
        /// <summary>
        /// aucun découpage
        /// </summary>
        SplitNone,
        /// <summary>
        /// découpé en 16 bits
        /// </summary>
        SplitBy16,
        /// <summary>
        /// découpé en 4 quartets
        /// </summary>
        SplitBy4,
        /// <summary>
        /// découpé en 2 octets
        /// </summary>
        SplitBy2,
    }
}

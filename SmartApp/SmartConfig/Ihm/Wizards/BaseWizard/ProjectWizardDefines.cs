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
        /// bloc d'entr�e supervision
        /// </summary>
        IN,
        /// <summary>
        /// bloc de sortie supervision
        /// </summary>
        OUT,
    }

    /// <summary>
    /// type de d�coupage d'une entr�e/sortie
    /// </summary>
    public enum IOSplitFormat
    {
        /// <summary>
        /// aucun d�coupage
        /// </summary>
        SplitNone,
        /// <summary>
        /// d�coup� en 16 bits
        /// </summary>
        SplitBy16,
        /// <summary>
        /// d�coup� en 4 quartets
        /// </summary>
        SplitBy4,
        /// <summary>
        /// d�coup� en 2 octets
        /// </summary>
        SplitBy2,
    }
}

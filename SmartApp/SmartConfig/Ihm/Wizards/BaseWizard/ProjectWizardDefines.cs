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

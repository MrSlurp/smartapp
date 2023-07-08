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
    /// classe spécifique à la gestion des bloc SL
    /// </summary>
    public class SLBlocConfig : BlocConfig
    {
        /// <summary>
        /// nombre d'ES par bloc SL
        /// </summary>
        protected const int IO_PER_BLOC = 8;

        /// <summary>
        /// constructeur par défaut
        /// </summary>
        /// <param name="type">type de bloc</param>
        /// <param name="indice">indice du bloc</param>
        public SLBlocConfig(BlocsType type, int indice)
        {
            m_ListIO = new IOConfig[IO_PER_BLOC];
            m_BlocType = type;
            m_Indice = indice;
            for (int i = 0; i < IO_PER_BLOC; i++)
            {
                if (m_BlocType == BlocsType.IN)
                {
                    m_ListIO[i] = new IOConfig(string.Format("{0}{1}", "Output ", i + 1),
                                               string.Format("{0}{1}", "O", i + 1));
                }
                else if (m_BlocType == BlocsType.OUT)
                {
                    m_ListIO[i] = new IOConfig(string.Format("{0}{1}", "Input ", i + 1),
                                               string.Format("{0}{1}", "I", i + 1));
                }
            }
        }

        /// <summary>
        /// renvoie le nom spécifique d'un bloc SL
        /// </summary>
        public override string Name
        {
            get { return string.Format("SL{0} {1}", m_BlocType.ToString(), m_Indice); }
        }
    }
}

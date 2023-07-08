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
    /// classe spécifique à la gestion des bloc ETH
    /// </summary>
    public class M3XN05BlocConfig : BlocConfig
    {
        /// <summary>
        /// nombre d'ES par bloc SL
        /// </summary>
        protected const int IO_PER_BLOC = 1;

        /// <summary>
        /// adresse de base des registre ETH IN
        /// </summary>
        protected const int BaseInRegAddr = 12;

        /// <summary>
        /// adresse de base des registre ETH OUT
        /// </summary>
        protected const int BaseOutRegAddr = 20;

        /// <summary>
        /// adresse absolue du registre modbus
        /// </summary>
        protected int m_MbRegAddr;

        /// <summary>
        /// obtient l'adresse du registre modbus correspondant au bloc
        /// </summary>
        public int MbRegAddr
        {
            get { return m_MbRegAddr; }
        }

        /// <summary>
        /// constructeur par défaut
        /// </summary>
        /// <param name="type">type de bloc</param>
        /// <param name="indice">indice du bloc</param>
        public M3XN05BlocConfig(BlocsType type, int indice)
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
                    m_MbRegAddr = BaseInRegAddr + i;
                }
                else if (m_BlocType == BlocsType.OUT)
                {
                    m_ListIO[i] = new IOConfig(string.Format("{0}{1}", "Input ", i + 1),
                                               string.Format("{0}{1}", "I", i + 1));
                    m_MbRegAddr = BaseOutRegAddr + i;
                }
            }
        }

        /// <summary>
        /// renvoie le nom spécifique d'un bloc SL
        /// </summary>
        public override string Name
        {
            get { return string.Format("ETH {0}{1}", m_BlocType.ToString(), m_Indice); }
        }
    }
}

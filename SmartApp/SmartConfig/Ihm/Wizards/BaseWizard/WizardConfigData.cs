/*
    This file is part of SmartApp.

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
using System.Drawing;

namespace SmartApp.Wizards
{
    /// <summary>
    /// classe de base repr�sentant les �l�ment standard d'une config de project wizard
    /// </summary>
    public abstract class WizardConfigData
    {
        /// <summary>
        /// table des images a afficher pour le split des entr�e
        /// </summary>
        protected Image[] m_SplitInTabImages;

        /// <summary>
        /// table des images a afficher pour le split des entr�e
        /// </summary>
        protected Image[] m_SplitOutTabImages;

        /// <summary>
        /// liste des blocs d'entr�e de donn�es (blocs qui ont des sorties)
        /// </summary>
        protected BlocConfig[] m_INBlocList;

        /// <summary>
        /// liste des blocs de sortie de donn�es (blocs qui ont des entr�e)
        /// </summary>
        protected BlocConfig[] m_OUTBlocList;

        /// <summary>
        /// renvoie la liste des bloc du type donn�
        /// </summary>
        /// <param name="type">type de bloc</param>
        /// <returns></returns>
        public BlocConfig[] GetBlocListByType(BlocsType type)
        {
            if (type == BlocsType.IN)
                return m_INBlocList;
            else if (type == BlocsType.OUT)
                return m_OUTBlocList;
            else
                return null;
        }

        /// <summary>
        /// permet de d�finir les blocs qui sont utilis� ou non
        /// </summary>
        /// <param name="type">type de bloc</param>
        /// <param name="indice">indice du bloc</param>
        /// <param name="Used">true si utilis�</param>
        public void SetBlocUsed(BlocsType type, int indice, bool Used)
        {
            if (type == BlocsType.IN)
            {
                System.Diagnostics.Debug.Assert(indice >= 0 && indice <= m_INBlocList.Length);
                m_INBlocList[indice].IsUsed = Used;
            }
            else if (type == BlocsType.OUT)
            {
                System.Diagnostics.Debug.Assert(indice >= 0 && indice <= m_OUTBlocList.Length);
                m_OUTBlocList[indice].IsUsed = Used;
            }
        }

        /// <summary>
        /// renvoie si au moins un bloc du type donn� est utilis�
        /// </summary>
        /// <param name="type">type de bloc</param>
        /// <returns></returns>
        public bool HaveIOTypeUsed(BlocsType type)
        {
            bool bUsed = false;
            if (type == BlocsType.IN)
            {
                for (int i = 0; i < m_INBlocList.Length; i++)
                {
                    bUsed |= m_INBlocList[i].IsUsed;
                }
                return bUsed;
            }
            else if (type == BlocsType.OUT)
            {
                for (int i = 0; i < m_OUTBlocList.Length; i++)
                {
                    bUsed |= m_OUTBlocList[i].IsUsed;
                }
                return bUsed;
            }
            else
                return false;
        }

        /// <summary>
        /// renvoie le speech de bienvenue du wizard
        /// </summary>
        /// <returns>speech d'intro</returns>
        public abstract string GetWelcomeSpeech();

        /// <summary>
        /// renvoie l'image affich�e sur la page de bienvenue
        /// </summary>
        /// <returns>Image d'intro</returns>
        public abstract Image GetWelcomeImage();

        /// <summary>
        /// cr�e le r�sum� final du wizard en fonction de ce qui est configur�
        /// </summary>
        /// <returns>r�sume</returns>
        public abstract string CreateFinalSummury();

        /// <summary>
        /// renvoie les images correspondantes au mode de split pour les entr�e supervision
        /// </summary>
        public Image[] TabInSplitImage
        {
            get { return m_SplitInTabImages; }
        }

        /// <summary>
        /// renvoie les images correspondantes au mode de split pour les entr�e sorties
        /// </summary>
        public Image[] TabOutSplitImage
        {
            get { return m_SplitOutTabImages; }
        }

    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace SmartApp.Wizards
{
    /// <summary>
    /// classe de base repr�sentant les �l�ment standard d'une config de project wizard
    /// </summary>
    public class WizardConfigData
    {
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
    }
}

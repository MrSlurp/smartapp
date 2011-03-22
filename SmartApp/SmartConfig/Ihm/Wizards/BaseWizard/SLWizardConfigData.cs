using System;
using System.Collections.Generic;
using System.Text;

namespace SmartApp.Wizards
{
    /// <summary>
    /// classe spécifique de configuration d'un project wizard utilisant les bloc SL M3Z2
    /// </summary>
    public class SLWizardConfigData : WizardConfigData
    {
        /// <summary>
        /// nombre de bloc SL de chaque type
        /// </summary>
        protected const int NB_SL_IO_BLOC = 3;

        /// <summary>
        /// constructeur par défaut initialisant les tables de la classe
        /// </summary>
        public SLWizardConfigData()
        {
            m_INBlocList = new SLBlocConfig[NB_SL_IO_BLOC];
            m_OUTBlocList = new SLBlocConfig[NB_SL_IO_BLOC];
            for (int i = 1; i <= NB_SL_IO_BLOC; i++)
            {
                m_INBlocList[i-1] = new SLBlocConfig(BlocsType.IN, i);
                m_OUTBlocList[i-1] = new SLBlocConfig(BlocsType.OUT, i);
            }
        }
    }
}

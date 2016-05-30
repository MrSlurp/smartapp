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
using System.Windows.Forms;
using CommonLib;

namespace SmartApp.Wizards
{
    /// <summary>
    /// classe spécifique de configuration d'un project wizard utilisant les bloc NUM IN/OUT M3Z2
    /// </summary>
    class M3XN05WizardConfigData : WizardConfigData
    {
        /// <summary>
        /// nombre de bloc SL de chaque type
        /// </summary>
        protected const int NB_XN05_IO_BLOC = 8;

        protected const int NB_HOUR_IO_BLOC = 4;

        /// <summary>
        /// liste des blocs d'entrée de données (blocs qui ont des sorties)
        /// </summary>
        protected BlocConfig[] m_INHourBlocList;

        /// <summary>
        /// liste des blocs de sortie de données (blocs qui ont des entrée)
        /// </summary>
        protected BlocConfig[] m_OUTHourBlocList;

        /// <summary>
        /// constructeur par défaut initialisant les tables de la classe
        /// </summary>
        public M3XN05WizardConfigData()
        {
            m_INBlocList = new M3XN05BlocConfig[NB_XN05_IO_BLOC];
            m_OUTBlocList = new M3XN05BlocConfig[NB_XN05_IO_BLOC];
            for (int i = 1; i <= NB_XN05_IO_BLOC; i++)
            {
                m_INBlocList[i - 1] = new M3XN05BlocConfig(BlocsType.IN, i);
                m_OUTBlocList[i - 1] = new M3XN05BlocConfig(BlocsType.OUT, i);
            }


            m_INHourBlocList = new M3XN05BlocConfig[NB_HOUR_IO_BLOC];
            m_OUTHourBlocList = new M3XN05BlocConfig[NB_HOUR_IO_BLOC];
            for (int i = 10; i <= NB_HOUR_IO_BLOC; i++)
            {
                m_INHourBlocList[i - 1] = new M3XN05BlocConfig(BlocsType.IN, i);
                m_INHourBlocList[i - 1].ListIO[0].SplitFormat = IOSplitFormat.SplitBy2;
                m_OUTHourBlocList[i - 1] = new M3XN05BlocConfig(BlocsType.OUT, i);
                m_OUTHourBlocList[i - 1].ListIO[0].SplitFormat = IOSplitFormat.SplitBy2;
            }

            m_SplitInTabImages = new Image[3];
            m_SplitInTabImages[0] = Resources.TypeSplit16_ETH_IN;
            m_SplitInTabImages[1] = Resources.TypeSplit4_ETH_IN;
            m_SplitInTabImages[2] = Resources.TypeSplit2_ETH_IN;
            m_SplitOutTabImages = new Image[3];
            m_SplitOutTabImages[0] = Resources.TypeSplit16_ETH_OUT;
            m_SplitOutTabImages[1] = Resources.TypeSplit4_ETH_OUT;
            m_SplitOutTabImages[2] = Resources.TypeSplit2_ETH_OUT;
        }

        /// <summary>
        /// renvoie le speech de bienvenue du wizard
        /// </summary>
        /// <returns>speech d'intro</returns>
        public override string GetWelcomeSpeech()
        {
            string speech = Program.LangSys.C("Welcome to the Millenium 3 project wizard") +
                              "\n" +
                              Program.LangSys.C("This wizard will help you to create a new project for") +
                              "\n" +
                              Program.LangSys.C("Millenium 3 supervision through TCP Modbus link using XN05 expansion module");
            return speech;
        }

        /// <summary>
        /// renvoie l'image affichée sur la page de bienvenue
        /// </summary>
        /// <returns>Image d'intro</returns>
        public override Image GetWelcomeImage()
        {
            return Resources.WizardEthProject;
        }

        /// <summary>
        /// crée le résumé final du wizard en fonction de ce qui est configuré
        /// </summary>
        /// <returns>résume</returns>
        public override string CreateFinalSummury()
        {
            string resume = string.Empty;
            if (HaveIOTypeUsed(BlocsType.IN))
            {
                resume = Program.LangSys.C("Frames for read and write:") + "\r\n";
                BlocConfig[] InCfg = GetBlocListByType(BlocsType.IN);
                for (int i = 0; i < InCfg.Length; i++)
                {
                    if (InCfg[i].IsUsed)
                    {
                        if (i != 0)
                            resume += ", ";
                        resume += InCfg[i].Name;
                    }
                }
                resume += "\r\n" + Program.LangSys.C("(All will be included in a single function)");
                resume += "\r\n";
            }

            if (HaveIOTypeUsed(BlocsType.OUT))
            {
                resume += Program.LangSys.C("Frames for read:") + "\r\n";
                BlocConfig[] InCfg = GetBlocListByType(BlocsType.OUT);
                for (int i = 0; i < InCfg.Length; i++)
                {
                    if (i != 0)
                        resume += ",";
                    if (InCfg[i].IsUsed)
                    {
                        string format = Program.LangSys.C("{0} bloc");
                        resume += string.Format(format, InCfg[i].Name);
                    }
                }
                resume += "\r\n" + Program.LangSys.C("(All will be included in a single function)");
                resume += "\r\n" + Program.LangSys.C("A timer will be created for periodically reading Eth out bloc");
                resume += "\r\n";
            }
            resume += "\r\n" + Program.LangSys.C("All datas corresponding to your configurations");
            resume += "\r\n";
            resume += "\r\n" + Program.LangSys.C("A default screen with needed initialisations");

            return resume;
        }

        public Control GetAdditonalsOptionsPanel()
        {
            return null;
        }
    }
}

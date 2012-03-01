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
    class Z2SR3NETWizardConfigData : WizardConfigData
    {
        /// <summary>
        /// nombre de bloc SL de chaque type
        /// </summary>
        protected const int NB_SR3NET_IO_BLOC = 4;

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
        public Z2SR3NETWizardConfigData()
        {
            m_INBlocList = new M3XN05BlocConfig[NB_SR3NET_IO_BLOC];
            m_OUTBlocList = new M3XN05BlocConfig[NB_SR3NET_IO_BLOC];
            for (int i = 1; i <= NB_SR3NET_IO_BLOC; i++)
            {
                m_INBlocList[i - 1] = new M3XN05BlocConfig(BlocsType.IN, i);
                m_OUTBlocList[i - 1] = new M3XN05BlocConfig(BlocsType.OUT, i);
            }

            m_INHourBlocList = new M3XN05BlocConfig[NB_HOUR_IO_BLOC];
            m_OUTHourBlocList = new M3XN05BlocConfig[NB_HOUR_IO_BLOC];
            for (int i = 1; i <= NB_HOUR_IO_BLOC; i++)
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
            m_SplitOutTabImages = new Image[1];
            m_SplitOutTabImages[0] = Resources.TypeSplit16_ETH_OUT;

        }

        /// <summary>
        /// renvoie le speech de bienvenue du wizard
        /// </summary>
        /// <returns>speech d'intro</returns>
        public override string GetWelcomeSpeech()
        {
            string speech = Program.LangSys.C("Welcome to the Zélio 2 project wizard") +
                              "\n" +
                              Program.LangSys.C("This wizard will help you to create a new project for") +
                              "\n" +
                              Program.LangSys.C("Zélio 2 supervision through TCP Modbus link using XN05 expansion module");
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

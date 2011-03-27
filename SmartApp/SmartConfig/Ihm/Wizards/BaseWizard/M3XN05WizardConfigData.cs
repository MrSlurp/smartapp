using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
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
        }

        public override string GetWelcomeSpeech()
        {
            string speech = Program.LangSys.C("Welcome to the Millenium 3 project wizard") +
                              "\n" +
                              Program.LangSys.C("This wizard will help you to create a new project for ") +
                              "\n" +
                              Program.LangSys.C("Millenium 3 supervision through TCP Modbus link using XN05 expansion module");
            return speech;
        }

        public override Image GetWelcomeImage()
        {
            return Resources.WizardSLProject;
        }
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
                resume += Program.LangSys.C("Frames for read:") + "\n";
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
                resume += "\r\n" + Program.LangSys.C("A timer will be created for periodically reading SL out bloc");
                resume += "\r\n";
            }
            resume += "\r\n" + Program.LangSys.C("All datas corresponding to your configurations");
            resume += "\r\n";
            resume += "\r\n" + Program.LangSys.C("A default screen with needed initialisations");

            return resume;
        }
    }
}

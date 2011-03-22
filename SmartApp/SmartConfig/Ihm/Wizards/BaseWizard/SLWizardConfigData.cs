using System;
using System.Collections.Generic;
using System.Text;

namespace SmartApp.Ihm.Wizards
{
    public enum BlocsType
    {
        IN,
        OUT,
    }
    public enum IOSplitFormat
    {
        SplitNone,
        SplitBy16,
        SplitBy4,
        SplitBy2,
    }

    public class IOConfig
    {
        private IOSplitFormat m_SplitFormat = IOSplitFormat.SplitNone;
        private string m_IOBName = string.Empty; // de la forme Ox/Ix

        public IOConfig(string Name)
        {
            m_IOBName = Name;
        }

        public IOSplitFormat SplitFormat
        {
            get { return m_SplitFormat; }
            set { m_SplitFormat = value; }
        }

        public string Name
        {
            get { return m_IOBName; }
        }
    }

    public class SLBlocConfig
    {
        public const int IO_PER_BLOC = 8;
        private BlocsType m_BlocType;
        private int m_Indice;
        private IOConfig[] m_ListIO = new IOConfig[IO_PER_BLOC];
        private bool m_IsUsed = false;

        public IOConfig[] ListIO
        {
            get { return m_ListIO; }
        }

        public SLBlocConfig(BlocsType type, int indice)
        {
            m_BlocType = type;
            m_Indice = indice;
            for (int i = 0; i < IO_PER_BLOC; i++)
            {
                if (m_BlocType == BlocsType.IN)
                {
                    m_ListIO[i] = new IOConfig(string.Format("{0}{1}", "Output ", i + 1));
                }
                else if (m_BlocType == BlocsType.OUT)
                {
                    m_ListIO[i] = new IOConfig(string.Format("{0}{1}", "Input ", i + 1));
                }
            }
        }

        public BlocsType BlocType
        {
            get { return m_BlocType; }
        }

        public int Indice
        {
            get { return m_Indice; }
        }

        public string Name
        {
            get { return string.Format("SL{0} {1}",m_BlocType.ToString(), m_Indice) ; }
        }

        public bool IsUsed
        {
            get { return m_IsUsed; }
            set { m_IsUsed = value; }
        }
    }

    public class SLWizardConfigData
    {
        public const int NB_SL_IO_BLOC = 3;
        SLBlocConfig[] m_SLINBlocList = new SLBlocConfig[NB_SL_IO_BLOC];
        SLBlocConfig[] m_SLOUTBlocList = new SLBlocConfig[NB_SL_IO_BLOC];

        public SLWizardConfigData()
        {
            for (int i = 1; i <= NB_SL_IO_BLOC; i++)
            {
                m_SLINBlocList[i-1] = new SLBlocConfig(BlocsType.IN, i);
                m_SLOUTBlocList[i-1] = new SLBlocConfig(BlocsType.OUT, i);
            }
        }

        public void SetBlocUsed(BlocsType type, int indice, bool Used)
        {
            System.Diagnostics.Debug.Assert(indice >= 1 || indice <= 3);

            if (type == BlocsType.IN)
            {
                m_SLINBlocList[indice - 1].IsUsed = Used;
            }
            else if (type == BlocsType.OUT)
            {
                m_SLOUTBlocList[indice - 1].IsUsed = Used;
            }
        }

        public SLBlocConfig[] GetBlocListByType(BlocsType type)
        {
            if (type == BlocsType.IN)
                return m_SLINBlocList;
            else if (type == BlocsType.OUT)
                return m_SLOUTBlocList;
            else
                return null;
        }

        public bool HaveIOTypeUsed(BlocsType type)
        {
            bool bUsed = false;
            if (type == BlocsType.IN)
            {
                for (int i = 0; i < m_SLINBlocList.Length; i++)
                {
                    bUsed |= m_SLINBlocList[i].IsUsed;
                }
                return bUsed;
            }
            else if (type == BlocsType.OUT)
            {
                for (int i = 0; i < m_SLOUTBlocList.Length; i++)
                {
                    bUsed |= m_SLOUTBlocList[i].IsUsed;
                }
                return bUsed;
            }
            else
                return false;
        }
    }
}

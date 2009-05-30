﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace CommonLib
{
    public static class PathTranslator
    {
        public const string DOC_KEY = "$DocPath$";
        public const string EXE_KEY = "$ExePath$";
        static string m_BTDocPath = "";

        public static string BTDocPath
        {
            get
            { return m_BTDocPath; }
            set
            { m_BTDocPath = value; }
        }

        /// <summary>
        /// convertit le chemin réel d'un fichier en chemin relatif
        /// si le chemin du fichier ne contiens ni le chemin de l'exe, ni le chemin du document
        /// on renvoie le chemin complet
        /// </summary>
        /// <param name="CheminCompletFichier"></param>
        /// <returns></returns>
        public static string AbsolutePathToRelative(string CheminCompletFichier)
        {
            if (!string.IsNullOrEmpty(m_BTDocPath) && CheminCompletFichier.Contains(m_BTDocPath))
            {
                return CheminCompletFichier.Replace(m_BTDocPath, DOC_KEY);
            }
            else if (CheminCompletFichier.Contains(Application.StartupPath))
            {
                return CheminCompletFichier.Replace(Application.StartupPath, EXE_KEY);
            }
            else
                return CheminCompletFichier;
        }

        /// <summary>
        /// convertit le chemin relatif d'un fichier en chemin réèl
        /// si le chemin du fichier ne contiens ni la clef exe, ni la clef document
        /// on renvoie le chemin complet
        /// </summary>
        /// <param name="CheminRelatifFichier"></param>
        /// <returns></returns>
        public static string RelativePathToAbsolute(string CheminRelatifFichier)
        {
            string strRet = string.Empty;
            strRet = CheminRelatifFichier;
            if (CheminRelatifFichier.Contains(DOC_KEY))
            {
                strRet = CheminRelatifFichier.Replace(DOC_KEY, m_BTDocPath);
            }
            else if (CheminRelatifFichier.Contains(EXE_KEY))
            {
                strRet = CheminRelatifFichier.Replace(EXE_KEY, Application.StartupPath);
            }
            else if (CheminRelatifFichier.Contains(@".\"))
            {
                strRet = CheminRelatifFichier.Replace(@".\", Application.StartupPath + @"\");
            }
            return strRet;
        }

        public static string LinuxVsWindowsPathUse(string strPath)
        {
#if LINUX
            return strPath.Replace(@"\", @"/");
#else
            return strPath;
#endif
        }

        public static string LinuxVsWindowsPathStore(string strPath)
        {
#if LINUX
            return strPath.Replace(@"/", @"\");
#else
            return strPath;
#endif
        }

    }
}

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
using System.IO;
using System.Windows.Forms;

namespace CommonLib
{
    public class PathTranslator
    {
        public const string DOC_KEY = "$DocPath$";
        public const string EXE_KEY = "$ExePath$";
        public const string SOL_KEY = "$SolPath$";
        string m_BTDocPath = "";

        /// <summary>
        /// accesseur du chemin du document, valide dès qu'un document est ouvert
        /// </summary>
        public string BTDocPath
        {
            get { return m_BTDocPath; }
            set { m_BTDocPath = value; }
        }

        /// <summary>
        /// convertit le chemin réel d'un fichier en chemin relatif
        /// si le chemin du fichier ne contiens ni le chemin de l'exe, ni le chemin du document
        /// on renvoie le chemin complet
        /// </summary>
        /// <param name="CheminCompletFichier">chemin du fichier</param>
        /// <returns>chemin relatif du fichier</returns>
        public string AbsolutePathToRelative(string CheminCompletFichier)
        {
            if (!String.IsNullOrEmpty(CheminCompletFichier))
            {
                if (!string.IsNullOrEmpty(m_BTDocPath) && CheminCompletFichier.Contains(m_BTDocPath))
                {
                    return CheminCompletFichier.Replace(m_BTDocPath, DOC_KEY);
                }
                else if (CheminCompletFichier.Contains(Application.StartupPath))
                {
                    return CheminCompletFichier.Replace(Application.StartupPath, EXE_KEY);
                }
                else if (CheminCompletFichier.Contains(EXE_KEY) && string.IsNullOrEmpty(m_BTDocPath))
                {
                    return CheminCompletFichier.Replace(Application.StartupPath, EXE_KEY);
                }
                else
                    return CheminCompletFichier;
            }
            else
                return string.Empty;
        }

        /// <summary>
        /// convertit le chemin relatif d'un fichier en chemin réèl
        /// si le chemin du fichier ne contiens ni la clef exe, ni la clef document
        /// on renvoie le chemin complet
        /// </summary>
        /// <param name="CheminRelatifFichier">chemin relatif du fichier</param>
        /// <returns>chemin complet du fichier</returns>
        public string RelativePathToAbsolute(string CheminRelatifFichier)
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

        /// <summary>
        /// traduction des chemin entre OS Linux et Windows pour l'utilisation
        /// </summary>
        /// <param name="strPath">chemin à traduire</param>
        /// <returns>chemin traduit</returns>
        public static string LinuxVsWindowsPathUse(string strPath)
        {
#if LINUX
            return strPath.Replace(@"\", @"/");
#else
            return strPath;
#endif
        }

        /// <summary>
        /// traduction des chemin entre OS Linux et Windows pour la sauvegarde
        /// </summary>
        /// <param name="strPath">chemin à traduire</param>
        /// <returns>chemin traduit</returns>
        public static string LinuxVsWindowsPathStore(string strPath)
        {
#if LINUX
            return strPath.Replace(@"/", @"\");
#else
            return strPath;
#endif
        }

        public static void CheckFileExistOrThrow(string strFilePath)
        {
            if (File.Exists(strFilePath))
                return;
            else
                throw new Exception("File doesnot exists");
        }

    }
}

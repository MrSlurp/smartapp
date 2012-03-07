using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace CommonLib
{
    /// <summary>
    /// classe de centralisation des boite de dialogue d'ouverture de fichier
    /// </summary>
    public static class CentralizedFileDlg
    {
        private static OpenFileDialog m_usrFileDialog = new OpenFileDialog();
        private static OpenFileDialog m_prjoFileDialog = new OpenFileDialog();
        private static SaveFileDialog m_prjsFileDialog = new SaveFileDialog();

        #region ofd d'image/sons
        /// <summary>
        /// initialise la boite de dialogue des fichier images avec comme
        /// répertoire par défaut le chemin passé en paramètre
        /// </summary>
        /// <param name="initDir"></param>
        public static void InitImgFileDialog(string initDir)
        {
            m_usrFileDialog.InitialDirectory = initDir;
            m_usrFileDialog.CustomPlaces.Clear();
            m_usrFileDialog.CustomPlaces.Add(new FileDialogCustomPlace(PathTranslator.LinuxVsWindowsPathUse(Application.StartupPath + @"\" + "ImgLib")));
        }

        /// <summary>
        /// assigne le chemin initial de la fenêtre de parcour en fonction du "browse source" 
        /// selectionné par l'utilisateur a l'aide du menu du BrowsFileBTn
        /// </summary>
        /// <param name="fd">FileDialog à parémétrer</param>
        /// <param name="bf">Source de parcour</param>
        private static void SetDirFromBrowseSource(FileDialog fd, BrowseFileBtn.BrowseFrom bf)
        {
            switch (bf)
            {
                case BrowseFileBtn.BrowseFrom.App:
                    fd.InitialDirectory = Application.StartupPath;
                    break;
                case BrowseFileBtn.BrowseFrom.Project:
                    if (!string.IsNullOrEmpty(PathTranslator.BTDocPath))
                        fd.InitialDirectory = PathTranslator.BTDocPath;
                    else
                        fd.InitialDirectory = Application.StartupPath;
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// affiche la boite de dialogue de sélection d'image et si l'utilisateur valide avec OK
        /// mémorise le dernier chemin utilisé et le mémorise comme chemin pour la prochaine ouverture
        /// </summary>
        /// <returns>la réponse de l'utilisateur à la boite de dialogue</returns>
         /*
        public static DialogResult ShowImageFileDilaog()
        {
            m_usrFileDialog.Filter = Lang.LangSys.C("Image Files (jpeg, gif, bmp, png)|*.jpg;*.jpeg;*.gif;*.bmp;*.png|JPEG Files(*.jpg;*.jpeg)|*.jpg;*.jpeg|GIF Files(*.gif)|*.gif|BMP Files(*.bmp)|*.bmp|PNG Files(*.png)|*.png");
            DialogResult dlgRes = m_usrFileDialog.ShowDialog();
            if (dlgRes == DialogResult.OK)
            {
                string path = Path.GetDirectoryName(m_usrFileDialog.FileName);
                m_usrFileDialog.InitialDirectory = path;
            }
            return dlgRes;
        }
        */
        /// <summary>
        /// affiche la boite de dialogue de sélection d'image et si l'utilisateur valide avec OK
        /// mémorise le dernier chemin utilisé et le mémorise comme chemin pour la prochaine ouverture
        /// </summary>
        /// <param name="bf">Source du parcours</param>
        /// <returns>la réponse de l'utilisateur à la boite de dialogue</returns>
        public static DialogResult ShowImageFileDilaog(BrowseFileBtn.BrowseFrom bf)
        {
            SetDirFromBrowseSource(m_usrFileDialog, bf);
            m_usrFileDialog.Filter = Lang.LangSys.C("Image Files (jpeg, gif, bmp, png)|*.jpg;*.jpeg;*.gif;*.bmp;*.png|JPEG Files(*.jpg;*.jpeg)|*.jpg;*.jpeg|GIF Files(*.gif)|*.gif|BMP Files(*.bmp)|*.bmp|PNG Files(*.png)|*.png");
            DialogResult dlgRes = m_usrFileDialog.ShowDialog();
            if (dlgRes == DialogResult.OK)
            {
                string path = Path.GetDirectoryName(m_usrFileDialog.FileName);
                m_usrFileDialog.InitialDirectory = path;
            }
            return dlgRes;
        }

        /// <summary>
        /// affiche la boite de dialogue de sélection de fichier son et si l'utilisateur valide avec OK
        /// mémorise le dernier chemin utilisé et le mémorise comme chemin pour la prochaine ouverture
        /// </summary>
        /// <returns>la réponse de l'utilisateur à la boite de dialogue</returns>
        /*
        public static DialogResult ShowSndFileDilaog()
        {
            m_usrFileDialog.Filter = Lang.LangSys.C("WAVE Files (*.wav)|*.wav");
            DialogResult dlgRes = m_usrFileDialog.ShowDialog();
            if (dlgRes == DialogResult.OK)
            {
                string path = Path.GetDirectoryName(m_usrFileDialog.FileName);
                m_usrFileDialog.InitialDirectory = path;
            }
            return dlgRes;
        }*/

        /// <summary>
        /// affiche la boite de dialogue de sélection de fichier son et si l'utilisateur valide avec OK
        /// mémorise le dernier chemin utilisé et le mémorise comme chemin pour la prochaine ouverture
        /// </summary>
        /// <param name="bf">Source du parcours</param>
        /// <returns>la réponse de l'utilisateur à la boite de dialogue</returns>
        public static DialogResult ShowSndFileDilaog(BrowseFileBtn.BrowseFrom bf)
        {
            SetDirFromBrowseSource(m_usrFileDialog, bf);
            m_usrFileDialog.Filter = Lang.LangSys.C("WAVE Files (*.wav)|*.wav");
            DialogResult dlgRes = m_usrFileDialog.ShowDialog();
            if (dlgRes == DialogResult.OK)
            {
                string path = Path.GetDirectoryName(m_usrFileDialog.FileName);
                m_usrFileDialog.InitialDirectory = path;
            }
            return dlgRes;
        }

        /// <summary>
        /// renvoie le chemin de l'image sélectionné.
        /// </summary>
        public static string ImgFileName
        {
            get
            {
                return m_usrFileDialog.FileName;
            }
        }

        /// <summary>
        /// renvoie le chemin de l'image sélectionné.
        /// </summary>
        public static string SndFileName
        {
            get
            {
                return m_usrFileDialog.FileName;
            }
        }
        #endregion

        #region prj dialog
        /// <summary>
        /// initialise la boite de dialogue des fichier projets avec comme
        /// répertoire par défaut le chemin passé en paramètre
        /// </summary>
        /// <param name="initDir"></param>
        public static void InitPrjFileDialog(string initDir)
        {
            m_prjoFileDialog.Filter = Lang.LangSys.C("SmartApp File (*.saf)|*.saf");
            m_prjoFileDialog.InitialDirectory = initDir;
            m_prjoFileDialog.CustomPlaces.Clear();
            m_prjoFileDialog.CustomPlaces.Add(new FileDialogCustomPlace(Application.StartupPath));
            m_prjsFileDialog.Filter = Lang.LangSys.C("SmartApp File (*.saf)|*.saf");
            m_prjsFileDialog.InitialDirectory = initDir;
            m_prjsFileDialog.CustomPlaces.Clear();
            m_prjsFileDialog.CustomPlaces.Add(new FileDialogCustomPlace(Application.StartupPath));
        }

        // <summary>
        /// affiche la boite de dialogue de sélection de projet et si l'utilisateur valide avec OK
        /// mémorise le dernier chemin utilisé et le mémorise comme chemin pour la prochaine ouverture
        /// </summary>
        /// <returns>la réponse de l'utilisateur à la boite de dialogue</returns>
        public static DialogResult ShowOpenPrjFileDilaog()
        {
            DialogResult dlgRes = m_prjoFileDialog.ShowDialog();
            if (dlgRes == DialogResult.OK)
            {
                string path = Path.GetDirectoryName(m_prjoFileDialog.FileName);
                m_prjoFileDialog.InitialDirectory = path;
                m_prjsFileDialog.InitialDirectory = path;
            }
            return dlgRes;
        }

        // <summary>
        /// affiche la boite de dialogue de sélection de projet et si l'utilisateur valide avec OK
        /// mémorise le dernier chemin utilisé et le mémorise comme chemin pour la prochaine ouverture
        /// </summary>
        /// <returns>la réponse de l'utilisateur à la boite de dialogue</returns>
        public static DialogResult ShowSavePrjFileDilaog()
        {
            DialogResult dlgRes = m_prjsFileDialog.ShowDialog();
            if (dlgRes == DialogResult.OK)
            {
                string path = Path.GetDirectoryName(m_prjsFileDialog.FileName);
                m_prjsFileDialog.InitialDirectory = path;
                m_prjoFileDialog.InitialDirectory = path;
            }
            return dlgRes;
        }

        /// <summary>
        /// renvoie le chemin de l'image sélectionné.
        /// </summary>
        public static string PrjOpenFileName
        {
            get
            {
                return m_prjoFileDialog.FileName;
            }
        }

        /// <summary>
        /// renvoie le chemin de l'image sélectionné.
        /// </summary>
        public static string PrjSaveFileName
        {
            get
            {
                return m_prjsFileDialog.FileName;
            }
        }
        #endregion
    }
}

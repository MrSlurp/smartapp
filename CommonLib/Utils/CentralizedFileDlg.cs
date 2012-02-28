using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace CommonLib
{
    public static class CentralizedFileDlg
    {
        private static OpenFileDialog m_imgFileDialog = new OpenFileDialog();

        public static void InitImgFileDialog(string initDir)
        {
            m_imgFileDialog.Filter = Lang.LangSys.C("Image Files (jpeg, gif, bmp, png)|*.jpg;*.jpeg;*.gif;*.bmp;*.png|JPEG Files(*.jpg;*.jpeg)|*.jpg;*.jpeg|GIF Files(*.gif)|*.gif|BMP Files(*.bmp)|*.bmp|PNG Files(*.png)|*.png");
            m_imgFileDialog.InitialDirectory = initDir;
            //m_imgFileDialog.CustomPlaces.Clear();
            //m_imgFileDialog.CustomPlaces.Add(new FileDialogCustomPlace(Application.StartupPath));
            //m_imgFileDialog.CustomPlaces.Add(new FileDialogCustomPlace(initDir));
        }

        public static DialogResult ShowImageFileDilaog()
        {
            DialogResult dlgRes = m_imgFileDialog.ShowDialog();
            if (dlgRes == DialogResult.OK)
            {
                string path = Path.GetDirectoryName(m_imgFileDialog.FileName);
                m_imgFileDialog.InitialDirectory = path;
            }

            return dlgRes;
        }

        public static string ImgInitialDirectory
        {
            get
            {
                return m_imgFileDialog.InitialDirectory;
            }
            set
            {
                m_imgFileDialog.InitialDirectory = value;
            }
        }

        public static string ImgFileName
        {
            get
            {
                return m_imgFileDialog.FileName;
            }
        }
    }
}

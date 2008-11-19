using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace CtrlTwoBitmap
{
    internal static class TwoImageRes
    {
        public static Bitmap DefaultImg;
        public static void InitializeBitmap()
        {
            string strAppDir = System.IO.Path.GetDirectoryName(Application.ExecutablePath);
            DefaultImg = new Bitmap(strAppDir + "\\Res\\DefaultImage.bmp");
        }
    }
}

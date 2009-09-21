using System;
using System.Drawing;
using System.Collections.Generic;
using System.Text;

namespace CommonLib
{
    public class ColorTranslate
    {
        public static string ColorToString(Color cr)
        {
            return string.Format("{0}, {1}, {2}", cr.R, cr.G, cr.B);
        }

        public static Color StringToColor(string strCr)
        {
            string[] rgbVal = strCr.Split(',');
            int r = int.Parse(rgbVal[0]);
            int g = int.Parse(rgbVal[1]);
            int b = int.Parse(rgbVal[2]);
            return Color.FromArgb(r, g, b);
        }
    }
}

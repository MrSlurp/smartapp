using System;
using System.Drawing;
using System.Collections.Generic;
using System.Text;

namespace CommonLib
{
    public class ColorTranslate
    {
        /// <summary>
        /// convertit une couleur en chaine
        /// </summary>
        /// <param name="cr">couleur</param>
        /// <returns>chaine représentant la couleur en RGB</returns>
        public static string ColorToString(Color cr)
        {
            return string.Format("{0}, {1}, {2}", cr.R, cr.G, cr.B);
        }

        /// <summary>
        /// convertit une chaine en couleur
        /// </summary>
        /// <param name="strCr">chaine RGB</param>
        /// <returns>couleur</returns>
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

/*
    This file is part of SmartApp.

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

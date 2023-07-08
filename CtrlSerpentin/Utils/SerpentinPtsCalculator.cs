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
using System.Drawing;

namespace CtrlSerpentin
{
    internal static class SerpentinPtsCalculator
    {
        private const int ECART_PT = 6;
        private const int MARGE = 2;

        public static void CalcSerpentinPts(out Point[] ListPts, Size Rect)
        {
            // les points vont de gauche à droite et doivent commencer et finir à droite
            int xPtLeft = MARGE;
            int xPtRight = Rect.Width;
            int nbPoints = Rect.Height / ECART_PT;

            // il faut un nombre de points impaire
            // (un point de moins à gauche qu'a droite)
            if ((nbPoints % 2) == 0)
            {
                nbPoints -= 1;
            }
            if (nbPoints < 0)
            {
                nbPoints = 0;
            }
            ListPts = new Point[nbPoints];
            int yCurPt = MARGE;
            bool bPtAGauche = false;
            for (int i = 0; i < ListPts.Length; i++)
            {
                
                if (bPtAGauche)
                {
                    ListPts[i].X = xPtLeft;
                    ListPts[i].Y = yCurPt;
                }
                else
                {
                    ListPts[i].X = xPtRight;
                    ListPts[i].Y = yCurPt;
                }
                bPtAGauche = !bPtAGauche;
                yCurPt += ECART_PT;
            }
        }
    }
}

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
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace DigitalDisplay
{
    internal class SevenSegmentHelper
    {
        Graphics m_graphics;
        private const int MINUS_SIGN_IDX = 10;

        // Indicates what segments are illuminated for all 10 digits
        static byte[,] m_segmentData = {{1, 1, 1, 0, 1, 1, 1}, // 0
							            {0, 0, 1, 0, 0, 1, 0}, // 1
							            {1, 0, 1, 1, 1, 0, 1}, // 2
							            {1, 0, 1, 1, 0, 1, 1}, // 3
							            {0, 1, 1, 1, 0, 1, 0}, // 4
							            {1, 1, 0, 1, 0, 1, 1}, // 5
							            {1, 1, 0, 1, 1, 1, 1}, // 6
							            {1, 0, 1, 0, 0, 1, 0}, // 7
							            {1, 1, 1, 1, 1, 1, 1}, // 8
							            {1, 1, 1, 1, 0, 1, 1}, // 9
							            {0, 0, 0, 1, 0, 0, 0}};// -

        // Points that define each of the seven segments
        readonly Point[][] m_segmentPoints = new Point[7][];

        public SevenSegmentHelper(Graphics graphics)
        {
            this.m_graphics = graphics;
            m_segmentPoints[0] = new Point[] { new Point(3, 2), new Point(39, 2), new Point(31, 10), new Point(11, 10) };
            m_segmentPoints[1] = new Point[] { new Point(2, 3), new Point(10, 11), new Point(10, 31), new Point(2, 35) };
            m_segmentPoints[2] = new Point[] { new Point(40, 3), new Point(40, 35), new Point(32, 31), new Point(32, 11) };
            m_segmentPoints[3] = new Point[] { new Point(3, 36), new Point(11, 32), new Point(31, 32), new Point(39, 36), new Point(31, 40), new Point(11, 40) };
            m_segmentPoints[4] = new Point[] { new Point(2, 37), new Point(10, 41), new Point(10, 61), new Point(2, 69) };
            m_segmentPoints[5] = new Point[] { new Point(40, 37), new Point(40, 69), new Point(32, 61), new Point(32, 41) };
            m_segmentPoints[6] = new Point[] { new Point(11, 62), new Point(31, 62), new Point(39, 70), new Point(3, 70) };
        }

        public SizeF GetStringSize(string text, Font font)
        {
            SizeF sizef = new SizeF(0, m_graphics.DpiX * font.SizeInPoints / 72);

            for (int i = 0; i < text.Length; i++)
            {
                if (Char.IsDigit(text[i]) || text[i] == '-')
                    sizef.Width += 42 * m_graphics.DpiX * font.SizeInPoints / 72 / 72;
                else if (text[i] == ':' || text[i] == '.' || text[i] == ',')
                    sizef.Width += 12 * m_graphics.DpiX * font.SizeInPoints / 72 / 72;
            }
            return sizef;
        }

        public void DrawDigits(string text, Font font, Brush brush, Brush brushLight, float x, float y)
        {
            for (int cnt = 0; cnt < text.Length; cnt++)
            {
                // For digits 0-9
                if (Char.IsDigit(text[cnt]))
                    x = DrawDigit(text[cnt] - '0', font, brush, brushLight, x, y);
                // For colon :
                else if (text[cnt] == ':')
                    x = DrawColon(font, brush, x, y);
                // For dot .
                else if (text[cnt] == '.')
                    x = DrawDot(font, brush, x, y);
                else if (text[cnt] == ',')
                    x = DrawDot(font, brush, x, y);
                else if (text[cnt] == '-')
                    x = DrawDigit(MINUS_SIGN_IDX, font, brush, brushLight, x, y);
            }
        }

        private float DrawDigit(int num, Font font, Brush brush, Brush brushLight, float x, float y)
        {
            for (int cnt = 0; cnt < m_segmentPoints.Length; cnt++)
            {
                if (m_segmentData[num, cnt] == 1)
                {
                    FillPolygon(m_segmentPoints[cnt], font, brush, x, y);
                }
                else
                {
                    FillPolygon(m_segmentPoints[cnt], font, brushLight, x, y);
                }
            }
            return x + 42 * m_graphics.DpiX * font.SizeInPoints / 72 / 72;
        }

        private float DrawDot(Font font, Brush brush, float x, float y)
        {
            Point[][] dotPoints = new Point[1][];

            dotPoints[0] = new Point[] {new Point( 2, 64), new Point( 6, 61),
                                new Point(10, 64), new Point( 6, 69)};

            for (int cnt = 0; cnt < dotPoints.Length; cnt++)
            {
                FillPolygon(dotPoints[cnt], font, brush, x, y);
            }
            return x + 12 * m_graphics.DpiX * font.SizeInPoints / 72 / 72;
        }

        private float DrawColon(Font font, Brush brush, float x, float y)
        {
            Point[][] colonPoints = new Point[2][];
            colonPoints[0] = new Point[] { new Point(2, 21), new Point(6, 17), new Point(10, 21), new Point(6, 25) };
            colonPoints[1] = new Point[] { new Point(2, 51), new Point(6, 47), new Point(10, 51), new Point(6, 55) };

            for (int cnt = 0; cnt < colonPoints.Length; cnt++)
            {
                FillPolygon(colonPoints[cnt], font, brush, x, y);
            }
            return x + 12 * m_graphics.DpiX * font.SizeInPoints / 72 / 72;
        }

        private void FillPolygon(Point[] polygonPoints, Font font, Brush brush, float x, float y)
        {
            PointF[] polygonPointsF = new PointF[polygonPoints.Length];

            for (int cnt = 0; cnt < polygonPoints.Length; cnt++)
            {
                polygonPointsF[cnt].X = x + polygonPoints[cnt].X * m_graphics.DpiX * font.SizeInPoints / 72 / 72;
                polygonPointsF[cnt].Y = y + polygonPoints[cnt].Y * m_graphics.DpiY * font.SizeInPoints / 72 / 72;
            }
            m_graphics.FillPolygon(brush, polygonPointsF);
        }
    }
}

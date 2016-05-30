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
using System.Windows.Forms;
using CommonLib;

namespace CtrlSerpentin
{
    internal class SerpentinControl : BTSerpentinControl
    {
        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public SerpentinControl(BTDoc document)
            : base(document)
        {

        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public override void CreateControl()
        {
            if (m_Ctrl == null)
            {
                m_Ctrl = new Serpentin();
                m_Ctrl.Location = m_RectControl.Location;
                m_Ctrl.Name = m_strSymbol;
                m_Ctrl.Size = m_RectControl.Size;
                m_Ctrl.BackColor = Color.Transparent;
                ((Serpentin)m_Ctrl).ColorActive = ((TwoColorProp)SpecificProp).ColorActive;
                ((Serpentin)m_Ctrl).ColorInactive = ((TwoColorProp)SpecificProp).ColorInactive;
                ((Serpentin)m_Ctrl).InitSerpentinDraw();
                UpdateFromData();
            }
        }

        public override void OnControlEvent(object Sender, EventArgs Args)
        {
            return;
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public override void UpdateFromData()
        {
            if (m_AssociateData != null && m_Ctrl != null)
            {
                if (m_AssociateData.Value != 0)
                    ((Serpentin)m_Ctrl).IsActive = true;
                else
                    ((Serpentin)m_Ctrl).IsActive = false;
            }
        }
    }

    public class Serpentin : UserControl
    {
        private Color m_ColorInactive = Color.Black;
        private Color m_ColorActive = Color.Blue;

        private bool m_bIsActive = false;

        private Point[] m_ListPoint = new Point[1];
        public Serpentin()
        {
            this.DoubleBuffered = true;
        }

        public bool IsActive
        {
            get
            {
                return m_bIsActive;
            }
            set
            {
                m_bIsActive = value;
                Refresh();
            }
        }

        public Color ColorInactive
        {
            get
            {
                return m_ColorInactive;
            }
            set
            {
                m_ColorInactive = value;
            }
        }

        public Color ColorActive
        {
            get
            {
                return m_ColorActive;
            }
            set
            {
                m_ColorActive = value;
            }
        }

        public void InitSerpentinDraw()
        {
            SerpentinPtsCalculator.CalcSerpentinPts(out m_ListPoint, this.Size);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Color UsedColor = m_bIsActive ? this.m_ColorActive : this.m_ColorInactive;
            Pen pn = new Pen(UsedColor, 4);
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.DrawLines(pn, m_ListPoint);
            pn.Dispose();
        }
    }
}

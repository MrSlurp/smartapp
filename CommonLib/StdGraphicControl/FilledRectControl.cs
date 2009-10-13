using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace CommonLib
{
    public class FilledRectControl : BTFilledRectControl
    {
        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public FilledRectControl()
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
                m_Ctrl = new FilledRect();
                m_Ctrl.Location = m_RectControl.Location;
                m_Ctrl.Name = m_strSymbol;
                m_Ctrl.Size = m_RectControl.Size;
                ((FilledRect)m_Ctrl).ColorActive = ((TwoColorProp)SpecificProp).ColorActive;
                ((FilledRect)m_Ctrl).ColorInactive = ((TwoColorProp)SpecificProp).ColorInactive;
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
                    ((FilledRect)m_Ctrl).IsActive = true;
                else
                    ((FilledRect)m_Ctrl).IsActive = false;

                m_Ctrl.Refresh();
            }
        }
    }

    public class FilledRect : Control
    {
        private Color m_ColorInactive = Color.Black;
        private Color m_ColorActive = Color.Blue;

        private bool m_bIsActive = false;

        public FilledRect()
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

        protected override void OnPaint(PaintEventArgs e)
        {
            Color UsedColor = m_bIsActive ? this.m_ColorActive : this.m_ColorInactive;
            SolidBrush br = new SolidBrush(UsedColor);
            e.Graphics.FillRectangle(br, 0, 0, this.Width, this.Height);
            base.OnPaint(e);
        }
    }
}

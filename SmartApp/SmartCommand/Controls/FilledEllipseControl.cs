using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using SmartApp.Datas;

namespace SmartApp.Controls
{
    public class FilledEllipseControl : BTFilledEllipseControl
    {
        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public FilledEllipseControl()
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
                m_Ctrl = new FilledEllipse();
                m_Ctrl.Location = m_RectControl.Location;
                m_Ctrl.Name = m_strSymbol;
                m_Ctrl.Size = m_RectControl.Size;
                m_Ctrl.BackColor = Color.Transparent;
                ((FilledEllipse)m_Ctrl).ColorActive = ((TwoColorProp)SpecificProp).ColorActive;
                ((FilledEllipse)m_Ctrl).ColorInactive = ((TwoColorProp)SpecificProp).ColorInactive;
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
                    ((FilledEllipse)m_Ctrl).IsActive = true;
                else
                    ((FilledEllipse)m_Ctrl).IsActive = false;

                m_Ctrl.Refresh();
            }
        }
    }

    public class FilledEllipse : UserControl
    {
        private Color m_ColorInactive = Color.Black;
        private Color m_ColorActive = Color.Blue;

        private bool m_bIsActive = false;

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
            e.Graphics.FillEllipse(br, 0, 0, this.Width, this.Height);
            base.OnPaint(e);
        }
    }
}

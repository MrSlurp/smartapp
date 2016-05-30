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
using System.ComponentModel;
using System.IO;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Data;
using System.Text;
using System.Windows.Forms;
using CommonLib;

namespace CtrlDemux
{
    public partial class InteractiveCtrlDemuxDllControl : InteractiveControl, ISpecificControl
    {
        static UserControl m_SpecificPropPanel = new CtrlDemuxProperties();
        StandardPropEnabling m_stdPropEnabling = new StandardPropEnabling();
        SpecificGraphicProp m_SpecGraphicProp = new SpecificGraphicProp();

        public InteractiveCtrlDemuxDllControl()
        {
            InitializeComponent();
            m_SpecificPropPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            m_SpecificPropPanel.AutoSize = true;
            m_SpecificPropPanel.Name = "DllControlPropPanel";
            m_SpecificPropPanel.Text = "";

            // modifiez ici les valeur afin que le control rende disponibles les champs standard souhaités
            m_stdPropEnabling.m_bcheckReadOnlyChecked = false;
            m_stdPropEnabling.m_bcheckReadOnlyEnabled = false;
            m_stdPropEnabling.m_bcheckScreenEventEnabled = false;
            m_stdPropEnabling.m_bcheckScreenEventChecked = false;
            m_stdPropEnabling.m_bEditAssociateDataEnabled = false;
            m_stdPropEnabling.m_bEditTextEnabled = false;
            m_stdPropEnabling.m_bCtrlEventScriptEnabled = false;

            // modifiez ici les valeur afin que le control ai la taille min souhaité et ses possibilité de redimensionnement
            m_SpecGraphicProp.m_bcanResizeWidth = false;
            m_SpecGraphicProp.m_bcanResizeHeight = false;
            m_SpecGraphicProp.m_MinSize = new Size(70, 30);
            m_SpecGraphicProp.m_MaxSize = new Size(70, 30);
            this.ControlType = InteractiveControlType.DllControl;
        }

        public override InteractiveControl CreateNew()
        {
            return new InteractiveCtrlDemuxDllControl();
        }

        //*****************************************************************************************************
        // Description: accesseur pour le type, modifie atomatiquement les propriété de redimensionement et 
        // la taille du control quand le type change
        //*****************************************************************************************************
        public override InteractiveControlType ControlType
        {
            get
            {
                return InteractiveControlType.DllControl;
            }
        }

        public UserControl SpecificPropPanel
        {
            get
            {
                return m_SpecificPropPanel;
            }
        }

        public StandardPropEnabling StdPropEnabling
        {
            get
            {
                return m_stdPropEnabling;
            }
        }

        public SpecificGraphicProp SpecGraphicProp
        {
            get
            {
                return m_SpecGraphicProp;
            }
        }

        public void SelfPaint(Graphics gr, Control ctrl)
        {
            SizeF SizeText = gr.MeasureString("Demux", SystemFonts.DefaultFont);
            PointF PtText = new PointF(ctrl.ClientRectangle.Left + 2, (ctrl.Height - SizeText.Height) / 2);
            gr.DrawString("Demux", SystemFonts.DefaultFont, Brushes.Black, PtText);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            SelfPaint(e.Graphics, this);
            ControlPainter.DrawPresenceAssociateData(e.Graphics, this);
            DrawSelRect(e.Graphics);
        }

        public override bool IsDllControl
        {
            get
            {
                return true;
            }
        }

        public override uint DllControlID
        {
            get
            {
                return CtrlDemux.DllEntryClass.DLL_Control_ID;
            }
        }

    }
}

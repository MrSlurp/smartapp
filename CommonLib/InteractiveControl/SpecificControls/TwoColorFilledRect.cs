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
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace CommonLib
{
    public partial class TwoColorFilledRect : InteractiveControl, ISpecificControl
    {
        static UserControl m_SpecificPropPanel = new TwoColorProperties();
        StandardPropEnabling m_stdPropEnabling = new StandardPropEnabling();
        SpecificGraphicProp m_SpecGraphicProp = new SpecificGraphicProp();

        /// <summary>
        /// constructeur par défaut
        /// </summary>
        public TwoColorFilledRect()
        {
            InitializeComponent();
            m_SpecificPropPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            m_SpecificPropPanel.AutoSize = true;
            m_SpecificPropPanel.Name = "TwoColorFilledRectPorpPanel";
            m_SpecificPropPanel.Text = "";

            m_stdPropEnabling.m_bcheckReadOnlyChecked = false;
            m_stdPropEnabling.m_bcheckReadOnlyEnabled = false;
            m_stdPropEnabling.m_bcheckScreenEventEnabled = false;
            m_stdPropEnabling.m_bcheckScreenEventChecked= false;
            m_stdPropEnabling.m_bEditAssociateDataEnabled = true;
            m_stdPropEnabling.m_bEditTextEnabled = false;
            m_stdPropEnabling.m_bCtrlEventScriptEnabled = false;

            m_SpecGraphicProp.m_bcanResizeWidth = true;
            m_SpecGraphicProp.m_bcanResizeHeight = true;
            m_SpecGraphicProp.m_MinSize = new Size(5, 5);
            m_TypeControl = InteractiveControlType.SpecificControl;
            OnTypeControlChanged();

        }

        /// <summary>
        /// fonction de création sans constructeur
        /// </summary>
        /// <returns>objet TwoColorFilledRect</returns>
        public override InteractiveControl CreateNew()
        {
            return new TwoColorFilledRect();
        }

        /// <summary>
        /// accesseur pour le type, modifie atomatiquement les propriété de redimensionement et 
        /// la taille du control quand le type change
        /// </summary>
        public override InteractiveControlType ControlType
        {
            get
            {
                return InteractiveControlType.SpecificControl;
            }
            set
            {
                // pour les controles spécifiques et DLL, ce type ne peut être changé
            }
        }

        /// <summary>
        /// accesseur vers le panneau des propriété spécifique
        /// </summary>
        public UserControl SpecificPropPanel
        {
            get
            {
                return m_SpecificPropPanel;
            }
        }

        /// <summary>
        /// accesseur sur les propriété d'activation des paramètres standards
        /// </summary>
        public StandardPropEnabling StdPropEnabling
        {
            get
            {
                return m_stdPropEnabling;
            }
        }

        /// <summary>
        /// accesseur sur les propriété de comportement graphique des controles spécifiques et DLL
        /// </summary>
        public SpecificGraphicProp SpecGraphicProp 
        {
            get
            {
                return m_SpecGraphicProp;
            }
        }

        /// <summary>
        /// fonction de dessin personelle
        /// </summary>
        /// <param name="gr">graphics du control</param>
        /// <param name="ctrl">objet control</param>
        public void SelfPaint(Graphics gr, Control ctrl)
        {
            Rectangle DrawRect = ctrl.ClientRectangle;
            Color crbr = Color.Black;
            if (SourceBTControl != null)
                crbr = ((TwoColorProp)SourceBTControl.SpecificProp).ColorInactive;
            else
                crbr = Color.Blue;
            Brush br = new SolidBrush(crbr);
            gr.FillRectangle(br, DrawRect);
            ControlPainter.DrawPresenceAssociateData(gr, ctrl);
            br.Dispose();
        }

        /// <summary>
        /// surcharge de l'évènement OnPaint
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            SelfPaint(e.Graphics, this);
            DrawSelRect(e.Graphics);
        }

    }
}

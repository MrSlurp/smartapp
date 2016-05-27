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

namespace CtrlDataTrigger
{
    public partial class InteractiveCtrlDataTriggerDllControl : InteractiveControl, ISpecificControl
    {
        static UserControl m_SpecificPropPanel = new CtrlDataTriggerProperties();
        StandardPropEnabling m_stdPropEnabling = new StandardPropEnabling();
        SpecificGraphicProp m_SpecGraphicProp = new SpecificGraphicProp();

        public InteractiveCtrlDataTriggerDllControl()
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
            m_stdPropEnabling.m_bEditAssociateDataEnabled = true;
            m_stdPropEnabling.m_bEditTextEnabled = false;
            m_stdPropEnabling.m_bCtrlEventScriptEnabled = true;

            // modifiez ici les valeur afin que le control ai la taille min souhaité et ses possibilité de redimensionnement
            m_SpecGraphicProp.m_bcanResizeWidth = false;
            m_SpecGraphicProp.m_bcanResizeHeight = false;
            m_SpecGraphicProp.m_MinSize = new Size(70, 30);
            m_SpecGraphicProp.m_MaxSize = new Size(70, 30);

            this.ControlType = InteractiveControlType.DllControl;

            InitializeComponent();
        }

        /// <summary>
        /// appelé lorsqu'un control est dropé dans la surface de dessin
        /// crée un nouveau control du même type que celui dropé
        /// </summary>
        /// <returns>un nouveau control du type courant</returns>
        public override InteractiveControl CreateNew()
        {
            return new InteractiveCtrlDataTriggerDllControl();
        }

        /// <summary>
        /// Surcharge de la classe de base, dans le cas des template on est toujours de type DLL 
        /// </summary>
        public override InteractiveControlType ControlType
        {
            get
            {
                return InteractiveControlType.DllControl;
            }
        }

        /// <summary>
        /// accesseur vers le panneau des propriété spécifiques (ReadOnly)
        /// </summary>
        public UserControl SpecificPropPanel
        {
            get
            {
                return m_SpecificPropPanel;
            }
        }

        /// <summary>
        /// accesseur vers les propriété d'activation standard (ReadOnly)
        /// </summary>
        public StandardPropEnabling StdPropEnabling
        {
            get
            {
                return m_stdPropEnabling;
            }
        }

        /// <summary>
        /// accesseur vers les propriété spécifiques (ReadOnly)
        /// </summary>
        public SpecificGraphicProp SpecGraphicProp
        {
            get
            {
                return m_SpecGraphicProp;
            }
        }

        public void SelfPaint(Graphics gr, Control ctrl)
        {
            SizeF SizeText = gr.MeasureString("DataTrigger", SystemFonts.DefaultFont);
            PointF PtText = new PointF(ctrl.ClientRectangle.Left + 2, (ctrl.Height - SizeText.Height) / 2);
            gr.DrawString("DataTrigger", SystemFonts.DefaultFont, Brushes.Black, PtText);
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
                return CtrlDataTrigger.DllEntryClass.DLL_Control_ID;
            }
        }

    }
}

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
using ZedGraph;

namespace CtrlGraph
{
    public partial class InteractiveCtrlGraphDllControl : InteractiveControl, ISpecificControl
    {
        static UserControl m_SpecificPropPanel = new CtrlGraphProperties();
        StandardPropEnabling m_stdPropEnabling = new StandardPropEnabling();
        SpecificGraphicProp m_SpecGraphicProp = new SpecificGraphicProp();

        private Bitmap m_BmpDefault = CtrlGraphRes.DefaultBmp;
        public InteractiveCtrlGraphDllControl()
        {
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
            m_SpecGraphicProp.m_bcanResizeWidth = true;
            m_SpecGraphicProp.m_bcanResizeHeight = true;
            m_SpecGraphicProp.m_MinSize = new Size(250, 200);
            m_SpecGraphicProp.m_MaxSize = new Size(1680, 1050);

            this.ControlType = InteractiveControlType.DllControl;
            InitializeComponent();
        }

        public override InteractiveControl CreateNew()
        {
            return new InteractiveCtrlGraphDllControl();
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
            // dans tous les cas on dessine le fond
            if (this.SourceBTControl != null)
            {
                zedGraphControl.Visible = true;
                // si il y a un controle source, on récupère les propriété pour afficher 
                // le nom des variables
                // et l'étoile données associes
                
            }
            else
            {
                zedGraphControl.Visible = false;
                gr.DrawImage(m_BmpDefault, new Rectangle(new Point(0, 0), this.Size));
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            SelfPaint(e.Graphics, this);
            //ControlPainter.DrawPresenceAssociateData(e.Graphics, this);
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
                return DllEntryClass.DLL_Control_ID;
            }
        }

    }
}

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

namespace FourBitmap
{
    public partial class InteractiveFourBitmapDllControl : InteractiveControl, ISpecificControl
    {
        UserControl m_SpecificPropPanel = new FourBitmapProperties();
        StandardPropEnabling m_stdPropEnabling = new StandardPropEnabling();
        SpecificGraphicProp m_SpecGraphicProp = new SpecificGraphicProp();

        private string m_strImg0 = "";
        private Bitmap m_Bmp0;
        private Bitmap m_BmpDefault = FourBitmapRes.DefaultBmp;
        public InteractiveFourBitmapDllControl()
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
            m_stdPropEnabling.m_bCtrlEventScriptEnabled = false;

            // modifiez ici les valeur afin que le control ai la taille min souhaité et ses possibilité de redimensionnement
            m_SpecGraphicProp.m_bcanResizeWidth = true;
            m_SpecGraphicProp.m_bcanResizeHeight = false;
            m_SpecGraphicProp.m_MinSize = new Size(5, 5);
            this.ControlType = InteractiveControlType.DllControl;
        }

        public override InteractiveControl CreateNew()
        {
            return new InteractiveFourBitmapDllControl();
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
            if (this.SourceBTControl != null)
            {
                try
                {
                    if (((DllFourBitmapProp)this.SourceBTControl.SpecificProp).NomFichier0 != m_strImg0)
                    {
                        m_strImg0 = PathTranslator.RelativePathToAbsolute(((DllFourBitmapProp)this.SourceBTControl.SpecificProp).NomFichier0);
                        m_strImg0 = PathTranslator.LinuxVsWindowsPathUse(m_strImg0);
                        string strImageFullPath = m_strImg0;
                        m_Bmp0 = new Bitmap(strImageFullPath);
                        m_Bmp0.MakeTransparent(Color.Magenta);
                    }
                    gr.DrawImage(m_Bmp0, new Rectangle(new Point(0, 0), this.Size));
                    return;
                }
                catch (Exception)
                {
                }
            }
            gr.DrawImage(m_BmpDefault, new Rectangle(new Point(0, 0), this.Size));
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
                return FourBitmap.DllEntryClass.DLL_Control_ID;
            }
        }

    }
}

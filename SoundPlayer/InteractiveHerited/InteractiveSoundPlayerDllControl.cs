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

namespace SoundPlayer
{
    /// <summary>
    /// classe représentant l'objet affiché dans le designer de supervision
    /// </summary>
    public partial class InteractiveSoundPlayerDllControl : InteractiveControl, ISpecificControl
    {
        // panneau des propriété de l'objet
        UserControl m_SpecificPropPanel = new SoundPlayerProperties();
        // proriétés d'activation des paramètres standard des controls
        StandardPropEnabling m_stdPropEnabling = new StandardPropEnabling();
        // propriété de comportement de l'objet dans le designer
        SpecificGraphicProp m_SpecGraphicProp = new SpecificGraphicProp();

        /// <summary>
        /// Constructeur de la classe
        /// </summary>
        public InteractiveSoundPlayerDllControl()
        {
            // initialisation du panneau des propriété spécifiques
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
            m_SpecGraphicProp.m_bcanResizeHeight = true;
            m_SpecGraphicProp.m_MinSize = new Size(250, 200);
            m_SpecGraphicProp.m_MaxSize = new Size(1680, 1050);
            // Ne jamais supprimer cette ligne, provoque la prise en compte de tout ce qui est définit précédement
            // dans cette fonction
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
            return new InteractiveSoundPlayerDllControl();
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
            if (this.SourceBTControl != null)
            {
                // mettez ici le code de dessin du control lorsqu'il est posé dans la surface de dessin
            }
            else
            {
                // mettez ici le code de dessin du control lorsqu'il est dans la barre d'outil
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            SelfPaint(e.Graphics, this);
            // dessine l'étoile représentant la présence d'une donnée associée
            ControlPainter.DrawPresenceAssociateData(e.Graphics, this);
            // existe une version ou il est possible de passer le paramètre indquant l'affichage de 
            // l'étoile dont le prototype est le suivant
            // à utiliser si la donnée associée standard n'est pas utilisée
            //ControlPainter.DrawPresenceAssociateData(Graphics gr, Control ctrl, bool AssocOK)
            DrawSelRect(e.Graphics);
        }

        /// <summary>
        /// Surcharge de la classe de base, indique que c'est un plugin DLL
        /// </summary>
        public override bool IsDllControl
        {
            get
            {
                return true;
            }
        }

        /// <summary>
        /// Renvoie l'ID de la DLL
        /// </summary>
        public override uint DllControlID
        {
            get
            {
                return SoundPlayer.DllEntryClass.DLL_Control_ID;
            }
        }

    }
}

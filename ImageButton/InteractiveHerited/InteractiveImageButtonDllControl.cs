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
using System.IO;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Data;
using System.Text;
using System.Windows.Forms;
using CommonLib;

namespace ImageButton
{
    /// <summary>
    /// classe représentant l'objet affiché dans le designer de supervision
    /// </summary>
    public partial class InteractiveImageButtonDllControl : InteractiveControl, ISpecificControl
    {
        // panneau des propriété de l'objet
        static UserControl m_SpecificPropPanel = new ImageButtonProperties();
        // proriétés d'activation des paramètres standard des controls
        StandardPropEnabling m_stdPropEnabling = new StandardPropEnabling();
        // propriété de comportement de l'objet dans le designer
        SpecificGraphicProp m_SpecGraphicProp = new SpecificGraphicProp();

        string m_strReleasedImage;
        /// <summary>
        /// Constructeur de la classe
        /// </summary>
        public InteractiveImageButtonDllControl()
        {
            // initialisation du panneau des propriété spécifiques
            m_SpecificPropPanel.Name = "DllControlPropPanel";
            m_SpecificPropPanel.Text = "";

            // modifiez ici les valeur afin que le control rende disponibles les champs standard souhaités
            m_stdPropEnabling.m_bcheckReadOnlyChecked = false;
            m_stdPropEnabling.m_bcheckReadOnlyEnabled = false;
            m_stdPropEnabling.m_bcheckScreenEventEnabled = true;
            m_stdPropEnabling.m_bcheckScreenEventChecked = false;
            m_stdPropEnabling.m_bEditAssociateDataEnabled = true;
            m_stdPropEnabling.m_bEditTextEnabled = true;
            m_stdPropEnabling.m_bCtrlEventScriptEnabled = true;
            m_stdPropEnabling.m_bSelectFontEnabled = true;

            // modifiez ici les valeur afin que le control ai la taille min souhaité et ses possibilité de redimensionnement
            m_SpecGraphicProp.m_bcanResizeWidth = true;
            m_SpecGraphicProp.m_bcanResizeHeight = true;
            m_SpecGraphicProp.m_MinSize = new Size(15, 15);
            m_SpecGraphicProp.m_MaxSize = new Size(1680, 1050);
            // Ne jamais supprimer cette ligne, provoque la prise en compte de tout ce qui est définit précédement
            // dans cette fonction
            this.ControlType = InteractiveControlType.DllControl;

            InitializeComponent();
            this.imageButtonDispCtrl1.Enabled = false;
            imageButtonDispCtrl1.Paint += new PaintEventHandler(imageButtonDispCtrl1_Paint);
        }

        /// <summary>
        /// appelé lorsqu'un control est dropé dans la surface de dessin
        /// crée un nouveau control du même type que celui dropé
        /// </summary>
        /// <returns>un nouveau control du type courant</returns>
        public override InteractiveControl CreateNew()
        {
            return new InteractiveImageButtonDllControl();
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
                try
                {
                    DllImageButtonProp SpecProps = (DllImageButtonProp)this.SourceBTControl.SpecificProp;
                    if (SpecProps.ReleasedImage != m_strReleasedImage)
                    {
                        m_strReleasedImage = SpecProps.ReleasedImage;
                        string sReleasedImage = SourceBTControl.Document.PathTr.RelativePathToAbsolute(m_strReleasedImage);
                        sReleasedImage = PathTranslator.LinuxVsWindowsPathUse(sReleasedImage);
                        Bitmap bmp = new Bitmap(sReleasedImage);
                        bmp.MakeTransparent(Color.Magenta);
                        this.imageButtonDispCtrl1.BackgroundImage = bmp;
                    }
                    else if (string.IsNullOrEmpty(SpecProps.ReleasedImage)
                        && this.imageButtonDispCtrl1.BackgroundImage != ImageButtonRes.DefaultImg)
                    {
                        this.imageButtonDispCtrl1.BackgroundImage = ImageButtonRes.DefaultImg;
                    }
                    if (SpecProps.BorderSize != this.imageButtonDispCtrl1.FlatAppearance.BorderSize)
                    {
                        this.imageButtonDispCtrl1.FlatAppearance.BorderSize = SpecProps.BorderSize;
                    }
                    if (SpecProps.Style != this.imageButtonDispCtrl1.FlatStyle)
                    {
                        this.imageButtonDispCtrl1.FlatStyle = SpecProps.Style;
                    }
                }
                catch (Exception )
                {
                }
                if (this.imageButtonDispCtrl1.Text != this.Text)
                {
                    this.imageButtonDispCtrl1.Text = this.Text;
                }
            }
            else
            {
                if (this.imageButtonDispCtrl1.BackgroundImage != ImageButtonRes.DefaultImg)
                    this.imageButtonDispCtrl1.BackgroundImage = ImageButtonRes.DefaultImg;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            // met a jour les attributs du bouton standard pour l'image de fond et le style
            SelfPaint(e.Graphics, this);
            //Comme on utilise un controle standard inclu dans l'interactive control, on capte son évènement de dessin et c'est sur
            // lui qu'on effectue les dessins de la présence de donnée associée
        }

        void imageButtonDispCtrl1_Paint(object sender, PaintEventArgs e)
        {
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
                return ImageButton.DllEntryClass.DLL_Control_ID;
            }
        }

    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace SmartApp.Ihm.Designer
{
    //positions possible des bouttons de redimensionnement
    public enum ResizeButtonPosition
    {
        MiddleRight,
        BottomCenter,
        BottomRight
    }

    public partial class ResizeButton : Panel
    {
        #region données membres
        //position du bouton de redimenssionement
        private ResizeButtonPosition resizeButtonPosition;
        //contrôle à redimenssioner
        private IInteractive interactiveControl = null;
        //variable qui va enregistrer la position du point au premier clic
        private Point clickPoint = new Point();
        //est-ce que le bouton gauche est appuyée?
        private bool isPressed = false;
        bool m_bCanBeVisible = true;
        #endregion

        #region attributes
        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public bool CanBeVisible
        {
            get
            {
                return m_bCanBeVisible;
            }
            set
            {
                m_bCanBeVisible = value;
                if (!m_bCanBeVisible)
                    this.ShowControl(false);
            }
        }
        #endregion

        #region Constructeur
        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public ResizeButton(ResizeButtonPosition resizeButtonPosition, IInteractive interactiveControl)
        {
            InitializeComponent();
            this.resizeButtonPosition = resizeButtonPosition;
            this.interactiveControl = interactiveControl;
            this.SetCursor();
        }
        #endregion

        #region Mouse Events
        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                clickPoint.X = e.X;
                clickPoint.Y = e.Y;
                isPressed = true;
            }
            base.OnMouseDown(e);
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left) isPressed = false;
            base.OnMouseUp(e);
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (isPressed)
            {
                //on calcule le déplacement
                Point mouseMove = new Point(e.X - clickPoint.X, e.Y - clickPoint.Y);
                //variable temporaire utilisé dans le cas où il faut appeler deux fonctions
                //afin d'achever l'effet du redimensionnement
                switch (this.resizeButtonPosition)
                {
                    #region Middle
                    case (ResizeButtonPosition.MiddleRight):
                        mouseMove.Y = 0;
                        interactiveControl.UpdateSize(mouseMove);
                        break;
                    #endregion
                    //------------------------------------------------
                    #region Bottom
                    case (ResizeButtonPosition.BottomCenter):
                        mouseMove.X = 0;
                        interactiveControl.UpdateSize(mouseMove);
                        break;
                    case (ResizeButtonPosition.BottomRight):
                        interactiveControl.UpdateSize(mouseMove);
                        break;
                    #endregion
                }
            }
            base.OnMouseMove(e);
        }
        #endregion

        #region UpdateLocation
        //cette fonction met à jour la position des boutons de redimenssionement
        //en fonction du boutton à redimenssioner
        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public bool UpdateLocation()
        {
            if (interactiveControl == null) return false;
            switch (this.resizeButtonPosition)
            {
                //------------------------------------------------
                #region Middle
                case (ResizeButtonPosition.MiddleRight):
                    Left = interactiveControl.Width - Width / 2 + interactiveControl.Left;
                    Top = interactiveControl.Height / 2 - Height / 2 + interactiveControl.Top;
                    break;
                #endregion
                //------------------------------------------------
                #region Bottom
                case (ResizeButtonPosition.BottomCenter):
                    Left = interactiveControl.Width / 2 - Width / 2 + interactiveControl.Left;
                    Top = interactiveControl.Height - Height / 2 + interactiveControl.Top;
                    break;
                case (ResizeButtonPosition.BottomRight):
                    Left = interactiveControl.Width - Width / 2 + interactiveControl.Left;
                    Top = interactiveControl.Height - Height / 2 + interactiveControl.Top;
                    break;
                #endregion
            }
            return true;
        }
        #endregion

        #region SetCursor
        //cette fonction ajuste le curseur du bouton de redimenssionement
        //selon sa position
        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public bool SetCursor()
        {
            switch (this.resizeButtonPosition)
            {
                case (ResizeButtonPosition.BottomRight):
                    Cursor = Cursors.SizeNWSE;
                    break;
                case (ResizeButtonPosition.BottomCenter):
                    Cursor = Cursors.SizeNS;
                    break;
                case (ResizeButtonPosition.MiddleRight):
                    Cursor = Cursors.SizeWE;
                    break;
            }
            return true;
        }
        #endregion

        #region ShowControl
        //cette fonction affiche le contrôle
        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public void ShowControl(bool show)
        {
            if (!show)
                this.Visible = false;
            else
            {
                if (UpdateLocation())
                    this.Visible = true && m_bCanBeVisible;
            }
        }
        #endregion

        #region Code généré par le Concepteur de composants
        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // ResizeButton
            // 
            this.BackColor = System.Drawing.Color.White;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.Size = new System.Drawing.Size(7, 7);
            this.Visible = false;
            this.ResumeLayout(false);

        }
        #endregion
    }
}

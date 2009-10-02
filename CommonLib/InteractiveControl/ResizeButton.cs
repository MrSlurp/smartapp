using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace CommonLib
{
    /// <summary>
    /// positions possible des bouttons de redimensionnement
    /// </summary>
    public enum ResizeButtonPosition
    {
        MiddleRight,
        BottomCenter,
        BottomRight,
        MiddleLeft,
        TopCenter,
    }

    public class ResizeButton : Panel
    {
        #region donn�es membres
        //position du bouton de redimenssionement
        private ResizeButtonPosition resizeButtonPosition;
        //contr�le � redimenssioner
        private IInteractive interactiveControl = null;
        //variable qui va enregistrer la position du point au premier clic
        private Point clickPoint = new Point();
        //est-ce que le bouton gauche est appuy�e?
        private bool isPressed = false;
        bool m_bCanBeVisible = true;
        #endregion

        #region attributes
        /// <summary>
        /// assigne ou obtiens la l'autrorisation d'affichage du control
        /// </summary>
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
        /// <summary>
        /// constructeur
        /// </summary>
        /// <param name="resizeButtonPosition">position du bouton</param>
        /// <param name="interactiveControl">control interactif associ�</param>
        public ResizeButton(ResizeButtonPosition resizeButtonPosition, IInteractive interactiveControl)
        {
            InitializeComponent();
            this.resizeButtonPosition = resizeButtonPosition;
            this.interactiveControl = interactiveControl;
            this.SetCursor();
        }
        #endregion

        #region Mouse Events
        /// <summary>
        /// �v�nement lors que le move button est enfonc�
        /// </summary>
        /// <param name="e"></param>
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

        /// <summary>
        /// �v�nement lors que le move button est relach�
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left) isPressed = false;
            base.OnMouseUp(e);
        }

        /// <summary>
        /// �v�nement appel� lorsque le curseur est d�plac�
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (isPressed)
            {
                //on calcule le d�placement
                Point mouseMove = new Point(e.X - clickPoint.X, e.Y - clickPoint.Y);
                //variable temporaire utilis� dans le cas o� il faut appeler deux fonctions
                //afin d'achever l'effet du redimensionnement
                switch (this.resizeButtonPosition)
                {
                    #region Top
                    case (ResizeButtonPosition.TopCenter):
                        mouseMove.X = 0;
                        interactiveControl.UpdateSizeAndLocation(mouseMove);
                        break;
                    #endregion
                    #region Middle
                    case (ResizeButtonPosition.MiddleRight):
                        mouseMove.Y = 0;
                        interactiveControl.UpdateSize(mouseMove);
                        break;
                    case (ResizeButtonPosition.MiddleLeft):
                        mouseMove.Y = 0;
                        interactiveControl.UpdateSizeAndLocation(mouseMove);
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
        /// <summary>
        /// cette fonction met � jour la position du contr�le en fonction du control � bouger
        /// </summary>
        /// <returns></returns>
        public bool UpdateLocation()
        {
            if (interactiveControl == null) return false;
            switch (this.resizeButtonPosition)
            {
                #region Top
                case (ResizeButtonPosition.TopCenter):
                    Left = interactiveControl.Width / 2 - Width / 2 + interactiveControl.Left;
                    Top = interactiveControl.Top - Height / 2;
                    break;
                #endregion                 
                //------------------------------------------------
                #region Middle
                case (ResizeButtonPosition.MiddleRight):
                    Left = interactiveControl.Width - Width / 2 + interactiveControl.Left;
                    Top = interactiveControl.Height / 2 - Height / 2 + interactiveControl.Top;
                    break;
                case (ResizeButtonPosition.MiddleLeft):
                    Left = interactiveControl.Left - Width / 2;
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
        /// <summary>
        /// cette fonction ajuste le curseur du bouton de redimenssionement selon sa position
        /// </summary>
        /// <returns></returns>
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
                case (ResizeButtonPosition.TopCenter):
                    Cursor = Cursors.SizeNS;
                    break;
                case (ResizeButtonPosition.MiddleRight):
                    Cursor = Cursors.SizeWE;
                    break;
                case (ResizeButtonPosition.MiddleLeft):
                    Cursor = Cursors.SizeWE;
                    break;
            }
            return true;
        }
        #endregion

        #region ShowControl
        /// <summary>
        /// cette fonction affiche ou masque le contr�le
        /// </summary>
        /// <param name="show">afficher ou masquer</param>
        public void ShowControl(bool show)
        {
            if (!show)
                this.Visible = false;
            else
            {
                if (UpdateLocation())
                {
                    if (m_bCanBeVisible)
                    {
                        this.Visible = true;
                        BringToFront();
                    }
                }
            }
        }
        #endregion

        #region Code g�n�r� par le Concepteur de composants
        /// <summary>
        /// initialise le composant
        /// </summary>
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

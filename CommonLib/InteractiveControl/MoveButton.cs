using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace CommonLib
{
    public partial class MoveButton : Control
    {
        #region variables
        //contrôle à bouger
        private IInteractive interactiveControl = null;
        //est-ce que le bouton gauche est appuyée?
        private bool isPressed = false;
        //variable qui va enregistrer la position du point au premier clic
        private Point clickPoint = new Point();
        #endregion

        #region Constructeur
        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public MoveButton(IInteractive interactiveControl)
        {
            InitializeComponent();
            //la taille du contrôle est celle de l'icône
            if (BackgroundImage != null)
                this.Size = BackgroundImage.Size;

            this.Visible = false;
            this.interactiveControl = interactiveControl;

        }
        #endregion

        #region UpdateLocation
        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        //cette fonction met à jour la position du contrôle en fonction
        //du contrôle à bouger
        public bool UpdateLocation()
        {
            if (interactiveControl == null) return false;
            this.Location = new Point(interactiveControl.Left + 9, interactiveControl.Top - (int)(this.Height / 2));
            return true;
        }
        #endregion

        #region ShowControl
        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        //cette fonction affiche le contrôle
        public void ShowControl(bool show)
        {
            if (!show) 
                this.Visible = false;
            else
            {
                if (UpdateLocation())
                {
                    this.Visible = true;
                    BringToFront();
                }
            }
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
                if (interactiveControl != null)
                {
                    interactiveControl.BeginMove();
                }
            }
            base.OnMouseDown(e);
        }
        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isPressed = false;
                if (interactiveControl != null)
                {
                    interactiveControl.EndMove();
                }
            }
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
                Point location = new Point(e.X - clickPoint.X + interactiveControl.Location.X, e.Y - clickPoint.Y + interactiveControl.Location.Y);
                interactiveControl.UpdateLocation(location);
            }
            base.OnMouseMove(e);
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
            // MoveButton
            // 
            this.BackgroundImage = Resources.move;
            this.Cursor = System.Windows.Forms.Cursors.SizeAll;
            this.ResumeLayout(false);

        }
        #endregion
    }
}

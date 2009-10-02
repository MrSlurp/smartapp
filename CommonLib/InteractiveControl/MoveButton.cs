using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace CommonLib
{
    public class MoveButton : Control
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
        /// <summary>
        /// contsructeur
        /// </summary>
        /// <param name="interactiveControl">control interactif associé</param>
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
        /// <summary>
        /// cette fonction met à jour la position du contrôle en fonction du control à bouger
        /// </summary>
        /// <returns></returns>
        public bool UpdateLocation()
        {
            if (interactiveControl == null) 
                return false;

            this.Location = new Point(interactiveControl.Left + 9, interactiveControl.Top - (int)(this.Height / 2));
            return true;
        }
        #endregion

        #region ShowControl
        /// <summary>
        /// cette fonction affiche ou masque le contrôle
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
                    this.Visible = true;
                    BringToFront();
                }
            }
        }
        #endregion

        #region Mouse Events
        /// <summary>
        /// évènement lors que le move button est enfoncé
        /// </summary>
        /// <param name="e"></param>
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

        /// <summary>
        /// évènement lors que le move button est relaché
        /// </summary>
        /// <param name="e"></param>
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

        /// <summary>
        /// évènement appelé lorsque le curseur est déplacé
        /// </summary>
        /// <param name="e"></param>
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
        /// <summary>
        /// initialise le composant
        /// </summary>
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

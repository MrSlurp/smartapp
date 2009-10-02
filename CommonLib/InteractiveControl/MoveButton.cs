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
        //contr�le � bouger
        private IInteractive interactiveControl = null;
        //est-ce que le bouton gauche est appuy�e?
        private bool isPressed = false;
        //variable qui va enregistrer la position du point au premier clic
        private Point clickPoint = new Point();
        #endregion

        #region Constructeur
        /// <summary>
        /// contsructeur
        /// </summary>
        /// <param name="interactiveControl">control interactif associ�</param>
        public MoveButton(IInteractive interactiveControl)
        {
            InitializeComponent();
            //la taille du contr�le est celle de l'ic�ne
            if (BackgroundImage != null)
                this.Size = BackgroundImage.Size;

            this.Visible = false;
            this.interactiveControl = interactiveControl;

        }
        #endregion

        #region UpdateLocation
        /// <summary>
        /// cette fonction met � jour la position du contr�le en fonction du control � bouger
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
                    this.Visible = true;
                    BringToFront();
                }
            }
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
                if (interactiveControl != null)
                {
                    interactiveControl.BeginMove();
                }
            }
            base.OnMouseDown(e);
        }

        /// <summary>
        /// �v�nement lors que le move button est relach�
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
        /// �v�nement appel� lorsque le curseur est d�plac�
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

        #region Code g�n�r� par le Concepteur de composants
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

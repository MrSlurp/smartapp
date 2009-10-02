using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace CommonLib
{
    #region interface IInteractive
    //*****************************************************************************************************
    // Description: interface des interactive control, cette interface est utilis�e par les movebutton
    // et les resizebutton
    // Return: /
    //*****************************************************************************************************
    public interface IInteractive
    {
        void UpdateLocation(Point newLocation);
        void UpdateSizeAndLocation(Point mouseMove);
        void UpdateSelectionLocation();
        void UpdateSize(Point mouseMove);
        void BeginMove();
        void EndMove();
        int Left { get; set; }
        int Right { get;}
        int Top { get; set; }
        int Bottom { get; }
        int Width { get; set; }
        int Height { get; set; }
        Point Location { get; set; }
        bool Selected { get; set;}
    }
    #endregion

    #region enum type de control
    //*****************************************************************************************************
    // Description: Aspects possibles des intercatives control
    // Return: /
    //*****************************************************************************************************
    public enum InteractiveControlType
    {
        Button,
        Combo,
        CheckBox,
        Slider,
        NumericUpDown,
        Text,
        SpecificControl,
        DllControl,
    }
    #endregion

    /// <summary>
    /// un contr�le interactif est un contr�le qu'on peut placer et redimensionn�
    /// dans la surface de dessin
    /// </summary>
    public partial class InteractiveControl : UserControl, IInteractive
    {
        #region donn�es membres
        protected bool canMove = true; // true si le control est d�pla�able
        protected bool canResizeWidth = true; // true si le control peux �tre redimensionn� horizontalement
        protected bool canResizeHeight = false;// true si le control peux �tre redimensionn� verticalement
        //est-que le contr�le poss�dent le focus
        protected bool IsFocused = false; // indique si le control a le focus
        protected bool IsSelected = false; // indique si le control fait parti de la s�l�ction
        //taille minmale et maximale
        protected Size minSize = new Size(32, 20);
        protected Size maxSize = new Size(800, 800);
        //tableau des boutons de redimenssionement
        private ResizeButton[] resizeButton = new ResizeButton[5];
        //bouton de d�placement
        private MoveButton moveButton;

        // type du control
        protected InteractiveControlType m_TypeControl = InteractiveControlType.Button;

        private BTControl m_SrcBTControl = null;
        private bool m_bInitialized = false;
        private Point m_ptMouseDown = new Point();
        #endregion

        #region Delegates & events
        public delegate bool InteractiveMove(InteractiveControl ctrl, ref Size szMove);
        public event InteractiveMove OnMouve;
        public delegate void AssociateDataDropedEvent(InteractiveControl iCtrl);
        public event AssociateDataDropedEvent AsscociateDataDroped;
        #endregion

        #region attributs
        /// <summary>
        /// taille minimum d'un control intractif
        /// </summary>
        public Size MinSize
        {
            get
            {
                return minSize;
            }
        }

        /// <summary>
        /// accesseur pour le type, modifie atomatiquement les propri�t� de redimensionement et 
        /// la taille du control quand le type change
        /// </summary>
        public virtual InteractiveControlType ControlType
        {
            get
            {
                return m_TypeControl;
            }
            set
            {
                m_TypeControl = value;
                OnTypeControlChanged();
                Refresh();
            }
        }

        /// <summary>
        /// accesseur utilis� par le parent, d�fini si l'objet fait partie de la s�l�ction et 
        /// modifie l'apparence en cons�quence
        /// </summary>
        public bool Selected
        {
            get
            {
                return IsSelected;
            }
            set
            {
                IsSelected = value;
                if (canMove)
                    moveButton.ShowControl(IsSelected);
                foreach (ResizeButton button in resizeButton)
                {
                    if (button != null)
                        button.ShowControl(IsSelected);
                }

                Refresh();
            }
        }

        /// <summary>
        /// indique si le control � �t� initialis� (pos� dans la surface de dessin)
        /// </summary>
        public bool Initialized
        {
            get
            {
                return m_bInitialized;
            }
        }

        /// <summary>
        /// accesseur vers l'objet BTControl source qui contiens les param�tres
        /// </summary>
        public BTControl SourceBTControl
        {
            get
            {
                return m_SrcBTControl;
            }
            set
            {
                m_SrcBTControl = value;
            }
        }

        /// <summary>
        /// point ou la souris � �t� cliqu� dans le control
        /// </summary>
        public Point PtMouseDown
        {
            get
            {
                return m_ptMouseDown;
            }
        }

        /// <summary>
        /// indique si le control est de type plugin DLL (surcharg� dans les classes h�ritant de celle ci 
        /// dans les controles DLL)
        /// </summary>
        public virtual bool IsDllControl
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// renvoie l'identifiant DLL du control si c'est bien un plugin, sinon renvoie 0
        /// </summary>
        public virtual uint DllControlID
        {
            get
            {
                return 0;
            }
        }

        #endregion

        #region constructeur et init
        /// <summary>
        /// constructeur par d�faut
        /// </summary>
        public InteractiveControl()
        {
            this.DoubleBuffered = true;
            InitializeComponent();
            //on initialise le bouton d�placement
            moveButton = new MoveButton(this);

            //initialisation des 3 boutons de redimenssionement
            resizeButton[0] = new ResizeButton(ResizeButtonPosition.BottomRight, this);
            resizeButton[1] = new ResizeButton(ResizeButtonPosition.MiddleRight, this);
            resizeButton[2] = new ResizeButton(ResizeButtonPosition.BottomCenter, this);
            resizeButton[3] = new ResizeButton(ResizeButtonPosition.MiddleLeft, this);
            resizeButton[4] = new ResizeButton(ResizeButtonPosition.TopCenter, this);
            this.Text = "Item";
            BackColor = Color.Transparent;
        }

        /// <summary>
        /// fonction de cr�ation sans appel directe au constructeur
        /// </summary>
        /// <returns>un nouveau InteractiveControl</returns>
        public virtual InteractiveControl CreateNew()
        {
            return new InteractiveControl();
        }

        /// <summary>
        /// cette fonction initialise les boutons de redimensionnement et de d�placement
        /// </summary>
        public void InitInteractiveControl()
        {
            if (this.Parent != null)
            {
                Parent.Controls.Add(moveButton);
                foreach (ResizeButton button in resizeButton)
                {
                    if (button != null)
                    {
                        Parent.Controls.Add(button);
                        button.BringToFront();
                    }
                }
                moveButton.BringToFront();
            }
            m_bInitialized = true;
        }
        #endregion

        #region Fonctions d'update de la taille et de la position
        //*****************************************************************************************************
        //les quatres fonctions suivantes s'occupent du d�placement,
        //du redmiensionnement et de la mise a jour des propri�t�s de l'ensemble des �l�ments
        //*****************************************************************************************************

        /// <summary>
        /// cette fonction est appell�e lorsque le contr�le est redimensionn� � partir des
        /// boutons de redimenssionnement 
        /// </summary>
        /// <param name="mouseMove">d�placement de la souris</param>
        public void UpdateSizeAndLocation(Point mouseMove)
        {
            //on s'assure que les nouvelles dimensions sont
            //entre la taille max et min
            if ((Width - mouseMove.X) > maxSize.Width)
                mouseMove.X = -(maxSize.Width - Width);
            else if ((Width - mouseMove.X) < minSize.Width)
                mouseMove.X = -(minSize.Width - Width);

            if ((Height - mouseMove.Y) > maxSize.Height)
                mouseMove.Y = -(maxSize.Height - Height);
            else if ((Height - mouseMove.Y) < minSize.Height)
                mouseMove.Y = -(minSize.Height - Height);

            Point newLocation = new Point(Left + mouseMove.X, Top + mouseMove.Y);
            //on s'assure qu'on d�borde pas de l'�cran (du contr�le parent)
            if (newLocation.X < 0) 
                newLocation.X = 0;
            if (newLocation.Y < 0) 
                newLocation.Y = 0;
            //on calcule la nouvelle taille
            Size newSize = new Size(Left + Width - newLocation.X, Top + Height - newLocation.Y);
            //on change la taille du contr�le et sa position
            this.SuspendLayout();
            this.Size = newSize;
            this.Location = newLocation;
            this.ResumeLayout();
            //on met � jour la position des contr�les de redimensionnement
            //et le bouton de d�placement
            this.UpdateSelectionLocation();
        }

        /// <summary>
        /// met a jour la position du control
        /// </summary>
        /// <param name="newLocation">nouvelle position du control</param>
        public void UpdateLocation(Point newLocation)
        {
            //on s'arrange pour ne pas d�border
            Point oldLocation = this.Location;

            if (newLocation.X < 0)
                newLocation.X = 0;
            if (newLocation.Y < 0)
                newLocation.Y = 0;
            if (newLocation.X + this.Width > Parent.ClientRectangle.Width)
                newLocation.X = Parent.ClientRectangle.Width - this.Width;
            if (newLocation.Y + this.Height > Parent.ClientRectangle.Height)
                newLocation.Y = Parent.ClientRectangle.Height - this.Height;
            Size MovementSize = new Size(newLocation.X - oldLocation.X, newLocation.Y - oldLocation.Y);
            if (OnMouve != null)
            {
                // le container peux modifier le vecteur de d�placement via l'appel a cette fonction
                // utilis� lors du d�placement de plusieurs controls sumiltan�ments
                OnMouve(this, ref MovementSize);
            }
            newLocation = new Point(Location.X + MovementSize.Width, Location.Y + MovementSize.Height);
            //on assigne � notre contr�le sa nouvelle position et on update le reste
            //this.Location = newLocation;
            //UpdateSelectionLocation();
        }

        /// <summary>
        /// met a jour la taille d'un control
        /// </summary>
        /// <param name="mouseMove">d�placement de la souris</param>
        public void UpdateSize(Point mouseMove)
        {
            Size newSize = new Size(Width + mouseMove.X, Height + mouseMove.Y);
            //on s'assur que le contr�le ne d�borde pas du cadre du contr�le parent
            if ((newSize.Width + Left) > Parent.ClientRectangle.Width)
                newSize.Width = Parent.ClientRectangle.Width - this.Left;
            if ((newSize.Height + Top) > Parent.ClientRectangle.Height)
                newSize.Height = Parent.ClientRectangle.Height - this.Top;

            //on s'assure que la nouvelle taille est entre la taille min et max
            if (newSize.Width < minSize.Width)
                newSize.Width = minSize.Width;
            else if (newSize.Width > maxSize.Width)
                newSize.Width = maxSize.Width;

            if (newSize.Height < minSize.Height)
                newSize.Height = minSize.Height;
            else if (newSize.Height > maxSize.Height)
                newSize.Height = maxSize.Height;
            //on set maintenant la taille du c�ntrole.
            this.Size = newSize;
            UpdateSelectionLocation();
            Refresh();
        }

        /// <summary>
        /// appel� au d�but du d�placement des controles
        /// </summary>
        public void BeginMove()
        {
            for (int i = 0; i< resizeButton.Length; i++) 
            {
                if (resizeButton[i] != null)
                    resizeButton[i].CanBeVisible = false;
            }
        }

        /// <summary>
        /// appel� � la fin du d�placement des controles
        /// </summary>
        public void EndMove()
        {
            UpdateResizeBtnsVisibility();
            for (int i = 0; i < resizeButton.Length; i++)
            {
                if (resizeButton[i] != null)
                    resizeButton[i].ShowControl(IsSelected);
            }
        }

        /// <summary>
        /// met a jour la position des poign�es de redimensionnement
        /// </summary>
        public void UpdateSelectionLocation()
        {
            for (int i = 0; i< resizeButton.Length; i++)
            {
                if (resizeButton[i] != null)
                    resizeButton[i].UpdateLocation();
            }
            UpdateMoveButtonLocation();
        }

        /// <summary>
        /// met a jour la position de la poign�e de d�placement
        /// </summary>
        public void UpdateMoveButtonLocation()
        {
            moveButton.UpdateLocation();
        }

        /// <summary>
        /// appel� pr mettre a jour l'�tat du control lorsque celui ci change de type
        /// </summary>
        protected void OnTypeControlChanged()
        {
            switch (m_TypeControl)
            {
                case InteractiveControlType.Button:
                    canResizeWidth = true;
                    canResizeHeight = true;
                    break;
                case InteractiveControlType.CheckBox:
                    canResizeWidth = true;
                    canResizeHeight = false;
                    this.Height = this.minSize.Height;
                    break;
                case InteractiveControlType.Combo:
                    canResizeWidth = true;
                    canResizeHeight = false;
                    this.Height = this.minSize.Height;
                    break;
                case InteractiveControlType.NumericUpDown:
                    canResizeWidth = true;
                    canResizeHeight = false;
                    this.Height = this.minSize.Height;
                    break;
                case InteractiveControlType.Slider:
                    canResizeWidth = true;
                    canResizeHeight = false;
                    this.Height = this.minSize.Height * 2 +5;// la taille min d'un slider en hauteur est de 45
                    break;
                case InteractiveControlType.Text:
                    canResizeWidth = true;
                    canResizeHeight = false;
                    this.Height = this.minSize.Height;
                    break;
                case InteractiveControlType.SpecificControl:
                case InteractiveControlType.DllControl:
                    canResizeWidth = ((ISpecificControl)this).SpecGraphicProp.m_bcanResizeWidth;
                    canResizeHeight = ((ISpecificControl)this).SpecGraphicProp.m_bcanResizeHeight;
                    this.minSize = ((ISpecificControl)this).SpecGraphicProp.m_MinSize;
                    break;
                default:
                    System.Diagnostics.Debug.Assert(false);
                    break;
            }
            UpdateResizeBtnsVisibility();
        }

        /// <summary>
        /// met a jour la visibilit� des poign�es de redimensionnement
        /// </summary>
        public void UpdateResizeBtnsVisibility()
        {
            if (canResizeHeight && canResizeWidth)
                resizeButton[0].CanBeVisible = true;
            else
                resizeButton[0].CanBeVisible = false;

            if (canResizeWidth)
            {
                resizeButton[1].CanBeVisible = true;
                resizeButton[3].CanBeVisible = true;
            }
            else
            {
                resizeButton[1].CanBeVisible = false;
                resizeButton[3].CanBeVisible = false;
            }

            if (canResizeHeight)
            {
                resizeButton[2].CanBeVisible = true;
                resizeButton[4].CanBeVisible = true;
            }
            else
            {
                resizeButton[2].CanBeVisible = false;
                resizeButton[4].CanBeVisible = false;
            }
        }
        #endregion

        #region overrides
        /// <summary>
        /// surcharge de la fonction de dessin
        /// </summary>
        /// <param name="e">argument de dessin</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            switch (m_TypeControl)
            {
                case InteractiveControlType.Button:
                    ControlPainter.DrawButton(e.Graphics, this);
                    break;
                case InteractiveControlType.CheckBox:
                    ControlPainter.DrawCheckBox(e.Graphics, this);
                    break;
                case InteractiveControlType.Combo:
                    ControlPainter.DrawComboBox(e.Graphics, this);
                    break;
                case InteractiveControlType.NumericUpDown:
                    ControlPainter.DrawNumUpDown(e.Graphics, this);
                    break;
                case InteractiveControlType.Slider:
                    ControlPainter.DrawSlider(e.Graphics, this);
                    break;
                case InteractiveControlType.Text:
                    ControlPainter.DrawText(e.Graphics, this);
                    break;
                case InteractiveControlType.SpecificControl:
                    ((ISpecificControl)this).SelfPaint(e.Graphics, this);
                    break;
                default:
                    System.Diagnostics.Debug.Assert(false);
                    break;
            }
            DrawSelRect(e.Graphics);
        }

        /// <summary>
        /// dessine le rectangle de s�lection du control
        /// </summary>
        /// <param name="gr">graphics du control</param>
        public void DrawSelRect(Graphics gr)
        {
            if (Selected || !Initialized)
            {
                Pen dashPen = new Pen(Color.Gray, 1);
                dashPen.DashStyle = DashStyle.Dash;
                Rectangle rect = new Rectangle(ClientRectangle.Left, ClientRectangle.Top,
                    ClientRectangle.Width - 1, ClientRectangle.Height - 1);
                gr.DrawRectangle(dashPen, rect);
                dashPen.Dispose();
            }

        }

        /// <summary>
        /// surcharge de la fonction appel� lors de la parte de focus
        /// </summary>
        /// <param name="e">argument de la fonction</param>
        protected override void OnLostFocus(EventArgs e)
        {
            IsFocused = false;
            foreach (ResizeButton button in resizeButton)
            {
                if (button != null)
                    button.ShowControl(IsFocused);
            }
            this.Refresh();
            base.OnLostFocus(e);
        }

        /// <summary>
        /// surcharge de l'�v�nement du clique souris
        /// </summary>
        /// <param name="e">arguments de l'�v�nement</param>
        protected override void OnClick(EventArgs e)
        {
            IsFocused = true;
            foreach (ResizeButton button in resizeButton)
            {
                if (button != null)
                    button.ShowControl(IsFocused && Selected);
            }
            this.Refresh();
            this.Focus();
            base.OnClick(e);
        }

        /// <summary>
        /// surcharge de l'�v�nement lors que le clique souris est enfonc�
        /// </summary>
        /// <param name="e">argument de l'�v�nement souris</param>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            IsFocused = true;
            base.OnMouseDown(e);
            
            if (Control.ModifierKeys == Keys.Control)
            {
                m_ptMouseDown.X = e.X;
                m_ptMouseDown.Y = e.Y;
                DoDragDrop(this, DragDropEffects.All);
            }
            this.Refresh();
            this.Focus();
        }
        #endregion

        /// <summary>
        /// �v�nement de survol souris en drag drop
        /// </summary>
        /// <param name="sender">objet emmetteur</param>
        /// <param name="e">arguments de l'�v�nement</param>
        private void InteractiveControl_DragOver(object sender, DragEventArgs e)
        {
            Data DropedItem = (Data)e.Data.GetData(typeof(Data));
            if (DropedItem != null && this.ControlType != InteractiveControlType.Text)
                e.Effect = DragDropEffects.All;
            else
                e.Effect = DragDropEffects.None;
        }

        /// <summary>
        /// �v�nement de fin de survol en drag drop
        /// </summary>
        /// <param name="sender">objet emmetteur</param>
        /// <param name="e">arguments de l'�v�nement</param>
        private void InteractiveControl_DragLeave(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// �v�nement de drop sur l'objet
        /// </summary>
        /// <param name="sender">objet emmetteur</param>
        /// <param name="e">arguments de l'�v�nement</param>
        private void InteractiveControl_DragDrop(object sender, DragEventArgs e)
        {
            Data DropedItem = (Data)e.Data.GetData(typeof(Data));
            if (DropedItem != null)
            {
                if (this.ControlType != InteractiveControlType.Text)
                {
                    m_SrcBTControl.AssociateData = DropedItem.Symbol;
                    if (AsscociateDataDroped != null)
                        AsscociateDataDroped(this);
                }
            }
        }
    }
}

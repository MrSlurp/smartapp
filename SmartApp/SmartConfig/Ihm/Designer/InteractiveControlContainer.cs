using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Data;
using System.Text;
using System.Windows.Forms;
using CommonLib;

namespace SmartApp.Ihm.Designer
{
    public delegate void IControlAddedEvent(InteractiveControl Ctrl, BTControl SrcBtControl);
    public delegate void IControlRemovedEvent(InteractiveControl Ctrl);
    public delegate void IControlBringToTop(InteractiveControl Ctrl);
    public delegate void SelectionChangeEvent();
    public delegate bool CanChangeSelectionEvent();
    public delegate void ControlDoubleClicked();
    public delegate void ControlsPosChanged();

    //*****************************************************************************************************
    // Description: enum des possibilitées de mouvement/resize du group d'interactive control selectionné
    // Return: /
    //*****************************************************************************************************
    public enum SelectionAbilitiesValues
    {
        UnableResize = 0,
        AbleResizeWidth = 0x01,
        AbleResizeHeight = 0x02,
        AbleResizeBoth = 0x03
    }

    //*****************************************************************************************************
    // Description: paneux contenant des interactive control et permettant de faire:
    // - des selection multiples
    // - des déplacements de séléction
    // - des alignements a gauche ou en haut
    // - des mises aux meme dimension des éléments séléctionnés
    //*****************************************************************************************************
    public partial class InteractiveControlContainer : UserControl
    {
        #region données membres
        private ArrayList m_ListSelection = null;
        private Rectangle m_RectSelection;
        private bool m_bMouseDown = false;
        private Point m_ptMouseDown;
        private Bitmap m_BmpBackImage = null;
        #endregion

        #region Events

        public event SelectionChangeEvent SelectionChange;
        public event IControlAddedEvent EventControlAdded;
        public event IControlRemovedEvent EventControlRemoved;
        public event CanChangeSelectionEvent EventCanChangeSelection;
        public event ControlDoubleClicked EventControlDblClick;
        public event ControlsPosChanged EventControlPosChanged;
        public event IControlBringToTop EventControlBringToTop;

        #endregion 

        #region attributs

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public int SelectionCount
        {
            get
            {
                return m_ListSelection.Count;
            }
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public InteractiveControl FirstSelected
        {
            get
            {
                if (m_ListSelection.Count > 0)
                    return (InteractiveControl) m_ListSelection[0];
                else
                    return null;
            }
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public Bitmap ScreenBckImage
        {
            get
            {
                return m_BmpBackImage;
            }
            set
            {
                m_BmpBackImage = value;
                Invalidate();
            }
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public void ClearSelection()
        {
            m_ListSelection.Clear();
            this.Update();
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public SelectionAbilitiesValues SelectionAbilities
        {
            get
            {
                SelectionAbilitiesValues Abilities = SelectionAbilitiesValues.AbleResizeBoth;
                if (m_ListSelection.Count <= 1)
                {
                    Abilities = SelectionAbilitiesValues.UnableResize;
                    return Abilities;
                }

                for (int i = 0; i < m_ListSelection.Count; i++)
                {
                    InteractiveControl Ctrl = (InteractiveControl)m_ListSelection[i];
                    switch (Ctrl.ControlType)
                    {
                        case InteractiveControlType.Button:
                        case InteractiveControlType.SpecificControl:
                        case InteractiveControlType.DllControl:
                            break;
                        case InteractiveControlType.CheckBox:
                        case InteractiveControlType.Slider:
                        case InteractiveControlType.Combo:
                        case InteractiveControlType.Text:
                        case InteractiveControlType.NumericUpDown:
                            Abilities = SelectionAbilitiesValues.AbleResizeWidth;
                            break;
                        default :
                            System.Diagnostics.Debug.Assert(false);
                            break;
                    }
                }
                return Abilities;
            }
        }
        #endregion

        #region constructeur
        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public InteractiveControlContainer()
        {
            m_ListSelection = new ArrayList();
            InitializeComponent();
            this.BackgroundImageLayout = ImageLayout.None;
        }
        #endregion

        #region Handler d'évènements

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        private bool InsideControlMouve(InteractiveControl Ctrl, ref Size szMove)
        {
            TraiteMove(Ctrl, szMove);
            if (EventControlPosChanged != null)
                EventControlPosChanged();
            return true;
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        private void InsideControlMouseDownHandler(object obj, MouseEventArgs e)
        {
            TraiteSelection(obj, e);
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        private void TraiteSelection(object obj, MouseEventArgs e)
        {
            if (EventCanChangeSelection != null && !EventCanChangeSelection())
                return;

            // on convertis en point écran
            Point ptMouse = ((Control)obj).PointToScreen(e.Location);
            // puis en point client pour this
            ptMouse = PointToClient(ptMouse);
            if (DropableItems.AllowedItem(obj.GetType()) || this.Controls.Contains((Control)obj))
            {
                for (int i = 0; i < m_ListSelection.Count; i++)
                {
                    ((IInteractive)m_ListSelection[i]).Selected = false;
                }
                if (((Control)obj).Bounds.Contains(ptMouse))
                {
                    if (Form.ModifierKeys == Keys.Shift)
                    {
                        if (!m_ListSelection.Contains(obj))
                        {
                            m_ListSelection.Add(obj);
                        }
                        else
                        {
                            m_ListSelection.Remove(obj);
                        }
                    }
                    else
                    {
                        m_ListSelection.Clear();
                        m_ListSelection.Add(obj);
                    }
                    for (int i = 0; i < m_ListSelection.Count; i++)
                    {
                        ((IInteractive)m_ListSelection[i]).Selected = true;
                    }
                }
                if (SelectionChange != null)
                    SelectionChange();
            }
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        protected void TraiteMove(Control Ctrl, Size szMove)
        {
            // rectangle représentant la séléction
            Rectangle RectItems = new Rectangle(32000, 32000, 0, 0);
            // on calcule la position du rectangle
            for (int i =0; i< m_ListSelection.Count; i++)
            {
                //on récupère les coordonnées les plus en haut a gauche
                Control ctl = (Control)m_ListSelection[i];
                if (RectItems.X > ctl.Left)
                    RectItems.X = ctl.Left;

                if (RectItems.Y > ctl.Top)
                    RectItems.Y = ctl.Top;

            }
            // on calcule la taille du rectangle
            foreach (Control ctl in m_ListSelection)
            {
                if (RectItems.Right < ctl.Right)
                    RectItems.Width = ctl.Right - RectItems.X;

                if (RectItems.Bottom < ctl.Bottom)
                    RectItems.Height = ctl.Bottom - RectItems.Y;
            }
            // on l'offset du déplacement qui va être effectué
            RectItems.Offset(szMove.Width, szMove.Height);
            if (!this.ClientRectangle.Contains(RectItems))
            {
                // on dois "rogner le déplacement"
                if (RectItems.Top < ClientRectangle.Top || RectItems.Bottom > ClientRectangle.Bottom)
                    szMove.Height -= RectItems.Y;
                if (RectItems.Left < ClientRectangle.Left || RectItems.Right > ClientRectangle.Right)
                    szMove.Width -= RectItems.X;
            }
            if (szMove.Width != 0 || szMove.Height != 0)
            {
                for (int i = 0; i < m_ListSelection.Count; i++)
                {
                    InteractiveControl Selection = (InteractiveControl)m_ListSelection[i];
                    Selection.Invalidate();
                    Selection.Location = new Point(Selection.Location.X + szMove.Width, Selection.Location.Y + szMove.Height);
                    Selection.UpdateSelectionLocation();
                }
                if (Ctrl != null)
                {
                    Ctrl.Focus();
                }
                this.Update();
            
            }
        }

        #endregion

        #region overrides

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        protected override void OnMouseClick(MouseEventArgs e)
        {
            if (EventCanChangeSelection != null && !EventCanChangeSelection())
                return;

            for (int i = 0; i < m_ListSelection.Count; i++)
            {
                ((IInteractive)m_ListSelection[i]).Selected = false;
            }
            this.Focus();
            m_ListSelection.Clear();
            if (SelectionChange != null)
                SelectionChange();
            base.OnMouseClick(e);
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            m_bMouseDown = true;
            this.Capture = true;
            m_ptMouseDown = e.Location;
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (m_BmpBackImage != null)
            {
                Rectangle rcDisplay = this.DisplayRectangle;
                e.Graphics.DrawImage(m_BmpBackImage, new Point(rcDisplay.X, rcDisplay.Y));
            }
            if (m_bMouseDown)
            {
                e.Graphics.DrawRectangle(new Pen(Brushes.Black), m_RectSelection);
            }
            // dessin des lignes "helpers"
            
            Pen penDotRed = new Pen(Color.Red);
            penDotRed.DashStyle = DashStyle.Dot;
            Point[] ptRepere1280par1024 = new Point[3] {new Point(0, 1024), new Point(1280, 1024), new Point(1280, 0)};
            e.Graphics.DrawLines(penDotRed, ptRepere1280par1024);
            string strHelpText = "1280 x 1024";
            e.Graphics.DrawString(strHelpText, SystemFonts.DefaultFont, Brushes.Red, new Point(640, 1024));

            Pen penDotBlue = new Pen(Color.Blue);
            penDotBlue.DashStyle = DashStyle.Dot;
            Point[] ptRepere1024par768 = new Point[3] { new Point(0, 768), new Point(1024, 768), new Point(1024, 0) };
            e.Graphics.DrawLines(penDotBlue, ptRepere1024par768);
            strHelpText = "1024 x 768";
            e.Graphics.DrawString(strHelpText, SystemFonts.DefaultFont, Brushes.Blue, new Point(512, 768));

            Pen penDotGreen = new Pen(Color.Green);
            penDotGreen.DashStyle = DashStyle.Dot;
            Point[] ptRepere1680par1050 = new Point[3] { new Point(0, 1050), new Point(1680, 1050), new Point(1680, 0) };
            e.Graphics.DrawLines(penDotGreen, ptRepere1680par1050);
            strHelpText = "1680 x 1050";
            e.Graphics.DrawString(strHelpText, SystemFonts.DefaultFont, Brushes.Green, new Point(840, 1050));

            Pen penDotPurple = new Pen(Color.Purple);
            penDotPurple.DashStyle = DashStyle.Dot;
            Point[] ptRepere1600par1200 = new Point[3] { new Point(0, 1200), new Point(1600, 1200), new Point(1600, 0) };
            e.Graphics.DrawLines(penDotPurple, ptRepere1600par1200);
            strHelpText = "1600 x 1200";
            e.Graphics.DrawString(strHelpText, SystemFonts.DefaultFont, Brushes.Purple, new Point(800, 1188));
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (m_bMouseDown)
            {
                
                m_RectSelection = new Rectangle(m_ptMouseDown.X, m_ptMouseDown.Y, e.X - m_ptMouseDown.X, e.Y - m_ptMouseDown.Y);
                if (m_RectSelection.Width < 0)
                {
                    m_RectSelection.Offset(m_RectSelection.Width, 0);
                    m_RectSelection.Width = -m_RectSelection.Width;
                }
                if (m_RectSelection.Height < 0)
                {
                    m_RectSelection.Offset(0, m_RectSelection.Height);
                    m_RectSelection.Height = -m_RectSelection.Height;
                }
                Invalidate();
                
            }
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            if (m_bMouseDown && !m_RectSelection.IsEmpty)
            {
                ArrayList ListToSelect = new ArrayList();
                for (int i = 0; i < this.Controls.Count; i++)
                {
                    Rectangle ctrlRect = this.Controls[i].Bounds;
                    if (m_RectSelection.IntersectsWith(ctrlRect))
                    {
                        ListToSelect.Add(this.Controls[i]);
                    }
                }
                m_ListSelection.Clear();
                for (int i = 0; i < ListToSelect.Count; i++)
                {
                    if (DropableItems.AllowedItem(ListToSelect[i].GetType()))
                    {
                        m_ListSelection.Add(ListToSelect[i]);
                        ((InteractiveControl)ListToSelect[i]).Selected = true;
                    }
                }
                m_RectSelection.X = 0;
                m_RectSelection.Y = 0;
                m_RectSelection.Width = 0;
                m_RectSelection.Height = 0;
                if (this.SelectionChange != null)
                    SelectionChange();

                Invalidate();
            }
            m_bMouseDown = false;
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        protected override void OnControlAdded(ControlEventArgs e)
        {
            base.OnControlAdded(e);
            // losrqu'un control est ajouté, les handlers suivants sont automatiquement ajoutés
            if (DropableItems.AllowedItem(e.Control.GetType()))
            {
                if (!((InteractiveControl)e.Control).Initialized)
                {
                    e.Control.MouseDown += new MouseEventHandler(InsideControlMouseDownHandler);
                    ((InteractiveControl)e.Control).OnMouve += new InteractiveControl.InteractiveMove(InsideControlMouve);
                    e.Control.KeyDown += new KeyEventHandler(OnControlKeydown);
                    e.Control.DoubleClick += new EventHandler(OnControlDoubleClick);
                    ((InteractiveControl)e.Control).AsscociateDataDroped += new InteractiveControl.AssociateDataDropedEvent(ICtrlDataAssigned);
                    //e.Control.KeyPress += new KeyPressEventHandler(OnArrowKeyPress);
                    //e.Control.KeyDown += new KeyEventHandler(OnArrowKeyPress);
                }
                // au moment ou il est initialisé, il deviens possible de le redimensionner ou de le déplacer
                // donc c'est uniquement lors qu'il est ajouté au container qu'il deviens complètement fonctionel
                ((InteractiveControl)e.Control).InitInteractiveControl();
            }
        }

        private void ICtrlDataAssigned(InteractiveControl iCtrl)
        {
            for (int i = 0; i < m_ListSelection.Count; i++)
            {
                ((IInteractive)m_ListSelection[i]).Selected = false;
            }
            // et on séléctionne l'objet posé 
            m_ListSelection.Clear();
            m_ListSelection.Add(iCtrl);
            iCtrl.Selected = true;
            iCtrl.Focus();
            if (SelectionChange != null)
                SelectionChange();
            if (EventControlDblClick != null)
                EventControlDblClick();

        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        protected override void OnControlRemoved(ControlEventArgs e)
        {
            base.OnControlRemoved(e);
            if (DropableItems.AllowedItem(e.Control.GetType()))
            {
                if (m_ListSelection.Contains(e.Control))
                {
                    ((InteractiveControl)e.Control).Selected = false;
                    m_ListSelection.Remove(e.Control);
                    if (SelectionChange != null)
                        SelectionChange();
                }

            }
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        protected void RemoveSelectedControls()
            {
            while (m_ListSelection.Count >0)
                {
                if (this.Controls.Contains((Control)m_ListSelection[0]))
                {
                    InteractiveControl iControl = (InteractiveControl)m_ListSelection[0];
                    this.Controls.Remove((Control)m_ListSelection[0]);
                    if (EventControlRemoved != null)
                        EventControlRemoved(iControl);

                }
            }
            m_ListSelection.Clear();
            if (SelectionChange != null)
                SelectionChange();

        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        protected void OnArrowKeyPress(object sender, KeyEventArgs e)
        {
            Size szMove = new Size(0,0);
            switch(e.KeyCode)
            {
                case Keys.Up:
                    szMove.Height = -1;
                    break;
                case Keys.Down:
                    szMove.Height = 1;
                    break;
                case Keys.Left:
                    szMove.Width = -1;
                    break;
                case Keys.Right:
                    szMove.Width = 1;
                    break;

            }
            if (szMove.Width != 0 || szMove.Height != 0)
            {
                TraiteMove(null, szMove);
            }
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        protected void OnControlKeydown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete
                || e.KeyCode == Keys.Back)
            {
                RemoveSelectedControls();
                return;
            }
            OnArrowKeyPress(sender, e);
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        protected void OnControlDoubleClick(object sender, EventArgs e)
        {
            if (EventControlDblClick != null)
                EventControlDblClick();
        }

        #endregion

        #region gestion du drag drop
        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        private void OnDragEnter(object sender, DragEventArgs e)
        {
            InteractiveControl DropedItem = DropableItems.GetDropableItem(e);
            if (DropedItem != null)
                e.Effect = DragDropEffects.All;
            else
                e.Effect = DragDropEffects.None;
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        private void OnDragDrop(object sender, DragEventArgs e)
        {
            // on récupère les données de l'objet dropé en correspondance avec ce qu'on veux
            InteractiveControl DropedItem = DropableItems.GetDropableItem(e);
            if (DropedItem != null)
            {
                // si on a bien dropé un InteractiveControl
                // on crée un nouvel objet avec les meme caractéristiques
                // et on le place a la position de la souris
                InteractiveControl newControl = DropedItem.CreateNew();
                Point PtMouse = new Point(e.X, e.Y);
                PtMouse = PointToClient(PtMouse);
                newControl.Name = DropedItem.Name;
                newControl.Location = new Point(PtMouse.X - DropedItem.PtMouseDown.X, 
                                                PtMouse.Y - DropedItem.PtMouseDown.Y);
                if (!newControl.IsDllControl)
                    newControl.ControlType = DropedItem.ControlType;

                int newHeigh = DropedItem.Size.Height;
                int newWidth = DropedItem.Size.Width;
                if (newControl.Size.Height < DropedItem.MinSize.Height)
                    newHeigh = DropedItem.MinSize.Height;

                if (newControl.Size.Width < DropedItem.MinSize.Width)
                    newWidth = DropedItem.MinSize.Width;

                newControl.Size = new Size(newWidth, newHeigh);
                newControl.Text = DropedItem.Text;
                // lorsqu'un control est ajouté, on supprime la séléction
                for (int i = 0; i < m_ListSelection.Count; i++)
                {
                    ((IInteractive)m_ListSelection[i]).Selected = false;
                }
                // et on séléctionne l'objet posé 
                m_ListSelection.Clear();
                newControl.Selected = true;
                if (EventControlAdded != null)
                    EventControlAdded(newControl, DropedItem.SourceBTControl);

                this.Controls.Add(newControl);
                m_ListSelection.Add(newControl);
                newControl.Focus();
                if (SelectionChange != null)
                    SelectionChange();
            }
        }

        #endregion

        #region fonctions de layout
        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public void AlignSelectionLeft()
        {
            if (m_ListSelection.Count > 1)
            {
                IInteractive FirstCtrl = (IInteractive) m_ListSelection[0];
                Point PtFirstControl = FirstCtrl.Location;
                for (int i = 1; i < m_ListSelection.Count; i++)
                {
                    ((IInteractive)m_ListSelection[i]).Location = new Point(PtFirstControl.X, ((IInteractive)m_ListSelection[i]).Location.Y);
                    ((IInteractive)m_ListSelection[i]).UpdateSelectionLocation();
                }
            }
            Refresh();
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public void AlignSelectionTop()
        {
            if (m_ListSelection.Count > 1)
            {
                IInteractive FirstCtrl = (IInteractive)m_ListSelection[0];
                Point PtFirstControl = FirstCtrl.Location;
                for (int i = 1; i < m_ListSelection.Count; i++)
                {
                    ((IInteractive)m_ListSelection[i]).Location = new Point(((IInteractive)m_ListSelection[i]).Location.X, PtFirstControl.Y);
                    ((IInteractive)m_ListSelection[i]).UpdateSelectionLocation();
                }
            }
            Refresh();
        }

        public void LayoutBringToFront()
        {
            if (m_ListSelection.Count >= 1)
            {
                for (int i = 0; i < m_ListSelection.Count; i++)
                {
                    ((InteractiveControl)m_ListSelection[i]).BringToFront();
                    ((InteractiveControl)m_ListSelection[i]).Selected = true;
                    if (EventControlBringToTop != null)
                        EventControlBringToTop((InteractiveControl)m_ListSelection[i]);
                }
            }
            Refresh();
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public void MakeSelectionSameWidth()
        {
            if (m_ListSelection.Count > 1)
            {
                IInteractive FirstCtrl = (IInteractive)m_ListSelection[0];
                int ctrlwidth = FirstCtrl.Width;
                for (int i = 1; i < m_ListSelection.Count; i++)
                {
                    ((IInteractive)m_ListSelection[i]).Width = ctrlwidth;
                    ((IInteractive)m_ListSelection[i]).UpdateSelectionLocation();
                }
            }
            Refresh();
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public void MakeSelectionSameHeight()
        {
            if (m_ListSelection.Count > 1)
            {
                IInteractive FirstCtrl = (IInteractive)m_ListSelection[0];
                int ctrlHeight = FirstCtrl.Height;
                for (int i = 1; i < m_ListSelection.Count; i++)
                {
                    ((IInteractive)m_ListSelection[i]).Height = ctrlHeight;
                    ((IInteractive)m_ListSelection[i]).UpdateSelectionLocation();
                }
            }
            Refresh();
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public void MakeSelectionSameSize()
        {
            if (m_ListSelection.Count > 1)
            {
                IInteractive FirstCtrl = (IInteractive)m_ListSelection[0];
                int ctrlHeight = FirstCtrl.Height;
                int ctrlwidth = FirstCtrl.Width;
                for (int i = 1; i < m_ListSelection.Count; i++)
                {
                    ((IInteractive)m_ListSelection[i]).Width = ctrlwidth;
                    ((IInteractive)m_ListSelection[i]).Height = ctrlHeight;
                    ((IInteractive)m_ListSelection[i]).UpdateSelectionLocation();
                }
            }
            Refresh();
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public void ArrangeItemsAcross()
        {
            if (m_ListSelection.Count > 1)
            {
                // on trie les objet du plus a gauche au plus a droite
                ArrayList ListOfControls = new ArrayList();
                ListOfControls.AddRange(m_ListSelection);
                for (int i = 0; i < ListOfControls.Count -1; )
                {
                    IInteractive Ctrl1 = (IInteractive)ListOfControls[i];
                    IInteractive Ctrl2 = (IInteractive)ListOfControls[i+1];
                    if (Ctrl2.Left < Ctrl1.Left)
                    {
                        ListOfControls[i] = Ctrl2;
                        ListOfControls[i + 1] = Ctrl1;
                        i = 0;
                    }
                    else
                        i++;
                }
                int MostLeft = ((IInteractive)ListOfControls[0]).Left;
                int MostRight = ((IInteractive)ListOfControls[ListOfControls.Count - 1]).Right;
                int TotalControlWidth = 0;
                for (int i = 0; i < ListOfControls.Count; i++)
                {
                    TotalControlWidth += ((IInteractive)ListOfControls[i]).Width;
                }
                int spaceBeteweenControls = 0;
                int totalAvailableWidth = MostRight - MostLeft;
                if (TotalControlWidth > totalAvailableWidth)
                {
                    MessageBox.Show("Not enought space");
                    return;
                    // marche po avec des controls de tailles varaibles
                    /*
                    int MostRightLeft = ((IInteractive)ListOfControls[ListOfControls.Count - 1]).Left;
                    totalAvailableWidth = MostRightLeft - MostLeft;
                    spaceBeteweenControls = totalAvailableWidth / ListOfControls.Count;
                    int CurrentOffset = spaceBeteweenControls;
                    for (int i = 1; i < ListOfControls.Count; i++)
                    {
                        ((IInteractive)ListOfControls[i]).Left = ((IInteractive)ListOfControls[0]).Left + CurrentOffset;
                        CurrentOffset += spaceBeteweenControls;
                        ((IInteractive)ListOfControls[i]).UpdateSelectionLocation();
                    }
                     * */
                }
                else
                {
                    spaceBeteweenControls = (totalAvailableWidth - TotalControlWidth) / (ListOfControls.Count-1);
                    int CurrentOffset = ((IInteractive)ListOfControls[0]).Right + spaceBeteweenControls;
                    for (int i = 1; i < ListOfControls.Count; i++)
                    {
                        ((IInteractive)ListOfControls[i]).Left = CurrentOffset;
                        CurrentOffset += ((IInteractive)ListOfControls[i]).Width;
                        CurrentOffset += spaceBeteweenControls;
                        ((IInteractive)ListOfControls[i]).UpdateSelectionLocation();
                    }
                }
                // espacement = hauteur totale - somme des hauteurs
                // en commencant par l'objet le plus haut, on
            }
            Refresh();
        }
        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public void ArrangeItemsDown()
        {
            if (m_ListSelection.Count > 1)
            {
                // on trie les objet du plus a gauche au plus a droite
                ArrayList ListOfControls = new ArrayList();
                ListOfControls.AddRange(m_ListSelection);
                for (int i = 0; i < ListOfControls.Count - 1; )
                {
                    IInteractive Ctrl1 = (IInteractive)ListOfControls[i];
                    IInteractive Ctrl2 = (IInteractive)ListOfControls[i + 1];
                    if (Ctrl2.Top < Ctrl1.Top)
                    {
                        ListOfControls[i] = Ctrl2;
                        ListOfControls[i + 1] = Ctrl1;
                        i = 0;
                    }
                    else
                        i++;
                }
                int MostTop = ((IInteractive)ListOfControls[0]).Top;
                int MostBottom = ((IInteractive)ListOfControls[ListOfControls.Count - 1]).Bottom;
                int TotalControlHeight = 0;
                for (int i = 0; i < ListOfControls.Count; i++)
                {
                    TotalControlHeight += ((IInteractive)ListOfControls[i]).Height;
                }
                int spaceBeteweenControls = 0;
                int totalAvailableHeight = MostBottom - MostTop;
                if (TotalControlHeight > totalAvailableHeight)
                {
                    MessageBox.Show("Not enough space");
                    return;
                    // marche po avec des controls de tailles varaibles
                    /*
                    int MostBottomTop = ((IInteractive)ListOfControls[ListOfControls.Count - 1]).Top;
                    totalAvailableHeight = MostBottomTop - MostTop;
                    spaceBeteweenControls = totalAvailableHeight / ListOfControls.Count;
                    int CurrentOffset = spaceBeteweenControls;
                    for (int i = 1; i < ListOfControls.Count; i++)
                    {
                        ((IInteractive)ListOfControls[i]).Top = ((IInteractive)ListOfControls[0]).Top + CurrentOffset;
                        CurrentOffset += spaceBeteweenControls;
                        ((IInteractive)ListOfControls[i]).UpdateSelectionLocation();
                    }
                     * */
                }
                else
                {
                    spaceBeteweenControls = (totalAvailableHeight - TotalControlHeight) / (ListOfControls.Count - 1);
                    int CurrentOffset = ((IInteractive)ListOfControls[0]).Bottom + spaceBeteweenControls;
                    for (int i = 1; i < ListOfControls.Count; i++)
                    {
                        ((IInteractive)ListOfControls[i]).Top = CurrentOffset;
                        CurrentOffset += ((IInteractive)ListOfControls[i]).Height;
                        CurrentOffset += spaceBeteweenControls;
                        ((IInteractive)ListOfControls[i]).UpdateSelectionLocation();
                    }
                }
                // espacement = hauteur totale - somme des hauteurs
                // en commencant par l'objet le plus haut, on
            }
            Refresh();
        }

        #endregion

        protected override Point ScrollToControl(Control activeControl)
        {
            return Point.Empty;
        }
    }
}

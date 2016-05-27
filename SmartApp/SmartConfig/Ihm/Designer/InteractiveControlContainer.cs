using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Data;
using System.Text;
using System.Xml;
using System.Windows.Forms;
using CommonLib;

namespace SmartApp.Ihm.Designer
{
    public delegate void IControlAddedEvent(InteractiveControl Ctrl, BTControl SrcBtControl, bool bFromDiffInstance);
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
    public partial class InteractiveControlContainer : UserControl, ILangNonTranslatable
    {
        private const string CLIP_FORMAT = "SmartAppCtrlList";
        DataFormats.Format InternalFormat = DataFormats.GetFormat(CLIP_FORMAT);
        #region données membres
        private ArrayList m_ListSelection = null;
        private Rectangle m_RectSelection;
        private bool m_bMouseDown = false;
        private Point m_ptMouseDown;
        private Bitmap m_BmpBackImage = null;
        private bool m_bDrawGuides = true;
        private Size m_SizeCustomLines = new Size(-1,-1);
        public BTDoc m_Document;

        private int m_GridSpacing = 20;
        #endregion

        #region Events

        public event SelectionChangeEvent SelectionChange;
        public event IControlAddedEvent EventControlAdded;
        public event IControlRemovedEvent EventControlRemoved;
        public event ControlDoubleClicked EventControlDblClick;
        public event IControlBringToTop EventControlBringToTop;

        #endregion 

        #region attributs
        public int GridSpacing
        {
            get { return m_GridSpacing; }
            set
            {
                m_GridSpacing = value;
                this.Refresh();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int SelectionCount
        {
            get
            {
                return m_ListSelection.Count;
            }
        }

        /// <summary>
        /// 
        /// </summary>
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

        /// <summary>
        /// 
        /// </summary>
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

        /// <summary>
        /// 
        /// </summary>
        public void ClearSelection()
        {
            m_ListSelection.Clear();
            this.Update();
        }

        /// <summary>
        /// 
        /// </summary>
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

        public Size CustomLineSize
        {
            get { return m_SizeCustomLines; }
            set { m_SizeCustomLines = value; }
        }
        #endregion

        #region constructeur
        /// <summary>
        /// 
        /// </summary>
        public InteractiveControlContainer()
        {
            m_ListSelection = new ArrayList();
            InitializeComponent();
            this.BackgroundImageLayout = ImageLayout.None;
        }
        #endregion

        #region Handler d'évènements

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Ctrl"></param>
        /// <param name="szMove"></param>
        /// <returns></returns>
        private bool InsideControlMouve(InteractiveControl Ctrl, ref Size szMove)
        {
            TraiteMove(Ctrl, szMove);
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        private void InsideControlEndMouve()
        {
            if (m_Document != null)
            {
                m_Document.Modified = true; ;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="e"></param>
        private void InsideControlMouseDownHandler(object obj, MouseEventArgs e)
        {
            TraiteSelection(obj, e);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="e"></param>
        private void TraiteSelection(object obj, MouseEventArgs e)
        {
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Ctrl"></param>
        /// <param name="szMove"></param>
        public void TraiteMove(Control Ctrl, Size szMove)
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseClick(MouseEventArgs e)
        {
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            m_bMouseDown = true;
            this.Capture = true;
            m_ptMouseDown = e.Location;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
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
            if (m_bDrawGuides)
            {
                using (Pen penDotRed = new Pen(Color.Red))
                using (Pen penDotBlue = new Pen(Color.Blue))
                using (Pen penDotGreen = new Pen(Color.Green))
                using (Pen penDotPurple = new Pen(Color.Purple))
                using (Pen penDotCustom = new Pen(Color.Orange))
                using (Pen penGrille = new Pen(Color.Gray))
                {
                    penDotRed.DashStyle = DashStyle.Dot;
                    Point[] ptRepere1280par1024 = new Point[3] { new Point(0, 1024), new Point(1280, 1024), new Point(1280, 0) };
                    e.Graphics.DrawLines(penDotRed, ptRepere1280par1024);
                    string strHelpText = "1280 x 1024";
                    e.Graphics.DrawString(strHelpText, SystemFonts.DefaultFont, Brushes.Red, new Point(640, 1024));

                    penDotBlue.DashStyle = DashStyle.Dot;
                    Point[] ptRepere1024par768 = new Point[3] { new Point(0, 768), new Point(1024, 768), new Point(1024, 0) };
                    e.Graphics.DrawLines(penDotBlue, ptRepere1024par768);
                    strHelpText = "1024 x 768";
                    e.Graphics.DrawString(strHelpText, SystemFonts.DefaultFont, Brushes.Blue, new Point(512, 768));

                    penDotGreen.DashStyle = DashStyle.Dot;
                    Point[] ptRepere1680par1050 = new Point[3] { new Point(0, 1050), new Point(1680, 1050), new Point(1680, 0) };
                    e.Graphics.DrawLines(penDotGreen, ptRepere1680par1050);
                    strHelpText = "1680 x 1050";
                    e.Graphics.DrawString(strHelpText, SystemFonts.DefaultFont, Brushes.Green, new Point(840, 1050));

                    penDotPurple.DashStyle = DashStyle.Dot;
                    Point[] ptRepere1600par1200 = new Point[3] { new Point(0, 1200), new Point(1600, 1200), new Point(1600, 0) };
                    e.Graphics.DrawLines(penDotPurple, ptRepere1600par1200);
                    strHelpText = "1600 x 1200";
                    e.Graphics.DrawString(strHelpText, SystemFonts.DefaultFont, Brushes.Purple, new Point(800, 1188));
                    penGrille.DashStyle = DashStyle.DashDot;
                    for (int i = m_GridSpacing; m_GridSpacing > 0 && i < this.Width; i += m_GridSpacing)
                    {
                        Point[] ptRepereGrid = new Point[] {  new Point(i, 0), 
                                                               new Point(i, this.Height) };
                        e.Graphics.DrawLines(penGrille, ptRepereGrid);
                    }
                    for (int i = m_GridSpacing; m_GridSpacing > 0 && i < this.Height; i += m_GridSpacing)
                    {
                        Point[] ptRepereGrid2 = new Point[] {  new Point(0, i), 
                                                               new Point(this.Width, i) };
                        e.Graphics.DrawLines(penGrille, ptRepereGrid2);
                    }
                    if (m_SizeCustomLines.Width != -1 && m_SizeCustomLines.Height != -1)
                    {
                        penDotCustom.DashStyle = DashStyle.Dot;
                        Point[] ptRepereCustom = new Point[3] {  new Point(0, m_SizeCustomLines.Height), 
                                                                      new Point(m_SizeCustomLines.Width, m_SizeCustomLines.Height), 
                                                                      new Point(m_SizeCustomLines.Width, 0) };
                        e.Graphics.DrawLines(penDotCustom, ptRepereCustom);
                        strHelpText = string.Format("{0} x {1}", m_SizeCustomLines.Width, m_SizeCustomLines.Height);
                        e.Graphics.DrawString(strHelpText, SystemFonts.DefaultFont, Brushes.Orange, new Point(m_SizeCustomLines.Width / 2, m_SizeCustomLines.Height - 12));
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
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
                //m_RectSelection.Offset(-5,-5);
                //m_RectSelection.Inflate(10,10);
                //Invalidate(m_RectSelection);
                Invalidate();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnControlAdded(ControlEventArgs e)
        {
            base.OnControlAdded(e);
            // losrqu'un control est ajouté, les handlers suivants sont automatiquement ajoutés
            if (DropableItems.AllowedItem(e.Control.GetType()))
            {
                e.Control.MouseDown += new MouseEventHandler(InsideControlMouseDownHandler);
                ((InteractiveControl)e.Control).OnMouve += new InteractiveControl.InteractiveMove(InsideControlMouve);
                ((InteractiveControl)e.Control).EndMouve += new InteractiveControl.InteractiveEndMove(InsideControlEndMouve);
                e.Control.KeyDown += new KeyEventHandler(OnControlKeydown);
                e.Control.DoubleClick += new EventHandler(OnControlDoubleClick);
                ((InteractiveControl)e.Control).AsscociateDataDroped += new InteractiveControl.AssociateDataDropedEvent(ICtrlDataAssigned);
                // au moment ou il est initialisé, il deviens possible de le redimensionner ou de le déplacer
                // donc c'est uniquement lors qu'il est ajouté au container qu'il deviens complètement fonctionel
                ((InteractiveControl)e.Control).InitInteractiveControl();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iCtrl"></param>
        /// <param name="dataSymbol"></param>
        /// <returns></returns>
        private bool ICtrlDataAssigned(InteractiveControl iCtrl, string dataSymbol, bool bDone)
        {
            if (m_Document.GestData.GetFromSymbol(dataSymbol) == null && !bDone)
            {
                string strErr = string.Format(Program.LangSys.C("This data ({0}) does not exists in this document"), dataSymbol);
                MessageBox.Show(strErr, Program.LangSys.C("Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (bDone)
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
            }
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnControlRemoved(ControlEventArgs e)
        {

            if (e.Control is InteractiveControl)
            {
                e.Control.MouseDown -= InsideControlMouseDownHandler;
                ((InteractiveControl)e.Control).OnMouve -= InsideControlMouve;
                ((InteractiveControl)e.Control).EndMouve -= InsideControlEndMouve;
                e.Control.KeyDown -= OnControlKeydown;
                e.Control.DoubleClick -= OnControlDoubleClick;
                ((InteractiveControl)e.Control).AsscociateDataDroped -= ICtrlDataAssigned;
            }

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

        /// <summary>
        /// 
        /// </summary>
        protected void RemoveSelectedControls()
        {
            while (m_ListSelection.Count > 0)
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            const int WM_KEYDOWN = 0x100;
            const int WM_SYSKEYDOWN = 0x104;
            if ((msg.Msg == WM_KEYDOWN) || (msg.Msg == WM_SYSKEYDOWN))
            {

                TraiteArrowKeys(keyData);
            }
            
            return base.ProcessCmdKey(ref msg, keyData);
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected void TraiteArrowKeys(Keys e)
        {
            if ((e & Keys.Control) == 0)
            {
                Size szMove = new Size(0, 0);
                if (e == Keys.Right)
                {
                    szMove.Width = 1;
                }
                else if (e == Keys.Down)
                {
                    szMove.Height = 1;
                }
                else if (e == Keys.Left)
                {
                    szMove.Width = -1;
                }
                else if (e == Keys.Up)
                {
                    szMove.Height = -1;
                }
                if (szMove.Width != 0 || szMove.Height != 0)
                {
                    TraiteMove(null, szMove);
                }
            }
            else if ((e & Keys.Control) != 0)
            {
                e = e & ~Keys.Control;
                if (e == Keys.Right)
                {
                    this.SizeCtrlsPlusW();
                }
                else if (e == Keys.Down)
                {
                    this.SizeCtrlsPlusH();
                }
                else if (e == Keys.Left)
                {
                    this.SizeCtrlsMinusW();
                }
                else if (e == Keys.Up)
                {
                    this.SizeCtrlsMinusH();
                }
                else if (e == Keys.C)
                {
                    TreatCopy();
                }
                else if (e == Keys.V)
                {
                    if (AllowDrop)
                        TreatPaste();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void TreatCopy()
        {
            // copier
            Clipboard.Clear();
            GestControl ctrlGest = new GestControl();
            for (int i = 0; i < m_ListSelection.Count; i++)
            {
                ctrlGest.AddObj(((InteractiveControl)m_ListSelection[i]).SourceBTControl);
            }
            XmlDocument clipDoc = new XmlDocument();
            string strRootXml = string.Format("<Root><pid id=\"{0}\" /></Root>", System.Diagnostics.Process.GetCurrentProcess().Id); ;
            try
            {
                clipDoc.LoadXml(strRootXml);
            }
            catch (Exception)
            {
                return;
            }
            ctrlGest.WriteOutForClipBoard(clipDoc, clipDoc.DocumentElement, m_Document);
            Clipboard.SetData(InternalFormat.Name, clipDoc.OuterXml);
        }
    
        /// <summary>
        /// 
        /// </summary>
        public void TreatPaste()
        {
            // coller
            if (Clipboard.ContainsData(InternalFormat.Name))
            {
                IDataObject DataObj = Clipboard.GetDataObject(); 
                string PasteText = DataObj.GetData(InternalFormat.Name) as string;
  
                GestControl ctrlGest = new GestControl();
                if (PasteText != null)
                {
                    XmlDocument clipDoc = new XmlDocument();
                    try
                    {
                        clipDoc.LoadXml(PasteText);
                    }
                    catch (Exception)
                    {
                        return;
                    }
                    int Pid = 0;
                    Traces.LogAddDebug(TraceCat.SmartConfig, "clipDoc.DocumentElement.FirstChild.Name = " + clipDoc.DocumentElement.FirstChild.Name);
                    Traces.LogAddDebug(TraceCat.SmartConfig, "clipDoc.DocumentElement.FirstChild.OuterXml = " + clipDoc.DocumentElement.FirstChild.OuterXml);
                    if (clipDoc.DocumentElement.FirstChild.Name == "pid")
                    {
                        XmlNode AttrPid = clipDoc.DocumentElement.FirstChild.Attributes.GetNamedItem("id");
                        Pid = int.Parse(AttrPid.Value);
                    } 
                    Traces.LogAddDebug(TraceCat.SmartConfig, "Paste en cours avec LoadXml OK");
                    if (ctrlGest.ReadInForClipBoard(clipDoc.DocumentElement, Program.DllGest, this.m_Document))
                    {
                        List<InteractiveControl> ListSrc = new List<InteractiveControl>();
                        List<InteractiveControl> ListNew = new List<InteractiveControl>();
                        for (int i = 0; i< ctrlGest.Count; i++)
                        {
                            Traces.LogAddDebug(TraceCat.SmartConfig, "Un objet ajouté de type " + ((BTControl)ctrlGest[i]).IControl.GetType().ToString());
                            InteractiveControl newICtrl = ((BTControl)ctrlGest[i]).IControl.CreateNew();
                            ListNew.Add(newICtrl);                            
                            ListSrc.Add(((BTControl)ctrlGest[i]).IControl);
                        }
                        bool bDiffInstance = Pid != System.Diagnostics.Process.GetCurrentProcess().Id;
                        Traces.LogAddDebug(TraceCat.SmartConfig, "myPID = " + System.Diagnostics.Process.GetCurrentProcess().Id + " / clipboardPID = " + Pid);
                        InsertDropedItems(ListNew, ListSrc, Point.Empty, bDiffInstance);                            
                    }
                    else
                        Traces.LogAddDebug(TraceCat.SmartConfig, "Erreur ReadInForClipBoard ");
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnControlKeydown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete
                || e.KeyCode == Keys.Back)
            {
                RemoveSelectedControls();
                return;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnControlDoubleClick(object sender, EventArgs e)
        {
            if (EventControlDblClick != null)
                EventControlDblClick();
        }

        #endregion

        #region gestion du drag drop
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnDragEnter(object sender, DragEventArgs e)
        {
            InteractiveControl DropedItem = DropableItems.GetDropableItem(e);
            if (DropedItem != null)
                e.Effect = DragDropEffects.All;
            else
                e.Effect = DragDropEffects.None;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnDragDrop(object sender, DragEventArgs e)
        {
            // on récupère les données de l'objet dropé en correspondance avec ce qu'on veux
            InteractiveControl DropedItem = DropableItems.GetDropableItem(e);
            if (DropedItem != null)
            {
                // si on a bien dropé un InteractiveControl
                // on crée un nouvel objet avec les meme caractéristiques
                // et on le place a la position de la souris
                List<InteractiveControl> ListSrc = new List<InteractiveControl>();
                ListSrc.Add(DropedItem);
                InteractiveControl newControl = DropedItem.CreateNew();
                List<InteractiveControl> ListControls = new List<InteractiveControl>();
                ListControls.Add(newControl);
                InsertDropedItems(ListControls, ListSrc, new Point(e.X, e.Y), false);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ListNew"></param>
        /// <param name="ListSrc"></param>
        /// <param name="PtMouse"></param>
        /// <param name="bFromOtherInstance"></param>
        private void InsertDropedItems(List<InteractiveControl> ListNew, List<InteractiveControl> ListSrc, Point PtMouse, bool bFromOtherInstance)
        {
            //bool bFirstItemDone = false;
            //Point ptFirstInsertedItem;
            // si on a pas le même nombre d'item on sort de suite
            if (ListSrc.Count != ListNew.Count)
                return;
                
            // lorsqu'un control est ajouté, on supprime la séléction
            for (int j = 0; j < m_ListSelection.Count; j++)
            {
                ((IInteractive)m_ListSelection[j]).Selected = false;
            }
            m_ListSelection.Clear();
            
            Point SrcMousePos = Form.MousePosition;
            Traces.LogAddDebug(TraceCat.SmartConfig, "Position ecran de la souris = " + SrcMousePos.ToString());
            Traces.LogAddDebug(TraceCat.SmartConfig, "Position client de la souris = " + PointToClient(SrcMousePos).ToString());
            SrcMousePos = PointToClient(SrcMousePos);
            
            // on commence par déterminer l'item le plus en haut à gauche de la liste, 
            // ainsi que le rectangle total de la sélection
            Point ptTpLeft = new Point(int.MaxValue, int.MaxValue);
            Point ptBtRight = Point.Empty;
            for (int i = 0; i < ListSrc.Count; i++)
            {
                if (ptTpLeft.X > ListSrc[i].Left)
                    ptTpLeft.X = ListSrc[i].Left;
                if (ptTpLeft.Y > ListSrc[i].Top)
                    ptTpLeft.Y = ListSrc[i].Top;

                if (ptBtRight.X < ListSrc[i].Right)
                    ptBtRight.X = ListSrc[i].Right;
                if (ptBtRight.Y < ListSrc[i].Bottom)
                    ptBtRight.Y = ListSrc[i].Bottom;
            }

            // Calcule du rectangle total des objets insérés
            Rectangle rectSel = new Rectangle(ptTpLeft.X, ptTpLeft.Y, 
                                              ptBtRight.X - ptTpLeft.X, ptBtRight.Y - ptTpLeft.Y);

            if (SrcMousePos.X < 0)
                SrcMousePos.X = 0;
            if (SrcMousePos.Y < 0)
                SrcMousePos.Y = 0;
            if (SrcMousePos.X + rectSel.Width > this.Width)
                SrcMousePos.X = SrcMousePos.X - ((SrcMousePos.X + rectSel.Width) - this.Width );
            if (SrcMousePos.Y + rectSel.Height > this.Height)
                SrcMousePos.Y = SrcMousePos.Y - ((SrcMousePos.Y + rectSel.Height) - this.Height ); 

            for (int i = 0; i < ListSrc.Count; i++)
            {
                //PtMouse = PointToClient(PtMouse);
                ListNew[i].Name = ListSrc[i].Name;
                if (Point.Empty != PtMouse)
                {
                    // on repasse en coordonnée client
                    ListNew[i].Location = PointToClient( new Point(PtMouse.X - ListSrc[i].PtMouseDown.X,
                                                         PtMouse.Y - ListSrc[i].PtMouseDown.Y));
                }
                else
                {
                    // on calcule l'offset avec l'item le plus en haut à gauche
                    // dans les objets source. Les objet sont soit à la position ptTpLeft, soit plus
                    // loin en X et en Y 
                    Size offset = new Size(ListSrc[i].Left - ptTpLeft.X, ListSrc[i].Top - ptTpLeft.Y);
                    ListNew[i].Location = new Point(SrcMousePos.X + offset.Width, SrcMousePos.Y + offset.Height);
                }

                if (!ListNew[i].IsDllControl)
                    ListNew[i].ControlType = ListSrc[i].ControlType;

                int newHeigh = ListSrc[i].Size.Height;
                int newWidth = ListSrc[i].Size.Width;
                if (ListNew[i].Size.Height < ListSrc[i].MinSize.Height)
                    newHeigh = ListSrc[i].MinSize.Height;

                if (ListNew[i].Size.Width < ListSrc[i].MinSize.Width)
                    newWidth = ListSrc[i].MinSize.Width;

                ListNew[i].Size = new Size(newWidth, newHeigh);
                ListNew[i].Text = ListSrc[i].Text;
                // et on séléctionne l'objet posé 
                ListNew[i].Selected = true;
                Traces.LogAddDebug(TraceCat.SmartConfig, string.Format("Objet inséré de type {0}, position = {1}",ListNew[i].GetType().ToString()
                                                                                                ,ListNew[i].Location.ToString())
                                                                                                );
                if (EventControlAdded != null)
                    EventControlAdded(ListNew[i], ListSrc[i].SourceBTControl, bFromOtherInstance);

                this.Controls.Add(ListNew[i]);
                m_ListSelection.Add(ListNew[i]);
                //newControl.Focus();
            }
            if (SelectionChange != null)
                SelectionChange();
        }

        #endregion

        #region fonctions de layout
        /// <summary>
        /// 
        /// </summary>
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

        /// <summary>
        /// 
        /// </summary>
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

        /// <summary>
        /// 
        /// </summary>
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

        /// <summary>
        /// 
        /// </summary>
        public void MakeSelectionSameWidth()
        {
            if (m_ListSelection.Count > 1)
            {
                IInteractive FirstCtrl = (IInteractive)m_ListSelection[0];
                int ctrlwidth = FirstCtrl.Width;
                for (int i = 1; i < m_ListSelection.Count; i++)
                {
                    if (((InteractiveControl)m_ListSelection[i]).CanResizeWidth)
                    {
                        ((IInteractive)m_ListSelection[i]).Width = ctrlwidth;
                        ((IInteractive)m_ListSelection[i]).UpdateSelectionLocation();
                    }
                }
            }
            Refresh();
        }

        /// <summary>
        /// 
        /// </summary>
        public void MakeSelectionSameHeight()
        {
            if (m_ListSelection.Count > 1)
            {
                IInteractive FirstCtrl = (IInteractive)m_ListSelection[0];
                int ctrlHeight = FirstCtrl.Height;
                for (int i = 1; i < m_ListSelection.Count; i++)
                {
                    if (((InteractiveControl)m_ListSelection[i]).CanResizeHeight)
                    {
                        ((IInteractive)m_ListSelection[i]).Height = ctrlHeight;
                        ((IInteractive)m_ListSelection[i]).UpdateSelectionLocation();
                    }
                }
            }
            Refresh();
        }

        /// <summary>
        /// 
        /// </summary>
        public void MakeSelectionSameSize()
        {
            if (m_ListSelection.Count > 1)
            {
                IInteractive FirstCtrl = (IInteractive)m_ListSelection[0];
                int ctrlHeight = FirstCtrl.Height;
                int ctrlwidth = FirstCtrl.Width;
                for (int i = 1; i < m_ListSelection.Count; i++)
                {
                    if (((InteractiveControl)m_ListSelection[i]).CanResizeWidth)
                    {
                        ((IInteractive)m_ListSelection[i]).Width = ctrlwidth;
                    }
                    if (((InteractiveControl)m_ListSelection[i]).CanResizeHeight)
                    {
                        ((IInteractive)m_ListSelection[i]).Height = ctrlHeight;
                    }
                    ((IInteractive)m_ListSelection[i]).UpdateSelectionLocation();
                }
            }
            Refresh();
        }

        /// <summary>
        /// 
        /// </summary>
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
                    MessageBox.Show(Program.LangSys.C("Not enought space"), Program.LangSys.C("Error"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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

        /// <summary>
        /// 
        /// </summary>
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
                    MessageBox.Show(Program.LangSys.C("Not enought space"), Program.LangSys.C("Error"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="activeControl"></param>
        /// <returns></returns>
        protected override Point ScrollToControl(Control activeControl)
        {
            return Point.Empty;
        }

        /// <summary>
        /// 
        /// </summary>
        public void SaveAsBitmap()
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "Image File (*.png)|*.png";
            dlg.InitialDirectory = Application.StartupPath;
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                List<Control> tmpHidedCtrlList = new List<Control>(); 
                for (int i = 0; i < this.Controls.Count; i++)
                {
                    InteractiveControl iCtrl = this.Controls[i] as InteractiveControl;
                    
                    if (iCtrl != null)
                    { 
                        // si l'objet à une donnée associée, ou si c'est un control de base
                        // on masque l'objet pour créer le fond de plan
                        if ((iCtrl.SourceBTControl != null && iCtrl.SourceBTControl.HaveDataAssociation)
                            || 
                            (iCtrl.ControlType == InteractiveControlType.Button
                            ||  iCtrl.ControlType == InteractiveControlType.Combo
                            ||  iCtrl.ControlType == InteractiveControlType.CheckBox
                            ||  iCtrl.ControlType == InteractiveControlType.Slider
                            ||  iCtrl.ControlType == InteractiveControlType.NumericUpDown
                            ||  iCtrl.ControlType == InteractiveControlType.Text)
                            )
                        {
                            tmpHidedCtrlList.Add(iCtrl);
                            iCtrl.Selected = false;
                            iCtrl.Visible = false;
                        }
                    }
                }
                m_bDrawGuides = false;
                this.Refresh();
                //getthe instance of the graphics from the control
                using (Graphics g = this.CreateGraphics())
                {
                    //new bitmap object to save the image
                    Bitmap bmp = new Bitmap(this.Width, this.Height);
    
                    //Drawing control to the bitmap
                    this.DrawToBitmap(bmp, new Rectangle(0, 0, this.Width, this.Height));
    
                    bmp.Save(dlg.FileName);
                    bmp.Dispose();
                    for (int i = 0; i < tmpHidedCtrlList.Count; i++)
                    {
                        tmpHidedCtrlList[i].Visible = true;
                    }
                    m_bDrawGuides = true;
                    this.Refresh();
                }
            }

        }

        #region Sizes
        /// <summary>
        /// 
        /// </summary>
        public void SizeCtrlsPlusW()
        {
            if (m_ListSelection.Count >= 1)
            {
                for (int i = 0; i < m_ListSelection.Count; i++)
                {
                    if (((IInteractive)m_ListSelection[i]).Width + 1 <= ((IInteractive)m_ListSelection[i]).MaxSize.Width)
                    {
                        ((IInteractive)m_ListSelection[i]).Width = ((IInteractive)m_ListSelection[i]).Width + 1;
                        ((IInteractive)m_ListSelection[i]).UpdateSelectionLocation();
                    }
                }
            }
            Refresh();
        }

        /// <summary>
        /// 
        /// </summary>
        public void SizeCtrlsMinusW()
        {
            if (m_ListSelection.Count >= 1)
            {
                for (int i = 0; i < m_ListSelection.Count; i++)
                {
                    if (((IInteractive)m_ListSelection[i]).Width - 1 >= ((IInteractive)m_ListSelection[i]).MinSize.Width)
                    {
                        ((IInteractive)m_ListSelection[i]).Width = ((IInteractive)m_ListSelection[i]).Width - 1;
                        ((IInteractive)m_ListSelection[i]).UpdateSelectionLocation();
                    }
                }
            }
            Refresh();
        }

        /// <summary>
        /// 
        /// </summary>
        public void SizeCtrlsPlusH()
        {
            if (m_ListSelection.Count >= 1)
            {
                for (int i = 0; i < m_ListSelection.Count; i++)
                {
                    if (((IInteractive)m_ListSelection[i]).Height + 1 <= ((IInteractive)m_ListSelection[i]).MaxSize.Height)
                    {
                        ((IInteractive)m_ListSelection[i]).Height = ((IInteractive)m_ListSelection[i]).Height + 1;
                        ((IInteractive)m_ListSelection[i]).UpdateSelectionLocation();
                    }
                }
            }
            Refresh();
        }

        /// <summary>
        /// 
        /// </summary>
        public void SizeCtrlsMinusH()
        {
            if (m_ListSelection.Count >= 1)
            {
                for (int i = 0; i < m_ListSelection.Count; i++)
                {
                    if (((IInteractive)m_ListSelection[i]).Height - 1 >= ((IInteractive)m_ListSelection[i]).MinSize.Height)
                    {
                        ((IInteractive)m_ListSelection[i]).Height = ((IInteractive)m_ListSelection[i]).Height - 1;
                        ((IInteractive)m_ListSelection[i]).UpdateSelectionLocation();
                    }
                }
            }
            Refresh();
        }
        #endregion

    }
}

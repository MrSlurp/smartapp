using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using System.Windows.Forms;
using SmartApp.Ihm.Designer;

using CommonLib;

namespace SmartApp.Ihm
{
    public partial class DesignerForm : Form
    {
        #region données membres
        private BTDoc m_Document = null;
        private BTScreen m_Currentscreen = null;
        private DragItemPanel m_panelToolDragItem;
        BasePropertiesDialog m_PropDialog = new BasePropertiesDialog();
        #endregion

        #region attributs de la classe
        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************      
        public BTDoc Doc
        {
            get
            {
                return m_Document;
            }
            set
            {
                m_Document = value;
                m_PanelScreenListAndProp.Doc = m_Document;
                m_PropDialog.Document = m_Document;
            }
        }

        public GestScreen GestScreen
        {
            get
            {
                return m_Document.GestScreen;
            }
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************      
        public GestData GestData
        {
            get
            {
                return m_Document.GestData;
            }
        }
        #endregion

        #region constructeurs et inits
        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************      
        public DesignerForm()
        {
            //Program.LangSys.Initialize(this);
            InitializeComponent();
            m_toolBtnAlignLeft.Image = Resources.AlignLeft;
            m_toolBtnAlignTop.Image = Resources.AlignTop;
            m_toolBtnArrangeAcross.Image = Resources.ArrangeAcross;
            m_toolBtnArrangeDown.Image = Resources.ArrangeDown;
            m_toolBtnMSHeight.Image = Resources.MakeSameHeight;
            m_toolBtnMSWidth.Image = Resources.MakeSameWidth;
            m_toolBtnMSSize.Image = Resources.MakeSameBoth;

            // ce code est déporté de InitializeComponent() 
            // car ca pose un problème dans le designer
            m_tabCTrlConfig.SuspendLayout();
            m_panelToolDragItem = new DragItemPanel();
            // 
            // m_panelToolDragItem
            // 
            m_panelToolDragItem.AutoScroll = true;
            m_panelToolDragItem.BackColor = System.Drawing.Color.Transparent;
            m_panelToolDragItem.Dock = System.Windows.Forms.DockStyle.Fill;
            m_panelToolDragItem.Location = new System.Drawing.Point(3, 3);
            m_panelToolDragItem.Margin = new System.Windows.Forms.Padding(0);
            m_panelToolDragItem.Name = "m_panelToolDragItem";
            m_panelToolDragItem.Size = new System.Drawing.Size(275, 531);
            m_panelToolDragItem.TabIndex = 1;

            m_TabTools.Controls.Add(this.m_panelToolDragItem);
            m_tabCTrlConfig.ResumeLayout(false);
            // fin de code déporté de suspend layout

            m_InteractiveControlContainer.SelectionChange += new SelectionChangeEvent(OnScreenDesignerSelectionChange);
            m_InteractiveControlContainer.EventControlAdded += new IControlAddedEvent(this.OnDesignerControAdded);
            m_InteractiveControlContainer.EventControlRemoved += new IControlRemovedEvent(this.OnDesignerControRemoved);
            m_InteractiveControlContainer.EventControlBringToTop += new IControlBringToTop(this.OnDesignerControBringTop);
            m_InteractiveControlContainer.EventControlDblClick += new ControlDoubleClicked(this.OnControlDblClick);
            m_InteractiveControlContainer.EventControlPosChanged += new ControlsPosChanged(ControlPosChanged);
            m_InteractiveControlContainer.AllowDrop = false;

            m_PanelScreenListAndProp.ScreenDoubleClick += new EventHandler(PanelScreenListAndProp_ScreenDoubleClick);

            OnScreenDesignerSelectionChange();
            this.m_PanelScreenListAndProp.SelectedScreenChange += new ScreenPropertiesChange(this.OnSelectedScreenChange);
        }

        void PanelScreenListAndProp_ScreenDoubleClick(object sender, EventArgs e)
        {
            OnControlDblClick();
        }

        void SubInitComponent()
        {

        }

        void ControlPosChanged()
        {
            m_Document.Modified = true;
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************      
        public void Initialize()
        {
            m_PanelScreenListAndProp.Initialize();
            OnSelectedScreenChange(null);
            m_InteractiveControlContainer.ClearSelection();
        }
        #endregion

        #region event handlers
        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************      
        private void OnScreenDesignerSelectionChange()
        {
            UpdateLayoutToolBarButtons();
            if (m_InteractiveControlContainer.SelectionCount >= 1)
            {
                m_PropDialog.ConfiguredItem = m_InteractiveControlContainer.FirstSelected.SourceBTControl;
            }
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************      
        private void OnFormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                this.WindowState = FormWindowState.Minimized;
                e.Cancel = true;
            }
            else if (e.CloseReason == CloseReason.MdiFormClosing)
            {
            }
        }
        #endregion

        #region fonctions d'update de l'IHM
        //*****************************************************************************************************
        // Description: met a jour l'état des boutons de la tool bar de mise en forme en fonction de la selection
        // du designer (InteractiveControlContainer)
        // Return: /
        //*****************************************************************************************************      
        private void UpdateLayoutToolBarButtons()
        {
            if (m_InteractiveControlContainer.SelectionCount <= 1)
            {
                m_toolBtnAlignLeft.Enabled = false;
                m_toolBtnAlignTop.Enabled = false;
                m_toolBtnArrangeAcross.Enabled = false;
                m_toolBtnArrangeDown.Enabled = false;
            }
            else
            {
                m_toolBtnAlignLeft.Enabled = true;
                m_toolBtnAlignTop.Enabled = true;
                m_toolBtnArrangeAcross.Enabled = true;
                m_toolBtnArrangeDown.Enabled = true;
            }
            if (m_InteractiveControlContainer.SelectionCount >= 1)
            {
                m_toolBtnBringToFront.Enabled = true;
                toolStripDropDownButton1.Enabled = true;
                toolStripDropDownButton2.Enabled = true;
            }
            else
            {
                m_toolBtnBringToFront.Enabled = false;
                toolStripDropDownButton1.Enabled = false;
                toolStripDropDownButton2.Enabled = false;
            }


            if (m_InteractiveControlContainer.SelectionAbilities == SelectionAbilitiesValues.UnableResize)
            {
                m_toolBtnMSHeight.Enabled = false;
                m_toolBtnMSWidth.Enabled = false;
                m_toolBtnMSSize.Enabled = false;
            }
            else if (m_InteractiveControlContainer.SelectionAbilities == SelectionAbilitiesValues.AbleResizeBoth)
            {
                m_toolBtnMSHeight.Enabled = true;
                m_toolBtnMSWidth.Enabled = true;
                m_toolBtnMSSize.Enabled = true;
            }
            else if (m_InteractiveControlContainer.SelectionAbilities == SelectionAbilitiesValues.AbleResizeWidth)
            {
                m_toolBtnMSHeight.Enabled = false;
                m_toolBtnMSWidth.Enabled = true;
                m_toolBtnMSSize.Enabled = false;
            }
            else if (m_InteractiveControlContainer.SelectionAbilities == SelectionAbilitiesValues.AbleResizeHeight)
            {
                m_toolBtnMSHeight.Enabled = true;
                m_toolBtnMSWidth.Enabled = false;
                m_toolBtnMSSize.Enabled = false;
            }
        }

        #endregion

        #region Event de la tool bar de layout
        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        private void AlignLeftClick(object sender, EventArgs e)
        {
            m_InteractiveControlContainer.AlignSelectionLeft();
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        private void AlignTopClick(object sender, EventArgs e)
        {
            m_InteractiveControlContainer.AlignSelectionTop();
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        private void MSWidthClick(object sender, EventArgs e)
        {
            m_InteractiveControlContainer.MakeSelectionSameWidth();
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        private void MSHeightClick(object sender, EventArgs e)
        {
            m_InteractiveControlContainer.MakeSelectionSameHeight();
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        private void MSSizeClick(object sender, EventArgs e)
        {
            m_InteractiveControlContainer.MakeSelectionSameSize();
        }
        #endregion

        #region fonctions de gestion du transfert des données Designer => objet && objet ==> designer
        //*****************************************************************************************************
        // Description: appelé lorsque l'écran séléctionné a changé
        // Return: /
        //*****************************************************************************************************      
        public void OnSelectedScreenChange(BTScreen Scr)
        {
            m_Currentscreen = Scr;
            m_PropDialog.CurrentScreen = Scr;
            m_PropDialog.ConfiguredItem = Scr;
            if (m_Currentscreen == null)
            {
                // si il n'y a pas d'écran selectionné
                // on ne peux pas droper dans le designer
                m_InteractiveControlContainer.AllowDrop = false;
                m_InteractiveControlContainer.ScreenBckImage = null;
                m_LabelSelectedScreen.Text = Program.LangSys.C("No selection");
                toolbtnScreenToBitmap.Enabled = false;
                tsbtn_copy.Enabled = false;
                tsbtn_paste.Enabled = false;
                UpdateDesignerFromScreen(null);
                return;
            }
            m_LabelSelectedScreen.Text = Scr.Symbol;
            m_InteractiveControlContainer.AllowDrop = true;
            toolbtnScreenToBitmap.Enabled = true;
            tsbtn_copy.Enabled = true;
            tsbtn_paste.Enabled = true;

            try
            {
                string chemincomplet = PathTranslator.RelativePathToAbsolute(m_Currentscreen.BackPictureFile);
                chemincomplet = PathTranslator.LinuxVsWindowsPathUse(chemincomplet);
                if (!string.IsNullOrEmpty(chemincomplet)
                    && File.Exists(chemincomplet))
                {
                    Bitmap imgBack = new Bitmap(chemincomplet);
                    imgBack.MakeTransparent(Cste.TransparencyColor);
                    m_InteractiveControlContainer.ScreenBckImage = imgBack;
                }
                else
                {
                    m_InteractiveControlContainer.ScreenBckImage = null;
                }
            }
            catch (Exception )
            {
                m_InteractiveControlContainer.ScreenBckImage = null;
                Traces.LogAddDebug(TraceCat.SmartConfig, "Erreur chargement du fichier fond de plan");
            }
            // on met a jour le designer avec l'écran séléctionné
            UpdateDesignerFromScreen(m_Currentscreen);
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************      
        private void UpdateDesignerFromScreen(BTScreen Scr)
        {
            m_InteractiveControlContainer.Controls.Clear();
            if (Scr != null)
            {
                for (int i = Scr.Controls.Count -1; i >= 0 ; i--)
                {
                    BTControl Ctrl = (BTControl)Scr.Controls[i];
                    m_InteractiveControlContainer.Controls.Add(Ctrl.IControl);
                }
            }
        }

        //*****************************************************************************************************
        // Description: appelé lorsqu'on ajoute manuellement un control au designer
        // ajoute le control dans la liste des controls de l'écran
        // Return: /
        //*****************************************************************************************************      
        private void OnDesignerControAdded(InteractiveControl ctrl, BTControl SrcBtControl,bool bFromOtherInstance)
        {
            if (m_Currentscreen == null)
                return;

            BTControl NewCtrl = null;
            if (ctrl.IsDllControl)
                NewCtrl = Program.DllGest[ctrl.DllControlID].CreateBTControl(ctrl);
            else
                NewCtrl = BTControl.CreateNewBTControl(ctrl);

            if (SrcBtControl != null )
            {
                NewCtrl.CopyParametersFrom(SrcBtControl, bFromOtherInstance);
            }

            NewCtrl.Symbol = m_Currentscreen.Controls.GetNextDefaultSymbol();
            m_Currentscreen.Controls.AddObj(NewCtrl);
            NewCtrl.Parent = m_Currentscreen;
            m_Document.Modified = true;
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************      
        private void OnDesignerControRemoved(InteractiveControl ctrl)
        {
            if (m_Currentscreen == null)
                return;

            BTControl Ctrl = ctrl.SourceBTControl;
            if (Ctrl != null)
            {
                m_Currentscreen.Controls.RemoveObj(Ctrl);
            }
            m_Document.Modified = true;
        }


        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************      
        private void OnControlDblClick()
        {
            m_tabCTrlConfig.SelectedIndex = 2; // TODO constante pour la page des controls
            if (m_PropDialog.ConfiguredItem != null)
            {
                m_PropDialog.Initialize();
                m_PropDialog.ShowDialog();
            }
        }

        #endregion

        #region fonctions "helpers"
        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************      
        /*
        public BTControl FindControlFromIControl(InteractiveControl iCtrl)
        {
            if (m_Currentscreen != null)
            {
                for (int i = 0; i < m_Currentscreen.Controls.Count; i++)
                {
                    BTControl Ctrl = (BTControl)m_Currentscreen.Controls[i];
                    if (Ctrl.IControl == iCtrl)
                        return Ctrl;
                }
            }
            return null;
        }
         * */
        #endregion

        private void OnDesignerKeyDown(object sender, KeyEventArgs e)
        {
        }

        #region arrange functions
        private void m_toolBtnArrangeAcross_Click(object sender, EventArgs e)
        {
            m_InteractiveControlContainer.ArrangeItemsAcross();
        }

        private void m_toolBtnArrangeDown_Click(object sender, EventArgs e)
        {
            m_InteractiveControlContainer.ArrangeItemsDown();
        }
        #endregion

        private void m_toolBtnBringToFront_Click(object sender, EventArgs e)
        {
            m_InteractiveControlContainer.LayoutBringToFront();
        }

        private void OnDesignerControBringTop(InteractiveControl ctrl)
        {
            if (m_Currentscreen != null)
            {
                m_Currentscreen.Controls.BringControlToTop(ctrl.SourceBTControl);
            }
        }

        private void toolbtnScreenToBitmap_Click(object sender, EventArgs e)
        {
            m_InteractiveControlContainer.SaveAsBitmap();
        }

        #region size functions
        private void tsbtn_increaseWidth_Click(object sender, EventArgs e)
        {
            m_InteractiveControlContainer.SizeCtrlsPlusW();
        }

        private void tsbtn_decreaseWidth_Click(object sender, EventArgs e)
        {
            m_InteractiveControlContainer.SizeCtrlsMinusW();
        }

        private void tsbtn_increaseHeight_Click(object sender, EventArgs e)
        {
            m_InteractiveControlContainer.SizeCtrlsPlusH();
        }

        private void tsbtn_decreaseHeight_Click(object sender, EventArgs e)
        {
            m_InteractiveControlContainer.SizeCtrlsMinusH();
        }
        #endregion

        #region move functions
        private void tsbtn_moveLeft_Click(object sender, EventArgs e)
        {
            Size szMove = new Size(-1, 0);
            m_InteractiveControlContainer.TraiteMove(null, szMove);
        }

        private void tsbtn_moveRight_Click(object sender, EventArgs e)
        {
            Size szMove = new Size(1, 0);
            m_InteractiveControlContainer.TraiteMove(null, szMove);
        }

        private void tsbtn_moveUp_Click(object sender, EventArgs e)
        {
            Size szMove = new Size(0, -1);
            m_InteractiveControlContainer.TraiteMove(null, szMove);
        }

        private void tsbtn_moveDown_Click(object sender, EventArgs e)
        {
            Size szMove = new Size(0, 1);
            m_InteractiveControlContainer.TraiteMove(null, szMove);
        }
    
        private void tsbtn_copy_click(object sender, EventArgs e)
        {
            m_InteractiveControlContainer.TreatCopy();            
        }
    
        private void tsbtn_paste_click(object sender, EventArgs e)
        {
            m_InteractiveControlContainer.TreatPaste();            
        }
        #endregion
    }
}
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
                m_PanelControlProperties.Doc = m_Document;
                m_PanelScreenInitScript.Title = "Screen Init Script";
                m_PanelScreenInitScript.Doc = m_Document;
                m_PanelScreenEventScript.Title = "Screen Event Script";
                m_PanelScreenEventScript.Doc = m_Document;
                m_PanelCtrlEventScript.Title = "Control Event Script";
                m_PanelCtrlEventScript.Doc = m_Document;
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
            InitializeComponent();
            this.m_toolBtnAlignLeft.Image = Resources.AlignLeft;
            this.m_toolBtnAlignTop.Image = Resources.AlignTop;
            this.m_toolBtnArrangeAcross.Image = Resources.ArrangeAcross;
            this.m_toolBtnArrangeDown.Image = Resources.ArrangeDown;
            this.m_toolBtnMSHeight.Image = Resources.MakeSameHeight;
            this.m_toolBtnMSWidth.Image = Resources.MakeSameWidth;
            this.m_toolBtnMSSize.Image = Resources.MakeSameBoth;
            m_InteractiveControlContainer.SelectionChange += new SelectionChangeEvent(OnScreenDesignerSelectionChange);
            m_InteractiveControlContainer.EventControlAdded += new IControlAddedEvent(this.OnDesignerControAdded);
            m_InteractiveControlContainer.EventControlRemoved += new IControlRemovedEvent(this.OnDesignerControRemoved);
            m_InteractiveControlContainer.EventControlBringToTop += new IControlBringToTop(this.OnDesignerControBringTop);
            m_InteractiveControlContainer.EventCanChangeSelection += new CanChangeSelectionEvent(this.OnControlContainerAskChangeSelection);
            m_InteractiveControlContainer.EventControlDblClick += new ControlDoubleClicked(this.OnControlDblClick);
            m_InteractiveControlContainer.EventControlPosChanged += new ControlsPosChanged(ControlPosChanged);
            m_InteractiveControlContainer.AllowDrop = false;
            OnScreenDesignerSelectionChange();
            this.m_PanelScreenListAndProp.SelectedScreenChange += new ScreenPropertiesChange(this.OnSelectedScreenChange);
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
            UpdateOptionPanel();
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
            UpdateOptionPanel();
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************      
        private void OnFormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                if (m_PanelControlProperties.IsDataValuesValid)
                {
                    this.WindowState = FormWindowState.Minimized;
                }
                e.Cancel = true;
            }
            else if (e.CloseReason == CloseReason.MdiFormClosing)
            {
                if (!m_PanelControlProperties.IsDataValuesValid)
                    e.Cancel = true;
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
            }
            else
            {
                m_toolBtnBringToFront.Enabled = false;
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

        //*****************************************************************************************************
        // Description: affiche le panel correspondant au premier objet selectionné si la selection ne contiens
        // qu'un seul objet
        // Return: /
        //*****************************************************************************************************      
        private void UpdateOptionPanel()
        {
            UpdateScriptFromControlType();
            if (m_InteractiveControlContainer.SelectionCount == 0)
            {
                m_PanelControlProperties.Enabled = false;
                m_PanelControlProperties.BTControl = null;
                //m_PanelCtrlEventScript.Enabled = false;
            }
            else if (m_InteractiveControlContainer.SelectionCount ==1)
            {
                m_PanelControlProperties.Enabled = true;
                m_PanelControlProperties.BTControl = m_InteractiveControlContainer.FirstSelected.SourceBTControl;
                //m_PanelCtrlEventScript.ScriptableItem = m_PanelControlProperties.BTControl;
                //m_PanelCtrlEventScript.Enabled = true;
            }
            else
            {
                //m_PanelCtrlEventScript.ScriptableItem = m_PanelControlProperties.BTControl;
                //m_PanelCtrlEventScript.Enabled = false;
                m_PanelControlProperties.Enabled = false;
            }
            
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        protected void UpdateScriptFromControlType()
        {

            BTControl ctrl = null;
            if (m_InteractiveControlContainer.FirstSelected != null)
                ctrl = m_InteractiveControlContainer.FirstSelected.SourceBTControl;

            if (ctrl == null)
            {
                m_PanelCtrlEventScript.ScriptableItem = null;
                return;
            }

            switch (ctrl.IControl.ControlType)
            {
                case InteractiveControlType.Button:
                case InteractiveControlType.CheckBox:
                case InteractiveControlType.Combo:
                    m_PanelCtrlEventScript.ScriptableItem = ctrl;
                    break;
                case InteractiveControlType.Text:
                case InteractiveControlType.NumericUpDown:
                case InteractiveControlType.Slider:
                    m_PanelCtrlEventScript.ScriptableItem = null;
                    break;
                case InteractiveControlType.SpecificControl:
                    if (((ISpecificControl)ctrl.IControl).StdPropEnabling.m_bCtrlEventScriptEnabled)
                        m_PanelCtrlEventScript.ScriptableItem = ctrl;
                    else
                        m_PanelCtrlEventScript.ScriptableItem = null;
                    break;
                case InteractiveControlType.DllControl:
                    if (((ISpecificControl)ctrl.IControl).StdPropEnabling.m_bCtrlEventScriptEnabled)
                        m_PanelCtrlEventScript.ScriptableItem = ctrl;
                    else
                        m_PanelCtrlEventScript.ScriptableItem = null;
                    break;
                default:
                    System.Diagnostics.Debug.Assert(false);
                    break;
            }
            if (m_InteractiveControlContainer.SelectionCount > 1)
            {
                m_PanelCtrlEventScript.Enabled = false;
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
            if (m_Currentscreen == null)
            {
                // si il n'y a pas d'écran selectionné
                // on ne peux pas droper dans le designer
                this.m_PanelControlProperties.GestControl = null;
                m_InteractiveControlContainer.AllowDrop = false;
                m_InteractiveControlContainer.ScreenBckImage = null;
                m_PanelScreenInitScript.InitScriptableItem = null;
                m_PanelScreenEventScript.ScriptableItem = null;
                m_PanelCtrlEventScript.ScriptableItem = null;
                m_LabelSelectedScreen.Text = "No selection";
                UpdateDesignerFromScreen(null);
                return;
            }
            m_LabelSelectedScreen.Text = Scr.Symbol;
            m_PanelScreenInitScript.InitScriptableItem = m_Currentscreen;
            m_PanelScreenEventScript.ScriptableItem = m_Currentscreen;
            m_InteractiveControlContainer.AllowDrop = true;
            try
            {
                if (!string.IsNullOrEmpty(m_Currentscreen.BackPictureFile)
                    && File.Exists(m_Currentscreen.BackPictureFile))
                {
                    Bitmap imgBack = new Bitmap(m_Currentscreen.BackPictureFile);
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
            }
            // on met a jour le designer avec l'écran séléctionné
            UpdateDesignerFromScreen(m_Currentscreen);
            this.m_PanelControlProperties.GestControl = m_Currentscreen.Controls;
            UpdateOptionPanel();
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
        private void OnDesignerControAdded(InteractiveControl ctrl)
        {
            if (m_Currentscreen == null)
                return;

            BTControl NewCtrl = null;
            if (ctrl.IsDllControl)
                NewCtrl = Program.DllGest[ctrl.DllControlID].CreateBTControl(ctrl);
            else
                NewCtrl = BTControl.CreateNewBTControl(ctrl);

            NewCtrl.Symbol = m_Currentscreen.Controls.GetNextDefaultSymbol();
            m_Currentscreen.Controls.AddObj(NewCtrl);
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
        private bool OnControlContainerAskChangeSelection()
        {
            return m_PanelControlProperties.IsDataValuesValid;
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************      
        private void OnControlDblClick()
        {
            m_tabCTrlConfig.SelectedIndex = 2; // TODO constante pour la page des controls
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

        private void m_toolBtnArrangeAcross_Click(object sender, EventArgs e)
        {
            m_InteractiveControlContainer.ArrangeItemsAcross();
        }

        private void m_toolBtnArrangeDown_Click(object sender, EventArgs e)
        {
            m_InteractiveControlContainer.ArrangeItemsDown();
        }

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
    }
}
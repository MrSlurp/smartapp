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
        BasePropertiesDialog m_PropDialog = new BasePropertiesDialog();
        #endregion

        #region attributs de la classe
        /// <summary>
        /// 
        /// </summary>
        public BTDoc Doc
        {
            get
            {
                return m_Document;
            }
            set
            {
                m_Document = value;
                m_PropDialog.Document = m_Document;
                m_InteractiveControlContainer.m_Document = m_Document;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public GestScreen GestScreen
        {
            get
            {
                return m_Document.GestScreen;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public GestData GestData
        {
            get
            {
                return m_Document.GestData;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public BTScreen CurrentScreen
        {
            get { return m_Currentscreen; }
            set
            {
                if (m_Currentscreen != null)
                {
                    m_Currentscreen.PropertiesChanged -= ScreenPropsChanged;
                }
                m_Currentscreen = value;
                if (m_Currentscreen != null)
                {
                    m_Currentscreen.PropertiesChanged += ScreenPropsChanged;
                }
                SelectedScreenChange();
            }
        }
        #endregion

        #region constructeurs et inits
        /// <summary>
        /// 
        /// </summary>
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

            // fin de code déporté de suspend layout
            m_InteractiveControlContainer.SelectionChange += new SelectionChangeEvent(OnScreenDesignerSelectionChange);
            m_InteractiveControlContainer.EventControlAdded += new IControlAddedEvent(this.OnDesignerControAdded);
            m_InteractiveControlContainer.EventControlRemoved += new IControlRemovedEvent(this.OnDesignerControRemoved);
            m_InteractiveControlContainer.EventControlBringToTop += new IControlBringToTop(this.OnDesignerControBringTop);
            m_InteractiveControlContainer.EventControlDblClick += new ControlDoubleClicked(this.OnControlDblClick);
            m_InteractiveControlContainer.AllowDrop = false;


            OnScreenDesignerSelectionChange();
        }

        /// <summary>
        /// 
        /// </summary>
        public void Initialize()
        {
            SelectedScreenChange();
            m_InteractiveControlContainer.ClearSelection();
        }
        #endregion

        #region event handlers
        /// <summary>
        /// 
        /// </summary>
        private void OnScreenDesignerSelectionChange()
        {
            UpdateLayoutToolBarButtons();
            if (m_InteractiveControlContainer.SelectionCount >= 1)
            {
                m_PropDialog.ConfiguredItem = m_InteractiveControlContainer.FirstSelected.SourceBTControl;
            }
        }

        #endregion

        #region fonctions d'update de l'IHM
        /// <summary>
        /// 
        /// </summary>
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
        protected void ScreenPropsChanged(BaseObject bobj)
        {
            SelectedScreenChange();
        }

        /// <summary>
        /// 
        /// </summary>
        protected void SelectedScreenChange()
        {
            m_PropDialog.CurrentScreen = m_Currentscreen;
            m_PropDialog.ConfiguredItem = m_Currentscreen;
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
                m_InteractiveControlContainer.CustomLineSize = new Size(-1,-1);
                return;
            }
            string strFileName = Path.GetFileNameWithoutExtension(m_Document.FileName);
            m_LabelSelectedScreen.Text = m_Currentscreen.Symbol + " @ " + strFileName;
            m_InteractiveControlContainer.AllowDrop = true;
            toolbtnScreenToBitmap.Enabled = true;
            tsbtn_copy.Enabled = true;
            tsbtn_paste.Enabled = true;
            m_InteractiveControlContainer.CustomLineSize = m_Currentscreen.ScreenSize;
            try
            {
                string chemincomplet = m_Document.PathTr.RelativePathToAbsolute(m_Currentscreen.BackPictureFile);
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
            m_InteractiveControlContainer.Refresh();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Scr"></param>
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

        /// <summary>
        /// appelé lorsqu'on ajoute manuellement un control au designer
        /// ajoute le control dans la liste des controls de l'écran
        /// </summary>
        /// <param name="ctrl"></param>
        /// <param name="SrcBtControl"></param>
        /// <param name="bFromOtherInstance"></param>
        private void OnDesignerControAdded(InteractiveControl ctrl, BTControl SrcBtControl,bool bFromOtherInstance)
        {
            if (m_Currentscreen == null)
                return;

            BTControl NewCtrl = null;
            if (ctrl.IsDllControl)
                NewCtrl = Program.DllGest[ctrl.DllControlID].CreateBTControl(m_Document, ctrl);
            else
                NewCtrl = BTControl.CreateNewBTControl(ctrl, m_Document);

            if (SrcBtControl != null )
            {
                NewCtrl.CopyParametersFrom(SrcBtControl, bFromOtherInstance, m_Document);
            }

            NewCtrl.Symbol = m_Currentscreen.Controls.GetNextDefaultSymbol();
            m_Currentscreen.Controls.AddObj(NewCtrl);
            NewCtrl.Parent = m_Currentscreen;
            m_Document.Modified = true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ctrl"></param>
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


        /// <summary>
        /// 
        /// </summary>
        private void OnControlDblClick()
        {
            if (m_PropDialog.ConfiguredItem != null)
            {
                m_PropDialog.Initialize();
                DialogResult dlgRes = m_PropDialog.ShowDialog();
            }
        }

        #endregion
 
        #region arrange functions
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void m_toolBtnArrangeAcross_Click(object sender, EventArgs e)
        {
            m_InteractiveControlContainer.ArrangeItemsAcross();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void m_toolBtnArrangeDown_Click(object sender, EventArgs e)
        {
            m_InteractiveControlContainer.ArrangeItemsDown();
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void m_toolBtnBringToFront_Click(object sender, EventArgs e)
        {
            m_InteractiveControlContainer.LayoutBringToFront();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ctrl"></param>
        private void OnDesignerControBringTop(InteractiveControl ctrl)
        {
            if (m_Currentscreen != null)
            {
                m_Currentscreen.Controls.BringControlToTop(ctrl.SourceBTControl);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolbtnScreenToBitmap_Click(object sender, EventArgs e)
        {
            m_InteractiveControlContainer.SaveAsBitmap();
        }

        #region size functions
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbtn_increaseWidth_Click(object sender, EventArgs e)
        {
            m_InteractiveControlContainer.SizeCtrlsPlusW();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbtn_decreaseWidth_Click(object sender, EventArgs e)
        {
            m_InteractiveControlContainer.SizeCtrlsMinusW();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbtn_increaseHeight_Click(object sender, EventArgs e)
        {
            m_InteractiveControlContainer.SizeCtrlsPlusH();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbtn_decreaseHeight_Click(object sender, EventArgs e)
        {
            m_InteractiveControlContainer.SizeCtrlsMinusH();
        }
        #endregion

        #region move functions
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbtn_moveLeft_Click(object sender, EventArgs e)
        {
            Size szMove = new Size(-1, 0);
            m_InteractiveControlContainer.TraiteMove(null, szMove);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbtn_moveRight_Click(object sender, EventArgs e)
        {
            Size szMove = new Size(1, 0);
            m_InteractiveControlContainer.TraiteMove(null, szMove);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbtn_moveUp_Click(object sender, EventArgs e)
        {
            Size szMove = new Size(0, -1);
            m_InteractiveControlContainer.TraiteMove(null, szMove);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbtn_moveDown_Click(object sender, EventArgs e)
        {
            Size szMove = new Size(0, 1);
            m_InteractiveControlContainer.TraiteMove(null, szMove);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbtn_copy_click(object sender, EventArgs e)
        {
            m_InteractiveControlContainer.TreatCopy();            
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbtn_paste_click(object sender, EventArgs e)
        {
            m_InteractiveControlContainer.TreatPaste();            
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DesignerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            m_InteractiveControlContainer.Controls.Clear();
            m_Currentscreen.PropertiesChanged -= this.ScreenPropsChanged;
        }

        private void tsMenuGrid_Click(object sender, EventArgs e)
        {
            if (sender == tsMenuGridOff)
            {
                m_InteractiveControlContainer.GridSpacing = 0;
            }
            else if (sender == tsMenuGrid20)
            {
                m_InteractiveControlContainer.GridSpacing = 20;
            }
            else if (sender == tsMenuGrid40)
            {
                m_InteractiveControlContainer.GridSpacing = 40;
            }
        }
    }
}
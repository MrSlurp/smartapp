using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using CommonLib;

namespace CommonLib
{
    public partial class ScreenItemStdPropertiesPanel : BaseControlPropertiesPanel, IObjectPropertyPanel
    {
        #region données membres
        /// <summary>
        /// 
        /// </summary>
        private UserControl m_CurrentSpecificControlPropPanel = null;

        /// <summary>
        /// 
        /// </summary>
        private Font m_CtrlFont = null;

        #endregion

        #region constructeur
        /// <summary>
        /// 
        /// </summary>
        public ScreenItemStdPropertiesPanel()
        {
            InitializeComponent();
        }
        #endregion

        #region attribut d'accès aux valeurs de la page de propriété

        /// <summary>
        /// 
        /// </summary>
        public string Txt
        {
            get { return m_EditText.Text; }
            set { m_EditText.Text = value; }
        }

        public Font CtrlFont
        {
            get { return m_CtrlFont; }
            set { m_CtrlFont = value; }
        }

        public Color CtrlFontColor
        {
            get { return this.panel_fontColor.BackColor; }
            set { this.panel_fontColor.BackColor = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string AssociateData
        {
            get { return m_EditAssociateData.Text; }
            set { m_EditAssociateData.Text = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IsReadOnly
        {
            get { return m_checkReadOnly.Checked; }
            set { m_checkReadOnly.Checked = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool UseScreenEvent
        {
            get { return m_checkScreenEvent.Checked; }
            set { m_checkScreenEvent.Checked = value; }
        }

        #endregion

        #region validation des données
        /// <summary>
        /// 
        /// </summary>
        public override bool IsObjectPropertiesValid
        {
            get
            {
                if (this.m_Control == null)
                    return true;

                bool bRet = true;
                if (!string.IsNullOrEmpty(this.AssociateData))
                {
                    Data dat = (Data)this.m_Document.GestData.GetFromSymbol(this.AssociateData);
                    if (dat == null)
                        bRet = false;
                }
                if (m_CurrentSpecificControlPropPanel != null
                    && !((ISpecificPanel)m_CurrentSpecificControlPropPanel).IsObjectPropertiesValid)
                    bRet = false;


                return bRet;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override bool ValidateProperties()
        {
            if (this.m_Control == null)
                return true;

            bool bRet = true;
            string strMessage = "";
            if (!string.IsNullOrEmpty(this.AssociateData))
            {
                Data dat = (Data)this.m_Document.GestData.GetFromSymbol(this.AssociateData);
                if (bRet && dat == null)
                {
                    strMessage = string.Format(Lang.LangSys.C("Associate data {0} is not valid"), this.AssociateData);
                    bRet = false;
                }
            }
            if (!bRet)
            {
                MessageBox.Show(strMessage, Lang.LangSys.C("Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return bRet;
            }
            if (m_CurrentSpecificControlPropPanel != null
                && !((ISpecificPanel)m_CurrentSpecificControlPropPanel).ValidateProperties())
            {
                bRet = false;
            }

            return bRet;
        }

        /// <summary>
        /// 
        /// </summary>
        public void ObjectToPanel()
        {
            UpdateStateFromControlType();
            this.AssociateData = m_Control.AssociateData; ;
            this.IsReadOnly = m_Control.IsReadOnly;
            this.UseScreenEvent = m_Control.UseScreenEvent;
            this.Txt = m_Control.IControl.Text;
            this.CtrlFont = m_Control.TextFont;
            this.CtrlFontColor = m_Control.TextColor;
            if (m_CurrentSpecificControlPropPanel != null)
            {
                ((ISpecificPanel)m_CurrentSpecificControlPropPanel).Document = this.Document;
                ((ISpecificPanel)m_CurrentSpecificControlPropPanel).ObjectToPanel();
            }

        }

        /// <summary>
        /// 
        /// </summary>
        public void PanelToObject()
        {
            bool bDataPropChange = false;
            if (m_Control.AssociateData != this.AssociateData)
                bDataPropChange |= true;
            if (m_Control.IsReadOnly != this.IsReadOnly)
                bDataPropChange |= true;
            if (m_Control.UseScreenEvent != this.UseScreenEvent)
                bDataPropChange |= true;
            if (this.Txt != m_Control.IControl.Text)
                bDataPropChange |= true;

            if (this.CtrlFont != m_Control.TextFont)
                bDataPropChange |= true;
            if (this.CtrlFontColor != m_Control.TextColor)
                bDataPropChange |= true;


            if (bDataPropChange)
            {
                m_Control.AssociateData = this.AssociateData;
                m_Control.IsReadOnly = this.IsReadOnly;
                m_Control.UseScreenEvent = this.UseScreenEvent;
                m_Control.IControl.Text = this.Txt;
                m_Control.TextFont = this.CtrlFont;
                m_Control.TextColor = this.CtrlFontColor;
                Document.Modified = true;
            }
            if (m_CurrentSpecificControlPropPanel != null)
            {
                ((ISpecificPanel)m_CurrentSpecificControlPropPanel).PanelToObject();
            }
        }

        #endregion

        #region handlers d'évènement
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnEditAssociateDataDragDrop(object sender, DragEventArgs e)
        {
            Data DropedItem = (Data)e.Data.GetData(typeof(Data));
            if (DropedItem != null)
            {
                this.AssociateData = DropedItem.Symbol;
                m_EditAssociateData.Focus();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnEditAssociateDataDragEnter(object sender, DragEventArgs e)
        {
            Data dt = (Data)e.Data.GetData(typeof(Data));
            if (dt != null)
                e.Effect = DragDropEffects.All;
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        protected void UpdateStateFromControlType()
        {
            if (m_Control == null)
            {
                SetSpecificPanelProp(null);
                return;
            }

            switch (m_Control.IControl.ControlType)
            {
                case InteractiveControlType.Button:
                    EnableTextEdition(true);
                    EnableFontSelection(true);
                    m_EditAssociateData.Enabled = true;
                    btn_pickdata.Enabled = true;
                    m_checkReadOnly.Enabled = false;
                    m_checkScreenEvent.Enabled = true;
                    SetSpecificPanelProp(null);
                    break;
                case InteractiveControlType.CheckBox:
                case InteractiveControlType.Combo:
                    EnableTextEdition(true);
                    EnableFontSelection(true);
                    m_EditAssociateData.Enabled = true;
                    btn_pickdata.Enabled = true;
                    m_checkReadOnly.Enabled = true;
                    m_checkScreenEvent.Enabled = true;
                    SetSpecificPanelProp(null);
                    break;
                case InteractiveControlType.Text:
                    EnableTextEdition(true);
                    EnableFontSelection(true);
                    m_EditAssociateData.Enabled = true;
                    btn_pickdata.Enabled = true;
                    m_checkReadOnly.Enabled = false;
                    m_checkScreenEvent.Enabled = false;
                    SetSpecificPanelProp(null);
                    break;
                case InteractiveControlType.NumericUpDown:
                    EnableTextEdition(false);
                    EnableFontSelection(true);
                    m_EditAssociateData.Enabled = true;
                    btn_pickdata.Enabled = true;
                    m_checkReadOnly.Enabled = true;
                    m_checkScreenEvent.Enabled = false;
                    m_checkScreenEvent.Checked = false;
                    SetSpecificPanelProp(null);
                    break;
                case InteractiveControlType.Slider:
                    EnableTextEdition(false);
                    m_EditAssociateData.Enabled = true;
                    btn_pickdata.Enabled = true;
                    m_checkReadOnly.Enabled = true;
                    m_checkScreenEvent.Enabled = false;
                    m_checkScreenEvent.Checked = false;
                    SetSpecificPanelProp(null);
                    break;
                case InteractiveControlType.SpecificControl:
                case InteractiveControlType.DllControl:
                    StandardPropEnabling stdProps = ((ISpecificControl)m_Control.IControl).StdPropEnabling;
                    m_EditAssociateData.Enabled = stdProps.m_bEditAssociateDataEnabled;
                    btn_pickdata.Enabled = stdProps.m_bEditAssociateDataEnabled;
                    m_checkReadOnly.Enabled = stdProps.m_bcheckReadOnlyEnabled;
                    m_checkReadOnly.Checked = stdProps.m_bcheckReadOnlyChecked;
                    m_checkScreenEvent.Enabled = stdProps.m_bcheckScreenEventEnabled;
                    m_checkScreenEvent.Checked = stdProps.m_bcheckScreenEventChecked;
                    SetSpecificPanelProp(((ISpecificControl)m_Control.IControl).SpecificPropPanel);
                    ((ISpecificPanel)((ISpecificControl)m_Control.IControl).SpecificPropPanel).ConfiguredItem = m_Control;
                    ((ISpecificPanel)((ISpecificControl)m_Control.IControl).SpecificPropPanel).Document = m_Document;
                    EnableTextEdition(stdProps.m_bEditTextEnabled);
                    if (m_EditText.Enabled)
                        this.Txt = m_Control.IControl.Text;
                    EnableFontSelection(stdProps.m_bSelectFontEnabled);
                    break;
                default:
                    System.Diagnostics.Debug.Assert(false);
                    break;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bEnable"></param>
        private void EnableTextEdition(bool bEnable)
        {
            if (!bEnable)
            {
                m_EditText.Enabled = false;
                m_EditText.Text = string.Empty;
            }
            else
            {
                m_EditText.Enabled = true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bEnable"></param>
        private void EnableFontSelection(bool bEnable)
        {
            if (!bEnable)
            {
                btn_SelFont.Enabled = false;
                lbl_Font.Enabled = false;
                lbl_Font.Text = string.Empty;
                btn_FontColor.Enabled = false;
            }
            else
            {
                btn_SelFont.Enabled = true;
                lbl_Font.Enabled = true;
                btn_FontColor.Enabled = true;
                if (this.m_Control != null)
                {
                    lbl_Font.Text = this.m_Control.TextFont.Name + ", " 
                        + this.m_Control.TextFont.Size.ToString() + ", "
                        + this.m_Control.TextFont.Style.ToString();
                }
                else
                {
                    lbl_Font.Text = string.Empty;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ctrl"></param>
        protected void SetSpecificPanelProp(UserControl ctrl)
        {
            if (m_CurrentSpecificControlPropPanel != null)
            {
                this.Controls.Remove(m_CurrentSpecificControlPropPanel);
            }
            if (ctrl != null)
            {
                try
                {
                    this.Controls.Add(ctrl);
                    ctrl.Location = this.m_panelPlaceSpec.Location;
                    m_CurrentSpecificControlPropPanel = ctrl;
                    m_CurrentSpecificControlPropPanel.Parent = this;
                    //m_CurrentSpecificControlPropPanel.Width = this.Width-20;
                    //m_CurrentSpecificControlPropPanel.Height = this.Height - ctrl.Location.X -10;
                    m_CurrentSpecificControlPropPanel.AutoScroll = true;
                    m_CurrentSpecificControlPropPanel.Visible = true;
                    m_CurrentSpecificControlPropPanel.Anchor = AnchorStyles.Left | AnchorStyles.Top;
                }
                catch (Exception e)
                {
                    Traces.LogAddDebug(TraceCat.SmartConfig,"Item PropPanel","Erreur de mise en place du panneau des propriété spécifiques");
                    Console.WriteLine(e.Message);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnControlAdded(ControlEventArgs e)
        {
            base.OnControlAdded(e);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_pickdata_Click(object sender, EventArgs e)
        {
            PickDataForm PickData = new PickDataForm();
            PickData.Document = this.Document;
            if (PickData.ShowDialog() == DialogResult.OK)
            {
                if (PickData.SelectedData != null)
                    this.AssociateData = PickData.SelectedData.Symbol;
                else
                    this.AssociateData = string.Empty;
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_SelFont_Click(object sender, EventArgs e)
        {
            if (m_Control != null)
            {
                FontDialog fontDlg = new FontDialog();
                fontDlg.Font = this.CtrlFont;
                if (fontDlg.ShowDialog() == DialogResult.OK)
                {
                    this.CtrlFont = fontDlg.Font;
                }
            }
        }

        private void btn_FontColor_Click(object sender, EventArgs e)
        {
            if (m_Control != null)
            {
                ColorDialog colorDlg = new ColorDialog();
                colorDlg.Color = this.CtrlFontColor;
                if (colorDlg.ShowDialog() == DialogResult.OK)
                {
                    this.CtrlFontColor = colorDlg.Color;
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using CommonLib;

using SmartApp.Ihm.Designer;

namespace SmartApp.Ihm
{
    public partial class ScreenItemPropertiesControl : UserControl
    {
        #region données membres
        private BTControl m_Control = null;

        private GestControl m_GestControl = null;

        private BTDoc m_Document = null;

        private UserControl m_CurrentSpecificControlPropPanel = null;

        private Font m_CtrlFont = null;

        #endregion

        #region events
        public event ControlPropertiesChange ControlPropertiesChanged;
        #endregion

        #region constructeur
        /// <summary>
        /// 
        /// </summary>
        public ScreenItemPropertiesControl()
        {
            InitializeComponent();
        }
        #endregion

        #region attribut d'accès aux valeurs de la page de propriété
        /// <summary>
        /// 
        /// </summary>
        public BTControl BTControl
        {
            get { return m_Control; }
            set
            {
                m_Control = value;
                if (m_Control != null)
                {
                    this.Enabled = true;
                    this.Description = m_Control.Description;
                    this.Symbol = m_Control.Symbol;
                    this.AssociateData = m_Control.AssociateData; ;
                    this.IsReadOnly = m_Control.IsReadOnly;
                    this.UseScreenEvent = m_Control.UseScreenEvent;
                    this.Txt = m_Control.IControl.Text;
                    this.m_LabelCurControl.Text = m_Control.Symbol;
                    this.CtrlFont = m_Control.TextFont;
                    this.CtrlFontColor = m_Control.TextColor;
                }
                else
                {
                    this.Description = "";
                    this.Symbol = "";
                    this.AssociateData = "";
                    this.IsReadOnly = false;
                    this.UseScreenEvent = false;
                    this.Enabled = false;
                    this.Txt = "";
                    this.m_LabelCurControl.Text = Program.LangSys.C("No selection");
                    this.m_CtrlFont = null;
                    this.CtrlFontColor = Color.Black;
                }
                UpdateStateFromControlType();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Description
        {
            get { return m_richTextBoxDesc.Text; }
            set { m_richTextBoxDesc.Text = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Symbol
        {
            get { return m_textSymbol.Text; }
            set { m_textSymbol.Text = value; }
        }

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

        /// <summary>
        /// 
        /// </summary>
        public BTDoc Doc
        {
            get { return m_Document; }
            set { m_Document = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public GestData GestData
        {
            get { return m_Document.GestData; }
        }

        /// <summary>
        /// 
        /// </summary>
        public GestControl GestControl
        {
            get { return m_GestControl; }
            set { m_GestControl = value; }
        }
        #endregion

        #region validation des données
        /// <summary>
        /// 
        /// </summary>
        public bool IsDataValuesValid
        {
            get
            {
                if (this.BTControl == null)
                    return true;

                if (string.IsNullOrEmpty(this.Symbol))
                    return false;
                bool bRet = true;
                BTControl dt = (BTControl)this.GestControl.GetFromSymbol(this.Symbol);
                if (dt != null && dt != this.BTControl)
                    bRet = false;
                if (!string.IsNullOrEmpty(this.AssociateData))
                {
                    Data dat = (Data)this.GestData.GetFromSymbol(this.AssociateData);
                    if (dat == null)
                        bRet = false;
                }
                if (m_CurrentSpecificControlPropPanel != null
                    && !((ISpecificPanel)m_CurrentSpecificControlPropPanel).IsDataValuesValid)
                    bRet = false;


                return bRet;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool ValidateValues()
        {
            if (this.BTControl == null)
                return true;

            bool bRet = true;
            string strMessage = "";
            if (string.IsNullOrEmpty(this.Symbol))
            {
                strMessage = Program.LangSys.C("Symbol must not be empty");
                bRet = false;
            }
            if (GestControl != null)
            {
                BTControl Sc = (BTControl)GestControl.GetFromSymbol(this.Symbol);
                if (bRet && Sc != null && Sc != this.BTControl)
                {
                    strMessage = string.Format(Program.LangSys.C("A control with symbol {0} already exist"), Symbol);
                    bRet = false;
                }
            }
            if (!string.IsNullOrEmpty(this.AssociateData))
            {
                Data dat = (Data)this.GestData.GetFromSymbol(this.AssociateData);
                if (bRet && dat == null)
                {
                    strMessage = string.Format(Program.LangSys.C("Associate data {0} is not valid"), this.AssociateData);
                    bRet = false;
                }
            }
            if (!bRet)
            {
                MessageBox.Show(strMessage, Program.LangSys.C("Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return bRet;
            }
            bool bDataPropChange = false;
            if (m_Control.Description != this.Description)
                bDataPropChange |= true;
            if (m_Control.Symbol != this.Symbol)
                bDataPropChange |= true;

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


            if (m_CurrentSpecificControlPropPanel != null
                && ((ISpecificPanel)m_CurrentSpecificControlPropPanel).ValidateValues())
                bDataPropChange |= true;

            if (bDataPropChange)
            {
                m_Control.Description = this.Description;
                m_Control.Symbol = this.Symbol;
                m_Control.AssociateData = this.AssociateData;
                m_Control.IsReadOnly = this.IsReadOnly;
                m_Control.UseScreenEvent = this.UseScreenEvent;
                m_Control.IControl.Text = this.Txt;
                m_Control.TextFont = this.CtrlFont;
                m_Control.TextColor = this.CtrlFontColor;
                Doc.Modified = true;
            }
            if (bDataPropChange && ControlPropertiesChanged != null)
                ControlPropertiesChanged(m_Control);
            return true;
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PropertiesControlValidating(object sender, CancelEventArgs e)
        {
            if (!ValidateValues())
                e.Cancel = true;
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
                    this.AssociateData = string.Empty;
                    m_EditAssociateData.Enabled = false;
                    btn_pickdata.Enabled = false;
                    m_checkReadOnly.Enabled = false;
                    m_checkReadOnly.Checked = false;
                    m_checkScreenEvent.Enabled = false;
                    m_checkScreenEvent.Checked = false;
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
                    ((ISpecificPanel)((ISpecificControl)m_Control.IControl).SpecificPropPanel).BTControl = m_Control;
                    ((ISpecificPanel)((ISpecificControl)m_Control.IControl).SpecificPropPanel).Doc = m_Document;
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
                    m_CurrentSpecificControlPropPanel.Visible = true;
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
            PickData.Document = this.Doc;
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

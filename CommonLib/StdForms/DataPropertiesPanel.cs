/*
    This file is part of SmartApp.
    Copyright (C) 2007-2016  Pascal Bigot

    SmartApp is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    SmartApp is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with Foobar.  If not, see <http://www.gnu.org/licenses/>.
*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace CommonLib
{
    public partial class DataPropertiesPanel : BaseObjectPropertiesPanel, IObjectPropertyPanel
    {
        #region données membres
        CComboData[] m_TabCboDataStruct;
        bool bLockUpdateRange = false;
        Data m_ConfiguredData = null;

        /// <summary>
        /// 
        /// </summary>
        private GestData m_GestData = null;


        #endregion

        #region constructeur
        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public DataPropertiesPanel()
        {
            InitializeComponent();
            LoadNonStandardLang();
            this.Enabled = false;
        }

        public void LoadNonStandardLang()
        {
            m_TabCboDataStruct = new CComboData[7];
            m_TabCboDataStruct[0] = new CComboData(Lang.LangSys.C("1 bit data"), DATA_SIZE.DATA_SIZE_1B);
            m_TabCboDataStruct[1] = new CComboData(Lang.LangSys.C("2 bits data"), DATA_SIZE.DATA_SIZE_2B);
            m_TabCboDataStruct[2] = new CComboData(Lang.LangSys.C("4 bits data"), DATA_SIZE.DATA_SIZE_4B);
            m_TabCboDataStruct[3] = new CComboData(Lang.LangSys.C("8 bits data"), DATA_SIZE.DATA_SIZE_8B);
            m_TabCboDataStruct[4] = new CComboData(Lang.LangSys.C("16 bits data (signed)"), DATA_SIZE.DATA_SIZE_16B);
            m_TabCboDataStruct[5] = new CComboData(Lang.LangSys.C("16 bits data (unsigned)"), DATA_SIZE.DATA_SIZE_16BU);
            m_TabCboDataStruct[6] = new CComboData(Lang.LangSys.C("32 bits data (signed)"), DATA_SIZE.DATA_SIZE_32B);

            m_cboSize.ValueMember = "Object";
            m_cboSize.DisplayMember = "DisplayedString";
            m_cboSize.DataSource = m_TabCboDataStruct;
            m_cboSize.SelectedIndex = 0;
        }

        #endregion

        #region attributs

        /// <summary>
        /// 
        /// </summary>
        public BaseGest ConfiguredItemGest
        {
            get { return m_GestData; }
            set { m_GestData = value as GestData; }
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public BaseObject ConfiguredItem
        {
            get
            {
                return m_ConfiguredData;
            }
            set
            {
                m_ConfiguredData = value as Data;
            }
        }

        #endregion

        #region validation des données
        //*****************************************************************************************************
        // Description: test si les valeurs saisies sont valides et renvoie false si elle ne le sont pas
        // sinon renvoie true
        // Return: /
        //*****************************************************************************************************
        public override bool IsObjectPropertiesValid
        {
            get
            {
                bool bRet = true;
                if (DefaultValue < MinValue)
                    bRet = false;
                if (DefaultValue > MaxValue)
                    bRet = false;

                return bRet;
            }
        }

        //*****************************************************************************************************
        // Description: test si les valeurs saisies sont valides et affiche un message d'erreur concernant
        // ce qui n'est pas valide
        // si les données sont valides, les propriété de la données sont mises a jour, et si elles ont changé
        // l'évènement DataPropertiesChanged est appelé
        // Return: /
        //*****************************************************************************************************
        public override bool ValidateProperties()
        {
            bool bRet = true;
            string strMessage = "";

            if (bRet && DefaultValue < MinValue)
            {
                strMessage = Lang.LangSys.C("Default value must be superior to minimum value");
                bRet = false;
            }
            if (bRet && DefaultValue > MaxValue)
            {
                strMessage = Lang.LangSys.C("Default value must be inferior to maximum value");
                bRet = false;
            }
            if (!bRet)
            {
                MessageBox.Show(strMessage, Lang.LangSys.C("Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return bRet;
            }

            return true;
        }

        public void ObjectToPanel()
        {
            this.Enabled = true;
            bLockUpdateRange = true;
            //this.DataSize = m_Data.Size;
            SelectComboSizeIndexAccordingToData();
            bLockUpdateRange = false;
            UpdateRangeFromDataSize();
            this.MinValue = m_ConfiguredData.Minimum;
            this.MaxValue = m_ConfiguredData.Maximum;
            this.DefaultValue = m_ConfiguredData.DefaultValue;
            this.IsConstant = m_ConfiguredData.IsConstant;
        }

        public void PanelToObject()
        {
            bool bDataPropChange = false;
            if (m_ConfiguredData.SizeAndSign != this.DataSizeAndSign)
                bDataPropChange |= true;
            if (m_ConfiguredData.Minimum != this.MinValue)
                bDataPropChange |= true;
            if (m_ConfiguredData.Maximum != this.MaxValue)
                bDataPropChange |= true;
            if (m_ConfiguredData.DefaultValue != this.DefaultValue)
                bDataPropChange |= true;
            if (m_ConfiguredData.IsConstant != this.IsConstant)
                bDataPropChange |= true;

            if (bDataPropChange)
            {
                m_ConfiguredData.SizeAndSign = this.DataSizeAndSign;
                m_ConfiguredData.Minimum = this.MinValue;
                m_ConfiguredData.Maximum = this.MaxValue;
                m_ConfiguredData.DefaultValue = this.DefaultValue;
                m_ConfiguredData.IsConstant = this.IsConstant;
                Document.Modified = true;
            }
        }
        #endregion

        #region attribut d'accès aux valeurs de la page de propriété
        /// <summary>
        /// 
        /// </summary>
        public int DataSizeAndSign
        {
            get
            {
                if (this.m_cboSize != null && this.m_cboSize.SelectedValue != null)
                    return (int)this.m_cboSize.SelectedValue;
                else
                    return 0;
            }
            set
            {
                if (this.m_cboSize != null)
                    this.m_cboSize.SelectedValue = (int)value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int MinValue
        {
            get
            {
                return (int)this.m_numUDMin.Value;
            }
            set
            {
                this.m_numUDMin.Value = (int)value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int MaxValue
        {
            get
            {
                return (int)this.m_numUDMax.Value;
            }
            set
            {
                this.m_numUDMax.Value = (int)value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int DefaultValue
        {
            get
            {
                return (int)this.m_numUDDefault.Value;
            }
            set
            {
                this.m_numUDDefault.Value = (int)value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IsConstant
        {
            get
            {
                return this.m_checkConstant.Checked;
            }
            set
            {
                this.m_checkConstant.Checked = value;
            }
        }
        #endregion

        #region handlers d'events
        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        private void OnSizeSelectedIndexChanged(object sender, EventArgs e)
        {
            if (bLockUpdateRange)
                return;
            UpdateRangeFromDataSize();
        }
        #endregion

        #region update de l'IHM
        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        private void UpdateRangeFromDataSize()
        {
            if (m_ConfiguredData == null)
                return;
            m_ConfiguredData.SizeAndSign = this.DataSizeAndSign;
            switch ((DATA_SIZE)m_ConfiguredData.SizeAndSign)
            {
                case DATA_SIZE.DATA_SIZE_1B:
                    m_numUDMax.Maximum = 1;
                    m_numUDMax.Minimum = 0;
                    m_numUDMin.Maximum = 1;
                    m_numUDMin.Minimum = 0;
                    m_numUDDefault.Maximum = 1;
                    m_numUDDefault.Minimum = 0;
                    m_numUDMax.Value = 1;
                    m_numUDMin.Value = 0;
                    m_numUDDefault.Value = 0;
                    break;
                case DATA_SIZE.DATA_SIZE_2B:
                    m_numUDMax.Maximum = 3;
                    m_numUDMax.Minimum = 0;
                    m_numUDMin.Maximum = 3;
                    m_numUDMin.Minimum = 0;
                    m_numUDDefault.Maximum = 3;
                    m_numUDDefault.Minimum = 0;
                    m_numUDMax.Value = 3;
                    m_numUDMin.Value = 0;
                    m_numUDDefault.Value = 0;
                    break;
                case DATA_SIZE.DATA_SIZE_4B:
                    m_numUDMax.Maximum = 15;
                    m_numUDMax.Minimum = 0;
                    m_numUDMin.Maximum = 15;
                    m_numUDMin.Minimum = 0;
                    m_numUDDefault.Maximum = 15;
                    m_numUDDefault.Minimum = 0;
                    m_numUDMax.Value = 15;
                    m_numUDMin.Value = 0;
                    m_numUDDefault.Value = 0;
                    break;
                case DATA_SIZE.DATA_SIZE_8B:
                    m_numUDMax.Maximum = 255;
                    m_numUDMax.Minimum = 0;
                    m_numUDMin.Maximum = 255;
                    m_numUDMin.Minimum = 0;
                    m_numUDDefault.Maximum = 255;
                    m_numUDDefault.Minimum = 0;
                    m_numUDMax.Value = 255;
                    m_numUDMin.Value = 0;
                    m_numUDDefault.Value = 0;
                    break;
                case DATA_SIZE.DATA_SIZE_16B:
                    m_numUDMax.Maximum = 32767;
                    m_numUDMax.Minimum = -32768;
                    m_numUDMin.Maximum = 32767;
                    m_numUDMin.Minimum = -32768;
                    m_numUDDefault.Maximum = 32767;
                    m_numUDDefault.Minimum = -32768;
                    m_numUDMax.Value = 32767;
                    m_numUDMin.Value = -32768;
                    m_numUDDefault.Value = 0;
                    break;
                case DATA_SIZE.DATA_SIZE_16BU:
                    m_numUDMax.Maximum = 0xFFFF;
                    m_numUDMax.Minimum = 0;
                    m_numUDMin.Maximum = 0xFFFF;
                    m_numUDMin.Minimum = 0;
                    m_numUDDefault.Maximum = 0xFFFF;
                    m_numUDDefault.Minimum = 0;
                    m_numUDMax.Value = 0xFFFF;
                    m_numUDMin.Value = 0;
                    break;
                case DATA_SIZE.DATA_SIZE_32B:
                    m_numUDMax.Maximum = int.MaxValue;
                    m_numUDMax.Minimum = int.MinValue;
                    m_numUDMin.Maximum = int.MaxValue;
                    m_numUDMin.Minimum = int.MinValue;
                    m_numUDDefault.Maximum = int.MaxValue;
                    m_numUDDefault.Minimum = int.MinValue;
                    m_numUDMax.Value = int.MaxValue;
                    m_numUDMin.Value = int.MinValue;
                    break;
                default:
                    System.Diagnostics.Debug.Assert(false);
                    break;
            }
            if (m_numUDDefault.Value < m_numUDMin.Value
                || m_numUDDefault.Value > m_numUDMax.Value)
            {
                m_numUDDefault.Value = 0;
            }
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        protected void SelectComboSizeIndexAccordingToData()
        {
            if (m_ConfiguredData == null)
                return;

            switch ((DATA_SIZE)m_ConfiguredData.SizeAndSign)
            {
                case DATA_SIZE.DATA_SIZE_1B:
                    this.m_cboSize.SelectedIndex = 0;
                    break;
                case DATA_SIZE.DATA_SIZE_2B:
                    this.m_cboSize.SelectedIndex = 1;
                    break;
                case DATA_SIZE.DATA_SIZE_4B:
                    this.m_cboSize.SelectedIndex = 2;
                    break;
                case DATA_SIZE.DATA_SIZE_8B:
                    this.m_cboSize.SelectedIndex = 3;
                    break;
                case DATA_SIZE.DATA_SIZE_16B:
                    this.m_cboSize.SelectedIndex = 4;
                    break;
                case DATA_SIZE.DATA_SIZE_16BU:
                    this.m_cboSize.SelectedIndex = 5;
                    break;
                case DATA_SIZE.DATA_SIZE_32B:
                    this.m_cboSize.SelectedIndex = 6;
                    break;
                default:
                    System.Diagnostics.Debug.Assert(false);
                    break;
            }

        }
        #endregion
    }
}

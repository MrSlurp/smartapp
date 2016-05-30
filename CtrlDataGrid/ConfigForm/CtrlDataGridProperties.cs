/*
    This file is part of SmartApp.

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
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CommonLib;

namespace CtrlDataGrid
{
    public partial class CtrlDataGridProperties : BaseControlPropertiesPanel, ISpecificPanel
    {

        struct DispLogAsso
        {
            public SAVE_PERIOD DispPeriod;
            public LOG_PERIOD[] LogPeriods;

            public DispLogAsso(SAVE_PERIOD Disp, LOG_PERIOD[] LogPer)
            {
                DispPeriod = Disp;
                LogPeriods = LogPer;
            }
        }

        DllCtrlDataGridProp m_Props = new DllCtrlDataGridProp(null);

        DataParam[] m_ListData = new DataParam[DllCtrlDataGridProp.NB_DATA];

        CComboData[] m_CboDataDispPeriod;
        CComboData[] m_CboDataLogPeriod;

        DispLogAsso[] m_DispLogAsso =
        {
            new DispLogAsso(SAVE_PERIOD.SAVE_5_min, new LOG_PERIOD[] 
                                                     {LOG_PERIOD.LOG_1_sec, LOG_PERIOD.LOG_10_sec,  
                                                      LOG_PERIOD.LOG_30_sec}),
            new DispLogAsso(SAVE_PERIOD.SAVE_10_min, new LOG_PERIOD[] 
                                                     {LOG_PERIOD.LOG_1_sec, LOG_PERIOD.LOG_10_sec,  
                                                      LOG_PERIOD.LOG_30_sec}),
            new DispLogAsso(SAVE_PERIOD.SAVE_15_min, new LOG_PERIOD[] 
                                                     {LOG_PERIOD.LOG_1_sec, LOG_PERIOD.LOG_10_sec,  
                                                      LOG_PERIOD.LOG_30_sec, LOG_PERIOD.LOG_1_min}),
            new DispLogAsso(SAVE_PERIOD.SAVE_1_h,    new LOG_PERIOD[] 
                                                     {LOG_PERIOD.LOG_1_sec,  LOG_PERIOD.LOG_10_sec, 
                                                      LOG_PERIOD.LOG_30_sec, LOG_PERIOD.LOG_1_min, 
                                                      LOG_PERIOD.LOG_2_min}),
            new DispLogAsso(SAVE_PERIOD.SAVE_2_h,    new LOG_PERIOD[] 
                                                     {LOG_PERIOD.LOG_1_sec,  LOG_PERIOD.LOG_10_sec,  
                                                      LOG_PERIOD.LOG_30_sec, LOG_PERIOD.LOG_1_min, 
                                                      LOG_PERIOD.LOG_2_min}),
            new DispLogAsso(SAVE_PERIOD.SAVE_6_h,    new LOG_PERIOD[] 
                                                     {LOG_PERIOD.LOG_10_sec, LOG_PERIOD.LOG_30_sec, 
                                                      LOG_PERIOD.LOG_1_min,  LOG_PERIOD.LOG_2_min, 
                                                      LOG_PERIOD.LOG_5_min,  LOG_PERIOD.LOG_10_min}),
            new DispLogAsso(SAVE_PERIOD.SAVE_12_h,   new LOG_PERIOD[] 
                                                     {LOG_PERIOD.LOG_10_sec, LOG_PERIOD.LOG_30_sec, 
                                                      LOG_PERIOD.LOG_1_min,  LOG_PERIOD.LOG_2_min, 
                                                      LOG_PERIOD.LOG_5_min,  LOG_PERIOD.LOG_10_min})/*,
            
            new DispLogAsso(SAVE_PERIOD.SAVE_1_j,    new LOG_PERIOD[] 
                                                     {LOG_PERIOD.LOG_10_sec, LOG_PERIOD.LOG_30_sec, 
                                                      LOG_PERIOD.LOG_1_min,  LOG_PERIOD.LOG_2_min, 
                                                      LOG_PERIOD.LOG_5_min,  LOG_PERIOD.LOG_10_min,}),
            new DispLogAsso(SAVE_PERIOD.SAVE_2_j,    new LOG_PERIOD[] 
                                                     {LOG_PERIOD.LOG_30_sec, LOG_PERIOD.LOG_1_min,
                                                      LOG_PERIOD.LOG_2_min,  LOG_PERIOD.LOG_5_min,   
                                                      LOG_PERIOD.LOG_10_min}),
            new DispLogAsso(SAVE_PERIOD.SAVE_4_j,    new LOG_PERIOD[] 
                                                     {LOG_PERIOD.LOG_30_sec, LOG_PERIOD.LOG_1_min,
                                                      LOG_PERIOD.LOG_2_min,  LOG_PERIOD.LOG_5_min,   
                                                      LOG_PERIOD.LOG_10_min}),
            new DispLogAsso(SAVE_PERIOD.SAVE_7_j,    new LOG_PERIOD[] 
                                                     {LOG_PERIOD.LOG_1_min, LOG_PERIOD.LOG_2_min, 
                                                      LOG_PERIOD.LOG_5_min, LOG_PERIOD.LOG_10_min})*/
        };

        public CtrlDataGridProperties()
        {
            DllEntryClass.LangSys.Initialize(this);
            InitializeComponent();

            Point BasePos = new Point(0, 0);
            for (int i = 0; i < DllCtrlDataGridProp.NB_DATA; i++)
            {
                m_ListData[i] = new DataParam();
                m_ListData[i].Location = BasePos;
                BasePos.Y += m_ListData[i].Size.Height + 8;
                m_ListData[i].Name = string.Format("DataParam{0}", i);
                m_ListData[i].DataIndex = i;
                m_ListData[i].Doc = this.Document;
                this.uscPanelCurveCfg.Controls.Add(m_ListData[i]);
            }

            m_CboDataDispPeriod = new CComboData[7];
            m_CboDataDispPeriod[0] = new CComboData(DllEntryClass.LangSys.C("5 minutes"), SAVE_PERIOD.SAVE_5_min);
            m_CboDataDispPeriod[1] = new CComboData(DllEntryClass.LangSys.C("10 minutes"), SAVE_PERIOD.SAVE_10_min);
            m_CboDataDispPeriod[2] = new CComboData(DllEntryClass.LangSys.C("15 minutes"), SAVE_PERIOD.SAVE_15_min);
            m_CboDataDispPeriod[3] = new CComboData(DllEntryClass.LangSys.C("1 hour"), SAVE_PERIOD.SAVE_1_h);
            m_CboDataDispPeriod[4] = new CComboData(DllEntryClass.LangSys.C("2 hours"), SAVE_PERIOD.SAVE_2_h);
            m_CboDataDispPeriod[5] = new CComboData(DllEntryClass.LangSys.C("6 hours"), SAVE_PERIOD.SAVE_6_h);
            m_CboDataDispPeriod[6] = new CComboData(DllEntryClass.LangSys.C("12 hours"), SAVE_PERIOD.SAVE_12_h);

            m_CboDataLogPeriod = new CComboData[8];
            m_CboDataLogPeriod[0] = new CComboData(DllEntryClass.LangSys.C("1 sec"), LOG_PERIOD.LOG_1_sec);
            m_CboDataLogPeriod[1] = new CComboData(DllEntryClass.LangSys.C("10 sec"), LOG_PERIOD.LOG_10_sec);
            m_CboDataLogPeriod[2] = new CComboData(DllEntryClass.LangSys.C("30 sec"), LOG_PERIOD.LOG_30_sec);
            m_CboDataLogPeriod[3] = new CComboData(DllEntryClass.LangSys.C("1 minute"), LOG_PERIOD.LOG_1_min);
            m_CboDataLogPeriod[4] = new CComboData(DllEntryClass.LangSys.C("2 minutes"), LOG_PERIOD.LOG_2_min);
            m_CboDataLogPeriod[5] = new CComboData(DllEntryClass.LangSys.C("5 minutes"), LOG_PERIOD.LOG_5_min);
            m_CboDataLogPeriod[6] = new CComboData(DllEntryClass.LangSys.C("10 minutes"), LOG_PERIOD.LOG_10_min);
            m_CboDataLogPeriod[7] = new CComboData(DllEntryClass.LangSys.C("15 minutes"), LOG_PERIOD.LOG_10_min);

            // pour la combo des périodes, on peux directement assigner la liste complète
            cboDispPeriod.ValueMember = "Object";
            cboDispPeriod.DisplayMember = "DisplayedString";
            cboDispPeriod.DataSource = m_CboDataDispPeriod;
            cboDispPeriod.SelectedIndex = 0;
        }

        #region attributs
        public override BTDoc Document
        {
            get
            {
                return m_Document;
            }
            set
            {
                m_Document = value;
                for (int i = 0; i < DllCtrlDataGridProp.NB_DATA; i++)
                {
                    m_ListData[i].Doc = m_Document;
                }

            }
        }

        public DllCtrlDataGridProp Props
        {
            get
            {
                for (int i = 0; i < DllCtrlDataGridProp.NB_DATA; i++)
                {
                    m_Props.SetSymbol(i, m_ListData[i].DataSymbol);
                    m_Props.SetAlias(i, m_ListData[i].Alias);
                }
                m_Props.SavePeriod = (SAVE_PERIOD)cboDispPeriod.SelectedValue;
                m_Props.LogPeriod = (LOG_PERIOD)cboLogPeriod.SelectedValue;
                return m_Props;
            }
            set
            {
                if (value != null)
                {
                    m_Props.CopyParametersFrom(value, false, this.Document);
                    for (int i = 0; i < DllCtrlDataGridProp.NB_DATA; i++)
                    {
                        m_ListData[i].DataSymbol = m_Props.GetSymbol(i);
                        m_ListData[i].Alias = m_Props.GetAlias(i);
                    }
                    cboDispPeriod.SelectedValue = m_Props.SavePeriod;
                    UpdateLogPeriodFromDisp();
                }
                else
                {
                    for (int i = 0; i < DllCtrlDataGridProp.NB_DATA; i++)
                    {
                        m_ListData[i].DataSymbol = "";
                        m_ListData[i].Alias = "";
                    }
                    cboDispPeriod.SelectedIndex = 0;
                    UpdateLogPeriodFromDisp();
                }
            }
        }

        public void PanelToObject()
        {
            DllCtrlDataGridProp itemprops = m_Control.SpecificProp as DllCtrlDataGridProp;
            itemprops.CopyParametersFrom(Props, false, this.Document);
            Document.Modified = true;
        }

        public void ObjectToPanel()
        {
            DllCtrlDataGridProp itemprops = m_Control.SpecificProp as DllCtrlDataGridProp;
            this.Props = itemprops;
            //Props.CopyParametersFrom(itemprops, false);
        }

        #endregion

        private void UpdateLogPeriodFromDisp()
        {
            CComboData[] TempListCbo = null;
            bool bCurValueInList = false;
            SAVE_PERIOD SelDisp = (SAVE_PERIOD)cboDispPeriod.SelectedValue;
            // on parcour la liste des associations
            for (int iDisp = 0; iDisp < m_DispLogAsso.Length; iDisp++)
            {
                // si on est sur l'assoc de la valeur de Disp courante
                if (m_DispLogAsso[iDisp].DispPeriod == SelDisp)
                {
                    // on parcour la list de LogPeriod Disponibles
                    TempListCbo = new CComboData[m_DispLogAsso[iDisp].LogPeriods.Length];
                    for (int iLog = 0; iLog < m_DispLogAsso[iDisp].LogPeriods.Length; iLog++)
                    {
                        //on recherche dans la liste des item de combo ceux qu'on peux ajouter
                        for (int i = 0; i < m_CboDataLogPeriod.Length; i++)
                        {
                            if ((int)m_CboDataLogPeriod[i].Object == (int)m_DispLogAsso[iDisp].LogPeriods[iLog])
                            {
                                TempListCbo[iLog] = m_CboDataLogPeriod[i];
                                if ((LOG_PERIOD)m_CboDataLogPeriod[i].Object == m_Props.LogPeriod)
                                    bCurValueInList = true;
                                break;
                            }
                        }
                    }
                    break;
                }
            }
            cboLogPeriod.ValueMember = "Object";
            cboLogPeriod.DisplayMember = "DisplayedString";
            cboLogPeriod.DataSource = TempListCbo;
            if (bCurValueInList)
                cboLogPeriod.SelectedValue = m_Props.LogPeriod;
            else
                cboLogPeriod.SelectedIndex = 0;
        }
    }
}
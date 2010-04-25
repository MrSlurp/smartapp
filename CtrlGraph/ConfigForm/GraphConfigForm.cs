using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CommonLib;

namespace CtrlGraph
{
    public partial class GraphConfigForm : Form
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
        #region données membres
        private BTDoc m_Document = null;

        DllCtrlGraphProp m_Props = new DllCtrlGraphProp();
        CurveParam[] m_ListCurve = new CurveParam[DllCtrlGraphProp.NB_CURVE];
        //TextBox[] m_ListSymb = new TextBox[DllCtrlGraphProp.NB_CURVE];
        //TextBox[] m_ListAlias = new TextBox[DllCtrlGraphProp.NB_CURVE];
        //TextBox[] m_ListColors = new TextBox[DllCtrlGraphProp.NB_CURVE];

        CComboData[] m_CboDataDispPeriod;
        CComboData[] m_CboDataLogPeriod;
        
        // compatibilité disp \ log period
        // DISP         LOG        |     DISP     LOG
        //  10 min                 |     2 J
        //              1  sec     |              30 sec
        //              10 sec     |              1  min
        //              30 sec     |              2  min
        //                         |              5  min
        //  1h / 2h                |              10 min
        //              1  sec     |     
        //              10 sec     |     4 J
        //              30 sec     |              30 sec            
        //              1  min     |              1  min
        //              2  min     |              2  min
        //                         |              5  min
        //  6h / 12h / 1J          |              10 min     
        //              10 sec     |
        //              30 sec     |     1 W            
        //              1  min     |              1 min        
        //              2  min     |              2  min       
        //              5  min     |              5  min       
        //              10 min     |              10 min       
        //                            
       
        DispLogAsso[] m_DispLogAsso =
        {
            new DispLogAsso(SAVE_PERIOD.SAVE_10_min, new LOG_PERIOD[] 
                                                     {LOG_PERIOD.LOG_1_sec, LOG_PERIOD.LOG_10_sec,  
                                                      LOG_PERIOD.LOG_30_sec}),
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
                                                      LOG_PERIOD.LOG_5_min,  LOG_PERIOD.LOG_10_min}),
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
                                                      LOG_PERIOD.LOG_5_min, LOG_PERIOD.LOG_10_min})
        };
        #endregion

        #region constructeur
        public GraphConfigForm()
        {
            DllEntryClass.LangSys.Initialize(this);
            InitializeComponent();
            // initialisation dynamique de la form avec n composant
            // CuvreParam en fonction de la constante DllCtrlGraphProp.NB_CURVE
            Point BasePos = new Point(0,0);
            for (int i = 0; i < DllCtrlGraphProp.NB_CURVE; i++)
            {
                m_ListCurve[i] = new CurveParam();
                m_ListCurve[i].Location = BasePos;
                BasePos.Y += m_ListCurve[i].Size.Height + 8;  
                m_ListCurve[i].Name = string.Format("CurveParam{0}", i); 
                m_ListCurve[i].DataIndex = i;
                m_ListCurve[i].Doc = this.Doc;
                this.uscPanelCurveCfg.Controls.Add(m_ListCurve[i]);
            }

            m_CboDataDispPeriod = new CComboData[9];
            m_CboDataDispPeriod[0] = new CComboData(DllEntryClass.LangSys.C("10 minutes"), SAVE_PERIOD.SAVE_10_min);
            m_CboDataDispPeriod[1] = new CComboData(DllEntryClass.LangSys.C("1 hour"), SAVE_PERIOD.SAVE_1_h);
            m_CboDataDispPeriod[2] = new CComboData(DllEntryClass.LangSys.C("2 hours"), SAVE_PERIOD.SAVE_2_h);
            m_CboDataDispPeriod[3] = new CComboData(DllEntryClass.LangSys.C("6 hours"), SAVE_PERIOD.SAVE_6_h);
            m_CboDataDispPeriod[4] = new CComboData(DllEntryClass.LangSys.C("12 hours"), SAVE_PERIOD.SAVE_12_h);
            m_CboDataDispPeriod[5] = new CComboData(DllEntryClass.LangSys.C("1 day"), SAVE_PERIOD.SAVE_1_j);
            m_CboDataDispPeriod[6] = new CComboData(DllEntryClass.LangSys.C("2 days"), SAVE_PERIOD.SAVE_2_j);
            m_CboDataDispPeriod[7] = new CComboData(DllEntryClass.LangSys.C("4 days"), SAVE_PERIOD.SAVE_4_j);
            m_CboDataDispPeriod[8] = new CComboData(DllEntryClass.LangSys.C("1 week"), SAVE_PERIOD.SAVE_7_j);

            m_CboDataLogPeriod = new CComboData[7];
            m_CboDataLogPeriod[0] = new CComboData(DllEntryClass.LangSys.C("1 sec"), LOG_PERIOD.LOG_1_sec);
            m_CboDataLogPeriod[1] = new CComboData(DllEntryClass.LangSys.C("10 sec"), LOG_PERIOD.LOG_10_sec);
            m_CboDataLogPeriod[2] = new CComboData(DllEntryClass.LangSys.C("30 sec"), LOG_PERIOD.LOG_30_sec);
            m_CboDataLogPeriod[3] = new CComboData(DllEntryClass.LangSys.C("1 minute"), LOG_PERIOD.LOG_1_min);
            m_CboDataLogPeriod[4] = new CComboData(DllEntryClass.LangSys.C("2 minutes"), LOG_PERIOD.LOG_2_min);
            m_CboDataLogPeriod[5] = new CComboData(DllEntryClass.LangSys.C("5 minutes"), LOG_PERIOD.LOG_5_min);
            m_CboDataLogPeriod[6] = new CComboData(DllEntryClass.LangSys.C("10 minutes"), LOG_PERIOD.LOG_10_min);

            // pour la combo des périodes, on peux directement assigner la liste complète
            cboDispPeriod.ValueMember = "Object";
            cboDispPeriod.DisplayMember = "DisplayedString";
            cboDispPeriod.DataSource = m_CboDataDispPeriod;
            cboDispPeriod.SelectedIndex = 0;
        }
        #endregion

        #region attributs
        public BTDoc Doc
        {
            get
            {
                return m_Document;
            }
            set
            {
                m_Document = value;
                for (int i = 0; i < DllCtrlGraphProp.NB_CURVE; i++)
                {
                    m_ListCurve[i].Doc = m_Document;
                }

            }
        }

        public DllCtrlGraphProp Props
        {
            get
            {
                for (int i = 0; i < DllCtrlGraphProp.NB_CURVE; i++)
                {
                    m_Props.SetSymbol(i, m_ListCurve[i].DataSymbol);
                    m_Props.SetAlias(i, m_ListCurve[i].Alias);
                    m_Props.SetColor(i, m_ListCurve[i].CurveColor);
                }
                m_Props.SavePeriod = (SAVE_PERIOD)cboDispPeriod.SelectedValue;
                m_Props.LogPeriod = (LOG_PERIOD)cboLogPeriod.SelectedValue;
                m_Props.GraphTitle = edtTitle.Text;
                m_Props.XAxisTitle = edtXAxis.Text;
                m_Props.YAxisTitle = edtYAxis.Text;
                return m_Props;
            }
            set
            {
                if (value != null)
                {
                    m_Props.CopyParametersFrom(value, false);
                    for (int i = 0; i < DllCtrlGraphProp.NB_CURVE; i++)
                    {
                        m_ListCurve[i].DataSymbol = m_Props.GetSymbol(i);
                        m_ListCurve[i].Alias = m_Props.GetAlias(i);
                        m_ListCurve[i].CurveColor = m_Props.GetColor(i);
                    }
                    edtTitle.Text = m_Props.GraphTitle;
                    edtXAxis.Text = m_Props.XAxisTitle;
                    edtYAxis.Text = m_Props.YAxisTitle;
                    cboDispPeriod.SelectedValue = m_Props.SavePeriod;
                    UpdateLogPeriodFromDisp();

                }
                else
                {
                    for (int i = 0; i < DllCtrlGraphProp.NB_CURVE; i++)
                    {
                        m_ListCurve[i].DataSymbol = "";
                        m_ListCurve[i].Alias = "";
                        m_ListCurve[i].CurveColor = Color.Black;
                    }
                    edtTitle.Text = "";
                    edtXAxis.Text = "";
                    edtYAxis.Text = "";
                    cboDispPeriod.SelectedIndex = 0;
                    UpdateLogPeriodFromDisp();
                }
            }
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
                        for (int i =0;i< m_CboDataLogPeriod.Length; i++)
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

        #region event handlers
        private void cboDispPeriod_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateLogPeriodFromDisp();
        }
        #endregion
    }
}
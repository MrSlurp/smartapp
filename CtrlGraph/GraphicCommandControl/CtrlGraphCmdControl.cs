using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.ComponentModel;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using CommonLib;
using ZedGraph;

namespace CtrlGraph
{
    internal class CtrlGraphCmdControl : BTDllCtrlGraphControl
    {

        #region donnée spécifiques aux fonctionement en mode Command
        // timer executant périodiquement les logs
        Timer m_Timer = new Timer();

        // liste des données a loguer utilisé en mode Command
        private ArrayList m_ListRefDatas = new ArrayList();
        private StringCollection m_ListAliases = new StringCollection();
        private List<Color> m_ListCr = new List<Color>();

        // indique si le timer est actif
        bool m_bTimerActive = false;

        int iQueueLenght = 0;
        #endregion

        public int QueueLenght
        {
            get
            {
                return iQueueLenght;
            }
        }

        public ArrayList ListRefDatas
        {
            get
            {
                return m_ListRefDatas;
            }
        }

        public StringCollection ListAliases
        {
            get
            {
                return m_ListAliases;
            }
        }

        public List<Color> ColorList
        {
            get
            {
                return m_ListCr;
            }
        }

        public override bool DisabledOnStop
        {
            get
            {
                return false;
            }
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public CtrlGraphCmdControl()
        {

        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public override void CreateControl()
        {
            if (m_Ctrl == null)
            {
                m_Ctrl = new CtrlGraphDispCtrl(this);
                m_Ctrl.Location = m_RectControl.Location;
                m_Ctrl.Name = m_strSymbol;
                m_Ctrl.Size = m_RectControl.Size;
                m_Ctrl.BackColor = Color.Transparent;
                // faites ici les initialisation spécifiques du control affiché
            }
        }

        public override void OnControlEvent(object Sender, EventArgs Args)
        {
            // traitez ici les évènement déclenché par le control (click souris par exemple)
            return;
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public override void UpdateFromData()
        {
            if (m_AssociateData != null && m_Ctrl != null)
            {
                // effectuez ici le traitement à executer lorsque la valeur change
            }
        }

        //*****************************************************************************************************
        // Description: termine la lecture de l'objet. utilisé en mode Commande pour récupérer les référence
        // vers les objets utilisés
        // Return: /
        //*****************************************************************************************************
        public override bool FinalizeRead(BTDoc Doc)
        {
            DllCtrlGraphProp Props = (DllCtrlGraphProp)m_SpecificProp;

            // on fait une liste de références directe sur les données
            for (int i = 0; i < DllCtrlGraphProp.NB_CURVE; i++)
            {
                string strData = Props.GetSymbol(i);
                Data Dat = (Data)Doc.GestData.GetFromSymbol(strData);
                if (Dat == null)
                {
                    if (string.IsNullOrEmpty(strData))
                        continue;

                    string strMessage;
                    strMessage = string.Format(DllEntryClass.LangSys.C("Data to log not found (Graphic {0}, Data {1})"), m_strSymbol, strData);
                    LogEvent log = new LogEvent(LOG_EVENT_TYPE.INFO, strMessage);
                    AddLogEvent(log);
                    continue;
                }
                m_ListRefDatas.Add(Dat);
                m_ListAliases.Add(Props.GetAlias(i));
                m_ListCr.Add(Props.GetColor(i));
            }
            m_Timer.Interval = (int)Props.LogPeriod * 1000;
            m_Timer.Tick += new EventHandler(OnTimerTick);

            // calcule de la taille de la fifo de stockage des données
            // 1 : la valeur étant stocké en minutes, on convertis en secondes
            int tempDispInSec = (int)Props.SavePeriod * 60;

            // on vérifie le future résultat, il ne faut pas de valeur non entière
            int tempRest = tempDispInSec % (int)Props.LogPeriod;
            if (tempRest != 0)
                System.Diagnostics.Debug.Assert(false);
            // 2 : on fait une simple division
            iQueueLenght = tempDispInSec / (int)Props.LogPeriod;

            return true;
        }

        public override void TraiteMessage(MESSAGE Mess, object obj, TYPE_APP TypeApp)
        {
            base.TraiteMessage(Mess, obj, TypeApp);
            if (TypeApp == TYPE_APP.SMART_COMMAND)
            {
                switch (Mess)
                {
                    // message de requête sur les conséquence d'une supression
                    case MESSAGE.MESS_CMD_STOP:
                        m_Timer.Stop();
                        m_bTimerActive = false;
                        break;
                    case MESSAGE.MESS_CMD_RUN:
                        ((CtrlGraphDispCtrl)m_Ctrl).ClearCurves();
                        m_Timer.Start();
                        m_bTimerActive = true;
                        break;
                    default:
                        break;
                }
            }
        }

        //*****************************************************************************************************
        // Description: évènement du timer
        // Return: /
        //*****************************************************************************************************
        private void OnTimerTick(object sender, EventArgs e)
        {
            m_Timer.Stop();
            ((CtrlGraphDispCtrl)m_Ctrl).DataLogNotify();
            if (m_bTimerActive)
                m_Timer.Start();
        }
    }


    internal class CtrlGraphDispCtrl : UserControl
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        double m_DisplayedRange = 0;

        double MinStep = 0;
        SAVE_PERIOD CurrentDispPeriod = SAVE_PERIOD.SAVE_1_h;

        // ajouter ici les données membres du control affiché
        ZedGraphControl m_ZedGraphCtrl = null;
        CtrlGraphCmdControl m_SourceCtrl = null;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="srcCtrl"></param>
        public CtrlGraphDispCtrl(CtrlGraphCmdControl srcCtrl)
        {
            m_SourceCtrl = srcCtrl;
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.m_ZedGraphCtrl = new ZedGraph.ZedGraphControl();
            this.SuspendLayout();

            this.m_ZedGraphCtrl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_ZedGraphCtrl.Location = new System.Drawing.Point(0, 0);
            this.m_ZedGraphCtrl.Name = "m_ZedGraphCtrl";
            this.m_ZedGraphCtrl.ScrollGrace = 0;
            this.m_ZedGraphCtrl.ScrollMaxX = 0;
            this.m_ZedGraphCtrl.ScrollMaxY = 0;
            this.m_ZedGraphCtrl.ScrollMaxY2 = 0;
            this.m_ZedGraphCtrl.ScrollMinX = 0;
            this.m_ZedGraphCtrl.ScrollMinY = 0;
            this.m_ZedGraphCtrl.ScrollMinY2 = 0;
            this.m_ZedGraphCtrl.IsEnableVPan = false;
            this.m_ZedGraphCtrl.IsEnableVZoom = false;
            this.m_ZedGraphCtrl.Size = this.Size;
            this.m_ZedGraphCtrl.TabIndex = 0;
            this.m_ZedGraphCtrl.ContextMenuBuilder += new ZedGraphControl.ContextMenuBuilderEventHandler(ZedGraphCtrl_ContextMenuBuilder);

            this.Controls.Add(this.m_ZedGraphCtrl);
            this.ResumeLayout(false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="menuStrip"></param>
        /// <param name="mousePt"></param>
        /// <param name="objState"></param>
        void ZedGraphCtrl_ContextMenuBuilder(ZedGraphControl sender, ContextMenuStrip menuStrip, Point mousePt, ZedGraphControl.ContextMenuObjectState objState)
        {
            // traduction hors zed graph
            for (int i = 0; i< menuStrip.Items.Count; i++)
            {
                menuStrip.Items[i].Text = DllEntryClass.LangSys.C(menuStrip.Items[i].Text);                 
            }
            // create a new menu item
            ToolStripMenuItem itemDropDispPer = new ToolStripMenuItem();
            // This is the user-defined Tag so you can find this menu item later if necessary
            itemDropDispPer.Name = "Disp_Period";
            itemDropDispPer.Tag = "Disp_Period";
            // This is the text that will show up in the menu
            itemDropDispPer.Text = DllEntryClass.LangSys.C("Displayed Period");
            // Add a handler that will respond when that menu item is selected
            // Add the menu item to the menu
            menuStrip.Items.Add(itemDropDispPer);


            DllCtrlGraphProp specProps = (DllCtrlGraphProp)m_SourceCtrl.SpecificProp;

            if ((int)specProps.SavePeriod >= (int)SAVE_PERIOD.SAVE_10_min)
            {
                ToolStripMenuItem Disp10Min = new ToolStripMenuItem();
                Disp10Min.Name = "10Min";
                Disp10Min.Text = DllEntryClass.LangSys.C("10 Minutes");
                Disp10Min.Tag = SAVE_PERIOD.SAVE_10_min;
                Disp10Min.Click += new EventHandler(DispPerdiodIem_Click);
                itemDropDispPer.DropDownItems.Add(Disp10Min);
            }
            if ((int)specProps.SavePeriod >= (int)SAVE_PERIOD.SAVE_1_h)
            {
                ToolStripMenuItem Disp1Hour = new ToolStripMenuItem();
                Disp1Hour.Name = "1Hour";
                Disp1Hour.Text = DllEntryClass.LangSys.C("1 Hour");
                Disp1Hour.Tag = SAVE_PERIOD.SAVE_1_h;
                Disp1Hour.Click += new EventHandler(DispPerdiodIem_Click);
                itemDropDispPer.DropDownItems.Add(Disp1Hour);
            }
            if ((int)specProps.SavePeriod >= (int)SAVE_PERIOD.SAVE_2_h)
            {
                ToolStripMenuItem Disp2Hour = new ToolStripMenuItem();
                Disp2Hour.Name = "2Hours";
                Disp2Hour.Text = DllEntryClass.LangSys.C("2 Hours");
                Disp2Hour.Tag = SAVE_PERIOD.SAVE_2_h;
                Disp2Hour.Click += new EventHandler(DispPerdiodIem_Click);
                itemDropDispPer.DropDownItems.Add(Disp2Hour);
            }
            if ((int)specProps.SavePeriod >= (int)SAVE_PERIOD.SAVE_6_h)
            {
                ToolStripMenuItem Disp6Hour = new ToolStripMenuItem();
                Disp6Hour.Name = "6Hours";
                Disp6Hour.Text = DllEntryClass.LangSys.C("6 Hours");
                Disp6Hour.Tag = SAVE_PERIOD.SAVE_6_h;
                Disp6Hour.Click += new EventHandler(DispPerdiodIem_Click);
                itemDropDispPer.DropDownItems.Add(Disp6Hour);
            }
            if ((int)specProps.SavePeriod >= (int)SAVE_PERIOD.SAVE_12_h)
            {
                ToolStripMenuItem Disp12Hour = new ToolStripMenuItem();
                Disp12Hour.Name = "12Hours";
                Disp12Hour.Text = DllEntryClass.LangSys.C("12 Hours");
                Disp12Hour.Tag = SAVE_PERIOD.SAVE_12_h;
                Disp12Hour.Click += new EventHandler(DispPerdiodIem_Click);
                itemDropDispPer.DropDownItems.Add(Disp12Hour);
            }
            if ((int)specProps.SavePeriod >= (int)SAVE_PERIOD.SAVE_1_j)
            {
                ToolStripMenuItem Disp1Day = new ToolStripMenuItem();
                Disp1Day.Name = "1Day";
                Disp1Day.Text = DllEntryClass.LangSys.C("1 Day");
                Disp1Day.Tag = SAVE_PERIOD.SAVE_1_j;
                Disp1Day.Click += new EventHandler(DispPerdiodIem_Click);
                itemDropDispPer.DropDownItems.Add(Disp1Day);
            }
            if ((int)specProps.SavePeriod >= (int)SAVE_PERIOD.SAVE_2_j)
            {
                ToolStripMenuItem Disp2Day = new ToolStripMenuItem();
                Disp2Day.Name = "2Days";
                Disp2Day.Text = DllEntryClass.LangSys.C("2 Days");
                Disp2Day.Tag = SAVE_PERIOD.SAVE_2_j;
                Disp2Day.Click += new EventHandler(DispPerdiodIem_Click);
                itemDropDispPer.DropDownItems.Add(Disp2Day);
            }
            if ((int)specProps.SavePeriod >= (int)SAVE_PERIOD.SAVE_4_j)
            {
                ToolStripMenuItem Disp4Day = new ToolStripMenuItem();
                Disp4Day.Name = "4Days";
                Disp4Day.Text = DllEntryClass.LangSys.C("4 Days");
                Disp4Day.Tag = SAVE_PERIOD.SAVE_4_j;
                Disp4Day.Click += new EventHandler(DispPerdiodIem_Click);
                itemDropDispPer.DropDownItems.Add(Disp4Day);
            }
            if ((int)specProps.SavePeriod >= (int)SAVE_PERIOD.SAVE_7_j)
            {
                ToolStripMenuItem Disp1Week = new ToolStripMenuItem();
                Disp1Week.Name = "1 Week";
                Disp1Week.Text = DllEntryClass.LangSys.C("1 Week");
                Disp1Week.Tag = SAVE_PERIOD.SAVE_7_j;
                Disp1Week.Click += new EventHandler(DispPerdiodIem_Click);
                itemDropDispPer.DropDownItems.Add(Disp1Week);
            }

            ToolStripMenuItem itemHideCurves = new ToolStripMenuItem();
            // This is the user-defined Tag so you can find this menu item later if necessary
            itemHideCurves.Name = "Curves_Visib";
            itemHideCurves.Tag = "Curves_Visib";
            // This is the text that will show up in the menu
            itemHideCurves.Text = DllEntryClass.LangSys.C("Curves Visibility");
            // Add a handler that will respond when that menu item is selected
            // Add the menu item to the menu
            menuStrip.Items.Add(itemHideCurves);

            for (int i = 0; i < m_ZedGraphCtrl.GraphPane.CurveList.Count; i++)
            {
                LineItem curve = m_ZedGraphCtrl.GraphPane.CurveList[i] as LineItem;
                if (curve == null)
                    continue;

                ToolStripMenuItem CurveItem = new ToolStripMenuItem();
                CurveItem.Name = string.Format("curve{0}", i);
                CurveItem.Text = curve.Label.Text;
                CurveItem.Tag = i;
                CurveItem.Checked = curve.Line.IsVisible;
                CurveItem.Click += new EventHandler(CurveItem_Click);
                itemHideCurves.DropDownItems.Add(CurveItem);
            }
        }

        void CurveItem_Click(object sender, EventArgs e)
        {
            if (sender != null)
            {
                int CurveIndex = (int)((ToolStripMenuItem)sender).Tag;
                LineItem curve = m_ZedGraphCtrl.GraphPane.CurveList[CurveIndex] as LineItem;
                curve.Line.IsVisible = !curve.Line.IsVisible;
                m_ZedGraphCtrl.Refresh();
            }
        }

        void DispPerdiodIem_Click(object sender, EventArgs e)
        {
            DllCtrlGraphProp specProps = (DllCtrlGraphProp)m_SourceCtrl.SpecificProp;
            CurrentDispPeriod = specProps.SavePeriod;
            if (sender != null)
            {
                CurrentDispPeriod = (SAVE_PERIOD)((ToolStripMenuItem)sender).Tag;
            }
            XDate BaseDate = new XDate(1899, 12, 30, 0, 0, 0);
            switch (CurrentDispPeriod)
            {
                case SAVE_PERIOD.SAVE_10_min:
                    BaseDate.AddMinutes(10);
                    break;
                case SAVE_PERIOD.SAVE_1_h:
                    BaseDate.AddHours(1);
                    break;
                case SAVE_PERIOD.SAVE_2_h:
                    BaseDate.AddHours(2);
                    break;
                case SAVE_PERIOD.SAVE_6_h:
                    BaseDate.AddHours(6);
                    break;
                case SAVE_PERIOD.SAVE_12_h:
                    BaseDate.AddHours(12);
                    break;
                case SAVE_PERIOD.SAVE_1_j:
                    BaseDate.AddDays(1);
                    break;
                case SAVE_PERIOD.SAVE_2_j:
                    BaseDate.AddDays(2);
                    break;
                case SAVE_PERIOD.SAVE_4_j:
                    BaseDate.AddDays(4);
                    break;
                case SAVE_PERIOD.SAVE_7_j:
                    BaseDate.AddDays(7);
                    break;
            }
            m_DisplayedRange = BaseDate;
            UpdateDispRange();
        }

        protected void UpdateDispRange()
        {
            if (m_ZedGraphCtrl.GraphPane.IsZoomed)
                m_ZedGraphCtrl.ZoomOutAll(m_ZedGraphCtrl.GraphPane);

            double XDateNow = new XDate(DateTime.Now);
            double XDateMax = XDateNow + (m_DisplayedRange / 10);
            double XDateMin = XDateMax - m_DisplayedRange;
            Scale xScale = m_ZedGraphCtrl.GraphPane.XAxis.Scale;
            xScale.Max = XDateMax;
            xScale.Min = XDateMin;
            CalcScaleSteps();
            // Scale the axes
            m_ZedGraphCtrl.AxisChange();
            // Force a redraw
            m_ZedGraphCtrl.Invalidate();
        }

        protected void CalcScaleSteps()
        {
            double TimeInMin = (double)CurrentDispPeriod;
            double MinStepInSec = (TimeInMin*60) / 100;
            MinStep = new XDate(0, 0, 0, 0, 0, MinStepInSec);
            Scale xScale = m_ZedGraphCtrl.GraphPane.XAxis.Scale;
            xScale.MinorStep = MinStep;
            xScale.MajorStep = MinStep * 10;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            InitGraphs();
            DispPerdiodIem_Click(null, null);
            UpdateDispRange();
        }

        /// <summary>
        /// 
        /// </summary>
        public void InitGraphs()
        {
            GraphPane myPane = m_ZedGraphCtrl.GraphPane;
            DllCtrlGraphProp specProps = (DllCtrlGraphProp)m_SourceCtrl.SpecificProp;
            myPane.Title.Text = specProps.GraphTitle;
            myPane.XAxis.Title.Text = specProps.XAxisTitle;
            myPane.YAxis.Title.Text = specProps.YAxisTitle;

            for (int i = 0; i < m_SourceCtrl.ListRefDatas.Count; i++)
            {
                Data Data = (Data)m_SourceCtrl.ListRefDatas[i];
                string strDataAlias = m_SourceCtrl.ListAliases[i];
                string usedText = string.IsNullOrEmpty(strDataAlias) ? Data.Symbol : strDataAlias;
                RollingPointPairList ptsList = new RollingPointPairList(m_SourceCtrl.QueueLenght);
                LineItem curve = myPane.AddCurve(usedText, ptsList, m_SourceCtrl.ColorList[i], SymbolType.None);
                curve.Tag = Data;
            }
            myPane.XAxis.Type = AxisType.Date;
        }

        public void DataLogNotify()
        {
            if (m_ZedGraphCtrl.GraphPane.CurveList.Count <= 0)
                return;

            PointPair ptPair = new PointPair();
            
            for (int i = 0; i < m_ZedGraphCtrl.GraphPane.CurveList.Count; i++)
            {
                LineItem curve = m_ZedGraphCtrl.GraphPane.CurveList[i] as LineItem;
                if (curve == null)
                    continue;
                IPointListEdit list = curve.Points as IPointListEdit;
                if (list == null)
                    continue;

                // Time is measured in seconds
                double X = new XDate(DateTime.Now);
                list.Add(X, ((Data)curve.Tag).Value);
            }

            Scale xScale = m_ZedGraphCtrl.GraphPane.XAxis.Scale;
            double XDateNow = new XDate(DateTime.Now);
            double XDateTemp = (m_DisplayedRange / 20);
            if (XDateNow > xScale.Max - XDateTemp && !m_ZedGraphCtrl.GraphPane.IsZoomed)
            {
                UpdateDispRange();
                //si on entre ici, l'invalidate est déja fait donc on return;
                return;
            }

            m_ZedGraphCtrl.AxisChange();
            // Force a redraw
            m_ZedGraphCtrl.Invalidate();
        }

        public void ClearCurves()
        {
            if (m_ZedGraphCtrl.GraphPane.CurveList.Count <= 0)
                return;

            // on vide les courbes
            for (int i = 0; i < m_ZedGraphCtrl.GraphPane.CurveList.Count; i++)
            {
                LineItem curve = m_ZedGraphCtrl.GraphPane.CurveList[i] as LineItem;
                if (curve == null)
                    continue;
                curve.Clear();
                // Make sure the Y axis is rescaled to accommodate actual data
                m_ZedGraphCtrl.AxisChange();
                // Force a redraw
                m_ZedGraphCtrl.Invalidate();
            }
            DllCtrlGraphProp specProps = (DllCtrlGraphProp)m_SourceCtrl.SpecificProp;
            GraphPane myPane = m_ZedGraphCtrl.GraphPane;
            UpdateDispRange();
        }
    }
}

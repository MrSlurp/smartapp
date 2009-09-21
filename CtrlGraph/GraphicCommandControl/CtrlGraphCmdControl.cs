using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
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
                    strMessage = string.Format("Data to log not found (Graphic {0}, Data {1}", m_strSymbol, strData);
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

        int tickStart = 0;

        // ajouter ici les données membres du control affiché
        ZedGraphControl m_ZedGraphCtrl = null;
        CtrlGraphCmdControl m_SourceCtrl = null;

        public CtrlGraphDispCtrl(CtrlGraphCmdControl srcCtrl)
        {
            m_SourceCtrl = srcCtrl;
            InitializeComponent();
        }

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
            this.m_ZedGraphCtrl.Size = this.Size;
            this.m_ZedGraphCtrl.TabIndex = 0;

            this.Controls.Add(this.m_ZedGraphCtrl);
            this.ResumeLayout(false);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            InitGraphs();
        }


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
            myPane.XAxis.Scale.Min = 0;
            myPane.XAxis.Scale.Max = (int)specProps.LogPeriod * 60;
            myPane.XAxis.Scale.MinorStep = (int)specProps.LogPeriod;
            myPane.XAxis.Scale.MajorStep = 5 * (int)specProps.LogPeriod;
            // Scale the axes
            m_ZedGraphCtrl.AxisChange();

            // Save the beginning time for reference
            tickStart = Environment.TickCount;
        }

        public void DataLogNotify()
        {
            if (m_ZedGraphCtrl.GraphPane.CurveList.Count <= 0)
                return;

            double time = (Environment.TickCount - tickStart) / 1000.0;

            for (int i = 0; i < m_ZedGraphCtrl.GraphPane.CurveList.Count; i++)
            {
                LineItem curve = m_ZedGraphCtrl.GraphPane.CurveList[i] as LineItem;
                if (curve == null)
                    continue;
                IPointListEdit list = curve.Points as IPointListEdit;
                if (list == null)
                    continue;

                // Time is measured in seconds
                list.Add(time, ((Data)curve.Tag).Value);
            }
            // Keep the X scale at a rolling 30 second interval, with one
            // major step between the max X value and the end of the axis
            Scale xScale = m_ZedGraphCtrl.GraphPane.XAxis.Scale;
            DllCtrlGraphProp specProps = (DllCtrlGraphProp)m_SourceCtrl.SpecificProp;
            if (time > xScale.Max - xScale.MajorStep)
            {
                xScale.Max = time + xScale.MajorStep;
                xScale.Min = xScale.Max - ((int)specProps.LogPeriod * 60);
            }

            // Make sure the Y axis is rescaled to accommodate actual data
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
            // on reprend le temps de base
            tickStart = Environment.TickCount;
        }
    }
}

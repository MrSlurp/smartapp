using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Collections;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using CommonLib;

namespace CtrlDataGrid
{
    /// <summary>
    /// Cette classe serta  définir le comportement du control lorsqu'il est executé dans SmartCommand
    /// </summary>
    internal class CtrlDataGridCmdControl : BTDllCtrlDataGridControl
    {
        #region donnée spécifiques aux fonctionement en mode Command
        // timer executant périodiquement les logs
        Timer m_Timer = new Timer();

        // liste des données a loguer utilisé en mode Command
        private ArrayList m_ListRefDatas = new ArrayList();
        private StringCollection m_ListAliases = new StringCollection();

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
    
        public override bool DisabledOnStop
        {
            get
            {
                return false;
            }
        }
            
        /// <summary>
        /// Constructeur de la classe
        /// </summary>
        public CtrlDataGridCmdControl()
        {

        }

        /// <summary>
        /// Constructeur de l'objet graphique affiché dans les écrans de supervision
        /// </summary>
        public override void CreateControl()
        {
            // on vérifie qu'il n'y a pas déja un control graphique (cette méthode ne doit être appelé qu'une seul fois)
            if (m_Ctrl == null)
            {
                // on crée l'objet graphique qui sera affiché
                m_Ctrl = new CtrlDataGridDispCtrl(this);
                m_Ctrl.Location = m_RectControl.Location;
                m_Ctrl.Name = m_strSymbol;
                m_Ctrl.Size = m_RectControl.Size;
                m_Ctrl.BackColor = Color.Transparent;
                // faites ici les initialisation spécifiques du control affiché
            }
        }

        /// <summary>
        /// Fonction existant par défaut pour gérer les évènement déclenché par le control affiché
        /// il est tout a fait possible d'en crée d'autre 
        /// </summary>
        /// <param name="Sender">objet ayant envoyé l'event</param>
        /// <param name="EventArgs">arguments l'event</param>
        public override void OnControlEvent(object Sender, EventArgs Args)
        {
            // traitez ici les évènement déclenché par le control (click souris par exemple)
            return;
        }

        /// <summary>
        /// Handler de l'évènement "DataValueChanged" déclenché par la donnée associée par défaut
        /// permet de mettre a jour l'état du control associé en fonction de celle ci.
        /// Pour chaque donnée qui serait utilisé dans les propriété, il est possible de réutiliser cet handler
        /// ou d'en crée d'autres (voir FinalizeRead pour la liaison des évènements)
        /// </summary>
        public override void UpdateFromData()
        {
            if (m_AssociateData != null && m_Ctrl != null)
            {
                // effectuez ici le traitement à executer lorsque la valeur change
            }
        }
    
        
        public override bool FinalizeRead(BTDoc Doc)
        {
            DllCtrlDataGridProp Props = (DllCtrlDataGridProp)m_SpecificProp;

            // on fait une liste de références directe sur les données
            for (int i = 0; i < DllCtrlDataGridProp.NB_DATA; i++)
            {
                string strData = Props.GetSymbol(i);
                Data Dat = (Data)Doc.GestData.GetFromSymbol(strData);
                if (Dat == null)
                {
                    if (string.IsNullOrEmpty(strData))
                        continue;

                    string strMessage;
                    strMessage = string.Format(DllEntryClass.LangSys.C("Data to log not found (DataGrid {0}, Data {1})"), m_strSymbol, strData);
                    LogEvent log = new LogEvent(LOG_EVENT_TYPE.INFO, strMessage);
                    AddLogEvent(log);
                    continue;
                }
                m_ListRefDatas.Add(Dat);
                m_ListAliases.Add(Props.GetAlias(i));
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

        /// <summary>
        /// Traite les message intra applicatif de SmartConfig
        /// Ces messages informes de : 
        /// - passage en RUN de la supervision
        /// - passage en STOP de la supervision
        /// </summary>
        /// <param name="Mess">Type du message</param>
        /// <param name="obj">paramètre du message (aucun paramètre dans SmartCommand)</param>
        /// <param name="TypeApp">Type de l'application courante (SmartConfig / SmartCommand)</param>
        public override void TraiteMessage(MESSAGE Mess, object obj, TYPE_APP TypeApp)
        {
            base.TraiteMessage(Mess, obj, TypeApp);
            if (TypeApp == TYPE_APP.SMART_COMMAND)
            {
                switch (Mess)
                {
                    // message de requête sur les conséquence d'une supression
                    case MESSAGE.MESS_CMD_STOP:
                        // traitez ici le passage en mode stop du control si nécessaire
                        m_Timer.Stop();
                        Traces.LogAddDebug(TraceCat.Plugin, "DataGrid Timer stop");
                        m_bTimerActive = false;
                        break;
                    case MESSAGE.MESS_CMD_RUN:
                        // traitez ici le passage en mode run du control si nécessaire
                        ((CtrlDataGridDispCtrl)m_Ctrl).ClearGrid();
                        Traces.LogAddDebug(TraceCat.Plugin, "DataGrid Timer start");
                        m_Timer.Start();
                        m_bTimerActive = true;
                        break;
                    default:
                        break;
                }
            }
        }
    
        private void OnTimerTick(object sender, EventArgs e)
        {
            m_Timer.Stop();
            Traces.LogAddDebug(TraceCat.Plugin, "DataGrid Timer tick");
            ((CtrlDataGridDispCtrl)m_Ctrl).DataLogNotify();
            if (m_bTimerActive)
                m_Timer.Start();
        }
    
    }

    /// <summary>
    /// classe héritant de UserControl
    /// représente l'objet graphique affiché dans la supervision
    /// on peux en faire a peut près ce qu'on veux :
    /// - du dessin
    /// - une aggregation de plusieurs controls standards, 
    /// - les deux, etc.
    /// </summary>
    internal class CtrlDataGridDispCtrl : UserControl
    {
        // ajouter ici les données membres du control affiché
        private System.ComponentModel.IContainer components = null;
        //SAVE_PERIOD CurrentDispPeriod = SAVE_PERIOD.SAVE_1_h;

        DataGridView m_Grid = null;
        CtrlDataGridCmdControl m_SourceCtrl = null;


        public CtrlDataGridDispCtrl(CtrlDataGridCmdControl srcControl)
        {
            m_SourceCtrl = srcControl;
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.m_Grid = new System.Windows.Forms.DataGridView();
            this.SuspendLayout();
            this.m_Grid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_Grid.Location = new System.Drawing.Point(0, 0);
            this.m_Grid.Name = "m_Grid";
            this.m_Grid.TabIndex = 0;
            this.Controls.Add(this.m_Grid);
            this.ResumeLayout(false);
        }
    
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            InitGrid();
        }
    
        public void InitGrid()
        {
            DllCtrlDataGridProp specProps = (DllCtrlDataGridProp)m_SourceCtrl.SpecificProp;
            DataGridViewTextBoxColumn colTime = new DataGridViewTextBoxColumn();
            colTime.Name = "ColTime"; 
            colTime.HeaderText= DllEntryClass.LangSys.C("Time"); 
            colTime.ReadOnly = true;   
            m_Grid.Columns.Add(colTime);
            for (int i = 0; i < m_SourceCtrl.ListRefDatas.Count; i++)
            {
                Data Data = (Data)m_SourceCtrl.ListRefDatas[i];
                if (Data != null)
                {
                    string strDataAlias = m_SourceCtrl.ListAliases[i];
                    string usedText = string.IsNullOrEmpty(strDataAlias) ? Data.Symbol : strDataAlias;
                    DataGridViewTextBoxColumn colData = new DataGridViewTextBoxColumn();
                    colData.Name = "col" + Data.Symbol;
                    colData.HeaderText= usedText; 
                    colData.ReadOnly = true;   
                    m_Grid.Columns.Add(colData);
                }
            }
        }
    
        public void DataLogNotify()
        {
            String trace = "DataGrid ajout d'un ligne ("; 
            string[] TabValues = new string[m_SourceCtrl.ListRefDatas.Count+1];
            TabValues[0] = DateTime.Now.ToString();
            for (int i = 0; i < m_SourceCtrl.ListRefDatas.Count; i++)
            {
                Data vData = m_SourceCtrl.ListRefDatas[i] as Data; 
                if (vData != null)
                {
                    TabValues[i+1] = vData.Value.ToString();
                    trace += TabValues[i+1];
                } 
            }
            trace += ")";
            Traces.LogAddDebug(TraceCat.Plugin, trace);
            m_Grid.Rows.Insert(0,TabValues);
            Traces.LogAddDebug(TraceCat.Plugin, "DataGrid QueueLenght = " + m_SourceCtrl.QueueLenght);
            while( m_Grid.Rows.Count > m_SourceCtrl.QueueLenght)
            {
                m_Grid.Rows.RemoveAt(m_Grid.Rows.Count-1);                
            }
        }
    
        public void ClearGrid()
        {
            Traces.LogAddDebug(TraceCat.Plugin, "DataGrid Clean");
            m_Grid.Rows.Clear();
        }            
    }
}

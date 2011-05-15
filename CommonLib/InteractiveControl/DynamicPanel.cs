/***************************************************************************/
// PROJET : BTCommand : system de commande paramétrable pour équipement
// ayant une mécanisme de commande par liaison série/ethernet/http
/***************************************************************************/
// Fichier : 
/***************************************************************************/
// description :
// 
/***************************************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;
using System.IO;
using System.Windows.Forms;


namespace CommonLib
{
    /// <summary>
    /// Utilisé dans SmartCommand, cette control est le panneau qui contiendra l'ensemble des controle d'un écran
    /// </summary>
    public partial class DynamicPanel : UserControl
    {
        public delegate void SetMeToTopEvent(Form MyParent);
        public delegate void SetToTopEvent();
        
        List<Control> m_ListToDrawManually = new List<Control>();
        // optimisation
        // pour chaque objet dessiner en fond il y a une liste des controls à rafraichir qui vont avec
        Hashtable m_MapBackDrawToListRefresh = new Hashtable();

        List<Control> m_ListNotDisabledControlOnStop = new List<Control>();
        public event SetMeToTopEvent SetMeToTop;

        private bool m_bInternalEnabled = true;

        public event AddLogEventDelegate EventAddLogEvent;
        
        protected string m_Title = string.Empty;
        
        protected string m_DocumentFileName = string.Empty;

        /// <summary>
        /// constructeur par défaut
        /// </summary>
        public DynamicPanel()
        {
            InitializeComponent();
            
        }

        public string Title
        {
            get
            {
                return m_Title;
            }
            set
            {
                m_Title = value;
            }
        }
    
        public string DocumentFileName
        {
            get
            {
                return m_DocumentFileName;
            }
            set
            {
                m_DocumentFileName = value;
            }
        }
            

        public bool SpecialEnabled
        {
            get
            {
                return m_bInternalEnabled;
            }
            set
            {
                m_bInternalEnabled = value;
                for (int i = 0; i < this.Controls.Count; i++)
                {
                    if (!m_ListNotDisabledControlOnStop.Contains(this.Controls[i]))
                        this.Controls[i].Enabled = m_bInternalEnabled;
                }
            }
        }

        /// <summary>
        /// Construit son affichage a partir des objets BaseControl crées a la 
        /// lecture du fichier
        /// </summary>
        /// <param name="ControlsList">Liste des controls a afficher
        /// tout les objets doivent dériver de BaseControl
        /// </param>
        public void MyInitializeComponent(List<BTControl> ControlsList)
        {
            this.SuspendLayout();
            for (int i = ControlsList.Count-1; i >=0 ; i--)
            {
                BTControl Ctrl = null;
                Ctrl = ControlsList[i];
                //Ctrl.CreateControl();
                if (Ctrl.DisplayedControl != null)
                {
                    Type tpCtrl = Ctrl.DisplayedControl.GetType();
                    if (tpCtrl.IsSubclassOf(typeof(DrawInParentCmdCtrl))
                        && ((DrawInParentCmdCtrl)Ctrl.DisplayedControl).DrownInParent
                        )
                    {
                        m_ListToDrawManually.Add(Ctrl.DisplayedControl);
                        ((DrawInParentCmdCtrl)Ctrl.DisplayedControl).SetDelgateRefresh(new NeedSuperposedRefresh(TraiteRefreshSuperposedItem));
                    }
                    if (!Ctrl.DisabledOnStop)
                        m_ListNotDisabledControlOnStop.Add(Ctrl.DisplayedControl);

                    this.Controls.Add(Ctrl.DisplayedControl);
                }
            }
            ResumeLayout(true);

            //optimisation, création des liste de refresh pour les controls dessiner sur le parent
            for (int i = 0; i < m_ListToDrawManually.Count; i++)
            {
                DrawInParentCmdCtrl dpCtrl = (DrawInParentCmdCtrl)m_ListToDrawManually[i];
                Rectangle drawRect = RectangleToClient(dpCtrl.RectangleToScreen(dpCtrl.ClientRectangle));

                List<Control> listCtrlToRef = new List<Control>();
                for (int j = 0; j < this.Controls.Count; j++)
                {
                    Rectangle OtherCtrlRect = RectangleToClient(this.Controls[j].RectangleToScreen(this.Controls[j].ClientRectangle));
                    if (OtherCtrlRect.IntersectsWith(drawRect) && !m_ListToDrawManually.Contains(this.Controls[j]))
                    {
                        listCtrlToRef.Add(this.Controls[j]);
                    }
                }
                m_MapBackDrawToListRefresh.Add(dpCtrl, listCtrlToRef);
            }
        }

        /// <summary>
        /// évènement appelé par le fonction de script permettant de placer cet écran au premier plan
        /// </summary>
        public void SetToTop()
        {
            if (SetMeToTop != null)
            {
                if (this.InvokeRequired)
                {
                    SetToTopEvent AsyncCall = new SetToTopEvent(SetToTop);
                    this.Invoke(AsyncCall);
                }
                else
                {
                    SetMeToTop(this.Parent as Form);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath"></param>
        public void DoScreenShot(string fileName, string ScreenSymbol)
        {
            string filePath = string.Empty;
            try
            {
                using (Graphics g = this.CreateGraphics())
                {
                    //new bitmap object to save the image
                    Bitmap bmp = new Bitmap(this.Width, this.Height, g);
                    //Drawing control to the bitmap

                    this.DrawToBitmap(bmp, new Rectangle(0, 0, this.Width, this.Height));

                    // truc de con, le Draw to bitmap fait un render inversé par rapport au paint
                    // il faut donc redessiner manuellement les controls dans l'ordre voulu pour que les superpositions
                    // se passent bien.
                    for (int i = this.Controls.Count -1; i >= 0; i--)
                    {
                        Control c = this.Controls[i];
                        c.DrawToBitmap(bmp, new Rectangle(c.Location.X, c.Location.Y, c.Width, c.Height));
                    }
                    
                    //string NowTime = DateTime.Now.ToString("dd_MM__HH-mm-ss");  
                    string NowTime = DateTime.Now.ToString("yyyy-MM_dd_HH-mm-ss");
                    string outFileName;

                    if (string.IsNullOrEmpty(fileName))
                    {
                        if (string.IsNullOrEmpty(m_Title))
                            outFileName = ScreenSymbol + "_" + NowTime;
                        else 
                            outFileName = m_Title+ "_" + NowTime;
                    }
                    else
                        outFileName = fileName + "_" + NowTime;

                    outFileName = outFileName.Replace(" ", "_") + ".png";
                        
                    filePath = Path.GetDirectoryName(m_DocumentFileName) + 
                               Path.DirectorySeparatorChar + 
                               "SnapShot" +
                               Path.DirectorySeparatorChar + outFileName;
                               
                    if (Traces.IsDebugAndCatOK(TraceCat.ExecuteScreen))
                        Traces.LogAddDebug(TraceCat.ExecuteScreen, 
                                           string.Format("SnapShot {0} written", filePath));
                                           
                    Directory.CreateDirectory(Path.GetDirectoryName(filePath));
                    bmp.Save(filePath, ImageFormat.Png);
                    bmp.Dispose();
                }
            }
            catch (Exception e)
            {
                LogEvent log = new LogEvent(LOG_EVENT_TYPE.ERROR, string.Format("Error creating screen shot {0}", filePath));
                AddLogEvent(log);
                if (Traces.IsDebugAndCatOK(TraceCat.ExecuteScreen))
                    Traces.LogAddDebug(TraceCat.ExecuteScreen, 
                                       string.Format("Error While creating SnapShot {0} ({1})", filePath, e.Message));
            }
        }


        /// <summary>
        /// provoque le rafraichissementg des controls superposés à un control qui se dessine en fond
        /// </summary>
        /// <param name="ctrl">control se dessinant en fond</param>
        protected void TraiteRefreshSuperposedItem(Control ctrl)
        {
            List<Control> listCtrlToRef = (List<Control>)m_MapBackDrawToListRefresh[ctrl];
            if (listCtrlToRef != null)
            {
                for (int i = 0; i < listCtrlToRef.Count; i++)
                {
                    listCtrlToRef[i].Refresh();
                }
            }
        }

        /// <summary>
        /// surcharge de la fonction de dessin
        /// </summary>
        /// <param name="e">paramètres de dessin</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            // pour chaque qui se dessine en fond
            // on appel la fonction de dessin dans la parent pour que celui ci se dessine ce graphics
            for (int i = 0; i < m_ListToDrawManually.Count; i++)
            {
                DrawInParentCmdCtrl dpCtrl = (DrawInParentCmdCtrl)m_ListToDrawManually[i];
                Rectangle drawRect = RectangleToClient(dpCtrl.RectangleToScreen(dpCtrl.ClientRectangle));
                dpCtrl.OnPaintInParent(e.Graphics, drawRect);
            }
            base.OnPaint(e);
        }

        #region fonction diverses
        public void AddLogEvent(LogEvent Event)
        {
            if (EventAddLogEvent != null)
            {
                EventAddLogEvent(Event);
            }
        }
        #endregion

    }
}
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
using System.Text;
using System.Windows.Forms;


namespace CommonLib
{
    /// <summary>
    /// Utilisé dans SmartCommand, cette control est le panneau qui contiendra l'ensemble des controle d'un écran
    /// </summary>
    public partial class DynamicPanel : UserControl
    {
        public delegate void SetMeToTopEvent(Form MyParent);
        
        List<Control> m_ListToDrawManually = new List<Control>();
        // optimisation
        // pour chaque objet dessiner en fond il y a une liste des controls à rafraichir qui vont avec
        Hashtable m_MapBackDrawToListRefresh = new Hashtable();

        List<Control> m_ListNotDisabledControlOnStop = new List<Control>();
        public event SetMeToTopEvent SetMeToTop;

        private bool m_bInternalEnabled = true;

        public event AddLogEventDelegate EventAddLogEvent;

        /// <summary>
        /// constructeur par défaut
        /// </summary>
        public DynamicPanel()
        {
            InitializeComponent();
            
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
                SetMeToTop((Form)this.Parent);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath"></param>
        public void DoScreenShot(string filePath)
        {
            //"Image File (*.png)|*.png";
            //TODO faire le screen shot du panel
            try
            {
                using (Graphics g = this.CreateGraphics())
                {
                    //new bitmap object to save the image
                    Bitmap bmp = new Bitmap(this.Width, this.Height);
                    //Drawing control to the bitmap
                    this.DrawToBitmap(bmp, new Rectangle(0, 0, this.Width, this.Height));

                    bmp.Save(filePath);
                    bmp.Dispose();
                }
            }
            catch (Exception)
            {
                LogEvent log = new LogEvent(LOG_EVENT_TYPE.ERROR, string.Format("Error creating screen shot {0}", filePath));
                AddLogEvent(log);
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
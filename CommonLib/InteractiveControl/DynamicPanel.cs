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

        public event SetMeToTopEvent SetMeToTop;
        /// <summary>
        /// constructeur par défaut
        /// </summary>
        public DynamicPanel()
        {
            InitializeComponent();
            
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
    }
}
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
    public partial class DynamicPanel : UserControl
    {

        //ControlCollection m_ListToDrawManually = null;
        List<Control> m_ListToDrawManually = new List<Control>();
        // optimisation
        // pour chaque objet dessiner en fond il y a une liste des controls à rafraichir qui vont avec
        Hashtable m_MapBackDrawToListRefresh = new Hashtable();
        /// <summary>
        /// 
        /// </summary>
        public DynamicPanel()
        {
            InitializeComponent();
            //m_ListToDrawManually = new ControlCollection(this);
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

        protected override void OnPaint(PaintEventArgs e)
        {
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
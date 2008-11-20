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
            for (int i = 0; i < ControlsList.Count; i++)
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
                    }
                    this.Controls.Add(Ctrl.DisplayedControl);
            }
            }
            ResumeLayout(true);
        }

        protected override void OnControlAdded(ControlEventArgs e)
        {
            base.OnControlAdded(e);
        }

        protected override void OnControlRemoved(ControlEventArgs e)
        {
            base.OnControlRemoved(e);
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
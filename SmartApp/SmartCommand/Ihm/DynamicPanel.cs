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

using SmartApp.Controls;

namespace SmartApp.Ihm
{
    public partial class DynamicPanel : UserControl
    {

        /// <summary>
        /// 
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
        public void MyInitializeComponent(ArrayList ControlsList)
        {
            this.SuspendLayout();
            for (int i = 0; i < ControlsList.Count; i++)
            {
                BaseControl Ctrl = null;
                Ctrl = (BaseControl)ControlsList[i];
                Ctrl.CreateControl();
                if (Ctrl.DisplayedControl != null)
                    this.Controls.Add(Ctrl.DisplayedControl);
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
    }
}
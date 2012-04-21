using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.IO;
using System.Windows.Forms;
using CommonLib;

namespace GradientBaloon
{
    public partial class GradientBaloonProperties : BaseControlPropertiesPanel, ISpecificPanel
    {

        public GradientBaloonProperties()
        {
            DllEntryClass.LangSys.Initialize(this);
            InitializeComponent();
        }

        #region validation des données
        public void PanelToObject()
        {
            /*
            bool bDataPropChange = false;

            // testez ici si les paramètres ont changé en les comparant avec ceux contenu dans les propriété
            // spécifiques du BTControl
            // si c'est le cas, assignez bDataPropChange à true;

            if (bDataPropChange)
            {
                Document.Modified = true;
            }*/
        }

        public void ObjectToPanel()
        {

        }


        #endregion
    }
}

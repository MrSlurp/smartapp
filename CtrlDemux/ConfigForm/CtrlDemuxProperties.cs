using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.IO;
using System.Windows.Forms;
using CommonLib;

namespace CtrlDemux
{
    public partial class CtrlDemuxProperties : BaseControlPropertiesPanel, ISpecificPanel
    {
        #region events
        public event ControlPropertiesChange ControlPropertiesChanged;
        #endregion

        public CtrlDemuxProperties()
        {
            DllEntryClass.LangSys.Initialize(this);
            InitializeComponent();
        }

        #region validation des données
        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public override bool IsObjectPropertiesValid
        {
            get
            {
                if (this.ConfiguredItem == null)
                    return true;

                return true;
            }
        }


        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public override bool ValidateProperties()
        {
            if (this.ConfiguredItem == null)
                return true;

            bool bDataPropChange = false;

            // testez ici si les paramètres ont changé en les comparant avec ceux contenu dans les propriété
            // spécifiques du BTControl
            // si c'est le cas, assignez bDataPropChange à true;

            if (bDataPropChange)
            {
                Document.Modified = true;
                m_Control.IControl.Refresh();
            }
            if (bDataPropChange && ControlPropertiesChanged != null)
                ControlPropertiesChanged(m_Control);
            return true;
        }

        public void PanelToObject()
        {

        }

        public void ObjectToPanel()
        {

        }

        #endregion

        private void btnConfig_Click(object sender, EventArgs e)
        {
            DemuxConfigForm CfgForm = new DemuxConfigForm();
            CfgForm.Doc = this.Document;
            CfgForm.Props = (DllCtrlDemuxProp)this.m_Control.SpecificProp;

            if (CfgForm.ShowDialog() == DialogResult.OK)
            {
                // si OK on recopie les param
                this.m_Control.SpecificProp.CopyParametersFrom(CfgForm.Props, false);
            }

        }
    }
}

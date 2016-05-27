using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using CommonLib;

namespace CtrlDemux
{
    internal class BTDllCtrlDemuxControl : BasePluginBTControl
    {
        public BTDllCtrlDemuxControl(BTDoc document)
            : base(document)
        {
            m_IControl = new InteractiveCtrlDemuxDllControl();
            if (m_IControl != null)
                m_IControl.SourceBTControl = this;

            m_SpecificProp = new DllCtrlDemuxProp(this.ItemScripts);
        }

        public BTDllCtrlDemuxControl(BTDoc document, InteractiveControl Ctrl)
            : base(document, Ctrl)
        {
            m_SpecificProp = new DllCtrlDemuxProp(this.ItemScripts);
        }


        public override uint DllControlID
        {
            get
            {
                return CtrlDemux.DllEntryClass.DLL_Control_ID;
            }
        }
		
        /// <summary>
        /// renvoie true si le control à au moins une donnée associé qui rend son état dynamique
        /// Utilisé pour masquer les controls lors de l'enregistrement de l'image de fond de plan
        /// </summary>
        public override bool HaveDataAssociation
        {
            get
            {
                return true;
            }
        } 

    }
}

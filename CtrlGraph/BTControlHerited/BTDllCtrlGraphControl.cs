using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using CommonLib;

namespace CtrlGraph
{
    internal class BTDllCtrlGraphControl : BasePluginBTControl
    {
        public BTDllCtrlGraphControl(BTDoc document)
            : base(document)
        {
            m_IControl = new InteractiveCtrlGraphDllControl();
            if (m_IControl != null)
                m_IControl.SourceBTControl = this;

            m_SpecificProp = new DllCtrlGraphProp(this.ItemScripts);
        }

        public BTDllCtrlGraphControl(BTDoc document, InteractiveControl Ctrl)
            : base(document, Ctrl)
        {
            m_SpecificProp = new DllCtrlGraphProp(this.ItemScripts);
        }

        public override uint DllControlID
        {
            get
            {
                return CtrlGraph.DllEntryClass.DLL_Control_ID;
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

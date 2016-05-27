using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using CommonLib;

namespace CtrlDataComp
{
    internal class BTDllCtrlDataCompControl : BasePluginBTControl
    {

        /// <summary>
        /// Constructeur de la classe
        /// </summary>
        public BTDllCtrlDataCompControl(BTDoc document)
            : base(document)
        {
            m_IControl = new InteractiveCtrlDataCompDllControl();
            if (m_IControl != null)
                m_IControl.SourceBTControl = this;

            m_SpecificProp = new DllCtrlDataCompProp(this.ItemScripts);
        }

        /// <summary>
        /// Constructeur de la à partir d'un control interactif
        /// </summary>
        public BTDllCtrlDataCompControl(BTDoc document, InteractiveControl Ctrl)
            : base(document, Ctrl)
        {
            m_SpecificProp = new DllCtrlDataCompProp(this.ItemScripts);
        }

        /// <summary>
        /// Renvoie l'ID de la DLL
        /// </summary>
        public override uint DllControlID
        {
            get
            {
                return CtrlDataComp.DllEntryClass.DLL_Control_ID;
            }
        }
    }
}

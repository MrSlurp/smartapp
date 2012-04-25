using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using CommonLib;

namespace DigitalDisplay
{
    internal class BTDllDigitalDisplayControl : BasePluginBTControl
    {
        public BTDllDigitalDisplayControl(BTDoc document)
            : base(document)
        {
            m_IControl = new InteractiveDigitalDisplayDllControl();
            if (m_IControl != null)
                m_IControl.SourceBTControl = this;

            m_SpecificProp = new DllDigitalDisplayProp(this.ItemScripts);
        }

        public BTDllDigitalDisplayControl(BTDoc document, InteractiveControl Ctrl)
            : base(document, Ctrl)
        {
            m_SpecificProp = new DllDigitalDisplayProp(this.ItemScripts);
        }

        public override uint DllControlID
        {
            get
            {
                return DigitalDisplay.DllEntryClass.DLL_Control_ID;
            }
        }

    }
}

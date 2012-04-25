using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using CommonLib;

namespace CtrlJauge
{
    internal class BTDllCtrlJaugeControl : BasePluginBTControl
    {
        public BTDllCtrlJaugeControl(BTDoc document)
            : base(document)
        {
            m_IControl = new InteractiveCtrlJaugeDllControl();
            if (m_IControl != null)
                m_IControl.SourceBTControl = this;

            m_SpecificProp = new DllCtrlJaugeProp(this.ItemScripts);
        }

        public BTDllCtrlJaugeControl(BTDoc document, InteractiveControl Ctrl)
            : base(document, Ctrl)
        {
            m_SpecificProp = new DllCtrlJaugeProp(this.ItemScripts);
        }

        public override uint DllControlID
        {
            get
            {
                return CtrlJauge.DllEntryClass.DLL_Control_ID;
            }
        }

    }
}

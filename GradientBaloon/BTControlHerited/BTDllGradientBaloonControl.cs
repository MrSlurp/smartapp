using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using CommonLib;

namespace GradientBaloon
{
    internal class BTDllGradientBaloonControl : BasePluginBTControl
    {
        public BTDllGradientBaloonControl(BTDoc document)
            : base(document)
        {
            m_IControl = new InteractiveGradientBaloonDllControl();
            if (m_IControl != null)
                m_IControl.SourceBTControl = this;

            m_SpecificProp = new DllGradientBaloonProp(this.ItemScripts);
        }

        public BTDllGradientBaloonControl(BTDoc document, InteractiveControl Ctrl)
            : base(document, Ctrl)
        {
            m_SpecificProp = new DllGradientBaloonProp(this.ItemScripts);
        }

        public override uint DllControlID
        {
            get
            {
                return GradientBaloon.DllEntryClass.DLL_Control_ID;
            }
        }

    }
}

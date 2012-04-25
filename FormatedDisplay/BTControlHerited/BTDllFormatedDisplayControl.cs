using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using CommonLib;

namespace FormatedDisplay
{
    internal class BTDllFormatedDisplayControl : BasePluginBTControl
    {

        public BTDllFormatedDisplayControl(BTDoc document)
            : base(document)
        {
            m_IControl = new InteractiveFormatedDisplayDllControl();
            if (m_IControl != null)
                m_IControl.SourceBTControl = this;

            m_SpecificProp = new DllFormatedDisplayProp(this.ItemScripts);
        }

        public BTDllFormatedDisplayControl(BTDoc document, InteractiveControl Ctrl)
            : base(document, Ctrl)
        {
            m_SpecificProp = new DllFormatedDisplayProp(this.ItemScripts);
        }

        public override uint DllControlID
        {
            get
            {
                return FormatedDisplay.DllEntryClass.DLL_Control_ID;
            }
        }

    }
}

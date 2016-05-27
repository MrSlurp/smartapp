using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using CommonLib;

namespace CtrlSerpentin
{
    internal class BTSerpentinControl : BasePluginBTControl
    {
        public BTSerpentinControl(BTDoc document)
            : base(document)
        {
            m_IControl = new InteractiveSerpentin();
            if (m_IControl != null)
                m_IControl.SourceBTControl = this;

            m_SpecificProp = new TwoColorProp(this.ItemScripts);
        }

        public BTSerpentinControl(BTDoc document, InteractiveControl Ctrl)
            : base(document, Ctrl)
        {
            m_SpecificProp = new TwoColorProp(this.ItemScripts);
        }

        public override uint DllControlID
        {
            get
            {
                return DllEntryClass.Serpentin_Control_ID;
            }
        }

    }
}

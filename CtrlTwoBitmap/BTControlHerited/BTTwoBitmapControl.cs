using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using CommonLib;

namespace CtrlTwoBitmap
{
    internal class BTTwoBitmapControl : BasePluginBTControl
    {

        public BTTwoBitmapControl(BTDoc document)
            : base(document)
        {
            m_IControl = new InteractiveTwoBitmap();
            if (m_IControl != null)
                m_IControl.SourceBTControl = this;

            m_SpecificProp = new TwoBitmapProp(this.ItemScripts);
        }

        public BTTwoBitmapControl(BTDoc document, InteractiveControl Ctrl)
            : base(document, Ctrl)
        {
            m_SpecificProp = new TwoBitmapProp(this.ItemScripts);
        }

        public override uint DllControlID
        {
            get
            {
                return DllEntryClass.TwoBitmap_Control_ID;
            }
        }

    }
}

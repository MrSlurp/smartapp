using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using CommonLib;

namespace FourBitmap
{
    internal class BTDllFourBitmapControl : BasePluginBTControl
    {

        public BTDllFourBitmapControl(BTDoc document)
            : base(document)
        {
            m_IControl = new InteractiveFourBitmapDllControl();
            if (m_IControl != null)
                m_IControl.SourceBTControl = this;

            m_SpecificProp = new DllFourBitmapProp(this.ItemScripts);
        }

        public BTDllFourBitmapControl(BTDoc document, InteractiveControl Ctrl)
            : base(document, Ctrl)
        {
            m_SpecificProp = new DllFourBitmapProp(this.ItemScripts);
        }

        public override uint DllControlID
        {
            get
            {
                return FourBitmap.DllEntryClass.DLL_Control_ID;
            }
        }

    }
}

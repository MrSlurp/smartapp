using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace CommonLib
{
    public interface ISpecificControl
    {
        void SelfPaint(Graphics gr, Control ctrl);
        UserControl SpecificPropPanel { get;}
        StandardPropEnabling StdPropEnabling { get;}
        SpecificGraphicProp SpecGraphicProp { get;}
    }

    public struct StandardPropEnabling
    {
        public bool m_bEditTextEnabled;
        public bool m_bEditAssociateDataEnabled;
        public bool m_bcheckReadOnlyEnabled;
        public bool m_bcheckReadOnlyChecked;
        public bool m_bcheckScreenEventEnabled;
        public bool m_bcheckScreenEventChecked;
        public bool m_bCtrlEventScriptEnabled;
    }

    public struct SpecificGraphicProp
    {
        public bool m_bcanResizeWidth;
        public bool m_bcanResizeHeight;
        public Size m_MinSize;
    }
}

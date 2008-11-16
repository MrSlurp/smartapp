using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace CommonLib
{
    public interface IDllControlInterface
    {
        uint DllID { get; }
        BTControl CreateBTControl();
        BTControl CreateBTControl(InteractiveControl iCtrl);
        BTControl CreateCommandBTControl();
        InteractiveControl CreateInteractiveControl();
        string DefaultControlName { get;}
        //UserControl SpecificPropPanel { get;}
        Size ToolWindSize { get;}
    }
}

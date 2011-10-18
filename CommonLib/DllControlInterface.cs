using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Xml;

namespace CommonLib
{
    public interface IDllControlInterface
    {
        uint DllID { get; }
        string CurrentLang { get; set;}
        BTControl CreateBTControl();
        BTControl CreateBTControl(InteractiveControl iCtrl);
        BTControl CreateCommandBTControl();
        InteractiveControl CreateInteractiveControl();
        string DefaultControlName { get;}
        //UserControl SpecificPropPanel { get;}
        Size ToolWindSize { get;}
        bool ReadInModuleGlobalInfo(XmlNode DllInfoNode);
        bool WriteOutModuleGlobalInfo(XmlDocument document, XmlNode XmlGlobalNode);
    }
}

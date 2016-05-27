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
        BTControl CreateBTControl(BTDoc document);
        BTControl CreateBTControl(BTDoc document, InteractiveControl iCtrl);
        BTControl CreateCommandBTControl(BTDoc document);
        InteractiveControl CreateInteractiveControl();
        string DefaultControlName { get;}
        //UserControl SpecificPropPanel { get;}
        Size ToolWindSize { get;}
        string PluginType { get; }
        bool ReadInModuleGlobalInfo(XmlNode DllInfoNode);
        bool WriteOutModuleGlobalInfo(XmlDocument document, XmlNode XmlGlobalNode);
    }
}

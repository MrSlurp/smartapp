using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace CommonLib
{
    public interface IObjectPropertyPanel 
    {
        Control Panel { get; }
        BTDoc Document { get; set; }
        BaseObject ConfiguredItem { get; set; }
        BaseGest ConfiguredItemGest { get; set; }

        bool IsObjectPropertiesValid { get; }

        bool ValidateProperties();

        void ObjectToPanel();

        void PanelToObject();

    }
}

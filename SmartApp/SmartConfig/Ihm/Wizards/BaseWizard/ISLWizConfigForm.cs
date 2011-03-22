using System;
using System.Collections.Generic;
using System.Text;

namespace SmartApp.Wizards
{
    interface IWizConfigForm
    {
        WizardConfigData WizConfig
        { set; }

        void HmiToData();
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace SmartApp.Ihm.Wizards
{
    interface ISLWizConfigForm
    {
        SLWizardConfigData WizConfig
        { set; }

        void HmiToData();
    }
}

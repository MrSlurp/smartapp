using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace SmartApp.Wizards
{
    /// <summary>
    /// interface des panneau de configuration des project wizard
    /// </summary>
    interface IWizConfigForm
    {
        WizardConfigData WizConfig
        { set; }

        void HmiToData();

        AnchorStyles Anchor
        { get; set;}

        bool Visible
        { get; set;}

        int Width
        { get; set;}

        int Height
        { get; set;}

        /*
        bool CanGoBack();

        bool CanGoNext();*/
    }
}

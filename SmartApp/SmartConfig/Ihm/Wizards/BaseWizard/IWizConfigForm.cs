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
        /// <summary>
        /// donnée de configuration du wizard
        /// </summary>
        WizardConfigData WizConfig
        { set; }

        /// <summary>
        /// fonction effectuant le transfert entre l'IHM et les config du Wizard
        /// </summary>
        void HmiToData();

        /// <summary>
        /// position d'attachement de la form dans le parent
        /// </summary>
        AnchorStyles Anchor
        { get; set;}

        /// <summary>
        /// définit si le panel est visible
        /// </summary>
        bool Visible
        { get; set;}

        /// <summary>
        /// assigne ou obtient la largeur du panel
        /// </summary>
        int Width
        { get; set;}

        /// <summary>
        /// assigne ou obtient la hauteur du panel
        /// </summary>
        int Height
        { get; set;}

        // provision pour plus tard sans doute.
        /*
        bool CanGoBack();

        bool CanGoNext();*/
    }
}

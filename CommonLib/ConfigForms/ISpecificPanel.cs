using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLib
{
    /// <summary>
    /// interface à implémenter par les panneaux de propriété spécifiques des controles DLL
    /// </summary>
    public interface ISpecificPanel
    {
        #region attributs de l'interface
        /// <summary>
        /// acesseur de l'objet BTControl qui contiens les paramètres
        /// </summary>
        BTControl BTControl { get; set;}

        /// <summary>
        /// accesseur du document
        /// </summary>
        BTDoc Doc { get; set;}

        /// <summary>
        /// vérifie si les paramètres entré par l'utilisateur don cohérents
        /// </summary>
        bool IsDataValuesValid { get;}
        #endregion

        #region méthodes publiques de l'interface
        /// <summary>
        /// Valide les paramètres saisis par l'utilisateur (transfert les valeur de 
        /// la fenêtre de propriété vers l'objet BTControl
        /// </summary>
        /// <returns></returns>
        bool ValidateValues();
        #endregion

        #region évènements de l'interface
        /// <summary>
        /// évènement déclenché lors que les propriété de l'objet on changé
        /// </summary>
        event ControlPropertiesChange ControlPropertiesChanged;
        #endregion
    }
}

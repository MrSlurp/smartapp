using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLib
{
    /// <summary>
    /// interface à implémenter par les panneaux de propriété spécifiques des controles DLL
    /// </summary>
    public interface ISpecificPanel : IObjectPropertyPanel
    {
        #region évènements de l'interface
        /// <summary>
        /// évènement déclenché lors que les propriété de l'objet on changé
        /// </summary>
        event ControlPropertiesChange ControlPropertiesChanged;
        #endregion
    }
}

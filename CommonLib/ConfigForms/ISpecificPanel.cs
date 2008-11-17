using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLib
{
    public interface ISpecificPanel
    {
        BTControl BTControl { get; set;}
        BTDoc Doc { get; set;}
        bool IsDataValuesValid { get;}
        bool ValidateValues();
        event ControlPropertiesChange ControlPropertiesChanged;
    }
}

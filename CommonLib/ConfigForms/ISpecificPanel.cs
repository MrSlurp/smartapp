using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLib
{
    interface ISpecificPanel
    {
        BTControl BTControl { get; set;}
        BTDoc Doc { get; set;}
    }
}

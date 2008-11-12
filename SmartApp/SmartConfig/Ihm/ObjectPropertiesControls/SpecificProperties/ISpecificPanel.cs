using System;
using System.Collections.Generic;
using System.Text;
using SmartApp.Datas;

namespace SmartApp.Ihm
{
    interface ISpecificPanel
    {
        BTControl BTControl { get; set;}
        BTDoc Doc { get; set;}
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace ImageButton
{
    public partial class ImageButtonDispCtrl : Button
    {
        public ImageButtonDispCtrl()
        {
            this.BackgroundImageLayout = ImageLayout.Stretch;
        }
    }
}

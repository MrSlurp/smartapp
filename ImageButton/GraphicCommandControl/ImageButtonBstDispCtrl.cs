using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace ImageButton
{
    public partial class ImageButtonBstDispCtrl : CheckBox
    {
        public ImageButtonBstDispCtrl()
        {
            this.BackgroundImageLayout = ImageLayout.Stretch;
            this.Appearance = Appearance.Button;
            this.UseVisualStyleBackColor = true;
            this.TextAlign = ContentAlignment.MiddleCenter;
        }
    }
}

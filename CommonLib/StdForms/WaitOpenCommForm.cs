using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CommonLib
{
    public partial class WaitOpenCommForm : Form
    {
        public WaitOpenCommForm()
        {
            Lang.LangSys.Initialize(this);
            InitializeComponent();
        }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SmartApp
{
    public partial class AboutForm : Form
    {
        static public void ShowAbout()
        {
            AboutForm frm = new AboutForm();
            frm.ShowDialog();
        }

        public AboutForm()
        {
            InitializeComponent();
            labelVersion.Text = Application.ProductVersion;
        }
    }
}
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
            this.label1.Text = "Smart Application Suite.";
            this.label2.Text = "Copyright Pascal Bigot 2007 - 2009";
            this.label3.Text = "This development is realized totally outside of any professional ";
#if KENNEN
            this.label4.Text = "activities. Developer can’t be responsible for any damages or ";
            this.label5.Text = "miss-functioning that occur through the use of this free software ";
            this.label6.Text = "witch is developed only under privates activities.";
            this.label7.Visible = false;
#else
            this.label4.Text = "activities and with no support of Crouzet. Crouzet or developer ";
            this.label5.Text = "can’t be responsible for any damages or miss-functioning that occur ";
            this.label6.Text = "through the use of this free software witch is developed only under";
            this.label7.Text = "privates activities.";
#endif

        }
    }
}
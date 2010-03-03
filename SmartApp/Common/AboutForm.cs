using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CommonLib;

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
            this.label1.Text = Lang.C("Smart Application Suite.", Program.LangSys);
            this.label2.Text = Lang.C("Copyright Pascal Bigot 2007 - 2009",Program.LangSys);
            this.label3.Text = Lang.C("This development is realized totally outside of any professional ", Program.LangSys);
#if KENNEN
            this.label4.Text = "activities. Developer can’t be responsible for any damages or ";
            this.label5.Text = "miss-functioning that occur through the use of this free software ";
            this.label6.Text = "witch is developed only under privates activities.";
            this.label7.Visible = false;
#else
            this.label4.Text = Lang.C("activities and with no support of Crouzet. Crouzet or developer ", Program.LangSys);
            this.label5.Text = Lang.C("can’t be responsible for any damages or miss-functioning that occur ", Program.LangSys);
            this.label6.Text = Lang.C("through the use of this free software witch is developed only under", Program.LangSys);
            this.label7.Text = Lang.C("privates activities.", Program.LangSys);
#endif

        }
    }
}
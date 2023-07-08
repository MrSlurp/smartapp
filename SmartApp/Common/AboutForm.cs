/*
    This file is part of SmartApp.
    Copyright (C) 2007-2016  Pascal Bigot

    SmartApp is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    SmartApp is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with Foobar.  If not, see <http://www.gnu.org/licenses/>.
*/
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
            Program.LangSys.Initialize(this);
            InitializeComponent();
            labelVersion.Text = Application.ProductVersion;
            this.label1.Text = Program.LangSys.C("Smart Application Suite.");
            this.label2.Text = Program.LangSys.C("Copyright Pascal Bigot 2007 - 2012");
            this.label3.Text = Program.LangSys.C("This development is realized totally outside of any professional ");
            this.label4.Text = Program.LangSys.C("activities and with no support of Crouzet. Crouzet or developer ");
            this.label5.Text = Program.LangSys.C("can't be responsible for any damages or miss-functioning that occur ");
            this.label6.Text = Program.LangSys.C("through the use of this free software witch is developed only under");
            this.label7.Text = Program.LangSys.C("privates activities.");
        }
    }
}
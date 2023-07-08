﻿/*
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
using System.IO;
using System.Windows.Forms;

namespace CommonLib
{
    public partial class TraceConsole : Form
    {
        TextWriter _writer = null;
        TextWriter _defaultOut;
        public TraceConsole()
        {
            InitializeComponent();
        }

        private void TraceConsole_Load(object sender, EventArgs e)
        {
            _defaultOut = Console.Out;
            _writer = new TextBoxStreamWriter(txtConsole);
            Console.SetOut(_writer);
            Console.WriteLine("Trace Console Initialized");
        }

        private void TraceConsole_FormClosed(object sender, FormClosedEventArgs e)
        {
            Console.SetOut(_defaultOut);
            _writer.Close();
        }

        private void btn_clear_Click(object sender, EventArgs e)
        {
            txtConsole.Clear();
        }
    }
}
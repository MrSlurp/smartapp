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
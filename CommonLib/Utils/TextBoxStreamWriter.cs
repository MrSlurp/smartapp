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
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace CommonLib
{
    public class TextBoxStreamWriter : TextWriter
    {
        TextBox _output = null;
        StringBuilder m_Builder = new StringBuilder(2000);
 
        delegate void WriteText(string value);
        public TextBoxStreamWriter(TextBox output)
        {
            _output = output;
        }
 
        public override void Write(char value)
        {
            base.Write(value);
            m_Builder = m_Builder.Append(value);
            if (value == '\n')
            {
                if (_output.InvokeRequired)
                {
                    WriteText d = new WriteText(InvokedWriteString);
                    _output.Invoke(d, m_Builder.ToString());
                    m_Builder = new StringBuilder(2000);
                }
                else
                {
                    _output.AppendText(m_Builder.ToString()); // When character data is written, append it to the text box.
                    m_Builder = new StringBuilder(2000);
                }
            }
        }

        private void InvokedWriteString(string value)
        {
            _output.AppendText(value); // When character data is written, append it to the text box.
        }
 
        public override Encoding Encoding
        {
            get { return System.Text.Encoding.UTF8; }
        }
    }
}

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
                _output.AppendText(m_Builder.ToString()); // When character data is written, append it to the text box.
                m_Builder = new StringBuilder(2000);
            }
        }
 
        public override Encoding Encoding
        {
            get { return System.Text.Encoding.UTF8; }
        }
    }
}

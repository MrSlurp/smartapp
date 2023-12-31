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
using System.Text;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using CommonLib;

namespace SmartApp
{
    public class FormsOptions
    {
        private IniFileParser m_IniOptionFile = new IniFileParser();

        public FormsOptions()
        {
        }

        public void Load(string OptionFileName)
        {
            m_IniOptionFile.Load(OptionFileName);
        }

        public void Save()
        {
            m_IniOptionFile.Save();
        }

        public Point GetFormPos(Form frm)
        {
            string strPt = m_IniOptionFile.GetValue(frm.Name, "Position");
            Point pt;
            if (!string.IsNullOrEmpty(strPt) && strPt.Split(',').Length == 2)
            {
                pt = new Point(int.Parse(strPt.Split(',')[0]), 
                               int.Parse(strPt.Split(',')[1]));
            }
            else
            {
                pt = Point.Empty;
            }
            return pt;            
        }
    
        public void SetFormPos(Form frm)
        {
            m_IniOptionFile.SetValue(frm.Name, "Position", frm.Location.X.ToString()+","+frm.Location.Y.ToString());
        }
     
        public Size GetFormSize(Form frm)
        {
            string strSz = m_IniOptionFile.GetValue(frm.Name, "Size");
            Size sz;
            if (!string.IsNullOrEmpty(strSz) && strSz.Split(',').Length == 2)
            {
                sz = new Size(int.Parse(strSz.Split(',')[0]), 
                              int.Parse(strSz.Split(',')[1]));
            }
            else
            {
                sz = Size.Empty;
            }
            return sz;            
        }
     
        public void SetFormSize(Form frm)
        {
            m_IniOptionFile.SetValue(frm.Name, "Size", frm.Size.Width.ToString() + ","+frm.Size.Height.ToString());
        }

        public FormWindowState GetFormState(Form frm)
        {
            string strFState = m_IniOptionFile.GetValue(frm.Name, "State");
            FormWindowState fState = FormWindowState.Normal;
            try
            {                
                fState = (FormWindowState)Enum.Parse(typeof(FormWindowState), strFState, true);
            }
            catch (Exception)
            {
                Traces.LogAddDebug(TraceCat.SmartConfig, "Exception lors de la r�cup�ration de l'�tat de la form " + frm.Name);
            }  
            return fState;
        }
    
        public void SetFormState(Form frm)
        {
            m_IniOptionFile.SetValue(frm.Name, "State", frm.WindowState.ToString());
        }
    }
}

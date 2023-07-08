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
using System.Drawing;
using System.Data;
using System.Text;
using System.IO;
using System.Xml;
using System.Windows.Forms;

namespace CommonLib
{
    public partial class ScenarioPanel : UserControl
    {
        public event EventHandler SelectionChange;
        BTDoc m_Document;

        public ScenarioPanel()
        {
            InitializeComponent();
        }

        public bool ClicheGridEnabled
        {
            get
            {
                return m_dataGrid.Enabled;
            }
            set
            {
                m_dataGrid.Enabled = value;
            }
        }

        public BTDoc Document
        {
            get { return m_Document; }
            set { m_Document = value; }
        }

        public void SaveScenario(XmlDocument XmlDoc, XmlNode DocRootNode)
        {
            for (int i = 0; i < m_dataGrid.Rows.Count; i++)
            {
                string strTemp = (string)m_dataGrid.Rows[i].Cells[2].Value;
                XmlNode NodeFile = XmlDoc.CreateElement(XML_CF_TAG.File.ToString());
                XmlText nodeText = XmlDoc.CreateTextNode(PathTranslator.LinuxVsWindowsPathStore(strTemp));
                NodeFile.AppendChild(nodeText);
                DocRootNode.AppendChild(NodeFile);
            }
        }

        public void LoadScenario(XmlNode DocRootNode)
        {
            m_dataGrid.Rows.Clear();
            for (int i= 0; i< DocRootNode.ChildNodes.Count; i++)
            {
                XmlNode node = DocRootNode.ChildNodes[i];
                XmlText text = node.FirstChild as XmlText;
                string str = m_Document.PathTr.RelativePathToAbsolute(text.Value);
                AddFileToGrid(str);
            }
        }

        public void AddClicheFiles(string[] strFulleFileName)
        {
            for (int i = 0; i < strFulleFileName.Length; i++)
            {
                AddFileToGrid(strFulleFileName[i]);
            }
        }

        protected void AddFileToGrid(string strFile)
        {
            string[] TabValues = new string[3];
            TabValues[0] = string.Format("{0}", m_dataGrid.Rows.Count + 1);
            TabValues[1] = Path.GetFileName(strFile);
            TabValues[2] = m_Document.PathTr.AbsolutePathToRelative(strFile);
            m_dataGrid.Rows.Add(TabValues);
        }


        public void RemoveSelected()
        {
            if (m_dataGrid.SelectedRows.Count != 0)
            {
                m_dataGrid.Rows.Remove(m_dataGrid.SelectedRows[0]);
            }
        }

        public bool LoadNextLine(bool bPlayLoop)
        {
            if (m_dataGrid.SelectedRows.Count != 0 && m_dataGrid.Rows.Count>0)
            {
                int idx = m_dataGrid.SelectedRows[0].Index;
                string strTemp;
                if (idx < m_dataGrid.Rows.Count - 1)
                {
                    m_dataGrid.Rows[idx + 1].Selected = true;
                    strTemp = (string)m_dataGrid.Rows[idx + 1].Cells[2].Value;
                    strTemp = m_Document.PathTr.RelativePathToAbsolute(strTemp);
                }
                else 
                {
                    if (bPlayLoop)
                    {
                        m_dataGrid.Rows[0].Selected = true;
                        strTemp = (string)m_dataGrid.Rows[0].Cells[2].Value;
                        strTemp = m_Document.PathTr.RelativePathToAbsolute(strTemp);
                    }
                    else
                        return false;
                }
                VirtualDataForm parent = this.Parent.Parent.Parent as VirtualDataForm;
                if (parent != null)
                {
                    parent.LoadCliche(strTemp);
                    return true;
                }
                else
                    return false;
            }
            return false;
        }

        public void LoadPrevLine(bool bPlayLoop)
        {
            if (m_dataGrid.SelectedRows.Count != 0 && m_dataGrid.Rows.Count > 0)
            {
                int idx = m_dataGrid.SelectedRows[0].Index;
                string strTemp; 
                if (idx > 0)
                {
                    m_dataGrid.Rows[idx - 1].Selected = true;
                    strTemp = (string)m_dataGrid.Rows[idx - 1].Cells[2].Value;
                    strTemp = m_Document.PathTr.RelativePathToAbsolute(strTemp);
                }
                else
                {
                    if (bPlayLoop)
                    {
                        m_dataGrid.Rows[m_dataGrid.Rows.Count-1].Selected = true;
                        strTemp = (string)m_dataGrid.Rows[m_dataGrid.Rows.Count - 1].Cells[2].Value;
                        strTemp = m_Document.PathTr.RelativePathToAbsolute(strTemp);
                    }
                    else
                        return;
                }
                VirtualDataForm parent = this.Parent.Parent.Parent as VirtualDataForm;
                if (parent != null)
                    parent.LoadCliche(strTemp);
            }
        }

        public void LoadSelectedLine()
        {
            if (m_dataGrid.SelectedRows.Count != 0 && m_dataGrid.Rows.Count > 0)
            {
                int idx = m_dataGrid.SelectedRows[0].Index;
                string strTemp = (string)m_dataGrid.Rows[idx].Cells[2].Value;
                strTemp = m_Document.PathTr.RelativePathToAbsolute(strTemp);
                VirtualDataForm parent = this.Parent.Parent.Parent as VirtualDataForm;
                if (parent != null)
                    parent.LoadCliche(strTemp);
            }
        }

        private void m_dataGrid_SelectionChanged(object sender, EventArgs e)
        {
            if (SelectionChange != null)
                SelectionChange(sender, e);
        }

        public void MoveSelectedUp()
        {
            if (m_dataGrid.SelectedRows.Count != 0 && m_dataGrid.Rows.Count > 0)
            {
                int idx = m_dataGrid.SelectedRows[0].Index;
                if (idx == 0)
                    return;
                string str11 = (string)m_dataGrid.Rows[idx].Cells[2].Value;
                string str12 = (string)m_dataGrid.Rows[idx].Cells[1].Value;
                string str21 = (string)m_dataGrid.Rows[idx-1].Cells[2].Value;
                string str22 = (string)m_dataGrid.Rows[idx-1].Cells[1].Value;
                m_dataGrid.Rows[idx].Cells[2].Value = str21;
                m_dataGrid.Rows[idx].Cells[1].Value = str22;
                m_dataGrid.Rows[idx-1].Cells[2].Value = str11;
                m_dataGrid.Rows[idx-1].Cells[1].Value = str12;
                m_dataGrid.Rows[idx-1].Selected = true;
            }
        }

        public void MoveSelectedDown()
        {
            if (m_dataGrid.SelectedRows.Count != 0 && m_dataGrid.Rows.Count > 0)
            {
                int idx = m_dataGrid.SelectedRows[0].Index;
                if (idx == m_dataGrid.Rows.Count-1)
                    return;
                string str11 = (string)m_dataGrid.Rows[idx].Cells[2].Value;
                string str12 = (string)m_dataGrid.Rows[idx].Cells[1].Value;
                string str21 = (string)m_dataGrid.Rows[idx+1].Cells[2].Value;
                string str22 = (string)m_dataGrid.Rows[idx+1].Cells[1].Value;
                m_dataGrid.Rows[idx].Cells[2].Value = str21;
                m_dataGrid.Rows[idx].Cells[1].Value = str22;
                m_dataGrid.Rows[idx+1].Cells[2].Value = str11;
                m_dataGrid.Rows[idx+1].Cells[1].Value = str12;
                m_dataGrid.Rows[idx+1].Selected = true;
            }
        }

        private void m_dataGrid_DoubleClick(object sender, EventArgs e)
        {
            LoadSelectedLine();
        }

        private void m_dataGrid_CellMouseClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == m_dataGrid.Columns["ColButtonEdit"].DisplayIndex)
            {
                // todo, editeur de cliché
            }
        }
    }
}

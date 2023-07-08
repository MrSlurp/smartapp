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
using System.Windows.Forms;

namespace CommonLib
{
    public partial class StandardObjectPropPanel : UserControl, IObjectPropertyPanel
    {
        /// <summary>
        /// 
        /// </summary>
        BTDoc m_Document = null;
        /// <summary>
        /// 
        /// </summary>
        BaseObject m_baseObjectItem = null;
        /// <summary>
        /// 
        /// </summary>
        BaseGest m_baseGestItem = null;

        /// <summary>
        /// 
        /// </summary>
        public StandardObjectPropPanel()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        public Control Panel 
        {
            get { return this; }  
        }

        /// <summary>
        /// 
        /// </summary>
        public BaseObject ConfiguredItem
        {
            get { return m_baseObjectItem; }
            set { m_baseObjectItem = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IsObjectPropertiesValid 
        { 
            get { return true; } 
        }

        /// <summary>
        /// 
        /// </summary>
        public BTDoc Document 
        {
            get { return m_Document; }
            set { m_Document = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public BaseGest ConfiguredItemGest 
        {
            get { return m_baseGestItem; }
            set { m_baseGestItem = value; } 
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool ValidateProperties()
        {
            bool bRet = true;
            string strMessage = "";
            if (string.IsNullOrEmpty(this.m_textBoxSymbol.Text))
            {
                strMessage = Lang.LangSys.C("Symbol must not be empty");
                bRet = false;
            }
            BaseObject item = m_baseGestItem.GetFromSymbol(this.m_textBoxSymbol.Text);
            if (item != null && item != this.m_baseObjectItem)
            {
                strMessage = string.Format(Lang.LangSys.C("An object with symbol {0} already exist"), this.m_textBoxSymbol.Text);
                bRet = false;
            }
            if (!bRet)
            {
                MessageBox.Show(strMessage, Lang.LangSys.C("Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return bRet;
            }

            return bRet;
        }

        /// <summary>
        /// 
        /// </summary>
        public void ObjectToPanel()
        {
            if (m_baseObjectItem != null)
            {
                this.m_richTextDesc.Text = m_baseObjectItem.Description;
                this.m_textBoxSymbol.Text = m_baseObjectItem.Symbol;
            }
            else
            {
                this.m_richTextDesc.Text = string.Empty;
                this.m_textBoxSymbol.Text = string.Empty;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void PanelToObject()
        {
            m_baseObjectItem.Description = this.m_richTextDesc.Text;
            m_baseObjectItem.Symbol = this.m_textBoxSymbol.Text;
        }
    }
}

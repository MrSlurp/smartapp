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
            return bRet;
        }

        /// <summary>
        /// 
        /// </summary>
        public void ObjectToPanel()
        {
            this.m_richTextDesc.Text = m_baseObjectItem.Description;
            this.m_textBoxSymbol.Text = m_baseObjectItem.Symbol;
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

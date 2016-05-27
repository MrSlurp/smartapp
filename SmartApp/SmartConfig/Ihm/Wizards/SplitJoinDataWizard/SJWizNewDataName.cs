using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using CommonLib;

namespace SmartApp.Wizards
{
    public partial class SJWizNewDataName : UserControl, ISJWizForm
    {
        public SJWizNewDataName()
        {
            InitializeComponent();
        }

        SJDataWizardManager m_Manager;
        public SJDataWizardManager SJManager
        { get { return m_Manager; } set { m_Manager = value; } }

        public bool AllowGoNext { get { return true; } }

        public void HmiToData()
        {

        }

        public void DataToHmi()
        {
        }
    }
}

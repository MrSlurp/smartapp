using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SmartApp.Wizards
{
    public partial class SJWizSelectOperation : UserControl, ISJWizForm
    {

        public SJWizSelectOperation()
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
            switch (m_Manager.SplitJoinOp)
            {
                case SplitJoinOperation.SplitOneInTwo:
                    radioButton1.Checked = true;
                    break;
                case SplitJoinOperation.SplitOneInFour:
                    radioButton2.Checked = true;
                    break;
                case SplitJoinOperation.SplitOneInSixteen:
                    radioButton3.Checked = true;
                    break;
                case SplitJoinOperation.JoinTwoInOne:
                    radioButton4.Checked = true;
                    break;
                case SplitJoinOperation.JoinFourInOne:
                    radioButton5.Checked = true;
                    break;
                case SplitJoinOperation.JoinSixteenInOne:
                    radioButton6.Checked = true;
                    break;
            }
        }

        private void SelectedOperation_Changed(object sender, EventArgs e)
        {
            if (sender == radioButton1)
            {
                m_Manager.SplitJoinOp = SplitJoinOperation.SplitOneInTwo;
            }
            else if (sender == radioButton2)
            {
                m_Manager.SplitJoinOp = SplitJoinOperation.SplitOneInFour;
            }
            else if (sender == radioButton3)
            {
                m_Manager.SplitJoinOp = SplitJoinOperation.SplitOneInSixteen;
            }
            else if (sender == radioButton4)
            {
                m_Manager.SplitJoinOp = SplitJoinOperation.JoinTwoInOne;
            }
            else if (sender == radioButton5)
            {
                m_Manager.SplitJoinOp = SplitJoinOperation.JoinFourInOne;
            }
            else if (sender == radioButton6)
            {
                m_Manager.SplitJoinOp = SplitJoinOperation.JoinSixteenInOne;
            }
        }

    }
}

/*
    This file is part of SmartApp.

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

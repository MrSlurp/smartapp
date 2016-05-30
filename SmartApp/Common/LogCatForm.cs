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
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CommonLib;

namespace SmartApp
{
    public partial class LogCatForm : Form
    {
        public LogCatForm()
        {
            InitializeComponent();
            string[] ListNames = Enum.GetNames(typeof(TracesLevel));
            Array ListValues = Enum.GetValues(typeof(TracesLevel));
            CComboData[] ListLevels = new CComboData[ListNames.Length];
            for (int i = 0; i < ListLevels.Length; i++)
            {
                ListLevels[i] = new CComboData(ListNames[i], ListValues.GetValue(i));
            }
            m_cboLevel.ValueMember = "Object";
            m_cboLevel.DisplayMember = "DisplayedString";
            m_cboLevel.DataSource = ListLevels;
            m_cboLevel.SelectedIndex = 0;
        }

        public TracesLevel Level
        {
            get
            {
                return (TracesLevel)m_cboLevel.SelectedValue;
            }
            set
            {
                m_cboLevel.SelectedValue = value;
            }
        }

        public bool LogToFile
        {
            get
            {
                return m_chkLogToFile.Checked;
            }
            set
            {
                m_chkLogToFile.Checked = value;
            }
        }


        public TraceCat ActiveCats
        {
            get
            {
                TraceCat retCat = TraceCat.None;

                if (m_chkCatParser.Checked)
                    retCat |= TraceCat.Parser;
                if (m_chkCatLang.Checked)
                    retCat |= TraceCat.Lang;
                if (m_chkCatSmartCfg.Checked)
                    retCat |= TraceCat.SmartConfig;
                if (m_chkCatSmartCmd.Checked)
                    retCat |= TraceCat.SmartCommand;
                if (m_chkCatCommonLib.Checked)
                    retCat |= TraceCat.Communication;
                if (m_chkCatCommonLib.Checked)
                    retCat |= TraceCat.Communication;

                if (m_chkCatComm.Checked)
                    retCat |= TraceCat.Communication;
                if (m_chkCatCommonLib.Checked)
                    retCat |= TraceCat.CommonLib;

                if (m_chkCatSerialize.Checked)
                    retCat |= TraceCat.Serialization;
                if (m_chkCatPlugin.Checked)
                    retCat |= TraceCat.Plugin;
                if (m_chkCatOther.Checked)
                    retCat |= TraceCat.Others;
                if (m_chkCatDocument.Checked)
                    retCat |= TraceCat.Document;
                if (m_chkCatScriptEditor.Checked)
                    retCat |= TraceCat.ScriptEditor;
                if (m_chkCatPerf.Checked)
                    retCat |= TraceCat.PerfChrono;

                if (m_chkCatExec.Checked)
                    retCat |= TraceCat.Executer;
                if (m_chkCatExecFrame.Checked)
                    retCat |= TraceCat.ExecuteFrame;
                if (m_chkCatExecFunc.Checked)
                    retCat |= TraceCat.ExecuteFunc;
                if (m_chkCatExecLogger.Checked)
                    retCat |= TraceCat.ExecuteLogger;
                if (m_chkCatExecLogic.Checked)
                    retCat |= TraceCat.ExecuteLogic;
                if (m_chkCatExecMaths.Checked)
                    retCat |= TraceCat.ExecuteMath;
                if (m_chkCatExecScreen.Checked)
                    retCat |= TraceCat.ExecuteScreen;
                if (m_chkCatExecTimer.Checked)
                    retCat |= TraceCat.ExecuteTimer;

                return retCat;
            }
            set
            {
                m_chkCatParser.Checked = ((value & TraceCat.Parser) != TraceCat.None);
                m_chkCatLang.Checked = ((value & TraceCat.Lang) != TraceCat.None);
                m_chkCatSmartCfg.Checked   = ((value & TraceCat.SmartConfig) != TraceCat.None);
                m_chkCatSmartCmd.Checked   = ((value & TraceCat.SmartCommand) != TraceCat.None);
                m_chkCatCommonLib.Checked  = ((value & TraceCat.Communication) != TraceCat.None);
                m_chkCatCommonLib.Checked  = ((value & TraceCat.Communication) != TraceCat.None);
                m_chkCatComm.Checked       = ((value & TraceCat.Communication) != TraceCat.None);
                m_chkCatCommonLib.Checked  = ((value & TraceCat.CommonLib) != TraceCat.None);
                m_chkCatSerialize.Checked  = ((value & TraceCat.Serialization) != TraceCat.None);
                m_chkCatPlugin.Checked     = ((value & TraceCat.Plugin) != TraceCat.None);
                m_chkCatOther.Checked      = ((value & TraceCat.Others) != TraceCat.None);
                m_chkCatDocument.Checked   = ((value & TraceCat.Document) != TraceCat.None);
                m_chkCatScriptEditor.Checked=((value & TraceCat.ScriptEditor) != TraceCat.None);
                m_chkCatPerf.Checked       = ((value & TraceCat.PerfChrono) != TraceCat.None);

                m_chkCatExec.Checked       = ((value & TraceCat.Executer) != TraceCat.None);
                m_chkCatExecFrame.Checked  = ((value & TraceCat.ExecuteFrame) != TraceCat.None);
                m_chkCatExecFunc.Checked   = ((value & TraceCat.ExecuteFunc) != TraceCat.None);
                m_chkCatExecLogger.Checked = ((value & TraceCat.ExecuteLogger) != TraceCat.None);
                m_chkCatExecLogic.Checked  = ((value & TraceCat.ExecuteLogic) != TraceCat.None);
                m_chkCatExecMaths.Checked  = ((value & TraceCat.ExecuteMath) != TraceCat.None);
                m_chkCatExecScreen.Checked = ((value & TraceCat.ExecuteScreen) != TraceCat.None);
                m_chkCatExecTimer.Checked  = ((value & TraceCat.ExecuteTimer) != TraceCat.None);
            }
        }

        private void m_chkCatExec_CheckedChanged(object sender, EventArgs e)
        {
            if (m_chkCatExec.Checked)
                m_grExecCat.Enabled = true;
            else
            {
                m_grExecCat.Enabled = false;
                m_chkCatExecFrame.Checked = false;
                m_chkCatExecFunc.Checked = false;
                m_chkCatExecLogger.Checked = false;
                m_chkCatExecLogic.Checked = false;
                m_chkCatExecMaths.Checked = false;
                m_chkCatExecScreen.Checked = false;
                m_chkCatExecTimer.Checked = false;
            }
        }

        private void m_cboLevel_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SmartApp.Ihm;

namespace SmartApp.Scripts
{
    public partial class ScriptEditordialog : Form
    {
        private ScriptParser m_Parser = new ScriptParser();
        private BTDoc m_Document = null;


        #region attributs
        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public BTDoc Doc
        {
            get
            {
                return m_Document;
            }
            set
            {
                m_Document = value;
                m_Parser.Document = m_Document;
            }
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public string[] ScriptLines
        {
            get
            {
                return m_EditScript.Lines;
            }
            set
            {
                m_EditScript.Lines = value;
                m_AutoComplListBox.Hide();
            }
        }
        #endregion

        #region constructeur
        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public ScriptEditordialog()
        {
            InitializeComponent();
            m_AutoComplListBox.Sorted = true;
        }
        #endregion

        #region handlers d'events
        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public void OnScriptCheck(object sender, EventArgs e)
        {
            bool bRet = true;
            m_listViewError.Items.Clear();
            List<ScriptParserError> ListErr = new List<ScriptParserError>();
            if (!m_Parser.ParseScript(this.ScriptLines, ListErr))
            {
                bRet = false;
            }
            if (!bRet)
            {
                for (int i = 0; i < ListErr.Count; i++)
                {
                    ListViewItem lviError = m_listViewError.Items.Add(ListErr[i].m_ErrorType.ToString());
                    lviError.SubItems.Add((ListErr[i].m_line + 1).ToString());
                    lviError.SubItems.Add(ListErr[i].m_strMessage);
                }
            }
            else
                m_BtnOk.Enabled = true;

        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        private void m_BtnOK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            this.Close();
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        private void m_BtnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        private void OnEditScriptTextChanged(object sender, EventArgs e)
        {
            m_BtnOk.Enabled = false;
            int posCarret = m_EditScript.SelectionStart;
            RichTextBoxSelectionTypes oldval = m_EditScript.SelectionType;
            m_EditScript.Text = m_EditScript.Text.ToUpper();
            int CarretLine = m_EditScript.GetLineFromCharIndex(posCarret);
            int carretPosOnLine = posCarret - m_EditScript.GetFirstCharIndexOfCurrentLine();
            if (CarretLine > m_EditScript.Lines.Length - 1)
                return;
            string line = m_EditScript.Lines[CarretLine];
            int indexOfPoint = line.LastIndexOf('.');
            if (indexOfPoint < 0)
            {
                m_EditScript.SelectionStart = ((posCarret - line.Length) > 0)? (posCarret - line.Length) : 0;
                m_EditScript.SelectionLength = line.Length;
            }
            else
            {
                m_EditScript.SelectionStart = posCarret - (line.Length - (indexOfPoint + 1));
                m_EditScript.SelectionLength = line.Length - indexOfPoint;
            }
            string SelectedText = m_EditScript.SelectedText;
            m_EditScript.SelectionLength = 0;
            m_EditScript.SelectionStart = posCarret;

            StringCollection AutoCompleteList = m_Parser.GetAutoCompletStringListAtPos(line, carretPosOnLine);
            if (AutoCompleteList != null && AutoCompleteList.Count > 0)
            {
                Point PtCarret = m_EditScript.GetPositionFromCharIndex(posCarret);
                m_AutoComplListBox.DataSource = AutoCompleteList;
                PositioneAutoCompletionListBox();

                // on parcour la liste de façon décrémentiel de sorte a ce que le dernier 
                // item sur lequel on passe soit le premier dans la liste
                if (!string.IsNullOrEmpty(SelectedText))
                {
                    for (int i = m_AutoComplListBox.Items.Count - 1; i > 0; i--)
                    {
                        string strItem = m_AutoComplListBox.Items[i].ToString();
                        if (strItem.StartsWith(SelectedText))
                        {
                            m_AutoComplListBox.SelectedIndex = i;
                        }
                        // si on a le mot exacte on le selectionne et on quitte
                        if (m_AutoComplListBox.Items[i].ToString() == SelectedText)
                        {
                            m_AutoComplListBox.SelectedIndex = i;
                            break;
                        }
                    }
                }
                if (!m_AutoComplListBox.Visible)
                {
                    m_AutoComplListBox.Show();
                }
            }
            else
            {
                m_AutoComplListBox.Hide();
                m_AutoComplListBox.BackColor = SystemColors.Control;
            }
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        private void OnAutoComplListBoxClick(object sender, EventArgs e)
        {
            DoInsertAutoCompleteString();
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        private void OnEditScriptClick(object sender, EventArgs e)
        {
            if (GetChildAtPoint(PointToClient(MousePosition)) != m_AutoComplListBox)
            {
                m_AutoComplListBox.Hide();
                m_AutoComplListBox.BackColor = SystemColors.Control;
            }
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        private void ThisLeave(object sender, EventArgs e)
        {
            if (m_AutoComplListBox.Visible)
            {
                m_AutoComplListBox.Hide();
                m_AutoComplListBox.BackColor = SystemColors.Control;
            }
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        private void OnEditScriptKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down && m_AutoComplListBox.Visible)
            {
                m_AutoComplListBox.Focus();
                m_AutoComplListBox.BackColor = SystemColors.Window;
            }
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        private void OnAutoComplListBoxKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                DoInsertAutoCompleteString();
            }
        }
        #endregion

        #region gestion de l'auto complete
        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        private void PositioneAutoCompletionListBox()
        {
            int posCarret = m_EditScript.SelectionStart;
            int CarretLine = m_EditScript.GetLineFromCharIndex(posCarret);
            int carretPosOnLine = posCarret - m_EditScript.GetFirstCharIndexOfCurrentLine();
            string line = m_EditScript.Lines[CarretLine];
            int indexOfPoint = line.LastIndexOf('.');
            int charOffset = 0;
            if (indexOfPoint < 0)
            {
                charOffset = line.Length;
            }
            else
            {
                charOffset = line.Length - indexOfPoint;
            }

            Point PtCarret = m_EditScript.GetPositionFromCharIndex(posCarret - charOffset);
            //
            PtCarret = m_EditScript.PointToScreen(PtCarret);
            PtCarret = this.PointToClient(PtCarret);
            int itemHeight = m_AutoComplListBox.GetItemHeight(0);
            int ItemsHeight = itemHeight * (m_AutoComplListBox.Items.Count + 1);
            if (ItemsHeight > itemHeight * 10)
                ItemsHeight = itemHeight * 10;

            if (ItemsHeight < itemHeight * 5)
                ItemsHeight = itemHeight * 5;


            m_AutoComplListBox.Size = new System.Drawing.Size(350, ItemsHeight);
            PtCarret.Y += 15;
            m_AutoComplListBox.Location = PtCarret;
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        private void DoInsertAutoCompleteString()
        {
            string strClicked = (string)m_AutoComplListBox.SelectedItem;
            // selectionner le mot en cour, et remplacer par le texte de la list box
            int posCarret = m_EditScript.SelectionStart;
            int CarretLine = m_EditScript.GetLineFromCharIndex(posCarret);
            int carretPosOnLine = posCarret - m_EditScript.GetFirstCharIndexOfCurrentLine();
            string line = m_EditScript.Lines[CarretLine];
            int indexOfPoint = line.LastIndexOf('.');
            if (indexOfPoint < 0)
            {
                m_EditScript.SelectionStart = ((posCarret - line.Length) > 0) ? (posCarret - line.Length) : 0;
                m_EditScript.SelectionLength = line.Length;
            }
            else
            {
                m_EditScript.SelectionStart = posCarret - (line.Length - (indexOfPoint + 1));
                m_EditScript.SelectionLength = line.Length - indexOfPoint;
            }
            m_EditScript.SelectedText = strClicked;
            m_AutoComplListBox.Hide();
            m_AutoComplListBox.BackColor = SystemColors.Control;
            m_EditScript.Focus();
            m_EditScript.SelectionLength = 0;
            m_EditScript.SelectionStart = m_EditScript.Text.Length;
        }
        #endregion

        private void m_EditScript_Leave(object sender, EventArgs e)
        {
            if (m_AutoComplListBox.Visible)
            {
                if (!m_AutoComplListBox.Focused)
                {
                    m_AutoComplListBox.Hide();
                    m_AutoComplListBox.BackColor = SystemColors.Control;
                }
            }
        }
    }
}
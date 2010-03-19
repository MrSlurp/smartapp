using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CommonLib;

namespace CommonLib
{
    public partial class ScriptEditordialog : Form
    {
        private ScriptParser m_Parser = new ScriptParser();
        private BTDoc m_Document = null;
        bool m_bIsParameter = false;
        bool m_AutoBoxWillGetFocus = false;

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
#if LINUX
				for (int i = 0; i < value.Length; i++)
					value[i] += " ";
#endif
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
            Lang.LangSys.Initialize(this);
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
            m_Document.Modified = true;
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
            //            m_EditScript.SelectionStart = posCarret;
            //            int carretPosOnLine = posCarret - m_EditScript.GetFirstCharIndexOfCurrentLine();

            m_EditScript.SelectionStart = posCarret;
            int posFirstCharOfLine = m_EditScript.GetFirstCharIndexOfCurrentLine();
            int carretPosOnLine = posCarret - posFirstCharOfLine;

            if (CarretLine > m_EditScript.Lines.Length - 1)
                return;
            string line = "";
            if (m_EditScript.Lines.Length != 0)
                line = m_EditScript.Lines[CarretLine];
            List<int> listPointPos = new List<int>();
            int PosPoint = 0;
            while (PosPoint != -1 && line.Length > 0)
            {
                PosPoint = line.IndexOf(ParseExecGlobals.TOKEN_SEPARATOR, PosPoint + 1);
                if (PosPoint != -1)
                    listPointPos.Add(PosPoint);
            }
            bool bCarretIsAfterLastParenthese = false;
            if (line.LastIndexOf(')') != -1 && line.LastIndexOf(')') < carretPosOnLine)
                bCarretIsAfterLastParenthese = true;

            int finalCarretPos = m_EditScript.Text.Length;
            int CarretIsAfterPointAt = -1; // indique après quel point se situe le chariot
            int CarretIsBeforePointAt = int.MaxValue; // indique avant quel point se situe le chariot
            //les points sont classés par ordre d'apparition de gauche a droite
            for (int i = 0; i < listPointPos.Count; i++)
            {
                if (CarretIsAfterPointAt <= listPointPos[i] && carretPosOnLine > listPointPos[i])
                    CarretIsAfterPointAt = listPointPos[i];
                if (CarretIsBeforePointAt > listPointPos[i] && carretPosOnLine <= listPointPos[i])
                    CarretIsBeforePointAt = listPointPos[i];
            }
            // le curseur est entre deux point dont on a les coordonnées sur la ligne courante,
            // on selectionne entre ces deux points
            if (CarretIsAfterPointAt != -1 && CarretIsBeforePointAt != int.MaxValue)
            {
                Traces.LogAddDebug(TraceCat.ScriptEditor, "TextChange", "Curseur entre deux points");
                m_EditScript.SelectionStart = posFirstCharOfLine + (CarretIsAfterPointAt + 1);
                m_EditScript.SelectionLength = CarretIsBeforePointAt - (CarretIsAfterPointAt + 1);
            }
            //Le chariot est après le dernier point
            else if (CarretIsAfterPointAt != -1 && CarretIsBeforePointAt == int.MaxValue)
            {
                Traces.LogAddDebug(TraceCat.ScriptEditor, "TextChange", "Curseur après dernier point");
                m_EditScript.SelectionStart = posFirstCharOfLine + (CarretIsAfterPointAt + 1); // +1 car après le point
                m_EditScript.SelectionLength = line.Length - (CarretIsAfterPointAt + 1);// dela fin jusqu'au point
            }
            // le chariot se trouve avant le premier point
            else if (CarretIsAfterPointAt == -1 && CarretIsBeforePointAt != int.MaxValue)
            {
                Traces.LogAddDebug(TraceCat.ScriptEditor, "TextChange", "Curseur avant premier point");
                m_EditScript.SelectionStart = posFirstCharOfLine;
                m_EditScript.SelectionLength = CarretIsBeforePointAt;
            }
            // pas de points
            else if (CarretIsAfterPointAt == -1 && CarretIsBeforePointAt == int.MaxValue)
            {
                Traces.LogAddDebug(TraceCat.ScriptEditor, "TextChange", "pas de points sur la ligne");
                m_EditScript.SelectionStart = posFirstCharOfLine;
                m_EditScript.SelectionLength = line.Length;
            }
            else
                System.Diagnostics.Debug.Assert(false);

            string SelectedText = m_EditScript.SelectedText;
            m_EditScript.SelectionLength = 0;
            m_EditScript.SelectionStart = posCarret;


            StringCollection AutoCompleteList = m_Parser.GetAutoCompletStringListAtPos(line, carretPosOnLine, out m_bIsParameter);
            if (AutoCompleteList != null && AutoCompleteList.Count > 0 && !bCarretIsAfterLastParenthese)
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
                            m_AutoComplListBox.Hide();
                            m_AutoComplListBox.BackColor = SystemColors.Control;
                            return;
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
            m_AutoBoxWillGetFocus = true;
            m_AutoComplListBox.Focus();
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
            if (m_AutoComplListBox.Visible && !m_AutoComplListBox.Focused)
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
            if ((e.KeyCode == Keys.Down || e.KeyCode == Keys.Up) && m_AutoComplListBox.Visible)
            {
                m_AutoComplListBox.BackColor = SystemColors.Window;
                m_AutoComplListBox.Visible = true;
                m_AutoBoxWillGetFocus = true;
                m_AutoComplListBox.Focus();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Enter && m_AutoComplListBox.Visible)
            {
                DoInsertAutoCompleteString();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Escape && m_AutoComplListBox.Visible)
            {
                m_AutoComplListBox.Visible = false;
            }
            else
            {
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
            else if (e.KeyCode == Keys.Escape && m_AutoComplListBox.Visible)
            {
                m_AutoComplListBox.Visible = false;
                m_EditScript.Focus();
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
            int indexOfPoint = line.LastIndexOf(ParseExecGlobals.TOKEN_SEPARATOR);
            int charOffset = 0;
            if (indexOfPoint < 0)
            {
#if LINUX
                charOffset = line.Length -1;
#else
                charOffset = line.Length;
#endif
            }
            else
            {
                charOffset = line.Length - indexOfPoint;
            }

            int FinalCharIndex = ((posCarret - charOffset) >= 0) ? (posCarret - charOffset) : 0;
            Point PtCarret = m_EditScript.GetPositionFromCharIndex(FinalCharIndex);
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
            m_EditScript.SelectionStart = posCarret;
            int posFirstCharOfLine = m_EditScript.GetFirstCharIndexOfCurrentLine();
            int carretPosOnLine = posCarret - posFirstCharOfLine;
            string line = "";
            if (m_EditScript.Lines.Length != 0)
                line = m_EditScript.Lines[CarretLine];

            List<int> listSepratorPos = new List<int>();
            int PosSep = 0;
            while (PosSep != -1 && line.Length > 0)
            {
                PosSep = line.IndexOf(ParseExecGlobals.TOKEN_SEPARATOR, PosSep + 1);
                if (PosSep != -1)
                {
                    listSepratorPos.Add(PosSep);
                    continue;
                }
            }
            PosSep = 0;
            while (PosSep != -1 && line.Length > 0)
            {
                PosSep = line.IndexOf('(', PosSep + 1);
                if (PosSep != -1)
                {
                    listSepratorPos.Add(PosSep);
                    continue;
                }
            }
            PosSep = 0;
            while (PosSep != -1 && line.Length > 0)
            {
                PosSep = line.IndexOf(',', PosSep + 1);
                if (PosSep != -1)
                {
                    listSepratorPos.Add(PosSep);
                    continue;
                }
            }
            int finalCarretPos = m_EditScript.Text.Length;
            int CarretIsAfterSepAt = -1; // indique après quel point se situe le chariot
            int CarretIsBeforeSepAt = int.MaxValue; // indique avant quel point se situe le chariot

            for (int i = 0; i < listSepratorPos.Count; i++)
            {
                if (CarretIsAfterSepAt < listSepratorPos[i] && carretPosOnLine > listSepratorPos[i])
                    CarretIsAfterSepAt = listSepratorPos[i];
                if (CarretIsBeforeSepAt > listSepratorPos[i] && carretPosOnLine < listSepratorPos[i])
                    CarretIsBeforeSepAt = listSepratorPos[i];
            }
            // le curseur est entre deux separateur dont on a les coordonnées sur la ligne courante,
            // on selectionne entre ces deux separateur
            if (CarretIsAfterSepAt != -1 && CarretIsBeforeSepAt != int.MaxValue)
            {
                Traces.LogAddDebug(TraceCat.ScriptEditor, "DoInsert", "entre deux points");
                m_EditScript.SelectionStart = posFirstCharOfLine + (CarretIsAfterSepAt + 1);
                m_EditScript.SelectionLength = CarretIsBeforeSepAt - (CarretIsAfterSepAt + 1);
            }
            //Le chariot est après le dernier separateur
            else if (CarretIsAfterSepAt != -1 && CarretIsBeforeSepAt == int.MaxValue)
            {
                Traces.LogAddDebug(TraceCat.ScriptEditor, "DoInsert", "après dernier point");
                m_EditScript.SelectionStart = posFirstCharOfLine + (CarretIsAfterSepAt + 1); // +1 car après le separateur
                m_EditScript.SelectionLength = line.Length - (CarretIsAfterSepAt + 1);// dela fin jusqu'au separateur
            }
            // le chariot se trouve avant le premier separateur
            else if (CarretIsAfterSepAt == -1 && CarretIsBeforeSepAt != int.MaxValue)
            {
                Traces.LogAddDebug(TraceCat.ScriptEditor, "DoInsert", "avant premier separateur");
                m_EditScript.SelectionStart = posFirstCharOfLine;
                m_EditScript.SelectionLength = CarretIsBeforeSepAt;
            }
            // pas de separateur
            else if (CarretIsAfterSepAt == -1 && CarretIsBeforeSepAt == int.MaxValue)
            {
                Traces.LogAddDebug(TraceCat.ScriptEditor, "DoInsert", "pas de separateur sur la ligne");
                m_EditScript.SelectionStart = posFirstCharOfLine;
                m_EditScript.SelectionLength = line.Length;
            }
            else
                System.Diagnostics.Debug.Assert(false);
            m_EditScript.SelectedText = strClicked;
            m_AutoComplListBox.Hide();
            m_AutoComplListBox.BackColor = SystemColors.Control;
            m_EditScript.Focus();
            m_EditScript.SelectionLength = 0;
        }
        #endregion

        private void m_EditScript_Leave(object sender, EventArgs e)
        {
            if (m_AutoComplListBox.Visible)
            {
                if (!m_AutoComplListBox.Focused && !m_AutoBoxWillGetFocus)
                {
                    m_AutoComplListBox.Hide();
                    m_AutoComplListBox.BackColor = SystemColors.Control;
                }
                if (m_AutoBoxWillGetFocus)
                    m_AutoBoxWillGetFocus = false;
            }
        }
    }
}
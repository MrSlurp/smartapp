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
                PosPoint = line.IndexOf('.', PosPoint + 1);
                if (PosPoint != -1)
                    listPointPos.Add(PosPoint);
            }
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
                Console.WriteLine("entre deux points");
                m_EditScript.SelectionStart = posFirstCharOfLine + (CarretIsAfterPointAt + 1);
                m_EditScript.SelectionLength = CarretIsBeforePointAt - (CarretIsAfterPointAt + 1);
            }
            //Le chariot est après le dernier point
            else if (CarretIsAfterPointAt != -1 && CarretIsBeforePointAt == int.MaxValue)
            {
                Console.WriteLine("après dernier point");
                m_EditScript.SelectionStart = posFirstCharOfLine + (CarretIsAfterPointAt + 1); // +1 car après le point
                m_EditScript.SelectionLength = line.Length - (CarretIsAfterPointAt + 1);// dela fin jusqu'au point
            }
            // le chariot se trouve avant le premier point
            else if (CarretIsAfterPointAt == -1 && CarretIsBeforePointAt != int.MaxValue)
            {
                Console.WriteLine("avant premier point");
                m_EditScript.SelectionStart = posFirstCharOfLine;
                m_EditScript.SelectionLength = CarretIsBeforePointAt;
            }
            // pas de points
            else if (CarretIsAfterPointAt == -1 && CarretIsBeforePointAt == int.MaxValue)
            {
                Console.WriteLine("pas de points sur la ligne");
                m_EditScript.SelectionStart = posFirstCharOfLine;
                m_EditScript.SelectionLength = line.Length;
            }
            else
                System.Diagnostics.Debug.Assert(false);

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
            if ((e.KeyCode == Keys.Down || e.KeyCode == Keys.Up) && m_AutoComplListBox.Visible)
            {
                m_AutoComplListBox.Focus();
                m_AutoComplListBox.BackColor = SystemColors.Window;
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
            m_EditScript.SelectionStart = posCarret;
            int posFirstCharOfLine = m_EditScript.GetFirstCharIndexOfCurrentLine();
            int carretPosOnLine = posCarret - posFirstCharOfLine;
            string line = "";
            if (m_EditScript.Lines.Length != 0)
                line = m_EditScript.Lines[CarretLine];
            List<int> listPointPos = new List<int>();
            int PosPoint = 0;
            while (PosPoint != -1 && line.Length > 0)
            {
                PosPoint = line.IndexOf('.', PosPoint + 1);
                if (PosPoint != -1)
                    listPointPos.Add(PosPoint);
            }
            int finalCarretPos = m_EditScript.Text.Length;
            int CarretIsAfterPointAt = -1; // indique après quel point se situe le chariot
            int CarretIsBeforePointAt = int.MaxValue; // indique avant quel point se situe le chariot
            //les points sont classés par ordre d'apparition de gauche a droite
            for (int i = 0; i < listPointPos.Count; i++)
            {
                if (CarretIsAfterPointAt < listPointPos[i] && carretPosOnLine > listPointPos[i])
                    CarretIsAfterPointAt = listPointPos[i];
                if (CarretIsBeforePointAt > listPointPos[i] && carretPosOnLine < listPointPos[i])
                    CarretIsBeforePointAt = listPointPos[i];
            }
            // le curseur est entre deux point dont on a les coordonnées sur la ligne courante,
            // on selectionne entre ces deux points
            if (CarretIsAfterPointAt != -1 && CarretIsBeforePointAt != int.MaxValue)
            {
                Console.WriteLine("entre deux points");
                m_EditScript.SelectionStart = posFirstCharOfLine + (CarretIsAfterPointAt+1);
                m_EditScript.SelectionLength = CarretIsBeforePointAt - (CarretIsAfterPointAt+1);
            }
            //Le chariot est après le dernier point
            else if (CarretIsAfterPointAt != -1 && CarretIsBeforePointAt == int.MaxValue)
            {
                Console.WriteLine("après dernier point");
                m_EditScript.SelectionStart = posFirstCharOfLine + (CarretIsAfterPointAt+1); // +1 car après le point
                m_EditScript.SelectionLength = line.Length - (CarretIsAfterPointAt+1);// dela fin jusqu'au point
            }
            // le chariot se trouve avant le premier point
            else if (CarretIsAfterPointAt == -1 && CarretIsBeforePointAt != int.MaxValue)
            {
                Console.WriteLine("avant premier point");
                m_EditScript.SelectionStart = posFirstCharOfLine;
                m_EditScript.SelectionLength = CarretIsBeforePointAt;
            }
            // pas de points
            else if (CarretIsAfterPointAt == -1 && CarretIsBeforePointAt == int.MaxValue)
            {
                Console.WriteLine("pas de points sur la ligne");
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
            //m_EditScript.SelectionStart = finalCarretPos;
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
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
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;
using System.Text;

namespace CommonLib
{
    //*****************************************************************************************************
    // Description: classe héritant du text box et n'autorisant que la saisie de lettre majuscules, de chiffres
    // et du caractères underscore 
    // Return: /
    //*****************************************************************************************************
    public partial class SymbolTextBox : TextBox
    {
        // on met aussi les caractères en minuscule car la text box est en UpperCase
        // les caractères autorisé sont les lettre, les chiffres et le underscore
        public const string AthorizedChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ_0123456789- ";

        //*****************************************************************************************************
        // Description: constructeur de la classe, met automatiquement le texte box en UpperCase
        // Return: /
        //*****************************************************************************************************
        public SymbolTextBox()
        {
            InitializeComponent();
            // les symbol sont toujours en majuscules
            this.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
        }

        //*****************************************************************************************************
        // Description: gère la saisie des caractères autorisés
        // Return: /
        //*****************************************************************************************************
        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            char CurChar = e.KeyChar;
            string strCurChar = CurChar.ToString();

            if (!AthorizedChars.Contains(strCurChar))
            {
                // Touches de suppression
                if (!(CurChar == 46 || CurChar == 8) && Form.ModifierKeys != Keys.Control)
                    e.Handled = true;
            }

            base.OnKeyPress(e);
        }

        protected override void OnTextChanged(EventArgs e)
        {
            
            string finalString = this.Text;
            foreach (char c in this.Text)
            {
                if (c == ' ')
                    finalString = finalString.Replace(new string(c, 1), "_");
                else if (!AthorizedChars.Contains(new string(c, 1)))
                {
                    finalString = finalString.Replace(new string(c, 1), "");
                }
            }
            base.OnTextChanged(e);
            if (finalString != this.Text)
            {
                this.Text = finalString;
                this.SelectionStart = this.Text.Length;
            }
        }
    }
}

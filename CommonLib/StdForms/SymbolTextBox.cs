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
        public const string AthorizedChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ_0123456789-";

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
                if (!(CurChar == 46 || CurChar == 8))
                    e.Handled = true;
            }

            base.OnKeyPress(e);
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.IO;
using System.Security.Cryptography;
using System.Windows.Forms;
using CommonLib;

namespace PasswordControler
{
    internal partial class PasswordControlerProperties : BaseControlPropertiesPanel, ISpecificPanel
    {
        /// <summary>
        /// Constructeur de la classe
        /// </summary>
        public PasswordControlerProperties()
        {
            DllEntryClass.LangSys.Initialize(this);
            InitializeComponent();
        }

        #region validation des données
        public void ObjectToPanel()
        {
            edtPassword1.Text = string.Empty;
            edtPassword2.Text = string.Empty;
            if (m_Control != null)
            {
                this.Enabled = true;
                if (string.IsNullOrEmpty(((DllPasswordControlerProp)m_Control.SpecificProp).PasswordHash))
                    lblPasswdExist.Visible = false;
                else
                    lblPasswdExist.Visible = true;
            }
        }

        public void PanelToObject()
        {

        }

        #endregion

        public static string HashWithMD5(string stringToHash)
        {
            MD5 md5HashAlgo = MD5.Create();
            // Place le texte à hacher dans un tableau d'octets 
            byte[] byteArrayToHash = Encoding.UTF8.GetBytes(stringToHash);

            // Hash le texte et place le résulat dans un tableau d'octets 
            byte[] hashResult = md5HashAlgo.ComputeHash(byteArrayToHash);

            StringBuilder result = new StringBuilder();

            for (int i = 0; i < hashResult.Length; i++)
            {
                // Affiche le Hash en hexadecimal 
                result.Append(hashResult[i].ToString("X2"));
            }
            return result.ToString();
        }

        private void btnValidate_Click(object sender, EventArgs e)
        {
            bool bDataPropChange = false;

            // testez ici si les paramètres ont changé en les comparant avec ceux contenu dans les propriété
            // spécifiques du BTControl
            // si c'est le cas, assignez bDataPropChange à true;
            string md5 = string.Empty;
            if ((!string.IsNullOrEmpty(edtPassword2.Text) && !string.IsNullOrEmpty(edtPassword1.Text)) &&
                edtPassword1.Text == edtPassword2.Text)
            {
                md5 = HashWithMD5(edtPassword1.Text);
                if (md5 != ((DllPasswordControlerProp)m_Control.SpecificProp).PasswordHash)
                    bDataPropChange = true;
            }
            else if ((!string.IsNullOrEmpty(edtPassword2.Text) && !string.IsNullOrEmpty(edtPassword1.Text)) &&
                edtPassword1.Text != edtPassword2.Text)
            {
                string strMessage = DllEntryClass.LangSys.C("The two password are different");
                MessageBox.Show(strMessage, DllEntryClass.LangSys.C("Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (bDataPropChange)
            {
                Document.Modified = true;
                ((DllPasswordControlerProp)m_Control.SpecificProp).PasswordHash = md5;
                lblPasswdExist.Visible = true;
            }
        }

    }
}

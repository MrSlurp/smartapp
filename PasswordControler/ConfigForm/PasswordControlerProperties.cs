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
    internal partial class PasswordControlerProperties : UserControl, ISpecificPanel
    {
        // controle dont on édite les propriété
        BTControl m_Control = null;
        // document courant
        private BTDoc m_Document = null;

        #region events
        public event ControlPropertiesChange ControlPropertiesChanged;
        #endregion

        /// <summary>
        /// Constructeur de la classe
        /// </summary>
        public PasswordControlerProperties()
        {
            DllEntryClass.LangSys.Initialize(this);
            InitializeComponent();
        }

        /// <summary>
        /// Accesseur du control
        /// </summary>
        public BTControl BTControl
        {
            get
            {
                return m_Control;
            }
            set
            {
                if (value != null && value.SpecificProp.GetType() == typeof(DllPasswordControlerProp))
                    m_Control = value;
                else
                    m_Control = null;
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
                else
                {
                    this.Enabled = false;
                    // mettez ici les valeur par défaut pour le panel de propriété spécifiques
                }
            }
        }

        /// <summary>
        /// Accesseur du document
        /// </summary>
        public BTDoc Doc
        {
            get
            {
                return m_Document;
            }
            set
            {
                m_Document = value;
            }
        }

        #region validation des données
        /// <summary>
        /// Accesseur de validité des propriétés
        /// renvoie true si les propriété sont valides, sinon false
        /// </summary>
        public bool IsDataValuesValid
        {
            get
            {
                if (this.BTControl == null)
                    return true;

                return true;
            }
        }

        /// <summary>
        /// validitation des propriétés
        /// </summary>
        /// <returns>true si les propriété sont valides, sinon false</returns>
        public bool ValidateValues()
        {
            if (this.BTControl == null)
                return true;

            return true;
        }
        #endregion

        public string HashWithMD5(string stringToHash)
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
                Doc.Modified = true;
                ((DllPasswordControlerProp)m_Control.SpecificProp).PasswordHash = md5;
                m_Control.IControl.Refresh();
                lblPasswdExist.Visible = true;
            }
            if (bDataPropChange && ControlPropertiesChanged != null)
                ControlPropertiesChanged(m_Control);
        }
    }
}

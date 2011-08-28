using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using CommonLib;

namespace PasswordControler
{
    /// <summary>
    /// classe héritant de UserControl
    /// représente l'objet graphique affiché dans la supervision
    /// on peux en faire a peut près ce qu'on veux :
    /// - du dessin
    /// - une aggregation de plusieurs controls standards, 
    /// - les deux, etc.
    /// </summary>
    internal partial class PasswordControlerDispCtrl : UserControl
    {
        // ajouter ici les données membres du control affiché
        protected PasswordControlerCmdControl m_SourceCtrl = null;
        Timer m_TimerPassInvalid = new Timer();

        public PasswordControlerCmdControl SourceCtrl
        {
            get { return m_SourceCtrl; }
            set { m_SourceCtrl = value; }
        }


        public PasswordControlerDispCtrl()
        {
            InitializeComponent();
            txtPasswd.Text = string.Empty;
            m_TimerPassInvalid.Tick += new EventHandler(m_TimerPassInvalid_Tick);
            m_TimerPassInvalid.Interval = 2000;
        }

        void m_TimerPassInvalid_Tick(object sender, EventArgs e)
        {
            label1.Visible = false;
            m_TimerPassInvalid.Enabled = false;
        }

        void ShowInvalidPassword()
        {
            if (!m_TimerPassInvalid.Enabled)
            {
                m_TimerPassInvalid.Start();
                label1.Visible = true;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            // mettez ici le code de dessin du control
            base.OnPaint(e);
        }

        private void btnUnlock_Click(object sender, EventArgs e)
        {
            if (PasswordControlerProperties.HashWithMD5(txtPasswd.Text) ==
                ((DllPasswordControlerProp)m_SourceCtrl.SpecificProp).PasswordHash)
            {
                m_SourceCtrl.SetAssociateDataValue(1);
            }
            else
                ShowInvalidPassword();
        }

        private void btnLock_Click(object sender, EventArgs e)
        {
            m_SourceCtrl.SetAssociateDataValue(0);
            txtPasswd.Text = string.Empty;
        }
    }
}

/*
    This file is part of SmartApp.
    Copyright (C) 2007-2016  Pascal Bigot

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
    internal partial class PasswordControlerDispCtrl : UserControl, ILangReloadable
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
            LoadNonStandardLang();
            txtPasswd.Text = string.Empty;
            m_TimerPassInvalid.Tick += new EventHandler(m_TimerPassInvalid_Tick);
            m_TimerPassInvalid.Interval = 2000;
        }

        public void LoadNonStandardLang()
        {
            btnUnlock.Text = DllEntryClass.LangSys.C("Unlock");
            btnLock.Text = DllEntryClass.LangSys.C("Lock");
            label1.Text = DllEntryClass.LangSys.C("Invalid password");
            this.Refresh();
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

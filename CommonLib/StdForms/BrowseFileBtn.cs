﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace CommonLib
{
    /// <summary>
    /// bouton de parcours affichant un menu contextuel proposant plusieurs choix de dossiers.
    /// cette classe s'utilise conjointement avec La classe CentralizedFileDlg
    /// </summary>
    public class BrowseFileBtn : Button
    {
        public delegate void BrowseFromEvent(object sender, EventArgs e, BrowseFrom bf);

        public event BrowseFromEvent OnBrowseFrom;

        public enum BrowseFrom
        {
            Project,
            App,
            Last,
        };

        public BrowseFileBtn()
        {
            this.ContextMenu = new ContextMenu();
            if (Lang.LangSys != null)
            {
                this.ContextMenu.MenuItems.Add(new MenuItem(Lang.LangSys.C("Browse from project directory"), InternalBrowseFromProjectClick));
                this.ContextMenu.MenuItems.Add(new MenuItem(Lang.LangSys.C("Browse from application directory"), InternalBrowseFromAppClick));
                this.ContextMenu.MenuItems.Add(new MenuItem(Lang.LangSys.C("Browse from last used directory"), InternalBrowseFromLastClick));
            }
        }
        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            this.ContextMenu.Show(this, new Point(this.Width/2, this.Height/2));
        }

        private void InternalBrowseFromProjectClick(object sender, EventArgs e)
        {
            if (OnBrowseFrom != null)
            {
                OnBrowseFrom(this, e, BrowseFrom.Project);
            }
        }

        private void InternalBrowseFromAppClick(object sender, EventArgs e)
        {
            if (OnBrowseFrom != null)
            {
                OnBrowseFrom(this, e, BrowseFrom.App);
            }
        }

        private void InternalBrowseFromLastClick(object sender, EventArgs e)
        {
            if (OnBrowseFrom != null)
            {
                OnBrowseFrom(this, e, BrowseFrom.Last);
            }
        }
    }
}

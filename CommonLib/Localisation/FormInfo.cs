using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace CommonLib
{
    /// <summary>
    /// Contient les données d'une IHM (System.Windows.Form)
    /// </summary>
    internal class FormInfo
    {
        readonly string FilePath = "";

        private Form Me;

        public readonly string FormName = "";

        private List<object> Controls = new List<object>();

        private List<string> ControlsText = new List<string>();

        private Lang m_LangSys;
        /// <summary>
        /// Constructeur static
        /// </summary>
        /// <param name="frm">Feuille concernée</param>
        /// <returns>Un objet FormInfo initialisé</returns>
        public static FormInfo CreateFormInfo(Lang LangSys, Form frm)
        {
            return new FormInfo(LangSys, frm);
        }

        /// <summary>
        /// Constructeur PRIVé de FormInfo
        /// </summary>
        /// <param name="frm">Objet System.Windows.Form</param>
        private FormInfo(Lang LangSys, Form frm)
        {
            // stocke le nom de la feuille
            m_LangSys = LangSys;
            FormName = frm.Name;
            Me = frm;
            // stocke de nom du fichier de langue concerné
            // il servira de clé d'indexation
            FilePath = m_LangSys.CreateFilePath(frm);
            // attache l'intercepteur d'évènements
            frm.Load += Form_Load;
            // charge le fichier de langue adapté
            m_LangSys.LoadLangage(FilePath);
            // Vérification des textes demandés par Lang.C(****)
            if (m_LangSys.CreateOnMissingItem)
            {
                m_LangSys.GetCode_LangC(frm);
            }
        }

        /// <summary>
        /// Intercepteur des evènements Load de la feuille concernée
        /// </summary>
        /// <param name="sender">Object System.Windows.Form</param>
        public void Form_Load(object sender, EventArgs e)
        {
            if (sender != null)
            {
                Controls.Clear();
                ControlsText.Clear();
                Form frm = (Form)sender;
                Update_Title(frm);
                Update_Controls(frm.Controls);
                if (frm.ContextMenu != null)
                    Update_Controls(frm.ContextMenu.MenuItems);
            }
            else
            {
                m_LangSys.LoadLangage(FilePath);
                foreach (object var in Controls)
                {
                    if (var is Control)
                    {
                        ((Control)var).Text = m_LangSys.C(FilePath, ControlsText[Controls.IndexOf(var)]);
                    }
#if !PocketPC
                    else if (var is ToolStripItem)
                    {
                        ((ToolStripItem)var).Text = m_LangSys.C(FilePath, ControlsText[Controls.IndexOf(var)]);
                    }
#endif
                    else if (var is MenuItem)
                    {
                        ((MenuItem)var).Text = m_LangSys.C(FilePath, ControlsText[Controls.IndexOf(var)]);
                    }
                }


            }
        }

        /// <summary>
        /// Parcourt tous les controls d'une Form et localise les Text
        /// Attention fonction récursive ! ! !
        /// </summary>
        private void Update_Controls(Control.ControlCollection ctrl)
        {

            // il faut rechercher les controls à la mano
            for (int index = 0; index < ctrl.Count; index++)
            {
#if !PocketPC
                if (
                       !(ctrl[index] is RichTextBox)
                    && !(ctrl[index] is TextBox)
                    && !(ctrl[index] is StatusStrip)
                    && !(ctrl[index] is NumericUpDown)
                    && !(ctrl[index] is ComboBox)
                    && !(ctrl[index] is ListView)
                    && (ctrl[index].Text != ""))
#else
                    if (!(ctrl[index] is ListView)
                        && (ctrl[index].Text != ""))
#endif
                {
                    Controls.Add(ctrl[index]);
                    ControlsText.Add(ctrl[index].Text);
                    ctrl[index].Text = m_LangSys.C(FilePath, ctrl[index].Text);
                }
                if (ctrl[index] is ListView)
                {
                    Update_Controls((((ListView)ctrl[index])));
                }
#if !PocketPC
                else if (ctrl[index] is ToolStrip)
                {
                    Update_Controls((((ToolStrip)ctrl[index]).Items));
                }
#endif
                else
                {
                    Update_Controls(ctrl[index].Controls);
                }

            }
        }

        /// <summary>
        /// Parcourt tous les controls d'une ToolStripItemCollection et localise les Text
        /// Attention fonction récursive ! ! !
        /// </summary>
        /// <param name="ctrl"></param>
#if !PocketPC
        private void Update_Controls(ToolStripItemCollection ctrl)
        {

            for (int index = 0; index < ctrl.Count; index++)
            {
                if (ctrl[index].Text != "" && !(ctrl[index] is ToolStripStatusLabel))
                {
                    Controls.Add(ctrl[index]);
                    ControlsText.Add(ctrl[index].Text);
                    ctrl[index].Text = m_LangSys.C(FilePath, ctrl[index].Text);
                }
                if (ctrl[index] is ToolStripDropDownItem)
                {
                    Update_Controls(((ToolStripDropDownItem)ctrl[index]).DropDownItems);
                }

            }

        }
#endif

        /// <summary>
        /// Parcourt tous les header d'une ListView et localise les Text
        /// Attention fonction récursive ! ! !
        /// </summary>
        /// <param name="ctrl"></param>
        private void Update_Controls(ListView ctrl)
        {
            for (int index = 0; index < ctrl.Columns.Count; index++)
            {
                if (ctrl.Columns[index].Text != "")
                {
                    Controls.Add(ctrl.Columns[index]);
                    ControlsText.Add(ctrl.Columns[index].Text);
                    ctrl.Columns[index].Text = m_LangSys.C(FilePath, ctrl.Columns[index].Text);

                }
            }
        }

        /// <summary>
        /// Parcour tout les éléments du menu et localise le texte
        /// </summary>
        /// <param name="Collection">Collection d'item de menu</param>
        private void Update_Controls(ContextMenu.MenuItemCollection Collection)
        {
            for (int index = 0; index < Collection.Count; index++)
            {
                if (Collection[index].Text != "")
                {
                    Controls.Add(Collection[index]);
                    ControlsText.Add(Collection[index].Text);
                    Collection[index].Text = m_LangSys.C(FilePath, Collection[index].Text);
                }
            }
        }
        private void Update_Title(Form frm)
        {
            frm.Text = m_LangSys.C(FilePath, frm.Text);
        }
    }
}

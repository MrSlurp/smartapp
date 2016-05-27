using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace CommonLib
{
    /// <summary>
    /// Contient les données d'une IHM (System.Windows.Form)
    /// </summary>
    internal class BaseInfo
    {
        protected string FilePath = "";

        // cette liste stock les controls de la form
        protected List<object> Controls = new List<object>();

        // cette liste stocke le texte de développement du controle
        protected List<string> ControlsText = new List<string>();
        
        protected Lang m_LangSys;
        

        /// <summary>
        /// Constructeur PRIVé de FormInfo
        /// </summary>
        /// <param name="frm">Objet System.Windows.Form</param>
        protected BaseInfo(Lang LangSys)
        {
            // stocke le nom de la feuille
            m_LangSys = LangSys;
            // stocke de nom du fichier de langue concerné
            // il servira de clé d'indexation
            FilePath = Lang.CreateFilePath();
            // charge le fichier de langue adapté
            m_LangSys.LoadLangage(FilePath);
        }

        /// <summary>
        /// Parcourt tous les controls d'une Form et localise les Text
        /// Attention fonction récursive ! ! !
        /// </summary>
        protected void Update_Controls(Control.ControlCollection ctrl)
        {

            // il faut rechercher les controls à la mano
            for (int index = 0; index < ctrl.Count; index++)
            {
                if (ctrl[index] is DynamicPanel || ctrl[index] is ILangNonTranslatable)
                    continue;
#if !PocketPC
                if (
                       !(ctrl[index] is RichTextBox)
                    && !(ctrl[index] is TextBox)
                    && !(ctrl[index] is StatusStrip)
                    && !(ctrl[index] is NumericUpDown)
                    && !(ctrl[index] is ComboBox)
                    && !(ctrl[index] is ListView)
                    && !(ctrl[index] is DataGridView)
                    && (ctrl[index].Text != "" || (ctrl[index] is ILangReloadable))
                    )
#else
                    if (!(ctrl[index] is ListView)
                        && (ctrl[index].Text != ""))
#endif
                {
                    if (!Controls.Contains(ctrl[index]))
                    {
                        Controls.Add(ctrl[index]);
                        ControlsText.Add(ctrl[index].Text);
                        ctrl[index].Text = m_LangSys.C(FilePath, ctrl[index].Text);
                    }
                    else
                    {
                        int ListIdx = Controls.IndexOf(ctrl[index]);
                        ctrl[index].Text = m_LangSys.C(FilePath, ControlsText[ListIdx]);
                    }
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
                else if (ctrl[index] is DataGridView)
                {
                    Update_Controls(ctrl[index] as DataGridView);
                }
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
        protected void Update_Controls(ToolStripItemCollection ctrl)
        {

            for (int index = 0; index < ctrl.Count; index++)
            {
                if (ctrl[index].Text != "" && !(ctrl[index] is ToolStripStatusLabel))
                {
                    if (!Controls.Contains(ctrl[index]))
                    {
                        Controls.Add(ctrl[index]);
                        ControlsText.Add(ctrl[index].Text);
                        ctrl[index].Text = m_LangSys.C(FilePath, ctrl[index].Text);
                    }
                    else
                    {
                        int ListIdx = Controls.IndexOf(ctrl[index]);
                        ctrl[index].Text = m_LangSys.C(FilePath, ControlsText[ListIdx]);
                    }
                }
                if (ctrl[index] is ToolStripDropDownItem)
                {
                    if (((ToolStripDropDownItem)ctrl[index]).DropDownItems.Count > 0)
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
        protected void Update_Controls(ListView ctrl)
        {
            for (int index = 0; index < ctrl.Columns.Count; index++)
            {
                if (ctrl.Columns[index].Text != "")
                {
                    if (!Controls.Contains(ctrl.Columns[index]))
                    {
                        Controls.Add(ctrl.Columns[index]);
                        ControlsText.Add(ctrl.Columns[index].Text);
                        ctrl.Columns[index].Text = m_LangSys.C(FilePath, ctrl.Columns[index].Text);
                    }
                    else
                    {
                        int ListIdx = Controls.IndexOf(ctrl.Columns[index]);
                        ctrl.Columns[index].Text = m_LangSys.C(FilePath, ControlsText[ListIdx]);
                    }
                }
            }
        }

        /// <summary>
        /// Parcour tout les éléments du menu et localise le texte
        /// </summary>
        /// <param name="Collection">Collection d'item de menu</param>
        protected void Update_Controls(ContextMenu.MenuItemCollection Collection)
        {
            for (int index = 0; index < Collection.Count; index++)
            {
                if (Collection[index].Text != "")
                {
                    if (!Controls.Contains(Collection[index]))
                    {
                        Controls.Add(Collection[index]);
                        ControlsText.Add(Collection[index].Text);
                        Collection[index].Text = m_LangSys.C(FilePath, Collection[index].Text);
                    }
                    else
                    {
                        int ListIdx = Controls.IndexOf(Collection[index]);
                        Collection[index].Text = m_LangSys.C(FilePath, ControlsText[ListIdx]);
                    }
                }
            }
        }
        protected void Update_Controls(DataGridView gridView)
        {
            for (int index = 0; index < gridView.ColumnCount; index++)
            {
                if (gridView.Columns[index].HeaderText != "")
                {
                    if (!Controls.Contains(gridView.Columns[index]))
                    {
                        Controls.Add(gridView.Columns[index]);
                        ControlsText.Add(gridView.Columns[index].HeaderText);
                        gridView.Columns[index].HeaderText = m_LangSys.C(FilePath, gridView.Columns[index].HeaderText);
                    }
                    else
                    {
                        int ListIdx = Controls.IndexOf(gridView.Columns[index]);
                        gridView.Columns[index].HeaderText = m_LangSys.C(FilePath, ControlsText[ListIdx]);
                    }
                }
            }
        }
    }
}

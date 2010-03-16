using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace CommonLib
{
    class UserControlInfo : BaseInfo
    {
        private UserControl Me;

        public readonly string FormName = "";

        /// <summary>
        /// Constructeur static
        /// </summary>
        /// <param name="frm">Feuille concernée</param>
        /// <returns>Un objet FormInfo initialisé</returns>
        public static UserControlInfo CreateUserControlInfo(Lang LangSys, UserControl frm)
        {
            return new UserControlInfo(LangSys, frm);
        }

        /// <summary>
        /// Constructeur PRIVé de FormInfo
        /// </summary>
        /// <param name="frm">Objet System.Windows.Form</param>
        private UserControlInfo(Lang LangSys, UserControl frm) : base(LangSys)
        {
            // stocke le nom de la feuille
            FormName = frm.Name;
            Me = frm;
            // attache l'intercepteur d'évènements
            frm.Load += new EventHandler(this.UserControl_Load);
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
        public void UserControl_Load(object sender, EventArgs e)
        {
            if (sender != null)
            {
                Controls.Clear();
                ControlsText.Clear();
                UserControl frm = (UserControl)sender;
                Update_Controls(frm.Controls);
                if (frm.ContextMenu != null)
                    Update_Controls(frm.ContextMenu.MenuItems);
            }
            else
            {
                m_LangSys.LoadLangage(FilePath);

                Update_Controls(Me.Controls);
                foreach (object var in Controls)
                {
                    if (var is ILangReloadable)
                    {
                        ((ILangReloadable)var).LoadNonStandardLang();
                        Traces.LogAdd(Traces.LOG_LEVEL_INFO, "Lang", "appel à LoadNonStandardLang");
                    }
                }
            }
        }
    }
}

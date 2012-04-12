using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.IO;
using System.Windows.Forms;
using CommonLib;

namespace CtrlTwoBitmap
{
    public partial class TwoBitmapProperties : BaseControlPropertiesPanel, ISpecificPanel
    {
        #region events
        public event ControlPropertiesChange ControlPropertiesChanged;
        #endregion        

        public TwoBitmapProperties()
        {
            DllEntryClass.LangSys.Initialize(this);
            InitializeComponent();
        }

        #region validation des données
        public void PanelToObject()
        {
            bool bDataPropChange = false;
            if (m_txtBoxImg1.Text != ((TwoBitmapProp)m_Control.SpecificProp).NomFichierInactif)
                bDataPropChange = true;

            if (m_txtBoxImg2.Text != ((TwoBitmapProp)m_Control.SpecificProp).NomFichierActif)
                bDataPropChange = true;

            if (bDataPropChange)
            {
                ((TwoBitmapProp)m_Control.SpecificProp).NomFichierInactif = m_txtBoxImg1.Text;
                ((TwoBitmapProp)m_Control.SpecificProp).NomFichierActif = m_txtBoxImg2.Text;
                Document.Modified = true;
                m_Control.IControl.Refresh();
            }
            if (bDataPropChange && ControlPropertiesChanged != null)
                ControlPropertiesChanged(m_Control);

        }

        public void ObjectToPanel()
        {
            m_txtBoxImg1.Text = ((TwoBitmapProp)m_Control.SpecificProp).NomFichierInactif;
            m_txtBoxImg2.Text = ((TwoBitmapProp)m_Control.SpecificProp).NomFichierActif;
        }
        #endregion

        private void m_btnImg1_Click(object sender, EventArgs e, BrowseFileBtn.BrowseFrom bf)
        {
            DialogResult dlgRes = CentralizedFileDlg.ShowImageFileDilaog(bf);
            if (dlgRes == DialogResult.OK)
            {

                m_txtBoxImg1.Text = PathTranslator.LinuxVsWindowsPathStore(
                                    Document.PathTr.AbsolutePathToRelative(CentralizedFileDlg.ImgFileName));
            }

        }

        private void m_btnImg2_Click(object sender, EventArgs e, BrowseFileBtn.BrowseFrom bf)
        {
            DialogResult dlgRes = CentralizedFileDlg.ShowImageFileDilaog(bf);
            if (dlgRes == DialogResult.OK)
            {
                m_txtBoxImg2.Text = PathTranslator.LinuxVsWindowsPathStore(
                                    Document.PathTr.AbsolutePathToRelative(CentralizedFileDlg.ImgFileName));
            }
        }
    }
}

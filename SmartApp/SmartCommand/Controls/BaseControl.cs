/***************************************************************************/
// PROJET : BTCommand : system de commande paramétrable pour équipement
// ayant une mécanisme de commande par liaison série/ethernet/http
/***************************************************************************/
// Fichier : 
/***************************************************************************/
// description :
// 
/***************************************************************************/
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Xml;
using SmartApp.Datas;
using SmartApp.Ihm;
using SmartApp.Scripts;

namespace SmartApp.Controls
{
    public abstract class BaseControl : BTControl
    {
        #region Déclaration des données de la classe
        protected Control m_Ctrl;
        protected Data m_AssociateData;
        protected BTScreen m_Parent;

        protected Rectangle m_RectControl;
        #endregion

        protected ScriptExecuter m_Executer = null;

        public ScriptExecuter Executer
        {
            get
            {
                return m_Executer;
            }
            set
            {
                m_Executer = value;
            }
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public Control DisplayedControl
        {
            get
            {
                return m_Ctrl;
            }
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public BaseControl()
        {
            m_Ctrl = null;
            m_bUseScreenEvent = false;
            m_AssociateData = null;
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public void SetParent(BTScreen btScreen)
        {
            m_Parent = btScreen;
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public abstract void CreateControl();

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public abstract void OnControlEvent(Object Sender, EventArgs Args);

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public virtual void UpdateFromData()
        {

        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public override bool ReadIn(XmlNode Node)
        {
            base.ReadIn(Node);
            this.m_RectControl = new Rectangle(this.IControl.Location, this.IControl.Size);
            return true;
        }

        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public override bool FinalizeRead(BTDoc Doc)
        {
            if (!string.IsNullOrEmpty(m_strAssociateData))
            {
                m_AssociateData = (Data)Doc.GestData.GetFromSymbol(m_strAssociateData);
                if (m_AssociateData == null)
                {
                    // TODO : loguer les erreurs
                    // pas d'assert ici, car par exemple un bouton ou un static peuvent ne pas avoir de donnée
                    //Console.WriteLine("Donnée Associée non trouvée");
                    return true;
                }
                else
                {
                    m_AssociateData.DataValueChanged += new EventDataValueChange(UpdateFromData);
                }
            }
            Executer = Doc.Executer;
            return true;
        }

        #region Gestion des AppMessages
        //*****************************************************************************************************
        // Description:
        // Return: /
        //*****************************************************************************************************
        public override void TraiteMessage(MESSAGE Mess, object obj)
        {
            if (Mess == MESSAGE.MESS_UPDATE_FROM_DATA)
            {
                UpdateFromData();
            }
        }
        #endregion

    }
}

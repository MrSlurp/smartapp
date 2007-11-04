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
using System.Text;
using System.Xml;
using System.Windows.Forms;
using SmartApp.Datas;
using SmartApp.Ihm;

namespace SmartApp.Gestionnaires
{
    public class BaseGest : Object
    {
        #region constantes
        // divers constantes pour les symbols par défaut
        public const int MAX_DEFAULT_ITEM_SYMBOL = 1000;
        public const string DEFAULT_DATA_SYMB = "NEW_DATA_{0}";
        public const string DEFAULT_CTRL_SYMB = "NEW_CTRL_{0}";
        public const string DEFAULT_SCREEN_SYMB = "NEW_SCREEN_{0}";
        public const string DEFAULT_FUNCTION_SYMB = "NEW_FUNCTION_{0}";
        public const string DEFAULT_FRAME_SYMB = "NEW_FRAME_{0}";
        public const string DEFAULT_TIMER_SYMB = "NEW_TIMER_{0}";
        public const string DEFAULT_LOGGER_SYMB = "NEW_LOGGER_{0}";
        #endregion

        #region données membres
        // liste des objets du gestionnaire
        protected List<BaseObject> m_ListObject = new List<BaseObject>();
        #endregion

        #region events
        // event déclenché pour envoyer des messages
        public event SendMessage DoSendMessage;
        #endregion

        #region Methodes d'accès aux objets stocké par le gestionnaire
        //*****************************************************************************************************
        // Description: ajoute un objet
        // Return: /
        //*****************************************************************************************************
        /// Ajoute un objet au gestionnaire
        public virtual void AddObj(BaseObject Obj)
        {
            m_ListObject.Add(Obj);
            // le gestionnaire s'enregistre auprès de l'objet pour recevoir les message qu'il déclenche
            Obj.DoSendMessage += new SendMessage(ObjectSendMessage);
        }

        //*****************************************************************************************************
        // Description: enlève un objet 
        // Return: renvoie true si l'objet a bien été enlevé
        //*****************************************************************************************************
        public virtual bool RemoveObj(BaseObject Obj)
        {
            // avant de supprimer l'objet on fait tout plein de chose
            bool bProcessMessage = true;
            // déja on n'avertis pas l'utilisateur pour les données de control qui seraient supprimées automatiquement
            if (Obj.Symbol.EndsWith(Cste.STR_SUFFIX_CTRLDATA))
                bProcessMessage = false;

            MessAskDelete MessAskDel = new MessAskDelete();
            MessAskDel.TypeOfItem = Obj.GetType();
            MessAskDel.WantDeletetItemSymbol = Obj.Symbol;
            //on envoie un message pour savoir ce que ca va impliquer
            if (bProcessMessage)
            {
                this.ObjectSendMessage(MESSAGE.MESS_ASK_ITEM_DELETE, MessAskDel);
                if (MessAskDel.ListStrReturns.Count > 0)
                {
                    ChangeListForm Changeform = new ChangeListForm();
                    Changeform.strListMess = MessAskDel.ListStrReturns;
                    DialogResult dRes = Changeform.ShowDialog();
                    if (dRes == DialogResult.Cancel)
                    {
                        MessAskDel.CancelDelete = true;
                    }
                }
            }
            // si l'utilisateur est d'accord
            // 
            if (bProcessMessage && !MessAskDel.CancelDelete)
            {
                m_ListObject.Remove(Obj);
                MessDeleted MessDel = new MessDeleted();
                MessDel.TypeOfItem = Obj.GetType();
                MessDel.DeletetedItemSymbol = Obj.Symbol;
                ObjectSendMessage(MESSAGE.MESS_ITEM_DELETED, MessDel);
                return true;
            }
            else if (!bProcessMessage)
            {
                //Quand recrée les données de control, il ne faut pas envoyer de message de suppression
                // mais pluot un message de renomage....ceci est fait dans la fonction qui met a jour les données
                // de control
                m_ListObject.Remove(Obj);
                return true;
            }
            else
                return false;
        }

        //*****************************************************************************************************
        // Description: enlève un objet 
        // Return: /
        //*****************************************************************************************************
        public virtual bool RemoveObj(string strSymbol)
        {
            BaseObject obj = GetFromSymbol(strSymbol);
            return RemoveObj(obj);
        }

        //*****************************************************************************************************
        // Description: renvoie l'objet a l'index donné
        // Return: /
        //*****************************************************************************************************
        public BaseObject this[int i]
        {
            get
            {
                return m_ListObject[i];
            }
        }

        //*****************************************************************************************************
        // Description: obtient le nombre d'objet contenu dans le gestionaire
        // Return: /
        //*****************************************************************************************************
        public int Count
        {
            get
            {
                return m_ListObject.Count;
            }
        }

        //*****************************************************************************************************
        // Description: renvoie l'objet a partir du symbol donné
        // Return: /
        //*****************************************************************************************************
        public BaseObject GetFromSymbol(string strSymbol)
        {
            int nbItem = m_ListObject.Count;
            for (int i = 0; i < nbItem; i++)
            {
                BaseObject baseObj = (BaseObject)m_ListObject[i];
                if (baseObj.Symbol == strSymbol)
                    return baseObj;
            }
            return null;
        }

        //*****************************************************************************************************
        // Description:
        // obtien l'index de l'objet ayant le symbol passé en paramètres
        // L'ArrayList étant une donné protégé, et cette classe n'étant pas indexé
        // cette methode n'est utilisable qu'en interne
        // Return: /
        //*****************************************************************************************************
        protected int GetIndexFromSymbol(string strSymbol)
        {
            for (int i = 0; i < m_ListObject.Count; i++)
            {
                BaseObject baseObj = m_ListObject[i];
                if (baseObj.Symbol == strSymbol)
                    return i;
            }
            return -1;
        }

        #endregion

        #region ReadIn / WriteOut
        //*****************************************************************************************************
        // Description: Lit les données de l'objet a partir de son noeud XML
        // Return: /
        //*****************************************************************************************************
        public virtual bool ReadIn(XmlNode Node)
        {
            return true;
        }

        //*****************************************************************************************************
        // Description: ecrit les données de l'objet a partir de son noeud XML
        // Return: /
        //*****************************************************************************************************
        public virtual bool WriteOut(XmlDocument XmlDoc, XmlNode Node)
        {
            return true;
        }

        //*****************************************************************************************************
        // Description:
        // cette fonction a pour but de permettre aux objet crée d'utiliser le document pour terminer 
        // leurs initialisation car lors de la lecture, seul les symbols des objects sont relu
        // il faut les lier aux références
        // UTILISE DANS SMARTCmd uniquement. Le but principale est d'éviter d'avoir a parcourir les listes
        // des gestionaires lorsqu'on a besoin d'accèder a un objet a partir de son symbol
        // dans SMARTCmd les symbols d'objets ne changent pas donc on peux se rattacher directement a l'objet
        // Return: /
        //*****************************************************************************************************
        public virtual bool FinalizeRead(BTDoc Doc)
        {
            for (int i = 0; i < m_ListObject.Count; i++)
            {
                if (!m_ListObject[i].FinalizeRead(Doc))
                    return false;
            }
            return true;
        }
        #endregion

        #region Fonction "utilitaires"
        //*****************************************************************************************************
        // Description: chaque gestionaire est capable de donner un nom par défaut non encore utilisé a un nouveau objet 
        // crée, cette fonction virtuelle réalise ceci dans les classes filles.
        // Return: /
        //*****************************************************************************************************
        public virtual string GetNextDefaultSymbol()
        {
            return "";
        }
        #endregion

        #region Gestion des AppMessages
        //*****************************************************************************************************
        // Description: transmet les messages aux objets eux meme
        // Return: /
        //*****************************************************************************************************
        public virtual void TraiteMessage(MESSAGE Mess, object obj)
        {
            for (int i = 0; i < m_ListObject.Count; i++)
            {
                m_ListObject[i].TraiteMessage(Mess, obj);
            }
        }

        //*****************************************************************************************************
        // Description: callback appelé par l'event des objets
        // Return: /
        //*****************************************************************************************************
        public virtual void ObjectSendMessage(MESSAGE Mess, object Param)
        {
            if (DoSendMessage != null)
            {
                DoSendMessage(Mess, Param);
            }
        }
        #endregion
    }
}

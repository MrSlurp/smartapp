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
using System.Collections;
using System.Text;
using System.Xml;
using System.Windows.Forms;

namespace CommonLib
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
		
		protected Hashtable m_HashObjects = new Hashtable();
        #endregion

        #region events
        // event déclenché pour envoyer des messages
        public event SendMessage DoSendMessage;
        public event AddLogEventDelegate EventAddLogEvent;
        #endregion

        #region Methodes d'accès aux objets stocké par le gestionnaire
        /// <summary>
        /// Ajoute un objet dans le gestionnaire
        /// </summary>
        /// <param name="Obj">objet à ajouter au gestionnaire</param>
        public virtual void AddObj(BaseObject Obj)
        {
            m_ListObject.Add(Obj);
            // le gestionnaire s'enregistre auprès de l'objet pour recevoir les message qu'il déclenche
            Obj.DoSendMessage += new SendMessage(ObjectSendMessage);
            Obj.EventAddLogEvent += new AddLogEventDelegate(AddLogEvent); 
        }

        /// <summary>
        /// enlève un objet 
        /// </summary>
        /// <param name="Obj">objet à enlever</param>
        /// <returns>true si l'objet à bien été enlevé</returns>
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
                this.ObjectSendMessage(MESSAGE.MESS_ASK_ITEM_DELETE, MessAskDel, TYPE_APP.SMART_CONFIG);
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
                ObjectSendMessage(MESSAGE.MESS_ITEM_DELETED, MessDel, TYPE_APP.SMART_CONFIG);
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

        /// <summary>
        /// enlève un objet 
        /// </summary>
        /// <param name="strSymbol">symbole de l'objet à enlever</param>
        /// <returns>true si l'objet à bien été enlevé</returns>
        public virtual bool RemoveObj(string strSymbol)
        {
            BaseObject obj = GetFromSymbol(strSymbol);
            return RemoveObj(obj);
        }

        /// <summary>
        /// indexeur permettant d'accéder aux objets du gestionnaire comme pour un tableau
        /// </summary>
        /// <param name="i">index</param>
        /// <returns>l'objet à l'index i</returns>
        public BaseObject this[int i]
        {
            get
            {
                return m_ListObject[i];
            }
        }

        /// <summary>
        /// obtient le nombre d'objet contenu dans le gestionaire
        /// </summary>
        public int Count
        {
            get
            {
                return m_ListObject.Count;
            }
        }

        /// <summary>
        /// renvoie l'objet a partir du symbol donné
        /// </summary>
        /// <param name="strSymbol">Symbol de l'objet recherché</param>
        /// <returns>objet ayant le symbol donné</returns>
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

        public BaseObject QuickGetFromSymbol(string strSymbol)
        {
			BaseObject baseObj = (BaseObject)m_HashObjects[strSymbol];
			return baseObj;
        }

		
        /// <summary>
        /// utilisé uniquement en interne et dans la classé hérité, renvoie l'index d'un objet ayant le symbol donné
        /// </summary>
        /// <param name="strSymbol">symbol de l'objet dont on veut l'index</param>
        /// <returns>index de l'objet</returns>
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
        /// <summary>
        /// Lit les données de l'objet a partir de son noeud XML
        /// La classe de base ne fait rien car jamais utilisé tel quel
        /// </summary>
        /// <param name="Node">Noeud Xml de l'objet</param>
        /// <param name="TypeApp">type d'application courante</param>
        /// <returns>true si la lecture s'est bien passé</returns>
        public virtual bool ReadIn(XmlNode Node, TYPE_APP TypeApp)
        {
            return true;
        }

        /// <summary>
        /// écrit les données de l'objet dans le fichier XML
        /// La classe de base ne fait rien car jamais utilisé tel quel
        /// </summary>
        /// <param name="XmlDoc">Document XML courant</param>
        /// <param name="Node">Noeud parent du controle dans le document</param>
        /// <returns>true si l'écriture s'est déroulée avec succès</returns>
        public virtual bool WriteOut(XmlDocument XmlDoc, XmlNode Node)
        {
            return true;
        }

        /// <summary>
        /// cette fonction a pour but de permettre aux objet crée d'utiliser le document pour terminer  
        /// leurs initialisation car lors de la lecture, seul les symbols des objects sont relu
        /// il faut les lier aux références
        /// UTILISE DANS SMARTCmd uniquement. Le but principale est d'éviter d'avoir a parcourir les listes
        /// des gestionaires lorsqu'on a besoin d'accèder a un objet a partir de son symbol
        /// dans SMARTCmd les symbols d'objets ne changent pas donc on peux se rattacher directement a l'objet
        /// </summary>
        /// <param name="Doc">Document courant</param>
        /// <returns>true si tout s'est bien passé</returns>
        public virtual bool FinalizeRead(BTDoc Doc)
        {
            for (int i = 0; i < m_ListObject.Count; i++)
            {
				m_HashObjects.Add(m_ListObject[i].Symbol, m_ListObject[i]);
                if (!m_ListObject[i].FinalizeRead(Doc))
                    return false;
            }
            return true;
        }
        #endregion

        #region Fonction "utilitaires"
        /// <summary>
        /// chaque gestionaire est capable de donner un nom par défaut non encore utilisé a un nouveau objet 
        /// crée, cette fonction virtuelle réalise ceci dans les classes filles.
        /// </summary>
        /// <returns>le symbol du prochain objet de base qui sera créé</returns>
        public virtual string GetNextDefaultSymbol()
        {
            return "";
        }
        #endregion

        #region Gestion des AppMessages
        /// <summary>
        /// effectue les opération nécessaire lors de la récéption d'un message
        /// </summary>
        /// <param name="Mess">Type de message</param>
        /// <param name="obj">objet contenant les paramètres du messages</param>
        /// <param name="TypeApp">Type d'application courante</param>
        public virtual void TraiteMessage(MESSAGE Mess, object obj, TYPE_APP TypeApp)
        {
            for (int i = 0; i < m_ListObject.Count; i++)
            {
                m_ListObject[i].TraiteMessage(Mess, obj, TypeApp);
            }
        }

        /// <summary>
        /// callback appelé par l'event des objets
        /// </summary>
        /// <param name="Mess">Type de message</param>
        /// <param name="obj">objet contenant les paramètres du messages</param>
        /// <param name="TypeApp">Type d'application courante</param>
        public virtual void ObjectSendMessage(MESSAGE Mess, object Param, TYPE_APP TypeApp)
        {
            if (DoSendMessage != null)
            {
                DoSendMessage(Mess, Param, TypeApp);
            }
        }
        #endregion

        #region Méthodes diverses
        /// <summary>
        /// envoie un évènement vers le logger de SmartCommand
        /// </summary>
        /// <param name="Event">objet évènement</param>
        protected void AddLogEvent(LogEvent Event)
        {
            if (EventAddLogEvent != null)
            {
                EventAddLogEvent(Event);
            }
        }
        #endregion
    }
}

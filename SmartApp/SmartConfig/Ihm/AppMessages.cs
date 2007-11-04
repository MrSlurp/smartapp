using System;
using System.Collections.Specialized;
using System.Text;

namespace SmartApp
{
    // fonction délégué a l'envoie de message inter objets
    public delegate void SendMessage(MESSAGE Mess, object Param);
 
    // enum des types de messages
    public enum MESSAGE
    {
        // message notifiant la destruction d'un objet
        MESS_ITEM_DELETED,

        // message notifiant l'intention de detruire un objet
        // permet d'obtenir via la liste passé en paramètres toutes les modification que ca implique
        // pour demander a l'utilisateur si il veux continuer
        MESS_ASK_ITEM_DELETE,

        // message notifiant le renomage (symbol) d'un objet
        MESS_ITEM_RENAMED,

        // message indiquant que des paramètres ont changés
        // utilisé pr mettre ajour le modified flag
        MESS_CHANGE,

        // Message spécifique a SmartCommand
        // demande la mise a jour des controls en fonction de la donnée associée
        MESS_UPDATE_FROM_DATA,

        // message run mode Commande
        MESS_CMD_RUN,

        // message stop mode Commande
        MESS_CMD_STOP,
    }

    public class BaseMessage
    {
        public Type TypeOfItem;

    }
    //*****************************************************************************************************
    // Description:
    // Return: /
    //*****************************************************************************************************
    public class MessDeleted : BaseMessage
    {
        public string DeletetedItemSymbol;
    }

    //*****************************************************************************************************
    // Description:
    // Return: /
    //*****************************************************************************************************
    public class MessAskDelete : BaseMessage
    {
        public string WantDeletetItemSymbol;
        public StringCollection ListStrReturns = new StringCollection();
        private bool m_bCancel = false;

        public bool CancelDelete
        {
            get
            {
                return m_bCancel;
            }
            set
            {
                // une fois que le cancel a été validé, on ne peux plus l'annuler
                m_bCancel |= value;
            }
        }
    }

    //*****************************************************************************************************
    // Description:
    // Return: /
    //*****************************************************************************************************
    public class MessItemRenamed : BaseMessage
    {
        public string OldItemSymbol;
        public string NewItemSymbol;
    }

}

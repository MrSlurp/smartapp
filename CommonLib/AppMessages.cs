/*
    This file is part of SmartApp.

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
using System.Collections.Specialized;
using System.Text;

namespace CommonLib
{
    // fonction délégué a l'envoie de message inter objets
    public delegate void SendMessage(MESSAGE Mess, object Param, TYPE_APP TypeApp);
 
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

        // message run mode Commande
        MESS_CMD_RUN,

        // message stop mode Commande
        MESS_CMD_STOP,
        
        // message envoyé avant la commande RUN pour demander aux objet scriptables de 
        // pré parser leur script
        MESS_PRE_PARSE,

        MESS_SPLIT,

        MESS_JOIN,
    }

    //*****************************************************************************************************
    // Description: classe de base qui possède le type de l'objet détruit / modifié
    // Return: /
    //*****************************************************************************************************
    public class BaseMessage
    {
        public Type TypeOfItem;

    }

    //*****************************************************************************************************
    // Description: message envoyé lorsque l'utilisateur supprime un objet
    // Return: /
    //*****************************************************************************************************
    public class MessDeleted : BaseMessage
    {
        public string DeletetedItemSymbol;
    }

    //*****************************************************************************************************
    // Description: message envoyé lorsque l'utilisateur souhaite supprimer qqch
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
    // Description: message envoyé lorsqu'un objet change de nom
    // Return: /
    //*****************************************************************************************************
    public class MessItemRenamed : BaseMessage
    {
        public string OldItemSymbol;
        public string NewItemSymbol;
    }

    //*****************************************************************************************************
    // Description: message envoyé lorsqu'un objet change de nom
    // Return: /
    //*****************************************************************************************************
    public class MessDataSplited : BaseMessage
    {
        public string DataSplittedSymbol;
        public StringCollection NewReplacingDatas;
    }

    //*****************************************************************************************************
    // Description: message envoyé lorsqu'un objet change de nom
    // Return: /
    //*****************************************************************************************************
    public class MessDataJoined : BaseMessage
    {
        public StringCollection DataJoinedSymbols;
        public string NewReplacingData;
    }

    //*****************************************************************************************************
    // Description: message qui améliore la finesse de rafraichissement des IHM en fonction de ce qui a changé
    // Return: /
    //*****************************************************************************************************
    public class MessNeedUpdate : BaseMessage
    {
        public bool bUpdateScreenForm;

        public MessNeedUpdate(BaseMessage mess)
        {
            if (mess.TypeOfItem == typeof(CommonLib.Data))
            {
                bUpdateScreenForm = true;
            }
        }
    }
}

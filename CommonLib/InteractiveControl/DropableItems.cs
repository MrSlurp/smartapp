using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace CommonLib
{
    public static class DropableItems
    {
        /// <summary>
        /// parcour les données de drag drop pour en extraire l'objet qui peut être droppé dans la surface de
        /// dessin
        /// </summary>
        /// <param name="e">argument de drap/drop</param>
        /// <returns>l'objet extrait des données de drag drop</returns>
        public static InteractiveControl GetDropableItem(DragEventArgs e)
        {
            //InteractiveControl DropedItem = null;
            string[] strListFormats = e.Data.GetFormats();
            for (int i = 0; i < strListFormats.Length;/* i++*/)
            {
                Type tp = Type.GetType(strListFormats[i], false, false);
                if (tp != null && tp.IsSubclassOf(typeof(InteractiveControl)))
                {
                    return (InteractiveControl)e.Data.GetData(tp);
                }
                else
                {
                    return (InteractiveControl)e.Data.GetData(strListFormats[i]);
                }
            }
            return null;
            
        }

        /// <summary>
        /// indique si l'objet du type donné est autorisé à être relaché dans la surface de dessin
        /// </summary>
        /// <param name="ObjType">type de l'objet</param>
        /// <returns></returns>
        public static bool AllowedItem(Type ObjType)
        {
            return (ObjType.IsSubclassOf(typeof(InteractiveControl)) || ObjType == typeof(InteractiveControl));
        }
    }
}

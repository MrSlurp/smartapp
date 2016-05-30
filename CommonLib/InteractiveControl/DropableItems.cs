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

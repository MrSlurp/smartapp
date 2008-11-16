using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace CommonLib
{
    public static class DropableItems
    {
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

        public static bool AllowedItem(Type ObjType)
        {
            return (ObjType.IsSubclassOf(typeof(InteractiveControl)) || ObjType == typeof(InteractiveControl));
        }
    }
}

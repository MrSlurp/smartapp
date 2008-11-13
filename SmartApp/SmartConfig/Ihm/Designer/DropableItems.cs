using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace SmartApp.Ihm.Designer
{
    public static class DropableItems
    {
        public static InteractiveControl GetDropableItem(DragEventArgs e)
        {
            InteractiveControl DropedItem = null;
            DropedItem = (InteractiveControl)e.Data.GetData(typeof(InteractiveControl));
            if (DropedItem != null)
                return DropedItem;
            DropedItem = (InteractiveControl)e.Data.GetData(typeof(TwoColorFilledRect));
            if (DropedItem != null)
                return DropedItem;
            DropedItem = (InteractiveControl)e.Data.GetData(typeof(TwoColorFilledEllipse));
            if (DropedItem != null)
                return DropedItem;

            return null;
        }

        public static bool AllowedItem(Type ObjType)
        {
            if (ObjType == typeof(InteractiveControl)
                || ObjType == typeof(TwoColorFilledRect)
                || ObjType == typeof(TwoColorFilledEllipse)
                )
                return true;
            else
                return false;
        }
    }
}

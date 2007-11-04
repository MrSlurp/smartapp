using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SmartApp.Ihm.Designer
{
    //*****************************************************************************************************
    // Description: ce panel contiens de base un intercative control de chaque type
    // elle offre la possibilité de faire un drag and drop vers le designer (Interactive control container)
    //*****************************************************************************************************
    public partial class DragItemPanel : UserControl
    {
        //*****************************************************************************************************
        // Description: constructeur
        // Return: /
        //*****************************************************************************************************
        public DragItemPanel()
        {
            InitializeComponent();
            m_ToolDragItemBtn.Text = "Button";
            m_ToolDragItemCheckBox.Text = "CheckBox";
            m_ToolDragItemSlider.Text = "Slider";
            m_ToolDragItemNumUpDown.Text = "Numeric Up/Down";
            m_ToolDragItemText.Text = "Text";
            m_ToolDragItemcombo.Text = "Combo";
        }

        //*****************************************************************************************************
        // Description: commence l'opération de drag and drop avec l'interactiveControl cliqué
        // Return: /
        //*****************************************************************************************************
        private void OnItemMouseDown(object sender, MouseEventArgs e)
        {
            // Starts a drag-and-drop operation with that item.
            if (sender != null)
            {
                this.DoDragDrop(sender, DragDropEffects.All);
            }
        }
    }
}

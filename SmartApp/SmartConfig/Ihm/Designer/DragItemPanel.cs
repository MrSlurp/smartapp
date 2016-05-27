using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using CommonLib;

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
            if (!this.DesignMode)
            {
                InitDllComponent();
                m_ToolDragItemBtn.Text = "Button";
                m_ToolDragItemCheckBox.Text = "CheckBox";
                m_ToolDragItemSlider.Text = "Slider";
                m_ToolDragItemNumUpDown.Text = "Numeric Up/Down";
                m_ToolDragItemText.Text = "Text";
                m_ToolDragItemcombo.Text = "Combo";
            }
        }

        protected void InitDllComponent()
        {
            int ECART = 8;
            this.SuspendLayout();
            int DownPos = 0;
            // on doit d'abord trouver la position basse du dernier control
            for (int i = 0; i < this.Controls.Count; i++)
            {
                if (DownPos < (this.Controls[i].Location.Y + this.Controls[i].Height))
                {
                    DownPos = (this.Controls[i].Location.Y + this.Controls[i].Height);
                }
            }
            DownPos += ECART;
            if (Program.DllGest != null)
            {
                StringCollection category = Program.DllGest.TypeList;
                for (int i = 0; i < category.Count; i++)
                {
                    List<IDllControlInterface> list = Program.DllGest[category[i]];

                    Label labelType = new Label();

                    labelType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Left)
                                | System.Windows.Forms.AnchorStyles.Right | System.Windows.Forms.AnchorStyles.Top)));
                    labelType.BackColor = System.Drawing.SystemColors.ActiveBorder;
                    labelType.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                    labelType.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    labelType.Location = new System.Drawing.Point(0, DownPos);
                    labelType.Name = "label" + category[i];
                    labelType.Size = new System.Drawing.Size(this.Width, 21);
                    labelType.TabIndex = 1;
                    labelType.Text = category[i];
                    DownPos += labelType.Size.Height;
                    DownPos += ECART;
                    this.Controls.Add(labelType);

                    for (int j = 0; j < list.Count; j++)
                    {
                        IDllControlInterface Dll = list[j];
                        Size sz = Dll.ToolWindSize;
                        InteractiveControl newICtrl = Dll.CreateInteractiveControl();
                        newICtrl.AllowDrop = true;
                        newICtrl.Location = new System.Drawing.Point(15, DownPos);
                        newICtrl.Name = Dll.DefaultControlName;
                        newICtrl.Selected = false;
                        newICtrl.Size = sz;
                        newICtrl.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnItemMouseDown);
                        this.Controls.Add(newICtrl);
                        DownPos += newICtrl.Size.Height;
                        DownPos += ECART;
                    }
                }
            }
            this.ResumeLayout(false);
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

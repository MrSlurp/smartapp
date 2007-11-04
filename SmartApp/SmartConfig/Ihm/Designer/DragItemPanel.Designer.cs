namespace SmartApp.Ihm.Designer
{
    partial class DragItemPanel
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.m_ToolDragItemBtn = new SmartApp.Ihm.Designer.InteractiveControl();
            this.m_ToolDragItemCheckBox = new SmartApp.Ihm.Designer.InteractiveControl();
            this.m_ToolDragItemSlider = new SmartApp.Ihm.Designer.InteractiveControl();
            this.m_ToolDragItemNumUpDown = new SmartApp.Ihm.Designer.InteractiveControl();
            this.m_ToolDragItemText = new SmartApp.Ihm.Designer.InteractiveControl();
            this.m_ToolDragItemcombo = new SmartApp.Ihm.Designer.InteractiveControl();
            this.SuspendLayout();
            // 
            // m_ToolDragItemBtn
            // 
            this.m_ToolDragItemBtn.ControlType = SmartApp.Ihm.Designer.InteractiveControlType.Button;
            this.m_ToolDragItemBtn.Location = new System.Drawing.Point(3, 3);
            this.m_ToolDragItemBtn.Name = "m_ToolDragItemBtn";
            this.m_ToolDragItemBtn.Selected = false;
            this.m_ToolDragItemBtn.Size = new System.Drawing.Size(132, 20);
            this.m_ToolDragItemBtn.SourceBTControl = null;
            this.m_ToolDragItemBtn.TabIndex = 0;
            this.m_ToolDragItemBtn.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnItemMouseDown);
            // 
            // m_ToolDragItemCheckBox
            // 
            this.m_ToolDragItemCheckBox.ControlType = SmartApp.Ihm.Designer.InteractiveControlType.CheckBox;
            this.m_ToolDragItemCheckBox.Location = new System.Drawing.Point(3, 29);
            this.m_ToolDragItemCheckBox.Name = "m_ToolDragItemCheckBox";
            this.m_ToolDragItemCheckBox.Selected = false;
            this.m_ToolDragItemCheckBox.Size = new System.Drawing.Size(132, 20);
            this.m_ToolDragItemCheckBox.SourceBTControl = null;
            this.m_ToolDragItemCheckBox.TabIndex = 1;
            this.m_ToolDragItemCheckBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnItemMouseDown);
            // 
            // m_ToolDragItemSlider
            // 
            this.m_ToolDragItemSlider.ControlType = SmartApp.Ihm.Designer.InteractiveControlType.Slider;
            this.m_ToolDragItemSlider.Location = new System.Drawing.Point(3, 55);
            this.m_ToolDragItemSlider.Name = "m_ToolDragItemSlider";
            this.m_ToolDragItemSlider.Selected = false;
            this.m_ToolDragItemSlider.Size = new System.Drawing.Size(132, 40);
            this.m_ToolDragItemSlider.SourceBTControl = null;
            this.m_ToolDragItemSlider.TabIndex = 2;
            this.m_ToolDragItemSlider.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnItemMouseDown);
            // 
            // m_ToolDragItemNumUpDown
            // 
            this.m_ToolDragItemNumUpDown.ControlType = SmartApp.Ihm.Designer.InteractiveControlType.NumericUpDown;
            this.m_ToolDragItemNumUpDown.Location = new System.Drawing.Point(3, 101);
            this.m_ToolDragItemNumUpDown.Name = "m_ToolDragItemNumUpDown";
            this.m_ToolDragItemNumUpDown.Selected = false;
            this.m_ToolDragItemNumUpDown.Size = new System.Drawing.Size(132, 20);
            this.m_ToolDragItemNumUpDown.SourceBTControl = null;
            this.m_ToolDragItemNumUpDown.TabIndex = 3;
            this.m_ToolDragItemNumUpDown.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnItemMouseDown);
            // 
            // m_ToolDragItemText
            // 
            this.m_ToolDragItemText.ControlType = SmartApp.Ihm.Designer.InteractiveControlType.Text;
            this.m_ToolDragItemText.Location = new System.Drawing.Point(3, 127);
            this.m_ToolDragItemText.Name = "m_ToolDragItemText";
            this.m_ToolDragItemText.Selected = false;
            this.m_ToolDragItemText.Size = new System.Drawing.Size(132, 20);
            this.m_ToolDragItemText.SourceBTControl = null;
            this.m_ToolDragItemText.TabIndex = 4;
            this.m_ToolDragItemText.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnItemMouseDown);
            // 
            // m_ToolDragItemcombo
            // 
            this.m_ToolDragItemcombo.ControlType = SmartApp.Ihm.Designer.InteractiveControlType.Combo;
            this.m_ToolDragItemcombo.Location = new System.Drawing.Point(3, 153);
            this.m_ToolDragItemcombo.Name = "m_ToolDragItemcombo";
            this.m_ToolDragItemcombo.Selected = false;
            this.m_ToolDragItemcombo.Size = new System.Drawing.Size(132, 20);
            this.m_ToolDragItemcombo.SourceBTControl = null;
            this.m_ToolDragItemcombo.TabIndex = 5;
            this.m_ToolDragItemcombo.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnItemMouseDown);
            // 
            // DragItemPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.m_ToolDragItemBtn);
            this.Controls.Add(this.m_ToolDragItemCheckBox);
            this.Controls.Add(this.m_ToolDragItemSlider);
            this.Controls.Add(this.m_ToolDragItemNumUpDown);
            this.Controls.Add(this.m_ToolDragItemText);
            this.Controls.Add(this.m_ToolDragItemcombo);
            this.Name = "DragItemPanel";
            this.Size = new System.Drawing.Size(157, 200);
            this.ResumeLayout(false);

        }

        private SmartApp.Ihm.Designer.InteractiveControl m_ToolDragItemBtn;
        private SmartApp.Ihm.Designer.InteractiveControl m_ToolDragItemCheckBox;
        private SmartApp.Ihm.Designer.InteractiveControl m_ToolDragItemSlider;
        private SmartApp.Ihm.Designer.InteractiveControl m_ToolDragItemNumUpDown;
        private SmartApp.Ihm.Designer.InteractiveControl m_ToolDragItemText;
        private SmartApp.Ihm.Designer.InteractiveControl m_ToolDragItemcombo;


        #endregion
    }
}

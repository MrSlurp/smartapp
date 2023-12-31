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
            this.m_ToolDragItemBtn = new CommonLib.InteractiveControl();
            this.m_ToolDragItemCheckBox = new CommonLib.InteractiveControl();
            this.m_ToolDragItemSlider = new CommonLib.InteractiveControl();
            this.m_ToolDragItemNumUpDown = new CommonLib.InteractiveControl();
            this.m_ToolDragItemText = new CommonLib.InteractiveControl();
            this.m_ToolDragItemcombo = new CommonLib.InteractiveControl();
            this.m_ToolDragItemFilledRect = new CommonLib.TwoColorFilledRect();
            this.m_ToolDragItemFilledEllipse = new CommonLib.TwoColorFilledEllipse();
            this.labelStd = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labelStd
            // 
            this.labelStd.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right | System.Windows.Forms.AnchorStyles.Top)));
            this.labelStd.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.labelStd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelStd.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelStd.Location = new System.Drawing.Point(0, 3);
            this.labelStd.Name = "labelstd";
            this.labelStd.Size = new System.Drawing.Size(157, 21);
            this.labelStd.TabIndex = 1;
            this.labelStd.Text = "1 - Standard";
            // 
            // m_ToolDragItemBtn
            // 
            this.m_ToolDragItemBtn.AllowDrop = true;
            this.m_ToolDragItemBtn.ControlType = CommonLib.InteractiveControlType.Button;
            this.m_ToolDragItemBtn.Location = new System.Drawing.Point(15, 28);
            this.m_ToolDragItemBtn.Name = "m_ToolDragItemBtn";
            this.m_ToolDragItemBtn.Selected = false;
            this.m_ToolDragItemBtn.Size = new System.Drawing.Size(132, 20);
            this.m_ToolDragItemBtn.SourceBTControl = null;
            this.m_ToolDragItemBtn.TabIndex = 2;
            this.m_ToolDragItemBtn.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnItemMouseDown);
            // 
            // m_ToolDragItemCheckBox
            // 
            this.m_ToolDragItemCheckBox.AllowDrop = true;
            this.m_ToolDragItemCheckBox.ControlType = CommonLib.InteractiveControlType.CheckBox;
            this.m_ToolDragItemCheckBox.Location = new System.Drawing.Point(15, 54);
            this.m_ToolDragItemCheckBox.Name = "m_ToolDragItemCheckBox";
            this.m_ToolDragItemCheckBox.Selected = false;
            this.m_ToolDragItemCheckBox.Size = new System.Drawing.Size(132, 20);
            this.m_ToolDragItemCheckBox.SourceBTControl = null;
            this.m_ToolDragItemCheckBox.TabIndex = 3;
            this.m_ToolDragItemCheckBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnItemMouseDown);
            // 
            // m_ToolDragItemSlider
            // 
            this.m_ToolDragItemSlider.AllowDrop = true;
            this.m_ToolDragItemSlider.ControlType = CommonLib.InteractiveControlType.Slider;
            this.m_ToolDragItemSlider.Location = new System.Drawing.Point(15, 80);
            this.m_ToolDragItemSlider.Name = "m_ToolDragItemSlider";
            this.m_ToolDragItemSlider.Selected = false;
            this.m_ToolDragItemSlider.Size = new System.Drawing.Size(132, 40);
            this.m_ToolDragItemSlider.SourceBTControl = null;
            this.m_ToolDragItemSlider.TabIndex = 4;
            this.m_ToolDragItemSlider.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnItemMouseDown);
            // 
            // m_ToolDragItemNumUpDown
            // 
            this.m_ToolDragItemNumUpDown.AllowDrop = true;
            this.m_ToolDragItemNumUpDown.ControlType = CommonLib.InteractiveControlType.NumericUpDown;
            this.m_ToolDragItemNumUpDown.Location = new System.Drawing.Point(15, 126);
            this.m_ToolDragItemNumUpDown.Name = "m_ToolDragItemNumUpDown";
            this.m_ToolDragItemNumUpDown.Selected = false;
            this.m_ToolDragItemNumUpDown.Size = new System.Drawing.Size(132, 20);
            this.m_ToolDragItemNumUpDown.SourceBTControl = null;
            this.m_ToolDragItemNumUpDown.TabIndex = 5;
            this.m_ToolDragItemNumUpDown.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnItemMouseDown);
            // 
            // m_ToolDragItemText
            // 
            this.m_ToolDragItemText.AllowDrop = true;
            this.m_ToolDragItemText.ControlType = CommonLib.InteractiveControlType.Text;
            this.m_ToolDragItemText.Location = new System.Drawing.Point(15, 152);
            this.m_ToolDragItemText.Name = "m_ToolDragItemText";
            this.m_ToolDragItemText.Selected = false;
            this.m_ToolDragItemText.Size = new System.Drawing.Size(132, 20);
            this.m_ToolDragItemText.SourceBTControl = null;
            this.m_ToolDragItemText.TabIndex = 6;
            this.m_ToolDragItemText.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnItemMouseDown);
            // 
            // m_ToolDragItemcombo
            // 
            this.m_ToolDragItemcombo.AllowDrop = true;
            this.m_ToolDragItemcombo.ControlType = CommonLib.InteractiveControlType.Combo;
            this.m_ToolDragItemcombo.Location = new System.Drawing.Point(15, 178);
            this.m_ToolDragItemcombo.Name = "m_ToolDragItemcombo";
            this.m_ToolDragItemcombo.Selected = false;
            this.m_ToolDragItemcombo.Size = new System.Drawing.Size(132, 20);
            this.m_ToolDragItemcombo.SourceBTControl = null;
            this.m_ToolDragItemcombo.TabIndex = 7;
            this.m_ToolDragItemcombo.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnItemMouseDown);
            // 
            // m_ToolDragItemFilledRect
            // 
            this.m_ToolDragItemFilledRect.AllowDrop = true;
            this.m_ToolDragItemFilledRect.ControlType = CommonLib.InteractiveControlType.SpecificControl;
            this.m_ToolDragItemFilledRect.Location = new System.Drawing.Point(15, 201);
            this.m_ToolDragItemFilledRect.Name = "m_ToolDragItemFilledRect";
            this.m_ToolDragItemFilledRect.Selected = false;
            this.m_ToolDragItemFilledRect.Size = new System.Drawing.Size(132, 20);
            this.m_ToolDragItemFilledRect.SourceBTControl = null;
            this.m_ToolDragItemFilledRect.TabIndex = 8;
            this.m_ToolDragItemFilledRect.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnItemMouseDown);
            // 
            // m_ToolDragItemFilledEllipse
            // 
            this.m_ToolDragItemFilledEllipse.AllowDrop = true;
            this.m_ToolDragItemFilledEllipse.ControlType = CommonLib.InteractiveControlType.SpecificControl;
            this.m_ToolDragItemFilledEllipse.Location = new System.Drawing.Point(15, 224);
            this.m_ToolDragItemFilledEllipse.Name = "m_ToolDragItemFilledEllipse";
            this.m_ToolDragItemFilledEllipse.Selected = false;
            this.m_ToolDragItemFilledEllipse.Size = new System.Drawing.Size(100, 40);
            this.m_ToolDragItemFilledEllipse.SourceBTControl = null;
            this.m_ToolDragItemFilledEllipse.TabIndex = 9;
            this.m_ToolDragItemFilledEllipse.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnItemMouseDown);
            // 
            // DragItemPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.labelStd);
            this.Controls.Add(this.m_ToolDragItemBtn);
            this.Controls.Add(this.m_ToolDragItemCheckBox);
            this.Controls.Add(this.m_ToolDragItemSlider);
            this.Controls.Add(this.m_ToolDragItemNumUpDown);
            this.Controls.Add(this.m_ToolDragItemText);
            this.Controls.Add(this.m_ToolDragItemcombo);
            this.Controls.Add(this.m_ToolDragItemFilledRect);
            this.Controls.Add(this.m_ToolDragItemFilledEllipse);
            this.Name = "DragItemPanel";
            this.Size = new System.Drawing.Size(157, 215);
            this.ResumeLayout(false);

        }

        private CommonLib.InteractiveControl m_ToolDragItemBtn;
        private CommonLib.InteractiveControl m_ToolDragItemCheckBox;
        private CommonLib.InteractiveControl m_ToolDragItemSlider;
        private CommonLib.InteractiveControl m_ToolDragItemNumUpDown;
        private CommonLib.InteractiveControl m_ToolDragItemText;
        private CommonLib.InteractiveControl m_ToolDragItemcombo;
        private CommonLib.TwoColorFilledRect m_ToolDragItemFilledRect;
        private CommonLib.TwoColorFilledEllipse m_ToolDragItemFilledEllipse;
        private System.Windows.Forms.Label labelStd;


        #endregion
    }
}

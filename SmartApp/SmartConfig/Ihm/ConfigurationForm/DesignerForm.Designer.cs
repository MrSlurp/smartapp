using CommonLib;

namespace SmartApp.Ihm
{
    partial class DesignerForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DesignerForm));
            this.m_TopToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.m_ContentPanel = new System.Windows.Forms.ToolStripContentPanel();
            this.m_splitterDesigner_Tool = new System.Windows.Forms.SplitContainer();
            this.m_LabelSelectedScreen = new System.Windows.Forms.Label();
            this.m_toolStripDesignLayout = new System.Windows.Forms.ToolStrip();
            this.m_toolBtnAlignLeft = new System.Windows.Forms.ToolStripButton();
            this.m_toolBtnAlignTop = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.m_toolBtnMSWidth = new System.Windows.Forms.ToolStripButton();
            this.m_toolBtnMSHeight = new System.Windows.Forms.ToolStripButton();
            this.m_toolBtnMSSize = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.m_toolBtnArrangeAcross = new System.Windows.Forms.ToolStripButton();
            this.m_toolBtnArrangeDown = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.tsbtn_increaseWidth = new System.Windows.Forms.ToolStripMenuItem();
            this.tsbtn_decreaseWidth = new System.Windows.Forms.ToolStripMenuItem();
            this.tsbtn_increaseHeight = new System.Windows.Forms.ToolStripMenuItem();
            this.tsbtn_decreaseHeight = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripDropDownButton2 = new System.Windows.Forms.ToolStripDropDownButton();
            this.tsbtn_moveLeft = new System.Windows.Forms.ToolStripMenuItem();
            this.tsbtn_moveRight = new System.Windows.Forms.ToolStripMenuItem();
            this.tsbtn_moveUp = new System.Windows.Forms.ToolStripMenuItem();
            this.tsbtn_moveDown = new System.Windows.Forms.ToolStripMenuItem();
            this.m_toolBtnBringToFront = new System.Windows.Forms.ToolStripButton();
            this.tsbtn_copy = new System.Windows.Forms.ToolStripButton();
            this.tsbtn_paste = new System.Windows.Forms.ToolStripButton();
            this.toolbtnScreenToBitmap = new System.Windows.Forms.ToolStripButton();
            this.panel1 = new SmartApp.Ihm.InteractivePanelContainer();
            this.m_InteractiveControlContainer = new SmartApp.Ihm.Designer.InteractiveControlContainer();
            this.m_splitterDesigner_Tool.Panel1.SuspendLayout();
            this.m_splitterDesigner_Tool.Panel2.SuspendLayout();
            this.m_splitterDesigner_Tool.SuspendLayout();
            this.m_toolStripDesignLayout.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_TopToolStripPanel
            // 
            this.m_TopToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.m_TopToolStripPanel.Name = "m_TopToolStripPanel";
            this.m_TopToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.m_TopToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.m_TopToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // m_ContentPanel
            // 
            this.m_ContentPanel.Size = new System.Drawing.Size(729, 543);
            // 
            // m_splitterDesigner_Tool
            // 
            this.m_splitterDesigner_Tool.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.m_splitterDesigner_Tool.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_splitterDesigner_Tool.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.m_splitterDesigner_Tool.IsSplitterFixed = true;
            this.m_splitterDesigner_Tool.Location = new System.Drawing.Point(0, 0);
            this.m_splitterDesigner_Tool.Name = "m_splitterDesigner_Tool";
            this.m_splitterDesigner_Tool.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // m_splitterDesigner_Tool.Panel1
            // 
            this.m_splitterDesigner_Tool.Panel1.Controls.Add(this.m_LabelSelectedScreen);
            this.m_splitterDesigner_Tool.Panel1.Controls.Add(this.m_toolStripDesignLayout);
            // 
            // m_splitterDesigner_Tool.Panel2
            // 
            this.m_splitterDesigner_Tool.Panel2.BackColor = System.Drawing.Color.White;
            this.m_splitterDesigner_Tool.Panel2.Controls.Add(this.panel1);
            this.m_splitterDesigner_Tool.Panel2.Margin = new System.Windows.Forms.Padding(10);
            this.m_splitterDesigner_Tool.Size = new System.Drawing.Size(772, 567);
            this.m_splitterDesigner_Tool.TabIndex = 1;
            // 
            // m_LabelSelectedScreen
            // 
            this.m_LabelSelectedScreen.AutoSize = true;
            this.m_LabelSelectedScreen.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_LabelSelectedScreen.Location = new System.Drawing.Point(7, 29);
            this.m_LabelSelectedScreen.Name = "m_LabelSelectedScreen";
            this.m_LabelSelectedScreen.Size = new System.Drawing.Size(81, 16);
            this.m_LabelSelectedScreen.TabIndex = 1;
            this.m_LabelSelectedScreen.Text = "No Screen";
            // 
            // m_toolStripDesignLayout
            // 
            this.m_toolStripDesignLayout.BackColor = System.Drawing.SystemColors.Control;
            this.m_toolStripDesignLayout.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.m_toolStripDesignLayout.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_toolBtnAlignLeft,
            this.m_toolBtnAlignTop,
            this.toolStripSeparator1,
            this.m_toolBtnMSWidth,
            this.m_toolBtnMSHeight,
            this.m_toolBtnMSSize,
            this.toolStripSeparator2,
            this.m_toolBtnArrangeAcross,
            this.m_toolBtnArrangeDown,
            this.toolStripSeparator3,
            this.toolStripDropDownButton1,
            this.toolStripDropDownButton2,
            this.m_toolBtnBringToFront,
            this.tsbtn_copy,
            this.tsbtn_paste,
            this.toolbtnScreenToBitmap});
            this.m_toolStripDesignLayout.Location = new System.Drawing.Point(0, 0);
            this.m_toolStripDesignLayout.Name = "m_toolStripDesignLayout";
            this.m_toolStripDesignLayout.Size = new System.Drawing.Size(768, 25);
            this.m_toolStripDesignLayout.TabIndex = 0;
            this.m_toolStripDesignLayout.Text = "toolStrip1";
            // 
            // m_toolBtnAlignLeft
            // 
            this.m_toolBtnAlignLeft.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.m_toolBtnAlignLeft.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.m_toolBtnAlignLeft.Name = "m_toolBtnAlignLeft";
            this.m_toolBtnAlignLeft.Size = new System.Drawing.Size(23, 22);
            this.m_toolBtnAlignLeft.Text = "Align left";
            this.m_toolBtnAlignLeft.Click += new System.EventHandler(this.AlignLeftClick);
            // 
            // m_toolBtnAlignTop
            // 
            this.m_toolBtnAlignTop.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.m_toolBtnAlignTop.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.m_toolBtnAlignTop.Name = "m_toolBtnAlignTop";
            this.m_toolBtnAlignTop.Size = new System.Drawing.Size(23, 22);
            this.m_toolBtnAlignTop.Text = "Align top";
            this.m_toolBtnAlignTop.Click += new System.EventHandler(this.AlignTopClick);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // m_toolBtnMSWidth
            // 
            this.m_toolBtnMSWidth.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.m_toolBtnMSWidth.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.m_toolBtnMSWidth.Name = "m_toolBtnMSWidth";
            this.m_toolBtnMSWidth.Size = new System.Drawing.Size(23, 22);
            this.m_toolBtnMSWidth.Text = "Make same width";
            this.m_toolBtnMSWidth.Click += new System.EventHandler(this.MSWidthClick);
            // 
            // m_toolBtnMSHeight
            // 
            this.m_toolBtnMSHeight.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.m_toolBtnMSHeight.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.m_toolBtnMSHeight.Name = "m_toolBtnMSHeight";
            this.m_toolBtnMSHeight.Size = new System.Drawing.Size(23, 22);
            this.m_toolBtnMSHeight.Text = "Make same height";
            this.m_toolBtnMSHeight.Click += new System.EventHandler(this.MSHeightClick);
            // 
            // m_toolBtnMSSize
            // 
            this.m_toolBtnMSSize.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.m_toolBtnMSSize.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.m_toolBtnMSSize.Name = "m_toolBtnMSSize";
            this.m_toolBtnMSSize.Size = new System.Drawing.Size(23, 22);
            this.m_toolBtnMSSize.Text = "Make same size";
            this.m_toolBtnMSSize.Click += new System.EventHandler(this.MSSizeClick);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // m_toolBtnArrangeAcross
            // 
            this.m_toolBtnArrangeAcross.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.m_toolBtnArrangeAcross.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.m_toolBtnArrangeAcross.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.m_toolBtnArrangeAcross.Name = "m_toolBtnArrangeAcross";
            this.m_toolBtnArrangeAcross.Size = new System.Drawing.Size(23, 22);
            this.m_toolBtnArrangeAcross.Text = "Make horizonntal spacing equal";
            this.m_toolBtnArrangeAcross.Click += new System.EventHandler(this.m_toolBtnArrangeAcross_Click);
            // 
            // m_toolBtnArrangeDown
            // 
            this.m_toolBtnArrangeDown.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.m_toolBtnArrangeDown.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.m_toolBtnArrangeDown.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.m_toolBtnArrangeDown.Name = "m_toolBtnArrangeDown";
            this.m_toolBtnArrangeDown.Size = new System.Drawing.Size(23, 22);
            this.m_toolBtnArrangeDown.Text = "Make vertical spacing equal";
            this.m_toolBtnArrangeDown.Click += new System.EventHandler(this.m_toolBtnArrangeDown_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbtn_increaseWidth,
            this.tsbtn_decreaseWidth,
            this.tsbtn_increaseHeight,
            this.tsbtn_decreaseHeight});
            this.toolStripDropDownButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton1.Image")));
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(44, 22);
            this.toolStripDropDownButton1.Text = "Sizes";
            // 
            // tsbtn_increaseWidth
            // 
            this.tsbtn_increaseWidth.Name = "tsbtn_increaseWidth";
            this.tsbtn_increaseWidth.Size = new System.Drawing.Size(163, 22);
            this.tsbtn_increaseWidth.Text = "Increase width";
            this.tsbtn_increaseWidth.Click += new System.EventHandler(this.tsbtn_increaseWidth_Click);
            // 
            // tsbtn_decreaseWidth
            // 
            this.tsbtn_decreaseWidth.Name = "tsbtn_decreaseWidth";
            this.tsbtn_decreaseWidth.Size = new System.Drawing.Size(163, 22);
            this.tsbtn_decreaseWidth.Text = "Decrease width";
            this.tsbtn_decreaseWidth.Click += new System.EventHandler(this.tsbtn_decreaseWidth_Click);
            // 
            // tsbtn_increaseHeight
            // 
            this.tsbtn_increaseHeight.Name = "tsbtn_increaseHeight";
            this.tsbtn_increaseHeight.Size = new System.Drawing.Size(163, 22);
            this.tsbtn_increaseHeight.Text = "Increase height";
            this.tsbtn_increaseHeight.Click += new System.EventHandler(this.tsbtn_increaseHeight_Click);
            // 
            // tsbtn_decreaseHeight
            // 
            this.tsbtn_decreaseHeight.Name = "tsbtn_decreaseHeight";
            this.tsbtn_decreaseHeight.Size = new System.Drawing.Size(163, 22);
            this.tsbtn_decreaseHeight.Text = "Decrease height";
            this.tsbtn_decreaseHeight.Click += new System.EventHandler(this.tsbtn_decreaseHeight_Click);
            // 
            // toolStripDropDownButton2
            // 
            this.toolStripDropDownButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripDropDownButton2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbtn_moveLeft,
            this.tsbtn_moveRight,
            this.tsbtn_moveUp,
            this.tsbtn_moveDown});
            this.toolStripDropDownButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton2.Image")));
            this.toolStripDropDownButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton2.Name = "toolStripDropDownButton2";
            this.toolStripDropDownButton2.Size = new System.Drawing.Size(62, 22);
            this.toolStripDropDownButton2.Text = "Positions";
            // 
            // tsbtn_moveLeft
            // 
            this.tsbtn_moveLeft.Name = "tsbtn_moveLeft";
            this.tsbtn_moveLeft.Size = new System.Drawing.Size(152, 22);
            this.tsbtn_moveLeft.Text = "Move left";
            this.tsbtn_moveLeft.Click += new System.EventHandler(this.tsbtn_moveLeft_Click);
            // 
            // tsbtn_moveRight
            // 
            this.tsbtn_moveRight.Name = "tsbtn_moveRight";
            this.tsbtn_moveRight.Size = new System.Drawing.Size(152, 22);
            this.tsbtn_moveRight.Text = "Move right";
            this.tsbtn_moveRight.Click += new System.EventHandler(this.tsbtn_moveRight_Click);
            // 
            // tsbtn_moveUp
            // 
            this.tsbtn_moveUp.Name = "tsbtn_moveUp";
            this.tsbtn_moveUp.Size = new System.Drawing.Size(152, 22);
            this.tsbtn_moveUp.Text = "Move up";
            this.tsbtn_moveUp.Click += new System.EventHandler(this.tsbtn_moveUp_Click);
            // 
            // tsbtn_moveDown
            // 
            this.tsbtn_moveDown.Name = "tsbtn_moveDown";
            this.tsbtn_moveDown.Size = new System.Drawing.Size(152, 22);
            this.tsbtn_moveDown.Text = "Move down";
            this.tsbtn_moveDown.Click += new System.EventHandler(this.tsbtn_moveDown_Click);
            // 
            // m_toolBtnBringToFront
            // 
            this.m_toolBtnBringToFront.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.m_toolBtnBringToFront.Image = ((System.Drawing.Image)(resources.GetObject("m_toolBtnBringToFront.Image")));
            this.m_toolBtnBringToFront.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.m_toolBtnBringToFront.Name = "m_toolBtnBringToFront";
            this.m_toolBtnBringToFront.Size = new System.Drawing.Size(75, 22);
            this.m_toolBtnBringToFront.Text = "Bring to front";
            this.m_toolBtnBringToFront.Click += new System.EventHandler(this.m_toolBtnBringToFront_Click);
            // 
            // tsbtn_copy
            // 
            this.tsbtn_copy.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbtn_copy.Name = "tsbtn_copy";
            this.tsbtn_copy.Size = new System.Drawing.Size(36, 22);
            this.tsbtn_copy.Text = "Copy";
            this.tsbtn_copy.Click += new System.EventHandler(this.tsbtn_copy_click);
            // 
            // tsbtn_paste
            // 
            this.tsbtn_paste.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbtn_paste.Name = "tsbtn_paste";
            this.tsbtn_paste.Size = new System.Drawing.Size(38, 22);
            this.tsbtn_paste.Text = "Paste";
            this.tsbtn_paste.Click += new System.EventHandler(this.tsbtn_paste_click);
            // 
            // toolbtnScreenToBitmap
            // 
            this.toolbtnScreenToBitmap.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolbtnScreenToBitmap.Image = ((System.Drawing.Image)(resources.GetObject("toolbtnScreenToBitmap.Image")));
            this.toolbtnScreenToBitmap.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolbtnScreenToBitmap.Name = "toolbtnScreenToBitmap";
            this.toolbtnScreenToBitmap.Size = new System.Drawing.Size(83, 22);
            this.toolbtnScreenToBitmap.Text = "Save to bitmap";
            this.toolbtnScreenToBitmap.Click += new System.EventHandler(this.toolbtnScreenToBitmap_Click);
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.m_InteractiveControlContainer);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(768, 509);
            this.panel1.TabIndex = 9;
            // 
            // m_InteractiveControlContainer
            // 
            this.m_InteractiveControlContainer.BackColor = System.Drawing.SystemColors.Control;
            this.m_InteractiveControlContainer.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.m_InteractiveControlContainer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.m_InteractiveControlContainer.Location = new System.Drawing.Point(10, 11);
            this.m_InteractiveControlContainer.Margin = new System.Windows.Forms.Padding(0);
            this.m_InteractiveControlContainer.Name = "m_InteractiveControlContainer";
            this.m_InteractiveControlContainer.ScreenBckImage = null;
            this.m_InteractiveControlContainer.Size = new System.Drawing.Size(1920, 1210);
            this.m_InteractiveControlContainer.TabIndex = 9;
            // 
            // DesignerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(772, 567);
            this.Controls.Add(this.m_splitterDesigner_Tool);
            this.DoubleBuffered = true;
            this.MinimumSize = new System.Drawing.Size(300, 200);
            this.Name = "DesignerForm";
            this.ShowIcon = false;
            this.Text = "Screens configuration";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DesignerForm_FormClosing);
            this.m_splitterDesigner_Tool.Panel1.ResumeLayout(false);
            this.m_splitterDesigner_Tool.Panel1.PerformLayout();
            this.m_splitterDesigner_Tool.Panel2.ResumeLayout(false);
            this.m_splitterDesigner_Tool.ResumeLayout(false);
            this.m_toolStripDesignLayout.ResumeLayout(false);
            this.m_toolStripDesignLayout.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStripPanel m_TopToolStripPanel;
        private System.Windows.Forms.ToolStripContentPanel m_ContentPanel;
        private System.Windows.Forms.SplitContainer m_splitterDesigner_Tool;
        private System.Windows.Forms.Label m_LabelSelectedScreen;
        private System.Windows.Forms.ToolStrip m_toolStripDesignLayout;
        private System.Windows.Forms.ToolStripButton m_toolBtnAlignLeft;
        private System.Windows.Forms.ToolStripButton m_toolBtnAlignTop;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton m_toolBtnMSWidth;
        private System.Windows.Forms.ToolStripButton m_toolBtnMSHeight;
        private System.Windows.Forms.ToolStripButton m_toolBtnMSSize;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton m_toolBtnArrangeAcross;
        private System.Windows.Forms.ToolStripButton m_toolBtnArrangeDown;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripMenuItem tsbtn_increaseWidth;
        private System.Windows.Forms.ToolStripMenuItem tsbtn_decreaseWidth;
        private System.Windows.Forms.ToolStripMenuItem tsbtn_increaseHeight;
        private System.Windows.Forms.ToolStripMenuItem tsbtn_decreaseHeight;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton2;
        private System.Windows.Forms.ToolStripMenuItem tsbtn_moveLeft;
        private System.Windows.Forms.ToolStripMenuItem tsbtn_moveRight;
        private System.Windows.Forms.ToolStripMenuItem tsbtn_moveUp;
        private System.Windows.Forms.ToolStripMenuItem tsbtn_moveDown;
        private System.Windows.Forms.ToolStripButton m_toolBtnBringToFront;
        private System.Windows.Forms.ToolStripButton tsbtn_copy;
        private System.Windows.Forms.ToolStripButton tsbtn_paste;
        private System.Windows.Forms.ToolStripButton toolbtnScreenToBitmap;
        private InteractivePanelContainer panel1;
        private SmartApp.Ihm.Designer.InteractiveControlContainer m_InteractiveControlContainer;

    }
}
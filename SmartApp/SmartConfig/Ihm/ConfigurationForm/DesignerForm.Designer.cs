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
            this.m_TopToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.m_ContentPanel = new System.Windows.Forms.ToolStripContentPanel();
            this.m_MainSplitterContainer = new System.Windows.Forms.SplitContainer();
            this.m_tabCTrlConfig = new System.Windows.Forms.TabControl();
            this.m_TabScreen = new System.Windows.Forms.TabPage();
            this.m_PanelScreenListAndProp = new SmartApp.Ihm.ScreenPropPanel();
            this.m_TabTools = new System.Windows.Forms.TabPage();
            this.m_panelToolDragItem = new SmartApp.Ihm.Designer.DragItemPanel();
            this.m_TabItemOption = new System.Windows.Forms.TabPage();
            this.m_PanelControlProperties = new SmartApp.Ihm.ScreenItemPropertiesControl();
            this.m_TabScript = new System.Windows.Forms.TabPage();
            this.m_PanelScreenInitScript = new SmartApp.Ihm.ScriptControl();
            this.m_PanelScreenEventScript = new SmartApp.Ihm.ScriptControl();
            this.m_PanelCtrlEventScript = new SmartApp.Ihm.ScriptControl();
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
            this.m_InteractiveControlContainer = new SmartApp.Ihm.Designer.InteractiveControlContainer();
            this.m_MainSplitterContainer.Panel1.SuspendLayout();
            this.m_MainSplitterContainer.Panel2.SuspendLayout();
            this.m_MainSplitterContainer.SuspendLayout();
            this.m_tabCTrlConfig.SuspendLayout();
            this.m_TabScreen.SuspendLayout();
            this.m_TabTools.SuspendLayout();
            this.m_TabItemOption.SuspendLayout();
            this.m_TabScript.SuspendLayout();
            this.m_splitterDesigner_Tool.Panel1.SuspendLayout();
            this.m_splitterDesigner_Tool.Panel2.SuspendLayout();
            this.m_splitterDesigner_Tool.SuspendLayout();
            this.m_toolStripDesignLayout.SuspendLayout();
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
            // m_MainSplitterContainer
            // 
            this.m_MainSplitterContainer.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.m_MainSplitterContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_MainSplitterContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.m_MainSplitterContainer.Location = new System.Drawing.Point(0, 0);
            this.m_MainSplitterContainer.MinimumSize = new System.Drawing.Size(600, 500);
            this.m_MainSplitterContainer.Name = "m_MainSplitterContainer";
            // 
            // m_MainSplitterContainer.Panel1
            // 
            this.m_MainSplitterContainer.Panel1.Controls.Add(this.m_tabCTrlConfig);
            this.m_MainSplitterContainer.Panel1MinSize = 250;
            // 
            // m_MainSplitterContainer.Panel2
            // 
            this.m_MainSplitterContainer.Panel2.Controls.Add(this.m_splitterDesigner_Tool);
            this.m_MainSplitterContainer.Panel2MinSize = 250;
            this.m_MainSplitterContainer.Size = new System.Drawing.Size(772, 567);
            this.m_MainSplitterContainer.SplitterDistance = 293;
            this.m_MainSplitterContainer.TabIndex = 6;
            // 
            // m_tabCTrlConfig
            // 
            this.m_tabCTrlConfig.Controls.Add(this.m_TabScreen);
            this.m_tabCTrlConfig.Controls.Add(this.m_TabTools);
            this.m_tabCTrlConfig.Controls.Add(this.m_TabItemOption);
            this.m_tabCTrlConfig.Controls.Add(this.m_TabScript);
            this.m_tabCTrlConfig.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_tabCTrlConfig.Location = new System.Drawing.Point(0, 0);
            this.m_tabCTrlConfig.Margin = new System.Windows.Forms.Padding(0);
            this.m_tabCTrlConfig.MinimumSize = new System.Drawing.Size(250, 500);
            this.m_tabCTrlConfig.Multiline = true;
            this.m_tabCTrlConfig.Name = "m_tabCTrlConfig";
            this.m_tabCTrlConfig.SelectedIndex = 0;
            this.m_tabCTrlConfig.Size = new System.Drawing.Size(289, 563);
            this.m_tabCTrlConfig.TabIndex = 5;
            // 
            // m_TabScreen
            // 
            this.m_TabScreen.Controls.Add(this.m_PanelScreenListAndProp);
            this.m_TabScreen.Location = new System.Drawing.Point(4, 22);
            this.m_TabScreen.Name = "m_TabScreen";
            this.m_TabScreen.Padding = new System.Windows.Forms.Padding(3);
            this.m_TabScreen.Size = new System.Drawing.Size(281, 537);
            this.m_TabScreen.TabIndex = 0;
            this.m_TabScreen.Text = "Screens";
            this.m_TabScreen.UseVisualStyleBackColor = true;
            // 
            // m_PanelScreenListAndProp
            // 
            this.m_PanelScreenListAndProp.BackColor = System.Drawing.Color.Transparent;
            this.m_PanelScreenListAndProp.Doc = null;
            this.m_PanelScreenListAndProp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_PanelScreenListAndProp.Location = new System.Drawing.Point(3, 3);
            this.m_PanelScreenListAndProp.Name = "m_PanelScreenListAndProp";
            this.m_PanelScreenListAndProp.Size = new System.Drawing.Size(275, 531);
            this.m_PanelScreenListAndProp.TabIndex = 0;
            // 
            // m_TabTools
            // 
            this.m_TabTools.Controls.Add(this.m_panelToolDragItem);
            this.m_TabTools.Location = new System.Drawing.Point(4, 22);
            this.m_TabTools.Name = "m_TabTools";
            this.m_TabTools.Padding = new System.Windows.Forms.Padding(3);
            this.m_TabTools.Size = new System.Drawing.Size(281, 537);
            this.m_TabTools.TabIndex = 1;
            this.m_TabTools.Text = "Tools";
            this.m_TabTools.UseVisualStyleBackColor = true;
            // 
            // m_panelToolDragItem
            // 
            this.m_panelToolDragItem.AutoScroll = true;
            this.m_panelToolDragItem.BackColor = System.Drawing.Color.Transparent;
            this.m_panelToolDragItem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_panelToolDragItem.Location = new System.Drawing.Point(3, 3);
            this.m_panelToolDragItem.Margin = new System.Windows.Forms.Padding(0);
            this.m_panelToolDragItem.Name = "m_panelToolDragItem";
            this.m_panelToolDragItem.Size = new System.Drawing.Size(275, 531);
            this.m_panelToolDragItem.TabIndex = 1;
            // 
            // m_TabItemOption
            // 
            this.m_TabItemOption.BackColor = System.Drawing.Color.Transparent;
            this.m_TabItemOption.Controls.Add(this.m_PanelControlProperties);
            this.m_TabItemOption.Location = new System.Drawing.Point(4, 22);
            this.m_TabItemOption.Name = "m_TabItemOption";
            this.m_TabItemOption.Padding = new System.Windows.Forms.Padding(3);
            this.m_TabItemOption.Size = new System.Drawing.Size(281, 521);
            this.m_TabItemOption.TabIndex = 0;
            this.m_TabItemOption.Text = "Control Options";
            this.m_TabItemOption.UseVisualStyleBackColor = true;
            // 
            // m_PanelControlProperties
            // 
            this.m_PanelControlProperties.AssociateData = "";
            this.m_PanelControlProperties.BackColor = System.Drawing.Color.Transparent;
            this.m_PanelControlProperties.BTControl = null;
            this.m_PanelControlProperties.Description = "";
            this.m_PanelControlProperties.Doc = null;
            this.m_PanelControlProperties.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_PanelControlProperties.GestControl = null;
            this.m_PanelControlProperties.IsReadOnly = false;
            this.m_PanelControlProperties.Location = new System.Drawing.Point(3, 3);
            this.m_PanelControlProperties.Margin = new System.Windows.Forms.Padding(0);
            this.m_PanelControlProperties.Name = "m_PanelControlProperties";
            this.m_PanelControlProperties.Size = new System.Drawing.Size(275, 515);
            this.m_PanelControlProperties.Symbol = "";
            this.m_PanelControlProperties.TabIndex = 1;
            this.m_PanelControlProperties.Txt = "";
            this.m_PanelControlProperties.UseScreenEvent = false;
            // 
            // m_TabScript
            // 
            this.m_TabScript.BackColor = System.Drawing.Color.Transparent;
            this.m_TabScript.Controls.Add(this.m_PanelScreenInitScript);
            this.m_TabScript.Controls.Add(this.m_PanelScreenEventScript);
            this.m_TabScript.Controls.Add(this.m_PanelCtrlEventScript);
            this.m_TabScript.Location = new System.Drawing.Point(4, 22);
            this.m_TabScript.Name = "m_TabScript";
            this.m_TabScript.Padding = new System.Windows.Forms.Padding(3);
            this.m_TabScript.Size = new System.Drawing.Size(281, 521);
            this.m_TabScript.TabIndex = 1;
            this.m_TabScript.Text = "Item Script";
            this.m_TabScript.UseVisualStyleBackColor = true;
            // 
            // m_PanelScreenInitScript
            // 
            this.m_PanelScreenInitScript.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_PanelScreenInitScript.BackColor = System.Drawing.Color.Transparent;
            this.m_PanelScreenInitScript.Doc = null;
            this.m_PanelScreenInitScript.InitScriptableItem = null;
            this.m_PanelScreenInitScript.Location = new System.Drawing.Point(7, 7);
            this.m_PanelScreenInitScript.Name = "m_PanelScreenInitScript";
            this.m_PanelScreenInitScript.ScriptableItem = null;
            this.m_PanelScreenInitScript.ScriptLines = new string[0];
            this.m_PanelScreenInitScript.Size = new System.Drawing.Size(268, 151);
            this.m_PanelScreenInitScript.TabIndex = 0;
            this.m_PanelScreenInitScript.Title = "Title";
            // 
            // m_PanelScreenEventScript
            // 
            this.m_PanelScreenEventScript.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_PanelScreenEventScript.BackColor = System.Drawing.Color.Transparent;
            this.m_PanelScreenEventScript.Doc = null;
            this.m_PanelScreenEventScript.InitScriptableItem = null;
            this.m_PanelScreenEventScript.Location = new System.Drawing.Point(7, 164);
            this.m_PanelScreenEventScript.Name = "m_PanelScreenEventScript";
            this.m_PanelScreenEventScript.ScriptableItem = null;
            this.m_PanelScreenEventScript.ScriptLines = new string[0];
            this.m_PanelScreenEventScript.Size = new System.Drawing.Size(268, 151);
            this.m_PanelScreenEventScript.TabIndex = 0;
            this.m_PanelScreenEventScript.Title = "Title";
            // 
            // m_PanelCtrlEventScript
            // 
            this.m_PanelCtrlEventScript.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_PanelCtrlEventScript.BackColor = System.Drawing.Color.Transparent;
            this.m_PanelCtrlEventScript.Doc = null;
            this.m_PanelCtrlEventScript.InitScriptableItem = null;
            this.m_PanelCtrlEventScript.Location = new System.Drawing.Point(7, 321);
            this.m_PanelCtrlEventScript.Name = "m_PanelCtrlEventScript";
            this.m_PanelCtrlEventScript.ScriptableItem = null;
            this.m_PanelCtrlEventScript.ScriptLines = new string[0];
            this.m_PanelCtrlEventScript.Size = new System.Drawing.Size(268, 151);
            this.m_PanelCtrlEventScript.TabIndex = 0;
            this.m_PanelCtrlEventScript.Title = "Title";
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
            this.m_splitterDesigner_Tool.Panel2.AutoScroll = true;
            this.m_splitterDesigner_Tool.Panel2.AutoScrollMinSize = new System.Drawing.Size(1034, 778);
            this.m_splitterDesigner_Tool.Panel2.BackColor = System.Drawing.Color.White;
            this.m_splitterDesigner_Tool.Panel2.Controls.Add(this.m_InteractiveControlContainer);
            this.m_splitterDesigner_Tool.Panel2.Margin = new System.Windows.Forms.Padding(10);
            this.m_splitterDesigner_Tool.Size = new System.Drawing.Size(475, 567);
            this.m_splitterDesigner_Tool.TabIndex = 0;
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
            this.m_toolBtnArrangeDown});
            this.m_toolStripDesignLayout.Location = new System.Drawing.Point(0, 0);
            this.m_toolStripDesignLayout.Name = "m_toolStripDesignLayout";
            this.m_toolStripDesignLayout.Size = new System.Drawing.Size(471, 25);
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
            // m_InteractiveControlContainer
            // 
            this.m_InteractiveControlContainer.BackColor = System.Drawing.SystemColors.Control;
            this.m_InteractiveControlContainer.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.m_InteractiveControlContainer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.m_InteractiveControlContainer.Location = new System.Drawing.Point(10, 10);
            this.m_InteractiveControlContainer.Margin = new System.Windows.Forms.Padding(0);
            this.m_InteractiveControlContainer.Name = "m_InteractiveControlContainer";
            this.m_InteractiveControlContainer.ScreenBckImage = null;
            this.m_InteractiveControlContainer.Size = new System.Drawing.Size(1024, 768);
            this.m_InteractiveControlContainer.TabIndex = 8;
            this.m_InteractiveControlContainer.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnDesignerKeyDown);
            // 
            // DesignerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(772, 567);
            this.Controls.Add(this.m_MainSplitterContainer);
            this.DoubleBuffered = true;
            this.MinimumSize = new System.Drawing.Size(620, 530);
            this.Name = "DesignerForm";
            this.ShowIcon = false;
            this.Text = "Screens configuration";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OnFormClosing);
            this.m_MainSplitterContainer.Panel1.ResumeLayout(false);
            this.m_MainSplitterContainer.Panel2.ResumeLayout(false);
            this.m_MainSplitterContainer.ResumeLayout(false);
            this.m_tabCTrlConfig.ResumeLayout(false);
            this.m_TabScreen.ResumeLayout(false);
            this.m_TabTools.ResumeLayout(false);
            this.m_TabItemOption.ResumeLayout(false);
            this.m_TabScript.ResumeLayout(false);
            this.m_splitterDesigner_Tool.Panel1.ResumeLayout(false);
            this.m_splitterDesigner_Tool.Panel1.PerformLayout();
            this.m_splitterDesigner_Tool.Panel2.ResumeLayout(false);
            this.m_splitterDesigner_Tool.ResumeLayout(false);
            this.m_toolStripDesignLayout.ResumeLayout(false);
            this.m_toolStripDesignLayout.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStripPanel m_TopToolStripPanel;
        private System.Windows.Forms.ToolStripContentPanel m_ContentPanel;
        private System.Windows.Forms.SplitContainer m_MainSplitterContainer;
        private System.Windows.Forms.TabControl m_tabCTrlConfig;
        private System.Windows.Forms.TabPage m_TabScreen;
        private System.Windows.Forms.TabPage m_TabTools;
        private System.Windows.Forms.TabPage m_TabItemOption;
        private System.Windows.Forms.TabPage m_TabScript;
        private System.Windows.Forms.SplitContainer m_splitterDesigner_Tool;
        private System.Windows.Forms.ToolStrip m_toolStripDesignLayout;
        private System.Windows.Forms.ToolStripButton m_toolBtnAlignLeft;
        private System.Windows.Forms.ToolStripButton m_toolBtnAlignTop;
        private System.Windows.Forms.ToolStripButton m_toolBtnMSWidth;
        private System.Windows.Forms.ToolStripButton m_toolBtnMSHeight;
        private System.Windows.Forms.ToolStripButton m_toolBtnMSSize;
        private SmartApp.Ihm.Designer.DragItemPanel m_panelToolDragItem;
        private SmartApp.Ihm.Designer.InteractiveControlContainer m_InteractiveControlContainer;
        private ScreenItemPropertiesControl m_PanelControlProperties;
        private ScreenPropPanel m_PanelScreenListAndProp;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private ScriptControl m_PanelScreenInitScript;
        private ScriptControl m_PanelScreenEventScript;
        private ScriptControl m_PanelCtrlEventScript;
        private System.Windows.Forms.Label m_LabelSelectedScreen;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton m_toolBtnArrangeAcross;
        private System.Windows.Forms.ToolStripButton m_toolBtnArrangeDown;

    }
}
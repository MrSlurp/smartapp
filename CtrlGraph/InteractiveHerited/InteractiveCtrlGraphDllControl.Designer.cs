namespace CtrlGraph
{
    partial class InteractiveCtrlGraphDllControl
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
            components = new System.ComponentModel.Container();
            this.zedGraphControl = new ZedGraph.ZedGraphControl();
            this.SuspendLayout();
            // 
            // zedGraphControl1
            // 
            this.zedGraphControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.zedGraphControl.Location = new System.Drawing.Point(0, 0);
            this.zedGraphControl.Name = "zedGraphControl";
            this.zedGraphControl.Enabled = false;
            this.zedGraphControl.IsEnableHPan = false;
            this.zedGraphControl.IsEnableHZoom = false;
            this.zedGraphControl.IsEnableVPan = false;
            this.zedGraphControl.IsEnableVZoom = false;
            this.zedGraphControl.IsEnableWheelZoom = false;
            this.zedGraphControl.IsPrintFillPage = false;
            this.zedGraphControl.IsPrintKeepAspectRatio = false;
            this.zedGraphControl.IsPrintScaleAll = false;
            this.zedGraphControl.IsShowContextMenu = false;
            this.zedGraphControl.IsShowCopyMessage = false;
            this.zedGraphControl.ScrollGrace = 0;
            this.zedGraphControl.ScrollMaxX = 0;
            this.zedGraphControl.ScrollMaxY = 0;
            this.zedGraphControl.ScrollMaxY2 = 0;
            this.zedGraphControl.ScrollMinX = 0;
            this.zedGraphControl.ScrollMinY = 0;
            this.zedGraphControl.ScrollMinY2 = 0;
            this.zedGraphControl.Size = this.ClientRectangle.Size;
            this.zedGraphControl.TabIndex = 0;
            this.zedGraphControl.Visible = false;
            
            this.Controls.Add(this.zedGraphControl);
            this.ResumeLayout(false);
        }

        #endregion

        private ZedGraph.ZedGraphControl zedGraphControl;

    }
}

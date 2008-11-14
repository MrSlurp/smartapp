namespace CommonLib
{
    partial class InteractiveControl
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
            this.SuspendLayout();
            // 
            // InteractiveControl
            // 
            this.AllowDrop = true;
            this.DoubleBuffered = true;
            this.Name = "InteractiveControl";
            this.DragOver += new System.Windows.Forms.DragEventHandler(this.InteractiveControl_DragOver);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.InteractiveControl_DragDrop);
            this.DragLeave += new System.EventHandler(this.InteractiveControl_DragLeave);
            this.ResumeLayout(false);

        }

        #endregion
    }
}

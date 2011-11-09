namespace ImageButton
{
    partial class InteractiveImageButtonDllControl
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
            this.imageButtonDispCtrl1 = new ImageButton.ImageButtonDispCtrl();
            this.SuspendLayout();
            // 
            // imageButtonDispCtrl1
            // 
            this.imageButtonDispCtrl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imageButtonDispCtrl1.Location = new System.Drawing.Point(0, 0);
            this.imageButtonDispCtrl1.Name = "imageButtonDispCtrl1";
            this.imageButtonDispCtrl1.Size = new System.Drawing.Size(155, 131);
            this.imageButtonDispCtrl1.TabIndex = 0;
            // 
            // InteractiveImageButtonDllControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.imageButtonDispCtrl1);
            this.Name = "InteractiveImageButtonDllControl";
            this.Size = new System.Drawing.Size(155, 131);
            this.ResumeLayout(false);

        }

        #endregion

        private ImageButtonDispCtrl imageButtonDispCtrl1;
    }
}

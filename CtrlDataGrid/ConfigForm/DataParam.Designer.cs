namespace CtrlDataGrid
{
    partial class DataParam
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
            this.btnPickData1 = new System.Windows.Forms.Button();
            this.edtAlias1 = new System.Windows.Forms.TextBox();
            this.edtSymb1 = new System.Windows.Forms.TextBox();
            this.lblAlias1 = new System.Windows.Forms.Label();
            this.lblSymb1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnPickData1
            // 
            this.btnPickData1.Location = new System.Drawing.Point(152, 18);
            this.btnPickData1.Name = "btnPickData1";
            this.btnPickData1.Size = new System.Drawing.Size(75, 23);
            this.btnPickData1.TabIndex = 25;
            this.btnPickData1.Text = "Pick";
            this.btnPickData1.UseVisualStyleBackColor = true;
            this.btnPickData1.Click += new System.EventHandler(this.btnPickData1_Click);
            // 
            // edtAlias1
            // 
            this.edtAlias1.Location = new System.Drawing.Point(233, 20);
            this.edtAlias1.Name = "edtAlias1";
            this.edtAlias1.Size = new System.Drawing.Size(115, 20);
            this.edtAlias1.TabIndex = 26;
            // 
            // edtSymb1
            // 
            this.edtSymb1.Location = new System.Drawing.Point(3, 21);
            this.edtSymb1.Name = "edtSymb1";
            this.edtSymb1.ReadOnly = true;
            this.edtSymb1.Size = new System.Drawing.Size(143, 20);
            this.edtSymb1.TabIndex = 28;
            this.edtSymb1.TabStop = false;
            // 
            // lblAlias1
            // 
            this.lblAlias1.AutoSize = true;
            this.lblAlias1.Location = new System.Drawing.Point(233, 4);
            this.lblAlias1.Name = "lblAlias1";
            this.lblAlias1.Size = new System.Drawing.Size(64, 13);
            this.lblAlias1.TabIndex = 21;
            this.lblAlias1.Text = "Data n Alias";
            // 
            // lblSymb1
            // 
            this.lblSymb1.AutoSize = true;
            this.lblSymb1.Location = new System.Drawing.Point(3, 4);
            this.lblSymb1.Name = "lblSymb1";
            this.lblSymb1.Size = new System.Drawing.Size(76, 13);
            this.lblSymb1.TabIndex = 23;
            this.lblSymb1.Text = "Data n Symbol";
            // 
            // DataParam
            // 
            this.Controls.Add(this.btnPickData1);
            this.Controls.Add(this.edtAlias1);
            this.Controls.Add(this.edtSymb1);
            this.Controls.Add(this.lblAlias1);
            this.Controls.Add(this.lblSymb1);
            this.Name = "DataParam";
            this.Size = new System.Drawing.Size(370, 43);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnPickData1;
        private System.Windows.Forms.TextBox edtAlias1;
        private System.Windows.Forms.TextBox edtSymb1;
        private System.Windows.Forms.Label lblAlias1;
        private System.Windows.Forms.Label lblSymb1;
    }
}

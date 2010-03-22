namespace CtrlGraph
{
    partial class CurveParam
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
            this.bntPickColor1 = new System.Windows.Forms.Button();
            this.btnPickData1 = new System.Windows.Forms.Button();
            this.edtColor1 = new System.Windows.Forms.TextBox();
            this.edtAlias1 = new System.Windows.Forms.TextBox();
            this.lblColor1 = new System.Windows.Forms.Label();
            this.edtSymb1 = new System.Windows.Forms.TextBox();
            this.lblAlias1 = new System.Windows.Forms.Label();
            this.lblSymb1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // bntPickColor1
            // 
            this.bntPickColor1.Location = new System.Drawing.Point(401, 17);
            this.bntPickColor1.Name = "bntPickColor1";
            this.bntPickColor1.Size = new System.Drawing.Size(75, 23);
            this.bntPickColor1.TabIndex = 27;
            this.bntPickColor1.Text = "Pick";
            this.bntPickColor1.UseVisualStyleBackColor = true;
            this.bntPickColor1.Click += new System.EventHandler(this.bntPickColor1_Click);
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
            // edtColor1
            // 
            this.edtColor1.Location = new System.Drawing.Point(354, 20);
            this.edtColor1.Name = "edtColor1";
            this.edtColor1.ReadOnly = true;
            this.edtColor1.Size = new System.Drawing.Size(40, 20);
            this.edtColor1.TabIndex = 24;
            this.edtColor1.TabStop = false;
            // 
            // edtAlias1
            // 
            this.edtAlias1.Location = new System.Drawing.Point(233, 20);
            this.edtAlias1.Name = "edtAlias1";
            this.edtAlias1.Size = new System.Drawing.Size(115, 20);
            this.edtAlias1.TabIndex = 26;
            // 
            // lblColor1
            // 
            this.lblColor1.AutoSize = true;
            this.lblColor1.Location = new System.Drawing.Point(340, 4);
            this.lblColor1.Name = "lblColor1";
            this.lblColor1.Size = new System.Drawing.Size(62, 13);
            this.lblColor1.TabIndex = 22;
            this.lblColor1.Text = "Curve Color";
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
            // CurveParam
            // 
            this.Controls.Add(this.bntPickColor1);
            this.Controls.Add(this.btnPickData1);
            this.Controls.Add(this.edtColor1);
            this.Controls.Add(this.edtAlias1);
            this.Controls.Add(this.lblColor1);
            this.Controls.Add(this.edtSymb1);
            this.Controls.Add(this.lblAlias1);
            this.Controls.Add(this.lblSymb1);
            this.Name = "CurveParam";
            this.Size = new System.Drawing.Size(480, 43);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button bntPickColor1;
        private System.Windows.Forms.Button btnPickData1;
        private System.Windows.Forms.TextBox edtColor1;
        private System.Windows.Forms.TextBox edtAlias1;
        private System.Windows.Forms.Label lblColor1;
        private System.Windows.Forms.TextBox edtSymb1;
        private System.Windows.Forms.Label lblAlias1;
        private System.Windows.Forms.Label lblSymb1;
    }
}

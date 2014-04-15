namespace phdesign.NppToolBucket.Forms
{
    partial class GuidGeneratorForm
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonGenerate = new System.Windows.Forms.Button();
            this.checkBoxHyphens = new System.Windows.Forms.CheckBox();
            this.checkBoxBraces = new System.Windows.Forms.CheckBox();
            this.checkBoxUppercase = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.numericHowMany = new System.Windows.Forms.NumericUpDown();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericHowMany)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.buttonGenerate);
            this.panel1.Controls.Add(this.checkBoxHyphens);
            this.panel1.Controls.Add(this.checkBoxBraces);
            this.panel1.Controls.Add(this.checkBoxUppercase);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.numericHowMany);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(279, 87);
            this.panel1.TabIndex = 0;
            // 
            // buttonGenerate
            // 
            this.buttonGenerate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonGenerate.Location = new System.Drawing.Point(192, 52);
            this.buttonGenerate.Name = "buttonGenerate";
            this.buttonGenerate.Size = new System.Drawing.Size(75, 23);
            this.buttonGenerate.TabIndex = 5;
            this.buttonGenerate.Text = "Generate";
            this.buttonGenerate.UseVisualStyleBackColor = true;
            this.buttonGenerate.Click += new System.EventHandler(this.buttonGenerate_Click);
            // 
            // checkBoxHyphens
            // 
            this.checkBoxHyphens.AutoSize = true;
            this.checkBoxHyphens.Checked = true;
            this.checkBoxHyphens.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxHyphens.Location = new System.Drawing.Point(12, 58);
            this.checkBoxHyphens.Name = "checkBoxHyphens";
            this.checkBoxHyphens.Size = new System.Drawing.Size(104, 17);
            this.checkBoxHyphens.TabIndex = 4;
            this.checkBoxHyphens.Text = "Include hyphens";
            this.checkBoxHyphens.UseVisualStyleBackColor = true;
            // 
            // checkBoxBraces
            // 
            this.checkBoxBraces.AutoSize = true;
            this.checkBoxBraces.Location = new System.Drawing.Point(12, 34);
            this.checkBoxBraces.Name = "checkBoxBraces";
            this.checkBoxBraces.Size = new System.Drawing.Size(114, 17);
            this.checkBoxBraces.TabIndex = 3;
            this.checkBoxBraces.Text = "Include braces \'{ }\'";
            this.checkBoxBraces.UseVisualStyleBackColor = true;
            // 
            // checkBoxUppercase
            // 
            this.checkBoxUppercase.AutoSize = true;
            this.checkBoxUppercase.Location = new System.Drawing.Point(12, 10);
            this.checkBoxUppercase.Name = "checkBoxUppercase";
            this.checkBoxUppercase.Size = new System.Drawing.Size(98, 17);
            this.checkBoxUppercase.TabIndex = 2;
            this.checkBoxUppercase.Text = "Use uppercase";
            this.checkBoxUppercase.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(151, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "How many?";
            // 
            // numericHowMany
            // 
            this.numericHowMany.Location = new System.Drawing.Point(220, 12);
            this.numericHowMany.Name = "numericHowMany";
            this.numericHowMany.Size = new System.Drawing.Size(45, 20);
            this.numericHowMany.TabIndex = 0;
            this.numericHowMany.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // GuidGeneratorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(279, 87);
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GuidGeneratorForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Generate GUIDs";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericHowMany)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.NumericUpDown numericHowMany;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox checkBoxUppercase;
        private System.Windows.Forms.Button buttonGenerate;
        private System.Windows.Forms.CheckBox checkBoxHyphens;
        private System.Windows.Forms.CheckBox checkBoxBraces;
    }
}
namespace SymbolicFormat
{
    partial class MainForm
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
            this.formatButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.outputTextBox = new System.Windows.Forms.TextBox();
            this.boxDivisor2 = new SymbolicFormat.BoxDivisor();
            this.boxOperator = new SymbolicFormat.BoxOperator();
            this.boxDivisor1 = new SymbolicFormat.BoxDivisor();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // formatButton
            // 
            this.formatButton.Location = new System.Drawing.Point(300, 168);
            this.formatButton.Name = "formatButton";
            this.formatButton.Size = new System.Drawing.Size(75, 23);
            this.formatButton.TabIndex = 3;
            this.formatButton.Text = "Format";
            this.formatButton.UseVisualStyleBackColor = true;
            this.formatButton.Click += new System.EventHandler(this.FormatButtonClick);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.outputTextBox);
            this.groupBox1.Location = new System.Drawing.Point(12, 197);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(363, 174);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Output";
            // 
            // outputTextBox
            // 
            this.outputTextBox.Location = new System.Drawing.Point(7, 20);
            this.outputTextBox.Multiline = true;
            this.outputTextBox.Name = "outputTextBox";
            this.outputTextBox.Size = new System.Drawing.Size(350, 148);
            this.outputTextBox.TabIndex = 0;
            // 
            // boxDivisor2
            // 
            this.boxDivisor2.Location = new System.Drawing.Point(220, 42);
            this.boxDivisor2.Name = "boxDivisor2";
            this.boxDivisor2.Size = new System.Drawing.Size(114, 89);
            this.boxDivisor2.TabIndex = 2;
            // 
            // boxOperator
            // 
            this.boxOperator.BoxOperatorValue = "";
            this.boxOperator.Location = new System.Drawing.Point(161, 74);
            this.boxOperator.Name = "boxOperator";
            this.boxOperator.Size = new System.Drawing.Size(53, 29);
            this.boxOperator.TabIndex = 1;
            // 
            // boxDivisor1
            // 
            this.boxDivisor1.Location = new System.Drawing.Point(41, 42);
            this.boxDivisor1.Name = "boxDivisor1";
            this.boxDivisor1.Size = new System.Drawing.Size(114, 89);
            this.boxDivisor1.TabIndex = 0;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(387, 383);
            this.Controls.Add(this.boxDivisor2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.boxOperator);
            this.Controls.Add(this.boxDivisor1);
            this.Controls.Add(this.formatButton);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.Text = "Symbolic Formatter";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button formatButton;
        private BoxDivisor boxDivisor1;
        private BoxOperator boxOperator;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox outputTextBox;
        private BoxDivisor boxDivisor2;
    }
}


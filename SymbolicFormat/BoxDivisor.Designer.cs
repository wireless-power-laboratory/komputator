namespace SymbolicFormat
{
    partial class BoxDivisor
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
            this.boxOperatorNumerator = new SymbolicFormat.BoxOperator();
            this.label1 = new System.Windows.Forms.Label();
            this.boxOperatorDenominator = new SymbolicFormat.BoxOperator();
            this.SuspendLayout();
            // 
            // boxOperatorNumerator
            // 
            this.boxOperatorNumerator.Location = new System.Drawing.Point(4, 6);
            this.boxOperatorNumerator.Name = "boxOperatorNumerator";
            this.boxOperatorNumerator.Size = new System.Drawing.Size(105, 29);
            this.boxOperatorNumerator.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "----------------------------";
            // 
            // boxOperatorDenominator
            // 
            this.boxOperatorDenominator.Location = new System.Drawing.Point(4, 54);
            this.boxOperatorDenominator.Name = "boxOperatorDenominator";
            this.boxOperatorDenominator.Size = new System.Drawing.Size(105, 29);
            this.boxOperatorDenominator.TabIndex = 2;
            // 
            // BoxDivisor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.boxOperatorDenominator);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.boxOperatorNumerator);
            this.Name = "BoxDivisor";
            this.Size = new System.Drawing.Size(114, 89);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private BoxOperator boxOperatorNumerator;
        private System.Windows.Forms.Label label1;
        private BoxOperator boxOperatorDenominator;
    }
}

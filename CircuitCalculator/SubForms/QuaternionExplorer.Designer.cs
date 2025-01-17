namespace CircuitCalculator
{
    partial class QuaternionExplorer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(QuaternionExplorer));
            this.geometryPictureBox = new System.Windows.Forms.PictureBox();
            this.closeWindowButton = new System.Windows.Forms.Button();
            this.openNotepadButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.geometryPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // geometryPictureBox
            // 
            this.geometryPictureBox.Image = ((System.Drawing.Image)(resources.GetObject("geometryPictureBox.Image")));
            this.geometryPictureBox.Location = new System.Drawing.Point(12, 12);
            this.geometryPictureBox.Name = "geometryPictureBox";
            this.geometryPictureBox.Size = new System.Drawing.Size(670, 670);
            this.geometryPictureBox.TabIndex = 0;
            this.geometryPictureBox.TabStop = false;
            // 
            // closeWindowButton
            // 
            this.closeWindowButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.closeWindowButton.Location = new System.Drawing.Point(2, 688);
            this.closeWindowButton.Name = "closeWindowButton";
            this.closeWindowButton.Size = new System.Drawing.Size(128, 23);
            this.closeWindowButton.TabIndex = 5;
            this.closeWindowButton.Text = "Close Window";
            this.closeWindowButton.UseVisualStyleBackColor = true;
            this.closeWindowButton.Click += new System.EventHandler(this.CloseWindowButton_Click);
            // 
            // openNotepadButton
            // 
            this.openNotepadButton.Location = new System.Drawing.Point(146, 688);
            this.openNotepadButton.Name = "openNotepadButton";
            this.openNotepadButton.Size = new System.Drawing.Size(118, 23);
            this.openNotepadButton.TabIndex = 6;
            this.openNotepadButton.Text = "Open Notepad";
            this.openNotepadButton.UseVisualStyleBackColor = true;
            this.openNotepadButton.Click += new System.EventHandler(this.CalculateVectorButton_Click);
            // 
            // QuaternionExplorer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(713, 721);
            this.Controls.Add(this.openNotepadButton);
            this.Controls.Add(this.closeWindowButton);
            this.Controls.Add(this.geometryPictureBox);
            this.Name = "QuaternionExplorer";
            this.Text = "Quaterions needed for this geometry!";
            ((System.ComponentModel.ISupportInitialize)(this.geometryPictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox geometryPictureBox;
        private System.Windows.Forms.Button closeWindowButton;
        private System.Windows.Forms.Button openNotepadButton;

    }
}
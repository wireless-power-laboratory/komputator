namespace CircuitCalculator
{
    partial class NoteTextPanel
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
            this.saveNotesButton = new System.Windows.Forms.Button();
            this.notifyLabel = new System.Windows.Forms.Label();
            this.closeButton = new System.Windows.Forms.Button();
            this.openTextFileButton = new System.Windows.Forms.Button();
            this.appendNoteCheckBox = new System.Windows.Forms.CheckBox();
            this.fontSizeSelectionBox = new System.Windows.Forms.ComboBox();
            this.notesTextBox = new System.Windows.Forms.RichTextBox();
            this.clearTextButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // saveNotesButton
            // 
            this.saveNotesButton.Location = new System.Drawing.Point(265, 418);
            this.saveNotesButton.Name = "saveNotesButton";
            this.saveNotesButton.Size = new System.Drawing.Size(75, 23);
            this.saveNotesButton.TabIndex = 1;
            this.saveNotesButton.Text = "Save note";
            this.saveNotesButton.UseVisualStyleBackColor = true;
            this.saveNotesButton.Click += new System.EventHandler(this.SaveNotesButton_Click);
            // 
            // notifyLabel
            // 
            this.notifyLabel.AutoSize = true;
            this.notifyLabel.Location = new System.Drawing.Point(12, 8);
            this.notifyLabel.Name = "notifyLabel";
            this.notifyLabel.Size = new System.Drawing.Size(0, 13);
            this.notifyLabel.TabIndex = 2;
            // 
            // closeButton
            // 
            this.closeButton.Location = new System.Drawing.Point(184, 3);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(62, 23);
            this.closeButton.TabIndex = 3;
            this.closeButton.Text = "Close";
            this.closeButton.UseVisualStyleBackColor = true;
            this.closeButton.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // openTextFileButton
            // 
            this.openTextFileButton.Location = new System.Drawing.Point(252, 3);
            this.openTextFileButton.Name = "openTextFileButton";
            this.openTextFileButton.Size = new System.Drawing.Size(88, 23);
            this.openTextFileButton.TabIndex = 4;
            this.openTextFileButton.Text = "Open note";
            this.openTextFileButton.UseVisualStyleBackColor = true;
            this.openTextFileButton.Click += new System.EventHandler(this.OpenTextFileButton_Click);
            // 
            // appendNoteCheckBox
            // 
            this.appendNoteCheckBox.AutoSize = true;
            this.appendNoteCheckBox.Location = new System.Drawing.Point(197, 422);
            this.appendNoteCheckBox.Name = "appendNoteCheckBox";
            this.appendNoteCheckBox.Size = new System.Drawing.Size(62, 17);
            this.appendNoteCheckBox.TabIndex = 5;
            this.appendNoteCheckBox.Text = "append";
            this.appendNoteCheckBox.UseVisualStyleBackColor = true;
            // 
            // fontSizeSelectionBox
            // 
            this.fontSizeSelectionBox.FormattingEnabled = true;
            this.fontSizeSelectionBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.fontSizeSelectionBox.Items.AddRange(new object[] {
            "10",
            "11",
            "12",
            "14",
            "16",
            "18"});
            this.fontSizeSelectionBox.Location = new System.Drawing.Point(12, 418);
            this.fontSizeSelectionBox.Name = "fontSizeSelectionBox";
            this.fontSizeSelectionBox.Size = new System.Drawing.Size(105, 21);
            this.fontSizeSelectionBox.TabIndex = 6;
            this.fontSizeSelectionBox.SelectedIndexChanged += new System.EventHandler(this.FontSizeSelectionBox_SelectedIndexChanged);
            // 
            // notesTextBox
            // 
            this.notesTextBox.Location = new System.Drawing.Point(12, 32);
            this.notesTextBox.Name = "notesTextBox";
            this.notesTextBox.Size = new System.Drawing.Size(328, 380);
            this.notesTextBox.TabIndex = 7;
            this.notesTextBox.Text = "";
            // 
            // clearTextButton
            // 
            this.clearTextButton.Location = new System.Drawing.Point(129, 418);
            this.clearTextButton.Name = "clearTextButton";
            this.clearTextButton.Size = new System.Drawing.Size(62, 23);
            this.clearTextButton.TabIndex = 8;
            this.clearTextButton.Text = "Clear";
            this.clearTextButton.UseVisualStyleBackColor = true;
            this.clearTextButton.Click += new System.EventHandler(this.ClearTextButton_Click);
            // 
            // NoteTextPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(352, 451);
            this.Controls.Add(this.clearTextButton);
            this.Controls.Add(this.notesTextBox);
            this.Controls.Add(this.fontSizeSelectionBox);
            this.Controls.Add(this.appendNoteCheckBox);
            this.Controls.Add(this.openTextFileButton);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.notifyLabel);
            this.Controls.Add(this.saveNotesButton);
            this.Name = "NoteTextPanel";
            this.Text = "Notepad - Experimenter\'s Station";
            this.Load += new System.EventHandler(this.NoteTextPanel_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button saveNotesButton;
        private System.Windows.Forms.Label notifyLabel;
        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.Button openTextFileButton;
        private System.Windows.Forms.CheckBox appendNoteCheckBox;
        private System.Windows.Forms.ComboBox fontSizeSelectionBox;
        private System.Windows.Forms.RichTextBox notesTextBox;
        private System.Windows.Forms.Button clearTextButton;
    }
}
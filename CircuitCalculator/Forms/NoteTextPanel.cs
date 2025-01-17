using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace CircuitCalculator
{
    public partial class NoteTextPanel : Form
    {
        private Form owner;
        private static bool instance = false;
        readonly SaveFileDialog saveFileDialog = new SaveFileDialog();
        private OpenFileDialog openFileDialog;
        private string filename;
        private bool fileSaved;

        public NoteTextPanel(Form mOwner)
        { 
            InitializeComponent();
            owner = mOwner;
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            this.StartPosition = FormStartPosition.CenterParent;
            this.ShowInTaskbar = false;
            this.ControlBox = false;
            instance = true;
            this.StartPosition = FormStartPosition.CenterParent;
            fontSizeSelectionBox.Text = "Select font size";
        }
        /// <summary>
        /// Checks the instance of the form.
        /// </summary>
        public static bool Instance { get { return instance; } set { instance = value; } }
        /// <summary>
        /// Saves the notes on the pad.
        /// </summary>
        protected void SaveNotes()
        {
            if (notesTextBox.Text == "")
            {
                MessageBox.Show("You must have text on the pad in order to save.", "Entry Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
            if (notesTextBox.Text != "")
            {
                saveFileDialog.Filter = "Text Files (*.txt)|*.txt";
                saveFileDialog.FilterIndex = 1;
                saveFileDialog.FileName = filename;
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    if (saveFileDialog.FileName != "")
                    {
                        try
                        {
                            StreamWriter saveStream;
                            if (appendNoteCheckBox.Checked)
                            {
                                saveStream = new StreamWriter(saveFileDialog.FileName, true);
                                saveStream.Write(notesTextBox.Text);
                                saveStream.Close();
                                notifyLabel.Text = "File saved with appended text.";
                            }
                            if (!appendNoteCheckBox.Checked)
                            {
                                saveStream = new StreamWriter(saveFileDialog.FileName, false);
                                saveStream.Write(notesTextBox.Text);
                                saveStream.Close();
                                notifyLabel.Text = "File has been replaced.";
                            }
                            fileSaved = true;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Could not save file: " + ex.Message);
                        }
                    }
                }
            }
            else
            {
                notifyLabel.Text = "Cancel pressed. File not saved.";
            }
        }
        /// <summary>
        /// Maps the path of the executing file.
        /// </summary>
        /// <param name="path">The path to map.</param>
        public virtual string MapPath(string path)
        {
            string location = System.Reflection.Assembly.GetEntryAssembly().Location;
            string zebra = System.AppDomain.CurrentDomain.BaseDirectory.ToString();
            string local = Environment.CurrentDirectory;
            return Path.Combine(zebra, path);
        }

        #region Events
        /// <summary>
        /// The load event for the inductance calculator form.
        /// </summary>
        private void NoteTextPanel_Load(object sender, EventArgs e)
        {
            ToolTip toolTip = new ToolTip
            {
                AutoPopDelay = 10000,
                InitialDelay = 400,
                ReshowDelay = 200,
                ShowAlways = true
            };
            toolTip.SetToolTip(this.openTextFileButton, "Select a file to open as a note on the pad.");
            toolTip.SetToolTip(this.saveNotesButton, "Save or SaveAs the note on the pad.");
            toolTip.SetToolTip(this.appendNoteCheckBox, "Tick this box to append the content of the pad to the existing text file.");
            toolTip.SetToolTip(this.closeButton, "Close the pad. It will prompt you to save if text is on the pad.");
        }
        /// <summary>
        /// Saves the notes on the form into a text file.
        /// </summary>
        private void SaveNotesButton_Click(object sender, EventArgs e)
        {
            SaveNotes();
        }
        /// <summary>
        /// Closes the Notepad form.
        /// </summary>
        private void CloseButton_Click(object sender, EventArgs e)
        {
            if (notesTextBox.Text == "")
            {
                instance = false;
                Close();
            }
            if (notesTextBox.Text != "" & !fileSaved)
            {
                if (MessageBox.Show("You have a note on the pad, do you want to save it?", "Query", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                {
                    SaveNotes();
                }
                else
                {
                    instance = false;
                    Close();
                }
            }
            else
            {
                instance = false;
                Close();
            }
        }
        /// <summary>
        /// Opens an existing note file.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenTextFileButton_Click(object sender, EventArgs e)
        {
            openFileDialog = new OpenFileDialog
            {
                InitialDirectory = "c:\\",
                Filter = "Text files (*.txt)|*.txt",
                FilterIndex = 2,
                RestoreDirectory = true
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Stream fileStream;
                    if ((fileStream = openFileDialog.OpenFile()) != null)
                    {
                        using (fileStream)
                        {
                            notesTextBox.Text = File.ReadAllText(openFileDialog.FileName);
                            filename = openFileDialog.FileName;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error, could not read file: " + ex.Message);
                }
                fileSaved = true;
            }
        }
        private void NotesTextBox_TextChanged(object sender, EventArgs e)
        {
            fileSaved = false;
        }
        private void FontSizeSelectionBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (fontSizeSelectionBox.SelectedItem.ToString() != "Select font size")
            {
                notesTextBox.SelectAll();
                notesTextBox.SelectionFont = new Font("Verdana", Convert.ToInt32(fontSizeSelectionBox.SelectedItem), FontStyle.Regular);
            }
            else
                notifyLabel.Text = "When changing font, use the numeric values.";
        }
        private void ClearTextButton_Click(object sender, EventArgs e)
        {
            notesTextBox.Text = "";
            notesTextBox.Refresh();
        }
        #endregion
    }
}

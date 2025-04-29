using NotesTaking.MVVM.Model;
using NotesTaking.MVVM.ViewModel;
using System;
using System.Windows;
using System.Windows.Media;

namespace NotesTaking.MVVM.View
{
    public partial class CreateNoteWindow : Window
    {
        public string NoteTitle { get; private set; }
        public string NoteContent { get; private set; }

        private DatabaseManager dbManager;

        public CreateNoteWindow()
        {
            InitializeComponent();
            dbManager = new DatabaseManager();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            NoteTitle = txtTitle.Text.Trim();
            NoteContent = txtContent.Text.Trim();
            string loggedInUsername = UserSession.LoggedInUsername;

            if (string.IsNullOrEmpty(NoteTitle) || NoteTitle == "Note Title:")
            {
                MessageBox.Show("Please enter a title for the note.", "Missing Title", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (string.IsNullOrEmpty(NoteContent) || NoteContent == "Note...")
            {
                MessageBox.Show("Please enter content for the note.", "Missing Content", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (string.IsNullOrEmpty(loggedInUsername))
            {
                MessageBox.Show("Error: No user is currently logged in.", "Login Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Create a new note object with the current date and time
            Note newNote = new Note
            {
                NoteTitle = NoteTitle,
                NoteContent = NoteContent,
                NoteDate = DateTime.Now // Provide the current datetime
            };

            // Get the account ID for the logged-in user
            int accountId = dbManager.GetLoggedInAccountId(loggedInUsername);
            if (accountId == -1)
            {
                MessageBox.Show("Error: Unable to find account for logged-in user.", "Account Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Insert the note into the database
            bool isInserted = dbManager.InsertNote(accountId, newNote.NoteTitle, newNote.NoteContent, newNote.NoteDate);
            if (isInserted)
            {
                MessageBox.Show("Note added successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                DialogResult = true;
            }
            else
            {
                MessageBox.Show("Error: Unable to add the note.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                DialogResult = false;
            }

            // Close the window
            this.Close();
        }



        private void txtTitle_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtTitle.Text == "Note Title:")
            {
                txtTitle.Text = string.Empty;
                txtTitle.Foreground = new SolidColorBrush(Colors.Black);
            }
        }

        private void txtTitle_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTitle.Text))
            {
                txtTitle.Text = "Note Title:";
                txtTitle.Foreground = new SolidColorBrush(Colors.Gray);
            }
        }

        private void txtContent_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtContent.Text == "Note...")
            {
                txtContent.Text = string.Empty;
                txtContent.Foreground = new SolidColorBrush(Colors.Black);
            }
        }

        private void txtContent_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtContent.Text))
            {
                txtContent.Text = "Note...";
                txtContent.Foreground = new SolidColorBrush(Colors.Gray);
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void CreateNoteButton_Click(object sender, RoutedEventArgs e)
        {
            CreateNoteWindow createNoteWindow = new CreateNoteWindow();
            bool? result = createNoteWindow.ShowDialog();

            if (result == true)
            {
                Note newNote = new Note
                {
                    NoteTitle = createNoteWindow.NoteTitle,
                    NoteContent = createNoteWindow.NoteContent
                };

                // Add additional logic if needed, e.g., updating the UI
            }
        }
    }
}

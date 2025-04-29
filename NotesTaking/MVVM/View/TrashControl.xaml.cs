using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using MySql.Data.MySqlClient;
using NotesTaking.MVVM.Model;
using NotesTaking.MVVM.ViewModel;

namespace NotesTaking.MVVM.View
{
    public partial class TrashControl : UserControl
    {
        public ObservableCollection<Note> TrashedNotes { get; set; }
        private DatabaseManager dbManager;

        public TrashControl()
        {
            InitializeComponent();
            TrashedNotes = new ObservableCollection<Note>();
            TrashItemsControl.ItemsSource = TrashedNotes;

            dbManager = new DatabaseManager();
            int accountId = dbManager.GetLoggedInAccountId(UserSession.LoggedInUsername);
            if (accountId != -1)
            {
                LoadTrashedNotes(accountId);
            }
            else
            {
                MessageBox.Show("Error: Unable to find account for logged-in user.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LoadTrashedNotes(int accountId)
        {
            try
            {
                TrashedNotes = dbManager.LoadTrashedNotes(accountId);
                TrashItemsControl.ItemsSource = TrashedNotes;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load trashed notes: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void RestoreNoteFromTrash(int accountId, int noteId)
        {
            if (accountId != -1)
            {
                if (dbManager.RestoreNoteFromTrash(accountId, noteId))
                {
                    // Note restored successfully, update UI
                    var note = TrashedNotes.FirstOrDefault(n => n.NotesID == noteId);
                    if (note != null)
                    {
                        TrashedNotes.Remove(note);
                    }
                }
                else
                {
                    MessageBox.Show("Failed to restore note.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Error: Unable to find account for logged-in user.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void RestoreButton_Click(object sender, RoutedEventArgs e)
        {
            Button restoreButton = (Button)sender;
            if (restoreButton.DataContext is Note selectedNote)
            {
                int accountId = dbManager.GetLoggedInAccountId(UserSession.LoggedInUsername);
                RestoreNoteFromTrash(accountId, selectedNote.NotesID);
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            Button deleteButton = (Button)sender;
            if (deleteButton.DataContext is Note selectedNote)
            {
                int accountId = dbManager.GetLoggedInAccountId(UserSession.LoggedInUsername);

                // Display confirmation dialog
                MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this note?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

                // Check user's choice
                if (result == MessageBoxResult.Yes)
                {
                    // User confirmed deletion, proceed with deletion
                    DeleteNoteFromTrash(accountId, selectedNote.NotesID);
                }
            }
        }

        private void DeleteNoteFromTrash(int accountId, int noteId)
        {
            if (accountId != -1)
            {
                if (dbManager.DeleteNoteFromTrash(accountId, noteId))
                {
                    // Note deleted successfully, update UI
                    var note = TrashedNotes.FirstOrDefault(n => n.NotesID == noteId);
                    if (note != null)
                    {
                        TrashedNotes.Remove(note);
                    }
                }
                else
                {
                    MessageBox.Show("Failed to delete note.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Error: Unable to find account for logged-in user.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}

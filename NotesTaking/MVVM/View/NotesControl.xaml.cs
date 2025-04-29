using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using NotesTaking.MVVM.Model;
using NotesTaking.MVVM.ViewModel;

namespace NotesTaking.MVVM.View
{
    public partial class NotesControl : UserControl
    {
        public ObservableCollection<Note> Notes { get; set; }
        private DatabaseManager dbManager;

        public NotesControl()
        {
            InitializeComponent();
            Notes = new ObservableCollection<Note>();
            NotesItemsControl.ItemsSource = Notes;

            dbManager = new DatabaseManager();
            int accountId = dbManager.GetLoggedInAccountId(UserSession.LoggedInUsername);
            if (accountId != -1)
            {
                LoadNotes(accountId);
            }
            else
            {
                MessageBox.Show("Error: Unable to find account for logged-in user.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
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
                Notes.Add(newNote);
            }
        }

        private void LoadNotes(int accountId)
        {
            try
            {
                var notes = dbManager.LoadNotes(accountId);
                Notes.Clear();
                foreach (var note in notes)
                {
                    Notes.Add(note);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load notes: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ArchiveNoteButton_Click(object sender, RoutedEventArgs e)
        {
            Button archiveButton = (Button)sender;
            if (archiveButton.DataContext is Note selectedNote)
            {
                int accountId = dbManager.GetLoggedInAccountId(UserSession.LoggedInUsername);
                ArchiveNoteFromNotes(accountId, selectedNote.NotesID);
            }
        }

        private void ArchiveNoteFromNotes(int accountId, int noteId)
        {
            if (accountId != -1)
            {
                if (dbManager.ArchiveNoteFromNotes(accountId, noteId))
                {
                    var note = Notes.FirstOrDefault(n => n.NotesID == noteId);
                    if (note != null)
                    {
                        Notes.Remove(note);
                    }
                }
                else
                {
                    MessageBox.Show("Failed to archive note.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Error: Unable to find account for logged-in user.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DeleteNoteButton_Click(object sender, RoutedEventArgs e)
        {
            Button deleteButton = (Button)sender;
            if (deleteButton.DataContext is Note selectedNote)
            {
                // Pass only the noteId to the DeleteNoteFromNotes method
                DeleteNoteFromNotes(selectedNote.NotesID);
            }
        }


        private void DeleteNoteFromNotes(int noteId)
        {
            if (dbManager.DeleteNoteFromNotes(noteId))
            {
                var note = Notes.FirstOrDefault(n => n.NotesID == noteId);
                if (note != null)
                {
                    Notes.Remove(note);
                }
            }
            else
            {
                MessageBox.Show("Failed to delete note.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


    }
}

using MySql.Data.MySqlClient;
using NotesTaking.MVVM.Model;
using NotesTaking.MVVM.ViewModel;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace NotesTaking.MVVM.View
{
    public partial class NotePopupWindow : Window
    {
        private readonly NoteViewModel _noteViewModel;

        public NotePopupWindow()
        {   
            InitializeComponent();
            _noteViewModel = new NoteViewModel();
            DataContext = _noteViewModel;
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            Button deleteButton = sender as Button;
            if (deleteButton != null)
            {
                Note noteToDelete = deleteButton.DataContext as Note;
                if (noteToDelete != null)
                {
                    MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this note?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                    if (result == MessageBoxResult.Yes)
                    {
                        try
                        {
                            DatabaseManager dbManager = new DatabaseManager();
                            dbManager.MoveNoteToTrash(noteToDelete.NotesID); // Pass the note ID
                            dbManager.DeleteNoteFromDatabase(noteToDelete);

                            MessageBox.Show("Note deleted successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                            this.Close();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Error deleting note: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }
            }
        }


        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            Button saveButton = sender as Button;
            if (saveButton != null)
            {
                Note noteToSave = saveButton.DataContext as Note;
                if (noteToSave != null)
                {
                    try
                    {
                        DatabaseManager dbManager = new DatabaseManager();
                        bool success = dbManager.UpdateNote(noteToSave);

                        if (success)
                        {
                            MessageBox.Show("Note successfully updated.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        else
                        {
                            MessageBox.Show("Failed to update note. Note not found.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error updating note: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }

            if (this.DataContext is NoteViewModel noteViewModel)
            {
                MessageBox.Show("Note saved.");
            }
            this.DialogResult = true;
            this.Close();
        }

        private void ArchiveButton_Click(object sender, RoutedEventArgs e)
        {
            Button archiveButton = sender as Button;
            if (archiveButton != null)
            {
                Note noteToArchive = archiveButton.DataContext as Note;
                if (noteToArchive != null)
                {
                    try
                    {
                        DatabaseManager dbManager = new DatabaseManager();
                        dbManager.ArchiveNote(noteToArchive); // Pass the Note object
                        dbManager.DeleteNoteFromNotes(noteToArchive.NotesID); // Pass the note ID

                        MessageBox.Show("Note archived successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error archiving note: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }



        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close(); // Close the window when Cancel button is clicked
        }

        private void UnarchiveButton_Click(object sender, RoutedEventArgs e)
        {
            Button unarchiveButton = sender as Button;
            if (unarchiveButton != null)
            {
                Note noteToUnarchive = unarchiveButton.DataContext as Note;
                if (noteToUnarchive != null)
                {
                    try
                    {
                        DatabaseManager dbManager = new DatabaseManager();
                        dbManager.UnarchiveNote(noteToUnarchive); // Pass the Note object

                        MessageBox.Show("Note unarchived successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error unarchiving note: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }
    }
}

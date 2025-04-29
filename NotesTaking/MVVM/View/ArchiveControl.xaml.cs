using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using NotesTaking.MVVM.Model;
using NotesTaking.MVVM.ViewModel;

namespace NotesTaking.MVVM.View
{
    public partial class ArchiveControl : UserControl
    {
        public ObservableCollection<Note> ArchivedNotes { get; set; }
        private DatabaseManager dbManager;

        public ArchiveControl()
        {
            InitializeComponent();
            ArchivedNotes = new ObservableCollection<Note>();
            ArchiveItemsControl.ItemsSource = ArchivedNotes;

            dbManager = new DatabaseManager();
            int accountId = dbManager.GetLoggedInAccountId(UserSession.LoggedInUsername);
            if (accountId != -1)
            {
                LoadArchivedNotes(accountId);
            }
            else
            {
                MessageBox.Show("Error: Unable to find account for logged-in user.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LoadArchivedNotes(int accountId)
        {
            try
            {
                ArchivedNotes = dbManager.GetArchivedNotes(accountId);
                ArchiveItemsControl.ItemsSource = ArchivedNotes;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
            }
        }
    }
}

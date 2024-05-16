﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace NotesTaking.MVVM.View
{
    /// <summary>
    /// Interaction logic for CreateNoteWindow.xaml
    /// </summary>
    public partial class CreateNoteWindow : Window
    {
        public string NoteTitle { get; private set; }
        public string NoteContent { get; private set; }

        public CreateNoteWindow()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            NoteTitle = txtTitle.Text;
            NoteContent = txtContent.Text;
            string loggedInUsername = UserSession.LoggedInUsername; // Retrieve the logged-in username

            if (string.IsNullOrEmpty(loggedInUsername))
            {
                Console.WriteLine("Error: No user is currently logged in.");
                return;
            }

            // Get the logged-in account ID using the DatabaseManager
            DatabaseManager dbManager = new DatabaseManager();
            int accountId = dbManager.GetLoggedInAccountId(loggedInUsername);

            if (accountId == -1)
            {
                Console.WriteLine("Error: Unable to find account for logged-in user.");
                return;
            }

            // Insert the note into the database
            bool isInserted = dbManager.InsertNote(accountId, NoteTitle, NoteContent);

            if (isInserted)
            {
                Console.WriteLine("Note added successfully.");
                DialogResult = true; // Indicate the note was successfully created
            }
            else
            {
                Console.WriteLine("Error: Unable to add the note.");
                DialogResult = false; // Indicate the note creation failed
            }

            // Close the window
            this.Close();
        }
    }
}
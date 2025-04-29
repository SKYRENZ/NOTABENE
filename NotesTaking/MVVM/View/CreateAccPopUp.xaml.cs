using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    public partial class CreateAccPopUp : Window
    {
        private DatabaseManager dbManager;

        public CreateAccPopUp()
        {
            InitializeComponent();
            dbManager = new DatabaseManager();
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            string username = crtUsername.Text;
            string password = crtPassword.Password;
            string confirmPassword = crtPassword_Copy.Password;

            // Update password variable if the password is currently visible
            if (crtPasswordText.Visibility == Visibility.Visible)
            {
                password = crtPasswordText.Text;
            }

            // Update confirmPassword variable if the confirmPassword is currently visible
            if (crtPasswordCopyText.Visibility == Visibility.Visible)
            {
                confirmPassword = crtPasswordCopyText.Text;
            }

            //Checker if the username textbox is empty
            if (string.IsNullOrEmpty(username))
            {
                MessageBox.Show("Username cannot be empty.", "Username Required", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Validate inputs
            if (!IsPasswordValid(password, out string validationError))
            {
                MessageBox.Show(validationError, "Password Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (password != confirmPassword)
            {
                MessageBox.Show("Passwords do not match.", "Password Mismatch", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (IsUsernameTaken(username))
            {
                MessageBox.Show("Username is already taken.", "Username Taken", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Insert new account
            if (InsertAccount(username, password))
            {
                MessageBox.Show("Account created successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close(); // Close the popup
            }
            else
            {
                MessageBox.Show("Failed to create account.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private bool IsPasswordValid(string password, out string validationError)
        {
            validationError = string.Empty;

            if (password.Length <= 8 || password.Length >= 24)
            {
                validationError = "Password must be between 8 and 24 characters.";
                return false;
            }

            if (!Regex.IsMatch(password, @"[A-Z]"))
            {
                validationError = "Password must contain at least one uppercase letter.";
                return false;
            }

            if (!Regex.IsMatch(password, @"[0-9]"))
            {
                validationError = "Password must contain at least one numeric value.";
                return false;
            }

            if (!Regex.IsMatch(password, @"[\W_]")) // Non-word characters or underscore
            {
                validationError = "Password must contain at least one special character.";
                return false;
            }

            return true;
        }

        private bool IsUsernameTaken(string username)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(dbManager.ConnectionString))
                {
                    connection.Open();

                    string query = "SELECT COUNT(*) FROM account WHERE AccountUser = @username";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@username", username);

                    int count = Convert.ToInt32(command.ExecuteScalar());
                    return count > 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error checking username: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return true;
            }
        }

        private bool InsertAccount(string username, string password)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(dbManager.ConnectionString))
                {
                    connection.Open();

                    string query = "INSERT INTO account (AccountUser, AccountPass) VALUES (@username, @password)";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@username", username);
                    command.Parameters.AddWithValue("@password", password);

                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error creating account: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

        private void UnhideButton_Click(object sender, RoutedEventArgs e)
        {
            TogglePasswordVisibility(crtPassword, crtPasswordText);
        }

        private void UnhideButtonCopy_Click(object sender, RoutedEventArgs e)
        {
            TogglePasswordVisibility(crtPassword_Copy, crtPasswordCopyText);
        }

        private void TogglePasswordVisibility(PasswordBox passwordBox, TextBox textBox)
        {
            if (passwordBox.Visibility == Visibility.Visible)
            {
                textBox.Text = passwordBox.Password;
                textBox.Visibility = Visibility.Visible;
                passwordBox.Visibility = Visibility.Collapsed;
            }
            else
            {
                passwordBox.Password = textBox.Text;
                textBox.Visibility = Visibility.Collapsed;
                passwordBox.Visibility = Visibility.Visible;
            }
        }
    }
}

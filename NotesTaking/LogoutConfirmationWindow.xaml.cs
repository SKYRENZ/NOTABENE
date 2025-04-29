using System.Windows;

namespace NotesTaking
{
    public partial class LogoutConfirmationWindow : Window
    {
        public bool IsLogoutConfirmed { get; private set; }

        public LogoutConfirmationWindow()
        {
            InitializeComponent();
            IsLogoutConfirmed = false; // Initialize to false
        }

        private void Yes_Click(object sender, RoutedEventArgs e)
        {
            IsLogoutConfirmed = true; // Set to true when Yes is clicked
            Close();
        }

        private void No_Click(object sender, RoutedEventArgs e)
        {
            IsLogoutConfirmed = false; // Set to false when No is clicked
            Close();
        }
    }
}

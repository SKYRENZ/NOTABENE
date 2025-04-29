using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MySql.Data.MySqlClient;
using NotesTaking.MVVM.ViewModel;

namespace NotesTaking
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DatabaseManager databaseManager;
        private SolidColorBrush? originalFill, originalStroke, originalFillMinimize, originalStrokeMinimize;

        public MainWindow()
        {

            InitializeComponent();
            databaseManager = new DatabaseManager();
            //Revert Color of Close Button
            originalFill = btnClose.Fill as SolidColorBrush;
            originalStroke = btnClose.Stroke as SolidColorBrush;

            //Revert Color of Minimize Button
            originalFillMinimize = btnMinimize.Fill as SolidColorBrush;
            originalStrokeMinimize = btnMinimize.Stroke as SolidColorBrush;
        }


        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ButtonState == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }

        private void btnClose_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
           Application.Current.Shutdown();
        }


        private void btnClose_MouseEnter(object sender, MouseEventArgs e)
        {
            //Changes the color of close button when hovered
            btnClose.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#C7282D"));
            btnClose.Stroke = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF343A"));
        }

        private void btnMinimize_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }


        private void btnSignIn_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Password.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter both username and password.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (databaseManager.ValidateUser(username, password))
            {
                UserSession.LoggedInUsername = username;
                Dashboard mainDashboard = new Dashboard();
                this.Visibility = Visibility.Hidden;
                mainDashboard.Show();
            }
            else
            {
                MessageBox.Show("Invalid username or password.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void CreateAcc_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            MVVM.View.CreateAccPopUp createAccPopUp = new MVVM.View.CreateAccPopUp();
            createAccPopUp.Owner = this;
            createAccPopUp.ShowDialog();
        }

        private void btnMinimize_MouseEnter(object sender, MouseEventArgs e)
        {
            //Changes the color of minimize button when hovered
            btnMinimize.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#C79436"));
            btnMinimize.Stroke = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFBE45"));
        }

        private void btnMinimize_MouseLeave(object sender, MouseEventArgs e)
        {
            //reverts color to original minimize button
            btnMinimize.Fill = originalFillMinimize;
            btnMinimize.Stroke = originalStrokeMinimize;
        }

        private void btnClose_MouseLeave(object sender, MouseEventArgs e)
        {   
            //reverts color to original Close button
            btnClose.Fill = originalFill;
            btnClose.Stroke = originalStroke;
        }
    }
}

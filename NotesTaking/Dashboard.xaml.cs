using NotesTaking;
using NotesTaking.MVVM.View;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace NotesTaking
{
    public partial class Dashboard : Window
    {
        public CornerRadius CornerRadius { get; set; }

        private SolidColorBrush? originalFill, originalStroke, originalFillMinimize, originalStrokeMinimize;
        private Button previousButton; // Variable to keep track of the previously clicked button

        public Dashboard()
        {
            InitializeComponent();
            //Revert Color of Close Button
            originalFill = btnClose.Fill as SolidColorBrush;
            originalStroke = btnClose.Stroke as SolidColorBrush;

            //Revert Color of Minimize Button
            originalFillMinimize = btnMinimize.Fill as SolidColorBrush;
            originalStrokeMinimize = btnMinimize.Stroke as SolidColorBrush;

            // Set NotesControl as the default view
            SetDefaultView();
            DataContext = new DateTimeViewModel();
        }

        private void SetDefaultView()
        {
            NotesControl notesControl = new NotesControl();
            contentArea.Content = notesControl;
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

        private void btnMinimize_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnClose_MouseEnter(object sender, MouseEventArgs e)
        {
            //Changes the color of close button when hovered
            btnClose.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#C7282D"));
            btnClose.Stroke = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF343A"));
        }

        private void HighlightButton(Button button)
        {
            // Highlight the clicked button
            button.Background = Brushes.LightBlue;

            // If there was a previously clicked button, reset its background
            if (previousButton != null && previousButton != button)
            {
                // Reset the background of the previously clicked button
                previousButton.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#28282B"));
            }

            // Update the previousButton variable to the currently clicked button
            previousButton = button;
        }

        private void btnNotes_Click(object sender, RoutedEventArgs e)
        {
            // Load the Notes control into the content area
            NotesControl notesControl = new NotesControl();
            contentArea.Content = notesControl;

            // Highlight the clicked button
            HighlightButton(sender as Button);
        }

        private void btnArchive_Click(object sender, RoutedEventArgs e)
        {
            // Load the Archive control into the content area
            ArchiveControl archiveControl = new ArchiveControl();
            contentArea.Content = archiveControl;

            // Highlight the clicked button
            HighlightButton(sender as Button);
        }

        private void btnReminders_Click(object sender, RoutedEventArgs e)
        {
            // Load the Reminders control into the content area
            RemindersControl remindersControl = new RemindersControl();
            contentArea.Content = remindersControl;

            // Highlight the clicked button
            HighlightButton(sender as Button);
        }

        private void btnTrash_Click(object sender, RoutedEventArgs e)
        {
            // Load the Trash control into the content area
            TrashControl trashControl = new TrashControl();
            contentArea.Content = trashControl;

            // Highlight the clicked button
            HighlightButton(sender as Button);
        }

        private void btnLogOut_Click(object sender, RoutedEventArgs e)
        {
            // Create an instance of the LogoutConfirmationWindow
            LogoutConfirmationWindow logoutWindow = new LogoutConfirmationWindow();

            // Show the window as a dialog (blocking the main window until it's closed)
            logoutWindow.ShowDialog();

            // Check the user's choice
            if (logoutWindow.IsLogoutConfirmed)
            {
                // Perform logout actions if Yes was clicked
                MainWindow loginWindow = new MainWindow();
                loginWindow.Show();
                this.Close(); // Close the current dashboard window
            }
            // If No was clicked, simply return and do nothing (the dialog will have already closed itself)
        }

        private void Dashboard_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void btnClose_MouseLeave(object sender, MouseEventArgs e)
        {
            //reverts color to original Close button
            btnClose.Fill = originalFill;
            btnClose.Stroke = originalStroke;
        }

        private void btnClose_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //When Clicked
            Application.Current.Shutdown();
        }
    }
}

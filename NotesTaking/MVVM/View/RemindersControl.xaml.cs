using System;
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
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace NotesTaking.MVVM.View
{
    public partial class RemindersControl : UserControl
    {
        public RemindersControl()
        {
            InitializeComponent();
        }

        private void AddReminder_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(NewReminderTextBox.Text))
            {
                var newReminder = new Border
                {
                    Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#3B3B3D")),
                    CornerRadius = new CornerRadius(10),
                    Padding = new Thickness(10),
                    Margin = new Thickness(0, 5, 0, 5),
                    Child = new TextBlock
                    {
                        Text = NewReminderTextBox.Text,
                        Foreground = new SolidColorBrush(Colors.WhiteSmoke)
                    }
                };

                ReminderList.Children.Add(newReminder);
                NewReminderTextBox.Clear();
            }
        }
    }
}
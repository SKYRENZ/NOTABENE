using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace NotesTaking.MVVM.View
{
    public partial class NoteContainer : UserControl
    {
        public NoteContainer()
        {
            InitializeComponent();
           
        }

        private void NoteContainer_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            NotePopupWindow popupWindow = new NotePopupWindow
            {
                DataContext = this.DataContext // Pass the data context to the popup
            };
            popupWindow.ShowDialog();
        }   
    }
}

using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using NotesTaking.MVVM.Model;

namespace NotesTaking.MVVM.ViewModel
{
    public class NoteViewModel : INotifyPropertyChanged
    {
        private string _noteTitle;
        private string _noteContent;
        private DateTime _noteDate;

        public ObservableCollection<Note> Notes { get; set; }

        public string NoteTitle
        {
            get => _noteTitle;
            set
            {
                _noteTitle = value;
                OnPropertyChanged(nameof(NoteTitle));
            }
        }

        public string NoteContent
        {
            get => _noteContent;
            set
            {
                _noteContent = value;
                OnPropertyChanged(nameof(NoteContent));
            }
        }

        public DateTime NoteDate
        {
            get => _noteDate;
            set
            {
                _noteDate = value;
                OnPropertyChanged(nameof(NoteDate));
            }
        }

        public NoteViewModel()
        {
            Notes = new ObservableCollection<Note>();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private bool _isDeleteButtonVisible;

        public bool IsDeleteButtonVisible
        {
            get => _isDeleteButtonVisible;
            set
            {
                _isDeleteButtonVisible = value;
                OnPropertyChanged(nameof(IsDeleteButtonVisible));
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesTaking.MVVM.Model
{
    internal class ArchiveButtonVisibilityModel : INotifyPropertyChanged
    {
        private bool _isArchiveButtonVisible;
        public bool IsArchiveButtonVisible
        {
            get { return _isArchiveButtonVisible; }
            set
            {
                _isArchiveButtonVisible = value;
                OnPropertyChanged(nameof(IsArchiveButtonVisible));
            }
        }

        // Other properties and methods...

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

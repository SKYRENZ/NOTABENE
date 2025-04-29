using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesTaking.MVVM.Model
{
    public class Note
    {
        public int NotesID { get; set; }
        public string NoteTitle { get; set; }
        public string NoteContent { get; set; }
        public DateTime NoteDate { get; set; } // Add this property
        public bool IsArchived { get; set; } // Optional, if you need it to indicate if the note is archived
    

    public Note() { }

        public Note(string noteTitle, string noteContent, DateTime noteDate)
        {
            NoteTitle = noteTitle;
            NoteContent = noteContent;
            NoteDate = noteDate;
        }
    }
}



using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planify
{
    internal class Note
    {
        private string _userId;
        private string _noteId;
        private string _title;
        private string _description;
        private DateTime _created;

        public Note(string userId, string noteId, string title, string description)
        {
            _userId = userId;
            _noteId = noteId;
            _title = title;
            _description = description;
            _created = DateTime.Now;
        }
        
        public string UserId { get { return _userId; } }
        public string NoteId { get { return _noteId; } }
        public string Title { get { return _title; } }
        public string Description { get { return _description; } }
        public DateTime Created { get { return _created; } }


    }
}

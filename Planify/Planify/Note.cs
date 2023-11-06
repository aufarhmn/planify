using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planify
{
    internal class Note : Item
    {
        private string _noteId;

        public Note(string userId, string noteId, string title, string description) 
            : base(userId, title, description)
        {
            _noteId = noteId;
        }

        public string UserId 
        { 
            get { return _userId; } 
        }
        public string NoteId 
        { 
            get { return _noteId; } 
        }
        public string Title 
        { 
            get { return _title; }
            set { _title = value; }
        }
        
        public string Description 
        { 
            get { return _description; }
            set { _description = value; }
        
        }
        public DateTime DateCreated 
        { 
            get { return _dateCreated; }
            
        }

        public override bool CreateItem(Item item)
        {
            throw new NotImplementedException();
        }
    }
}

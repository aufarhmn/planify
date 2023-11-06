using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planify
{
    internal class Task : Item
    {
        private string _taskId;
        private string _category;
        private DateTime _dateEnd;

        public Task(string userId, string taskId, string title, string description, string category,  DateTime dateEnd) 
            : base(userId, title, description)
        {
            _taskId = taskId;
            _category = category;
            _dateEnd = dateEnd;
        }
        public string UserId 
        { 
            get { return _userId; } 
        }
        public string TaskId 
        { 
            get { return _taskId; } 
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
        public string Category 
        { 
            get { return _category; } 
            set { _category = value; }
        }
        public DateTime DateCreated 
        { 
            get { return _dateCreated; } 
        }
        public DateTime DateTimeEnd 
        { 
            get { return _dateEnd; } 
            set { _dateEnd = value; }
        }

        public override bool CreateItem(Item item)
        {
            
            
            throw new NotImplementedException();
        }

        
    }
}





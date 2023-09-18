using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planify
{
    internal class Task
    {
        private string _userId;
        private string _taskId;
        private string _title;
        private string _description;
        private string _category;
        private DateTime _dateCreate;
        private DateTime _dateEnd;

        public Task(string userId, string taskId, string title, string description, string category, DateTime dateEnd)
        {
            _userId = userId;
            _taskId = taskId;
            _title = title;
            _description = description;
            _category = category;
            _dateCreate = DateTime.Now;
            _dateEnd = dateEnd;
        }



        public string UserId { get { return _userId; } }
        public string TaskId { get { return _taskId; } }
        public string Title { get { return _title; } }
        public string Description { get { return _description; } }
        public string Category { get { return _category; } }
        public DateTime DateCreate { get { return _dateCreate; } }
        public DateTime DateTimeEnd { get { return _dateEnd; } }
    }
}



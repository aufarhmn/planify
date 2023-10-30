using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planify
{
    internal class Item
    {
        protected string _userId;
        protected string _title;
        protected string _description;
        protected DateTime _dateCreated;
        
        public Item(string userId, string title, string description, DateTime dateCreated)
        {
            _userId = userId;
            _title = title;
            _description = description;
            _dateCreated = dateCreated;
        }
    }
}

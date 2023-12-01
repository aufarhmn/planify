using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planify
{
    public abstract class Item
    {
        protected string _userId;
        protected string _title;
        protected string _description;
        protected DateTime _dateCreated;

        public Item(string userId, string title, string description)
        {
            _userId = userId;
            _title = title;
            _description = description;
            _dateCreated = DateTime.Now;
        }

        public abstract bool CreateItem(Item item);

    }
}

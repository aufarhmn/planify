using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planify
{
    public class Controller
    {
        private User userAccount;

        public void setUser (int userId, string name, string password)
        {
            this.userAccount.userId = userId;
            this.userAccount.name = name;
            this.userAccount.password = password;

        }

        public User getUser()
        {
            return this.userAccount;
        }
    }
}

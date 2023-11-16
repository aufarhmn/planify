using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planify
{
    public class User
    {
        private string _userId;
        private string _name;
        private string _email;
        private string _password;
        private string _fotoProfil;

        public string userId
        { 
            set { _userId = value; }
            get { return _userId; }
        }
        public string name
        {   
            set { _name = value; }
            get { return _name; }
        }

        public string email
        {
            get { return _email; }
        }

        public string password
        {
            set { _password = value; }
            get { return _password; }
        }
        public string fotoProfil
        {
            get { return _fotoProfil; }
        }
    }
}

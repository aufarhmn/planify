using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planify
{
    internal class Login
    {
        private string _userId;
        private string _name;
        private string _email;
        private string _password;
        private string _fotoProfil;

        public string userId
        {
            get { return _userId; }
        }
        public string name
        {
            get { return _name; }
        }

        public string email
        {
            get { return _email; }
        }

        public string password
        {
            get { return _password; }
        }
        public string fotoProfil
        {
            get { return _fotoProfil; }
        }
    }
}

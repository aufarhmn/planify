using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planify
{
    internal class Register
    {
        private string _name;
        private string _email;
        private string _password;
        private string _confirmPassword;

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
        public string confirmPassword
        {
            get { return _confirmPassword; }
        }

        //public Dictionary postDataRegisterToDatabase(string name, string email, string password)
        //{

        //}
    }
}

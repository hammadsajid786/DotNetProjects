using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibLoginAuthenticator
{
    public static class LoginAuthenticator
    {
        private const string _username = "PRG650";
        private const string _password = "pass";
        public static bool Authenticate(string username, string password)
        {
            if (username== _username && password==_password)
            {
                return true;
            }
            return false;
        }

    }
}

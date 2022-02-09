using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginAuthenticatorConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            do
            {
                Console.WriteLine("Please enter your username:");
                string username = Console.ReadLine();
                Console.WriteLine("Please enter your password:");
                string password = Console.ReadLine();

                bool isSuccess = LibLoginAuthenticator.LoginAuthenticator.Authenticate(username, password);
                if (isSuccess)
                {
                    Console.WriteLine("Hurray, Your credentials are correct!");
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid credentials.\n");
                }

            } while (true);
        }
    }
}

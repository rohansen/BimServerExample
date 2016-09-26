using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BimServerAPI
{
    public class LoginParameters : Parameters
    {
        public LoginParameters(string username, string password)
        {
            Username = username;
            Password = password;
        }
        public string Username { get; set; }
        public string Password { get; set; }
    }


    //Responses
    public class LoginResponse
    {
        public LoginData Response { get; set; }
    }

    public class LoginData
    {
        public string Result { get; set; }
    }
}

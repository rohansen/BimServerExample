using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BimServerAPI
{
    public class BimServerRequest
    {

        public BimServerRequest(string token, Request request)
        {
            Token = token;
            Request = request;
        }

        public string Token { get; set; }
        public Request Request { get; set; }
    }

    public class Request
    {
        public string @Interface { get; set; }
        public string Method { get; set; }
        public Parameters Parameters { get; set; }
    }

    public abstract class Parameters
    {

    }

 




}

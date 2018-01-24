using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WebSocket
{
    class Program
    {
        static void Main(string[] args)
        {
            // to create new one:
            WebServer server = new WebServer();
            // to start it
            server.start(IPAddress.Parse("127.0.0.1"), 5055, 100, "");
            Console.WriteLine("127.0.0.1:5055 is open...");
            // to stop it
            //server.stop();
        }
    }
}

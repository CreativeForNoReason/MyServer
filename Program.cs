using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocketSharp.Server;

namespace MyServer
{
    class Program
    {
        static void Main(string[] args)
        {
            WebSocketServer wssv = new WebSocketServer("ws://127.0.0.1:80");

            // attaches the behaviour under particular route
            wssv.AddWebSocketService<CodeGenerator>("/Generate");
            wssv.AddWebSocketService<UseCode>("/Use");

            wssv.Start();
            Console.WriteLine("Ws server started on ws://127.0.0.1:80/Generate");
            Console.WriteLine("Ws server started on ws://127.0.0.1:80/Use");

            Console.ReadKey();
            wssv.Stop();
        }
    }
}

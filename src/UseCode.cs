using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace MyServer
{
    class UseCode : WebSocketBehavior
    {
        protected override void OnMessage(MessageEventArgs e)
        {
            var storedCodes = XMLService.ReadXML<string>();
            byte[] output = new byte[1];

            if (storedCodes.Contains(e.Data.ToString()))
            {
                output[0] = 1;
            }
            else
            {
                output[0] = 2;
                Console.WriteLine("Try this code: " + storedCodes[0]);
            }

            Send(output);
        }
    }
}

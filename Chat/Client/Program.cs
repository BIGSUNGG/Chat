using System.Collections.Generic;
using System;
using System.Threading;
using System.Text;
using WebSocketSharp;

namespace Client
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Client Start");

            string url = "ws://localhost:5000/chat"; 
            var client = new WebSocketClient(url);

            client.Connect();

            Console.WriteLine("Type a message and press Enter to send it to the server.");
            Console.WriteLine("Type 'exit' to close the connection.");

            string message;
            while ((message = Console.ReadLine()) != null)
            {
                if (message.Equals("exit", StringComparison.OrdinalIgnoreCase))
                {
                    client.Close();
                    break;
                }
                else
                {
                    client.Send(message);
                }
            }
        }
    }
}
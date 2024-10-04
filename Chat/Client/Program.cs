using System.Collections.Generic;
using System;
using System.Threading;
using System.Text;
using WebSocketSharp;

namespace Client
{
    public class Program
    {
        public static void PrintManual()
        {
            Console.WriteLine("Type 'account' to open AccountWebService.");
            Console.WriteLine("Type 'chat' to open ChatWebService.");
            Console.WriteLine("Type 'exit' to close the connection.");
        }

        public static void Main(string[] args)
        {
            Console.WriteLine("Client Start");
            ClientWebService chat = null;

            PrintManual();

            {
                string message;
                while ((message = Console.ReadLine()) != null)
                {
                    if (message.Equals("chat", StringComparison.OrdinalIgnoreCase))
                    {
                        if (chat != null)
                            chat.Close();

                        PrintManual();

                        chat = new ChatWebService();
                        chat.Connect();
                    }
                    else if (message.Equals("account", StringComparison.OrdinalIgnoreCase))
                    {
                        if (chat != null)
                            chat.Close();

                        PrintManual();

                        chat = new AccountWebService();
                        chat.Connect();
                    }
                    else if (message.Equals("exit", StringComparison.OrdinalIgnoreCase))
                    {
                        if (chat != null) 
                            chat.Close();

                        break;
                    }
                    else
                    {
                        chat.Send(message);
                    }
                }
            }
        }
    }
}
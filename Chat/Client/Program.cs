using System.Collections.Generic;
using System;
using System.Threading;
using System.Text;
using WebSocketSharp;

namespace Client
{
    public class Program
    {
        static ClientWebService _curService = null;

        public static void ChangeService(ServiceType type)
        {
            if (_curService != null)
                _curService.Close();

            ClientWebService service = ClientWebService.Create(type);
            _curService = service;
            _curService.Connect();
        }

        public static void Main(string[] args)
        {
            Console.WriteLine("Client Start");
            ChangeService(ServiceType.account);

            string message;
            while ((message = Console.ReadLine()) != null)
            {
                if (_curService != null)
                    _curService.Input(message);
            }
        }
    }
}
using System.Collections.Generic;
using System;
using System.Threading;
using System.Text;
using WebSocketSharp.Server;

namespace Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Server Start");

            // 소켓 서버 생성
            var wssv = new WebSocketServer("ws://localhost:5000");

            // 서비스 추가
            wssv.AddWebSocketService<ServiceWebSocket>("/chat");

            // 서버 시작
            wssv.Start();
            Console.WriteLine("WebSocket server started at ws://localhost:5000/chat");

            // 키 입력 시 서버 중지
            Console.WriteLine("Press Enter key to stop the server...");
            Console.ReadLine();

            wssv.Stop();
            Console.WriteLine("WebSocket server stopped.");
        }
    }

}
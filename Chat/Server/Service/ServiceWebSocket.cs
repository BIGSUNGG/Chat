using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocketSharp;

public class ServiceWebSocket : ServerWebSocket
{
    #region Event
    protected override void OnOpen()
    {
        base.OnOpen();

        Console.WriteLine($"Client connected: {ID}");
        // 클라이언트에게 환영 메시지 전송
        Send("Welcome to the WebSocket server!");
    }

    protected override void OnMessage(MessageEventArgs e)
    {
        base.OnMessage(e);
        // 클라이언트로부터 메시지를 받으면 연결된 모든 클라이언트에게 브로드캐스트
        BroadcastExcept(e.Data, ID);
        Console.WriteLine($"Received from client [{ID}]: {e.Data}");
    }

    protected override void OnError(ErrorEventArgs e)
    {
        base.OnError(e);

        Console.WriteLine($"Error from client [{ID}]: {e.Message}");
    }

    protected override void OnClose(CloseEventArgs e)
    {
        base.OnClose(e);

        Console.WriteLine($"Client disconnected: {ID}");
    }
    #endregion
}

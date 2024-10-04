using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocketSharp;

public class AccountWebService : ServerWebService
{
    #region Event
    protected override void OnOpen()
    {
        base.OnOpen();

        Console.WriteLine($"Client connected: {ID}");

        Send("Welcome to the WebSocket server!");

        // 쿠키 수집
        var name = Context.CookieCollection["name"];
        if (name != null)
            Console.WriteLine($"{name.Value}");
    }

    protected override void OnMessage(MessageEventArgs e)
    {
        base.OnMessage(e);
        // 클라이언트로부터 메시지를 받으면 연결된 모든 클라이언트에게 브로드캐스트
        Console.WriteLine($"Received from client [{ID}]: {e.Data}");

        Send(e.Data);
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

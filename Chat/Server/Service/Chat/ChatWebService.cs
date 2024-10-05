using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocketSharp;

public class ChatWebService : ServerWebService
{
    string Id = string.Empty;
    string Password = string.Empty;

    #region Event
    protected override void OnOpen()
    {
        base.OnOpen();

        Console.WriteLine($"Client connected: {ID}");

        Id = Context.CookieCollection["Id"].Value;
        Password = Context.CookieCollection["Password"].Value;
        Sessions.Broadcast($"[{Id}] connected");
    }

    protected override void OnMessage(MessageEventArgs e)
    {
        base.OnMessage(e);        

        BroadcastExcept($"[{Id}] : {e.Data}", ID);
        Console.WriteLine($"Received from client [{Id}] : {e.Data}");
    }

    protected override void OnError(ErrorEventArgs e)
    {
        base.OnError(e);

        Console.WriteLine($"Error from client [{Id}] : {e.Message}");
    }

    protected override void OnClose(CloseEventArgs e)
    {
        base.OnClose(e);

        Sessions.Broadcast($"[{Id}] disconnected");
    }
    #endregion
}

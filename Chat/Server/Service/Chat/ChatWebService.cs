using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocketSharp;

public class ChatWebService : ServerWebService
{
    string _name = string.Empty;

    #region Event
    protected override void OnOpen()
    {
        base.OnOpen();

        Console.WriteLine($"Client connected: {ID}");

        _name = Context.CookieCollection["name"].Value;
        Sessions.Broadcast($"[{_name}] connected");
    }

    protected override void OnMessage(MessageEventArgs e)
    {
        base.OnMessage(e);        

        BroadcastExcept($"[{_name}] : {e.Data}", ID);
        Console.WriteLine($"Received from client [{_name}] : {e.Data}");
    }

    protected override void OnError(ErrorEventArgs e)
    {
        base.OnError(e);

        Console.WriteLine($"Error from client [{_name}] : {e.Message}");
    }

    protected override void OnClose(CloseEventArgs e)
    {
        base.OnClose(e);

        Sessions.Broadcast($"[{_name}] disconnected");
    }
    #endregion
}

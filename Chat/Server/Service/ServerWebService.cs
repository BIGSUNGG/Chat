using System;
using WebSocketSharp;
using WebSocketSharp.Server;

public class ServerWebService : WebSocketBehavior
{
    public virtual void BroadcastExcept(string data, string id)
    { 
        foreach(IWebSocketSession session in Sessions.Sessions)
        {
            if (session.ID == id)
                continue;

            session.Context.WebSocket.Send(data);
        }
    }

    #region Event
    protected override void OnOpen()
    {
        base.OnOpen();
    }

    protected override void OnMessage(MessageEventArgs e)
    {
        base.OnMessage(e);
    }

    protected override void OnError(ErrorEventArgs e)
    {
        base.OnError(e);
    }

    protected override void OnClose(CloseEventArgs e)
    {
        base.OnClose(e);
    }
    #endregion
}

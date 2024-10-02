using System;
using WebSocketSharp;
using WebSocketSharp.Server;

public class ServerWebSocket : WebSocketBehavior
{
    bool _notifyConnect = true;
    bool _notifyDisconnect = true;

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

        if(_notifyConnect)
        {
            BroadcastExcept($"Client connected: {ID}", ID);
        }
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

        if (_notifyDisconnect)
        {
            BroadcastExcept($"Client disconnected: {ID}", ID);
        }
    }
    #endregion
}

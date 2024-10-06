using System;
using WebSocketSharp;
using WebSocketSharp.Server;

public abstract class ServerWebService : WebSocketBehavior
{
    #region Action

    public virtual void Broadcast(string data)
    {
        Sessions.Broadcast(data);
    }

    public virtual void BroadcastExcept(string data, string id)
    { 
        foreach(IWebSocketSession session in Sessions.Sessions)
        {
            if (session.ID == id)
                continue;

            session.Context.WebSocket.Send(data);
        }
    }
    #endregion

    #region Cookie
    public abstract void LoadCookie();
    #endregion

    #region Event
    protected override void OnOpen()
    {
        base.OnOpen();

        LoadCookie();
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

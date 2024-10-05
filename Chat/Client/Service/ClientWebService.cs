using System;
using WebSocketSharp;
using WebSocketSharp.Net;

public abstract class ClientWebService
{
    protected WebSocket _ws;
    private string clientId;

    protected ClientWebService(string url)
    {
        _ws = new WebSocket(url);

        // sender = this
        _ws.OnOpen += (sender, e) => OnOpen();
        _ws.OnMessage += (sender, e) => OnMessage(e);
        _ws.OnClose += (sender, e) => OnClose(e);
        _ws.OnError += (sender, e) => OnError(e);
    }

    #region Action
    public abstract void Input(string data);

    public virtual void Connect()
    {
        LoadCookie();

        _ws.Connect();
    }

    public virtual void Send(string data)
    {
        _ws.Send(data);
    }

    public virtual void Close()
    {
        _ws.Close();
    }
    #endregion

    #region Cookie
    public abstract void SaveCookie();
    public abstract void LoadCookie();
    #endregion

    #region Event
    protected virtual void OnOpen()
    {
        Console.WriteLine($"Connected to the {this.GetType().Name}.");
    }

    protected virtual void OnMessage(MessageEventArgs e)
    {
        Console.WriteLine($"Received from {this.GetType().Name} : {e.Data}");
    }

    protected virtual void OnError(ErrorEventArgs e)
    {
        Console.WriteLine($"Error : {e.Message}");
    }

    protected virtual void OnClose(CloseEventArgs e)
    {
        SaveCookie();
        Console.WriteLine($"Disconnected from the {this.GetType().Name}.");
    }
    #endregion

    #region Factory
    static public ClientWebService Create(ServiceType type)
    {
        if (type == ServiceType.account)
            return new AccountWebService();
        else if (type == ServiceType.chat)
            return new ChatWebService();

        return null;
    }
    #endregion
}

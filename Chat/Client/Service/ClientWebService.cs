using System;
using WebSocketSharp;
using WebSocketSharp.Net;

public class ClientWebService
{
    protected WebSocket ws;
    private string clientId;

    protected ClientWebService(string url)
    {
        ws = new WebSocket(url);

        // sender = this
        ws.OnOpen += (sender, e) => OnOpen();
        ws.OnMessage += (sender, e) => OnMessage(e);
        ws.OnClose += (sender, e) => OnClose(e);
        ws.OnError += (sender, e) => OnError(e);
    }

    public void Connect()
    {
        LoadCookie();

        ws.Connect();
    }

    public void Send(string data)
    {
        ws.Send(data);
    }

    public void Send(byte[] data)
    {
        ws.Send(data);
    }

    public void Close()
    {
        ws.Close();
    }

    public virtual void SaveCookie()
    {
    }

    public virtual void LoadCookie()
    {
    }

    #region Event
    protected virtual void OnOpen()
    {
        Console.WriteLine($"Connected to the server.");
    }

    protected virtual void OnMessage(MessageEventArgs e)
    {
        Console.WriteLine($"Received from server : {e.Data}");
    }

    protected virtual void OnError(ErrorEventArgs e)
    {
        Console.WriteLine($"Error : {e.Message}");
    }

    protected virtual void OnClose(CloseEventArgs e)
    {
        SaveCookie();
        Console.WriteLine("Disconnected from the server.");
    }
    #endregion
}

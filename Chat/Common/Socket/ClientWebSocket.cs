using System;
using WebSocketSharp;

public class WebSocketClient
{
    private WebSocket ws;
    private string clientId;

    public WebSocketClient(string url)
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

    #region Event
    private void OnOpen()
    {
        Console.WriteLine($"Connected to the server.");
    }

    private void OnMessage(MessageEventArgs e)
    {
        Console.WriteLine($"Received from server: {e.Data}");

        // 서버로부터 자신의 ID를 전달받았는지 확인
        if (e.Data.StartsWith("Your ID is "))
        {
            clientId = e.Data.Substring("Your ID is ".Length);
            Console.WriteLine($"My client ID is: {clientId}");
        }
    }

    private void OnError(ErrorEventArgs e)
    {
        Console.WriteLine($"Error: {e.Message}");
    }

    private void OnClose(CloseEventArgs e)
    {
        Console.WriteLine("Disconnected from the server.");
    }
    #endregion
}

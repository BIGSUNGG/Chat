using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocketSharp;

public class ChatWebService : ServerWebService
{
    AccountCookieHandler _account = null;
    string _name = string.Empty;

    public ChatWebService()
    {
    }

    #region Cookie
    public override void LoadCookie()
    {
        if(_account == null)
            _account = new AccountCookieHandler(Context);

        _account.LoadCookie();
    }
    #endregion

    #region Event
    protected override void OnOpen()
    {
        base.OnOpen();

        Console.WriteLine($"Client connected: {ID}");

        using (ChatDbContext db = new ChatDbContext())
        {
            AccountDb account = db.Accounts.Where(a => a.Id == _account.Id.Value && a.Password == _account.Password.Value).FirstOrDefault();

            // 존재하지 않는 계정 정보를 받으면 연결 끊기
            if (account == null)
            {
                Context.WebSocket.Close(CloseStatusCode.InvalidData);
                return;
            }

            _name = account.Name;
        }

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

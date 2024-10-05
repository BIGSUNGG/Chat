using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocketSharp;
using WebSocketSharp.Net;

public class ChatWebService : ClientWebService
{
    AccountCookieHandler _account = null;

    public ChatWebService() : base("ws://localhost:5000/" + ServiceType.chat)
    {
        _account = new AccountCookieHandler(_ws);
    }

    #region Action
    public override void Input(string data)
    {
        Send(data);
    }
    #endregion

    #region Cookie
    public override void SaveCookie()
    {
        _account.SaveCookie();
    }

    public override void LoadCookie()
    {
        _account.LoadCookie();
    }
    #endregion

    #region Event
    #endregion
}

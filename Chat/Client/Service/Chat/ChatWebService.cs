using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocketSharp;
using WebSocketSharp.Net;

public class ChatWebService : ClientWebService
{
    AccountCookieHandler account = new AccountCookieHandler();

    public ChatWebService() : base("ws://localhost:5000/" + ServiceType.chat)
    {

    }

    public override void SaveAllCookie()
    {
        base.SaveAllCookie();

        account.SaveCookie();
    }

    public override void LoadAllCookie()
    {
        base.LoadAllCookie();

        var cookies = account.LoadCookie();
        if (cookies != null)
        {
            foreach (var cookie in cookies)
            {
                ws.SetCookie(cookie);
            }
        }
    }

    #region Event
    #endregion
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocketSharp;
using WebSocketSharp.Net;

public class AccountWebService : ClientWebService
{
    AccountCookieHandler account = new AccountCookieHandler();

    public AccountWebService() : base("ws://localhost:5000/" + ServiceType.account)
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
    protected override void OnMessage(MessageEventArgs e)
    {
        base.OnMessage(e);

        account.Name = e.Data;
        SaveAllCookie();
    }

    protected override void OnClose(CloseEventArgs e)
    {
        base.OnClose(e);
    }
    #endregion
}

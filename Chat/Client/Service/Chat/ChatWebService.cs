using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocketSharp;
using WebSocketSharp.Net;

public class ChatWebService : ClientWebService
{
    public ChatWebService() : base("ws://localhost:5000/" + ServiceType.chat)
    {

    }

    public override void SaveCookie()
    {
        base.SaveCookie();
    }

    public override void LoadCookie()
    {
        base.LoadCookie();

        var cookies = CookieManager.Instance.LoadCookies(CookieType.account);
        foreach (var cookie in cookies)
        {
            ws.SetCookie(cookie);
        }
    }

    #region Event
    #endregion
}

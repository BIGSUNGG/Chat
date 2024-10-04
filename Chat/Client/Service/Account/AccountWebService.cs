using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocketSharp;
using WebSocketSharp.Net;

public class AccountWebService : ClientWebService
{
    string _name = "BIGSUNGG";

    public AccountWebService() : base("ws://localhost:5000/" + ServiceType.account)
    {
    }

    public override void SaveCookie()
    {
        base.SaveCookie();

        List<Cookie> cookies = new List<Cookie>();

        // Name
        {
            Cookie cookie = new Cookie()
            {
                Name = "Name",
                Value = _name,
            };

            cookies.Add(cookie);
        }

        CookieManager.Instance.SaveCookies(cookies, CookieType.account);
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
    protected override void OnMessage(MessageEventArgs e)
    {
        base.OnMessage(e);

        _name = e.Data;
        SaveCookie();
    }

    protected override void OnClose(CloseEventArgs e)
    {
        base.OnClose(e);
    }
    #endregion
}

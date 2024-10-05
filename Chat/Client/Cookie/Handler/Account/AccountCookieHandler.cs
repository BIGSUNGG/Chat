using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using WebSocketSharp;
using WebSocketSharp.Net;

public class AccountCookieHandler : ClientCookieHandler
{
    public Cookie AutoLogin = null;
    public Cookie Id = null;
    public Cookie Password = null;

    public AccountCookieHandler(WebSocket ws) : base(ws)
    {
    }

    public override void SaveCookie()
    {
        base.SaveCookie();

        List<Cookie> cookies = new List<Cookie>();
        cookies.Add(AutoLogin);
        cookies.Add(Id);
        cookies.Add(Password);

        CookieManager.Instance.SaveCookies(cookies, CookieType.account);
    }

    public override void LoadCookie()
    {
        base.LoadCookie();

        List<Cookie> cookies = CookieManager.Instance.LoadCookies(CookieType.account);

        if (cookies!=null)
        { 
            foreach (var cookie in cookies)
            {
                if (cookie == null)
                    continue;

                switch (cookie.Name)
                {
                    case "AutoLogin":
                        AutoLogin = cookie;
                        break;
                    case "Id":
                        Id = cookie;
                        break;
                    case "Password":
                        Password = cookie;
                        break;
                    default:
                        Console.WriteLine("unknown cookie");
                        break;
                }
            }

            foreach (var cookie in cookies)
            {
                if (cookie == null)
                    continue;
                _ws.SetCookie(cookie);
            }
        }
    }

    public override void ResetCookie()
    {
        AutoLogin = new Cookie()
        {
            Name = "AutoLogin",
            Value = "false"
        };

        Id = new Cookie()
        {
            Name = "Id",
            Value = string.Empty
        };

        Password = new Cookie()
        {
            Name = "Password",
            Value = string.Empty
        };
    }
}
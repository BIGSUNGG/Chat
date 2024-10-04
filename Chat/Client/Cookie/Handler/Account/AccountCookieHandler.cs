using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using WebSocketSharp.Net;

public class AccountCookieHandler : CookieHandler
{
    public string Name = string.Empty;

    public override void SaveCookie()
    {
        List<Cookie> cookies = new List<Cookie>();

        // Name
        {
            Cookie cookie = new Cookie()
            {
                Name = "Name",
                Value = Name,
            };

            cookies.Add(cookie);
        }

        CookieManager.Instance.SaveCookies(cookies, CookieType.account);
    }

    public override List<Cookie> LoadCookie()
    {
        List<Cookie> cookies = CookieManager.Instance.LoadCookies(CookieType.account);

        // Name
        {
            Cookie name = cookies.Where(c => c.Name == "Name").FirstOrDefault();
            if (name != null)
                this.Name = name.Value;
        }

        return cookies;
    }
}
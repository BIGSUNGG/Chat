using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocketSharp.Net;

public abstract class CookieHandler
{
    public abstract void SaveCookie();
    public abstract List<Cookie> LoadCookie();
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocketSharp;
using WebSocketSharp.Net;

public abstract class ClientCookieHandler
{
    protected WebSocket _ws = null;

    public ClientCookieHandler(WebSocket ws)
    {
        _ws = ws;
    }

    public virtual void SaveCookie()
    {
    }

    public virtual void LoadCookie()
    {
        ResetCookie();
    }

    public abstract void ResetCookie();
}
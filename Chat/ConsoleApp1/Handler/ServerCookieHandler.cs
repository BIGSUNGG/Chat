using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocketSharp;
using WebSocketSharp.Net;
using WebSocketSharp.Server;
using WebSocketSharp.Net.WebSockets;

public abstract class ServerCookieHandler
{
    protected WebSocketContext _context = null;

    public ServerCookieHandler(WebSocketContext context)
    {
        _context = context;
    }

    public virtual void LoadCookie()
    {
        ResetCookie();
    }

    public abstract void ResetCookie();
}
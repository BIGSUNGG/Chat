using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using WebSocketSharp;
using WebSocketSharp.Net;
using WebSocketSharp.Server;
using WebSocketSharp.Net.WebSockets;
using Microsoft.EntityFrameworkCore;

public class AccountCookieHandler : ServerCookieHandler
{
    public Cookie Id = null;
    public Cookie Password = null;

    public AccountCookieHandler(WebSocketContext context) : base(context)
    {
    }

    public override void LoadCookie()
    {       
        // Id
        {
            Id = _context.CookieCollection["Id"];
            if (Id == null)
                Id = new Cookie();
        }

        // Password
        {
            Password = _context.CookieCollection["Password"];
            if (Password == null)
                Password = new Cookie();
        }
    }

    public override void ResetCookie()
    {
        Id = new Cookie();
        Password = new Cookie();
    }
}

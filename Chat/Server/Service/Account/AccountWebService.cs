﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using WebSocketSharp;

public class AccountWebService : ServerWebService
{
    AccountCookieHandler _account = null;

    string _name = string.Empty;

    public AccountWebService()
    {
    }

    #region Cookie
    public override void LoadCookie()
    {
        if (_account == null)
            _account = new AccountCookieHandler(Context);

        _account.LoadCookie();
    }
    #endregion

    #region Event
    protected override void OnOpen()
    {
        base.OnOpen();

        Console.WriteLine($"Client connected: {ID}");
    }

    protected override void OnMessage(MessageEventArgs e)
    {
        base.OnMessage(e);
        // 클라이언트로부터 메시지를 받으면 연결된 모든 클라이언트에게 브로드캐스트
        Console.WriteLine($"Received from client [{ID}]: {e.Data}");

        MessageReader _accountMessageReader = new MessageReader(e.Data);

        if (Enum.TryParse(_accountMessageReader.MessageType, out AccountMessageType messageType))
        {
            switch(messageType)
            {
                case AccountMessageType.Create:
                    using (ChatDbContext db = new ChatDbContext())
                    {
                        string type = _accountMessageReader.MessageType.ToString();
                        string name = _accountMessageReader.Messages[0].ToString();
                        string id = _accountMessageReader.Messages[1].ToString();
                        string password = _accountMessageReader.Messages[2].ToString();

                        AccountDb account =  db.Accounts.Where(a => a.Id == id).FirstOrDefault();
                        if (account != null)
                        {
                            Console.WriteLine("Create account failed");
                            return;
                        }

                        account = new AccountDb();
                        account.Name = name;
                        account.Id = id;
                        account.Password = password;
                        db.Accounts.Add(account);

                        _name = name;
                        db.SaveChanges();
                        Console.WriteLine("Create account succeed");

                        Send(e.Data);
                    }
                    break;
                case AccountMessageType.Login:
                    using (ChatDbContext db = new ChatDbContext())
                    {
                        string type = _accountMessageReader.MessageType.ToString();
                        string id = _accountMessageReader.Messages[0].ToString();
                        string password = _accountMessageReader.Messages[1].ToString();

                        AccountDb account = db.Accounts.Where(a => a.Id == id && a.Password == password).FirstOrDefault();
                        if (account == null)
                        {
                            Console.WriteLine($"{_name} Account Login failed");
                            return;
                        }

                        _name = account.Name;
                        Console.WriteLine($"{_name} Account Login succeed");

                        Send(e.Data);
                    }
                    break;
                case AccountMessageType.Logout:
                    break;
                default:
                    break;
            }
        }
    }

    protected override void OnError(ErrorEventArgs e)
    {
        base.OnError(e);

        Console.WriteLine($"Error from client [{ID}]: {e.Message}");
    }

    protected override void OnClose(CloseEventArgs e)
    {
        base.OnClose(e);

        Console.WriteLine($"Client disconnected: {ID}");
    }
    #endregion
}

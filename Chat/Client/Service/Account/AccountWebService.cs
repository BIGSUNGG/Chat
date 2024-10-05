using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using WebSocketSharp;
using WebSocketSharp.Net;
using Client;

public class AccountWebService : ClientWebService
{
    AccountCookieHandler _account = null;
    MessageWriter _accountWriter = new MessageWriter();
    int _messageCount = int.MaxValue;

    public AccountWebService() : base("ws://localhost:5000/" + ServiceType.account)
    {
        _account = new AccountCookieHandler(_ws);
    }

    #region Action
    public override void Input(string data)
    {
        if(_accountWriter.MessageType == string.Empty)
        {
            if(Enum.TryParse(data, out AccountMessageType type))
            {
                switch(type)
                {
                    case AccountMessageType.Create:
                        _messageCount = 3;

                        Console.WriteLine("     Input Create Account Info     ");
                        Console.WriteLine("+-------------+ +----+ +----------+");
                        Console.WriteLine("| User Name   | | Id | | Password |");
                        Console.WriteLine("+-------------+ +----+ +----------+");
                        break;
                    case AccountMessageType.Login:
                        _messageCount = 2;

                        Console.WriteLine("     Input Login Account Info     ");
                        Console.WriteLine("       +----+ +----------+");
                        Console.WriteLine("       | Id | | Password |");
                        Console.WriteLine("       +----+ +----------+");

                        // 자동 로그인
                        if (_account.AutoLogin.Value != "true")
                            break;

                        _accountWriter.WriteMessage(_account.Id.Value);
                        _accountWriter.WriteMessage(_account.Password.Value);
                        Console.WriteLine(_account.Id.Value);
                        Console.WriteLine(_account.Password.Value);

                        break;
                    case AccountMessageType.Logout:
                        _messageCount = int.MaxValue;

                        // 자동 로그인 취소
                        _account.AutoLogin.Value = "false";
                        _account.SaveCookie();
                        return;
                    default:
                        Console.WriteLine("Need to implementation");
                        break;
                }

                _accountWriter.WriteMessageType(data);
            }
            else
            {
                Console.WriteLine("Choose wrong action");
            }
        }
        else
        {
            _accountWriter.WriteMessage(data);
        }

        if (_accountWriter.Messages.Count >= _messageCount)
        {
            Send(_accountWriter.ToMessage());
            _accountWriter.Clear();
            _messageCount = int.MaxValue;
        }
    }
    #endregion

    #region Cookie
    public override void SaveCookie()
    {
        _account.SaveCookie();
    }

    public override void LoadCookie()
    {
        _account.LoadCookie();
    }
    #endregion

    #region Event
    protected override void OnOpen()
    {
        base.OnOpen();

        Console.WriteLine("           Choose Action           ");
        Console.WriteLine("+---------+ +---------+ +---------+");
        Console.WriteLine("| Create  | |  Login  | |  Logout |");
        Console.WriteLine("+---------+ +---------+ +---------+");
    }

    protected override void OnMessage(MessageEventArgs e)
    {
        base.OnMessage(e);

        MessageReader reader = new MessageReader(e.Data);
        if (Enum.TryParse(reader.MessageType, out AccountMessageType type))
        {
            switch (type)
            {
                case AccountMessageType.Create:
                    _account.AutoLogin.Value = "true";
                    _account.Id.Value = reader.Messages[1];
                    _account.Password.Value = reader.Messages[2];
                    break;
                case AccountMessageType.Login:
                    _account.AutoLogin.Value = "true";
                    _account.Id.Value = reader.Messages[0];
                    _account.Password.Value = reader.Messages[1];
                    break;
                case AccountMessageType.Logout:
                    _account.AutoLogin.Value = "false";
                    break;
                default:
                    Console.WriteLine("Need to implementation");
                    break;
            }
        }

        SaveCookie();
        Program.ChangeService(ServiceType.chat);
    }
    #endregion
}

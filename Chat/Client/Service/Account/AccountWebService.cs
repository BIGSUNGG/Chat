using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using WebSocketSharp;
using WebSocketSharp.Net;

public class AccountWebService : ClientWebService
{
    AccountCookieHandler _account = null;
    MessageWriter _accountMessage = new MessageWriter();

    public AccountWebService() : base("ws://localhost:5000/" + ServiceType.account)
    {
        _account = new AccountCookieHandler(_ws);
    }

    #region Action
    public override void Send(string data)
    {
        if(_accountMessage.MessageType == string.Empty)
        {
            // 메시지 타입이 적절하지 않은 경우
            if (Enum.TryParse(data, out AccountMessageType type) == false)
            {
                Console.WriteLine("Wrong Type");
                return;
            }

            _accountMessage.WriteMessage(data);
        }
        else
        {
            _accountMessage.WriteMessage(data);
        }

        // 전송할 메시지가 모두 모인 경우
        if(_accountMessage.Messages.Count == 3)
        {
            // 1. Type
            // 2. Name
            // 3. ID
            // 4. Password
            _ws.Send(_accountMessage.ToString());
            _account.Id.Value = _accountMessage.Messages[1];
            _account.Password.Value = _accountMessage.Messages[2];
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
    protected override void OnMessage(MessageEventArgs e)
    {
        base.OnMessage(e);

        SaveCookie();
    }

    protected override void OnClose(CloseEventArgs e)
    {
        base.OnClose(e);
    }
    #endregion
}

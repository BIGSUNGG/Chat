using System;
using System.Collections.Generic;

public class MessageReader
{
    public readonly string MessageType = string.Empty;
    public readonly List<string> Messages = new List<string>();

    public MessageReader(string messages) 
    {
        List<string> resultList = new List<string>();

        string[] messageArr = messages.Split(',');
        if (messageArr.Length == 0)
            return;

        MessageType = messageArr[0];
        for (int i = 1; i < messageArr.Length; i++)
            Messages.Add(messageArr[i]);
    }

    public string ToMessage()
    {
        string result = "";
        result += MessageType;
        foreach(string message in Messages)
        {
            result += ("," + message);
        }

        return result;
    }
}
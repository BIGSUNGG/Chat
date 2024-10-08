﻿using System.Collections.Generic;

public class MessageWriter
{
    public string MessageType => _messageType;
    public List<string> Messages => _messages;

    private string _messageType = string.Empty;
    private List<string> _messages = new List<string>();

    #region Init
    public MessageWriter() { }

    public MessageWriter(string messageType) : this()
    {
        _messageType = messageType;
    }

    public MessageWriter(string messageType, List<string> messages) : this(messageType)
    {
        _messages = messages;
    }
    #endregion

    public void WriteMessageType(string messageType)
    {
        _messageType = messageType;
    }

    public void WriteMessage(string message)
    {
        _messages.Add(message);
    }

    public string ToMessage()
    {
        string result = "";
        result += _messageType;
        foreach(string message in _messages)
        {
            result += ("," + message);
        }
        return result;
    }

    public void Clear()
    {
        _messageType = string.Empty;
        _messages.Clear();
    }
}
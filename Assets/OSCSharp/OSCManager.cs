using System;
using System.Collections.Generic;
using OSCsharp.Data;
using OSCsharp.Net;
using OSCsharp.Utils;
using UnityEngine;

public class OSCManager
{
    private UDPReceiver udpReceiver;

    public int Port { get; private set; }

    public event EventHandler<OSCEventArgs> OnData;

    public OSCManager() : this(3333)
    { }

    public OSCManager(int port)
    {
        Port = port;

        udpReceiver = new UDPReceiver(Port, false);
        udpReceiver.MessageReceived += handlerOscMessageReceived;
        udpReceiver.ErrorOccured += handlerOscErrorOccured;
    }

    public void Connect()
    {
        if (!udpReceiver.IsRunning) udpReceiver.Start();
    }

    public void Disconnect()
    {
        if (udpReceiver.IsRunning) udpReceiver.Stop();
    }

    private void parseOscMessage(OscMessage message)
    {
        switch (message.Address)
        {
            case "/tuio/2Dcur":
                if (message.Data.Count == 0) return;
                var command = message.Data[0].ToString();
                switch (command)
                {
                    case "set":
                        if (message.Data.Count < 4) return;
                        var id = (int)message.Data[1];
                        var xPos = (float)message.Data[2];
                        var yPos = (float)message.Data[3];
                        break;
                    case "fseq":
                        if (message.Data.Count < 2) return;
                        //if (OnData != null) OnData(this, new OSCEventArgs(message.Data));
                        if (OnData != null) OnData(this, new OSCEventArgs(message.Data.ToString()));
                        break;
                }
                break;
        }
    }

    private void handlerOscErrorOccured(object sender, ExceptionEventArgs exceptionEventArgs)
    {
        Debug.Log("OSC Error: " + exceptionEventArgs.ToString());
    }

    private void handlerOscMessageReceived(object sender, OscMessageReceivedEventArgs oscMessageReceivedEventArgs)
    {
        parseOscMessage(oscMessageReceivedEventArgs.Message);
    }
}

public class OSCEventArgs : EventArgs
{
    public string myCustomVar;

    public OSCEventArgs(string myCustomVar)
    {
        //Cursor = myCustomVar;
    }
}
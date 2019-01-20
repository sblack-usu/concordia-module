using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
//using OSCsharp.Net;
//using OSCsharp.Data;

public struct PlanetPacket
{
    public PlanetPacket(string date, Vector3 coordinates)
    {
        this.date = date;
        this.coordinates = coordinates;
    }
    string date;
    Vector3 coordinates;

    public string GetDate()
    {
        return date;
    }

    public Vector3 GetCoordinates()
    {
        return coordinates;
    }
}
/*
public class ConcordiaOSCReceiver : MonoBehaviour {


    public enum OscMode { SEND, RECEIVE, SEND_RECEIVE };
    public OscMode oscMode = OscMode.RECEIVE;
    public string outIP = "127.0.0.1";
    public int outPort = 9999;
    public int inPort = 4002;

    private UDPReceiver udpReceiver;
    private UDPTransmitter udpTransmitter;
    private int bufferSize = 100; // Buffer size of the application (stores 100 messages from different servers)
    private int rxBufferSize = 1024;
    private int sleepMs = 10;

    public delegate void PacketReceiveAction(PlanetPacket p0, PlanetPacket p1);
    public static event PacketReceiveAction OnReceive;

    void Start()
    {
        Debug.Log("Starting up");

        // Initialize OSC clients (transmitters)
        if (oscMode == OscMode.SEND || oscMode == OscMode.SEND_RECEIVE)
        {
            udpTransmitter = new UDPTransmitter(outIP, outPort);
            udpTransmitter.Connect();
        }

        if (oscMode == OscMode.RECEIVE || oscMode == OscMode.SEND_RECEIVE)
        {
            Debug.Log("Setup receiving");
            udpReceiver = new UDPReceiver(inPort, false);
            udpReceiver.MessageReceived += receivedOSC;
            udpReceiver.Start();

            Debug.Log("Finished setting up receiving");
        }
    }

    // Reads all the messages received between the previous update and this one
    void Update()
    {

        // Send random number to the client
        if (oscMode == OscMode.SEND || oscMode == OscMode.SEND_RECEIVE)
        {
            float randVal = UnityEngine.Random.Range(0f, 0.7f);
            OscMessage msg = new OscMessage("/test", randVal);
            udpTransmitter.Send(msg);
            //OscPacket packet = new OscPacket()
            //udpTransmitter.Send()
            //OSCHandler.Instance.SendMessageToClient("myClient", "/1/fader1", randVal);
        }

        if (OnReceive != null)
        {
            while (planets.Count != 0)
            {
                PlanetPacket[] p = planets.Dequeue();
                OnReceive(p[0], p[1]);
            }
        }
    }

    Queue<PlanetPacket[]> planets = new Queue<PlanetPacket[]>();

    // Process OSC message
    private void receivedOSC(object sender, OscMessageReceivedEventArgs oscMessageReceivedEventArgs)
    {
        OscMessage msg = oscMessageReceivedEventArgs.Message;
        
        Debug.Log("received message");
        if (msg.Data.Count == 0)
        {
            Debug.Log("Empty packet");
            return;
        }
        
        string date = (string)msg.Data[0];
        PlanetPacket p0 = new PlanetPacket(date, new Vector3((float)msg.Data[1], (float)msg.Data[2], (float)msg.Data[3]));
        PlanetPacket p1 = new PlanetPacket(date, new Vector3((float)msg.Data[4], (float)msg.Data[5], (float)msg.Data[6]));
        planets.Enqueue(new PlanetPacket[] { p0, p1 });

        /*
        // Origin
        int serverPort = pckt.server.ServerPort;

        // Address
        string address = pckt.Address.Substring(1);

        // Data at index 0
        string data0 = pckt.Data.Count != 0 ? pckt.Data[0].ToString() : "null";

        // Print out messages
        Debug.Log("Input port: " + serverPort.ToString() + "\nAddress: " + address + "\nData [0]: " + data0);
		
    }
}
*/
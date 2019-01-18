using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityOSC;

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

public class ConcordiaOSCReceiver : MonoBehaviour {


    public enum OscMode { SEND, RECEIVE, SEND_RECEIVE };
    public OscMode oscMode = OscMode.RECEIVE;
    public string outIP = "127.0.0.1";
    public int outPort = 9999;
    public int inPort = 4002;

    private OSCServer myServer;
    private int bufferSize = 100; // Buffer size of the application (stores 100 messages from different servers)
    private int rxBufferSize = 1024;
    private int sleepMs = 10;

    public delegate void PacketReceiveAction(PlanetPacket p0, PlanetPacket p1);
    public static event PacketReceiveAction OnReceive;

    void Start()
    {
        // init OSC
        OSCHandler.Instance.Init();

        // Initialize OSC clients (transmitters)
        if (oscMode == OscMode.SEND || oscMode == OscMode.SEND_RECEIVE)
        {
            OSCHandler.Instance.CreateClient("myClient", IPAddress.Parse(outIP), outPort);
        }

        if (oscMode == OscMode.RECEIVE || oscMode == OscMode.SEND_RECEIVE)
        {
            // Initialize OSC servers (listeners)
            myServer = OSCHandler.Instance.CreateServer("myServer", inPort);
            // Set buffer size (bytes) of the server (default 1024)
            myServer.ReceiveBufferSize = rxBufferSize;
            // Set the sleeping time of the thread (default 10)
            myServer.SleepMilliseconds = sleepMs;
        }
    }

    // Reads all the messages received between the previous update and this one
    void Update()
    {
        if (oscMode == OscMode.RECEIVE || oscMode == OscMode.SEND_RECEIVE)
        {
            // Read received messages
            for (var i = 0; i < OSCHandler.Instance.packets.Count; i++)
            {
                // Process OSC
                receivedOSC(OSCHandler.Instance.packets[i]);
                // Remove them once they have been read.
                OSCHandler.Instance.packets.Remove(OSCHandler.Instance.packets[i]);
                i--;
            }
        }

        // Send random number to the client
        if (oscMode == OscMode.SEND || oscMode == OscMode.SEND_RECEIVE)
        {
            float randVal = UnityEngine.Random.Range(0f, 0.7f);
            OSCHandler.Instance.SendMessageToClient("myClient", "/1/fader1", randVal);
        }
    }

    // Process OSC message
    private void receivedOSC(OSCPacket pckt)
    {
        if (pckt == null)
        {
            Debug.Log("Empty packet");
            return;
        }

        float x = 0f;
        OSCMessage msg = pckt.Data[0] as UnityOSC.OSCMessage;
        string date = (string)msg.Data[0];
        PlanetPacket p0 = new PlanetPacket(date, new Vector3((float)msg.Data[1], (float)msg.Data[2], (float)msg.Data[3]));
        PlanetPacket p1 = new PlanetPacket(date, new Vector3((float)msg.Data[4], (float)msg.Data[5], (float)msg.Data[6]));
        //Debug.Log(date + " - " + planetIndex + " " + coordinates);

        if (OnReceive != null)
        {
            OnReceive(p0, p1);
        }

        /*
        // Origin
        int serverPort = pckt.server.ServerPort;

        // Address
        string address = pckt.Address.Substring(1);

        // Data at index 0
        string data0 = pckt.Data.Count != 0 ? pckt.Data[0].ToString() : "null";

        // Print out messages
        Debug.Log("Input port: " + serverPort.ToString() + "\nAddress: " + address + "\nData [0]: " + data0);
		*/
    }
}

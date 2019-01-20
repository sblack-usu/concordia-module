using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.MagicLeap;

public class AllFeelsOSCReceiver : MonoBehaviour {

    public OSC osc;
	// Use this for initialization
	void Start ()
    {
        osc.SetAddressHandler("/date", receiveDraw);

        osc.SetAddressHandler("/glove", receiveGlove);
        MLPrivileges.Start();
    }

    public delegate void PacketReceiveAction(PlanetPacket p0, PlanetPacket p1);
    public static event PacketReceiveAction OnReceive;

    public delegate void GloveReceiveeAction(float code, float value);
    public static event GloveReceiveeAction OnGloveReceive;

    //public ConcordiaCSV csv;

    bool gloves = false;
    // Reads all the messages received between the previous update and this one
    void Update()
    {
        if (OnGloveReceive != null)
        {
            OnGloveReceive(0, 0);
            //csv.StartCoordinates();
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
    Queue<float[]> gloveSignals = new Queue<float[]>();

    // Process OSC message
    private void receiveDraw(OscMessage msg)
    {

        gloves = true;
        Debug.Log("received message " + msg.values[0]);
        if (msg.values.Count == 0)
        {
            Debug.Log("Empty packet");
            return;
        }

        string date = (string)msg.values[0];
        PlanetPacket p0 = new PlanetPacket(date, new Vector3((float)msg.values[1], (float)msg.values[2], (float)msg.values[3]));
        PlanetPacket p1 = new PlanetPacket(date, new Vector3((float)msg.values[4], (float)msg.values[5], (float)msg.values[6]));
        planets.Enqueue(new PlanetPacket[] { p0, p1 });
    }

    private void receiveGlove(OscMessage message)
    {
        if (message.values.Count == 0)
        {
            Debug.Log("Empty packet");
            return;
        }
        gloveSignals.Enqueue(new float[] { (float)message.values[0], (float)message.values[1] });
    }


    void Receive(OscMessage message)
    {
        Debug.Log("We got the message");
    }
}

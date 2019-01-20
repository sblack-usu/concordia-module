using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConcordiaCSV : MonoBehaviour {

    public float delay = 1f;
    public TextAsset coordinates;
    Queue<PlanetPacket[]> parsed = new Queue<PlanetPacket[]>();

    public delegate void PacketReceiveAction(PlanetPacket p0, PlanetPacket p1);
    public static event PacketReceiveAction OnReceive;
    
    void Start () {
        foreach(string l in coordinates.text.Split('\n'))
        {
            if(l != "" && l != null)
            {
                string[] data = l.Split(' ');
                parsed.Enqueue(new PlanetPacket[] { new PlanetPacket(data[1], new Vector3(float.Parse(data[2]), float.Parse(data[3]), float.Parse(data[4]))), new PlanetPacket(data[1], new Vector3(float.Parse(data[5]), float.Parse(data[6]), float.Parse(data[7]))) });
            }
        }
        StartCoroutine(WalkCoordinates());
    }
	
	IEnumerator WalkCoordinates () {
        while(parsed.Count != 0)
        {
            yield return new WaitForSeconds(delay);
            if (OnReceive != null)
            {
                PlanetPacket[] p = parsed.Dequeue();
                OnReceive(p[0], p[1]);
            }
        }
	}
    bool started = false;

    public void StartCoordinates()
    {
        Debug.Log("Starting Coordinates ");
        if (!started)
        {
            Debug.Log("Starting!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!! ");
            started = true;
            StartCoroutine(WalkCoordinates());
        }
    }
}

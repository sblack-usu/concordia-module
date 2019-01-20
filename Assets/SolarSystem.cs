using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolarSystem : MonoBehaviour {

    private void OnEnable()
    {
        //ConcordiaOSCReceiver.OnReceive += Receive;
        ConcordiaCSV.OnReceive += Receive;
        AllFeelsOSCReceiver.OnReceive += Receive;
    }

    private void OnDisable()
    {
        //ConcordiaOSCReceiver.OnReceive -= Receive;
        ConcordiaCSV.OnReceive -= Receive;
        AllFeelsOSCReceiver.OnReceive -= Receive;
    }
    
    List<Vector3[]> coordinates = new List<Vector3[]>();
    public List<LineRenderer> lines = new List<LineRenderer>();
    public int numLines = 100;
    public LineRenderer linePrefab;

    void Start()
    {
        lineParent.transform.parent = transform;
        //currentScale = scale;
        for (int i=0; i< numLines; i++)
        {
            LineRenderer line = Instantiate(linePrefab);
            line.name = i.ToString();
            line.transform.parent = lineParent.transform;
            lines.Add(line);
        }
    }

    int currentLineIndex = 0;
    public GameObject lineParent;

    void Update()
    {
        if(positionsQueue.Count > 0)
        {
            LineRenderer line = lines[currentLineIndex];
            currentLineIndex++;
            if(currentLineIndex >= lines.Count)
            {
                currentLineIndex = 0;
            }
            line.useWorldSpace = false;
            line.positionCount = 2;
            Vector3[] positions = positionsQueue.Dequeue();
            line.SetPosition(0, positions[0]);
            line.SetPosition(1, positions[1]);
            line.GetComponent<AlignCapsule>().Align(positions[0], positions[1]);

        }
    }

    Queue<Vector3[]> positionsQueue = new Queue<Vector3[]>();

    void Receive(PlanetPacket p0, PlanetPacket p1)
    {
        positionsQueue.Enqueue(new Vector3[] { p0.GetCoordinates(), p1.GetCoordinates() });
    }
}

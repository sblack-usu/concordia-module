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

    public TextMesh date;
    public float scale = 1f;
    float currentScale;
    public int numPlanets = 2;
    public Planet planetPrefab;
    List<Planet> planets = new List<Planet>();
    List<Vector3[]> coordinates = new List<Vector3[]>();
    List<LineRenderer> lines = new List<LineRenderer>();
    public int numLines = 100;
    public LineRenderer linePrefab;

    void Start()
    {
        currentScale = scale;
        for(int i=0; i< numLines; i++)
        {
            LineRenderer line = Instantiate(linePrefab);
            line.transform.parent = transform;
            lines.Add(line);
        }
        for (int i = 0; i < numPlanets; i++)
        {
            planets.Add(Instantiate(planetPrefab));
        }
    }

    string currentDate = "";
    string incomingDate = "";

    int currentLineIndex = 0;

    void Update()
    {
        if(!currentDate.Equals(incomingDate))
        {
            currentDate = incomingDate;
            LineRenderer line = lines[currentLineIndex];
            currentLineIndex++;
            if(currentLineIndex >= lines.Count)
            {
                currentLineIndex = 0;
            }
            line.positionCount = planets.Count;
            for(int i=0; i<planets.Count; i++)
            {
                //Debug.Log(i + " " + planets[i].transform.position * scale);
                line.SetPosition(i, planets[i].transform.position * scale);
            }
            line.SetWidth(scale * .5f, scale * .5f);
            coordinates.Add(new Vector3[] { planets[0].transform.position, planets[1].transform.position });

        }
        if(scale != currentScale)
        {
            for(int i=0; i<coordinates.Count; i++)
            {
                LineRenderer line = lines[i];
                line.SetPosition(0, coordinates[i][0] * scale);
                line.SetPosition(1, coordinates[i][1] * scale);
                line.SetWidth(scale * .5f, scale * .5f);
            }
            currentScale = scale;
        }
    }

    void Receive(PlanetPacket p0, PlanetPacket p1)
    {
        planets[0].transform.position = p0.GetCoordinates();
        planets[1].transform.position = p1.GetCoordinates();
        incomingDate = p0.GetDate();
        date.text = p0.GetDate();
    }
}

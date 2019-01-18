using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolarSystem : MonoBehaviour {

    private void OnEnable()
    {
        ConcordiaOSCReceiver.OnReceive += Receive;
    }

    private void OnDisable()
    {
        ConcordiaOSCReceiver.OnReceive -= Receive;
    }

    public int numPlanets = 2;
    public Planet planetPrefab;
    List<Planet> planets = new List<Planet>();
    List<LineRenderer> lines = new List<LineRenderer>();
    public int numLines = 100;
    public LineRenderer linePrefab;

    void Start()
    {
        for(int i=0; i< numLines; i++)
        {
            lines.Add(Instantiate(linePrefab));
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
                line.SetPosition(i, planets[i].transform.position);
            }

        }
    }

    void Receive(PlanetPacket p0, PlanetPacket p1)
    {
        planets[0].transform.position = p0.GetCoordinates();
        planets[1].transform.position = p1.GetCoordinates();
        incomingDate = p0.GetDate();
    }
}

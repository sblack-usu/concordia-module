using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LatkOscSender : MonoBehaviour {

    public OSC osc;
    public SolarSystem solarSystem;
    public float sendInterval = 1f;

    void Start()
    {
        StartCoroutine(SendLines());
    }

    IEnumerator SendLines() {
        foreach (LineRenderer line in solarSystem.lines) {
            Vector3[] points = new Vector3[line.positionCount];
            line.GetPositions(points);
            OscMessage message = new OscMessage();
            message.address = "/ccdaline";

            foreach (Vector3 point in points) {
                message.values.Add(point.x);
                message.values.Add(point.y);
                message.values.Add(point.z);
                //Debug.Log(point);
            }

            osc.Send(message);
            yield return new WaitForSeconds(sendInterval);
        }

    }
    
}

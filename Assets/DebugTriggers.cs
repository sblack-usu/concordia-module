using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugTriggers : MonoBehaviour {

    public OSC osc;
    public delegate void CollisionAction(int index, bool enter);
    public static event CollisionAction OnCollision;

    HashSet<string> intersectingLines = new HashSet<string>();
	void OnTriggerEnter(Collider col)
    {
        Debug.Log("Trigger Enter " + col.transform.parent.name);
        if (!intersectingLines.Contains(col.transform.parent.name))
        {
            OscMessage msg = new OscMessage();
            msg.address = "/collision/";
            msg.values = new ArrayList() { int.Parse(col.transform.parent.name) };
            osc.Send(msg);
        }
        intersectingLines.Add(col.transform.parent.name);
    }

    /*
    void OnTriggerExit(Collider col)
    {
        OscMessage msg = new OscMessage();
        msg.address = "/collision/";
        msg.values = new ArrayList() { int.Parse(col.transform.parent.name) };
        osc.Send(msg);
        Debug.Log("Trigger Exit " + col.transform.parent.name);
        intersectingLines.Remove(col.transform.parent.name);

    }
    */

    void Update()
    {
        //Debug.Log(intersectingLines.Count);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugTriggers : MonoBehaviour {

    HashSet<string> intersectingLines = new HashSet<string>();
	void OnTriggerEnter(Collider col)
    {
        Debug.Log("Trigger Enter " + col.transform.parent.name);
        intersectingLines.Add(col.transform.parent.name);
    }

    void OnTriggerExit(Collider col)
    {
        Debug.Log("Trigger Exit " + col.transform.parent.name);
        intersectingLines.Remove(col.transform.parent.name);
    }

    void Update()
    {
        Debug.Log(intersectingLines.Count);
    }
}

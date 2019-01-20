using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightUp : MonoBehaviour {

    LineRenderer line;
    Color startColor;
    Color endColor;

    public Color highlightStartColor;
    public Color highlightEndColor;

	void Start () {
        line = GetComponentInParent<LineRenderer>();
        startColor = line.startColor;
        endColor = line.endColor;
    }
	
	void OnTriggerEnter(Collider col)
    {
        line.startColor = highlightStartColor;
        line.endColor = highlightEndColor;
    }

    void OnTriggerExit(Collider col)
    {
        line.startColor = startColor;
        line.endColor = endColor;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SharpOSC;

public class OSCTest : MonoBehaviour {

    public int port = 4002;
    UDPListener li;

	void Start () {
        li = new UDPListener(port);
    }
	
	void Update () {

        var bundle = li.Receive();
        if (bundle != null)
        {
            //var msg = (bundle).Messages[0];
            //Debug.Log(msg.Arguments[0].ToString());
            Debug.Log(bundle);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorqueKeyboard : MonoBehaviour {

    public float forceMultiplier = 1f;
    Rigidbody rb;
    Vector3 rotationAxis = new Vector3(0, 0, 1);

	void Start () {
        rb = GetComponent<Rigidbody>();
	}
	
	void FixedUpdate () {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rb.AddTorque(rotationAxis * forceMultiplier);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            rb.AddTorque(rotationAxis * -forceMultiplier);
        }

    }
}

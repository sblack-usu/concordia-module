using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class KeyboardUpDown : MonoBehaviour {

    public float yLimit = 150;
    public float speed = 1f;

    Rigidbody rb;

	void Start () {
        rb = GetComponent<Rigidbody>();
	}
	
	void FixedUpdate () {

        if (Input.GetKey(KeyCode.DownArrow) && transform.position.y > -yLimit)
        {
            rb.AddForce(Vector3.down * speed);
        }
        if (Input.GetKey(KeyCode.UpArrow) && transform.position.y < yLimit)
        {
            rb.AddForce(Vector3.up * speed);
        }
    }
}

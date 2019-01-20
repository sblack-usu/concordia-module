using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlignCapsule : MonoBehaviour {

    CapsuleCollider capsule;
    float radius;

	void Start () {
        capsule = GetComponentInChildren<CapsuleCollider>();
	}
	
	public void Align (Vector3 start, Vector3 target) {
        capsule.radius = radius;
        capsule.center = Vector3.zero;
        capsule.direction = 2; // Z-axis for easier "LookAt" orientation

        capsule.transform.position = start + (target - start) / 2;
        capsule.transform.LookAt(start);
        capsule.height = (target - start).magnitude;
    }
}

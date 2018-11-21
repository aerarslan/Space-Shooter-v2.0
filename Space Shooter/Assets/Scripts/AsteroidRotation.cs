using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidRotation : MonoBehaviour {

    Rigidbody physics;

	void Start () {
        physics = GetComponent<Rigidbody>();
        physics.angularVelocity = Random.insideUnitSphere*3;
	}
	
}

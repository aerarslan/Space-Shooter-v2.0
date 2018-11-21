using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    Rigidbody physics;

	void Start () {

        physics = GetComponent<Rigidbody>();
        physics.velocity = transform.forward*11;
	}
	

}

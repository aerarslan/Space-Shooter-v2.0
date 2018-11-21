using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldRotation : MonoBehaviour {

    public GameObject player;
   

    private Vector3 offset;

    void Start()
    {
        offset = transform.position - player.transform.position;
    }

    private void FixedUpdate()
    {
        transform.Rotate(new Vector3(0, 900, 100)*Time.deltaTime);      
    }

    void LateUpdate()
    {
        transform.position = player.transform.position + offset;
    }

}


﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasyEnemyMov : MonoBehaviour {

    Rigidbody physics;
    
    GameObject World;
    World world;

    void Start()
    {
        World = GameObject.FindGameObjectWithTag("world");
        world = World.GetComponent<World>();

        physics = GetComponent<Rigidbody>();
        physics.velocity = transform.forward * world.enemySpeed;
    }
}

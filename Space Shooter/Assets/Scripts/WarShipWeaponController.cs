using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarShipWeaponController : MonoBehaviour {

    public GameObject bullet;
    public Transform[] bulletTransforms;
    public float fireRate;
    public float fireDelay;

    GameObject World;
    World world;

    void Start()
    {
        World = GameObject.FindGameObjectWithTag("world");
        world = World.GetComponent<World>();

        InvokeRepeating("Fire", fireDelay, fireRate + world.enemyFireRate);
    }

    void Fire()
    {
        foreach (var bulletTransform in bulletTransforms)
        {
            Instantiate(bullet, bulletTransform.position, Quaternion.identity);
        }     
    }
}

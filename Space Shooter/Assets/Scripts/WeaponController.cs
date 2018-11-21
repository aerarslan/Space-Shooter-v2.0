using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour {

    public GameObject bullet;
    public Transform bulletTransform;
    public float fireRate;
    public float fireDelay;

    GameObject World;
    World world;

    void Start () {
        World = GameObject.FindGameObjectWithTag("world");
        world = World.GetComponent<World>();

        InvokeRepeating("Fire", fireDelay, fireRate + world.enemyFireRate);
	}
	
	void Fire()
    {
        Instantiate(bullet, bulletTransform.position, Quaternion.identity);
    }
}

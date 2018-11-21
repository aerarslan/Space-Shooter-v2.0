using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemy : MonoBehaviour {

    Rigidbody physics;
    public GameObject playerExplosion;
    
    GameObject World;
    World world;

    GameObject Player;
    PlayerSkills playerSkills;

    void Start () {
        World = GameObject.FindGameObjectWithTag("world");
        world = World.GetComponent<World>();

        if (GameObject.FindGameObjectWithTag("Player"))
        {
            Player = GameObject.FindGameObjectWithTag("Player");
            playerSkills = Player.GetComponent<PlayerSkills>();
        }

        physics = GetComponent<Rigidbody>();
        physics.velocity = transform.forward * -14;
    }


    private void OnTriggerEnter(Collider other)
    {       
        if ((other.tag == "Player" || other.tag == "shield") && playerSkills.invincibility)
        {                  
            Destroy(gameObject);                      
        }
        else if (other.tag == "Player" && !playerSkills.invincibility)
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
            Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
            world.GameOver();
        }
    }

}

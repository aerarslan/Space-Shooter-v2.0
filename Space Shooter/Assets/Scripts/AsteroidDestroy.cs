using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidDestroy : MonoBehaviour {

    public GameObject explosion;
    public GameObject playerExplosion;

    GameObject World;
    World world;

    GameObject Player;
    PlayerSkills playerSkills;
    

    private void Start()
    {
        World = GameObject.FindGameObjectWithTag("world");
        world = World.GetComponent<World>();

        if (GameObject.FindGameObjectWithTag("Player"))
        {
            Player = GameObject.FindGameObjectWithTag("Player");
            playerSkills = Player.GetComponent<PlayerSkills>();
        }   
    }



    private void OnTriggerEnter(Collider other)
    {
        
        if (other.tag != "border" && other.tag != "enemy" && other.tag != "asteroid")
        {
            if((other.tag == "Player" || other.tag == "shield") && playerSkills.invincibility)
            {
                Destroy(gameObject);
            }
            else
            {
                Destroy(other.gameObject);
                Destroy(gameObject);
            }                     
                Instantiate(explosion, transform.position, transform.rotation);              
                               
                world.ScoreIncrease(100);
                    
        }   
        if(other.tag == "Player" && !playerSkills.invincibility)
        {
            Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
            world.GameOver();
        }

    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {

    Rigidbody playerBody;
    float horizontal;
    float vertical;
    Vector3 vec;
    float bulletCooldown = 0.0f;
    public GameObject Bullet;
    public Transform[] BulletTransforms;
    AudioSource bulletAudio;
    private int bulletType;

    public GameObject shieldAsteroid;

    
    GameObject Player;
    PlayerSkills playerSkills;

    void Start () {
		playerBody = GetComponent<Rigidbody>();
        bulletAudio = GetComponent<AudioSource>();

        shieldAsteroid.SetActive(false);

        if (GameObject.FindGameObjectWithTag("Player"))
        {
            Player = GameObject.FindGameObjectWithTag("Player");
            playerSkills = Player.GetComponent<PlayerSkills>();
        }
    }

    private void Update()
    {
        bulletType = playerSkills.bulletUpgrade;

        if (Input.GetButton("Fire1") && Time.time > bulletCooldown)
        {
            if(bulletType == 0)
            {
                Instantiate(Bullet, BulletTransforms[0].position, Quaternion.identity);
            }
            else if(bulletType == 1)
            {
                for(int i = 1; i <= 2; i++)
                {
                    Instantiate(Bullet, BulletTransforms[i].position, Quaternion.identity);
                }               
            }
            else
            {
                foreach (var BulletTransform in BulletTransforms)
                {
                    Instantiate(Bullet, BulletTransform.position, Quaternion.identity);
                }
            }
           

            bulletCooldown = Time.time + 0.2f;
            bulletAudio.Play();
        }
        
    }


    void FixedUpdate ()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        vec = new Vector3(horizontal,0,vertical);

        playerBody.velocity = vec*8;

        playerBody.position = new Vector3
            (
                Mathf.Clamp(playerBody.position.x,-6,6),
                0.1f,
                Mathf.Clamp(playerBody.position.z,-9,9)
            );

        playerBody.rotation = Quaternion.Euler(playerBody.velocity.z*2,0 , playerBody.velocity.x*-3);
	}
}

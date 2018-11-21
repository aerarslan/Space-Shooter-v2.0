using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMov : MonoBehaviour {

    Rigidbody physics;

    GameObject World;
    World world;

    public float dodge;
    public float smoothing;
    public float tilt;
    public Vector2 startWait;
    public Vector2 maneuverTime; // x min y max
    public Vector2 maneuverWait;
    private Transform playerTransform;

    private float currentSpeed;
    private float targetManeuver;
    

    void Start()
    {
        World = GameObject.FindGameObjectWithTag("world");
        world = World.GetComponent<World>();

        if(GameObject.FindGameObjectWithTag("Player"))
        {
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        }
        
        physics = GetComponent<Rigidbody>();
        physics.velocity = transform.forward * world.enemySpeed;
        currentSpeed = physics.velocity.z;
        StartCoroutine(Evade());
    }

    IEnumerator Evade()
    {
        yield return new WaitForSeconds(Random.Range(startWait.x, startWait.y));

        while (true)
        {          
            targetManeuver = Random.Range(1, dodge) * -Mathf.Sign(transform.position.x);                            
            yield return new WaitForSeconds(Random.Range(maneuverTime.x, maneuverTime.y));
            targetManeuver = 0;
            yield return new WaitForSeconds(Random.Range(maneuverWait.x, maneuverWait.y));
            if (playerTransform != null)
            {              
                targetManeuver = playerTransform.position.x;
                yield return new WaitForSeconds(Random.Range(maneuverTime.x, maneuverTime.y));
                targetManeuver = 0;
                yield return new WaitForSeconds(Random.Range(maneuverWait.x, maneuverWait.y));
            }                            
        }
    }

    private void FixedUpdate()
    {
        float newManeuver = Mathf.MoveTowards(physics.velocity.x, targetManeuver, Time.deltaTime * smoothing);
        physics.velocity = new Vector3(newManeuver, 0.0f, currentSpeed);
        physics.position = new Vector3
            (
                Mathf.Clamp(physics.position.x, -6, 6),
                0.1f,
                Mathf.Clamp(physics.position.z, -12, 12)
            );
        physics.rotation = Quaternion.Euler(0.0f, 0.0f, physics.velocity.x * -tilt);
    }
}

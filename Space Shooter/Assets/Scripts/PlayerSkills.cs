using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using System; // !!!

public class PlayerSkills : MonoBehaviour {

    public GameObject[] shieldAsteroids;
    public GameObject bulletRain;

    GameObject World;
    World world;

    public bool invincibility; 
    float timer = 0.0f;
    int seconds;
    int invincibilityDuration;
    public int bulletUpgrade;
    public Text invincibilityText;
    public Text bulletRainText;
    public Text bulletUpgradeText;

    private void Start()
    {
        World = GameObject.FindGameObjectWithTag("world");
        world = World.GetComponent<World>();

        invincibility = false;
        invincibilityDuration = 5;
        bulletUpgrade = 0;
    }

    private void Update()
    {     
        if (world.score >= 3000 && bulletUpgrade < 2)
        {
            bulletUpgradeText.text = "E = Weapon Upgrade 3000 score";
            invincibilityText.text = "F = Invincibility 2000 score ";
            bulletRainText.text = "Q = BulletRain 1000 score ";
        }
        else if (world.score >= 2000)
        {
            invincibilityText.text = "F = Invincibility 2000 score ";
            bulletRainText.text = "Q = BulletRain 1000 score ";
            bulletUpgradeText.text = "";
        }
        else if (world.score >= 1000)
        {
            bulletRainText.text = "Q = BulletRain 1000 score ";
            invincibilityText.text = "";
            bulletUpgradeText.text = "";
        }
        else
        {
            invincibilityText.text = "";
            bulletRainText.text = "";
            bulletUpgradeText.text = "";
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            if (world.score >= 2000 && !invincibility)
            {            
                world.score -= 2000;
                world.scoreText.text = "Score: " + world.score;
                invincibility = true;
                foreach (var shieldAsteroid in shieldAsteroids)
                {
                    shieldAsteroid.SetActive(true);
                }
                StartCoroutine(CloseInvincibility());
            }                  
        }
        if(invincibility)
        {      
            timer += Time.deltaTime;
            seconds = Convert.ToInt32(timer % 60);
            invincibilityText.text = "F = Invincibility / " + (invincibilityDuration - seconds);
        }
        
        
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (world.score >= 1000)
            {
                world.score -= 1000;
                world.scoreText.text = "Score: " + world.score;
                Instantiate(bulletRain, bulletRain.transform.position, bulletRain.transform.rotation);
            }
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (world.score >= 3000 && bulletUpgrade < 2)
            {
                world.score -= 3000;
                world.scoreText.text = "Score: " + world.score;
                bulletUpgrade += 1;
            }
        }

    }

    IEnumerator CloseInvincibility()
    {
        yield return new WaitForSeconds(invincibilityDuration);
        invincibilityDuration = 5;
        timer = 0;
        seconds = 0;
        invincibility = false;
        invincibilityText.text = "";
        foreach (var shieldAsteroid in shieldAsteroids)
        {
            shieldAsteroid.SetActive(false);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class World : MonoBehaviour {

    public GameObject[] asteroids;
    public GameObject starField;
    public GameObject starField2;
    public Vector3 vecBorders;
    public float spawnRate;
    public float startDelay;
    public float levelDelay;
    public Text scoreText;
    public Text scoreTextEnd;
    public Text gameOverText;
    public Text restartText;
    public Text levelText;
    public float asteroidSpeed;
    public float enemySpeed;
    public float enemyFireRate;  
    bool gameOverControl = false;
    bool restartControl = false;
    int level;  
    public int score;
    public int totalScore;

    GameObject Player;
    PlayerSkills playerSkills;

    GameObject backGround;
    BGScroller scroller;

    void Start () {

        StartCoroutine(create());

        backGround = GameObject.FindGameObjectWithTag("background");
        scroller = backGround.GetComponent<BGScroller>();

        if (GameObject.FindGameObjectWithTag("Player"))
        {
            Player = GameObject.FindGameObjectWithTag("Player");
            playerSkills = Player.GetComponent<PlayerSkills>();
        }

        score = 0;
        scoreText.text = "Score : " + score;
        asteroidSpeed = -5;
        enemySpeed = -2;
        enemyFireRate = 0.1f;
        spawnRate = 0.9f;
        level = 1;     

        starField.SetActive(true);
        scroller.scrollSpeed = 0.01f;

        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && restartControl)
        {
            SceneManager.LoadScene("level 1");
        }

        if (gameOverControl)
        {
            restartText.text = "Press R to restart";           
        }
    }

    IEnumerator create()
    {                         
            yield return new WaitForSeconds(startDelay);

            while (true)
            {

                for (int i = 0; i < 8*(level/2+1); i++)
                {
                    GameObject asteroid = asteroids[0];
                    GameObject asteroidAddition = asteroids[0];
                    if (level >= 1 && level <= 2)
                    {
                        asteroid = asteroids[Random.Range(0, 4)];
                    }
                    else if (level > 2 && level <= 4)
                    {
                        asteroid = asteroids[Random.Range(1, 5)];
                    }
                    else if (level > 4 && level <= 6)
                    {
                        asteroid = asteroids[Random.Range(1, 6)];
                    }
                    else
                    {
                        asteroid = asteroids[Random.Range(1, 7)];
                    }
                    asteroidAddition = asteroids[Random.Range(0, 3)];
                    Vector3 spawnPoint = new Vector3(Random.Range(-vecBorders.x, vecBorders.x), 0, vecBorders.z);
                    if (spawnRate <= 0.1f)
                    {

                        Vector3 spawnPoint2 = new Vector3(Random.Range(-vecBorders.x, vecBorders.x), 0, Random.Range(5, 8));
                        Instantiate(asteroidAddition, spawnPoint, Quaternion.identity);
                        for (int j = 0; j < 2 * (level/2 + 1); j++)
                        {
                            Vector3 spawnPoint3 = new Vector3(Random.Range(-vecBorders.x, vecBorders.x), 0, Random.Range(8, 10));
                            Instantiate(asteroid, spawnPoint3, Quaternion.identity);
                            yield return new WaitForSeconds(0.2f);
                        }

                        Instantiate(asteroid, spawnPoint2, Quaternion.identity);
                        yield return new WaitForSeconds(spawnRate);
                    }
                    Instantiate(asteroid, spawnPoint, Quaternion.identity);
                    yield return new WaitForSeconds(spawnRate);
                }

                if (spawnRate >= 0.1f)
                {
                    spawnRate -= 0.2f;
                    asteroidSpeed -= 0.3f;
                    enemySpeed -= 0.15f;
                    enemyFireRate += 0.20f;
                }
          
            while (GameObject.FindGameObjectWithTag("enemy") || GameObject.FindGameObjectWithTag("asteroid"))
            {
                yield return new WaitForSeconds(1);
            }

            if (gameOverControl)
            {
                restartText.text = "Press R to restart";
                break;
            }

            yield return new WaitForSeconds(1);
            level++;
            levelText.text = "LEVEL " + level;
            scroller.scrollSpeed = 2;
            starField.SetActive(false);
            starField2.SetActive(true);
                          
            yield return new WaitForSeconds(levelDelay);
            levelText.text = "";
            scroller.scrollSpeed = 0.01f;
            starField.SetActive(true);
            starField2.SetActive(false);
            
            }
            
    }

    public void ScoreIncrease(int increase)
    {
        score += increase;
        totalScore += increase;
        scoreText.text = "Score: " + score;      
    }

    public void GameOver()
    {
        scoreText.enabled = false;
        gameOverControl = true;
        restartControl = true;
        gameOverText.text = "GAME OVER";
        scoreTextEnd.text = "Your score: " + score;

        playerSkills.invincibilityText.text = "";
        playerSkills.bulletRainText.text = "";
        playerSkills.bulletUpgradeText.text = "";
    }
  
}

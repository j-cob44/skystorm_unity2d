using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject[] hazards;
    public GameObject[] bosses;

    public Vector3 spawnValues;
    public int hazardCount;
    public int increaseHazardsBy;
    public float spawnTimer;
    public float startTimer;
    public float waveBreakTimer;
    public float enemySpawningBreak;

    public GameObject playerObject;
    public float lives;
    public Vector3 respawnLocation;

    public GameObject pauseMenu;
    public Text scoreText;
    public Text restartText;
    public Text gameOverText;
    public Text nextWaveText;
    public Text livesText;

    public GameObject lifeImg1;
    public GameObject lifeImg2;
    public GameObject lifeImg3;
    public GameObject lifeImg4;
    public GameObject lifeImg5;

    [HideInInspector]
    public bool bossAlive;
    [HideInInspector]
    public float currentBossHealth;

    [HideInInspector]
    public bool gameOver;
    [HideInInspector]
    public bool gameWin = false;
    [HideInInspector]
    public bool gamePaused = false;
    [HideInInspector]
    public bool restart;
    private bool restartGame = false;
    private bool exitToMenu = false;
    [HideInInspector]
    public int score;
    private int waveNumber;

    [HideInInspector]
    public float weakestEnemy;
    [HideInInspector]
    public float hardestEnemy;

    private PlayerController playerController;

    private Scene scene;

    void Start()
    {
        scene = SceneManager.GetActiveScene();
        Debug.Log("Current Scene: " + scene.name);

        gameObject.GetComponent<audioFading>().fadeMusic(1, 0f);
        gameOver = false;
        restart = false;

        score = 0;
        bossAlive = false;
        waveNumber = 1;
        weakestEnemy = 0;
        hardestEnemy = 2;

        restartText.text = "";
        gameOverText.text = "";
        nextWaveText.text = "Get Ready!";
        livesText.text = "Lives: ";

        UpdateScore ();

        StartCoroutine(SpawnWaves());

        GameObject playerObject = GameObject.FindWithTag("Player");
        if (playerObject != null)
        {
            playerController = playerObject.GetComponent<PlayerController>();
        }
        if (playerController == null)
        {
            Debug.Log("Cannot find 'Player Controller'.");
        }

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape))
        {
            gamePaused = !gamePaused;
            pauseMenu.SetActive(gamePaused);
            if (gamePaused == false)
            {
                gameObject.GetComponent<audioFading>().fadeMusic(1, 0.025f);
                //gameOverText.text = "";
                Time.timeScale = 1;
                //gamePaused = false;
            }
            else
            {
                //gameOverText.text = "Game Paused! \n Press 'P' to Unpause.";
                Time.timeScale = 0;
                //gamePaused = true;
                gameObject.GetComponent<audioFading>().fadeMusic(-1, 0.025f);
            }
        }

        if (lives >= 0 && gameWin == false)
        {
            lifeImg1.SetActive(true);
        }
        else
        {
            lifeImg1.SetActive(false);
        }

        if (lives >= 1 && gameWin == false)
        {
            lifeImg2.SetActive(true);
        }
        else
        {
            lifeImg2.SetActive(false);
        }

        if (lives >= 2 && gameWin == false)
        {
            lifeImg3.SetActive(true);
        }
        else
        {
            lifeImg3.SetActive(false);
        }

        if (lives >= 3 && gameWin == false)
        {
            lifeImg4.SetActive(true);
        }
        else
        {
            lifeImg4.SetActive(false);
        }

        if (lives == 4 && gameWin == false)
        {
            lifeImg5.SetActive(true);
        }
        else
        {
            lifeImg5.SetActive(false);
        }

        if (restart)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                restartGame = true;
                StartCoroutine(ChangeLevel());
            }
        }
    }
    public void menuExit()
    {
        exitToMenu = true;
        StartCoroutine(ChangeLevel());
    }
   

    IEnumerator ChangeLevel()
    {
        Time.timeScale = 1;
        float fadeTime = gameObject.GetComponent<gameFading>().StartFade(1);
        gameObject.GetComponent<audioFading>().fadeMusic(-1, 0f);
        yield return new WaitForSecondsRealtime(fadeTime);
        if (restartGame)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
        }
        if (exitToMenu)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("main");
        }
    }

    IEnumerator SpawnWaves()
    {
        if (scene.name == "endless")
        {
            yield return new WaitForSeconds(startTimer);
            nextWaveText.text = "";
            while (true)
            {
                if (waveNumber % 10 == 0)
                {
                    nextWaveText.text = "+1 Life for making it to Wave " + waveNumber;
                    AddLife();
                    yield return new WaitForSeconds(waveBreakTimer / 2);
                    nextWaveText.text = "";
                }
                if (waveNumber % 8 == 0)
                {
                    if (waveNumber == 8)
                    {
                        weakestEnemy = 0;
                        hardestEnemy = 9;
                    }
                    Debug.Log("Wave: " + waveNumber + " Weakest Enemy:" + Mathf.FloorToInt(weakestEnemy));
                    Debug.Log("Hardest Enemy:" + Mathf.CeilToInt(hardestEnemy - 1));
                    nextWaveText.text = "BOSS WAVE";
                    yield return new WaitForSeconds(waveBreakTimer / 2);
                    nextWaveText.text = "";

                    GameObject boss = bosses[waveNumber % 8];
                    Vector3 bossSpawnPosition = new Vector3(0, spawnValues.y, spawnValues.z);
                    Quaternion bossSpawnRotation = Quaternion.identity;
                    Instantiate(boss, bossSpawnPosition, bossSpawnRotation);
                    int i = 0;

                    yield return new WaitForSeconds(spawnTimer);

                    while (bossAlive)
                    {
                        GameObject hazard = hazards[Random.Range(Mathf.FloorToInt(weakestEnemy), Mathf.CeilToInt(hardestEnemy))];
                        Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(hazard, spawnPosition, spawnRotation);

                        if (gameOver)
                        {
                            restartText.text = "Press 'R' to Restart.";
                            restart = true;
                            break;
                        }
                        yield return new WaitForSeconds(spawnTimer + 0.1f);
                        if (i % 8 == 0)
                        {
                            yield return new WaitForSeconds(enemySpawningBreak);
                        }
                        i++;
                    }
                }
                else
                {
                    Debug.Log("Wave: " + waveNumber + " Weakest Enemy:" + Mathf.FloorToInt(weakestEnemy));
                    Debug.Log("Hardest Enemy:" + Mathf.CeilToInt(hardestEnemy - 1));
                    for (int i = 0; i < hazardCount; i++)
                    {
                        GameObject hazard = hazards[Random.Range(Mathf.FloorToInt(weakestEnemy), Mathf.CeilToInt(hardestEnemy))];
                        Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(hazard, spawnPosition, spawnRotation);

                        if (gameOver)
                        {
                            restartText.text = "Press 'R' to Restart.";
                            restart = true;
                            break;
                        }
                        yield return new WaitForSeconds(spawnTimer);

                    }
                }

                if (waveNumber % 8 != 0)
                {
                    if (weakestEnemy < hazards.Length - 2)
                    {
                        weakestEnemy++;
                    }
                    if (hardestEnemy < hazards.Length)
                    {
                        hardestEnemy++;
                    }
                }

                if (gameOver)
                {
                    break;
                }

                if (waveNumber < 10)
                {
                    hazardCount = hazardCount + increaseHazardsBy;
                }

                waveNumber++;
                yield return new WaitForSeconds(waveBreakTimer / 2);
                nextWaveText.text = "Wave " + waveNumber;
                yield return new WaitForSeconds(waveBreakTimer / 2);
                nextWaveText.text = "";
            }
        }
    }

    public void AddScore (int newScoreValue)
    {
        score += newScoreValue;
        UpdateScore ();
    }

    void UpdateScore()
    {
        scoreText.text = "Score: " + score;
    }

    public void GameOver()
    {
        nextWaveText.enabled = false;
        if(scene.name == "endless")
        {
            gameOverText.text = "Game Over! \n You made it to Wave: " + waveNumber;
        }
        else
        {
            gameOverText.text = "Game Over";
        }
        gameOver = true;
    }

    public void AddLife()
    {
        if (lives == 4)
        {
            return;
        }
        else
        {
            lives++;
        }
    }

    public void LostLife()
    {
        if (lives == 0)
        {
            lives--;
            playerController.Death();
            GameOver();
        }
        else
        {
            lives--;
            playerController.respawn();
        }
    }

    public float bossLostHealth()
    {
        currentBossHealth--;
        return currentBossHealth;
    }
}

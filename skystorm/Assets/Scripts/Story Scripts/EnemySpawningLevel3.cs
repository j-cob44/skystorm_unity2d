using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class EnemySpawningLevel3 : MonoBehaviour
{
    private GameController gameController;
    private PlayerController playerController;

    public Vector3 spawnValues;
    public GameObject boss;

    private GameObject[] hazards;

    public float startTimer;
    public float spawnTimer;
    public float waveBreakTimer;

    public float waveNumber;
    public float hazardCount;
    public float targetScore;

    private float score;

    private int currentPatternNumber;
    private Vector2[] currentPattern;
    private GameObject pauseMenu;
    private Text scoreText;
    private Text restartText;
    private Text gameOverText;
    private Text nextWaveText;
    private Text livesText;

    private float weakestEnemy;
    private float hardestEnemy;

    private float hardestAllowedEnemy;

    private bool winTriggered;

    private bool bossAlive;

    [HideInInspector]
    public bool gameOver;

    private bool running = true;

    private DataControl dataControl;

    void Start()
    {
        DataControl dataControl = gameObject.GetComponent<DataControl>();

        dataControl.Load();

        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
            hazards = gameController.hazards;
            weakestEnemy = gameController.weakestEnemy;
            hardestEnemy = gameController.weakestEnemy;
            pauseMenu = gameController.pauseMenu;
            scoreText = gameController.scoreText;
            restartText = gameController.restartText;
            gameOverText = gameController.gameOverText;
            nextWaveText = gameController.nextWaveText;
            livesText = gameController.livesText;
            score = gameController.score;
            bossAlive = gameController.bossAlive;
        }

        GameObject playerObject = GameObject.FindWithTag("Player");
        if (playerObject != null)
        {
            playerController = playerObject.GetComponent<PlayerController>();
        }

        
        waveNumber = 1;
        weakestEnemy = 0;
        hardestEnemy = 2;
        hardestAllowedEnemy = 8;

        nextWaveText.text = "Survive to Win";

        StartCoroutine(SpawnWaves());

    }

    void Update()
    {
        gameOver = gameController.gameOver;
        score = gameController.score;
        bossAlive = gameController.bossAlive;

        if (gameController.gameWin)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                StartCoroutine(ChangeLevel());
            }
        }

    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startTimer);
        nextWaveText.text = "Wave " + waveNumber;
        yield return new WaitForSeconds(startTimer);
        nextWaveText.text = "";
        while (running)
        {
            if (waveNumber % 8 == 0)
            {
                weakestEnemy = 0;
                hardestEnemy = 9;

                Debug.Log("Wave: " + waveNumber + " Weakest Enemy:" + Mathf.FloorToInt(weakestEnemy));
                Debug.Log("Hardest Enemy:" + Mathf.CeilToInt(hardestEnemy - 1));
                nextWaveText.text = "BOSS WAVE";
                yield return new WaitForSeconds(waveBreakTimer / 2);
                nextWaveText.text = "";

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
                        gameController.restart = true;
                        break;
                    }
                    yield return new WaitForSeconds(spawnTimer + 0.1f);
                    if (i % 8 == 0)
                    {
                        yield return new WaitForSeconds(waveBreakTimer);
                    }
                    if (gameOver)
                    {
                        restartText.text = "Press 'R' to Restart.";
                        gameController.restart = true;
                        break;
                    }

                    i++;

                    
                }

                if (!bossAlive)
                {
                    gameController.gameWin = true;

                    DataControl dataControl = gameObject.GetComponent<DataControl>();

                    dataControl.levelThreeCompleted = true;
                    dataControl.Save();

                    winTriggered = true;
                    running = false;
                    break;
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
                        gameController.restart = true;
                        break;
                    }
                    yield return new WaitForSeconds(spawnTimer);

                }
            }

            if (winTriggered)
            {
                break;
            }

            if (gameOver)
            {
                restartText.text = "Press 'R' to Restart.";
                gameController.restart = true;
                break;
            }

            yield return new WaitForSeconds(spawnTimer * 2);

            if (weakestEnemy < hardestAllowedEnemy - 2)
            {
                weakestEnemy++;
            }
            if (hardestEnemy < hardestAllowedEnemy)
            {
                hardestEnemy++;
            }

            waveNumber++;
            yield return new WaitForSeconds(waveBreakTimer / 2);
            nextWaveText.text = "Wave " + waveNumber;
            yield return new WaitForSeconds(waveBreakTimer / 2);
            nextWaveText.text = "";
        }

        if(gameController.gameWin == true){
            playerController.boundary.yMax = 100;
            gameController.lifeImg1.SetActive(false);
            gameController.lifeImg2.SetActive(false);
            gameController.lifeImg3.SetActive(false);
            gameController.lifeImg4.SetActive(false);
            gameController.lifeImg5.SetActive(false);
            livesText.text = "";
            scoreText.text = "";
            gameOverText.text = "Fantastic! \n Level 3 Complete!";
            nextWaveText.text = "Press 'Space' to Continue";
        }
    }

    IEnumerator ChangeLevel()
    {
        Time.timeScale = 1;
        float fadeTime = gameController.gameObject.GetComponent<gameFading>().StartFade(1);
        gameController.gameObject.GetComponent<audioFading>().fadeMusic(-1, 0f);
        yield return new WaitForSecondsRealtime(fadeTime);
        UnityEngine.SceneManagement.SceneManager.LoadScene("main");
    }
}
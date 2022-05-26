using UnityEngine;
using System.Collections;

public class PowerupController : MonoBehaviour {

    public float lowPupSpawnTime;
    public float highPupSpawnTime;

    public float invincibilityTime;
    public float fasterFireTime;
    public float quadShotTime;

    public GameObject[] powerups;

    public Vector3 spawnValues;

    private GameController gameController;
    private Collider2D playerCollider;
    private SpriteRenderer playerSprite;
    private PlayerController playerController;

    void Start () {
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
        }
        if (gameController == null)
        {
            Debug.Log("Cannot find 'Player Collider'.");
        }

        GameObject playerObject = GameObject.FindWithTag("Player");
        if (playerObject != null)
        {
            playerCollider = playerObject.GetComponent<Collider2D>();
        }
        if (playerCollider == null)
        {
            Debug.Log("Cannot find 'Player Collider'.");
        }

        if (playerObject != null)
        {
            playerSprite = playerObject.GetComponent<SpriteRenderer>();
        }
        if (playerSprite == null)
        {
            Debug.Log("Cannot find 'Player SpriteRenderer'.");
        }

        if (playerObject != null)
        {
            playerController = playerObject.GetComponent<PlayerController>();
        }
        if (playerSprite == null)
        {
            Debug.Log("Cannot find 'Player Controller'.");
        }

        StartCoroutine( SpawnPups());
    }
	
    IEnumerator SpawnPups()
    {
        while (!gameController.gameOver)
        {
            float pupSpawnTimer = Random.Range(lowPupSpawnTime, highPupSpawnTime);
            yield return new WaitForSeconds(pupSpawnTimer);
            GameObject powerup = powerups[Random.Range(0, powerups.Length)];
            Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
            Quaternion spawnRotation = Quaternion.identity;
            Instantiate(powerup, spawnPosition, spawnRotation);
        }
    }
    public void invincibilityInitiator()
    {
        StartCoroutine (invincibility());
    }

    IEnumerator invincibility()
    {
        Debug.Log("Invincible");
        playerController.tag = "Invincible";
        playerSprite.color = new Color(0.5F, 0.5F, 0.5F, 0.25F);
        yield return new WaitForSeconds(invincibilityTime);
        float flickerRate = 0.2F;
        for (int i = 0; i < 6; i++)
        {
            playerSprite.color = new Color(0.5F, 0.5F, 0.5F, 0.25F);
            yield return new WaitForSeconds(flickerRate);
            playerSprite.color = new Color(1F, 1F, 1F, 1F);
            yield return new WaitForSeconds(flickerRate);
            if(flickerRate < 1F)
            {
                flickerRate = flickerRate + 0.05F;
            }
        }
        Debug.Log("Mortal");
        playerController.tag = "Player";
    }

    public void fasterFireInitiator()
    {
        StartCoroutine(fasterFireRate());
    }

    IEnumerator fasterFireRate()
    {
        Debug.Log("Fast!");
        float normalFire = playerController.fireRate;
        playerController.fireRate = 0.1F;
        yield return new WaitForSeconds(fasterFireTime);
        playerController.fireRate = normalFire;
        Debug.Log("Slow.");
    }

    public void quadShotInitiator()
    {
        StartCoroutine(quadShot());
    }

    IEnumerator quadShot()
    {
        Debug.Log("Quad.");
        playerController.quadShot = true;
        yield return new WaitForSeconds(quadShotTime);
        playerController.quadShot = false;
        Debug.Log("Double.");
    }
}

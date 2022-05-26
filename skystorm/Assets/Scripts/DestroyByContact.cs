using UnityEngine;
using System.Collections;

public class DestroyByContact : MonoBehaviour {

    public GameObject explosion;
    public GameObject playerExplosion;
    public int scoreValue;

    private float killScoreTime;
    public GameController gameController;

    void Start()
    {
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if(gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
        }
        if(gameController == null)
        {
            Debug.Log ("Cannot find 'Game Controller' script.");
        }


    }
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (CompareTag("Enemy"))
        {
            Debug.Log("Enemy hits : " + other.tag);

            if (other.CompareTag("Player"))
            {
                Destroy(gameObject);
                Instantiate(playerExplosion, other.transform.position, other.transform.rotation);

                //Enemy Explosion
                Vector3 explosionPos = new Vector3(transform.position.x, transform.position.y, 2f);
                Instantiate(explosion, explosionPos, transform.rotation);

                //Player Life Loss
                gameController.LostLife();
            }

            if (other.CompareTag("PlayerShot"))
            {
                gameController.AddScore(scoreValue);
                Destroy(gameObject);
                if (explosion != null)
                {
                    Vector3 explosionPos = new Vector3(transform.position.x, transform.position.y, 2f);
                    Instantiate(explosion, explosionPos, transform.rotation);
                }
                DestroyObject(other.gameObject);
            }
        }

        if (CompareTag("EnemyShot"))
        {
            if (other.CompareTag("Player"))
            {
                Destroy(gameObject);
                Instantiate(playerExplosion, other.transform.position, other.transform.rotation);

                //Player Life Loss
                gameController.LostLife();
            }

            if (other.CompareTag("PlayerShot"))
            {
                Destroy(gameObject);
                DestroyObject(other.gameObject);
            }
        }

        if (CompareTag("Boss"))
        {
            if (other.CompareTag("Player"))
            {
                Instantiate(playerExplosion, other.transform.position, other.transform.rotation);

                //Enemy Explosion
                Vector3 explosionPos = new Vector3(transform.position.x, transform.position.y, 2f);
                Instantiate(explosion, explosionPos, transform.rotation);

                //Player Life Loss
                gameController.LostLife();
            }

            if (other.CompareTag("PlayerShot"))
            {
                DestroyObject(other.gameObject);
                gameController.bossLostHealth();
                if (gameController.bossLostHealth() <= 0 && Time.time > killScoreTime)
                {
                    gameController.AddScore(scoreValue);
                    killScoreTime = 30f;
                }
            }
        }
    }
}

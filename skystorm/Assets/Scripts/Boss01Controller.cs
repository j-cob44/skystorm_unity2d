using UnityEngine;
using System.Collections;

public class Boss01Controller : MonoBehaviour {

    public GameObject shot;
    public Transform shotSpawnLeft01;
    public Transform shotSpawnLeft02;
    public Transform shotSpawnCenter;
    public Transform shotSpawnRight01;
    public Transform shotSpawnRight02;
    public float fireRate;
    public float startDelay;

    public float bossHealth;

    public Sprite enemySprite;
    public Sprite enemyShooting01;
    public Sprite enemyShooting02;

    public GameObject explosion;
    public GameObject bossExplosion;

    public GameObject lifePup;

    private SpriteRenderer spriteRenderer;

    private bool enemyShot;
    private bool deathActive;
    private bool canFire;

    private float shotChance;

    private AudioSource audioSource;

    private GameController gameController;

    void Start()
    {
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
        }
        if (gameController == null)
        {
            Debug.Log("Cannot find 'Game Controller' script.");
        }

        gameController.bossAlive = true;
        canFire = true;
        gameController.currentBossHealth = bossHealth;

        audioSource = GetComponent<AudioSource>();
        InvokeRepeating("FireWeapon", startDelay, fireRate);

        spriteRenderer = this.transform.GetChild(0).GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        bossHealth = gameController.currentBossHealth;

        if (bossHealth <= 0)
        {
            canFire = false;
            StartCoroutine(deathExplosion());
        }
    }

    IEnumerator deathExplosion()
    {
        if (!deathActive)
        {
            deathActive = true;
            for (int i = 0; i <= 8; i++)
            {

                Vector3 explosionPos = new Vector3(Random.Range(transform.position.x - 0.3f, transform.position.x + 0.3f), Random.Range(transform.position.y - 0.35f, transform.position.y + 0.85f), 0);
                Instantiate(explosion, explosionPos, transform.rotation);
                yield return new WaitForSeconds(0.15f);

            }
            Vector3 finalExplosionPos = new Vector3(Random.Range(transform.position.x - 0.1f, transform.position.x + 0.1f), Random.Range(transform.position.y - 0.1f, transform.position.y + 0.1f), 0);
            Instantiate(bossExplosion, finalExplosionPos, transform.rotation);


            Instantiate(lifePup, transform.position, transform.rotation);
            gameController.bossAlive = false;
            Destroy(gameObject);
        }
    }
    
    void FireWeapon()
    {
        if (canFire)
        {
            enemyShot = true;
            float gunToFire = Random.Range(0, 2);
            if (gunToFire == 0)
            {
                Instantiate(shot, shotSpawnCenter.position, shotSpawnCenter.rotation);
                Instantiate(shot, shotSpawnLeft01.position, shotSpawnLeft01.rotation);
                Instantiate(shot, shotSpawnRight01.position, shotSpawnRight01.rotation);
                StartCoroutine(SpriteAnimation01());
            }
            if (gunToFire == 1)
            {
                Instantiate(shot, shotSpawnCenter.position, shotSpawnCenter.rotation);
                Instantiate(shot, shotSpawnLeft02.position, shotSpawnLeft02.rotation);
                Instantiate(shot, shotSpawnRight02.position, shotSpawnRight02.rotation);
                StartCoroutine(SpriteAnimation02());
            }
            audioSource.Play();
        }
    }

    IEnumerator SpriteAnimation01()
    {
        if (spriteRenderer.sprite == enemySprite && enemyShot == true)
        {
            spriteRenderer.sprite = enemyShooting01;
            enemyShot = false;
            yield return new WaitForSeconds(0.25f);
            spriteRenderer.sprite = enemySprite;
        }
        else
        {
            spriteRenderer.sprite = enemySprite;
        }
    }

    IEnumerator SpriteAnimation02()
    {
        if (spriteRenderer.sprite == enemySprite && enemyShot == true)
        {
            spriteRenderer.sprite = enemyShooting02;
            enemyShot = false;
            yield return new WaitForSeconds(0.25f);
            spriteRenderer.sprite = enemySprite;
        }
        else
        {
            spriteRenderer.sprite = enemySprite;
        }
    }
}
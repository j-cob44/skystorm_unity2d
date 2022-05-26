using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boundary
{
    public float xMin, xMax, yMin, yMax;
}

public class PlayerController : MonoBehaviour
{
    public float speed;
    public Boundary boundary;

    public GameObject Shot;
    public Transform ShotSpawnLeft01;
    public Transform ShotSpawnRight01;
    public Transform ShotSpawnLeft02;
    public Transform ShotSpawnRight02;
    public float fireRate;
    public float flickerTime;

    public Sprite playerSprite;
    public Sprite playerShooting01;
    public Sprite playerShooting02;

    private SpriteRenderer spriteRenderer;

    private bool playerShot;
    public bool quadShot;

    private float nextFire;
    private AudioSource audioSource;

    private Rigidbody2D rb;
    //private Collider2D poly;
    private float winTime;

    private GameController gameController;

    private bool audioMuted = false;

    public Vector2 aPosition1;

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

        

        rb = GetComponent<Rigidbody2D>();
        //poly = GetComponent<PolygonCollider2D>();
        audioSource = GetComponent<AudioSource>();

        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer.sprite == null)
        {
            spriteRenderer.sprite = playerSprite;
        }
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.M))
        {
            if (audioMuted == true)
            {
                AudioListener.volume = 1;
                audioMuted = false;
                
            }
            else
            {
                AudioListener.volume = 0;
                audioMuted = true;
            }
        }
        if ((Input.GetButton("Fire1") && Time.time > nextFire && gameController.gamePaused == false && gameController.gameWin == false) || (Input.GetKey(KeyCode.Space) && Time.time > nextFire && gameController.gamePaused == false))
        {
            playerShot = true;
            nextFire = Time.time + fireRate;
            Instantiate(Shot, ShotSpawnLeft01.position, ShotSpawnLeft01.rotation);
            Instantiate(Shot, ShotSpawnRight01.position, ShotSpawnRight01.rotation);
            if (quadShot == true)
            {
                Instantiate(Shot, ShotSpawnLeft02.position, ShotSpawnLeft02.rotation);
                Instantiate(Shot, ShotSpawnRight02.position, ShotSpawnRight02.rotation);
            }
            audioSource.Play();
            StartCoroutine(SpriteAnimation());
        }
    }

    IEnumerator SpriteAnimation()
    {
        if (spriteRenderer.sprite == playerSprite && playerShot == true && !quadShot)
        {
            spriteRenderer.sprite = playerShooting01;
            playerShot = false;
            yield return new WaitForSeconds(fireRate / 1.2f);
            spriteRenderer.sprite = playerSprite;
        }
        else if(spriteRenderer.sprite == playerSprite && playerShot == true && quadShot)
        {
            spriteRenderer.sprite = playerShooting02;
            playerShot = false;
            yield return new WaitForSeconds(fireRate / 1.2f);
            spriteRenderer.sprite = playerSprite;
        }
        else
        {
            spriteRenderer.sprite = playerSprite;
        }
    }
    public void respawn()
    {
        StartCoroutine( RespawnFlicker() );
    }

    IEnumerator RespawnFlicker()
    {
        //gameObject.GetComponent<PolygonCollider2D>().enabled = false;
        gameObject.tag = "Invincible";
        for (int i = 0; i < 4; i++)
        {
            spriteRenderer.color = new Color(0.5F, 0.5F, 0.5F, 0.25F);
            yield return new WaitForSeconds(0.2F);
            spriteRenderer.color = new Color(1F, 1F, 1F, 1F);
            yield return new WaitForSeconds(0.2F);
        }
        //gameObject.GetComponent<PolygonCollider2D>().enabled = true;
        gameObject.tag = "Player";
    }

    public void Death()
    {
        Destroy(gameObject);
    }

    void FixedUpdate()
    {
        if(gameController.gameWin == false)
        {
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");

            Vector3 movement = new Vector3(moveHorizontal, moveVertical, 0.0f);
            rb.velocity = movement * speed;

            rb.position = new Vector3
            (
                Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax),
                Mathf.Clamp(rb.position.y, boundary.yMin, boundary.yMax),
                0.0f
            );
        }
        if(gameController.gameWin == true)
        {
            gameObject.tag = "Invincible";
            Vector3 movement = new Vector3(0, 0.8f, 0.0f);
            winTime += 0.025f;

            rb.velocity = movement * (speed * winTime);

            rb.position = Vector2.MoveTowards(new Vector2(transform.position.x, transform.position.y), new Vector2(0, transform.position.y), 3 * Time.fixedDeltaTime);
        }
    }
}

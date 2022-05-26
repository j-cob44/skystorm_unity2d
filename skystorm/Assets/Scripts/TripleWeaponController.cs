using UnityEngine;
using System.Collections;

public class TripleWeaponController : MonoBehaviour
{

    public GameObject shot;
    public Transform shotSpawnLeft;
    public Transform shotSpawnCenter;
    public Transform shotSpawnRight;
    public float fireRate;
    public float startDelay;

    public Sprite enemySprite;
    public Sprite enemyShooting;

    private SpriteRenderer spriteRenderer;

    private bool enemyShot;

    private AudioSource audioSource;


    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        InvokeRepeating("FireWeapon", startDelay, fireRate);

        spriteRenderer = this.transform.GetChild(0).GetComponent<SpriteRenderer>();
    }

    void FireWeapon()
    {
        enemyShot = true;
        Instantiate(shot, shotSpawnLeft.position, shotSpawnLeft.rotation);
        Instantiate(shot, shotSpawnCenter.position, shotSpawnCenter.rotation);
        Instantiate(shot, shotSpawnRight.position, shotSpawnRight.rotation);
        StartCoroutine(SpriteAnimation());
        audioSource.Play();
    }

    IEnumerator SpriteAnimation()
    {
        if (spriteRenderer.sprite == enemySprite && enemyShot == true)
        {
            spriteRenderer.sprite = enemyShooting;
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

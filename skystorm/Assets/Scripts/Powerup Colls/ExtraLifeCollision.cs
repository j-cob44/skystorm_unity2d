using UnityEngine;
using System.Collections;

public class ExtraLifeCollision : MonoBehaviour
{

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
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Boundary") || other.CompareTag("Enemy"))
        {
            return;
        }

        if (other.CompareTag("Player") || other.CompareTag("Invincible"))
        {
            Debug.Log("+1 Life");
            gameController.AddLife();
            Destroy(gameObject);
        }

    }
}
using UnityEngine;
using System.Collections;

public class QuadShotCollision : MonoBehaviour {

    private PowerupController powerupController;

    void Start()
    {

        GameObject powerupControllerObject = GameObject.FindWithTag("PowerupController");
        if (powerupControllerObject != null)
        {
            powerupController = powerupControllerObject.GetComponent<PowerupController>();
        }
        if (powerupController == null)
        {
            Debug.Log("Cannot find 'Powerup Controller' script.");
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
            powerupController.quadShotInitiator();
            Destroy(gameObject);
        }
    }
}

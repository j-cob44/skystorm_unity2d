using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2 : MonoBehaviour {

    public float playerSpeed = 4.5f;
    public float playerMaxSpeed = 8;
    public float playerTurnSpeed = 2.5f;

    public Vector3 currentSpeed;

    //BULLETS AND SHOOTING
    public GameObject playerBulletPrefab;
    public Transform mainBulletSpawn;


    private Rigidbody rb;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(transform.position.x, 5, transform.position.z);

        if (Input.GetKey(KeyCode.Space))
        {
            Instantiate(playerBulletPrefab, mainBulletSpawn.position, mainBulletSpawn.rotation);
        }
	}

    private void FixedUpdate()
    {
        currentSpeed = new Vector3(rb.velocity.x, 0, rb.velocity.z);

        if (currentSpeed.magnitude > playerMaxSpeed)
        {
            currentSpeed = currentSpeed.normalized;
            currentSpeed *= playerMaxSpeed;
        }

        if (Input.GetKey(KeyCode.W)) {
            rb.AddForce(transform.forward * playerSpeed);
        }
        if (Input.GetKey(KeyCode.S)) {
            rb.AddForce(-(transform.forward) * (playerSpeed/2));
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.up * -playerTurnSpeed);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.up * playerTurnSpeed);
        }

        /*rb.position = new Vector3
        (
            Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax),
            Mathf.Clamp(rb.position.y, boundary.yMin, boundary.yMax),
            0.0f
        );*/
    }
}

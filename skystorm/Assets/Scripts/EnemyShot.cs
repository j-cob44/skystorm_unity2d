using UnityEngine;
using System.Collections;

public class EnemyShot : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector3.down * speed;
    }
}

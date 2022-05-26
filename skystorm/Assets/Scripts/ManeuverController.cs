using UnityEngine;
using System.Collections;

public class ManeuverController : MonoBehaviour
{
    public float dodge;
    public float smoothTime;
    public Vector2 dodgeTimer;
    public Vector2 maneuverTime;
    public Vector2 maneuverWaitTime;
    public Boundary boundary;

    private float maneuverTarget;
    private float currentVelocity;
    private Rigidbody2D rb;

	void Start ()
    {
        rb = GetComponent<Rigidbody2D> ();
        currentVelocity = rb.velocity.y;
        StartCoroutine(Dodge());
	}

    IEnumerator Dodge ()
    {
        yield return new WaitForSeconds(Random.Range (dodgeTimer.x, dodgeTimer.y));

        while (true)
        {
            maneuverTarget = Random.Range (-dodge, dodge);
            yield return new WaitForSeconds(Random.Range (maneuverTime.x, maneuverTime.y));
            maneuverTarget = 0;
            yield return new WaitForSeconds(Random.Range (maneuverWaitTime.x, maneuverWaitTime.y));
        }
    }
	
	void FixedUpdate ()
    {
        float newManeuver = Mathf.MoveTowards(rb.velocity.x, maneuverTarget, Time.deltaTime * smoothTime);
        rb.velocity = new Vector3 (newManeuver, currentVelocity, 0.0f);
        rb.position = new Vector3
        (
            Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax),
            Mathf.Clamp(rb.position.y, boundary.yMin, boundary.yMax),
            0.0f
        );
    }
}

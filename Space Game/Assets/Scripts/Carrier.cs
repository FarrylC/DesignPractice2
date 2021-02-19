using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carrier : MonoBehaviour
{
    public enum CarrierType { Patrol, Stationary }
    public CarrierType type;

    public float visionRange, visionAngle, spawnRange;
    [HideInInspector] public bool isPlayerInSight, isPlayerInRange;
    public float cooldownTime;

    private int currentWaypoint = 0;
    public List<GameObject> waypoints = new List<GameObject>();
    public Rigidbody2D rb;
    public float acceleration, maxSpeed, waypointBuffer;

    public Animator animator;
    [Range(0.1f, 2f)] public float blinkSpeed = 1;
    public bool HasBlinked;
    [HideInInspector] public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Detect();
        HandleAnimation();
    }

    public void Detect()
    {
        // Check if the player is within vision range
        if (Vector2.Distance(transform.position, player.transform.position) < visionRange && Vector2.Angle(transform.position, player.transform.position) <= visionAngle)
            isPlayerInSight = true;
        else
            isPlayerInSight = false;

        // Check if the player is within minion spawn range
        if (Vector2.Distance(transform.position, player.transform.position) < spawnRange)
            isPlayerInRange = true;
        else
            isPlayerInRange = false;
    }

    public void Patrol()
    {
        // Only patrol if the carrier is a patrol type
        if (type != CarrierType.Patrol)
            return;

        // Point towards waypoint
        transform.up = waypoints[currentWaypoint].transform.position - transform.position;

        // Move towards waypoint
        rb.AddForce(acceleration * transform.up, ForceMode2D.Impulse);
        rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxSpeed);

        // If the enemy has reached the waypoint, go to next waypoint
        if(Vector2.Distance(transform.position, waypoints[currentWaypoint].transform.position) <= waypointBuffer)
        {
            // If this is not the last waypoint, continue to next waypoint
            if (currentWaypoint < waypoints.Count - 1)
                currentWaypoint++;

            // Otherwise, return to first waypoint
            else
                currentWaypoint = 0;
        }
    }

    public void Chase()
    {
        // Point towards the player
        transform.up = player.transform.position - transform.position;

        // Only chase if the carrier is a patrol type
        if (type != CarrierType.Patrol)
            return;

        // Move towards player
        rb.AddForce(acceleration * transform.up, ForceMode2D.Impulse);
        rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxSpeed);
    }

    public void SpawnMinion()
    {

    }
    public void HandleAnimation()
    {
        animator.SetFloat("blinkSpeed", blinkSpeed);

        if (isPlayerInSight)
        {
            animator.SetBool("IsAlerted", true);
        }
        else
        {
            animator.SetBool("IsAlerted", false);
        }
        if(HasBlinked)
        {
            animator.SetBool("IsBlinked", true);
        }
        else
        {
            animator.SetBool("IsBlinked", false);
        }

    }
    public void AnimEventAlertBlinkOffc(string message)
    {
        if(message.Equals("EyeShut") && HasBlinked)
        {
            animator.speed = 0;
            StartCoroutine(PauseToOpenEye());

        }
        if(message.Equals("BlinkAnimationOver") && HasBlinked)
        {
            HasBlinked = false;
        }
    }
    IEnumerator PauseToOpenEye()
    {
        animator.speed = 0;
        yield return new WaitForSeconds(blinkSpeed);
        animator.speed = 1;
    }
}

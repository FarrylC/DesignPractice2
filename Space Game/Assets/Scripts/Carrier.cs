﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carrier : MonoBehaviour
{
    public enum CarrierType { Patrol, Stationary }
    public CarrierType type;

    public GameObject minion;
    public int spawnedMinions;
    public int spawnableMinions;

    public float visionRange, visionAngle, spawnRange;
    [HideInInspector] public bool isPlayerInSight, isPlayerInRange;
    public float cooldownTime;
    public float currentCooldown;

    private int currentWaypoint = 0;
    public List<GameObject> waypoints = new List<GameObject>();
    public Rigidbody2D rb;
    public float acceleration, maxSpeed, waypointBuffer;

    public Animator enemyAnimator;
    private Animator visionAnimator;
    private SpriteRenderer spriteRenderer;
    [Range(0.1f, 2f)] public float blinkSpeed = 1;
    public bool HasBlinked;
    [HideInInspector] public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if(type == CarrierType.Patrol)
            spriteRenderer = transform.Find("vision_cone").GetComponent<SpriteRenderer>();
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
        { 
            isPlayerInRange = true;
        }     
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
        //Null checks in case we're missing a Spawner for them. Spawns them at a location is more pleasing to the player
        if(type == CarrierType.Patrol)
        {
            if (!GameObject.Find("PatrolMinionSpawner"))
                throw new System.Exception("missing PatrolMinionSpawner on Prefab");
            GameObject patrolMinionSpawner = GameObject.Find("PatrolMinionSpawner");
            Instantiate(minion, new Vector3(patrolMinionSpawner.transform.position.x, patrolMinionSpawner.transform.position.y, 0), Quaternion.identity);
        }
        else if(type == CarrierType.Stationary)
        {
            if (!GameObject.Find("StationaryMinionSpawner"))
                throw new System.Exception("missing StationaryMinionSpawner on Prefab");
            GameObject stationaryMinionSpawner = GameObject.Find("StationaryMinionSpawner");
            Instantiate(minion, new Vector3(stationaryMinionSpawner.transform.position.x, stationaryMinionSpawner.transform.position.y, 0), Quaternion.identity);
        }
            
        
       
    }
    public void HandleAnimation()
    {

      
        if(type == CarrierType.Stationary)
        {
            if (transform.Find("vision_cone_animated").GetComponent<Animator>() == null)
            {
                throw new System.Exception("Can't find vision_cone_animated Animator in the prefab");
            }
            else
            {
                visionAnimator = transform.Find("vision_cone_animated").GetComponent<Animator>();
            }
        }

        if(type == CarrierType.Stationary)
            visionAnimator.SetFloat("blinkSpeed", blinkSpeed);
        enemyAnimator.SetFloat("blinkSpeed", blinkSpeed);

        if (isPlayerInSight)
        {
            enemyAnimator.SetBool("IsAlerted", true);
        }
        else
        {
            enemyAnimator.SetBool("IsAlerted", false);
        }
        if(HasBlinked)
        {
            enemyAnimator.SetBool("IsBlinked", true);
            if (type == CarrierType.Stationary)
            {
                visionAnimator.SetBool("IsBlinked", true);
            }
        }
        else
        {
            enemyAnimator.SetBool("IsBlinked", false);
            if (type == CarrierType.Stationary)
            {
                visionAnimator.SetBool("IsBlinked", false);
            }
        }

    }
    //This is called from teh Redirector script on the enemy's sprite child
    public void AnimEventAlertBlinkOff(string message)
    {
        if(type == CarrierType.Stationary &&  visionAnimator == null)
        {
            throw new System.Exception("Can't find vision_cone_animated Animator in the prefab");
        }
        //Activated via the EyeShut animation Event when it gets to fully closed 
        if(message.Equals("EyeShut") && HasBlinked)
        {
            //makes it so that animation stops when eyes closed
            if(visionAnimator)
                visionAnimator.speed = 0;
            enemyAnimator.speed = 0;
            if(type == CarrierType.Patrol)
            {
                spriteRenderer.enabled = false;
            }
            //waits a few seconds before opening it again
            StartCoroutine(PauseToOpenEye(visionAnimator));
            //plays rest of blink animation

        }
        //Transitions to another state so its stops blinking
        if(message.Equals("BlinkAnimationOver") && HasBlinked)
        {
            HasBlinked = false;
        }
    }
    IEnumerator PauseToOpenEye(Animator visionAnim)
    {
        enemyAnimator.speed = 0;
        if (type == CarrierType.Stationary)
            visionAnim.speed = 0;
        yield return new WaitForSeconds(blinkSpeed);
        enemyAnimator.speed = 1;
        if (type == CarrierType.Stationary)
            visionAnim.speed = 1;
        if(type == CarrierType.Patrol)
        {
            spriteRenderer.enabled = true;
        }
    }
}

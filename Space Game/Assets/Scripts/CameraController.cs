using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Animator animator;
    
    public GameObject player;
    public Camera orthoCamera;
    public Rigidbody2D rb;

    public float buffer;
    public float followSpeed;
    public float orthographicSize;
    float followTimer = 1;

    // Start is called before the first frame update
    void Start()
    {
        // Start locked onto the player
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);

        //Set the orthographic camera viewport size and position
        orthoCamera.orthographicSize = orthographicSize;
    }

    // Update is called once per frame
    void Update()
    {
        FollowPlayer();
    }

    void FollowPlayer()
    {
        // If the player is too far away, start following
        if (Vector2.Distance(player.transform.position, transform.position) > buffer)
        {
            followTimer = 1;
            rb.velocity = new Vector2(followSpeed * (player.transform.position.x - transform.position.x), followSpeed * (player.transform.position.y - transform.position.y));
        }

        // Otherwise, count down to stop following
        else
            followTimer -= 10 * Time.deltaTime;

        // Stop following after the count down
        if (followTimer <= 0)
            rb.velocity = new Vector2(0, 0);
    }
}

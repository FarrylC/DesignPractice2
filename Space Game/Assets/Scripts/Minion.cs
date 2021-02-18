using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minion : MonoBehaviour
{
    public Rigidbody2D rb;

    [HideInInspector] public GameObject player;

    public float minionAcceleration;
    public float minionMaxSpeed;
    public float deathTimer;
    
    void Start()
    {
        //set timer to 5 and find player object
        deathTimer = 5f;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        //point toward player
        rb.transform.up = player.transform.position - transform.position;

        //accelerate and move toward player
        rb.AddForce(minionAcceleration * transform.up, ForceMode2D.Impulse);
        rb.velocity = Vector2.ClampMagnitude(rb.velocity, minionMaxSpeed);

        //count down the timer to destroy object
        deathTimer -= 1 * Time.deltaTime;

        //detroy gameObject after set amount of time
        if (deathTimer <= 0)
            Destroy(gameObject);
    }

    //detroy minion on contact with player
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            Destroy(gameObject);
    }
}

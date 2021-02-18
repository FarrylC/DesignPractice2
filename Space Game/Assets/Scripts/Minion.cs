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
    
    // Start is called before the first frame update
    void Start()
    {
        deathTimer = 5f;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        rb.transform.up = player.transform.position - transform.position;

        rb.AddForce(minionAcceleration * transform.up, ForceMode2D.Impulse);
        rb.velocity = Vector2.ClampMagnitude(rb.velocity, minionMaxSpeed);

        deathTimer -= 1 * Time.deltaTime;

        if (deathTimer <= 0)
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            Destroy(gameObject);
    }
}

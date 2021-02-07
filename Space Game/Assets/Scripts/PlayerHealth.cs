using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth;
    public int health;

    public Rigidbody2D rb;

    private float _time;
    private float _damageDuration;
    private bool _damaged;

    // Start is called before the first frame update
    void Start()
    {
        setHealth(maxHealth);
        _damageDuration = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        setHealth(health);
    }

    public void setHealth(int _health)
    {
        health = _health;

        //Health cannot excede max health
        if (health > maxHealth)
        {
            health = maxHealth;
        }

        //When health reaches 0, player Dies
        else if (health <= 0)
        {
            health = 0;
            Death();
        }
    }

    public void Death()
    {
        //animation of screen crack and fade out to reload scene

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // If the collided object is an asteroid, take damage
        if (collision.gameObject.CompareTag("Asteroid"))
        {
            _damaged = true;

            // Take damage depending on the size of the asteroid
            setHealth(health - 3);

            rb.AddForce(new Vector2(1000, 1000) * GameController.GetPlayerInput(), ForceMode2D.Force);


        }
    }
  

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Asteroid"))
        {
            _damaged = true;

            setHealth(health - 3);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        _damaged = false;
    }

    public bool IsDamaged
    {
        get { return _damaged; }
        set { _damaged = value; }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth;
    public int health;

    public Rigidbody2D rb;

    public Slider healthBar;

    private float _invincibleTime = 1.0f;
    private bool _isInvincible = false;
    private bool _damaged;
    
    // Start is called before the first frame update
    void Start()
    {
        setHealth(maxHealth);

        //Set start value of health bar
        healthBar.maxValue = maxHealth;
        healthBar.value = maxHealth;
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
        }

        //Update health bar value
        healthBar.value = _health;
        print("Value:" + healthBar.value);
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        // If the collided object is an asteroid, take damage
        if (collision.gameObject.CompareTag("Asteroid"))
        {
            if (!_isInvincible)
            {
                _damaged = true;
                
                setHealth(health - 1);

                SetInvincible();
            }
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Asteroid"))
        {
            if (!_isInvincible)
            {
                _damaged = true;

                setHealth(health - 1);

                SetInvincible();
            }
        }
    }

    //Stop player from taking damage for a given time.
    public void SetInvincible()
    {
        _isInvincible = true;

        //Stops the SetDamageable() function.
        CancelInvoke("SetDamageable");

        //Restarts the SetDamageable() funtion.
        Invoke("SetDamageable", _invincibleTime);
    }

    void SetDamageable()
    {
        _isInvincible = false;
    }

}

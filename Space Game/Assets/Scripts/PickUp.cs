using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public enum Type { health, black_box }
    public Type type;

    public int healthPickUpAmount;
    bool isActive = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void PickUpHealth()
    {
        // Get the player's health
        PlayerHealth playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();

        // If the PlayerHealth was not found, log a message
        if(playerHealth == null)
        {
            Debug.Log("PlayerHealth not found.");
            return;
        }

        // Increase the player's health
        playerHealth.health += healthPickUpAmount;
        if (playerHealth.health > playerHealth.maxHealth)
            playerHealth.health = playerHealth.maxHealth;
    }

    void PickUpBlackBox()
    {
        print("Black box obtained.");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Only allow the player to get the pickup once
        if (isActive)
            isActive = false;
        else
            return;

        // If the player comes in contact with the pickup, perform pickup's effect
        if (collision.GetComponent<PlayerController>())
        {
            if (type == Type.health)
                PickUpHealth();
            else
                PickUpBlackBox();

            // Remove the pickup from the game
            Destroy(gameObject);
        }
    }
}

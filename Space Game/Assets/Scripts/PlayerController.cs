using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float AccelerationTime = 1;
    public float DecelerationTime = 1;
    public float MaxSpeed = 1;
    public float rotationSpeed; // Degrees per second

    public Rigidbody2D rb;
    Vector2 input;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        input = GameController.GetPlayerInput();
        HandleMovement();
    }

    // Handles player movement input
    // NOTE: USE HandleAnimation() AND HandleSFX() FOR ANIMATION AND SFX RESPECTIVELY, DO NOT ADD THAT CODE IN THIS FUNCTION
    public void HandleMovement()
    {
        // Handle rotation
        transform.eulerAngles += new Vector3(0, 0, rotationSpeed * -input.x) * Time.deltaTime;

        // Determine movement force
        float force = input.y * MaxSpeed;

        // If velocity and force are in the same direction, accelerate
        if (Vector2.Angle(transform.up, rb.velocity) < 1)
            force /= Mathf.Pow(AccelerationTime, 2);
            //deceleration = false;

        // Otherwise, decelerate
        else
            force /= Mathf.Pow(DecelerationTime, 2);


        // Apply force
        rb.AddForce(force * transform.up, ForceMode2D.Impulse);
        rb.velocity = Vector2.ClampMagnitude(rb.velocity, MaxSpeed);

        // When there's no input, decelerate
        if (input.y == 0)
            rb.velocity *= 1 - (Time.deltaTime / Mathf.Pow(DecelerationTime, 2));
    }

    // Handles player sprite animation
    public void HandleAnimation()
    {

    }

    // Handles player SFX
    public void HandleSFX()
    {

    }
}

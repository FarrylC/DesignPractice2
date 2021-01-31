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

    //Animation
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        input = GameController.GetPlayerInput();
        HandleMovement();
        HandleAnimation();
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
        animator.SetInteger("direction_y", (int)input.y);

        //Turning while not moving
        animator.SetInteger("direction_x", (int)input.x);

        if(input.x > 0)
        {
             spriteRenderer.flipX = true; 
        }
        else
        {
            spriteRenderer.flipX = false;
        }
        //Not_moving transition accelerating back to idle
        if (input.x == 0)
        {
            animator.SetBool("not_moving_x", true);
        }else
        {
            animator.SetBool("not_moving_x", false);
        }

        if (input.y == 0)
        {
            animator.SetBool("not_moving_y", true);
        }
        else
        {
            animator.SetBool("not_moving_y", false);
        }


        //animator.SetFloat("direction_x", input.x)
        Debug.Log(input.x);
        
    }

    // Handles player SFX
    public void HandleSFX()
    {

    }
}

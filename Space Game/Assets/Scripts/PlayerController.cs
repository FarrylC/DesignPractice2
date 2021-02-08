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
    public Animator animator;
    public SpriteRenderer spriteRenderer;

    //private Space sound effects;
    private bool deceleration;
    private bool acceleration;
    private bool motor;
    private bool f_motor;
    private bool r_acc;
    private bool l_acc;
    private bool r_mot;
    private bool l_mot;
    private bool r_dec;
    private bool l_dec;
    public AudioSource acc;
    public AudioSource dec;
    public AudioSource mot;
    public AudioSource front_mot;
    public AudioSource left_t;
    public AudioSource right_t;
    public AudioSource left_acc;
    public AudioSource right_acc;
    public AudioSource left_dec;
    public AudioSource right_dec;

    // Start is called before the first frame update
    void Start()
    {
        //animator = GetComponent<Animator>();
        //spriteRenderer = GetComponent<SpriteRenderer>();
        deceleration = false;
        acceleration = false;
        motor = false;
        f_motor = false;
        r_acc = false;
        l_acc = false;
        r_mot = false;
        l_mot = false;
        r_dec = false;
        l_dec = false;
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

        //check if the player is accelerating and if the acceleration_sfx is NOT playing before play
        if (Input.GetKeyDown(KeyCode.W))
        {
            acceleration = true;
            motor = true;
            Spaceship_SFX();
        }
        else if (Input.GetKeyUp(KeyCode.W))
        {
            acceleration = false;
            deceleration = true;
            motor = false;
            Spaceship_SFX();
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            f_motor = true;
            motor = true;
            Spaceship_SFX();
        }
        else if (Input.GetKeyUp(KeyCode.S))
        {
            f_motor = false;
            deceleration = true;
            motor = false;
            Spaceship_SFX();
        }

        //check if the spaceship is turning right
        if (Input.GetKeyDown(KeyCode.D))
        {
            r_acc = true;
            r_mot = true;
            Spaceship_SFX();
        }
        else if (Input.GetKeyUp(KeyCode.D))
        {
            r_acc = false;
            r_mot = false;
            r_dec = true;
            Spaceship_SFX();
        }

        //check if the spaceship is turning left
        if (Input.GetKeyDown(KeyCode.A))
        {
            l_acc = true;
            l_mot = true;
            Spaceship_SFX();
        }
        else if (Input.GetKeyUp(KeyCode.A))
        {
            l_acc = false;
            l_mot = false;
            l_dec = true;
            Spaceship_SFX();
        }    
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
    }

    // Handles player SFX
    public void Spaceship_SFX()
    {
    //SFX Functions
    void Decelaration_SFX()
    {
        var deceleration_sfx = transform.Find("Deceleration").gameObject;
        var deceleration_sfx_source = deceleration_sfx.GetComponent<AudioSource>();
        deceleration_sfx_source.Play();
    }
    void Accelaration_SFX()
    {
        var acceleration_sfx = transform.Find("Acceleration").gameObject;
        var acceleration_sfx_source = acceleration_sfx.GetComponent<AudioSource>();
        acceleration_sfx_source.Play();
    }
    void Motor_SFX()
    {
        var motor_sfx = transform.Find("Main Motor").gameObject;
        var motor_sfx_source = motor_sfx.GetComponent<AudioSource>();
        motor_sfx_source.Play();
    }
    void F_Motor_SFX()
    {
        var f_motor_sfx = transform.Find("F-Motor").gameObject;
        var f_motor_sfx_source = f_motor_sfx.GetComponent<AudioSource>();
        f_motor_sfx_source.Play();
    }
    void R_Acc_SFX()
    {
        var r_acc_sfx = transform.Find("R-Acceleration").gameObject;
        var r_acc_sfx_source = r_acc_sfx.GetComponent<AudioSource>();
        r_acc_sfx_source.Play();
    }
    void L_Acc_SFX()
    {
        var left_t_sfx = transform.Find("L-Acceleration").gameObject;
        var l_acc_sfx_source = left_t_sfx.GetComponent<AudioSource>();
        l_acc_sfx_source.Play();
    }
    void R_Mot_SFX()
    {
        var r_mot_sfx = transform.Find("R-Motor").gameObject;
        var r_mot_sfx_source = r_mot_sfx.GetComponent<AudioSource>();
        r_mot_sfx_source.Play();
    }
    void L_Mot_SFX()
    {
        var l_mot_sfx = transform.Find("L-Motor").gameObject;
        var l_mot_sfx_source = l_mot_sfx.GetComponent<AudioSource>();
        l_mot_sfx_source.Play();
    }
    void R_Dec_SFX()
    {
        var r_dec_sfx = transform.Find("R-Deceleration").gameObject;
        var r_dec_sfx_source = r_dec_sfx.GetComponent<AudioSource>();
        r_dec_sfx_source.Play();
    }
    void L_Dec_SFX()
    {
        var l_dec_sfx = transform.Find("L-Deceleration").gameObject;
        var l_dec_sfx_source = l_dec_sfx.GetComponent<AudioSource>();
        l_dec_sfx_source.Play();
    }

        //SFX

        if (acceleration && !acc.isPlaying)
        {
            Accelaration_SFX();
        }
        //check if the ship is in movement
        if (motor && !mot.isPlaying && Input.anyKey)
        {
            Motor_SFX();
        }
        else if (!motor)
        {
            mot.Stop();
        }
        //check if the player is deceleting and if the deceleration_sfx is NOT playing before play
        if (deceleration && !dec.isPlaying && !Input.anyKey)
        {
            Decelaration_SFX();
            deceleration = false;
        }

        if (f_motor && !front_mot.isPlaying)
        {
            F_Motor_SFX();
        }
        //check if the ship is in movement
        if (motor && !mot.isPlaying && Input.anyKey)
        {
            Motor_SFX();
        }
        else if (!motor)
        {
            mot.Stop();
        }
        //check if the player is deceleting and if the deceleration_sfx is NOT playing before play
        if (deceleration && !dec.isPlaying && !Input.anyKey)
        {
            Decelaration_SFX();
            deceleration = false;
        }

        //check if the player is turn to right
        if (r_acc && !right_acc.isPlaying)
        {
            R_Acc_SFX();
        }
        //check if the ship is in movement to right
        if (r_mot && !right_t.isPlaying && Input.anyKey)
        {
            R_Mot_SFX();
        }
        else if (!r_mot)
        {
            right_t.Stop();
        }
        //check if the player is deceleting the right motor
        if (r_dec && !right_dec.isPlaying && !Input.anyKey)
        {
            R_Dec_SFX();
            r_dec = false;
        }

        //check if the player is turn to left
        if (l_acc && !left_acc.isPlaying)
        {
            L_Acc_SFX();
        }
        //check if the ship is in movement to left
        if (l_mot && !left_t.isPlaying && Input.anyKey)
        {
            L_Mot_SFX();
        }
        else if (!l_mot)
        {
            left_t.Stop();
        }
        //check if the player is deceleting the right motor
        if (l_dec && !left_dec.isPlaying && !Input.anyKey)
        {
            L_Dec_SFX();
            l_dec = false;
        }

    }
}

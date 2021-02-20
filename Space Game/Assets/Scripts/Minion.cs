using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minion : MonoBehaviour
{
    public Rigidbody2D rb;
    private Animator minionAnimator;

    [HideInInspector] public GameObject player;

    public float minionAcceleration;
    public float minionMaxSpeed;
    public float deathTimer;
    private bool hitPlayer;
    
    
    // Start is called before the first frame update
    void Start()
    {
        deathTimer = 5f;
        player = GameObject.FindGameObjectWithTag("Player");
        minionAnimator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveToPlayer();
    }
    private void MoveToPlayer()
    {
        if(!hitPlayer)
        {
            rb.transform.up = player.transform.position - transform.position;

            rb.AddForce(minionAcceleration * transform.up, ForceMode2D.Impulse);
            rb.velocity = Vector2.ClampMagnitude(rb.velocity, minionMaxSpeed);

            deathTimer -= 1 * Time.deltaTime;

            if (deathTimer <= 0)
            {
                hitPlayer = true;
                minionAnimator.SetBool("didHitPlayer", true);
            }
        }
    }
    //This method stops the Animator so that the death audio can be played
    //Afterwards the 
    private void HandleDeathAnimationForSounds()
    {
        minionAnimator.speed = 0;
        //TO - DO Add functionality to play audio Here

        //After audio finishes playing
        float deathSoundDuration = 2f;
        StartCoroutine(WaitForDeathSound(deathSoundDuration));
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //Triggers the death animation which has an Animation event that will call HandleDeathAnimationForSounds()
            hitPlayer = true;
            minionAnimator.SetBool("didHitPlayer", true);
        }
            //Destroy(gameObject);
    }
    IEnumerator WaitForDeathSound(float audioDuration)
    {
        //Waits for the death sound to finish playing before destroying Minion
        yield return new WaitForSeconds(audioDuration);
        Destroy(gameObject);
        Debug.Log("Destroyed");
    }

  

}

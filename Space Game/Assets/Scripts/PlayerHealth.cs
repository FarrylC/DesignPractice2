using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth;
    public int health;

    public Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    public Slider healthBar;

    private float _invincibleTime = 1.0f;
    private bool _isInvincible = false;
    private bool _damaged;

    public PostProcessVolume PostProcessVFX;

    //Shield Sprite on Player
    private GameObject playerShield;
    private SpriteRenderer playerShieldSpriteRenderer;


    //Audio_Variables
    public AudioSource asteroidHit;
    public AudioSource enemyHit;

    #region SFX
    //SFX Functions
    void Asteroid_Hit_SFX()
    {
        if (!asteroidHit.isPlaying)
        {
            var asteroid_hit_sfx = transform.Find("AsteroidCollision").gameObject;
            var asteroid_hit_sfx_source = asteroid_hit_sfx.GetComponent<AudioSource>();
            asteroid_hit_sfx_source.Play();
        }
    }
    void Enemy_Hit_SFX()
    {
        if (!enemyHit.isPlaying)
        {
            var enemy_hit_sfx = transform.Find("EnemyCollision").gameObject;
            var enemy_hit_sfx_source = enemy_hit_sfx.GetComponent<AudioSource>();
            enemy_hit_sfx_source.Play();
        }
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        playerShield = GameObject.Find("SpriteShield");
        if (!playerShield)
            throw new System.Exception("Mising SpriteShield child empty game obect with sprite and animation controller in Player Object");

        playerShieldSpriteRenderer = playerShield.GetComponent<SpriteRenderer>();
        if (!playerShieldSpriteRenderer)
            throw new System.Exception("Missing SpriteRenderer on SpriteShield gameobect as a child of Player Game Object since its used to ENABLE and DISABLE Shield animation instead of using states");
        setHealth(maxHealth);

        if (!GameObject.Find("Sprite").gameObject.GetComponent<SpriteRenderer>())
            throw new System.Exception("Can't find Sprite Game Object or its missing a Sprite Renderer");
        spriteRenderer = GameObject.Find("Sprite").gameObject.GetComponent<SpriteRenderer>();
        //Set start value of health bar
        healthBar.maxValue = maxHealth;
        healthBar.value = maxHealth;
        PostProcessVFX.weight = 0f;

    }

    // Update is called once per frame
    void Update()
    {
        setHealth(health);
        LowLifePPFX();
        HandleShieldAnim();
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
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        // If the collided object is an asteroid, take damage
        if (collision.gameObject.CompareTag("Asteroid"))
        {
            if (!_isInvincible)
            {
                _damaged = true;
                StartCoroutine(ColorShiftPlayerDamaged());
                SendMessage("KnockBack");
                setHealth(health - 1);
                Asteroid_Hit_SFX();
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
                StartCoroutine(ColorShiftPlayerDamaged());
                setHealth(health - 1);
                Asteroid_Hit_SFX();
                SetInvincible();

            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            if (!_isInvincible)
            {
                _damaged = true;
                StartCoroutine(ColorShiftPlayerDamaged());
                setHealth(health - 1);
                Enemy_Hit_SFX();
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
        _damaged = false;
    }

    private void HandleShieldAnim()
    {
        if (_isInvincible)
            playerShieldSpriteRenderer.enabled = true;
        else
            playerShieldSpriteRenderer.enabled = false;

    }
    IEnumerator ColorShiftPlayerDamaged()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.5f);
        spriteRenderer.color = Color.white;
    }

    void LowLifePPFX()
    {
        if (health < maxHealth/2)
        PostProcessVFX.weight = 1f/health;
        else
        PostProcessVFX.weight = 0f;
    }




}

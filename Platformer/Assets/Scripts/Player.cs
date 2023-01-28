using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour //ITakeDamage
{
    [SerializeField] int playerNumber = 1;
    [Header("Movement")]
    [SerializeField] float speed = 1f;
    [SerializeField] float slipFactor;
    [Header("Jump")]
    [SerializeField] float jumpVelocity = 1f;
    [SerializeField] int maxJumps = 2;
    [SerializeField] Transform feet;
    [SerializeField] Transform leftSensor;
    [SerializeField] Transform rightSensor;
    [SerializeField] float downPull = 5f;
    [SerializeField] float maxJumpDuration = 0.1f;
    [SerializeField] float wallSlideSpeed = 1f;
    [SerializeField] float acceleration = 1f;
    [SerializeField] float deceleration = 1f;
    [SerializeField] float airBreaking = 1f;
    [SerializeField] float airAcceleration = 1f;

    AudioSource audioSource;
    Vector2 startingPosition;
    int jumpsRemaining;
    float fallTimer;
    float jumpTimer;
    new Rigidbody2D rigidbody2D;
    Animator animator;
    SpriteRenderer spriteRenderer;
    float horizontal;
    bool isGrounded;
    bool isOnSlipperySurface;
    string jumpButton;
    string horizontalAxis;
    int layerMask;

    public int PlayerNumber => playerNumber;
    // public int PlayerNumber { get { return playerNumber; } }

    void Start()
    {
        startingPosition = transform.position;
        jumpsRemaining = maxJumps;
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        jumpButton = $"P{playerNumber}Jump";
        horizontalAxis = $"P{playerNumber}Horizontal";
        layerMask = LayerMask.GetMask("Default");
        audioSource = GetComponent<AudioSource>();
    }
    void Update()
    {
        UpdateIsGrounded();
        ReadHorizontalInput();
        if (isOnSlipperySurface)
        {
            SlipHorizontal();
        }
        else
        {
            MoveHorizontal();
        }

        UpdateAnimator();
        UpdateSpriteDirection();

        if(ShouldSlide())
        {
            if(ShouldStartJump())
            {
                WallJump();
            }
            Slide();
            return;
        }
        
        if (ShouldStartJump())
        {
            Jump();
        }
        else if (ShouldContinueJump())
        {
            ContinueJump();
        }

        jumpTimer += Time.deltaTime;

        if (isGrounded && fallTimer > 0)
        {
            fallTimer = 0;
            jumpsRemaining = maxJumps;
        }
        else
        {
            fallTimer += Time.deltaTime;
            var downForce = downPull * fallTimer * fallTimer;
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, rigidbody2D.velocity.y - downForce);
        }

    }

    private void WallJump()
    {
        rigidbody2D.velocity = new Vector2(-horizontal * jumpVelocity, jumpVelocity * 1.5f);
    }

    void Slide()
    {
        rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, -wallSlideSpeed);
    }

    void ContinueJump()
    {
        rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, jumpVelocity);
        fallTimer = 0;
    }

    bool ShouldContinueJump()
    {
        return Input.GetButton(jumpButton) && jumpTimer <= maxJumpDuration;
    }

    void Jump()
    {
        rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, jumpVelocity);
        jumpsRemaining--;
        Debug.Log($"Jumps remaining: {jumpsRemaining}");
        fallTimer = 0;
        jumpTimer = 0;
        if(audioSource != null) 
        {
            audioSource.Play();
        }
        
    }

    bool ShouldStartJump()
    {
        return Input.GetButtonDown(jumpButton) && jumpsRemaining > 0;
    }

    void MoveHorizontal()
    {
        float smoothnessMultiplier = horizontal == 0 ? deceleration : acceleration;
        if(isGrounded == false)
        {
            smoothnessMultiplier = horizontal == 0 ? airBreaking : airAcceleration;
        }
        float newHorizontal = Mathf.Lerp(rigidbody2D.velocity.x, horizontal * speed, Time.deltaTime * smoothnessMultiplier);
        rigidbody2D.velocity = new Vector2(newHorizontal, rigidbody2D.velocity.y);
    }

    void SlipHorizontal()
    {
        var desiredVelocity = new Vector2(horizontal * speed, rigidbody2D.velocity.y);
        var smoothedVelocity = Vector2.Lerp(
            rigidbody2D.velocity,
            desiredVelocity,
            Time.deltaTime / slipFactor);

        rigidbody2D.velocity = smoothedVelocity;
    }

    void ReadHorizontalInput()
    {
        horizontal = Input.GetAxis(horizontalAxis) * speed;
    }

    void UpdateSpriteDirection()
    {
        if (horizontal != 0)
        {
            var spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.flipX = horizontal < 0;
        }
    }

    void UpdateAnimator()
    {
        bool walking = horizontal != 0;
        animator.SetBool("Walk", walking);
        animator.SetBool("Jump", ShouldContinueJump());
        animator.SetBool("Slide", ShouldSlide());
    }

    void UpdateIsGrounded()
    {
        var hit = Physics2D.OverlapCircle(feet.position, 0.1f, layerMask);
        isGrounded = hit != null;


        //isOnSlipperySurface = hit?.CompareTag("Slippery) ?? false;
        if (hit != null)
        {
            isOnSlipperySurface = hit.CompareTag("Slippery");
        }
        else
        {
            isOnSlipperySurface = false;
        }
    }

    internal void ResetToStart()
    {
        SceneManager.LoadScene("Menu");
    }

    internal void TeleportTo(Vector3 position)
    {
        rigidbody2D.position = position;
        rigidbody2D.velocity = Vector2.zero;
    }


    bool ShouldSlide()
    {
        
        if(isGrounded) 
        {
            return false;
        }

        if(rigidbody2D.velocity.y > 0)
        {
            return false;
        }

        if (horizontal < 0)
        {
            var hit = Physics2D.OverlapCircle(leftSensor.position, 0.1f);
            if (hit != false && hit.CompareTag("Wall")) 
            {
                return true;
            }
        }
        if (horizontal > 0)
        {
            var hit = Physics2D.OverlapCircle(rightSensor.position, 0.1f);
            if (hit != false && hit.CompareTag("Wall")) 
            {
                return true;
            }
        }
        return false;
    }

    /*
    void OnCollisionEnter2D(Collision2D collision)
    {
        var fireball = collision.collider.GetComponent<Fireball>();
        if (fireball != null)
        {
            TakeDamage();
            Debug.Log("player hit fireball");
        }
        else
        {
            return;
        }

    }
    public void TakeDamage()
    {
        ResetToStart();
    }
    */
}
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] int playerNumber = 1;
    [Header("Movement")]
    [SerializeField] float speed = 1f;
    [SerializeField] float slipFactor;
    [Header("Jump")]
    [SerializeField] float jumpVelocity = 1f;
    [SerializeField] int maxJumps = 2;
    [SerializeField] Transform feet;
    [SerializeField] float downPull = 5f;
    [SerializeField] float maxJumpDuration = 0.1f;

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
    
    void Start()
    {
        startingPosition = transform.position;
        jumpsRemaining = maxJumps;
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
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

    void ContinueJump()
    {
        rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, jumpVelocity);
        fallTimer = 0;
    }

    bool ShouldContinueJump()
    {
        return Input.GetButton($"P{playerNumber}Jump") && jumpTimer <= maxJumpDuration;
    }

    void Jump()
    {
        rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, jumpVelocity);
        jumpsRemaining--;
        Debug.Log($"Jumps remaining: {jumpsRemaining}");
        fallTimer = 0;
        jumpTimer = 0;
    }

    bool ShouldStartJump()
    {
        return Input.GetButtonDown($"P{playerNumber}Jump") && jumpsRemaining > 0;
    }

    void MoveHorizontal()
    {
         rigidbody2D.velocity = new Vector2(horizontal * speed, rigidbody2D.velocity.y);
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
        horizontal = Input.GetAxis($"P{playerNumber}Horizontal") * speed;
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
    }

    void UpdateIsGrounded()
    {
        var hit = Physics2D.OverlapCircle(feet.position, 0.1f, LayerMask.GetMask("Default"));
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
        transform.position = startingPosition;
    }
}
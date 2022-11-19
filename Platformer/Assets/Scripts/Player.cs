using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float speed = 1f;
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
        MoveHorizontal();

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
        return Input.GetButton("Fire1") && jumpTimer <= maxJumpDuration;
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
        return Input.GetButtonDown("Fire1") && jumpsRemaining > 0;
    }

    void MoveHorizontal()
    {
        if (Mathf.Abs(horizontal) >= 1)
        {
            rigidbody2D.velocity = new Vector2(horizontal, rigidbody2D.velocity.y);
            //Debug.Log($"Velocity = {rigidbody2D.velocity}");
        }
    }

    void ReadHorizontalInput()
    {
        horizontal = Input.GetAxis("Horizontal") * speed;
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
    }

    internal void ResetToStart()
    {
        transform.position = startingPosition;
    }
}
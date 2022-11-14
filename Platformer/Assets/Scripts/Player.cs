using UnityEngine;

public class Player : MonoBehaviour
{
    Vector2 startingPosition;
    [SerializeField] float speed = 1f;
    [SerializeField] float jumpForce = 1f;

    private void Start()
    {
        startingPosition = transform.position;
    }
    void Update()
    {
        var horizontal = Input.GetAxis("Horizontal") * speed;
        var rigidbody2D = GetComponent<Rigidbody2D>();

        if(Mathf.Abs(horizontal) >= 1)
        {
            rigidbody2D.velocity = new Vector2(horizontal, rigidbody2D.velocity.y);
            Debug.Log($"Velocity = {rigidbody2D.velocity}");
        }

        var animator = GetComponent<Animator>();
        bool walking = horizontal != 0; // ce e codul asta ba
        animator.SetBool("Walk", walking); // animator.SetBool("Walk", horizontal != 0);

        if (horizontal != 0)
        {
            var spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.flipX = horizontal < 0;
        }

        if(Input.GetButtonDown("Fire1"))
        {
            rigidbody2D.AddForce(Vector2.up * jumpForce);
        }
    }

    internal void ResetToStart()
    {
        transform.position = startingPosition;
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour
{
    [SerializeField] Transform leftSensor;
    [SerializeField] Transform rightSensor;

    new Rigidbody2D rigidbody2D;
    SpriteRenderer sprite;
    float direction = -1;

    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        Debug.DrawRay(leftSensor.position, Vector2.down * 0.1f, Color.red);
        Debug.DrawRay(rightSensor.position, Vector2.down * 0.1f, Color.red);
        if (direction < 0)
        {
            rigidbody2D.velocity = new Vector2(direction, rigidbody2D.velocity.y);
            var result = Physics2D.Raycast(leftSensor.position, Vector2.down, 0.1f);
            if (result.collider == null)
            {
                TurnAround();
            }
        }
        else
        {
            rigidbody2D.velocity = new Vector2(direction, rigidbody2D.velocity.y);
            var result = Physics2D.Raycast(rightSensor.position, Vector2.down, 0.1f);
            if (result.collider == null)
            {
                TurnAround();
            }
        }
    }

    void TurnAround()
    {
        direction *= -1;
        sprite.flipX = direction > 0;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        var player = collision.collider.GetComponent<Player>();
        if((player == null))
        {
            return;
        }
        player.ResetToStart();
    }
}

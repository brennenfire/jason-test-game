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
        if (direction < 0)
        {
            ScanSensor(leftSensor);
        }
        else
        {
            ScanSensor(rightSensor);
        }
    }

    void ScanSensor(Transform sensor)
    {
        Debug.DrawRay(sensor.position, Vector2.down * 0.1f, Color.red);
        
        rigidbody2D.velocity = new Vector2(direction, rigidbody2D.velocity.y);
        var result = Physics2D.Raycast(sensor.position, Vector2.down, 0.1f);
        if (result.collider == null)
        {
            TurnAround();
        }

        Debug.DrawRay(sensor.position, new Vector2(direction, 0) * 0.1f, Color.red);
        var sideResult = Physics2D.Raycast(sensor.position, new Vector2(direction, 0), 0.1f);
        if(sideResult.collider != null)
        {
            TurnAround();
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

        var contact = collision.contacts[0];
        Vector2 normal = contact.normal;
        Debug.Log($"Normal = {normal}");

        if(normal.y <= -0.5)
        {
            Die();
        }
        else
        {
            player.ResetToStart();
        }
    }

    private void Die()
    {
        GameObject.Destroy(gameObject);
    }
}

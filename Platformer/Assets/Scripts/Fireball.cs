using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    [SerializeField] float launchForce = 4f;
    [SerializeField] float bounceForce = 4f;
    [SerializeField] int bouncesRemaining = 3;

    Rigidbody2D rigidBody;

    public float direction { get; set; }

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();  
        rigidBody.velocity = new Vector2(launchForce * direction, 0);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        ITakeDamage damageable = collision.collider.GetComponent<ITakeDamage>();
        if(damageable != null)
        {
            damageable.TakeDamage();
            Destroy(gameObject);
            return;
        }
        bouncesRemaining--;
        if(bouncesRemaining < 0)
        {
            Destroy(gameObject);
        }
        else
        {
            rigidBody.velocity = new Vector2(launchForce * direction, bounceForce);
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Switch : MonoBehaviour
{
    [SerializeField] Sprite rightSwitch;
    [SerializeField] Sprite leftSwitch;
    [SerializeField] UnityEvent onLeft;
    [SerializeField] UnityEvent onRight;
    Collider2D col;
    SpriteRenderer sprite;

    private void Start()
    {
        col = GetComponent<Collider2D>();
        sprite = GetComponent<SpriteRenderer>();
    }
    /*
    void OnCollisionEnter2D(Collision2D collision)
    {
        var player = collision.collider.GetComponent<Player>();
        var sprite = GetComponent<SpriteRenderer>();
        if (player == null)
        {
            return;
        }
        if (collision.contacts[0].normal.x > 0)
        {
            sprite.sprite = rightSwitch;
            onRight.Invoke();
        }
        if (collision.contacts[0].normal.x < 0)
        {
            sprite.sprite = leftSwitch;
            onLeft.Invoke();
        }
    }
    */

    void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<Player>();
        
        if (player == null)
        {
            return;
        }
        col.isTrigger = true;
        FindSide(collision);
    }

    private void FindSide(Collider2D collision)
    {
        if (collision.transform.position.x < col.transform.position.x)
        {
            Debug.Log("stanga");
            sprite.sprite = rightSwitch;
            onRight.Invoke();
        }
        if (collision.transform.position.x > col.transform.position.x)
        {
            Debug.Log("dreapta");
            sprite.sprite = leftSwitch;
            onLeft.Invoke();
        }
    }
}

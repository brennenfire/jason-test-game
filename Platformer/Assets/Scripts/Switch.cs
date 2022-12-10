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
}

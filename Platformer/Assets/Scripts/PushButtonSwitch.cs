using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PushButtonSwitch : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    [SerializeField] Sprite downSprite;
    [SerializeField] UnityEvent onEnter;

    void OnTriggerEnter2D(Collider2D collision)
    {
        spriteRenderer= GetComponent<SpriteRenderer>(); 
        var player = collision.GetComponent<Player>();
        if (player == null)
        {
            return;
        }
        spriteRenderer.sprite = downSprite;

        onEnter?.Invoke();
    }
}

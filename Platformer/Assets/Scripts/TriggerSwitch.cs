using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSwitch : MonoBehaviour
{
    [SerializeField] Sprite left;
    [SerializeField] Sprite right;

    SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer= GetComponent<SpriteRenderer>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<Player>();
        if (player == null) 
        {
            return;
        }
        bool wasOnRight = collision.transform.position.x > transform.position.x;    

        spriteRenderer.sprite = wasOnRight? left : right;

        /*
        if(wasOnRight)
        {
            spriteRenderer.sprite = right;
        }
        else
        {
            spriteRenderer.sprite = left;
        }
        */
    }
}

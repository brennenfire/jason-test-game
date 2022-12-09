﻿using System;
using UnityEngine;

public class HittableFromBelow : MonoBehaviour 
{
    [SerializeField] protected Sprite usedSprite;

    protected virtual bool CanUse => true;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (CanUse == false)
        {
            return;
        }
        var player = collision.collider.GetComponent<Player>();
        if (player == null)
        {
            return;
        }
        if (collision.contacts[0].normal.y > 0)
        {
            Use();
            if (CanUse == false)
            {
                GetComponent<SpriteRenderer>().sprite = usedSprite;
            }
        }
    }

    protected virtual void Use()
    {
        
    }
}

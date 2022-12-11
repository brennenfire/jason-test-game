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

    void OnTriggerStay2D(Collider2D other)
    {
        var player = other.GetComponent<Player>();
        if (player == null) 
        {
            return;
        }
        var playerRigidBody = player.GetComponent<Rigidbody2D>();
        if(playerRigidBody == null)
        {
            return;
        }

        bool wasOnRight = other.transform.position.x > transform.position.x;
        bool playerWalkingRight = playerRigidBody.velocity.x > 0;
        bool playerWalkingLeft = playerRigidBody.velocity.x < 0;

        //spriteRenderer.sprite = (wasOnRight && playerWalkingRight)? right : left; // prost


        if (wasOnRight && playerWalkingRight)
        {
            spriteRenderer.sprite = right;
        }
        else if(!wasOnRight && playerWalkingLeft)
        {
            spriteRenderer.sprite = left;
        }
        
    }
}

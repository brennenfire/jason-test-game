using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerSwitch : MonoBehaviour
{
    [SerializeField] UnityEvent onLeft;
    [SerializeField] UnityEvent onRight;

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
            SetPosition(true);
        }
        else if(!wasOnRight && playerWalkingLeft)
        {
            SetPosition(left);
        }
        
    }

    void SetPosition(bool isRight)
    {
        
        if (right)
        {
            spriteRenderer.sprite = right;
            onRight.Invoke();
        }
        else
        {
            spriteRenderer.sprite = left;
            onLeft.Invoke();
        }
    }

    public void LogUsingEvent()
    {
        Debug.Log("Using Event");
    }
}

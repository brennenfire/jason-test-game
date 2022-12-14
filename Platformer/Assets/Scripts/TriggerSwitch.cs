using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerSwitch : MonoBehaviour
{
    [SerializeField] UnityEvent onLeft;
    [SerializeField] UnityEvent onRight;
    [SerializeField] UnityEvent onCenter;

    [SerializeField] Sprite left;
    [SerializeField] Sprite right;
    [SerializeField] Sprite center;

    SpriteRenderer spriteRenderer;
    ToggleDirection currentDirection;
    
    enum ToggleDirection
    {
        Left,
        Center,
        Right 
    };

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
            SetToggleDirection(ToggleDirection.Right);
        }
        else if(!wasOnRight && playerWalkingLeft)
        {
            SetToggleDirection(ToggleDirection.Left);
        }
        
    }

    void SetToggleDirection(ToggleDirection direction)
    {
        if(currentDirection == direction)
        {
            return;
        }
        switch (direction)
        {
            case ToggleDirection.Left:
                spriteRenderer.sprite = left;
                onLeft.Invoke();
                break;
            case ToggleDirection.Center:
                spriteRenderer.sprite = center;
                onCenter.Invoke();
                break;
            case ToggleDirection.Right:
                spriteRenderer.sprite = right;
                onRight.Invoke();
                break;
            default:

                break;
        }
    }

    public void LogUsingEvent()
    {
        Debug.Log("Using Event");
    }
}

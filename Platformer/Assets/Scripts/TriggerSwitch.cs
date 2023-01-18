using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerSwitch : MonoBehaviour
{
    [SerializeField] ToggleDirection startingDirection = ToggleDirection.Center;

    [SerializeField] UnityEvent onLeft;
    [SerializeField] UnityEvent onRight;
    [SerializeField] UnityEvent onCenter;

    [SerializeField] Sprite left;
    [SerializeField] Sprite right;
    [SerializeField] Sprite center;

    [SerializeField] AudioClip leftSound;
    [SerializeField] AudioClip rightSound;

    SpriteRenderer spriteRenderer;
    ToggleDirection currentDirection;
    AudioSource audioSource;

    enum ToggleDirection
    {
        Left,
        Center,
        Right 
    };

    void Start()
    {
        //spriteRenderer= GetComponent<SpriteRenderer>();
        SetToggleDirection(startingDirection, true);
        audioSource = GetComponent<AudioSource>();

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

    void SetToggleDirection(ToggleDirection direction, bool force = false)
    {
        
        if(force == false && currentDirection == direction)
        {
            return;
        }
        currentDirection = direction;
        switch (direction)
        {
            case ToggleDirection.Left:
                if (audioSource != null)
                {
                    audioSource.PlayOneShot(leftSound);
                }
                spriteRenderer.sprite = left;
                onLeft.Invoke();
                break;
            case ToggleDirection.Center:
                spriteRenderer.sprite = center;
                onCenter.Invoke();
                break;
            case ToggleDirection.Right:
                if (audioSource != null)
                {
                    audioSource.PlayOneShot(rightSound);
                }
                spriteRenderer.sprite = right;
                onRight.Invoke();
                break;
            default:
                break;
        }
    }

    void OnValidate()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        switch (startingDirection)
        {
            case ToggleDirection.Left:
                spriteRenderer.sprite = left;
                break;
            case ToggleDirection.Center:
                spriteRenderer.sprite = center;
                break;
            case ToggleDirection.Right:
                spriteRenderer.sprite = right;
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

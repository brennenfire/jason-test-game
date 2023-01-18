using System;
using UnityEngine;

public class HittableFromBelow : MonoBehaviour 
{
    [SerializeField] protected Sprite usedSprite;
    Animator animator;
    AudioSource audioSource;

    protected virtual bool CanUse => true;

    void Awake()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }
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
            PlayAudio();
            PlayAnimation();
            Use();
            if (CanUse == false)
            {
                GetComponent<SpriteRenderer>().sprite = usedSprite;
            }
        }
    }

    private void PlayAudio()
    {
        if (audioSource != null)
        {
            GetComponent<AudioSource>().Play();
        }
    }

    void PlayAnimation()
    {
        if(animator != null) 
        {
            animator.SetTrigger("Use");
        }
    }

    protected virtual void Use()
    {
        
    }
}

using System.Collections;
using UnityEngine;

public class FadingCloud : HittableFromBelow
{
    [SerializeField] float resetTime = 2f;

    SpriteRenderer spriteRenderer;
    new Collider2D collider;
    
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        collider = GetComponent<Collider2D>();
    }

    protected override void Use()
    {
        spriteRenderer.enabled = false;
        collider.enabled = false;

        StartCoroutine(ResetAfterDelay());
    }

    IEnumerator ResetAfterDelay()
    {
        yield return new WaitForSeconds(resetTime);
        spriteRenderer.enabled = true;
        collider.enabled = true;
    }
}

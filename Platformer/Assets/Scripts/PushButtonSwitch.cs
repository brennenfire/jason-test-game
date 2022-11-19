using UnityEngine;
using UnityEngine.Events;

public class PushButtonSwitch : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    Sprite releasedSprite;
    [SerializeField] Sprite pressedSprite;
    [SerializeField] UnityEvent onPressed;
    [SerializeField] UnityEvent onReleased;


    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        releasedSprite = spriteRenderer.sprite;
        BecomeReleased();

    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<Player>();
        if (player == null)
        {
            return;
        }

        BecomePressed();
    }

    void BecomePressed()
    {
        spriteRenderer.sprite = pressedSprite;
        onPressed?.Invoke();
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        var player = collision.GetComponent<Player>();
        if (player == null)
        {
            return;
        }

        BecomeReleased();
    }

    void BecomeReleased()
    {
        spriteRenderer.sprite = releasedSprite;
        onReleased?.Invoke();
    }
}

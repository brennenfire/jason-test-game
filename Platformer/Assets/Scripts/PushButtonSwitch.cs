using UnityEngine;
using UnityEngine.Events;

public class PushButtonSwitch : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    Sprite releasedSprite;
    [SerializeField] Sprite pressedSprite;
    [SerializeField] UnityEvent onPressed;
    [SerializeField] UnityEvent onReleased;
    [SerializeField] int wrongPlayer = 2;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        releasedSprite = spriteRenderer.sprite; 
        BecomeReleased();

    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<Player>();
        if (player == null || player.PlayerNumber == wrongPlayer)
        {
            return;
        }
        BecomePressed();
    }

    void BecomePressed()
    {
        spriteRenderer.sprite = pressedSprite;
        onPressed?.Invoke();
        GetComponent<AudioSource>().Play();
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        var player = collision.GetComponent<Player>();
        if (player == null || player.PlayerNumber == wrongPlayer)
        {
            return;
        }

        BecomeReleased();
    }

    void BecomeReleased()
    {
        if (onReleased.GetPersistentEventCount() != 0)
        {
            spriteRenderer.sprite = releasedSprite;
            onReleased?.Invoke();
        }
    }

}

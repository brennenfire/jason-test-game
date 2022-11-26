using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] Sprite openMid;
    [SerializeField] Sprite openTop;
    [SerializeField] SpriteRenderer rendererMid;
    [SerializeField] SpriteRenderer rendererTop;
    [SerializeField] int requiredCoins = 3;
    [SerializeField] Door exit;
    [SerializeField] Canvas canvas;

    bool open;

    [ContextMenu("Open Door")]
    void Open()
    {
        open = true;
        rendererMid.sprite = openMid;
        rendererTop.sprite = openTop;
        if (canvas != null)
        {
            canvas.enabled = false;
        }
    }

    void Update()
    {
        if(open == false && Coin.CoinsCollected >= requiredCoins)
        {
            Open();
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(open == false)
        {
            return;
        }
        var player = collision.GetComponent<Player>();
        if (player != null && exit != null)
        {
            player.TeleportTo(exit.transform.position);
        }
    }
}

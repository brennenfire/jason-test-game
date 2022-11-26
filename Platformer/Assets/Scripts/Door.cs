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

    [ContextMenu("Open Door")]
    void Open()
    {
        rendererMid.sprite = openMid;
        rendererTop.sprite = openTop;
    }

    void Update()
    {
        if(Coin.CoinsCollected >= requiredCoins)
        {
            Open();
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<Player>();
        if (player != null && exit != null)
        {
            player.TeleportTo(exit.transform.position);
        }
    }
}

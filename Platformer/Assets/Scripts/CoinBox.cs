using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class CoinBox : MonoBehaviour
{
    [SerializeField] int totalCoins = 3;
    [SerializeField] Sprite usedSprite;

    int remainingCoins;

    void Start()
    {
        remainingCoins = totalCoins;    
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        var player = collision.collider.GetComponent<Player>();
        if (player == null)
        {
            return;
        }
        if (collision.contacts[0].normal.y > 0 && remainingCoins > 0)
        {
            remainingCoins--;
            Coin.CoinsCollected++;
            if(remainingCoins <= 0) 
            {
                GetComponent<SpriteRenderer>().sprite = usedSprite;
            }
        }
    }
}

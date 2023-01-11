using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Coin : MonoBehaviour
{
    public static int CoinsCollected;

    void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<Player>();
        if(player == null)
        {
            return;
        }
        CoinsCollected++;
        Score.Add(10);
        GetComponent<AudioSource>().Play();
        GetComponent<Collider2D>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
        Debug.Log(CoinsCollected);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Coin : MonoBehaviour
{
    public static int CoinsCollected;
    int index;

    [SerializeField] List<AudioClip> audioList;

    void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<Player>();
        if(player == null)
        {
            return;
        }
        CoinsCollected++;
        Score.Add(10);
        if(audioList.Count > 0)
        {
            index = Random.Range(0, audioList.Count);
            AudioClip clip = audioList[index];
            GetComponent<AudioSource>().PlayOneShot(clip);
        }
        else
        {
            GetComponent<AudioSource>().Play();
        }
        GetComponent<Collider2D>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
        //Debug.Log(CoinsCollected);
    }
}

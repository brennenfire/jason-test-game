using System;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    Collector thisCollector;

    void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<Player>();
        if (player == null) 
        {
            return;
        }

        gameObject.SetActive(false);
        thisCollector.ItemPickedUp();
    }
    public void SetCollector(Collector collector)
    {
        thisCollector = collector;
    }
}
﻿using System;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    public event Action OnPickedUp;
    void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<Player>();
        if (player == null) 
        {
            return;
        }

        gameObject.SetActive(false);
        /*foreach (var collector in collectorList)
        {
            collector.ItemPickedUp();
        }
        */
        /*
        if(OnPickedUp != null)
        {
            OnPickedUp.Invoke();
        }
        */
        OnPickedUp?.Invoke();
    }
    
}
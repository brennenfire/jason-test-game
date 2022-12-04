using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Collector : MonoBehaviour
{
    [SerializeField] List<Collectible> collectibles;
    [SerializeField] UnityEvent collectionComplete;

    TMP_Text remainingText;
    int countCollected;
    int countRemaining;

    void Start()
    {
        remainingText = GetComponentInChildren<TMP_Text>();
        foreach (var collectible in collectibles)
        {
            collectible.SetCollector(this);
        }
        countRemaining = collectibles.Count - countCollected;
        remainingText?.SetText(countRemaining.ToString());
    }
    public void ItemPickedUp()
    {
        countCollected++;
        countRemaining = collectibles.Count - countCollected;
        remainingText?.SetText(countRemaining.ToString());

        if (countRemaining > 0) 
        {
            return;
        }

        collectionComplete.Invoke();
    }

    void OnValidate()
    {
        collectibles = collectibles.Distinct().ToList();
    }

}

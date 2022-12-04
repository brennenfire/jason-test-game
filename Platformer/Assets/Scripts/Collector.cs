using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class Collector : MonoBehaviour
{
    [SerializeField] List<Collectible> collectibles;
    TMP_Text remainingText;

    void Start()
    {
        remainingText = GetComponentInChildren<TMP_Text>();
    }
    void Update()
    {
        int countRemaining = 0;
        foreach(var collectible in collectibles) 
        {
            if(collectible.isActiveAndEnabled) 
            {
                countRemaining++;
            }
        }
        remainingText?.SetText(countRemaining.ToString());

        if (countRemaining > 0) 
        {
            return;
        }

        Debug.Log("Got all gems");
    }

    void OnValidate()
    {
        collectibles = collectibles.Distinct().ToList();
    }
}

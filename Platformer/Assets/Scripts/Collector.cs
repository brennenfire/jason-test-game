using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Collector : MonoBehaviour
{
    [SerializeField] List<Collectible> collectibles;
    [SerializeField] UnityEvent collectionComplete;
    static Color gizmoColor = new Color(0.61f, 0.61f, 0.61f, 1);

    TMP_Text remainingText;
    int countCollected;
    int countRemaining;
    
    void Start()
    {
        remainingText = GetComponentInChildren<TMP_Text>();
        foreach (var collectible in collectibles)
        {
            collectible.OnPickedUp += ItemPickedUp;
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

    /*
    void OnDrawGizmos()
    {
        Gizmos.color = Color.gray;
        foreach (var collectible in collectibles) 
        {
            Gizmos.DrawLine(transform.position, collectible.transform.position);
        }
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        foreach (var collectible in collectibles)
        {
            Gizmos.DrawLine(transform.position, collectible.transform.position);
        }
    }
    */
    void OnDrawGizmos()
    {
        foreach(var collectible in collectibles) 
        {
            if(UnityEditor.Selection.activeGameObject == gameObject) 
            {
                Gizmos.color = Color.yellow;
            }
            else
            {
                Gizmos.color = gizmoColor;
            }
            Gizmos.DrawLine(transform.position, collectible.transform.position);
        }    
    }

}

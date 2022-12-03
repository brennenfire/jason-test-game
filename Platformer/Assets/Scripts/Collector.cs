using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Collector : MonoBehaviour
{
    [SerializeField] Collectible[] collectibles;

    void Update()
    {
        foreach(var collectible in collectibles) 
        {
            if(collectible.isActiveAndEnabled) 
            {
                return;
            }
        }

        Debug.Log("Got all gems");
    }
}

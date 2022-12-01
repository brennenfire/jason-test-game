using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Collector : MonoBehaviour
{
    [SerializeField] Collectible[] collectibles;

    void Update()
    {
        if (collectibles.Any(t => t.gameObject.activeSelf == true))
        {
            return;
        }

        Debug.Log("Got all gems");
    }
}

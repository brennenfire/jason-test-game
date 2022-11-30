using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    [SerializeField] KeyLock keyLockType;
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<Player>();
        if(player != null)
        {
            transform.SetParent(player.transform, true);
            //transform.position = player.transform.position + Vector3.up * collectedKeys;
            transform.localPosition = Vector3.up;
        }
        
        var keyLock = collision.GetComponent<KeyLock>();
        if(keyLock == keyLockType && keyLockType != null)
        {
            keyLock.Unlock();
            Destroy(gameObject);
        }
    }
}

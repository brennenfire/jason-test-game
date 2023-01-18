using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    //[SerializeField] KeyLock keyLockType;
    [SerializeField] List<KeyLock> keyLockType = new List<KeyLock>();
    int keyCount = 0;
    AudioSource audioSource;

    void Awake() => audioSource = GetComponent<AudioSource>();

    void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<Player>();
        if(player != null)
        {
            if(audioSource== null)
            {
                audioSource.Play();
            }
            transform.SetParent(player.transform, true);
            //transform.position = player.transform.position + Vector3.up * collectedKeys;
            transform.localPosition = Vector3.up;
        }
        
        var keyLock = collision.GetComponent<KeyLock>();
        
        if(keyLockType.Contains(keyLock) && keyLockType != null)
        {
            keyLock.Unlock();
            keyCount++;
            if (keyCount == keyLockType.Count)
            {
                Destroy(gameObject);
            }
        }
        
    }
    
}

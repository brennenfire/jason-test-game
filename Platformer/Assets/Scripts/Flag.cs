using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flag : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<Player>();
        if(player == null)
        {
            return;
        }

        // play flag wave
        var animator = GetComponent<Animator>();
        animator.SetTrigger("Raise");
        //load new level
    }
}

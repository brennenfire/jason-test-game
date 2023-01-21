using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    [SerializeField] float launchForce = 1.5f;

    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.right * launchForce; // new Vector2(launcForce, 0);
    }

    void Update()
    {
       
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FireballLauncher : MonoBehaviour
{
    [SerializeField] Fireball fireballPrefab;
    [SerializeField] Player player;
    [SerializeField] float fireRate = 0.25f;

    string fireButton;
    string horizontalAxis;
    float nextFireTime;
    
    private void Start()
    {
        fireButton = $"P{player.PlayerNumber}Fire";
        horizontalAxis = $"P{player.PlayerNumber}Horizontal";
    }

    private void Update()
    {
        if (Input.GetButtonDown(fireButton) && Time.time >= nextFireTime)
        {
            var horizontal = Input.GetAxis(horizontalAxis);
            Fireball fireball = Instantiate(fireballPrefab, transform.position, Quaternion.identity);
            fireball.direction = horizontal > 0 ? 1f : -1f;
            nextFireTime = Time.time + fireRate;   
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FireballLauncher : MonoBehaviour
{
    [SerializeField] Fireball fireballPrefab;
    [SerializeField] Player player;
    string fireButton;

    private void Start()
    {
        fireButton = $"P{player.PlayerNumber}Fire";
    }

    private void Update()
    {
        if (Input.GetButtonDown(fireButton))
        {
            Instantiate(fireballPrefab, transform.position, Quaternion.identity);
        }
    }
}
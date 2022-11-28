using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    HashSet<Player> playersInTrigger = new HashSet<Player>();
    public bool PlayerInside;
    Coroutine coroutine;
    Vector3 initialPosition;
    bool falling;
    float wiggleTimer;

    [Tooltip("resets the wiggle timer when no players are on the platform")]
    [SerializeField] bool resetOnEmpty = false;
    [SerializeField] float fallSpeed = 9;
    [Range(0.1f, 5f)] [SerializeField] float fallAfterSeconds = 3;
    [Range(0.005f, 0.1f)] [SerializeField] float shakeX = 0.005f;
    [Range(0.005f, 0.1f)] [SerializeField] float shakeY = 0.005f;
    
    private void Start()
    {
        initialPosition = transform.position;
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<Player>();
        if(player == null)
        {
            return;
        }

        playersInTrigger.Add(player);

        PlayerInside = true;

        if(playersInTrigger.Count == 1)
        coroutine = StartCoroutine(WiggleAndFall());
    }

    IEnumerator WiggleAndFall()
    {
        Debug.Log("Waiting to wiggle");
        yield return new WaitForSeconds(0.25f);
        Debug.Log("Wiggling");
        //wiggleTimer = 0;
        while (wiggleTimer < fallAfterSeconds)
        {
            
            float randomX = UnityEngine.Random.Range(-shakeX, shakeX);
            float randomY = UnityEngine.Random.Range(-shakeY, shakeY);
            transform.position = initialPosition + new Vector3(randomX, randomY);
            float randomDelay = UnityEngine.Random.Range(0.005f, 0.01f);
            yield return new WaitForSeconds(randomDelay);
            wiggleTimer += randomDelay;
        }
        Debug.Log("Falling");
        falling = true;
        Collider2D[] colliders = GetComponents<Collider2D>();
        foreach(var collider in colliders)
        {
            collider.enabled = false;
        }

        float fallTimer = 0f;
        while(fallTimer < 3f)
        {
            transform.position += Vector3.down * Time.deltaTime * fallSpeed;
            Debug.Log(transform.position);
            fallTimer += Time.deltaTime;
            yield return null;
        }

        Destroy(gameObject);
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if(falling)
        {
            return;
        }
        var player = collision.GetComponent<Player>();
        if (player == null)
        {
            return;
        }

        playersInTrigger.Remove(player);

        if (playersInTrigger.Count == 0)
        {
            PlayerInside = false;
            StopCoroutine(coroutine);

            if(resetOnEmpty)
            {
                wiggleTimer = 0f;
            }
        }
    }
}

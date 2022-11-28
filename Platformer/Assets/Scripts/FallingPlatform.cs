using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    public bool PlayerInside;
    Coroutine coroutine;
    Vector3 initialPosition;

    HashSet<Player> playersInTrigger = new HashSet<Player>();
    [SerializeField] float fallSpeed;
    bool falling;

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
        float wiggleTimer = 0;
        while (wiggleTimer < 1f)
        {
            
            float randomX = UnityEngine.Random.Range(-0.05f, 0.05f);
            float randomY = UnityEngine.Random.Range(-0.05f, 0.05f);
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
        }
    }
}

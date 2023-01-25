using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class Slime : MonoBehaviour, ITakeDamage
{
    [SerializeField] Transform leftSensor;
    [SerializeField] Transform rightSensor;
    [SerializeField] Sprite deadSprite;
    [SerializeField] List<AudioClip> audioList;

    new Rigidbody2D rigidbody2D;
    SpriteRenderer sprite;
    float direction = -1;

    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        StartCoroutine(AlternateSound());
    }

    void Update()
    {
        if (direction < 0)
        {
            ScanSensor(leftSensor);
        }
        else
        {
            ScanSensor(rightSensor);
        }
    }

    void ScanSensor(Transform sensor)
    {
        Debug.DrawRay(sensor.position, Vector2.down * 0.1f, Color.red);

        //StartCoroutine(AlternateSound());
        //AudioClip clip = audioList[0];
        //GetComponent<AudioSource>().PlayOneShot(clip);

        rigidbody2D.velocity = new Vector2(direction, rigidbody2D.velocity.y);
        var result = Physics2D.Raycast(sensor.position, Vector2.down, 0.1f);
        if (result.collider == null)
        {
            TurnAround();
        }

        Debug.DrawRay(sensor.position, new Vector2(direction, 0) * 0.1f, Color.red);
        var sideResult = Physics2D.Raycast(sensor.position, new Vector2(direction, 0), 0.1f);
        if(sideResult.collider != null)
        {
            TurnAround();
        }
    }

    

    void TurnAround()
    {
        direction *= -1;
        sprite.flipX = direction > 0;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        var player = collision.collider.GetComponent<Player>();
        if((player == null))
        {
            return;
        }

        var contact = collision.contacts[0];
        Vector2 normal = contact.normal;
        Debug.Log($"Normal = {normal}");

        if(normal.y <= -0.5)
        {
            PlayDeathSound();
            TakeDamage();
        }
        else
        {
            player.ResetToStart();
        }
    }

    

    IEnumerator Die()
    {
        sprite.sprite = deadSprite;
        GetComponent<Animator>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
        GetComponent<Rigidbody2D>().simulated = false;
        GetComponent<AudioSource>().enabled = false;
        float alpha = 1;
        while (alpha > 0)
        {
            yield return null;
            alpha -= Time.deltaTime;
            sprite.color = new Color(1, 1, 1, alpha);
        }
        
    }

    IEnumerator AlternateSound()
    {
        float seconds = 0.5f;
        AudioClip clip = audioList[0];
        GetComponent<AudioSource>().PlayOneShot(clip);
        yield return new WaitForSeconds(seconds);
        clip = audioList[1];
        GetComponent<AudioSource>().PlayOneShot(clip);
        yield return new WaitForSeconds(seconds);
        StartCoroutine(AlternateSound());
    }

    void PlayDeathSound()
    {
        AudioClip clip = audioList[2];
        GetComponent<AudioSource>().PlayOneShot(clip);
    }

    public void TakeDamage()
    {
        StartCoroutine(Die());
    }
}

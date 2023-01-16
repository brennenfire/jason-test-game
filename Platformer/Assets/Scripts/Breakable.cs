using UnityEngine;

public class Breakable : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.GetComponent<Player>() == null)
        {
            return;
        }
        if (collision.contacts[0].normal.y > 0)
        {
            TakeHit();
        }

    }

    void TakeHit()
    {
        var particleSystem = GetComponent<ParticleSystem>();
        particleSystem.Play();
        GetComponent<AudioSource>().Play();
        GetComponent<SpriteRenderer>().enabled  = false;
        GetComponent<BoxCollider2D>().enabled = false;
    }
}

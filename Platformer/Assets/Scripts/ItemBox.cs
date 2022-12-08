using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBox : MonoBehaviour
{
    [SerializeField] Sprite usedSprite;
    [SerializeField] GameObject item;
    [SerializeField] Vector2 itemLaunchVelocity;
    bool used;

    void Start()
    {
        if (item != null)
        {
            item.SetActive(false);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (used)
        {
            return;
        }
        var player = collision.collider.GetComponent<Player>();
        if (player == null)
        {
            return;
        }
        if (collision.contacts[0].normal.y > 0)
        {
            GetComponent<SpriteRenderer>().sprite = usedSprite;
            if (item != null)
            {
                used = true;
                item.SetActive(true);
                var itemRigidbody = item.GetComponent<Rigidbody2D>();
                if(itemRigidbody != null)
                {
                    itemRigidbody.velocity = itemLaunchVelocity;
                }
            }
        }
    }
}

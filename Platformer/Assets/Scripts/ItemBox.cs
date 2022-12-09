using UnityEngine;

public class ItemBox : HittableFromBelow
{
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

    protected override bool CanUse => used == false && item != null;
    protected override void Use()
    {
        if(item == null)
        {
            return;
        }
        base.Use();
        used = true;
        item.SetActive(true);
        var itemRigidbody = item.GetComponent<Rigidbody2D>();
        if (itemRigidbody != null)
        {
            itemRigidbody.velocity = itemLaunchVelocity;
        }
    }

}

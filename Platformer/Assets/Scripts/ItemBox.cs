using UnityEngine;

public class ItemBox : HittableFromBelow
{
    [SerializeField] GameObject itemPrefab;
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

    protected override bool CanUse => used == false;
    protected override void Use()
    {
        item = Instantiate(
            itemPrefab,
            transform.position + Vector3.up,
            Quaternion.identity,
            transform);

        if(item == null)
        {
            return;
        }
        used = true;
        item.SetActive(true);
        var itemRigidbody = item.GetComponent<Rigidbody2D>();
        if (itemRigidbody != null)
        {
            itemRigidbody.velocity = itemLaunchVelocity;
        }
    }

}

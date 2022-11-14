using UnityEngine;

public class Fly : MonoBehaviour
{
    Vector2 startingPosition;
    [SerializeField] Vector2 direction = Vector2.up;
    [SerializeField] float maxDistance = 2;
    [SerializeField] float speed = 2;
    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(direction.normalized * Time.deltaTime * speed);
        var distance = Vector2.Distance(startingPosition, transform.position);
        if(distance >= maxDistance)
        {
            transform.position = startingPosition + (direction.normalized * maxDistance);
            direction *= -1;
        }
    }
}

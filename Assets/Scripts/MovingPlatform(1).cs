using UnityEngine;

public class MovingPlatform : MonoBehaviour


{
    public float distance = 3f;   // how far to move left/right
    public float speed = 2f;      // movement speed

    private Vector3 startPos;
    private int direction = 1;

    void Start()
    {
        // remember the starting position
        startPos = transform.position;
    }

    void Update()
    {
        // move platform along X axis
        transform.Translate(Vector3.right * direction * speed * Time.deltaTime);

        // check distance from start
        if (Mathf.Abs(transform.position.x - startPos.x) >= distance)
        {
            // flip direction
            direction *= -1;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // make player a child so they ride the platform
            collision.transform.SetParent(transform);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // detach player when leaving
            collision.transform.SetParent(null);
        }
    }
}
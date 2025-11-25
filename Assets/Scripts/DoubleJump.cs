using UnityEngine;

public class DoubleJump : MonoBehaviour
{
    public int extraJumpsValue = 1;   // how many extra jumps allowed (1 = double jump, 2 = triple jump)
    private int extraJumps;           // counter for jumps left
    private Rigidbody2D rb;           // reference to Rigidbody2D for movement

    public Transform groundCheck;     // empty object at Player’s feet
    public LayerMask groundLayer;     // layer mask for ground detection
    private bool isGrounded;          // true when Player is touching ground

    public float jumpForce = 10f;     // upward force applied when jumping

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // get Rigidbody2D attached to Player
    }

    void Update()
    {
        // check if GroundCheck overlaps with ground layer
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);

        if (isGrounded)
        {
            extraJumps = extraJumpsValue; // reset extra jumps when grounded
        }

        if (Input.GetKeyDown(KeyCode.Space)) // when Space is pressed
        {
            if (isGrounded)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce); // normal jump
            }
            else if (extraJumps > 0)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce); // extra jump
                extraJumps--; // reduce available extra jumps
            }
        }
    }
}

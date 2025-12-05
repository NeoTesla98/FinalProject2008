using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int coins;

    // --- Movement & Animation ---
    private Animator animator;             // Reference to Animator for controlling animations
    public float moveSpeed = 4f;           // How fast the player moves left/right

    // --- Jump variables ---
    public float jumpForce = 8f;           // Base jump force (vertical speed)
    public int extraJumpsValue = 1;        // How many extra jumps allowed (1 = double jump, 2 = triple jump)
    private int extraJumps;                // Counter for jumps left

    public Transform groundCheck;          // Empty child object placed at the player's feet
    public float groundCheckRadius = 0.2f; // Size of the circle used to detect ground
    public LayerMask groundLayer;          // Which layer counts as "ground" (set in Inspector)

    // --- Internal state ---
    private Rigidbody2D rb;                // Reference to the Rigidbody2D component
    private bool isGrounded;               // True if player is standing on ground

    void Start()
    {
        // Grab references once at the start
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // --- Horizontal movement ---
        // Get input from keyboard (A/D or Left/Right arrows).
        float moveInput = Input.GetAxis("Horizontal");

        // Apply horizontal speed while keeping the current vertical velocity.
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

        // --- Ground check ---
        // Create an invisible circle at the GroundCheck position.
        // If this circle overlaps any collider on the "Ground" layer, player is grounded.
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);


        Debug.Log("Grounded" + isGrounded);

        // Reset extra jumps when grounded
        if (isGrounded)
        {
            extraJumps = extraJumpsValue;
        }

        // --- Jump & Double Jump ---
        // If Space is pressed:
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded)
            {
                // Normal jump
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            }
            else if (extraJumps > 0)
            {
                // Extra jump (double or triple depending on extraJumpsValue)
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
                extraJumps--; // Reduce available extra jumps
            }
        }

        // --- Animations ---
        SetAnimation(moveInput);
    }

    private void SetAnimation(float moveInput)
    {
        // Handle animations based on grounded state and movement
        if (isGrounded)
        {
            if (moveInput == 0)
            {
                animator.Play("Player_Idle"); // Idle animation when not moving
            }
            else
            {
                animator.Play("Player_Run");  // Run animation when moving
            }
        }
        else
        {
            if (rb.linearVelocityY > 0)
            {
                animator.Play("Player_Jump"); // Jump animation when moving upward
            }
            else
            {
                animator.Play("Player_Fall"); // Fall animation when moving downward
            }
        }
    }
}

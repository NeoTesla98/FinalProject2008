using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int coins;

    // --- Movement & Animation ---
    private Animator animator;             // Reference to Animator for controlling animations
    public float moveSpeed = 4f;           // How fast the player moves left/right

    // --- Jump variables ---
    public float jumpForce = 7.5f;
    public float jumpContinuesForce = 1f;// Base jump force (vertical speed)
    public int extraJumpsValue = 1;        // How many extra jumps allowed (1 = double jump, 2 = triple jump)
    private int extraJumps;                // Counter for jumps left

    public Transform groundCheck;          // Empty child object placed at the player's feet
    public float groundCheckRadius = 0.2f; // Size of the circle used to detect ground
    public LayerMask groundLayer;
    // Which layer counts as "ground" (set in Inspector)
    public float coyoteTime = 0.2f;
    private float coyoteTimeCounter;

    // --- Internal state ---
    private Rigidbody2D rb;                // Reference to the Rigidbody2D component
    private bool isGrounded;

    // True if player is standing on ground
    public float jumpBufferTime = 0.15f;
    private float jumpBufferCounter;
    // Addng audio
    private AudioSource audioSource;
    public AudioClip jumpClip;

    void Start()
    {
        // Grab references once at the start
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
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
            coyoteTimeCounter = coyoteTime;
            extraJumps = extraJumpsValue;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpBufferCounter = jumpBufferTime;
        }
        else
        {
            jumpBufferCounter -= Time.deltaTime;    
        }

        // --- Jump & Double Jump ---
        // If Space is pressed:
        if (jumpBufferCounter > 0f)
        {
            if (coyoteTimeCounter > 0f)
            {
                // Normal jump
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
                SoundManager.Instance.PlaySFX("JUMP");
                coyoteTimeCounter = 0f;
                jumpBufferCounter = 0f;
            }
            else if (extraJumps > 0)
            {
                // Extra jump (double or triple depending on extraJumpsValue)
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
                extraJumps--; // Reduce available extra jumps
                SoundManager.Instance.PlaySFX("JUMP");
                jumpBufferCounter = 0f;
            }
        }
        if(Input .GetKey(KeyCode.Space) &&  rb.linearVelocityY > 0)
        {
            rb.AddForceY(jumpContinuesForce);
        }

        // --- Animations ---
        SetAnimation(moveInput);
        if(rb.linearVelocityY < 0)
        {
            rb.gravityScale = 2f;
        }
        else
        {
            rb.gravityScale = 1.5f;

        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("BouncePad"))
        {
            // Stronger bounce
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce * 2f);

            // Sound effect (optional)
           
        }
        if (collision.gameObject.tag == "Banana")
        {
            extraJumps = 2;
            Destroy(collision.gameObject);
        }
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
    public void PlaySFX(AudioClip audioClip)
    {
        audioSource.clip = audioClip;
        audioSource.Play();
    }
    internal void PlaySFX(AudioClip coinClip, float v)
    {
        throw new NotImplementedException();
    }
}

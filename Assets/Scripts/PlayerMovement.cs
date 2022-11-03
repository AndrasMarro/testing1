using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;
    public Transform ceilingCheck;
    public Transform groundCheck;
    public LayerMask groundObjects;
    public float checkRadius;
    public int maxJumpCount;
    public int jumpCooldown = 300;

    private Rigidbody2D rb;
    private bool facingRight = true;
    private float moveDirection;
    private bool isJumping = false;
    private bool isGrounded;
    private int jumpCount;
    private bool wait = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        jumpCount = maxJumpCount;
    }

    // Update is called once per frame
    void Update()
    {
        // Get Inputs
        ProcessInputs();

        // Animate
        Animate();
    }

    void FixedUpdate()
    {
        // Check if grounded
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundObjects);
        if(isGrounded)
        {
            jumpCount = maxJumpCount;
        }

        // Move
        Move();
    }


    private void Move()
    {
        rb.velocity = new Vector2(moveDirection * moveSpeed, rb.velocity.y);
        if (isJumping && !wait)
        {
            rb.AddForce(new Vector2(0f, jumpForce));
            jumpCount--;
            Delay(jumpCooldown);
        }
        isJumping = false;
    }

    private void Animate()
    {
        if (moveDirection > 0 && !facingRight)
            FlipCharacter();
        else if (moveDirection < 0 && facingRight)
            FlipCharacter();
    }

    private void ProcessInputs() 
    { 
        moveDirection = Input.GetAxis("Horizontal"); 
        if (Input.GetButtonDown("Jump") && jumpCount > 0) 
        {
            isJumping = true;
        }
    }


    private void FlipCharacter()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }
    
    private async void Delay(int time)
    {
        wait = !wait;
        await Task.Delay(time);
        wait = !wait;
    }
}
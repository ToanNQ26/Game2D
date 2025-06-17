using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 15f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundCheck;
    private Animator animator;
    private bool isGrounded;

    private GameManager gameManager;
    private AudioManager audioManager;

    private Rigidbody2D rb;

    private void Awake()
    {
        this.gameManager = FindAnyObjectByType<GameManager>();
        this.audioManager = FindAnyObjectByType<AudioManager>();
        this.animator = GetComponent<Animator>();
        this.rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (gameManager.IsGameOver() || gameManager.IsGameWin()) return;
        this.UpdateAnimation();
        this.HandleMovement();
        this.HandleJump();
    }
    private void HandleMovement()
    {
        float moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
        if (moveInput > 0) transform.localScale = new Vector3(1, 1, 1);
        else if (moveInput < 0) transform.localScale = new Vector3(-1, 1, 1);
    }
    private void HandleJump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            audioManager.PlayJumpSound();
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);

    }

    private void UpdateAnimation()
    {
        bool isRunning = Mathf.Abs(rb.velocity.x) > 0.1f;
        bool isJumping = !isGrounded;
        animator.SetBool("IsRunning",isRunning);
        animator.SetBool("IsJumping",isJumping);
    }
    
}

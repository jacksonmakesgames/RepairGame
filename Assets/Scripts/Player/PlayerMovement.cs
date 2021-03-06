﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerMovement : MonoBehaviour
{
    public AudioClip[] footsteps;


    [SerializeField]
    LayerMask groundMask;

    [SerializeField]
    PhysicsMaterial2D noFriction;

    [SerializeField]
    private Vector3 velocity;

    [SerializeField]
    bool debug = false;

    [SerializeField]
    private float moveSpeed;


    protected Animator animator;
    private SpriteRenderer spriteRenderer;

    private Rigidbody2D rb;

    protected BoxCollider2D boxCollider;

    PhysicsMaterial2D originalPhysicsMaterial;

    CircleCollider2D groundCheck;

    bool isGrounded = false;

    [SerializeField]
    float jumpForce;

    public bool canMove = true;
    private void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        originalPhysicsMaterial = boxCollider.sharedMaterial;
    }

    float timeLastStep = 0.0f;
    public float timeBetweenSteps;
    public void Move()
    {
        
        float move = Input.GetAxisRaw("Horizontal");
        if (move != 0){
            boxCollider.sharedMaterial = noFriction;
            if (isGrounded && Time.time > timeBetweenSteps + timeLastStep) {
                timeLastStep = Time.time;
                GetComponent<AudioSource>().clip = footsteps[Random.Range(0,footsteps.Length)];
                GetComponent<AudioSource>().Play();

            }
        }
        else {
            boxCollider.sharedMaterial = originalPhysicsMaterial;
        }
        rb.velocity = new Vector2(move * moveSpeed, rb.velocity.y);

        // prevent player falling through floor //TODO: hacky...
        if (transform.position.y < -4.0f) {
            transform.position = new Vector3(transform.position.x, -    3.5f, transform.position.z);
        }

        //jump
        if (isGrounded && Input.GetButton("Jump")) {
              rb.velocity = (new Vector3(rb.velocity.x, jumpForce, 0 ));
        }
        
    }

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        }
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            animator = GetComponentInChildren<Animator>();
        }

        groundCheck = GetComponent<CircleCollider2D>();
    }


    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircleAll(transform.position + new Vector3(0, -.3f, 0), groundCheck.radius, groundMask).Length > 0;

        if (canMove)
            Move();
            
        else { rb.velocity = new Vector2(0, rb.velocity.y); boxCollider.sharedMaterial = originalPhysicsMaterial; }
        
        animator.SetFloat("VelocityX", Mathf.Abs(rb.velocity.x));
        animator.SetFloat("VelocityY", rb.velocity.y);

        //spriteRenderer.flipX = velocity.x < 0;
        spriteRenderer.flipX = Camera.main.ScreenToWorldPoint(Input.mousePosition).x < transform.position.x;
      

        //grounded:

        animator.SetBool("Grounded", isGrounded);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        //Gizmos.DrawWireSphere(transform.position + new Vector3(0, -.3f, 0), groundCheck.radius);
    }



}

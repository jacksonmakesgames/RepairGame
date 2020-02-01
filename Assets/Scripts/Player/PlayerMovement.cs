﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerMovement : MonoBehaviour
{


    [SerializeField]
    PhysicsMaterial2D noFriction;

    [SerializeField]
    private Vector3 velocity;

    [SerializeField]
    bool debug = false;

    public CollisionInfo collisions;

    [SerializeField]
    private float moveSpeed;


    protected Animator animator;
    private SpriteRenderer spriteRenderer;

    private Rigidbody2D rb;

    protected BoxCollider2D boxCollider;
    RaycastOrigins raycastOrigins;

    PhysicsMaterial2D originalPhysicsMaterial;
    struct RaycastOrigins
    {
        public Vector2 topLeft, bottomLeft, topRight, bottomRight;
    }

    public struct CollisionInfo
    {
        public bool above, below, left, right;
        public void Reset()
        {
            above = below = left = right = false;
        }

    }

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        originalPhysicsMaterial = boxCollider.sharedMaterial;
    }
    public void Move()
    {
        float move = Input.GetAxisRaw("Horizontal");
        if (move != 0){
            boxCollider.sharedMaterial = noFriction;
        }
        else {
            boxCollider.sharedMaterial = originalPhysicsMaterial;
        }
        rb.velocity = new Vector2(move * moveSpeed, rb.velocity.y);

        // prevent player falling through floor //TODO: hacky...
        if (transform.position.y < -4.0f) {
            transform.position = new Vector3(transform.position.x, -    3.5f, transform.position.z);
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
    }


    void FixedUpdate()
    {
        Move();
        
        animator.SetFloat("VelocityX", Mathf.Abs(rb.velocity.x));
        animator.SetFloat("VelocityY", rb.velocity.y);

        spriteRenderer.flipX = velocity.x < 0;

    }


   
}

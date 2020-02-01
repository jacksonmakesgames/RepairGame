using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    const float skinWidth = .015f;

    [SerializeField]
    PhysicsMaterial2D noFriction;

    [SerializeField]
    private Vector3 velocity;

    [SerializeField]
    bool debug = false;

    [SerializeField]
    private int horizontalRayCount = 4;
    [SerializeField]
    private int veritcalRayCount = 4;
    [SerializeField]
    private LayerMask collisionMask;

    private float maximumVerticalVelocity = 25f;

    float gravity = -20f;

    public CollisionInfo collisions;

    [SerializeField]
    private float moveSpeed;

    [SerializeField]
    float accelerationTimeGround = .05f;

    float velocityXSmoothing;

    protected Animator animator;
    private SpriteRenderer spriteRenderer;

    [SerializeField]
    float downGravityModifier = 1.2f;

    [SerializeField]
    LayerMask groundMask;

    float horizontalRaySpacing;
    float verticalRaySpacing;

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
        CalculateRaySpacing();
        originalPhysicsMaterial = boxCollider.sharedMaterial;
    }

    void UpdateRaycastOrigins()
    {
        Bounds bounds = boxCollider.bounds;
        bounds.Expand(skinWidth * -2);

        raycastOrigins.topLeft = new Vector2(bounds.min.x, bounds.max.y);
        raycastOrigins.bottomLeft = new Vector2(bounds.min.x, bounds.min.y);
        raycastOrigins.topRight = new Vector2(bounds.max.x, bounds.max.y);
        raycastOrigins.bottomRight = new Vector2(bounds.max.x, bounds.min.y);
    }

    void CalculateRaySpacing()
    {
        Bounds bounds = boxCollider.bounds;
        bounds.Expand(skinWidth * -2);
        horizontalRayCount = Mathf.Clamp(horizontalRayCount, 2, int.MaxValue);
        veritcalRayCount = Mathf.Clamp(veritcalRayCount, 2, int.MaxValue);

        horizontalRaySpacing = bounds.size.y / (horizontalRayCount - 1);
        verticalRaySpacing = bounds.size.x / (veritcalRayCount - 1);

    }


    public void Move(Vector3 velocity)
    {
        //UpdateRaycastOrigins();
        //collisions.Reset();

        //if (velocity.x != 0)
        //    HorizontalCollisions(ref velocity);
        //if (velocity.y != 0)
        //    VerticalCollisions(ref velocity);

        //transform.Translate(velocity);


        float move = Input.GetAxis("Horizontal");
        if (move != 0)
        {
            boxCollider.sharedMaterial = noFriction;
        }
        else {
            boxCollider.sharedMaterial = originalPhysicsMaterial;
        }
        rb.velocity = new Vector2(move * moveSpeed, rb.velocity.y);


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


    void Update()
    {

        //if ((collisions.below || collisions.above))
        //    velocity.y = 0;

        //Vector2 targetVelocity = getPlayerInput(); // todo: make AI system, maybe abstract out the getplayerinput method

        //velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocity.x, ref velocityXSmoothing, (accelerationTimeGround));
        //velocity.y = targetVelocity.y;

        //velocity.y += 1 * gravity * Time.deltaTime;

        //if (velocity.y > maximumVerticalVelocity) velocity.y = maximumVerticalVelocity;
        //if (velocity.y < -maximumVerticalVelocity) velocity.y = -maximumVerticalVelocity;


        Move(velocity * Time.deltaTime);

        animator.SetFloat("VelocityX", Mathf.Abs(rb.velocity.x));
        animator.SetFloat("VelocityY", rb.velocity.y);



        //animator.SetFloat("VelocityX", Mathf.Abs(velocity.x));
        //animator.SetFloat("VelocityY", velocity.y);
        spriteRenderer.flipX = velocity.x < 0;

    }


    private Vector2 getPlayerInput()
    {
        Vector2 targetVelocity = velocity;

        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        targetVelocity.x = input.x * moveSpeed;
        return targetVelocity;
    }
}

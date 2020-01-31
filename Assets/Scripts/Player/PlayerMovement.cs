using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    const float skinWidth = .015f;

    public Vector3 velocity;

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

    float accelerationTimeGround = .05f;
    private float moveSpeed;
    float velocityXSmoothing;

    protected Animator animator;
    private SpriteRenderer spriteRenderer;

    [SerializeField]
    float downGravityModifier = 1.2f;


    float horizontalRaySpacing;
    float verticalRaySpacing;


    protected BoxCollider2D boxCollider;
    RaycastOrigins raycastOrigins;


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
        CalculateRaySpacing();
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
        UpdateRaycastOrigins();
        collisions.Reset();

        if (velocity.x != 0)
            HorizontalCollisions(ref velocity);
        if (velocity.y != 0)
            VerticalCollisions(ref velocity);

        transform.Translate(velocity);
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


    void HorizontalCollisions(ref Vector3 velocity)
    {
        boxCollider.enabled = false; // TODO: performance?

        float dirX = Mathf.Sign(velocity.x);
        float rayLength = Mathf.Abs(velocity.x) + skinWidth;
        for (int i = 0; i < horizontalRayCount; i++)
        {
            Vector2 rayOrigin = (dirX == -1) ? raycastOrigins.bottomLeft : raycastOrigins.bottomRight;
            rayOrigin += Vector2.up * (verticalRaySpacing * i);
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * dirX, rayLength, collisionMask);
            if (hit)
            {
                velocity.x = (hit.distance - skinWidth) * dirX;
                rayLength = hit.distance;
                collisions.left = (dirX == -1);
                collisions.right = (dirX == 1);
            }

            if (debug)
                Debug.DrawRay(rayOrigin, Vector2.right * dirX * (rayLength) * 10.0f, Color.red);
        }
        boxCollider.enabled = true;
    }

    void VerticalCollisions(ref Vector3 velocity)
    {
        boxCollider.enabled = false;
        float dirY = Mathf.Sign(velocity.y);
        float rayLength = Mathf.Abs(velocity.y) + skinWidth;
        for (int i = 0; i < veritcalRayCount; i++)
        {
            Vector2 rayOrigin = (dirY == -1) ? raycastOrigins.bottomLeft : raycastOrigins.topLeft;
            rayOrigin += Vector2.right * (verticalRaySpacing * i + velocity.x);
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up * dirY, rayLength, collisionMask);
            if (hit)
            {
                velocity.y = (hit.distance - skinWidth) * dirY;
                rayLength = hit.distance;
                collisions.below = dirY == -1;
                collisions.above = dirY == 1;
                }
            }

            //if (debug)
            //    Debug.DrawRay(rayOrigin, Vector2.up * dirY * (rayLength) * 3.0f, Color.red);
        }


    
    void Update()
    {

    if ((collisions.below || collisions.above))
        velocity.y = 0;

    Vector2 targetVelocity = getPlayerInput(); // todo: make AI system, maybe abstract out the getplayerinput method


    velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocity.x, ref velocityXSmoothing, (accelerationTimeGround));
    velocity.y = targetVelocity.y;


    float mod = velocity.y < 0.0f ? downGravityModifier : 1 / downGravityModifier;

    velocity.y += mod * gravity * Time.deltaTime;

    if (velocity.y > maximumVerticalVelocity) velocity.y = maximumVerticalVelocity;
    if (velocity.y < -maximumVerticalVelocity) velocity.y = -maximumVerticalVelocity;

    Move(velocity * Time.deltaTime);

    animator.SetFloat("VelocityX", Mathf.Abs(velocity.x));
    animator.SetFloat("VelocityY", velocity.y);

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

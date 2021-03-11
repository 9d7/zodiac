using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterMovement : MonoBehaviour
{

    [SerializeField] private LayerMask whatIsGround;

    // To detect if the player is on the ground, we cast a box at the bottom of the player's collider.
    // However, that box has to have some width and should also not extend the full width of the player
    // (lest the boxcast collides with the wall). This parameter says how far to extend the boxcast vertically
    // as well as how far to retract it horizontally.
    [SerializeField] private float boxcastMargins;

    [Header("Control Settings")] [SerializeField] private float minJumpHeight;
    [SerializeField] private float maxJumpHeight;
    [SerializeField] private float gravityScale = 3f;

    private Rigidbody2D rbody;
    private BoxCollider2D boxCollider;

    private bool grounded;

    void Awake()
    {
        rbody = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        rbody.gravityScale = gravityScale;
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    private void FixedUpdate()
    {
        // bool wasGrounded = grounded;

        Bounds colliderPos = boxCollider.bounds;
        Vector2 point = new Vector2(colliderPos.center.x, colliderPos.min.y);
        Vector2 size = new Vector2(colliderPos.extents.x - boxcastMargins, boxcastMargins);

        Collider2D[] colliders = Physics2D.OverlapBoxAll(point, size, 0f, whatIsGround);
        grounded = (colliders.Length > 0);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnJump(InputValue value)
    {
        print(grounded);
        if (grounded && value.isPressed)
        {
            rbody.velocity = new Vector2(
                rbody.velocity.x,
                Mathf.Sqrt(-2f * Physics2D.gravity.y * gravityScale * maxJumpHeight));
        }

        if (!value.isPressed)
        {
            Vector2 vel = rbody.velocity;
            if (vel.y > 0)
            {
                rbody.velocity *= new Vector2(1f, Mathf.Sqrt(minJumpHeight / maxJumpHeight));
            }
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.WSA;

public class CharacterMovement : MonoBehaviour
{

    [SerializeField] private LayerMask whatIsGround;

    // To detect if the player is on the ground, we cast a box at the bottom of the player's collider.
    // However, that box has to have some width and should also not extend the full width of the player
    // (lest the boxcast collides with the wall). This parameter says how far to extend the boxcast vertically
    // as well as how far to retract it horizontally.
    [SerializeField] private float boxcastMargins;

    [Header("Speed Settings")] [SerializeField] private float minJumpHeight = 2f;
    [SerializeField] private float maxJumpHeight = 4.5f;
    [SerializeField] private float gravityScale = 4f;
    [Space]
    [SerializeField] private float acceleration = 60f;
    [SerializeField] private float airAcceleration = 30f;
    [SerializeField] private float maxSpeed = 15f;
    [SerializeField] [Range(0f, 0.1f)] private float stopDamping = 0.001f;

    [Header("Extra Stuff")] [SerializeField] private float coyoteTime = 0.2f;
    [SerializeField] private float bufferTime = 0.2f;


    private Rigidbody2D rbody;
    private CapsuleCollider2D capsuleCollider;

    private bool grounded;
    private float horizontalVelocity = 0.0f;

    private float timeSinceGrounded = Mathf.Infinity;
    private float timeSinceBuffered = Mathf.Infinity;

    void Awake()
    {
        rbody = GetComponent<Rigidbody2D>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        rbody.gravityScale = gravityScale;
    }

    private void FixedUpdate()
    {

        //
        // check if grounded
        //
        Bounds colliderPos = capsuleCollider.bounds;
        Vector2 point = new Vector2(colliderPos.center.x, colliderPos.min.y);
        Vector2 size = new Vector2(colliderPos.extents.x - boxcastMargins, boxcastMargins);

        Collider2D[] colliders = Physics2D.OverlapBoxAll(point, size, 0f, whatIsGround);
        grounded = false;
        if (colliders.Length > 0)
        {
            grounded = true;
            timeSinceGrounded = 0f;
        };

        //
        // try to jump
        //
        if (timeSinceBuffered < bufferTime && timeSinceGrounded < coyoteTime)
        {
            rbody.velocity = new Vector2(
                rbody.velocity.x,
                Mathf.Sqrt(-2f * Physics2D.gravity.y * gravityScale * maxJumpHeight));
            timeSinceBuffered = Mathf.Infinity;
            timeSinceGrounded = Mathf.Infinity;
        }

        //
        // update horizontal velocity
        //
        float a = grounded ? acceleration : airAcceleration;
        float xVel = rbody.velocity.x + a * horizontalVelocity * Time.fixedDeltaTime;

        if (Mathf.Approximately(horizontalVelocity, 0f) && grounded)
        {
            // damp based on stopDamping
            xVel *= Mathf.Pow(stopDamping / 60f, Time.fixedDeltaTime);
        }
        else
        {
            // damp based on maxSpeed
            xVel *= (maxSpeed / (maxSpeed + a * Time.fixedDeltaTime));
        }
        rbody.velocity = new Vector2(xVel, rbody.velocity.y);

    }

    void Update()
    {
        timeSinceBuffered += Time.deltaTime;
        timeSinceGrounded += Time.deltaTime;
    }

    void OnMove(InputValue value)
    {
        Vector2 val = value.Get<Vector2>();
        horizontalVelocity = val.x;
        print(horizontalVelocity);
    }
    void OnJump(InputValue value)
    {
        if (value.isPressed)
        {
            timeSinceBuffered = 0f;
        } else {
            Vector2 vel = rbody.velocity;
            if (vel.y > 0) // only damp if currently jumping
            {
                rbody.velocity *= new Vector2(1f, Mathf.Sqrt(minJumpHeight / maxJumpHeight));
            }
        }
    }
}

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
    [SerializeField] private int numberOfJumps = 1;

    private int timesJumped = 0;

    private Rigidbody2D rbody;
    private Collider2D _collider;

    private bool _grounded;
    public bool grounded => _grounded;
    private bool spacePressed;
    private float horizontalVelocity = 0.0f;

    private float timeSinceGrounded = Mathf.Infinity;
    private float timeSinceBuffered = Mathf.Infinity;

    public float health = 10f;
    public float underWaterTime = 3f;

    public bool waterproof = false;
    public bool spikeproof = false;

    private MainMenu menuControl;

    private SpriteRenderer pic;

    public Vector3 lastGroundedPosition;
    void Awake()
    {
        rbody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<BoxCollider2D>();
        rbody.gravityScale = gravityScale;
        menuControl = GameObject.FindObjectOfType<MainMenu>();
        timesJumped = numberOfJumps;
        pic = this.GetComponent<SpriteRenderer>();
    }

    public float GetHorizontalInput()
    {
        return horizontalVelocity;
    }

    public void SetHorizontalInput(float h)
    {
        horizontalVelocity = h;
    }

    private void FixedUpdate()
    {

        rbody.gravityScale = gravityScale;

        //
        // check if grounded
        //
        Bounds colliderPos = _collider.bounds;
        Vector2 point = new Vector2(colliderPos.center.x, colliderPos.min.y);
        Vector2 size = new Vector2(colliderPos.extents.x - boxcastMargins, boxcastMargins);

        Collider2D[] colliders = Physics2D.OverlapBoxAll(point, size, 0f, whatIsGround);
        if (colliders.Length > 0)
        {
            if (!grounded && timeSinceGrounded > 0.4f)
            {
                //SfxManager.PlaySound("land", transform.position);
                SfxManager.PlaySound("land", transform.position);
            }
            _grounded = true;
            timeSinceGrounded = 0f;
            timesJumped = 0;
        }
        else
        {
            _grounded = false;
        };
        
        

        //
        // try to jump
        //
        if (timeSinceBuffered < bufferTime && (timesJumped < numberOfJumps || timeSinceGrounded < coyoteTime))
        {
            timesJumped++;
            rbody.velocity = new Vector2(
                rbody.velocity.x,
                Mathf.Sqrt(-2f * Physics2D.gravity.y * gravityScale * maxJumpHeight));
            timeSinceBuffered = Mathf.Infinity;
            timeSinceGrounded = Mathf.Infinity;

            if (!spacePressed)
            {
                rbody.velocity *= new Vector2(1f, Mathf.Sqrt(minJumpHeight / maxJumpHeight));
            }

            SfxManager.PlaySound("jump", transform.position);
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
            xVel *= (maxSpeed / (maxSpeed + a * Time.fixedDeltaTime * 0.5f));
        }
        rbody.velocity = new Vector2(xVel, rbody.velocity.y);

    }

    void Update()
    {
        timeSinceBuffered += Time.deltaTime;
        timeSinceGrounded += Time.deltaTime;

        
        if(rbody.velocity.x < 0)
        {
            pic.flipX = true;
        }
        else
        {
            pic.flipX = false;
        }
    }


    public void Reset()
    {
        transform.position = lastGroundedPosition;
    }

    void OnMove(InputValue value)
    {
        Vector2 val = value.Get<Vector2>();
        horizontalVelocity = val.x;
    }
    void OnJump(InputValue value)
    {
        spacePressed = value.isPressed;
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "water")
        {
            Debug.Log("water");
            menuControl.GameEnd(false);
        }
        if (collision.gameObject.tag == "spike")
        {
            if (spikeproof)
            {
                return;
            }
            else
            {
                Debug.Log("spike");
                menuControl.GameEnd(false);
            }
            
        }
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterMovement_simple : MonoBehaviour
{
    [SerializeField] private LayerMask platformLayer;
    private Rigidbody2D rbody;
    private Collider2D _collider;
    private SpriteRenderer pic;
    private MainMenu menuControl;

    public float speed = 3f;
    public float jumpSpeed = 5f;

    private float horizontalVelocity = 0.0f;
    private float verticalVelocity = 0.0f;

    public int maxJumpTime = 1;
    private int jumpLeft;

    private float actionBufferTime;
    private bool onGround;
    private float timeSinceGrounded = 0;
    
    private Vector3 lastGroundedPosition;
    [SerializeField] private int maxJumps = 1;


    public bool CanTransform()
    {
        return onGround;
    }
    public bool spikeproof = false;

    void Awake()
    {
        rbody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<BoxCollider2D>();
        pic = GetComponent<SpriteRenderer>();
        menuControl = GameObject.FindObjectOfType<MainMenu>();
        actionBufferTime = 0;
        onGround = false;
        lastGroundedPosition = transform.position;
    }
    
    

    private bool wasGroundedLastFrame = true;
    private void Update()
    {
        bool grounded = IsGrounded();
        timeSinceGrounded += Time.deltaTime;
        
        
        if (actionBufferTime > 0)
        {
            actionBufferTime -= Time.deltaTime;
        }else
        {
            if (IsGrounded() && !onGround)
            {
                actionBufferTime = 0.2f;
                onGround = true;
                if (rbody.velocity.magnitude > 11)
                {
                    SfxManager.PlaySound("land", transform.position);
                }
                
            }
        }
        if (grounded && !wasGroundedLastFrame)
        {
            timeSinceGrounded = 0;
            
        }

        if (grounded && timeSinceGrounded > 0.2f)
        {
            jumpLeft = maxJumps;
            lastGroundedPosition = transform.position;
            
        }
        wasGroundedLastFrame = grounded;
    }

    public void Reset()
    {
        transform.position = lastGroundedPosition;
    }

    private void FixedUpdate()
    {
        
        rbody.velocity = new Vector2(horizontalVelocity, rbody.velocity.y);
        /*
        if (Mathf.Abs(rbody.velocity.y) > 15)
        {
            //Debug.Log(Mathf.Abs(rbody.velocity.y));
            onGround = false;
        }
        */
    }

    void OnMove(InputValue value)
    {
        Vector2 val = value.Get<Vector2>();
        if (val.x != 0) pic.flipX = val.x < 0;
        horizontalVelocity = val.x * speed;
    }

    void OnJump(InputValue value)
    {
        bool spacePressed = value.isPressed;
        if (spacePressed && jumpLeft > 0)
        {
            rbody.velocity = new Vector2(rbody.velocity.x, jumpSpeed);
            actionBufferTime = 0.2f;
            jumpLeft--;
            onGround = false;
            SfxManager.PlaySound("jump", transform.position);
        }
    }

    public bool IsGrounded()
    {
        float extraHeight = 0.1f;
        RaycastHit2D raycastHit = Physics2D.BoxCast(_collider.bounds.center, _collider.bounds.size, 0f, Vector2.down, extraHeight, platformLayer);
        return raycastHit.collider != null;
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

using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterMovement_simple : MonoBehaviour
{
    [SerializeField] private LayerMask platformLayer;
    private Rigidbody2D rbody;
    private Collider2D _collider;
    public float boxcastMargins = 0.1f;
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

    public ParticleSystem JumpEffect;
    public ParticleSystem LandEffect;


    public bool CanTransform()
    {
        return onGround;
    }
    public bool spikeproof = false;
    private float lastJumpTime;

    void Awake()
    {
        rbody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<BoxCollider2D>();
        pic = GetComponent<SpriteRenderer>();
        menuControl = GameObject.FindObjectOfType<MainMenu>();
        actionBufferTime = 0;
        onGround = false;
        jumpLeft = maxJumps;
        lastGroundedPosition = transform.position;
    }
    
    

    private bool wasGroundedLastFrame = true;
    private void Update()
    {
        
    }

    public void Reset()
    {
        transform.position = lastGroundedPosition;
        SfxManager.PlaySound("lose");
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
                LandEffect.Play();
            }
        }
        if (grounded && !wasGroundedLastFrame)
        {
            StartCoroutine(SetGroundedPosInTime(0.2f));
            timeSinceGrounded = 0;
            jumpLeft = maxJumps;

            if (!GetPlatform().GetComponent<HingeJoint2D>() && (!GetPlatform().GetComponent<Rigidbody2D>() || GetPlatform().GetComponent<Rigidbody2D>().bodyType == RigidbodyType2D.Static))
            {
                transform.SetParent(GetPlatform());
            }
            
            
        }else if (!grounded && wasGroundedLastFrame)
        {
            transform.SetParent(null);
        }

      
        
        wasGroundedLastFrame = grounded;
    }

    IEnumerator SetGroundedPosInTime(float _t)
    {
        Vector3 pos = transform.position;
        bool wasGrounded = true;
        for(float t = 0; t < _t; t += Time.deltaTime)
        {
            yield return new WaitForEndOfFrame();
            if (!IsGrounded())
            {
                wasGrounded = false;
                break;
            }
        }

        if (wasGrounded)
        {
            lastGroundedPosition = pos;
        }
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
            if (Time.time - lastJumpTime < 0.1f)
            {
                return;
            }
            lastJumpTime = Time.time;
            rbody.velocity = new Vector2(rbody.velocity.x, jumpSpeed);
            actionBufferTime = 0.2f;
            jumpLeft--;
            onGround = false;
            SfxManager.PlaySound("jump", transform.position);
            JumpEffect.Play();
        }
    }

    public Transform GetPlatform()
    {
        float extraHeight = boxcastMargins;
        Bounds colliderPos = _collider.bounds;

        RaycastHit2D raycastHit = Physics2D.Raycast(_collider.bounds.center, Vector2.down, _collider.bounds.extents.y + extraHeight, platformLayer);
        return raycastHit.collider.transform;
    }

    public bool IsGrounded()
    {
        float extraHeight = boxcastMargins;
        Bounds colliderPos = _collider.bounds;

        RaycastHit2D raycastHit = Physics2D.Raycast(_collider.bounds.center, Vector2.down, _collider.bounds.extents.y + extraHeight, platformLayer);
        return raycastHit.collider != null;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "water")
        {
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
                menuControl.GameEnd(false);
            }

        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
    }
}

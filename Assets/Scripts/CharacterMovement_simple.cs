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

    public float speed = 3f;
    public float jumpSpeed = 5f;

    private float horizontalVelocity = 0.0f;
    private float verticalVelocity = 0.0f;

    public int maxJumpTime = 1;
    private int jumpLeft;

    private float actionBufferTime;
    private bool onGround;


    void Awake()
    {
        rbody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<BoxCollider2D>();
        pic = GetComponent<SpriteRenderer>();
        actionBufferTime = 0;
        onGround = false;
    }

    private void Update()
    {
        if (actionBufferTime > 0)
        {
            actionBufferTime -= Time.deltaTime;
        }else
        {
            if (IsGrounded() && !onGround)
            {
                actionBufferTime = 0.2f;
                jumpLeft = maxJumpTime;
                onGround = true;
                SfxManager.PlaySound("land", transform.position);
            }
        }
    }

    private void FixedUpdate()
    {
        rbody.velocity = new Vector2(horizontalVelocity, rbody.velocity.y);
        if (Mathf.Abs(rbody.velocity.y) > 10)
        {
            //Debug.Log(Mathf.Abs(rbody.velocity.y));
            onGround = false;
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
            rbody.velocity = new Vector2(rbody.velocity.x, jumpSpeed);
            actionBufferTime = 0.2f;
            jumpLeft--;
            onGround = false;
            SfxManager.PlaySound("jump", transform.position);
        }
    }

    private bool IsGrounded()
    {
        float extraHeight = 0.1f;
        RaycastHit2D raycastHit = Physics2D.BoxCast(_collider.bounds.center, _collider.bounds.size, 0f, Vector2.down, extraHeight, platformLayer);
        return raycastHit.collider != null;
    }
}

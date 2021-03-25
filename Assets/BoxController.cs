using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxController : MonoBehaviour
{
    private const int MAX_COLLIDERS = 1;
    private Rigidbody2D rbody;
    [SerializeField]
    private float checkRadius;
    [SerializeField]
    private float checkGroundedDistance;

    [SerializeField] private LayerMask playerMask;
    [SerializeField] private LayerMask groundMask;

    private void Awake()
    {
        rbody = GetComponent<Rigidbody2D>();
        rbody.isKinematic = true;
    }

    private void FixedUpdate()
    {
        rbody.isKinematic = checkNearCharacter() && checkGrounded();
    }

    private bool checkNearCharacter()
    {
        Collider2D[] hits = new Collider2D[MAX_COLLIDERS];
        if(Physics2D.OverlapCircleNonAlloc(transform.position, checkRadius, hits, playerMask) > 0)
        {
            if (!hits[0].GetComponent<BoxPushController>())
            {
                return true;
            }
        }
        return false;
    }

    private bool checkGrounded()
    {
        RaycastHit2D[] hits = new RaycastHit2D[MAX_COLLIDERS];
        return Physics2D.RaycastNonAlloc(transform.position, Vector2.down, hits, checkGroundedDistance,
            groundMask) > 0;

    }
}

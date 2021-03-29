using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSound : MonoBehaviour
{
    [SerializeField] private float distanceBetweenFootsteps = 1.0f;

    // Start is called before the first frame update
    private CharacterMovement movement;
    private Vector3 lastPosition;
    private float totalMoved = 0f;
    void Start()
    {
        movement = GetComponent<CharacterMovement>();
        lastPosition = transform.position;
    }

    private void FixedUpdate()
    {
        if (movement.grounded)
        {
            totalMoved += (transform.position - lastPosition).magnitude;
        }
        else
        {
            totalMoved *= 0.99f;
        }

        lastPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        while (totalMoved > distanceBetweenFootsteps)
        {
            totalMoved -= distanceBetweenFootsteps;
            SfxManager.PlaySound("footstep");
        }
    }
}

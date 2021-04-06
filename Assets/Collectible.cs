using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<CharacterMovement_simple>())
        {
            SfxManager.PlaySound("coin");
            CounterManager.Instance.Increment();
            Destroy(gameObject);
        }
    }

}

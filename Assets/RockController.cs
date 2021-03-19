using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockController : MonoBehaviour
{
    private Collider2D playerColl;
    
    [SerializeField]
    private float timeUntilBreak;
    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.collider.GetComponent<BoxPushController>())
        {
            playerColl = other.collider;
            StopAllCoroutines();
            StartCoroutine(BreakRockInTime());
        }
    }

    private IEnumerator BreakRockInTime()
    {
        yield return new WaitForSeconds(timeUntilBreak);
        Destroy(gameObject);
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.collider == playerColl)
        {
            StopAllCoroutines();
        }
    }
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingPlatform : MonoBehaviour
{
    public Vector2 offset;
    // Start is called before the first frame update
    private float startTime;
    void Start()
    {
        startTime = Time.time;
        pointA = transform.position;
        pointB = (Vector2)(transform.position) + offset;
    }

    private Vector2 pointA;

    private Vector2 pointB;
    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.Lerp(pointA, pointB, (Mathf.Sin(Time.time - startTime) + 1) / 2);
    }
}

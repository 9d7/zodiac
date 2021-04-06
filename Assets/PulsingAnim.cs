using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulsingAnim : MonoBehaviour
{
    private SpriteRenderer sr;

    [SerializeField] private Color color1;

    [SerializeField] private Color color2;
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        sr.color = Color.Lerp(color1, color2, Mathf.PingPong(Time.time, 1));
    }
}

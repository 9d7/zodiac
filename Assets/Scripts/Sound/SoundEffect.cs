using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffect : MonoBehaviour
{
    private AudioSource src;

    void Start()
    {
        src = GetComponent<AudioSource>();
    }
    void Update()
    {
        if (!src.isPlaying) Destroy(gameObject);
    }
}

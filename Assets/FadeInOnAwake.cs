using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeInOnAwake : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Animator>().SetTrigger("FadeIn");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

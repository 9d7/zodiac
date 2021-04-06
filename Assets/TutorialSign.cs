using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialSign : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    // Start is called before the first frame update
    void Start()
    {
        canvas.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<CharacterMovement>() || other.GetComponent<CharacterMovement_simple>())
        {
            canvas.gameObject.SetActive(true);
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<CharacterMovement>() || other.GetComponent<CharacterMovement_simple>())
        {
            canvas.gameObject.SetActive(false);
        }
    }
}

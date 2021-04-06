using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialSign : MonoBehaviour
{
    [SerializeField] private Canvas canvas;

    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<CharacterMovement>() || other.GetComponent<CharacterMovement_simple>())
        {
            anim.SetTrigger("FadeIn");
            SfxManager.PlaySound("tutorialShow");
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<CharacterMovement>() || other.GetComponent<CharacterMovement_simple>())
        {
            anim.SetTrigger("FadeOut");
            SfxManager.PlaySound("tutorialHide");
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Experimental.Rendering.Universal;

public class Switch : MonoBehaviour
{
    public bool isButton;
    public List<GameObject> objectsToTrigger;
    public bool requiresSpecificCharacter;
    public string specificCharacterName;
    private SpriteRenderer pic;
    [SerializeField] private Sprite activated;
    [SerializeField] private Sprite deactivated;
    [SerializeField] private Light2D light;

    public bool flipLight;
    private bool isTouching = false;

    private void Start()
    {
        pic = this.GetComponent<SpriteRenderer>();
        foreach (GameObject gameObject in objectsToTrigger)
        {
            gameObject.SendMessage("SwitchRegister");
        }

        light.enabled = flipLight ? true : false;
        pic.sprite = deactivated;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (requiresSpecificCharacter && !other.gameObject.CompareTag(specificCharacterName)) return;

        if (!isTouching)
        {
            foreach (GameObject gameObject in objectsToTrigger)
            {
                gameObject.SendMessage("SwitchPress");
            }
            //when switch is triggered, faded.
            //pic.color = new Color(pic.color.r, pic.color.g, pic.color.b, 0.1F);
            isTouching = true;
            light.enabled = flipLight ? false : true;
            pic.sprite = activated;
        }


    }

    // not used for now
    private void OnTriggerExit2D(Collider2D other)
    {
        if (requiresSpecificCharacter && !other.gameObject.CompareTag(specificCharacterName)) return;
        if (!isButton) return;

        if (isTouching)
        {
            foreach (GameObject gameObject in objectsToTrigger)
            {
                gameObject.SendMessage("SwitchRelease");
            }

            pic.sprite = deactivated;
            isTouching = false;
            light.enabled = flipLight ? true : false;
        }
    }
}
